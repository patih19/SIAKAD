using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.UI.HtmlControls;

namespace Padu.pasca
{
    public partial class EditKrs : MhsPasca
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

        public int _TotalEditSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalEditSKS"].ToString()); }
            set { this.ViewState["TotalEditSKS"] = (object)value; }
        }

        public int _TotalSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavMasterKRS");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavMasterKRS");
                control2.Attributes.Add("style", "display: block;");

                this.PanelMhs.Visible = false;
                this.PanelEditKRS.Visible = false;

                try
                {
                    //----- read data mahasiswa ------
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

                    this.PanelEditKRS.Visible = false;

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                    string message = "alert('Mahasiswa tidak ditemukan')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }

        protected void BtnEditKRS_Click(object sender, EventArgs e)
        {
            try
            {
                //1. --------------- Gridview EDIT KRS Semester ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    //-------------- Read Status AKTIF  --------------------- //
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
                    CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "BatalTambah");

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
                        this.PanelEditKRS.Visible = false;

                        this.DLSemester.SelectedValue = "Semester";

                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                        string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }

                    // pastikan sudah mengisi KRS
                    //Cek KRS Semester Ini, error jika belum ada -----------
                    SqlCommand CekInputKRS = new SqlCommand("SpCekKrs2", con);
                    //CekKRS2.Transaction = trans;
                    CekInputKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CekInputKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CekInputKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CekInputKRS.ExecuteNonQuery();

                    //// ------------ Cek IP Semester Sebelumnya & Set Max SKS ------------ ////
                    // --- CEK MABA ---
                    string TahunMhs = "";

                    SqlCommand CmdCekMabaKrs = new SqlCommand("SELECT TOP (1) bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study " +
                            "FROM            bak_mahasiswa INNER JOIN " +
                                                     "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                            "WHERE jenjang = 'S2' AND dbo.bak_mahasiswa.id_prog_study = @IdProdi " +
                            "GROUP BY bak_mahasiswa.thn_angkatan, bak_prog_study.jenjang, bak_mahasiswa.id_prog_study " +
                            "ORDER BY bak_mahasiswa.thn_angkatan DESC", con);

                    CmdCekMabaKrs.CommandType = System.Data.CommandType.Text;
                    CmdCekMabaKrs.Parameters.AddWithValue("@IdProdi", this.LbKdProdi.Text.Trim());

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


                    if (this.LbTahun.Text.Trim() == TahunMhs)
                    {
                        // Maba awal semester semester max 24 sks
                        if (this.DLSemester.SelectedValue.Trim() == "1")
                        {
                            _MaxKRS = 24;
                        }
                        else if (this.DLSemester.SelectedValue.Trim() == "2")
                        {
                            // ------- Mulai Semester Dua Sama Dengan Mahasiswa Lama -------- //
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

                    //this.LbMaxSKS.Text = _MaxKRS.ToString().Trim();

                    // 2. ----- Get status Batal Tambah KRS ( Max 2x ) ----------------
                    SqlCommand CmdCntKRS = new SqlCommand("SpCntBatalKRS", con);
                    //CmdCekMasa.Transaction = trans;
                    CmdCntKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCntKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdCntKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                    CmdCntKRS.ExecuteNonQuery();


                    // 3. ----- Cek KRS Semester Ini, error jika belum ada -----------
                    SqlCommand CekKRS2 = new SqlCommand("SpCekKrs2", con);
                    CekKRS2.CommandType = System.Data.CommandType.StoredProcedure;

                    CekKRS2.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CekKRS2.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CekKRS2.ExecuteNonQuery();

                    //------------- Fill Gridview Edit KRS ------------------------
                    SqlCommand CmdEdit = new SqlCommand("SpListKRS2", con);
                    CmdEdit.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdEdit.Parameters.AddWithValue("@id_prodi", LbKdProdi.Text);
                    CmdEdit.Parameters.AddWithValue("@jenis_kelas", this.LbKelas.Text);
                    CmdEdit.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                    CmdEdit.Parameters.AddWithValue("@npm", this.Session["Name"].ToString().Trim());

                    DataTable TableEdit = new DataTable();
                    TableEdit.Columns.Add("Key");
                    TableEdit.Columns.Add("Kode");
                    TableEdit.Columns.Add("Mata Kuliah");
                    TableEdit.Columns.Add("SKS");
                    TableEdit.Columns.Add("Quota");
                    TableEdit.Columns.Add("Dosen");
                    TableEdit.Columns.Add("Kelas");
                    TableEdit.Columns.Add("Hari");
                    TableEdit.Columns.Add("Mulai");
                    TableEdit.Columns.Add("Selesai");
                    TableEdit.Columns.Add("Ruang");

                    using (SqlDataReader rdr = CmdEdit.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelEditKRS.Enabled = true;
                            this.PanelEditKRS.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableEdit.NewRow();
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

                                TableEdit.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVEditKRS.DataSource = TableEdit;
                            this.GVEditKRS.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableEdit.Rows.Clear();
                            TableEdit.Clear();
                            GVEditKRS.DataSource = TableEdit;
                            GVEditKRS.DataBind();

                            this.PanelEditKRS.Enabled = false;
                            this.PanelEditKRS.Visible = false;

                            this.DLSemester.SelectedValue = "Semester";

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Mahasiswa Tidak Ditemukan');", true);

                            string message = "alert('KRS Mahasiswa Tidak Ditemukan')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            return;
                        }
                    }

                    //-- 3. loop id jadwal yg sudah diambil berdasarkan no jadwal
                    //-- jika ada checked = true
                    int TotalSKS = 0;
                    for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                    {
                        SqlCommand CmdChecked = new SqlCommand("SpGetIdJadwalMhs", con);
                        CmdChecked.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdChecked.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);
                        CmdChecked.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdChecked.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        using (SqlDataReader rdrchecked = CmdChecked.ExecuteReader())
                        {
                            // ------ Cek Quota Makul (CHECKED) -------
                            if (rdrchecked.HasRows)
                            {
                                while (rdrchecked.Read())
                                {
                                    CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                                    ch.Checked = true;
                                    TotalSKS += Convert.ToInt32(this.GVEditKRS.Rows[i].Cells[5].Text);

                                    this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                                    try
                                    {
                                        string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                        using (SqlConnection con2 = new SqlConnection(CS))
                                        {
                                            con2.Open();

                                            SqlCommand CmdCekQuota = new SqlCommand("SpQuotaJadwal", con2);
                                            //CmdCekMasa.Transaction = trans;
                                            CmdCekQuota.CommandType = System.Data.CommandType.StoredProcedure;

                                            CmdCekQuota.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                            SqlParameter Quota = new SqlParameter();
                                            Quota.ParameterName = "@quota";
                                            Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                                            Quota.Size = 20;
                                            Quota.Direction = System.Data.ParameterDirection.Output;
                                            CmdCekQuota.Parameters.Add(Quota);

                                            SqlParameter Sisa = new SqlParameter();
                                            Sisa.ParameterName = "@sisa";
                                            Sisa.SqlDbType = System.Data.SqlDbType.Int;
                                            Sisa.Size = 20;
                                            Sisa.Direction = System.Data.ParameterDirection.Output;
                                            CmdCekQuota.Parameters.Add(Sisa);

                                            CmdCekQuota.ExecuteNonQuery();


                                            if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                                            {
                                                Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                LbSisa.Text = Sisa.Value.ToString();

                                                LbSisa.ForeColor = System.Drawing.Color.Green;
                                            }
                                            else
                                            {
                                                Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                LbSisa.Text = "Penuh";

                                                LbSisa.ForeColor = System.Drawing.Color.Red;

                                                CheckBox ch2 = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");

                                                //ch2.Visible = false;
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah Checked");
                                    }
                                }
                            }
                            else
                            {
                                // ------ Cek Quota Makul (UNCHECKED)-------
                                try
                                {
                                    string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                    using (SqlConnection con2 = new SqlConnection(CS))
                                    {
                                        con2.Open();

                                        SqlCommand CmdCekQuo = new SqlCommand("SpQuotaJadwal", con2);
                                        //CmdCekMasa.Transaction = trans;
                                        CmdCekQuo.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdCekQuo.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                        SqlParameter Quota = new SqlParameter();
                                        Quota.ParameterName = "@quota";
                                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                                        Quota.Size = 20;
                                        Quota.Direction = System.Data.ParameterDirection.Output;
                                        CmdCekQuo.Parameters.Add(Quota);

                                        SqlParameter Sisa = new SqlParameter();
                                        Sisa.ParameterName = "@sisa";
                                        Sisa.SqlDbType = System.Data.SqlDbType.Int;
                                        Sisa.Size = 20;
                                        Sisa.Direction = System.Data.ParameterDirection.Output;
                                        CmdCekQuo.Parameters.Add(Sisa);

                                        CmdCekQuo.ExecuteNonQuery();


                                        if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                                        {
                                            Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                            LbSisa.Text = Sisa.Value.ToString();

                                            LbSisa.ForeColor = System.Drawing.Color.Green;
                                        }
                                        else
                                        {
                                            Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                            LbSisa.Text = "Penuh";

                                            LbSisa.ForeColor = System.Drawing.Color.Red;

                                            CheckBox ch2 = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");

                                            ch2.Visible = false;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah Unchecked");
                                }
                            }
                        }
                    }
                    this.LbJumlahEditSKS.Text = TotalSKS.ToString();
                }
            }
            catch (Exception ex)
            {
                this.PanelEditKRS.Enabled = false;
                this.PanelEditKRS.Visible = false;

                this.DLSemester.SelectedValue = "Semester";

                string message = "alert('" + ex.Message.ToString() + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //return;
            }
        }

        protected void GVEditKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
        }

        int PrevTotalEditSKS;
        int JumlahEditSKS;
        protected void CBEdit_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            CheckBox ch = (CheckBox)GVEditKRS.Rows[index].FindControl("CBEdit");
            if (ch.Checked == true)
            {
                PrevTotalEditSKS = Convert.ToInt32(this.LbJumlahEditSKS.Text);
                _TotalEditSKS = PrevTotalEditSKS;
                this.GVEditKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                this.LbJumlahEditSKS.Text = _TotalEditSKS.ToString();


                //----- background makul checked ----- //
                _TotalEditSKS = 0;
                for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                    if (ch2.Checked == true)
                    {
                        this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                        _TotalEditSKS += Convert.ToInt16(this.GVEditKRS.Rows[i].Cells[5].Text);
                        this.LbJumlahEditSKS.Text = Convert.ToString(_TotalEditSKS);
                    }
                }

                // batas maximal KRS
                if (_TotalEditSKS > _MaxKRS)
                {
                    ch.Checked = false;
                    this.LbJumlahEditSKS.Text = PrevTotalEditSKS.ToString();
                    this.GVEditKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFCEC1");
                    _TotalEditSKS = PrevTotalEditSKS;
                }
            }
            else
            {
                ////----- hitung ulang sks berdasarkan makul checked ----- //
                //for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                //{
                //    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                //    if (ch2.Checked == true)
                //    {
                //        JumlahEditSKS += Convert.ToInt32(this.GVEditKRS.Rows[i].Cells[5].Text);
                //        _TotalEditSKS = JumlahEditSKS;
                //        this.LbJumlahEditSKS.Text = JumlahEditSKS.ToString();
                //    }
                //}

                ////----- background makul checked ----- //
                //for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                //{
                //    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                //    if (ch2.Checked == true)
                //    {
                //        this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                //    }
                //}


                //----- Cek Apakah semua makul UnChecked ----- //
                bool ischecked = false;
                for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                {
                    CheckBox ch0 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                    if (ch0.Checked == true)
                    {
                        // Tidak ada jadwal yang dipilih 
                        ischecked = true;
                    }
                }

                if (ischecked == true)
                {
                    //----- hitung ulang sks ( makul checked ) ----- //
                    for (int j = 0; j < GVEditKRS.Rows.Count; j++)
                    {
                        CheckBox ch3 = (CheckBox)GVEditKRS.Rows[j].FindControl("CBEdit");
                        if (ch3.Checked == true)
                        {
                            JumlahEditSKS += Convert.ToInt32(this.GVEditKRS.Rows[j].Cells[5].Text);
                            _TotalSKS = JumlahEditSKS;
                            this.LbJumlahEditSKS.Text = JumlahEditSKS.ToString();
                        }
                    }

                    //----- background makul checked ----- //
                    for (int j = 0; j < GVEditKRS.Rows.Count; j++)
                    {
                        CheckBox ch4 = (CheckBox)GVEditKRS.Rows[j].FindControl("CBEdit");
                        if (ch4.Checked == true)
                        {
                            this.GVEditKRS.Rows[j].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                        }
                    }
                }
                else
                {
                    // tidak ada jadwal yg dipilih
                    _TotalEditSKS = 0;
                    this.LbJumlahEditSKS.Text = "0";
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // cek checkbox pilih matakuliah
            Boolean val = false;

            for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                if (ch.Checked == true)
                {
                    val = true;
                    //exit loop completely
                    break;
                }
            }

            // all checkbox did not check
            if (val == false)
            {
                string message = "alert('Pilih Mata Kuliah')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }

            // ---- ================== Cek Makul Dobel ================== --- //
            string[] arr = new string[1000];
            for (int i = 0; i < GVEditKRS.Rows.Count; i++)
            {
                //makul checked
                CheckBox ch = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                if (ch.Checked == true)
                {
                    if (!arr.Contains(GVEditKRS.Rows[i].Cells[3].Text))
                    {
                        arr[i] = GVEditKRS.Rows[i].Cells[3].Text;
                    }
                    else
                    {
                        // clear chaecked jadwal
                        for (int j = 0; j < this.GVEditKRS.Rows.Count; j++)
                        {
                            CheckBox ch2 = (CheckBox)this.GVEditKRS.Rows[j].FindControl("CBEdit");
                            ch2.Checked = false;
                        }

                        // hide panel
                        this.PanelEditKRS.Enabled = false;
                        this.PanelEditKRS.Visible = false;

                        //set jumlah sks = 0
                        this.LbJumlahEditSKS.Text = "0";
                        _TotalEditSKS = 0;

                        //semester
                        this.DLSemester.SelectedValue = "Semester";

                        //duplicate
                        string message = "alert('Dobel Mata Kuliah, Proses Dibatalkan ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }
                }
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // ----=============  Cek BATAL TAMBAH ==============----
                    SqlCommand CmdBatal = new SqlCommand("SpInBatalKRS", con);
                    CmdBatal.Transaction = trans;
                    CmdBatal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBatal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CmdBatal.ExecuteNonQuery();

                    // ---- loop insert no jadwal ---
                    for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                        Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");

                        if (ch.Checked == true)
                        {
                            //Response.Write(GV.Rows[i].Cells[1].Text);
                            //Response.Write("DataKeyName:" + GV.DataKeys[i].Value.ToString());

                            if (LbSisa.Text == "Penuh")
                            {
                                //ignore
                                continue;
                            }
                            else
                            {
                                SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                cmdInKRS.Transaction = trans;

                                cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                cmdInKRS.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                cmdInKRS.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            SqlCommand cmdDelKRS = new SqlCommand("SpDelKRS", con);
                            cmdDelKRS.Transaction = trans;

                            cmdDelKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdDelKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                            cmdDelKRS.Parameters.AddWithValue("@no_jadwal", GVEditKRS.Rows[i].Cells[2].Text);

                            cmdDelKRS.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    trans.Dispose();

                    con.Close();
                    con.Dispose();

                    // hide panel
                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    //semester
                    this.DLSemester.SelectedValue = "Semester";

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update KRS Berhasil ...');", true);
                    string message = "alert('Update KRS Berhasil ...')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    con.Close();
                    con.Dispose();
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                    string message = "alert('" + ex.Message + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }
    }
}