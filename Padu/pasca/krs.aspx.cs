using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace Padu.pasca
{
    public partial class krs : MhsPasca
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        public decimal _PrevIPS
        {
            get { return Convert.ToDecimal(this.ViewState["PrevIPS"].ToString()); }
            set { this.ViewState["PrevIPS"] = (object)value; }
        }

        public int _MaxKRS
        {
            get { return Convert.ToInt32(this.ViewState["MaxKRS"].ToString()); }
            set { this.ViewState["MaxKRS"] = (object)value; }
        }

        public int _TotalSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        public int _TotalEditSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalEditSKS"].ToString()); }
            set { this.ViewState["TotalEditSKS"] = (object)value; }
        }

        public String _JenisKRS
        {
            get { return this.ViewState["JenisKRS"].ToString(); }
            set { this.ViewState["JenisKRS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavMasterKRS");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavMasterKRS");
                control2.Attributes.Add("style", "display: block;");

                _TotalSKS = 0;
                _TotalEditSKS = 0;
                _PrevIPS = 0;
                _MaxKRS = 0;

                try
                {
                    //----- read data mahasiswa ------
                    this.PanelKRS.Visible = false;
                    this.PanelMhs.Visible = false;

                    mhs.ReadMahasiswa(this.Session["Name"].ToString());

                    LbNpm.Text = this.Session["Name"].ToString();
                    LbNama.Text = mhs.nama.ToString();
                    LbKelas.Text = mhs.kelas.ToString();
                    LbKdProdi.Text = mhs.id_prodi.ToString();
                    LbProdi.Text = mhs.Prodi.ToString();
                    LbTahun.Text = mhs.thn_angkatan.ToString();

                }
                catch (Exception)
                {
                    LbNama.Text = "";
                    LbKelas.Text = "";
                    LbProdi.Text = "";
                    LbNpm.Text = "";
                    LbKdProdi.Text = "";

                    this.PanelKRS.Visible = false;

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                    string message = "alert('Mahasiswa tidak ditemukan')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }

        protected void GVAmbilKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            this.LbJumlahSKS.Text = "0";

            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
            //e.Row.Cells[8].Visible = false; 
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int BiayaSks = Convert.ToInt32(e.Row.Cells[1].Text);
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // ------ Cek Sisa Quota -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpQuotaJadwal", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[2].Text);

                        SqlParameter Quota = new SqlParameter();
                        Quota.ParameterName = "@quota";
                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                        Quota.Size = 20;
                        Quota.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Quota);

                        SqlParameter Sisa = new SqlParameter();
                        Sisa.ParameterName = "@sisa";
                        Sisa.SqlDbType = System.Data.SqlDbType.Int;
                        Sisa.Size = 20;
                        Sisa.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Sisa);

                        CmdCekMasa.ExecuteNonQuery();

                        if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = Sisa.Value.ToString();
                            LbSisa.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = "Penuh";
                            LbSisa.ForeColor = System.Drawing.Color.Red;
                            CheckBox ch = (CheckBox)e.Row.Cells[0].FindControl("CBMakul");
                            ch.Visible = false;
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah");
                }
            }
        }


        int PrevTotalSKS;
        int JumlahSKS;
        protected void CBMakul_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[index].FindControl("CBMakul");
            if (ch.Checked == true)
            {
                PrevTotalSKS = _TotalSKS;
                _TotalSKS += Convert.ToInt16(this.GVAmbilKRS.Rows[index].Cells[5].Text);
                this.GVAmbilKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                this.LbJumlahSKS.Text = _TotalSKS.ToString();

                //----- background makul checked ----- //
                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                    if (ch2.Checked == true)
                    {
                        this.GVAmbilKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                    }
                }

                // batas maximal KRS
                if (_TotalSKS > _MaxKRS)
                {
                    ch.Checked = false;
                    this.LbJumlahSKS.Text = PrevTotalSKS.ToString();
                    this.GVAmbilKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFCEC1");
                    _TotalSKS = PrevTotalSKS;
                }
            }
            else
            {
                ////----- hitung ulang sks berdasarkan makul checked ----- //
                //for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                //{
                //    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                //    if (ch2.Checked == true)
                //    {
                //        JumlahSKS += Convert.ToInt32(this.GVAmbilKRS.Rows[i].Cells[5].Text);
                //        _TotalSKS = JumlahSKS;
                //        this.LbJumlahSKS.Text = JumlahSKS.ToString();
                //    }
                //}

                ////----- background makul checked ----- //
                //for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                //{
                //    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                //    if (ch2.Checked == true)
                //    {
                //        this.GVAmbilKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                //    }
                //}

                //----- Cek Apakah semua makul UnChecked ----- //
                bool ischecked = false;
                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                {
                    CheckBox ch0 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                    if (ch0.Checked == true)
                    {
                        // Tidak ada jadwal yang dipilih 
                        ischecked = true;
                    }
                }

                if (ischecked == true)
                {
                    //----- hitung ulang sks ( makul checked ) ----- //
                    for (int j = 0; j < GVAmbilKRS.Rows.Count; j++)
                    {
                        CheckBox ch3 = (CheckBox)GVAmbilKRS.Rows[j].FindControl("CBMakul");
                        if (ch3.Checked == true)
                        {
                            JumlahSKS += Convert.ToInt32(this.GVAmbilKRS.Rows[j].Cells[5].Text);
                            _TotalSKS = JumlahSKS;
                            this.LbJumlahSKS.Text = JumlahSKS.ToString();
                        }
                    }

                    //----- background makul checked ----- //
                    for (int j = 0; j < GVAmbilKRS.Rows.Count; j++)
                    {
                        CheckBox ch4 = (CheckBox)GVAmbilKRS.Rows[j].FindControl("CBMakul");
                        if (ch4.Checked == true)
                        {
                            this.GVAmbilKRS.Rows[j].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                        }
                    }
                }
                else
                {
                    // tidak ada jadwal yg dipilih
                    _TotalSKS = 0;
                    this.LbJumlahSKS.Text = "0";
                }

            }
        }

        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);

            // cek checkbox pilih matakuliah
            Boolean val = false;

            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                if (ch.Checked == true)
                {
                    val = true;
                    break;
                }
            }

            // all checkbox did not check
            if (val == false)
            {
                string message = "alert('Pilih Mata Kuliah')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            // ---- ================== Cek Makul Dobel ================== --- //
            string[] arr = new string[1000];
            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
            {
                //makul checked
                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                if (ch.Checked == true)
                {
                    if (!arr.Contains(GVAmbilKRS.Rows[i].Cells[3].Text))
                    {
                        arr[i] = GVAmbilKRS.Rows[i].Cells[3].Text;
                    }
                    else
                    {
                        //duplicate
                        // clear ceklist jadwal
                        for (int j = 0; j < this.GVAmbilKRS.Rows.Count; j++)
                        {
                            CheckBox ch2 = (CheckBox)this.GVAmbilKRS.Rows[j].FindControl("CBMakul");
                            ch2.Checked = false;
                        }

                        // set jumlah sks = 0
                        this.LbJumlahSKS.Text = "0";
                        _TotalSKS = 0;

                        string message = "alert('Dobel Mata Kuliah, Proses Dibatalkan ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Dobel Mata Kuliah, Proses Dibatalkan ...');", true);
                        return;
                    }
                }
            }


            string TahunAngkatan = "";
            TahunAngkatan = this.LbTahun.Text.Trim().Substring(0, 4);
            int ThnAngkatan = Convert.ToInt16(TahunAngkatan);

            int MhsBaru = 0;
            string TahunMhs = "";

            //------------- Get Mahasiswa Tahun Paling Akhir/Baru  --------------------- //
            string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSMaba))
            {
                con.Open();
                SqlCommand CmdCekStatus = new SqlCommand("SELECT TOP (1) bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study "+
                    "FROM            bak_mahasiswa INNER JOIN "+
                                             "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                    "WHERE jenjang = 'S2' AND dbo.bak_mahasiswa.id_prog_study = @IdProdi "+
                    "GROUP BY bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study "+
                    "ORDER BY bak_mahasiswa.thn_angkatan DESC", con);
                CmdCekStatus.CommandType = System.Data.CommandType.Text;

                CmdCekStatus.Parameters.AddWithValue("@IdProdi", this.LbKdProdi.Text.Trim());

                using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            TahunMhs = rdr["thn_angkatan"].ToString();
                        }
                    }
                }
            }

            MhsBaru = Convert.ToInt16(TahunMhs.Substring(0, 4));

            // =========== BUKAN MAHASISWA BARU ==============//
            if (ThnAngkatan != MhsBaru)
            {
                string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                string CSPasca = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;

                SqlConnection ConUntidar = new SqlConnection(CSUntidar);
                SqlConnection ConPasca = new SqlConnection(CSPasca);

                ConUntidar.Open();
                ConPasca.Open();

                var TransUntidar = ConUntidar.BeginTransaction();
                var TransPasca = ConPasca.BeginTransaction();

                try
                {
                    // 1. ---------- Cek Masa KRS (difilter pd saat input KRS) -------
                       
                    // 2. ---------- Update Data KHS ----------
                    string thn = "";
                    string smstr = "";
                    if (this.DLSemester.SelectedValue == "1")
                    {
                        int tahun = Convert.ToInt32(this.DLTahun.SelectedValue);
                        tahun = tahun - 1;
                        thn = Convert.ToString(tahun);
                        smstr = "2";
                    }
                    else if (this.DLSemester.SelectedValue == "2")
                    {
                        thn = DLTahun.SelectedValue;
                        smstr = "1";
                    }

                    SqlCommand CmdUpKHS = new SqlCommand("SpTranKuliahMhs2", ConUntidar, TransUntidar);
                    CmdUpKHS.Transaction = TransUntidar;
                    CmdUpKHS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdUpKHS.Parameters.AddWithValue("@semester", thn + smstr);
                    CmdUpKHS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdUpKHS.ExecuteNonQuery();

                    // 3. ---------- Cek Ambil KRS TESIS ----------
                    int TotalChecked = 0;
                    for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                        if (ch.Checked == true)
                        {
                            TotalChecked += 1;
                        }
                    }
                    if (TotalChecked == 1)
                    {
                        try
                        {
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    // ---------- INSERT TAGIHAN TESIS (+Post Tagihan) ----------
                                    SqlCommand CmdSkripsi = new SqlCommand(""+
                                    "DECLARE @NO INT "+
                                    "DECLARE @Makul NVARCHAR(100) "+
                                    "DECLARE @MakulAkhir VARCHAR(20) "+
                                    "DECLARE @SKS INT "+

                                    "SELECT @NO = no, @Makul = makul, @SKS = sks, @MakulAkhir = makul_akhir FROM UntidarDb.dbo.bak_makul WHERE kode_makul = @KodeMakul "+
                                    "IF(@MakulAkhir = 'yes') "+
                                    "BEGIN "+
                                        "DECLARE @NoNIMTesis VARCHAR(12) " +
                                        "DECLARE @Th_Angkatan varchar(9) "+
                                        "DECLARE @KELAS VARCHAR(15) "+
                                        "DECLARE @IDPRODI VARCHAR(6) "+
                                        "DECLARE @PRODI VARCHAR(60) "+
                                        "DECLARE @NAMA VARCHAR(70) "+

                                        "SELECT      @NoNIMTesis = bak_mahasiswa.npm, @NAMA = bak_mahasiswa.nama, @Th_Angkatan = bak_mahasiswa.thn_angkatan, @KELAS = bak_mahasiswa.kelas, @IDPRODI = bak_mahasiswa.id_prog_study, @PRODI = bak_prog_study.prog_study "+
                                        "FROM        UntidarDb.dbo.bak_mahasiswa INNER JOIN "+
                                                    "UntidarDb.dbo.bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                                        "WHERE       npm = @NoNPM "+

                                        "DECLARE @ID INT "+
                                        "DECLARE @SPP DECIMAL "+
                                        "DECLARE @SARDIK DECIMAL "+
                                        "DECLARE @BIAYA DECIMAL "+

                                        "SELECT @ID = id_biaya, @SPP = SPP, @SARDIK = sardik FROM dbo.keu_biaya WHERE thn_angkatan = @Th_Angkatan AND id_prog_study = @IDPRODI "+
                                        "IF(@@ROWCOUNT = 0) "+
                                        "BEGIN "+
                                            "RAISERROR('Tagihan Periodik Ini Belum Dibuat ...', 16, 10) "+
                                            "RETURN "+
                                        "END "+

                                        "SET @BIAYA = @SPP "+

                                        "SELECT * FROM dbo.keu_posting_bank WHERE payeeId = @NoNPM AND billRef5 = 'TESIS' "+
                                        "IF(@@ROWCOUNT >= 1) "+
                                        "BEGIN "+
                                            "RAISERROR('Posting Tagihan Skripsi Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) "+
                                            "RETURN "+
                                        "END "+

                                        "SELECT * FROM keu_tagihan WHERE npm = @NoNPM AND jenis_tagihan = 'TESIS' "+

                                        "IF(@@ROWCOUNT >= 1) "+
                                        "BEGIN "+
                                            "RAISERROR('Tagihan Skripsi Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) "+
                                            "RETURN "+
                                        "END "+
                                        "ELSE "+
                                        "BEGIN "+

                                            "INSERT INTO keu_tagihan(npm, biaya, jenis_tagihan, semester, tgl) "+
                                            "VALUES(@NoNPM, @BIAYA, 'TESIS', @Semester, GETDATE()) "+
                                        "END "+

                                        "DECLARE @nomorTesis INT "+
                                        "DECLARE @NewNomorTesis VARCHAR(50) "+
                                        "DECLARE @PreFixTesis VARCHAR(10) = '10' "+

                                        "SELECT @nomorTesis = ISNULL(MAX(keu_posting_bank.nomor), 0) + 1 from keu_posting_bank "+
                                        "SELECT @NewNomorTesis = @PreFixTesis + RIGHT('00000000' + CAST(@nomorTesis AS VARCHAR(8)), 8) "+

                                        "INSERT INTO bpd.dbo.keu_posting_bank "+
                                                "(billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,cicilan,post_date, status, keterangan) "+
                                        "VALUES(@NewNomorTesis,  @NoNPM, @NAMA, @PRODI, @KELAS, @Th_Angkatan, @semester, 'TESIS', @BIAYA, NULL, GETDATE(), 'unpaid', 'BIAYA TESIS') "+

                                        "INSERT INTO dbo.keu_posting_bank "+
                                                "(billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,cicilan,post_date, status, keterangan) " +
                                        "VALUES(@NewNomorTesis,  @NoNPM, @NAMA, @PRODI, @KELAS, @Th_Angkatan, @semester, 'TESIS', @BIAYA, NULL, GETDATE(), 'unpaid', 'BIAYA TESIS') " +
                                    "END "+
                                    "ELSE "+
                                    "BEGIN "+
                                        "RAISERROR('Buka Mata Kuliah Akhir, Proses Dibatalkan...', 16, 10) "+
                                        "RETURN "+
                                    "END "+
                                    "", ConPasca, TransPasca);
                                    CmdSkripsi.CommandType = System.Data.CommandType.Text;

                                    CmdSkripsi.Parameters.AddWithValue("@KodeMakul", GVAmbilKRS.Rows[i].Cells[3].Text);
                                    CmdSkripsi.Parameters.AddWithValue("@Semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                    CmdSkripsi.Parameters.AddWithValue("@NoNPM", this.Session["Name"].ToString());

                                    //SqlParameter Biaya = new SqlParameter();
                                    //Biaya.ParameterName = "@TotalBiaya";
                                    //Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                                    //Biaya.Size = 20;
                                    //Biaya.Direction = System.Data.ParameterDirection.Output;
                                    //CmdSkripsi.Parameters.Add(Biaya);

                                    CmdSkripsi.ExecuteNonQuery();

                                    //decimal TotalBiaya;
                                    //TotalBiaya = Convert.ToDecimal(Biaya.Value.ToString());

                                    //string FormattedString9 = string.Format
                                    //    (new System.Globalization.CultureInfo("id"), "{0:c}", TotalBiaya);
                                    //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                    //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                    // ---- ---------- INSERT KRS TESIS ---------- ----
                                    // ---- Ambil KRS BIASA (1 Mata Kuliah) Tidak Disarankan
                                    //----- -------------------------------------- -----
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", ConUntidar, TransUntidar);
                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                    cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);
                                    cmdInKRS.ExecuteNonQuery();

                                    // ----- Simpan Perubahan -----
                                    TransUntidar.Commit();
                                    TransPasca.Commit();
                                    TransUntidar.Dispose();
                                    TransPasca.Dispose();
                                    ConPasca.Close();
                                    ConUntidar.Close();
                                    ConUntidar.Dispose();
                                    ConPasca.Dispose();

                                    string message = "alert('KRS TESIS Berhasil ...')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Skripsi Berhasil ...');", true);
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TransUntidar.Rollback();
                            TransPasca.Rollback();
                            TransUntidar.Dispose();
                            TransPasca.Dispose();
                            ConPasca.Close();
                            ConUntidar.Close();
                            ConUntidar.Dispose();
                            ConPasca.Dispose();

                            //Response.Write(ex.ToString());

                            string message = "alert('" + ex.Message + "')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                        return;
                    }
                    else
                    {
                        // ------------- KRS Biasa -------------------
                        // -------------------------------------------
                        // 4.-- Loop insert no jadwal (INPUT KRS) --
                        for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            if (ch.Checked == true)
                            {
                                SqlCommand cmdInKRS = new SqlCommand("SpInKRS", ConUntidar, TransUntidar);

                                cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                cmdInKRS.ExecuteNonQuery();
                            }
                        }


                        //5.-- POSTING tahihan to BANK by using SpInsertPostingPerMhs --
                        SqlCommand CmdPost = new SqlCommand(""+
                            "DECLARE @NO2 INT "+
                            "DECLARE @Makul2 NVARCHAR(100) "+
                            "DECLARE @MakulAkhir2 VARCHAR(20) "+
                            "DECLARE @SKS2 INT "+

                            "SELECT @NO2 = no, @Makul2 = makul, @SKS2 = sks, @MakulAkhir2 = makul_akhir FROM UntidarDb.dbo.bak_makul WHERE kode_makul = @KodeMakul2 "+

                            "IF(@MakulAkhir2 = 'yes') "+
                            "BEGIN "+
                                "DECLARE @NoNIM_KRS VARCHAR(12) "+
                                "DECLARE @Th_Angkatan_KRS varchar(9) "+
                                "DECLARE @KELAS_KRS VARCHAR(15) "+
                                "DECLARE @IDPRODI_KRS VARCHAR(6) "+
                                "DECLARE @PRODI_KRS VARCHAR(60) "+
                                "DECLARE @NAMA_KRS VARCHAR(70) "+

                                "SELECT      @NoNIM_KRS = bak_mahasiswa.npm, @NAMA_KRS = bak_mahasiswa.nama, @Th_Angkatan_KRS = bak_mahasiswa.thn_angkatan, @KELAS_KRS = bak_mahasiswa.kelas, @IDPRODI_KRS = bak_mahasiswa.id_prog_study, @PRODI_KRS = bak_prog_study.prog_study "+
                                "FROM        UntidarDb.dbo.bak_mahasiswa INNER JOIN "+ 
                                            "UntidarDb.dbo.bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                                "WHERE       npm = @NoNPM2 "+
                                 
                                "DECLARE @ID2 INT "+
                                "DECLARE @SPP2 DECIMAL "+
                                "DECLARE @SARDIK2 DECIMAL "+
                                "DECLARE @BIAYA2 DECIMAL "+

                                "SELECT @ID2 = id_biaya, @SPP2 = SPP, @SARDIK2 = sardik FROM dbo.keu_biaya WHERE thn_angkatan = @Th_Angkatan_KRS AND id_prog_study = @IDPRODI_KRS "+
                                "IF(@@ROWCOUNT = 0) "+
                                "BEGIN "+
                                    "RAISERROR('Tagihan Periodik Ini Belum Dibuat ...', 16, 10) "+
                                    "RETURN "+
                                "END "+

                                "SET @BIAYA2 = @SPP2 + @SARDIK2 "+

                                "SELECT * FROM dbo.keu_posting_bank WHERE payeeId = @NoNPM2 AND billRef4 = @Semester2 AND keterangan = 'ANGSURAN SEMESTER' "+
                                "IF(@@ROWCOUNT >= 1) "+
                                "BEGIN "+
                                    "RAISERROR('Posting Tagihan Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) "+
                                    "RETURN "+
                                "END "+

                                "SELECT * FROM keu_tagihan WHERE npm = @NoNPM2 AND semester = @Semester2 AND jenis_tagihan = 'ANGSURAN SEMESTER' "+
                                "IF(@@ROWCOUNT >= 1) "+

                                "BEGIN "+
                                    "RAISERROR('Tagihan Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) "+
                                    "RETURN "+
                                "END "+ 
                                "ELSE "+
                                "BEGIN "+
                                    "INSERT INTO keu_tagihan(npm, biaya, jenis_tagihan, semester, tgl) "+
                                    "VALUES(@NoNPM2, @BIAYA2, 'ANGSURAN SEMESTER', @Semester2, GETDATE()) "+
                                "END "+

                                "DECLARE @nomorKrs INT "+
                                "DECLARE @NewNomorKrs VARCHAR(50) "+
                                "DECLARE @PreFixKrs VARCHAR(10) = '10' "+

                                "SELECT @nomorKrs = ISNULL(MAX(keu_posting_bank.nomor), 0) + 1 from keu_posting_bank "+
                                "SELECT @NewNomorKrs = @PreFixKrs + RIGHT('00000000' + CAST(@nomorKrs AS VARCHAR(8)), 8) "+

                                "INSERT INTO bpd.dbo.keu_posting_bank "+
                                                         "(billingNo, payeeId, name, billRef1, billRef2, billRef3, billRef4, billRef5, amount_total, cicilan, post_date, status, keterangan) "+
                                "VALUES(@NewNomorKrs, @NoNPM2, @NAMA_KRS, @PRODI_KRS, @KELAS_KRS, @Th_Angkatan_KRS, @semester2, NULL, @BIAYA2, NULL, GETDATE(), 'unpaid', 'ANGSURAN SEMESTER') "+

                                "INSERT INTO keu_posting_bank "+
                                                         "(billingNo, payeeId, name, billRef1, billRef2, billRef3, billRef4, billRef5, amount_total, cicilan, post_date, status, keterangan) "+
                                "VALUES(@NewNomorKrs, @NoNPM2, @NAMA_KRS, @PRODI_KRS, @KELAS_KRS, @Th_Angkatan_KRS, @semester2, NULL, @BIAYA2, NULL, GETDATE(), 'unpaid', 'ANGSRAN SEMESTER') "+
                            "END "+
                            "ELSE "+
                            "BEGIN "+
                                "RAISERROR('Buka Mata Kuliah Akhir, Proses Dibatalkan...', 16, 10) "+
                                "RETURN "+
                            "END "+
                            "", ConPasca, TransPasca);
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@NoNPM2", this.Session["Name"].ToString());
                        CmdPost.Parameters.AddWithValue("@Semester2", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        CmdPost.ExecuteNonQuery();

                        //string FormattedString9 = string.Format
                        //    (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                        //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                        //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                        // 6.---------- Simpan Perubahan --------------
                        TransUntidar.Commit();
                        TransPasca.Commit();
                        TransUntidar.Dispose();
                        TransPasca.Dispose();
                        ConPasca.Close();
                        ConUntidar.Close();
                        ConUntidar.Dispose();
                        ConPasca.Dispose();

                        // clear ceklist jadwal
                        for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            ch.Checked = false;
                        }

                        // Hide Grid View
                        this.GVAmbilKRS.Visible = false;
                        this.BtnSimpan.Visible = false;

                        LbJumlahSKS.Text = _TotalSKS.ToString();

                        // set jumlah sks = 0
                        _TotalSKS = 0;

                        this.DLSemester.SelectedValue = "Semester";

                        string msg = "alert('Input KRS Berhasil ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);

                    }
                }
                catch (Exception ex)
                {
                    this.DLSemester.SelectedValue = "Semester";

                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelKRS.Visible = false;

                    // close connection
                    TransUntidar.Rollback();
                    TransPasca.Rollback();
                    TransUntidar.Dispose();
                    TransPasca.Dispose();
                    ConUntidar.Close();
                    ConPasca.Close();
                    ConUntidar.Dispose();
                    ConPasca.Dispose();

                    // clear ceklist jadwal
                    for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                        ch.Checked = false;
                    }

                    // set jumlah sks = 0
                    this.LbJumlahSKS.Text = "0";
                    _TotalSKS = 0;

                    string msg = "alert('" + ex.Message + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                }
            }
            else
            {
                // ===================== MABA ====================== //
                // ================================================= //

                // A.------- Maba Semester Gasal -------- //
                // insert KRS mahasiswa
                if (this.DLSemester.SelectedItem.Text.Trim() == "1")
                {
                    string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS2))
                    {
                        con.Open();
                        SqlTransaction trans = con.BeginTransaction();
                        try
                        {
                            // 1. ------ Cek Masa KRS (difilter pd saat input)-------

                            // ---- loop insert no jadwal ---
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                    cmdInKRS.Transaction = trans;

                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                                    cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                    cmdInKRS.ExecuteNonQuery();
                                }
                            }

                            trans.Commit();
                            trans.Dispose();

                            con.Close();
                            con.Dispose();

                            // clear ceklist jadwal
                            for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                ch.Checked = false;
                            }

                            // Hide Grid View
                            this.GVAmbilKRS.Visible = false;
                            this.BtnSimpan.Visible = false;

                            this.DLSemester.SelectedIndex = 0;

                            string FormattedString9 = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", 0);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;


                            string msg = "alert('Input KRS Berhasil ...')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                        }
                        catch (Exception ex)
                        {
                            this.LbJumlahSKS.Text = " ";
                            this.DLSemester.SelectedIndex = 0;

                            trans.Rollback();
                            con.Close();
                            con.Dispose();

                            string msg = "alert('" + ex.Message + "')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                            return;
                        }
                    }
                }
                else
                //B.-------- Maba Semester Genap {{PROSEDURNYA SAMA PERSIS DENGAN MAHASISWA LAMA / BUKAN MABA }}-------- //
                {
                    string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    string CSPasca = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;

                    SqlConnection ConUntidar = new SqlConnection(CSUntidar);
                    SqlConnection ConPasca = new SqlConnection(CSPasca);

                    ConUntidar.Open();
                    ConPasca.Open();

                    var TransUntidar = ConUntidar.BeginTransaction();
                    var TransPasca = ConPasca.BeginTransaction();

                    try
                    {
                        // 1. ------- Cek Masa KRS (difilter pd saat input KRS) -------------

                        // 2. ---------- Update Data KHS ----------
                        string thn = "";
                        string smstr = "";
                        if (this.DLSemester.SelectedValue == "1")
                        {
                            int tahun = Convert.ToInt32(this.DLTahun.SelectedValue);
                            tahun = tahun - 1;
                            thn = Convert.ToString(tahun);
                            smstr = "2";
                        }
                        else if (this.DLSemester.SelectedValue == "2")
                        {
                            thn = DLTahun.SelectedValue;
                            smstr = "1";
                        }

                        SqlCommand CmdUpKHS = new SqlCommand("SpTranKuliahMhs2", ConUntidar, TransUntidar);
                        CmdUpKHS.Transaction = TransUntidar;
                        CmdUpKHS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUpKHS.Parameters.AddWithValue("@semester", thn + smstr);
                        CmdUpKHS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdUpKHS.ExecuteNonQuery();

                        // 3. ---------- Cek Ambil KRS TESIS ----------
                        int TotalChecked = 0;
                        for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            if (ch.Checked == true)
                            {
                                TotalChecked += 1;
                            }
                        }
                        if (TotalChecked == 1)
                        {
                            try
                            {
                                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                                {
                                    CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                    if (ch.Checked == true)
                                    {
                                        // ---------- INSERT TAGIHAN TESIS (+Post Tagihan) ----------
                                        SqlCommand CmdSkripsi = new SqlCommand("" +
                                        "DECLARE @NO INT " +
                                        "DECLARE @Makul NVARCHAR(100) " +
                                        "DECLARE @MakulAkhir VARCHAR(20) " +
                                        "DECLARE @SKS INT " +

                                        "SELECT @NO = no, @Makul = makul, @SKS = sks, @MakulAkhir = makul_akhir FROM UntidarDb.dbo.bak_makul WHERE kode_makul = @KodeMakul " +
                                        "IF(@MakulAkhir = 'yes') " +
                                        "BEGIN " +
                                            "DECLARE @NoNIMTesis VARCHAR(12) " +
                                            "DECLARE @Th_Angkatan varchar(9) " +
                                            "DECLARE @KELAS VARCHAR(15) " +
                                            "DECLARE @IDPRODI VARCHAR(6) " +
                                            "DECLARE @PRODI VARCHAR(60) " +
                                            "DECLARE @NAMA VARCHAR(70) " +

                                            "SELECT      @NoNIMTesis = bak_mahasiswa.npm, @NAMA = bak_mahasiswa.nama, @Th_Angkatan = bak_mahasiswa.thn_angkatan, @KELAS = bak_mahasiswa.kelas, @IDPRODI = bak_mahasiswa.id_prog_study, @PRODI = bak_prog_study.prog_study " +
                                            "FROM        UntidarDb.dbo.bak_mahasiswa INNER JOIN " +
                                                        "UntidarDb.dbo.bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                                            "WHERE       npm = @NoNPM " +

                                            "DECLARE @ID INT " +
                                            "DECLARE @SPP DECIMAL " +
                                            "DECLARE @SARDIK DECIMAL " +
                                            "DECLARE @BIAYA DECIMAL " +

                                            "SELECT @ID = id_biaya, @SPP = SPP, @SARDIK = sardik FROM dbo.keu_biaya WHERE thn_angkatan = @Th_Angkatan AND id_prog_study = @IDPRODI " +
                                            "IF(@@ROWCOUNT = 0) " +
                                            "BEGIN " +
                                                "RAISERROR('Tagihan Periodik Ini Belum Dibuat ...', 16, 10) " +
                                                "RETURN " +
                                            "END " +

                                            "SET @BIAYA = @SPP " +

                                            "SELECT * FROM dbo.keu_posting_bank WHERE payeeId = @NoNPM AND billRef5 = 'TESIS' " +
                                            "IF(@@ROWCOUNT >= 1) " +
                                            "BEGIN " +
                                                "RAISERROR('Posting Tagihan Skripsi Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) " +
                                                "RETURN " +
                                            "END " +

                                            "SELECT * FROM keu_tagihan WHERE npm = @NoNPM AND jenis_tagihan = 'TESIS' " +

                                            "IF(@@ROWCOUNT >= 1) " +
                                            "BEGIN " +
                                                "RAISERROR('Tagihan Skripsi Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) " +
                                                "RETURN " +
                                            "END " +
                                            "ELSE " +
                                            "BEGIN " +

                                                "INSERT INTO keu_tagihan(npm, biaya, jenis_tagihan, semester, tgl) " +
                                                "VALUES(@NoNPM, @BIAYA, 'TESIS', @Semester, GETDATE()) " +
                                            "END " +

                                            "DECLARE @nomorTesis INT " +
                                            "DECLARE @NewNomorTesis VARCHAR(50) " +
                                            "DECLARE @PreFixTesis VARCHAR(10) = '10' " +

                                            "SELECT @nomorTesis = ISNULL(MAX(keu_posting_bank.nomor), 0) + 1 from keu_posting_bank " +
                                            "SELECT @NewNomorTesis = @PreFixTesis + RIGHT('00000000' + CAST(@nomorTesis AS VARCHAR(8)), 8) " +

                                            "INSERT INTO bpd.dbo.keu_posting_bank " +
                                                    "(billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,cicilan,post_date, status, keterangan) " +
                                            "VALUES(@NewNomorTesis,  @NoNPM, @NAMA, @PRODI, @KELAS, @Th_Angkatan, @semester, 'TESIS', @BIAYA, NULL, GETDATE(), 'unpaid', 'BIAYA TESIS') " +

                                            "INSERT INTO dbo.keu_posting_bank " +
                                                    "(billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,cicilan,post_date, status, keterangan) " +
                                            "VALUES(@NewNomorTesis,  @NoNPM, @NAMA, @PRODI, @KELAS, @Th_Angkatan, @semester, 'TESIS', @BIAYA, NULL, GETDATE(), 'unpaid', 'BIAYA TESIS') " +
                                        "END " +
                                        "ELSE " +
                                        "BEGIN " +
                                            "RAISERROR('Buka Mata Kuliah Akhir, Proses Dibatalkan...', 16, 10) " +
                                            "RETURN " +
                                        "END " +
                                        "", ConPasca, TransPasca);
                                        CmdSkripsi.CommandType = System.Data.CommandType.Text;

                                        CmdSkripsi.Parameters.AddWithValue("@KodeMakul", GVAmbilKRS.Rows[i].Cells[3].Text);
                                        CmdSkripsi.Parameters.AddWithValue("@Semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                        CmdSkripsi.Parameters.AddWithValue("@NoNPM", this.Session["Name"].ToString());

                                        //SqlParameter Biaya = new SqlParameter();
                                        //Biaya.ParameterName = "@TotalBiaya";
                                        //Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                                        //Biaya.Size = 20;
                                        //Biaya.Direction = System.Data.ParameterDirection.Output;
                                        //CmdSkripsi.Parameters.Add(Biaya);

                                        CmdSkripsi.ExecuteNonQuery();

                                        //decimal TotalBiaya;
                                        //TotalBiaya = Convert.ToDecimal(Biaya.Value.ToString());

                                        //string FormattedString9 = string.Format
                                        //    (new System.Globalization.CultureInfo("id"), "{0:c}", TotalBiaya);
                                        //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        // ---- ---------- INSERT KRS TESIS ---------- ----
                                        // ---- Ambil KRS BIASA (1 Mata Kuliah) Tidak Disarankan
                                        //----- -------------------------------------- -----
                                        SqlCommand cmdInKRS = new SqlCommand("SpInKRS", ConUntidar, TransUntidar);
                                        cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                        cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);
                                        cmdInKRS.ExecuteNonQuery();

                                        // ----- Simpan Perubahan -----
                                        TransUntidar.Commit();
                                        TransPasca.Commit();
                                        TransUntidar.Dispose();
                                        TransPasca.Dispose();
                                        ConPasca.Close();
                                        ConUntidar.Close();
                                        ConUntidar.Dispose();
                                        ConPasca.Dispose();

                                        string message = "alert('KRS TESIS Berhasil ...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Skripsi Berhasil ...');", true);
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                TransUntidar.Rollback();
                                TransPasca.Rollback();
                                TransUntidar.Dispose();
                                TransPasca.Dispose();
                                ConPasca.Close();
                                ConUntidar.Close();
                                ConUntidar.Dispose();
                                ConPasca.Dispose();

                                //Response.Write(ex.ToString());

                                string message = "alert('" + ex.Message + "')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            }
                            return;
                        }
                        else
                        {
                            // ------------- KRS Biasa -------------------
                            // -------------------------------------------
                            // 4.-- Loop insert no jadwal (INPUT KRS) --
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", ConUntidar, TransUntidar);

                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                    cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                    cmdInKRS.ExecuteNonQuery();
                                }
                            }


                            //5.-- POSTING tahihan to BANK by using SpInsertPostingPerMhs --
                            SqlCommand CmdPost = new SqlCommand("" +
                                "DECLARE @NO2 INT " +
                                "DECLARE @Makul2 NVARCHAR(100) " +
                                "DECLARE @MakulAkhir2 VARCHAR(20) " +
                                "DECLARE @SKS2 INT " +

                                "SELECT @NO2 = no, @Makul2 = makul, @SKS2 = sks, @MakulAkhir2 = makul_akhir FROM UntidarDb.dbo.bak_makul WHERE kode_makul = @KodeMakul2 " +

                                "IF(@MakulAkhir2 = 'yes') " +
                                "BEGIN " +
                                    "DECLARE @NoNIM_KRS VARCHAR(12) " +
                                    "DECLARE @Th_Angkatan_KRS varchar(9) " +
                                    "DECLARE @KELAS_KRS VARCHAR(15) " +
                                    "DECLARE @IDPRODI_KRS VARCHAR(6) " +
                                    "DECLARE @PRODI_KRS VARCHAR(60) " +
                                    "DECLARE @NAMA_KRS VARCHAR(70) " +

                                    "SELECT      @NoNIM_KRS = bak_mahasiswa.npm, @NAMA_KRS = bak_mahasiswa.nama, @Th_Angkatan_KRS = bak_mahasiswa.thn_angkatan, @KELAS_KRS = bak_mahasiswa.kelas, @IDPRODI_KRS = bak_mahasiswa.id_prog_study, @PRODI_KRS = bak_prog_study.prog_study " +
                                    "FROM        UntidarDb.dbo.bak_mahasiswa INNER JOIN " +
                                                "UntidarDb.dbo.bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                                    "WHERE       npm = @NoNPM2 " +

                                    "DECLARE @ID2 INT " +
                                    "DECLARE @SPP2 DECIMAL " +
                                    "DECLARE @SARDIK2 DECIMAL " +
                                    "DECLARE @BIAYA2 DECIMAL " +

                                    "SELECT @ID2 = id_biaya, @SPP2 = SPP, @SARDIK2 = sardik FROM dbo.keu_biaya WHERE thn_angkatan = @Th_Angkatan_KRS AND id_prog_study = @IDPRODI_KRS " +
                                    "IF(@@ROWCOUNT = 0) " +
                                    "BEGIN " +
                                        "RAISERROR('Tagihan Periodik Ini Belum Dibuat ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +

                                    "SET @BIAYA2 = @SPP2 + @SARDIK2 " +

                                    "SELECT * FROM dbo.keu_posting_bank WHERE payeeId = @NoNPM2 AND billRef4 = @Semester2 AND keterangan = 'ANGSURAN SEMESTER' " +
                                    "IF(@@ROWCOUNT >= 1) " +
                                    "BEGIN " +
                                        "RAISERROR('Posting Tagihan Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) " +
                                        "RETURN " +
                                    "END " +

                                    "SELECT * FROM keu_tagihan WHERE npm = @NoNPM2 AND semester = @Semester2 AND jenis_tagihan = 'ANGSURAN SEMESTER' " +
                                    "IF(@@ROWCOUNT >= 1) " +

                                    "BEGIN " +
                                        "RAISERROR('Tagihan Semester Ini Sudah Dibuat, Proses Dibatalkan...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                    "ELSE " +
                                    "BEGIN " +
                                        "INSERT INTO keu_tagihan(npm, biaya, jenis_tagihan, semester, tgl) " +
                                        "VALUES(@NoNPM2, @BIAYA2, 'ANGSURAN SEMESTER', @Semester2, GETDATE()) " +
                                    "END " +

                                    "DECLARE @nomorKrs INT " +
                                    "DECLARE @NewNomorKrs VARCHAR(50) " +
                                    "DECLARE @PreFixKrs VARCHAR(10) = '10' " +

                                    "SELECT @nomorKrs = ISNULL(MAX(keu_posting_bank.nomor), 0) + 1 from keu_posting_bank " +
                                    "SELECT @NewNomorKrs = @PreFixKrs + RIGHT('00000000' + CAST(@nomorKrs AS VARCHAR(8)), 8) " +

                                    "INSERT INTO bpd.dbo.keu_posting_bank " +
                                                             "(billingNo, payeeId, name, billRef1, billRef2, billRef3, billRef4, billRef5, amount_total, cicilan, post_date, status, keterangan) " +
                                    "VALUES(@NewNomorKrs, @NoNPM2, @NAMA_KRS, @PRODI_KRS, @KELAS_KRS, @Th_Angkatan_KRS, @semester2, NULL, @BIAYA2, NULL, GETDATE(), 'unpaid', 'ANGSURAN SEMESTER') " +

                                    "INSERT INTO keu_posting_bank " +
                                                             "(billingNo, payeeId, name, billRef1, billRef2, billRef3, billRef4, billRef5, amount_total, cicilan, post_date, status, keterangan) " +
                                    "VALUES(@NewNomorKrs, @NoNPM2, @NAMA_KRS, @PRODI_KRS, @KELAS_KRS, @Th_Angkatan_KRS, @semester2, NULL, @BIAYA2, NULL, GETDATE(), 'unpaid', 'ANGSRAN SEMESTER') " +
                                "END " +
                                "ELSE " +
                                "BEGIN " +
                                    "RAISERROR('Buka Mata Kuliah Akhir, Proses Dibatalkan...', 16, 10) " +
                                    "RETURN " +
                                "END " +
                                "", ConPasca, TransPasca);
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@NoNPM2", this.Session["Name"].ToString());
                            CmdPost.Parameters.AddWithValue("@Semester2", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPost.ExecuteNonQuery();

                            //string FormattedString9 = string.Format
                            //    (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                            //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                            // 6.---------- Simpan Perubahan --------------
                            TransUntidar.Commit();
                            TransPasca.Commit();
                            TransUntidar.Dispose();
                            TransPasca.Dispose();
                            ConPasca.Close();
                            ConUntidar.Close();
                            ConUntidar.Dispose();
                            ConPasca.Dispose();

                            // clear ceklist jadwal
                            for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                ch.Checked = false;
                            }

                            // Hide Grid View
                            this.GVAmbilKRS.Visible = false;
                            this.BtnSimpan.Visible = false;

                            LbJumlahSKS.Text = _TotalSKS.ToString();

                            // set jumlah sks = 0
                            _TotalSKS = 0;

                            this.DLSemester.SelectedValue = "Semester";

                            string msg = "alert('Input KRS Berhasil ...')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);

                        }
                    }
                    catch (Exception ex)
                    {
                        this.DLSemester.SelectedValue = "Semester";

                        this.PanelKRS.Enabled = false;
                        this.PanelKRS.Visible = false;

                        this.PanelKRS.Visible = false;

                        // close connection
                        TransUntidar.Rollback();
                        TransPasca.Rollback();
                        TransUntidar.Dispose();
                        TransPasca.Dispose();
                        ConUntidar.Close();
                        ConPasca.Close();
                        ConUntidar.Dispose();
                        ConPasca.Dispose();

                        // clear ceklist jadwal
                        for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            ch.Checked = false;
                        }

                        // set jumlah sks = 0
                        this.LbJumlahSKS.Text = "0";
                        _TotalSKS = 0;

                        string msg = "alert('" + ex.Message + "')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                    }
                }

            }
        }

        protected void BtnKRS_Click(object sender, EventArgs e)
        {
            try
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                string CSPasca = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
                SqlConnection ConPasca = new SqlConnection(CSPasca);
                ConPasca.Open();

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    //A. =============== Read Status AKTIF ================== //
                    SqlCommand CmdCekStatus = new SqlCommand("SELECT npm FROM dbo.bak_mahasiswa WHERE npm=@NPM AND status <> 'A'", con);
                    CmdCekStatus.CommandType = System.Data.CommandType.Text;
                    CmdCekStatus.Parameters.AddWithValue("@NPM", this.Session["Name"].ToString());

                    using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Tercatat Sebagai Mahasiswa Aktif');", true);
                            return;
                        }
                    }

                    // B. =============== INPUT KRS ===============//
                    string TahunMhs = "";

                    SqlCommand CmdCekMabaKrs = new SqlCommand("SELECT TOP (1) bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study " +
                            "FROM            bak_mahasiswa INNER JOIN " +
                                                     "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                            "WHERE jenjang = 'S2' AND dbo.bak_mahasiswa.id_prog_study = @IdProdi " +
                            "GROUP BY bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study " +
                            "ORDER BY bak_mahasiswa.thn_angkatan DESC", con);

                    CmdCekMabaKrs.CommandType = System.Data.CommandType.Text;
                    CmdCekMabaKrs.Parameters.AddWithValue("@IdProdi",this.LbKdProdi.Text.Trim());

                    using (SqlDataReader rdr = CmdCekMabaKrs.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                TahunMhs = rdr["thn_angkatan"].ToString();
                            }
                        }
                    }

                    // --------- Mahasiswa Baru ----------
                    if (this.LbTahun.Text.Trim() == TahunMhs)
                    {
                        // 1. ------ Cek Masa KRS -------
                        SqlCommand CmdCekMasa = new SqlCommand(""+
                            "DECLARE @tgl_mulai DATE "+
                            "DECLARE @tgl_sls DATE "+
                            "DECLARE @now DATE "+
                            "SELECT @now = GETDATE(); "+
                            "DECLARE @output VARCHAR(50) "+

                            "SELECT @tgl_mulai = tgl_mulai,@tgl_sls = tgl_sls FROM dbo.bak_kal WHERE jenis = @jenis_keg AND semester = @semester AND jenjang = 'S2' "+
                            "IF(@tgl_mulai IS NOT NULL) AND(@tgl_sls IS NOT NULL) "+
                            "BEGIN "+
                                "IF((@now >= @tgl_mulai) AND(@now <= @tgl_sls)) "+
                                "BEGIN "+
                                    "SELECT 'IN' AS MASA "+
                                "END "+
                                "ELSE "+
                                "IF((@now < @tgl_mulai) OR(@now > @tgl_sls)) "+
                                "BEGIN "+
                                    "SELECT 'OUT' AS MASA "+
                                "END "+
                            "END "+
                            "ELSE "+
                            "BEGIN "+
                                "DECLARE @ERROR varchar(100) "+
                                "SET @ERROR = 'Tidak Ada Jadwal ' + CAST(@jenis_keg AS varchar(20)) "+
                                "RAISERROR(@ERROR, 16, 10) "+
                            "END "+
                            "", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.Text;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSMABA");

                        string MasaKegiatan = "";
                        using (SqlDataReader rdr = CmdCekMasa.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    MasaKegiatan = rdr["MASA"].ToString();
                                }
                            }
                        }

                        if (MasaKegiatan == "OUT")
                        {
                            /// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DITOLAK ============== -- /////
                            this.PanelKRS.Visible = false;

                            this.DLSemester.SelectedValue = "Semester";

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                            string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            return;

                            ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DIPERBOLEHKAN ============ -- /////
                            //try
                            //{
                            //    //2. ------ Cek Masa Batal Tambah -------
                            //    SqlCommand CmdCekMasaBatal = new SqlCommand("SpCekMasaKeg", con);
                            //    //CmdCekMasa.Transaction = trans;
                            //    CmdCekMasaBatal.CommandType = System.Data.CommandType.StoredProcedure;

                            //    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                            //    CmdCekMasaBatal.Parameters.AddWithValue("@jenis_keg", "BatalTambah");

                            //    SqlParameter StatusBtl = new SqlParameter();
                            //    StatusBtl.ParameterName = "@output";
                            //    StatusBtl.SqlDbType = System.Data.SqlDbType.VarChar;
                            //    StatusBtl.Size = 20;
                            //    StatusBtl.Direction = System.Data.ParameterDirection.Output;
                            //    CmdCekMasaBatal.Parameters.Add(StatusBtl);

                            //    CmdCekMasaBatal.ExecuteNonQuery();

                            //    if (StatusBtl.Value.ToString() == "OUT")
                            //    {
                            //        con.Close();
                            //        con.Dispose();

                            //        this.DLSemester.SelectedValue = "Semester";

                            //        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Baru...')";
                            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            //        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                            //        return;
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    this.DLSemester.SelectedValue = "Semester";
                            //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                            //    string message = "alert('" + ex.Message.ToString() + "')";
                            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            //}
                        }

                    }
                    else
                    {
                        // ------------ INPUT KRS MAHASISWA LAMA -------------- //
                        // 1. ------ Cek Masa KRS -------
                        SqlCommand CmdCekMasa = new SqlCommand("" +
                            "DECLARE @tgl_mulai DATE " +
                            "DECLARE @tgl_sls DATE " +
                            "DECLARE @now DATE " +
                            "SELECT @now = GETDATE(); " +
                            "DECLARE @output VARCHAR(50) " +

                            "SELECT @tgl_mulai = tgl_mulai,@tgl_sls = tgl_sls FROM dbo.bak_kal WHERE jenis = @jenis_keg AND semester = @semester AND jenjang = 'S2' " +
                            "IF(@tgl_mulai IS NOT NULL) AND(@tgl_sls IS NOT NULL) " +
                            "BEGIN " +
                                "IF((@now >= @tgl_mulai) AND(@now <= @tgl_sls)) " +
                                "BEGIN " +
                                    "SELECT 'IN' AS MASA " +
                                "END " +
                                "ELSE " +
                                "IF((@now < @tgl_mulai) OR(@now > @tgl_sls)) " +
                                "BEGIN " +
                                    "SELECT 'OUT' AS MASA " +
                                "END " +
                            "END " +
                            "ELSE " +
                            "BEGIN " +
                                "DECLARE @ERROR varchar(100) " +
                                "SET @ERROR = 'Tidak Ada Jadwal ' + CAST(@jenis_keg AS varchar(20)) " +
                                "RAISERROR(@ERROR, 16, 10) " +
                            "END " +
                            "", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.Text;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSNONMABA");

                        string MasaKegiatan = "";
                        using (SqlDataReader rdr = CmdCekMasa.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    MasaKegiatan = rdr["MASA"].ToString();
                                }
                            }
                        }

                        if (MasaKegiatan == "OUT")
                        {
                            /// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DITOLAK ============== -- /////
                            this.PanelKRS.Visible = false;

                            this.DLSemester.SelectedValue = "Semester";

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                            string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            return;


                            ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DIPERBOLEHKAN ============ -- /////
                            //try
                            //{
                            //    //2. ------ Cek Masa Batal Tambah -------
                            //    SqlCommand CmdCekMasaBatal = new SqlCommand("SpCekMasaKeg", con);
                            //    //CmdCekMasa.Transaction = trans;
                            //    CmdCekMasaBatal.CommandType = System.Data.CommandType.StoredProcedure;

                            //    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                            //    CmdCekMasaBatal.Parameters.AddWithValue("@jenis_keg", "BatalTambah");

                            //    SqlParameter StatusBtl = new SqlParameter();
                            //    StatusBtl.ParameterName = "@output";
                            //    StatusBtl.SqlDbType = System.Data.SqlDbType.VarChar;
                            //    StatusBtl.Size = 20;
                            //    StatusBtl.Direction = System.Data.ParameterDirection.Output;
                            //    CmdCekMasaBatal.Parameters.Add(StatusBtl);

                            //    CmdCekMasaBatal.ExecuteNonQuery();

                            //    if (StatusBtl.Value.ToString() == "OUT")
                            //    {
                            //        con.Close();
                            //        con.Dispose();

                            //        this.DLSemester.SelectedValue = "Semester";

                            //        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Lama...')";
                            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            //        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                            //        return;
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    this.DLSemester.SelectedValue = "Semester";
                            //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                            //    string message = "alert('" + ex.Message.ToString() + "')";
                            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            //}
                        }

                    }

                    // 3. ================= Cek IP Semester Sebelumnya & Get Max SKS ================ ////
                    if (this.LbTahun.Text.Trim() == TahunMhs)
                    {
                        // ---------- MAHASISWA BARU ----------------//
                        // Maba awal semester semester max 24 sks
                        if (this.DLSemester.SelectedValue.Trim() == "1")
                        {
                            _MaxKRS = 24;
                        }
                        else if (this.DLSemester.SelectedValue.Trim() == "2")
                        {
                            // ------- Sama dengan mahasiswa lama -------- //

                            _PrevIPS = 0;
                            SqlCommand CmdCekIPS = new SqlCommand("SpGetPrevIPS", con);
                            //CmdCekMasa.Transaction = trans;
                            CmdCekIPS.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekIPS.Parameters.AddWithValue("@ThisSemester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                            CmdCekIPS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            SqlParameter CekIPS = new SqlParameter();
                            CekIPS.ParameterName = "@IPS";
                            CekIPS.SqlDbType = System.Data.SqlDbType.Decimal;
                            CekIPS.Precision = 4; // jumlah angka decimal
                            CekIPS.Scale = 2; // jumlah digit setelah koma
                            CekIPS.Direction = System.Data.ParameterDirection.Output;
                            CmdCekIPS.Parameters.Add(CekIPS);

                            CmdCekIPS.ExecuteNonQuery();

                            _PrevIPS = Convert.ToDecimal(CekIPS.Value);
                            if (_PrevIPS < Convert.ToDecimal(2.00))
                            {
                                _MaxKRS = 16;
                            }
                            else if ((_PrevIPS >= Convert.ToDecimal(2.00)) && (_PrevIPS <= Convert.ToDecimal(2.49)))
                            {
                                _MaxKRS = 20;
                            }
                            else if ((_PrevIPS >= Convert.ToDecimal(2.50)) && (_PrevIPS <= Convert.ToDecimal(2.99)))
                            {
                                _MaxKRS = 22;
                            }
                            else if (_PrevIPS >= Convert.ToDecimal(3.00))
                            {
                                _MaxKRS = 24;
                            }
                        }
                    }
                    else
                    {
                        // ---------- MAHASISWA LAMA ----------------//

                        _PrevIPS = 0;
                        SqlCommand CmdCekIPS = new SqlCommand("SpGetPrevIPS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekIPS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekIPS.Parameters.AddWithValue("@ThisSemester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                        CmdCekIPS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        SqlParameter CekIPS = new SqlParameter();
                        CekIPS.ParameterName = "@IPS";
                        CekIPS.SqlDbType = System.Data.SqlDbType.Decimal;
                        CekIPS.Precision = 4; // jumlah angka decimal
                        CekIPS.Scale = 2; // jumlah digit setelah koma
                        CekIPS.Direction = System.Data.ParameterDirection.Output;
                        CmdCekIPS.Parameters.Add(CekIPS);

                        CmdCekIPS.ExecuteNonQuery();

                        _PrevIPS = Convert.ToDecimal(CekIPS.Value);
                        if (_PrevIPS < Convert.ToDecimal(2.00))
                        {
                            _MaxKRS = 16;
                        }
                        else if ((_PrevIPS >= Convert.ToDecimal(2.00)) && (_PrevIPS <= Convert.ToDecimal(2.49)))
                        {
                            _MaxKRS = 20;
                        }
                        else if ((_PrevIPS >= Convert.ToDecimal(2.50)) && (_PrevIPS <= Convert.ToDecimal(2.99)))
                        {
                            _MaxKRS = 22;
                        }
                        else if (_PrevIPS >= Convert.ToDecimal(3.00))
                        {
                            _MaxKRS = 24;
                        }
                    }

                    this.LbMaxSKS.Text = _MaxKRS.ToString().Trim();

                    // 4. =================== Cek KRS Semester Ini, error jika Sudah ada =======================
                    SqlCommand CekKRS = new SqlCommand("SpCekKrs", con);
                    CekKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CekKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CekKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CekKRS.ExecuteNonQuery();

                    // 5. ================= Fill Gridview  =======================
                    SqlCommand CmdKRS = new SqlCommand("SpListKRS2", con);
                    CmdKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdKRS.Parameters.AddWithValue("@id_prodi", LbKdProdi.Text);
                    CmdKRS.Parameters.AddWithValue("@jenis_kelas", LbKelas.Text);
                    CmdKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                    CmdKRS.Parameters.AddWithValue("@npm", Session["Name"].ToString().Trim());

                    DataTable TableKRS = new DataTable();
                    TableKRS.Columns.Add("Key");
                    TableKRS.Columns.Add("Kode");
                    TableKRS.Columns.Add("Mata Kuliah");
                    TableKRS.Columns.Add("SKS");
                    TableKRS.Columns.Add("Quota");
                    TableKRS.Columns.Add("Dosen");
                    TableKRS.Columns.Add("Kelas");
                    TableKRS.Columns.Add("Hari");
                    TableKRS.Columns.Add("Mulai");
                    TableKRS.Columns.Add("Selesai");
                    TableKRS.Columns.Add("Ruang");

                    using (SqlDataReader rdr = CmdKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelKRS.Enabled = true;
                            this.PanelKRS.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKRS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Quota"] = rdr["quota"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari"];
                                datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                datarow["Ruang"] = rdr["nm_ruang"];

                                TableKRS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVAmbilKRS.DataSource = TableKRS;
                            this.GVAmbilKRS.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableKRS.Rows.Clear();
                            TableKRS.Clear();
                            GVAmbilKRS.DataSource = TableKRS;
                            GVAmbilKRS.DataBind();

                            this.PanelKRS.Enabled = false;
                            this.PanelKRS.Visible = false;

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data tidak ditemukan');", true);
                            string message = "alert('Jadwal Belum Ada, Hubungi Fakultas atau Program Studi ')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.PanelKRS.Enabled = false;
                this.PanelKRS.Visible = false;

                this.DLSemester.SelectedValue = "Semester";

                string message = "alert('" + ex.Message.ToString() + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //return;
            }
        }
    }
}