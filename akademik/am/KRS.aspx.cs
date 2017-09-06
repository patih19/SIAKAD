using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace akademik.am
{
    public partial class WebForm22 : Bak_staff
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        public int _TotalSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString());}
            set {this.ViewState["TotalSKS"] = (object)value;}
        }

        public int _TotalEditSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalEditSKS"].ToString());}
            set { this.ViewState["TotalEditSKS"] = (object)value;}
        }

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

        public string _NPM
        {
            get { return (this.ViewState["npm"].ToString()); }
            set { this.ViewState["npm"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;
                _PrevIPS = 0;
                _MaxKRS = 0;
                _NPM = "";

                this.PanelEditKRS.Visible = false;
                this.PanelKRS.Visible = false;
                this.PanelListKRS.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
                return;
            }
            //if (this.RBInputKRS.Checked == false && this.RBEditKRS.Checked == false && this.RBList.Checked == false)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Menu KRS');", true);
            //    return;
            //}
            if (this.RBEditKRS.Checked == false && this.RBList.Checked == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Menu KRS');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }


            //  ------- Read Data Mahasiswa ----------
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbTahun.Text = mhs.thn_angkatan.ToString();
                LbNpm.Text = mhs.npm.ToString();
                _NPM = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();


            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbTahun.Text = "Tahun Angkatan";
                LbNpm.Text = "NPM";
                LbIdProdi.Text = "";
                _NPM = "";

                //clear Gridview
                DataTable TableKP = new DataTable();
                TableKP.Rows.Clear();
                TableKP.Clear();
                this.GVAmbilKRS.DataSource = TableKP;
                this.GVAmbilKRS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }
          
            //--------------------- INPUT KRS ------------------------------//
            //if (RBInputKRS.Checked)
            //{
            //    try
            //    {
            //        _TotalSKS = 0;
            //        _TotalEditSKS = 0;
            //        _PrevIPS = 0;

            //        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //        using (SqlConnection con = new SqlConnection(CS))
            //        {
            //            //// ------ ==== ACTION CEK MASA KEGIATAN ADA PADA TOMBOL SIMPAN ==== ------ ////
            //            con.Open();
                        
            //            //------------- Read Status AKTIF  --------------------- //
            //            SqlCommand CmdCekStatus = new SqlCommand("SELECT npm FROM dbo.bak_mahasiswa WHERE npm=@NPM AND status <> 'A'", con);
            //            CmdCekStatus.CommandType = System.Data.CommandType.Text;
            //            CmdCekStatus.Parameters.AddWithValue("@NPM", _NPM);

            //            using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
            //            {
            //                if (rdr.HasRows)
            //                {
            //                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Tercatat Sebagai Mahasiswa Aktif');", true);
            //                    return;
            //                }
            //            }

            //            //// 1.  ------------ Cek IP Semester Sebelumnya ------------ ////
            //            SqlCommand CmdCekIPS = new SqlCommand("SpGetPrevIPS", con);
            //            //CmdCekMasa.Transaction = trans;
            //            CmdCekIPS.CommandType = System.Data.CommandType.StoredProcedure;

            //            CmdCekIPS.Parameters.AddWithValue("@ThisSemester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
            //            CmdCekIPS.Parameters.AddWithValue("@npm", _NPM);

            //            SqlParameter CekIPS = new SqlParameter();
            //            CekIPS.ParameterName = "@IPS";
            //            CekIPS.SqlDbType = System.Data.SqlDbType.Decimal;
            //            CekIPS.Precision = 4; // jumlah angka decimal
            //            CekIPS.Scale = 2; // jumlah digit setelah koma
            //            CekIPS.Direction = System.Data.ParameterDirection.Output;
            //            CmdCekIPS.Parameters.Add(CekIPS);

            //            CmdCekIPS.ExecuteNonQuery();

            //            _PrevIPS = Convert.ToDecimal(CekIPS.Value);
            //            if (_PrevIPS < Convert.ToDecimal(2.00))
            //            {
            //                _MaxKRS = 16;
            //            }
            //            else if ((_PrevIPS >= Convert.ToDecimal(2.00)) && (_PrevIPS <= Convert.ToDecimal(2.49)))
            //            {
            //                _MaxKRS = 20;
            //            }
            //            else if ((_PrevIPS >= Convert.ToDecimal(2.50)) && (_PrevIPS <= Convert.ToDecimal(2.99)))
            //            {
            //                _MaxKRS = 22;
            //            }
            //            else if (_PrevIPS >= Convert.ToDecimal(3.00))
            //            {
            //                _MaxKRS = 24;
            //            }

            //            ////Response.Write(_MaxKRS.ToString());

            //            // 2. --------------------- Cek KRS Semester Ini, error jika Sudah ada -----------------------
            //            SqlCommand CekKRS = new SqlCommand("SpCekKrs", con);
            //            CekKRS.CommandType = System.Data.CommandType.StoredProcedure;

            //            CekKRS.Parameters.AddWithValue("@npm", _NPM);
            //            CekKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

            //            CekKRS.ExecuteNonQuery();

            //            // --------------------- Fill Gridview  ------------------------
            //            SqlCommand CmdKRS = new SqlCommand("SpListKRS2", con);
            //            CmdKRS.CommandType = System.Data.CommandType.StoredProcedure;

            //            CmdKRS.Parameters.AddWithValue("@id_prodi", LbIdProdi.Text);
            //            CmdKRS.Parameters.AddWithValue("@jenis_kelas", LbKelas.Text);
            //            CmdKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

            //            DataTable TableKRS = new DataTable();
            //            TableKRS.Columns.Add("Key");
            //            TableKRS.Columns.Add("Kode");
            //            TableKRS.Columns.Add("Mata Kuliah");
            //            TableKRS.Columns.Add("SKS");
            //            TableKRS.Columns.Add("Quota");
            //            TableKRS.Columns.Add("Dosen");
            //            TableKRS.Columns.Add("Kelas");
            //            TableKRS.Columns.Add("Hari");
            //            TableKRS.Columns.Add("Mulai");
            //            TableKRS.Columns.Add("Selesai");
            //            TableKRS.Columns.Add("Ruang");

            //            using (SqlDataReader rdr = CmdKRS.ExecuteReader())
            //            {
            //                if (rdr.HasRows)
            //                {
            //                    this.PanelKRS.Enabled = true;
            //                    this.PanelKRS.Visible = true;

            //                    this.PanelEditKRS.Enabled = false;
            //                    this.PanelEditKRS.Visible = false;

            //                    this.PanelListKRS.Enabled = false;
            //                    this.PanelListKRS.Visible = false;

            //                    while (rdr.Read())
            //                    {
            //                        DataRow datarow = TableKRS.NewRow();
            //                        datarow["Key"] = rdr["no_jadwal"];
            //                        datarow["Kode"] = rdr["kode_makul"];
            //                        datarow["Mata Kuliah"] = rdr["makul"];
            //                        datarow["SKS"] = rdr["sks"];
            //                        datarow["Quota"] = rdr["quota"];
            //                        datarow["Dosen"] = rdr["nama"];
            //                        datarow["Kelas"] = rdr["kelas"];
            //                        datarow["Hari"] = rdr["hari"];
            //                        datarow["Mulai"] = rdr["jm_awal_kuliah"];
            //                        datarow["Selesai"] = rdr["jm_akhir_kuliah"];
            //                        datarow["Ruang"] = rdr["nm_ruang"];

            //                        TableKRS.Rows.Add(datarow);
            //                    }

            //                    //Fill Gridview
            //                    this.GVAmbilKRS.DataSource = TableKRS;
            //                    this.GVAmbilKRS.DataBind();
            //                }
            //                else
            //                {
            //                    //clear Gridview
            //                    TableKRS.Rows.Clear();
            //                    TableKRS.Clear();
            //                    GVAmbilKRS.DataSource = TableKRS;
            //                    GVAmbilKRS.DataBind();

            //                    this.PanelKRS.Enabled = false;
            //                    this.PanelKRS.Visible = false;

            //                    this.PanelEditKRS.Enabled = false;
            //                    this.PanelEditKRS.Visible = false;

            //                    this.PanelListKRS.Enabled = false;
            //                    this.PanelListKRS.Visible = false;

            //                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Tidak Ditemukan');", true);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        this.PanelKRS.Enabled = false;
            //        this.PanelKRS.Visible = false;

            //        this.PanelEditKRS.Enabled = false;
            //        this.PanelEditKRS.Visible = false;

            //        this.PanelListKRS.Enabled = false;
            //        this.PanelListKRS.Visible = false;

            //        this.DLSemester.SelectedIndex = 0;

            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
            //        return;
            //    }
            //} 
            // --------------------------------- EDIT KRS --------------------------------------// 
            //else if (this.RBEditKRS.Checked)
            
            if (this.RBEditKRS.Checked)
            {
                try
                {
                    //1. --------------- Gridview EDIT KRS Semester ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        //------------- Read Status AKTIF  --------------------- //
                        SqlCommand CmdCekStatus = new SqlCommand("SELECT npm FROM dbo.bak_mahasiswa WHERE npm=@NPM AND status <> 'A'", con);
                        CmdCekStatus.CommandType = System.Data.CommandType.Text;
                        CmdCekStatus.Parameters.AddWithValue("@NPM", _NPM);

                        using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Tercatat Sebagai Mahasiswa Aktif');", true);
                                return;
                            }
                        }

                        //1. ------ Cek Masa KRS -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                        CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRS");

                        SqlParameter Status = new SqlParameter();
                        Status.ParameterName = "@output";
                        Status.SqlDbType = System.Data.SqlDbType.VarChar;
                        Status.Size = 20;
                        Status.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Status);

                        CmdCekMasa.ExecuteNonQuery();

                        if (Status.Value.ToString() == "OUT")
                        {
                            try
                            {
                                //2. ------ Cek Masa Batal Tambah -------
                                SqlCommand CmdCekMasaBatal = new SqlCommand("SpCekMasaKeg", con);
                                //CmdCekMasa.Transaction = trans;
                                CmdCekMasaBatal.CommandType = System.Data.CommandType.StoredProcedure;

                                CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                                CmdCekMasaBatal.Parameters.AddWithValue("@jenis_keg", "BatalTambah");

                                SqlParameter StatusBtl = new SqlParameter();
                                StatusBtl.ParameterName = "@output";
                                StatusBtl.SqlDbType = System.Data.SqlDbType.VarChar;
                                StatusBtl.Size = 20;
                                StatusBtl.Direction = System.Data.ParameterDirection.Output;
                                CmdCekMasaBatal.Parameters.Add(StatusBtl);

                                CmdCekMasaBatal.ExecuteNonQuery();

                                if (StatusBtl.Value.ToString() == "OUT")
                                {
                                    con.Close();
                                    con.Dispose();

                                    this.DLSemester.SelectedIndex = 0;
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.DLSemester.SelectedIndex = 0;
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                return;
                            }
                        }

                        //// 3.  ------------ Cek IP Semester Sebelumnya ------------ ////
                        SqlCommand CmdCekIPS = new SqlCommand("SpGetPrevIPS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekIPS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekIPS.Parameters.AddWithValue("@ThisSemester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekIPS.Parameters.AddWithValue("@npm", _NPM);

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

                        //// 3.  ------------ Cek IP Semester Sebelumnya ------------ ////
                        //_PrevIPS = 0;

                        //SqlCommand cmdCekIPS = new SqlCommand("SELECT no,ips FROM dbo.bak_aktv_mhs WHERE npm =@npm AND smster=@semester", con);
                        //cmdCekIPS.CommandType = System.Data.CommandType.Text;

                        //string thn;
                        //string smstr;
                        //if (this.DLSemester.SelectedValue == "1")
                        //{
                        //    int tahun = Convert.ToInt32(this.DLTahun.SelectedValue);
                        //    tahun = tahun - 1;
                        //    thn = Convert.ToString(tahun);
                        //    smstr = "2";
                        //}
                        //else
                        //{
                        //    thn = DLTahun.SelectedValue;
                        //    smstr = "1";
                        //}

                        //cmdCekIPS.Parameters.AddWithValue("@npm", _NPM);
                        //cmdCekIPS.Parameters.AddWithValue("@semester", thn + smstr);

                        //using (SqlDataReader rdr = cmdCekIPS.ExecuteReader())
                        //{
                        //    if (rdr.HasRows)
                        //    {
                        //        while (rdr.Read())
                        //        {
                        //            _PrevIPS = Convert.ToDecimal(rdr["ips"]);
                        //            if (_PrevIPS <= Convert.ToDecimal(1.49))
                        //            {
                        //                _MaxKRS = 12;
                        //            }
                        //            else if ((_PrevIPS >= Convert.ToDecimal(1.50)) && (_PrevIPS <= Convert.ToDecimal(1.99)))
                        //            {
                        //                _MaxKRS = 15;
                        //            }
                        //            else if ((_PrevIPS >= Convert.ToDecimal(2.00)) && (_PrevIPS <= Convert.ToDecimal(2.49)))
                        //            {
                        //                _MaxKRS = 18;
                        //            }
                        //            else if ((_PrevIPS >= Convert.ToDecimal(2.50)) && (_PrevIPS <= Convert.ToDecimal(2.99)))
                        //            {
                        //                _MaxKRS = 21;
                        //            }
                        //            else if (_PrevIPS >= Convert.ToDecimal(3.00))
                        //            {
                        //                _MaxKRS = 24;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //// (TRAKM) Aktifitas Mahasiwa Semester Ini Tidak Ada
                        //        _PrevIPS = 0;
                        //        _MaxKRS = 12;
                        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Aktifitas Mahasiwa Semester Sebelumnya Tidak Ada, Jumlah Maksimal 12 SKS');", true);
                        //        return;
                        //    }
                        //}

                        // 4. ----- Get status edit KRS ----------------
                        SqlCommand CmdCntKRS = new SqlCommand("SpCntEditKRS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCntKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCntKRS.Parameters.AddWithValue("@npm", _NPM);
                        CmdCntKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                        CmdCntKRS.ExecuteNonQuery();

                        // 5. ----- Cek KRS Semester Ini, error jika belum ada -----------
                        SqlCommand CekKRS2 = new SqlCommand("SpCekKrs2", con);
                        CekKRS2.CommandType = System.Data.CommandType.StoredProcedure;

                        CekKRS2.Parameters.AddWithValue("@npm", _NPM);
                        CekKRS2.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CekKRS2.ExecuteNonQuery();

                        //- 6. ----- Fill Gridview Edit KRS ------------------------
                        SqlCommand CmdEdit = new SqlCommand("SpListKRS2", con);
                        CmdEdit.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdEdit.Parameters.AddWithValue("@id_prodi", LbIdProdi.Text);
                        CmdEdit.Parameters.AddWithValue("@jenis_kelas", this.LbKelas.Text);
                        CmdEdit.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

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
                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = true;
                                this.PanelEditKRS.Visible = true;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

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

                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Mahasiswa Tidak Ditemukan');", true);
                                return;
                            }
                        }

                        //-- 7. loop id jadwal yg sudah diambil berdasarkan no jadwal
                        //-- jika ada checked = true
                        int TotalSKS = 0;
                        for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                        {
                            SqlCommand CmdChecked = new SqlCommand("SpGetIdJadwalMhs", con);
                            CmdChecked.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdChecked.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);
                            CmdChecked.Parameters.AddWithValue("@npm", _NPM);
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
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
            //--------------------------- LIHAT KRS ------------------------------ //
            else if (this.RBList.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                try
                {
                    //1. ---------- Gridview SKS ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // --------------------- Fill Gridview  ------------------------
                        SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                        CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdListKRS.Parameters.AddWithValue("@npm", _NPM);
                        CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        DataTable TableKRS = new DataTable();
                        TableKRS.Columns.Add("Key");
                        TableKRS.Columns.Add("Kode");
                        TableKRS.Columns.Add("Mata Kuliah");
                        TableKRS.Columns.Add("SKS");
                        TableKRS.Columns.Add("Dosen");
                        TableKRS.Columns.Add("Kelas");
                        TableKRS.Columns.Add("Hari");
                        TableKRS.Columns.Add("Mulai");
                        TableKRS.Columns.Add("Selesai");
                        TableKRS.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = true;
                                this.PanelListKRS.Visible = true;

                                while (rdr.Read())
                                {
                                    DataRow datarow = TableKRS.NewRow();
                                    datarow["Key"] = rdr["no_jadwal"];
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    datarow["Dosen"] = rdr["nama"];
                                    datarow["Kelas"] = rdr["kelas"];
                                    datarow["Hari"] = rdr["hari"];
                                    datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                    datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                    datarow["Ruang"] = rdr["nm_ruang"];

                                    TableKRS.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVListKrs.DataSource = TableKRS;
                                this.GVListKrs.DataBind();

                                this.DLSemester.SelectedIndex = 0;
                            }
                            else
                            {
                                //clear Gridview
                                TableKRS.Rows.Clear();
                                TableKRS.Clear();
                                GVListKrs.DataSource = TableKRS;
                                GVListKrs.DataBind();

                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
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
                /////// asli

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

                //////---- jika tidak ada makul terpilih ----
                ////for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                ////{
                ////    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                ////    if (ch2.Checked == false)
                ////    {
                ////        UnCheckAll = 1;
                ////    }

                ////    if (UnCheckAll == 1)
                ////    {
                ////        _TotalSKS = 0;
                ////        this.LbJumlahSKS.Text = "0";
                ////    }
                ////}

                ////----- background makul checked ----- //
                //for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                //{
                //    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                //    if (ch2.Checked == true)
                //    {
                //        this.GVAmbilKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                //    }
                //}

                /////// end asli 


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
                //if (_TotalEditSKS > 24)
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

        protected void GVEditKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
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

        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(300);

            // --- =============== Semester Validation ==================== --//
            if ((this.DLSemester.Text != "1") && (this.DLSemester.Text != "2"))
            {
                string message = "alert('Pilih Semester ..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            // --- =============== Cek Checkbox Jadwal Matakuliah =================== --- //
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
                string message = "alert('Pilih Mata Kuliah ..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            // ---- ================== Cek Makul Dobel ================== --- //
            string[] arr = new string[500];
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
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Dobel Mata Kuliah, Proses Dibatalkan ...');", true);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }
                }
            }

            arr = null;

            if (this.LbTahun.Text == "1999/2000" || this.LbTahun.Text == "2000/2001" || this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013" || this.LbTahun.Text == "2013/2014" || this.LbTahun.Text == "2014/2015")
            {
                // insert KRS mahasiswa
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // ----============ Update Data KHS ==================---- //
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

                        SqlCommand CmdUpKHS = new SqlCommand("SpTranKuliahMhs2", con);
                        CmdUpKHS.Transaction = trans;
                        CmdUpKHS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUpKHS.Parameters.AddWithValue("@semester",thn + smstr);
                        CmdUpKHS.Parameters.AddWithValue("@npm", _NPM);
                        CmdUpKHS.ExecuteNonQuery();


                        // ----============ KRS Ambil Skripsi ================-----
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
                                        // ---- =========== TIDAK ADA CEK MASA KRS =============== ----

                                        // ---- =========== INSERT TAGIHAN SKRIPSI =============== ----
                                        SqlCommand CmdSkripsi = new SqlCommand("SpAmbilSkripsi", con);
                                        CmdSkripsi.Transaction = trans;
                                        CmdSkripsi.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdSkripsi.Parameters.AddWithValue("@kd_makul", GVAmbilKRS.Rows[i].Cells[3].Text);
                                        CmdSkripsi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                        CmdSkripsi.Parameters.AddWithValue("@npm", _NPM);

                                        SqlParameter Biaya = new SqlParameter();
                                        Biaya.ParameterName = "@TotalBiaya";
                                        Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                                        Biaya.Size = 20;
                                        Biaya.Direction = System.Data.ParameterDirection.Output;
                                        CmdSkripsi.Parameters.Add(Biaya);

                                        CmdSkripsi.ExecuteNonQuery();

                                        decimal TotalBiaya;
                                        TotalBiaya = Convert.ToDecimal(Biaya.Value.ToString());

                                        string FormattedString9 = string.Format
                                            (new System.Globalization.CultureInfo("id"), "{0:c}", TotalBiaya);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        // ---- =========== INSERT KRS SKRIPSI =============== ----
                                        // ---- Jika Mahasiwa Hanya Menambil Satu Jadwal Kuliah => Tidak Disarankan
                                        //----- ===================================== -----
                                        SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                        cmdInKRS.Transaction = trans;
                                        cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmdInKRS.Parameters.AddWithValue("@npm", _NPM);
                                        cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);
                                        cmdInKRS.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        con.Close();
                                        con.Dispose();

                                        string message = "alert('Input KRS Skripsi Berhasil ...')";
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Skripsi Berhasil ...');", true);
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                con.Close();
                                con.Dispose();
                                Response.Write(ex.ToString());

                                string message = "alert('" + ex.Message + "')";
                                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            }
                            return;
                        }
                        else
                        {
                            // ---- =========== KRS BIASA, LEBIH DARI 1 MATA KULIAH =============== ----
                            // ------------- Cek Masa KRS ------------
                            SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                            CmdCekMasa.Transaction = trans;
                            CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRS");

                            SqlParameter Status = new SqlParameter();
                            Status.ParameterName = "@output";
                            Status.SqlDbType = System.Data.SqlDbType.VarChar;
                            Status.Size = 20;
                            Status.Direction = System.Data.ParameterDirection.Output;
                            CmdCekMasa.Parameters.Add(Status);

                            CmdCekMasa.ExecuteNonQuery();

                            if (Status.Value.ToString() == "OUT")
                            {
                                PanelKRS.Enabled = false;
                                PanelKRS.Visible = false;

                                trans.Rollback();
                                con.Close();
                                con.Dispose();

                                this.PanelEditKRS.Visible = false;
                                this.PanelKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                string msg = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                                ScriptManager.RegisterClientScriptBlock((sender as Control),this.GetType(),"alert",msg,true);
                                return;
                            }

                            // ---- loop insert no jadwal ---
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                    cmdInKRS.Transaction = trans;

                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", _NPM);
                                    cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                    cmdInKRS.ExecuteNonQuery();
                                }
                            }
                        }

                        //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2013 
                        if (this.LbTahun.Text == "2013/2014")
                        {
                            // Procedure Posting Tagihan ke Bank :
                            // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbTahun.Text);
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", _NPM);
                            cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.LbJumlahSKS.Text) * 35000);
                            cmd.Parameters.AddWithValue("@sks", this.LbJumlahSKS.Text);
                            cmd.Parameters.AddWithValue("@dispen", "no");
                            cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            // biaya angsuran + separuh harga SKS
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 35000) / 2));
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            decimal total = biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 35000) / 2);

                            string FormattedString9 = string.Format
                                  (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                        }
                        //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2014
                        else if (this.LbTahun.Text == "2014/2015")
                        {
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", "2014/2015");
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", _NPM);
                            cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.LbJumlahSKS.Text) * 40000);
                            cmd.Parameters.AddWithValue("@sks", this.LbJumlahSKS.Text);
                            cmd.Parameters.AddWithValue("@dispen", "no");
                            cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            // biaya angsuran + separuh biaya SKS dalm 1 semester
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 40000) / 2));
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            decimal total = biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 40000) / 2);

                            string FormattedString9 = string.Format
                                  (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;
                        }

                        // Angsuran 1 Angkatan mulai 2012/2013 ke bawah .... ==> Biaya Saja
                        else if (this.LbTahun.Text == "1999/2000" || this.LbTahun.Text == "2000/2001" || this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013")
                        {
                            // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", LbTahun.Text);
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            // --- insert SKS tidak diperlukan (thn angkatan 2012/2013 dan di bawahnya sistem paket)
                            // --------------------------------------------------
                            //SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            //cmd.Transaction = trans;
                            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            //cmd.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                            //cmd.Parameters.AddWithValue("@semester", _semester);
                            //cmd.Parameters.AddWithValue("@biaya", 0);
                            //cmd.Parameters.AddWithValue("@sks", 0);
                            //cmd.Parameters.AddWithValue("@dispen", "no");
                            //cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            string FormattedString9 = string.Format
                                (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;
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

                        //set jumlah sks = 0
                        this.LbJumlahSKS.Text = "0";
                        _TotalSKS = 0;

                        //// hide panel
                        // this.PanelKRS.Enabled = false;
                        // this.PanelKRS.Visible = false;

                        this.LbJumlahSKS.Text = "";
                        this.DLSemester.SelectedIndex = 0;

                        string mssg = "alert('Input KRS Berhasil')";
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mssg, true);

                    }
                    catch (Exception ex)
                    {
                        this.LbJumlahSKS.Text = " ";
                        this.DLSemester.SelectedIndex = 0;

                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        // clear ceklist jadwal
                        for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            ch.Checked = false;
                        }

                        //set jumlah sks = 0
                        this.LbJumlahSKS.Text = "0";
                        _TotalSKS = 0;

                        string message = "alert('"+ex.Message+"')";
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }
                }
            }
            // --- =========================== (( UTM NEGERI )) ========================== ---- 
            // --- ========== BIAYA UKT Versi 1, DIPERUNTUKKAN KHUSUS MAHASISWA ANGKATAN 2015/2016 ========== ---
            // --- Operasi Dua Databse Untidar dan UKT ---
            else if (this.LbTahun.Text == "2015/2016")
            {
                string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;

                SqlConnection ConUntidar = new SqlConnection(CSUntidar);
                SqlConnection ConUKT = new SqlConnection(CSUKT);

                ConUntidar.Open();
                ConUKT.Open();

                var TransUntidar = ConUntidar.BeginTransaction();
                var TransUKT = ConUKT.BeginTransaction();

                try
                {
                    // 1. ------------- Cek Masa KRS ------------
                    SqlCommand CmdMasaKRS = new SqlCommand("SpCekMasaKeg", ConUntidar, TransUntidar);
                    CmdMasaKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdMasaKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                    CmdMasaKRS.Parameters.AddWithValue("@jenis_keg", "KRS");

                    SqlParameter Status = new SqlParameter();
                    Status.ParameterName = "@output";
                    Status.SqlDbType = System.Data.SqlDbType.VarChar;
                    Status.Size = 20;
                    Status.Direction = System.Data.ParameterDirection.Output;
                    CmdMasaKRS.Parameters.Add(Status);

                    CmdMasaKRS.ExecuteNonQuery();

                    if (Status.Value.ToString() == "OUT")
                    {
                        PanelKRS.Enabled = false;
                        PanelKRS.Visible = false;

                        this.PanelEditKRS.Visible = false;
                        this.PanelKRS.Visible = false;
                        this.DLSemester.SelectedIndex = 0;

                        TransUntidar.Rollback();
                        TransUKT.Rollback();

                        ConUntidar.Close();
                        ConUKT.Close();
                        ConUntidar.Dispose();
                        ConUKT.Dispose();

                        string msg = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                        return;
                    }

                    // 2. ---------- Loop insert no jadwal (INPUT KRS) ------------
                    for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                        if (ch.Checked == true)
                        {
                            SqlCommand cmdInKRS = new SqlCommand("SpInKRS", ConUntidar, TransUntidar);

                            cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdInKRS.Parameters.AddWithValue("@npm", _NPM);
                            cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                            cmdInKRS.ExecuteNonQuery();
                        }
                    }

                    // 3. ----=============== Update Data KHS ==================---- //
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

                    SqlCommand CmdUpKHS = new SqlCommand("SpTranKuliahMhs2", ConUntidar);
                    CmdUpKHS.Transaction = TransUntidar;
                    CmdUpKHS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdUpKHS.Parameters.AddWithValue("@semester", thn + smstr);
                    CmdUpKHS.Parameters.AddWithValue("@npm", _NPM);
                    CmdUpKHS.ExecuteNonQuery();

                    // 4. -------- Get Biaya Semester (UKT) --------
                    SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", ConUKT, TransUKT);
                    cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdBiayaUKT.Parameters.AddWithValue("@npm", this.LbNpm.Text);

                    SqlParameter Biaya = new SqlParameter();
                    Biaya.ParameterName = "@biaya";
                    Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                    Biaya.Size = 20;
                    Biaya.Direction = System.Data.ParameterDirection.Output;
                    cmdBiayaUKT.Parameters.Add(Biaya);

                    cmdBiayaUKT.ExecuteNonQuery();

                    decimal biaya;
                    biaya = Convert.ToDecimal(Biaya.Value.ToString());

                    // 5.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                    // --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                    // -----------------------------------------------------------------------------------------------------------------
                    SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", ConUKT, TransUKT);
                    CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                    CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                    CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                    //CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                    CmdPost.ExecuteNonQuery();

                    // 6.) Get Tagihan Semester (UKT)
                    biaya = Convert.ToDecimal(Biaya.Value.ToString());

                    string FormattedString9 = string.Format (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                    this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                    this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                    // 7.)---------- Simpan Perubahan --------------
                    // ------------------------------------------------------------------------
                    TransUntidar.Commit();
                    TransUKT.Commit();
                    TransUntidar.Dispose();
                    TransUKT.Dispose();
                    ConUKT.Close();
                    ConUntidar.Close();
                    ConUntidar.Dispose();
                    ConUKT.Dispose();

                    // clear ceklist jadwal
                    for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                        ch.Checked = false;
                    }

                    this.DLSemester.SelectedIndex = 0;

                    string mssg = "alert('Input KRS Berhasil...')";
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mssg, true);

                }
                catch (Exception ex)
                {
                    this.DLSemester.SelectedIndex = 0;

                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Visible = false;
                    this.PanelKRS.Visible = false;
                    this.DLSemester.SelectedIndex = 0;

                    // close connection
                    TransUntidar.Rollback();
                    TransUKT.Rollback();
                    TransUntidar.Dispose();
                    TransUKT.Dispose();
                    ConUntidar.Close();
                    ConUKT.Close();
                    ConUntidar.Dispose();
                    ConUKT.Dispose();

                    // clear ceklist jadwal
                    for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                        ch.Checked = false;
                    }

                    // set jumlah sks = 0
                    this.LbJumlahSKS.Text = "0";
                    _TotalSKS = 0;

                    string message = "alert('" + ex.Message + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }



                //string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(CS2))
                //{
                //    con.Open();
                //    SqlTransaction trans = con.BeginTransaction();
                //    try
                //    {
                //        // 1. ------------- Cek Masa KRS ------------
                //        SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                //        CmdCekMasa.Transaction = trans;
                //        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                //        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                //        CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRS");

                //        SqlParameter Status = new SqlParameter();
                //        Status.ParameterName = "@output";
                //        Status.SqlDbType = System.Data.SqlDbType.VarChar;
                //        Status.Size = 20;
                //        Status.Direction = System.Data.ParameterDirection.Output;
                //        CmdCekMasa.Parameters.Add(Status);

                //        CmdCekMasa.ExecuteNonQuery();

                //        if (Status.Value.ToString() == "OUT")
                //        {
                //            PanelKRS.Enabled = false;
                //            PanelKRS.Visible = false;

                //            trans.Rollback();
                //            con.Close();
                //            con.Dispose();

                //            this.PanelEditKRS.Visible = false;
                //            this.PanelKRS.Visible = false;

                //            this.DLSemester.SelectedIndex = 0;

                //            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                //            return;
                //        }


                //        // 2. ----- Loop insert no jadwal (INPUT KRS) -----
                //        for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                //        {
                //            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                //            if (ch.Checked == true)
                //            {
                //                SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                //                cmdInKRS.Transaction = trans;

                //                cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                //                cmdInKRS.Parameters.AddWithValue("@npm", _NPM);
                //                cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                //                cmdInKRS.ExecuteNonQuery();
                //            }
                //        }

                //        // 3. --------------- posting tagihan semester ------------------
                //        // -------------------- ADA PADA DATABASE UKT -------------------
                //        // --------------------------------------------------------------


                //        trans.Commit();
                //        trans.Dispose();

                //        con.Close();
                //        con.Dispose();

                //        this.DLSemester.SelectedIndex = 0;
                //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.LbJumlahSKS.Text = "";
                //        this.DLSemester.SelectedIndex = 0;

                //        trans.Rollback();
                //        con.Close();
                //        con.Dispose();

                //        // clear ceklist jadwal
                //        for (int i = 0; i < this.GVAmbilKRS.Rows.Count; i++)
                //        {
                //            CheckBox ch = (CheckBox)this.GVAmbilKRS.Rows[i].FindControl("CBMakul");
                //            ch.Checked = false;
                //        }

                //        //set jumlah sks = 0
                //        this.LbJumlahSKS.Text = "0";
                //        _TotalSKS = 0;

                //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //        return;
                //    }
                //}





                // --------------------- Untuk awal semester scripts ini tidak dieksekusi --------------------
                // karena semua procedure sudah dilakukan sampai dengan mahasiswa registrasi (sudah lunas semester pertama)
                // ------------------------------------------------------------------------------------------

                //string CS = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(CS))
                //{
                //    con.Open();
                //    SqlTransaction trans = con.BeginTransaction();
                //    try
                //    {
                //        // 1.) Insert Tagihan Periodik Mhs (UKT)
                //        SqlCommand CmdPeriodik = new SqlCommand("SpTagihanPeriodikUkt", con);
                //        CmdPeriodik.Transaction = trans;
                //        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                //        CmdPeriodik.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                //        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                //        CmdPeriodik.ExecuteNonQuery();

                //        // 2.) Insert SKS into DB by using SpInsertSksMhs tidak diperlukan ----

                //        // 3.) Get Biaya Semester (UKT)
                //        SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", con);
                //        cmdBiayaUKT.Transaction = trans;
                //        cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                //        cmdBiayaUKT.Parameters.AddWithValue("@npm", this.LbNpm.Text);

                //        SqlParameter Biaya = new SqlParameter();
                //        Biaya.ParameterName = "@biaya";
                //        Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                //        Biaya.Size = 20;
                //        Biaya.Direction = System.Data.ParameterDirection.Output;
                //        cmdBiayaUKT.Parameters.Add(Biaya);

                //        cmdBiayaUKT.ExecuteNonQuery();

                //        decimal biaya;
                //        biaya = Convert.ToDecimal(Biaya.Value.ToString());

                //        // 4.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                //        // --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                //        // -----------------------------------------------------------------

                //        SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", con);
                //        CmdPost.Transaction = trans;
                //        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                //        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                //        CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                //        CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                //        CmdPost.ExecuteNonQuery();

                //        trans.Commit();
                //        trans.Dispose();
                //    }
                //    catch (Exception ex)
                //    {
                //        trans.Rollback();
                //        con.Close();
                //        con.Dispose();
                //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //        return;
                //    }
                //}

                // --------------------------- End Script Awal --------------------------------------------

            }
            else
            {

            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Cek Semester
            if ((this.DLSemester.Text != "1") && (this.DLSemester.Text != "2"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

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
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }

            // ---- ================== Cek Makul Dobel ================== --- //
            string[] arr = new string[500];
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

                        //duplicate
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Dobel Mata Kuliah, Proses Dibatalkan ...');", true);
                        return;
                    }
                }
            }

            arr = null;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --- =============================================================================
                    //  ------ Get status edit KRS ----------------
                    SqlCommand CmdCntKRS = new SqlCommand("SpCntEditKRS", con);
                    CmdCntKRS.Transaction = trans;
                    CmdCntKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCntKRS.Parameters.AddWithValue("@npm", _NPM);
                    CmdCntKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                    CmdCntKRS.ExecuteNonQuery();


                    // ------ jika status edit blum ada do batal tambah operation -------
                    // -- Batal Tambah / Edit hanya boleh dilakukan SATU KALI SAJA
                    SqlCommand CmdBatal = new SqlCommand("SpInBatalKRS", con);
                    CmdBatal.Transaction = trans;
                    CmdBatal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBatal.Parameters.AddWithValue("@npm", _NPM);
                    CmdBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CmdBatal.ExecuteNonQuery();
                    // --- ==============================================================================


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

                                cmdInKRS.Parameters.AddWithValue("@npm", _NPM);
                                cmdInKRS.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                cmdInKRS.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            SqlCommand cmdDelKRS = new SqlCommand("SpDelKRS", con);
                            cmdDelKRS.Transaction = trans;

                            cmdDelKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdDelKRS.Parameters.AddWithValue("@npm", _NPM);
                            cmdDelKRS.Parameters.AddWithValue("@no_jadwal", GVEditKRS.Rows[i].Cells[2].Text);

                            cmdDelKRS.ExecuteNonQuery();
                        }
                    }

                    //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2013 
                    if (this.LbTahun.Text == "2013/2014")
                    {
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", this.LbJumlahEditSKS.Text);
                        decimal BIAYA;
                        BIAYA = Convert.ToDecimal(this.LbJumlahEditSKS.Text.ToString());
                        cmd.Parameters.AddWithValue("@biaya", BIAYA * 35000);
                        cmd.Parameters.AddWithValue("@npm", _NPM);
                        cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);

                        cmd.ExecuteNonQuery();

                    }
                    //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2014
                    else if (this.LbTahun.Text == "2014/2015")
                    {
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", this.LbJumlahEditSKS.Text);
                        decimal BIAYA;
                        BIAYA = Convert.ToDecimal(this.LbJumlahEditSKS.Text.ToString());
                        cmd.Parameters.AddWithValue("@biaya", BIAYA * 40000);
                        cmd.Parameters.AddWithValue("@npm", _NPM);
                        cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    // Angsuran 1 Angkatan mulai 2012/2013 ke bawah & UKT
                    else if (this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013")
                    {
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Edit Berhasil KRS ...');", true);
                    }

                    trans.Commit();
                    trans.Dispose();

                    con.Close();
                    con.Dispose();

                    // hide panel
                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    //semester
                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Edit KRS Berhasil ...');", true);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    con.Close();
                    con.Dispose();

                    // cleaar chaecked jadwal
                    for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                        ch.Checked = false;
                    }

                    //set jumlah sks = 0
                    this.LbJumlahEditSKS.Text = "0";
                    _TotalEditSKS = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        int TotalSKS = 0;
        protected void GVListKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[3].Text);
                TotalSKS += SKS;

                // this._TotalSkripsi = TotalSKS;
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                //e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Jumlah";
                e.Row.Cells[3].Text = TotalSKS.ToString();
                int JumlahTotalSKS = Convert.ToInt32(e.Row.Cells[3].Text);

                //string FormattedString4 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTotalSKS);
                //e.Row.Cells[1].Text = FormattedString4;
            }
        }
    }
}