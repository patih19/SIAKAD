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

namespace Padu.account
{
    //public partial class WebForm4 : System.Web.UI.Page
    public partial class WebForm4 : Mhs_account
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


        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            //Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _JenisKRS = "";

                _TotalSKS = 0;
                _TotalEditSKS = 0;
                _PrevIPS = 0;
                _MaxKRS = 0;

                try
                {
                    //----- read data mahasiswa ------
                    this.PanelEditKRS.Visible = false;
                    this.PanelKRS.Visible = false;
                    this.PanelListKRS.Visible = false;
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

                    this.PanelEditKRS.Visible = false;
                    this.PanelKRS.Visible = false;

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                    string message = "alert('Mahasiswa tidak ditemukan')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }

        protected void DLSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RBInput.Checked)
            {
                try
                {
                    _TotalSKS = 0;
                    _TotalEditSKS = 0;


                    string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
                    SqlConnection ConUKT = new SqlConnection(CSUKT);
                    ConUKT.Open();

                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        //------------- Read Status AKTIF  --------------------- //
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


                        // --------------- CEK MABA atau NON MABA ----------------//
                        string TahunMhs = "";

                        SqlCommand CmdCekMabaKrs = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                        CmdCekMabaKrs.CommandType = System.Data.CommandType.Text;

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
                            //============= INPUT KRS MAHASISWA BARU ================== //

                            // 1. ------ Cek Masa KRS -------
                            SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                            //CmdCekMasa.Transaction = trans;
                            CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSMABA");

                            SqlParameter Status = new SqlParameter();
                            Status.ParameterName = "@output";
                            Status.SqlDbType = System.Data.SqlDbType.VarChar;
                            Status.Size = 20;
                            Status.Direction = System.Data.ParameterDirection.Output;
                            CmdCekMasa.Parameters.Add(Status);

                            CmdCekMasa.ExecuteNonQuery();

                            if (Status.Value.ToString() == "OUT")
                            {
                                ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DITOLAK ============== -- /////
                                //this.PanelEditKRS.Visible = false;
                                //this.PanelKRS.Visible = false;

                                //this.DLSemester.SelectedValue = "Semester";

                                ////this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                                //string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                //return;

                                ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DIPERBOLEHKAN ============ -- /////
                                try
                                {
                                    //2. ------ Cek Masa Batal Tambah -------
                                    SqlCommand CmdCekMasaBatal = new SqlCommand("SpCekMasaKeg", con);
                                    //CmdCekMasa.Transaction = trans;
                                    CmdCekMasaBatal.CommandType = System.Data.CommandType.StoredProcedure;

                                    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
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

                                        this.DLSemester.SelectedValue = "Semester";

                                        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Baru...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.DLSemester.SelectedValue = "Semester";
                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                                    string message = "alert('" + ex.Message.ToString() + "')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }

                            // 3. ------------- Biodata Mahasiswa ------------
                            string Bidikmisi = "No";

                            SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        if (rdr["bdk"].ToString().Trim() == "1")
                                        {
                                            Bidikmisi = "Yes";
                                        }
                                    }
                                }
                                else
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Pembayaran Tidak Ditemukan');", true);
                                    return;
                                }

                                rdr.Close();
                                rdr.Dispose();
                            }

                            // 4. ------ Cek History Pembayaran Lunas Semester Satu --------
                            // -- Untuk memfilter Mahasiswa Baru Yg tidak lolos Bidikmisi ---
                            if (Bidikmisi == "No")
                            {
                                SqlCommand CmdHistory = new SqlCommand("SpHistoryBayarUkt", ConUKT);
                                CmdHistory.CommandType = System.Data.CommandType.StoredProcedure;

                                CmdHistory.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                                using (SqlDataReader rdr = CmdHistory.ExecuteReader())
                                {
                                    if (rdr.HasRows)
                                    {
                                        while (rdr.Read())
                                        {
                                            if (rdr["status"].ToString().Trim() == "unpaid")
                                            {
                                                string message = "alert('Lakukan Aktivasi Pembayaran UKT Semester Ini')";
                                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                                return;
                                            }
                                            else
                                            {
                                                // lolos sudah bayar ukt semester satu
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Belum aktivasi
                                        string message = "alert('Lakukan Aktivasi Pembayaran Semester Ini')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                        return;
                                    }

                                    rdr.Close();
                                    rdr.Dispose();
                                }
                            }

                        }
                        else
                        {
                            //============= INPUT KRS MAHASISWA LAMA ================== //

                            // 1. ------ Cek Masa KRS -------
                            SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                            //CmdCekMasa.Transaction = trans;
                            CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSNONMABA");

                            SqlParameter Status = new SqlParameter();
                            Status.ParameterName = "@output";
                            Status.SqlDbType = System.Data.SqlDbType.VarChar;
                            Status.Size = 20;
                            Status.Direction = System.Data.ParameterDirection.Output;
                            CmdCekMasa.Parameters.Add(Status);

                            CmdCekMasa.ExecuteNonQuery();

                            if (Status.Value.ToString() == "OUT")
                            {
                                ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DITOLAK ============== -- /////
                                //this.PanelEditKRS.Visible = false;
                                //this.PanelKRS.Visible = false;

                                //this.DLSemester.SelectedValue = "Semester";

                                ////this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                                //string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                //return;


                                ///// -- ============== (Pada Masa Batal Tambah) ==> BELUM KRS DIPERBOLEHKAN ============ -- /////
                                try
                                {
                                    //2. ------ Cek Masa Batal Tambah -------
                                    SqlCommand CmdCekMasaBatal = new SqlCommand("SpCekMasaKeg", con);
                                    //CmdCekMasa.Transaction = trans;
                                    CmdCekMasaBatal.CommandType = System.Data.CommandType.StoredProcedure;

                                    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
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

                                        this.DLSemester.SelectedValue = "Semester";

                                        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Lama...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.DLSemester.SelectedValue = "Semester";
                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                                    string message = "alert('" + ex.Message.ToString() + "')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }

                        }

                        // 3.  ------------ Cek IP Semester Sebelumnya & Get Max SKS------------ ////
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

                        // 4. --------------------- Cek KRS Semester Ini, error jika Sudah ada -----------------------
                        SqlCommand CekKRS = new SqlCommand("SpCekKrs", con);
                        CekKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CekKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CekKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CekKRS.ExecuteNonQuery();

                        // --------------------- Fill Gridview  ------------------------
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

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

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

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

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

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedValue = "Semester";

                    string message = "alert('" + ex.Message.ToString() + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //return;
                }
            }
            else if (this.RBEditKRS.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                _JenisKRS = "EditKRS";

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

                        //-------------- Read Status Validasi KRS  --------------------- //
                        SqlCommand CmdCekValidasi = new SqlCommand("SELECT id_persetujuan, val FROM dbo.bak_persetujuan_krs WHERE npm=@NPM AND semester=@semester AND jenis='krs'", con);
                        CmdCekValidasi.CommandType = System.Data.CommandType.Text;
                        CmdCekValidasi.Parameters.AddWithValue("@NPM", this.Session["Name"].ToString());
                        CmdCekValidasi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());

                        using (SqlDataReader rdr = CmdCekValidasi.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["val"] == DBNull.Value)
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS BELUM DIVALIDASI OLEH DOSEN PEMBIMBING');", true);
                                        return;
                                    }
                                    else if (rdr["val"].ToString() == "1")
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('HUBUNGI DOSEN PEMBIMBING UNTUK MEMBUKA KRS');", true);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('TIDAK ADA PENGAJUAN KRS');", true);
                                return;
                            }
                        }


                        // --- === CEK MABA  ATAU NON MABA === ---
                        string TahunMhs = "";

                        SqlCommand CmdCekMabaKrs = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                        CmdCekMabaKrs.CommandType = System.Data.CommandType.Text;

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
                            // ========= EDIT KRS MAHASISWA BARU =============//

                            SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                            //CmdCekMasa.Transaction = trans;
                            CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSMABA");

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

                                    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
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

                                        this.DLSemester.SelectedValue = "Semester";

                                        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Baru...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.DLSemester.SelectedValue = "Semester";
                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                                    string message = "alert('" + ex.Message.ToString() + "')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }
                        }
                        else
                        {
                            // ========= EDIT KRS MAHASISWA LAMA =============//

                            SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                            //CmdCekMasa.Transaction = trans;
                            CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRSNONMABA");

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

                                    CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
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

                                        this.DLSemester.SelectedValue = "Semester";

                                        string message = "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS Mahasiswa Lama ...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.DLSemester.SelectedValue = "Semester";
                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                                    string message = "alert('" + ex.Message.ToString() + "')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }
                        }

                        //// ------------ Cek IP Semester Sebelumnya & Set Max SKS ------------ ////
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

                        this.LbMaxSKS.Text = _MaxKRS.ToString().Trim();

                        // 2. ----- Get jumlah berapa kali melakukan edit KRS  ( Max 2X )-----
                        SqlCommand CmdCntKRS = new SqlCommand("SpCntEditKRS", con);
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
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedValue = "Semester";

                    string message = "alert('" + ex.Message.ToString() + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //return;
                }
            }
            else if (this.RbBatalTambahKRS.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                _JenisKRS = "BatalTambah";

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

                        //-------------- Read Status Validasi KRS  --------------------- //
                        SqlCommand CmdCekValidasi = new SqlCommand("SELECT id_persetujuan, val FROM dbo.bak_persetujuan_krs WHERE npm=@NPM AND semester=@semester AND jenis='krs'", con);
                        CmdCekValidasi.CommandType = System.Data.CommandType.Text;
                        CmdCekValidasi.Parameters.AddWithValue("@NPM", this.Session["Name"].ToString());
                        CmdCekValidasi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());

                        using (SqlDataReader rdr = CmdCekValidasi.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["val"] == DBNull.Value)
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS BELUM DIVALIDASI OLEH DOSEN PEMBIMBING');", true);
                                        return;
                                    }
                                    else if (rdr["val"].ToString() == "1")
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('HUBUNGI DOSEN PEMBIMBING UNTUK MEMBUKA KRS');", true);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('TIDAK ADA PENGAJAUN KRS');", true);
                                return;
                            }
                        }


                        //1. ------ Cek Masa KRS -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "BatalTambah");

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

                                CmdCekMasaBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
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

                                    this.DLSemester.SelectedValue = "Semester";

                                    string message = "alert('Tidak Ada Jadwal Batal Tambah KRS ...')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal Pengisian Dan Batal Tambah KRS ...');", true);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                this.DLSemester.SelectedValue = "Semester";
                                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                                string message = "alert('" + ex.Message.ToString() + "')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            }
                        }

                        // pastikan jika dari menu edit atau batal tambah krs harus sudah mengisi KRS
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

                        SqlCommand CmdCekMabaKrs = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                        CmdCekMabaKrs.CommandType = System.Data.CommandType.Text;

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

                        this.LbMaxSKS.Text = _MaxKRS.ToString().Trim();

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
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedValue = "Semester";


                    string message = "alert('" + ex.Message.ToString() + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //return;
                }
            }
            else if (this.RBList.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // A. ----------------- Read Status Validasi KRS  ----------------- //
                        string ReadValid = ""; 

                        SqlCommand CmdCekValidasi = new SqlCommand("SELECT id_persetujuan, val FROM dbo.bak_persetujuan_krs WHERE npm=@NPM AND semester=@semester AND jenis='krs'", con);
                        CmdCekValidasi.CommandType = System.Data.CommandType.Text;
                        CmdCekValidasi.Parameters.AddWithValue("@NPM", this.Session["Name"].ToString());
                        CmdCekValidasi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());

                        using (SqlDataReader rdr = CmdCekValidasi.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["val"] == DBNull.Value || rdr["val"].ToString().Trim() == "0")
                                    {
                                        ReadValid = "BelumValid";
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS BELUM DIVALIDASI OLEH DOSEN PEMBIMBING');", true);
                                        //return;
                                    }
                                    else if (rdr["val"].ToString() == "1")
                                    {
                                        ReadValid = "Valid";
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('HUBUNGI DOSEN PEMBIMBING UNTUK MEMBUKA KRS');", true);
                                        //return;
                                    }
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('TIDAK ADA PENGAJAUN KRS');", true);
                                return;
                            }
                        }

                        // B. --------------- VALIDASI ACTION-----------------
                        if (ReadValid == "BelumValid")
                        {

                            Response.Write("KRS Belum Valid");

                            // --- List KRS -----
                            SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                            CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdListKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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

                                    //this.DLSemester.SelectedValue = "Semester";
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

                                    this.DLSemester.SelectedValue = "Semester";

                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                                    string message = "alert('KRS Semester Ini Belum Ada')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }

                            // --- Disable download ---
                            this.BtnDwnKrs.Visible = false;
                            this.BtnDwnKrs.Enabled = false;
                        }
                        else if (ReadValid == "Valid")
                        {

                            Response.Write("KRS Valid");

                            SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                            CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdListKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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

                                    //this.DLSemester.SelectedValue = "Semester";
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

                                    this.DLSemester.SelectedValue = "Semester";

                                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                                    string message = "alert('KRS Semester Ini Belum Ada')";
                                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                }
                            }

                            // --- Enable download ---
                            this.BtnDwnKrs.Visible = true;
                            this.BtnDwnKrs.Enabled = true;
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

                    this.DLSemester.SelectedValue = "Semester";

                    string message = "alert('" + ex.Message.ToString() + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //return;
                }

            }
        }

        protected void GVListKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
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
                        // ----====== Update Data IP Semester (IPS) dan IP Kumulatif (IPK)  ======---- //
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

                        CmdUpKHS.Parameters.AddWithValue("@semester", thn + smstr);
                        CmdUpKHS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdUpKHS.ExecuteNonQuery();

                        // ----============ Ambil Skripsi ================-----
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
                                        // ---- =========== INSERT TAGIHAN SKRIPSI =============== ----
                                        SqlCommand CmdSkripsi = new SqlCommand("SpAmbilSkripsi", con);
                                        CmdSkripsi.Transaction = trans;
                                        CmdSkripsi.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdSkripsi.Parameters.AddWithValue("@kd_makul", GVAmbilKRS.Rows[i].Cells[3].Text);
                                        CmdSkripsi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                        CmdSkripsi.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

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

                                        //-- --------------  INSERT PERSETUJUAN KRS  --------------------  --
                                        SqlCommand CmdPersetujuan = new SqlCommand(""+
                                              "DECLARE @IdPersetujuan BIGINT "+
                                              "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs "+
                                              "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester "+
                                              "IF(@IdPersetujuan IS NULL) "+
                                              "BEGIN "+
                                                "DECLARE @nidn VARCHAR(20) "+
                                                "SELECT        @nidn = bak_dosen.nidn "+
                                                "FROM            bak_dosen INNER JOIN "+
                                                                         "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm "+
                                                "IF(@nidn IS NULL) "+
                                                "BEGIN "+
                                                    "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) "+
                                                    "RETURN "+
                                                "END "+
                                                "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) "+
                                              "END "+
                                              "ELSE "+
                                              "BEGIN "+
                                                "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan "+
                                                "IF(@@ROWCOUNT <> 1) "+
                                                "BEGIN "+
                                                    "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) "+
                                                    "RETURN "+
                                                "END "+
                                              "END" +
                                            "", con);
                                        CmdPersetujuan.Transaction = trans;
                                        CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                                        CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                        CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                                        CmdPersetujuan.ExecuteNonQuery();


                                        // ---- =========== INSERT KRS SKRIPSI =============== ----
                                        // ---- Ambil KRS BIASA 1 Mata Kuliah / Jadwal Tidak Disarankan
                                        //----- ===================================== -----
                                        SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                        cmdInKRS.Transaction = trans;
                                        cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                                        cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);
                                        cmdInKRS.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        con.Close();
                                        con.Dispose();

                                        string message = "alert('KRS Skripsi Berhasil ...')";
                                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Skripsi Berhasil ...');", true);
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                con.Close();
                                con.Dispose();

                                //Response.Write(ex.ToString());

                                string message = "alert('" + ex.Message + "')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            }
                            return;
                        }
                        else
                        {
                            // ---- =========== KRS BIASA, LEBIH DARI 1 MATA KULIAH =============== ----

                            // ---- loop insert no jadwal ---
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                    cmdInKRS.Transaction = trans;

                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
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


                            //5.) INSERT PERSETUJUAN KRS 
                            SqlCommand CmdPersetujuan = new SqlCommand("" +
                                  "DECLARE @IdPersetujuan BIGINT " +
                                  "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                                  "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                                  "IF(@IdPersetujuan IS NULL) " +
                                  "BEGIN " +
                                    "DECLARE @nidn VARCHAR(20) " +
                                    "SELECT        @nidn = bak_dosen.nidn " +
                                    "FROM            bak_dosen INNER JOIN " +
                                                             "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                    "IF(@nidn IS NULL) " +
                                    "BEGIN " +
                                        "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                    "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                                  "END " +
                                  "ELSE " +
                                  "BEGIN " +
                                    "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                    "IF(@@ROWCOUNT <> 1) " +
                                    "BEGIN " +
                                        "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                  "END" +
                                "", con);
                            CmdPersetujuan.Transaction = trans;
                            CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                            CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            CmdPersetujuan.ExecuteNonQuery();

                        }
                        //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2014
                        else if (this.LbTahun.Text.Trim() == "2014/2015")
                        {
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", "2014/2015");
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
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

                            //4.) INSERT PERSETUJUAN KRS 
                            SqlCommand CmdPersetujuan = new SqlCommand("" +
                                  "DECLARE @IdPersetujuan BIGINT " +
                                  "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                                  "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                                  "IF(@IdPersetujuan IS NULL) " +
                                  "BEGIN " +
                                    "DECLARE @nidn VARCHAR(20) " +
                                    "SELECT        @nidn = bak_dosen.nidn " +
                                    "FROM            bak_dosen INNER JOIN " +
                                                             "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                    "IF(@nidn IS NULL) " +
                                    "BEGIN " +
                                        "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                    "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                                  "END " +
                                  "ELSE " +
                                  "BEGIN " +
                                    "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                    "IF(@@ROWCOUNT <> 1) " +
                                    "BEGIN " +
                                        "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                  "END" +
                                "", con);
                            CmdPersetujuan.Transaction = trans;
                            CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                            CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            CmdPersetujuan.ExecuteNonQuery();

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
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            // --- insert SKS tidak diperlukan 
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
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            string FormattedString9 = string.Format
                                (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                            //5.) INSERT PERSETUJUAN KRS 
                            SqlCommand CmdPersetujuan = new SqlCommand("" +
                                  "DECLARE @IdPersetujuan BIGINT " +
                                  "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                                  "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                                  "IF(@IdPersetujuan IS NULL) " +
                                  "BEGIN " +
                                    "DECLARE @nidn VARCHAR(20) " +
                                    "SELECT        @nidn = bak_dosen.nidn " +
                                    "FROM            bak_dosen INNER JOIN " +
                                                             "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                    "IF(@nidn IS NULL) " +
                                    "BEGIN " +
                                        "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                    "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                                  "END " +
                                  "ELSE " +
                                  "BEGIN " +
                                    "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                    "IF(@@ROWCOUNT <> 1) " +
                                    "BEGIN " +
                                        "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                  "END" +
                                "", con);
                            CmdPersetujuan.Transaction = trans;
                            CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                            CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            CmdPersetujuan.ExecuteNonQuery();

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

                        LbJumlahSKS.Text = _TotalSKS.ToString();

                        // set jumlah sks = 0
                        _TotalSKS = 0;

                        this.DLSemester.SelectedValue = "Semester";

                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                        string msg = "alert('Input KRS Berhasil ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                    }
                    catch (Exception ex)
                    {
                        this.LbJumlahSKS.Text = " ";
                        this.DLSemester.SelectedValue = "Semester";

                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        string message = "alert('" + ex.Message + "')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    }
                }
            }
            // --- =========================== (( UTM NEGERI )) ========================== ---- 
            // --- ========== BIAYA UKT Versi 1, DIPERUNTUKKAN KHUSUS MAHASISWA ANGKATAN 2015/2016 dst. ========== ---
            // --- Operasi Dua Databse Untidar dan UKT ---
            else if (ThnAngkatan >= 2015)
            {
                int MhsBaru = 0;
                string TahunMhs = "";

                //------------- Get Mahasiswa Tahun Paling Akhir/Baru  --------------------- //
                string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CSMaba))
                {
                    con.Open();
                    SqlCommand CmdCekStatus = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                    CmdCekStatus.CommandType = System.Data.CommandType.Text;

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
                        CmdMasaKRS.Parameters.AddWithValue("@jenis_keg", "KRSNONMABA");

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
                            this.DLSemester.SelectedValue = "Semester";

                            TransUntidar.Rollback();
                            TransUKT.Rollback();

                            ConUntidar.Close();
                            ConUKT.Close();
                            ConUntidar.Dispose();
                            ConUKT.Dispose();

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);

                            string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            return;
                        }

                        // 2. ----=============== Update Data KHS ==================---- //
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
                        CmdUpKHS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdUpKHS.ExecuteNonQuery();

                        // 3. ---------- Loop insert no jadwal (INPUT KRS) ------------
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


                        //// -===================================================================================
                        //// -================ Sudah Ada pada Menu Aktivasi pembayaran SIPADU ===================
                        //// 4. -------- Get Biaya Semester (UKT) --------
                        //SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", ConUKT, TransUKT);
                        //cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                        //cmdBiayaUKT.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        //SqlParameter Biaya = new SqlParameter();
                        //Biaya.ParameterName = "@biaya";
                        //Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                        //Biaya.Size = 20;
                        //Biaya.Direction = System.Data.ParameterDirection.Output;
                        //cmdBiayaUKT.Parameters.Add(Biaya);

                        //cmdBiayaUKT.ExecuteNonQuery();

                        //decimal biaya;
                        //biaya = Convert.ToDecimal(Biaya.Value.ToString());


                        //// 5.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                        //// --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                        //// -----------------------------------------------------------------------------------------------------------------
                        //SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", ConUKT, TransUKT);
                        //CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        //CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
                        //CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        ////CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                        //CmdPost.ExecuteNonQuery();

                        //// 6.) Get Tagihan Semester (UKT)
                        ////biaya = Convert.ToDecimal(Biaya.Value.ToString());

                        //string FormattedString9 = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                        //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                        //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                        //// - ========================================================================================================================
                        //// - ========================================================================================================================

                        //4. -------------- Filter Cek Pembayaran UKT ----------------
                        // filter sama seperti cek download KRS
                        SqlCommand CmdFilterSemGasal = new SqlCommand("SpCekDownloadKRS", ConUntidar, TransUntidar);
                        CmdFilterSemGasal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdFilterSemGasal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());
                        CmdFilterSemGasal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdFilterSemGasal.ExecuteNonQuery();

                        //5.) INSERT PERSETUJUAN KRS 
                        SqlCommand CmdPersetujuan = new SqlCommand("" +
                              "DECLARE @IdPersetujuan BIGINT " +
                              "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                              "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                              "IF(@IdPersetujuan IS NULL) " +
                              "BEGIN " +
                                "DECLARE @nidn VARCHAR(20) " +
                                "SELECT        @nidn = bak_dosen.nidn " +
                                "FROM            bak_dosen INNER JOIN " +
                                                         "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                "IF(@nidn IS NULL) " +
                                "BEGIN " +
                                    "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                    "RETURN " +
                                "END " +
                                "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                              "END " +
                              "ELSE " +
                              "BEGIN " +
                                "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                "IF(@@ROWCOUNT <> 1) " +
                                "BEGIN " +
                                    "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                    "RETURN " +
                                "END " +
                              "END" +
                            "", ConUntidar, TransUntidar);

                        CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                        CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());
                        CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdPersetujuan.ExecuteNonQuery();



                        // 5.---------- Simpan Perubahan --------------
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
                    catch (Exception ex)
                    {
                        this.DLSemester.SelectedValue = "Semester";

                        this.PanelKRS.Enabled = false;
                        this.PanelKRS.Visible = false;

                        this.PanelEditKRS.Visible = false;
                        this.PanelKRS.Visible = false;

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

                                // ---- INSERT PERSETUJUAN KRS  -----
                                SqlCommand CmdPersetujuan = new SqlCommand("" +
                                      "DECLARE @IdPersetujuan BIGINT " +
                                      "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                                      "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                                      "IF(@IdPersetujuan IS NULL) " +
                                      "BEGIN " +
                                        "DECLARE @nidn VARCHAR(20) " +
                                        "SELECT        @nidn = bak_dosen.nidn " +
                                        "FROM            bak_dosen INNER JOIN " +
                                                                 "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                        "IF(@nidn IS NULL) " +
                                        "BEGIN " +
                                            "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                            "RETURN " +
                                        "END " +
                                        "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                                      "END " +
                                      "ELSE " +
                                      "BEGIN " +
                                        "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                        "IF(@@ROWCOUNT <> 1) " +
                                        "BEGIN " +
                                            "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                            "RETURN " +
                                        "END " +
                                      "END" +
                                    "", con);
                                CmdPersetujuan.Transaction = trans;

                                CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                                CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());
                                CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                                CmdPersetujuan.ExecuteNonQuery();


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
                    {
                        //B.-------- Maba Semester Genap (MULAI BAYAR UKT) {{PROSEDURNYA SAMA PERSIS DENGAN MAHASISWA LAMA / BUKAN MABA }}-------- //

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
                            CmdMasaKRS.Parameters.AddWithValue("@jenis_keg", "KRSMABA");

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
                                this.DLSemester.SelectedValue = "Semester";

                                TransUntidar.Rollback();
                                TransUKT.Rollback();

                                ConUntidar.Close();
                                ConUKT.Close();
                                ConUntidar.Dispose();
                                ConUKT.Dispose();

                                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);

                                string message = "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                return;
                            }

                            // 2. ----=============== Update Data KHS ==================---- //
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
                            CmdUpKHS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                            CmdUpKHS.ExecuteNonQuery();

                            // 3. ---------- Loop insert no jadwal (INPUT KRS) ------------
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


                            //// -===================================================================================
                            //// -================ Sudah Ada pada Menu Aktivasi pembayaran SIPADU ===================
                            //// 4. -------- Get Biaya Semester (UKT) --------
                            //SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", ConUKT, TransUKT);
                            //cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                            //cmdBiayaUKT.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            //SqlParameter Biaya = new SqlParameter();
                            //Biaya.ParameterName = "@biaya";
                            //Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            //Biaya.Size = 20;
                            //Biaya.Direction = System.Data.ParameterDirection.Output;
                            //cmdBiayaUKT.Parameters.Add(Biaya);

                            //cmdBiayaUKT.ExecuteNonQuery();

                            //decimal biaya;
                            //biaya = Convert.ToDecimal(Biaya.Value.ToString());


                            //// 5.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                            //// --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                            //// -----------------------------------------------------------------------------------------------------------------
                            //SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", ConUKT, TransUKT);
                            //CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            //CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
                            //CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            ////CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                            //CmdPost.ExecuteNonQuery();

                            //// 6.) Get Tagihan Semester (UKT)
                            ////biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //string FormattedString9 = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                            //this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            //this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                            //// - ========================================================================================================================
                            //// - ========================================================================================================================



                            //4. -------------- Filter Cek Pembayaran UKT ----------------
                            // filter sama seperti cek download KRS
                            SqlCommand CmdFilterSemGasal = new SqlCommand("SpCekDownloadKRS", ConUntidar, TransUntidar);
                            CmdFilterSemGasal.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdFilterSemGasal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                            CmdFilterSemGasal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            CmdFilterSemGasal.ExecuteNonQuery();


                            //5. ---- INSERT PERSETUJUAN KRS  -----
                            SqlCommand CmdPersetujuan = new SqlCommand("" +
                                  "DECLARE @IdPersetujuan BIGINT " +
                                  "SELECT @IdPersetujuan = id_persetujuan FROM bak_persetujuan_krs " +
                                  "WHERE npm = @npm AND jenis = 'krs' AND semester = @semester " +
                                  "IF(@IdPersetujuan IS NULL) " +
                                  "BEGIN " +
                                    "DECLARE @nidn VARCHAR(20) " +
                                    "SELECT        @nidn = bak_dosen.nidn " +
                                    "FROM            bak_dosen INNER JOIN " +
                                                             "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali WHERE npm = @npm " +
                                    "IF(@nidn IS NULL) " +
                                    "BEGIN " +
                                        "RAISERROR('DOSEN PEMBIMBIMG TIDAK DITEMUKAN, HUBUNGI TU PRODI ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                    "INSERT INTO bak_persetujuan_krs(npm, nidn, jenis, semester) VALUES(@npm, @nidn, 'krs', @semester) " +
                                  "END " +
                                  "ELSE " +
                                  "BEGIN " +
                                    "UPDATE dbo.bak_persetujuan_krs SET val = 1, tgl = GETDATE() WHERE id_persetujuan = @IdPersetujuan " +
                                    "IF(@@ROWCOUNT <> 1) " +
                                    "BEGIN " +
                                        "RAISERROR('UPDATE PERSETUJUAN KRS GAGAL ...', 16, 10) " +
                                        "RETURN " +
                                    "END " +
                                  "END" +
                                "", ConUntidar, TransUntidar);

                            CmdPersetujuan.CommandType = System.Data.CommandType.Text;

                            CmdPersetujuan.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.Trim() + this.DLSemester.Text.Trim());
                            CmdPersetujuan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                            CmdPersetujuan.ExecuteNonQuery();

                            // 5.---------- Simpan Perubahan --------------
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
                        catch (Exception ex)
                        {
                            this.DLSemester.SelectedValue = "Semester";

                            this.PanelKRS.Enabled = false;
                            this.PanelKRS.Visible = false;

                            this.PanelEditKRS.Visible = false;
                            this.PanelKRS.Visible = false;

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

                            string msg = "alert('" + ex.Message + "')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                        }
                    }

                }
            }
            else
            {
                // Kategori Tahun Masuk Tidak Terdaftar
                string msg = "alert('Kategori Tahun Masuk Tidak Terdaftar ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
            }
        }

        // hide key column in Grid View
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
                    // ----=============  Cek EDIT KRS atau BATAL TAMBAH ==============----
                    if (_JenisKRS.ToString().Trim() == "EditKRS")
                    {
                        SqlCommand CmdEditKRS = new SqlCommand("SpInEditKRS", con);
                        CmdEditKRS.Transaction = trans;
                        CmdEditKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdEditKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdEditKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CmdEditKRS.ExecuteNonQuery();
                    }
                    else if (_JenisKRS.ToString().Trim() == "BatalTambah")
                    {
                        SqlCommand CmdBatal = new SqlCommand("SpInBatalKRS", con);
                        CmdBatal.Transaction = trans;
                        CmdBatal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdBatal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CmdBatal.ExecuteNonQuery();
                    }
                    else
                    {
                        string messageerre = "alert('Tidak Tercatat Edit atau Batal Tambah KRS, Proses Dibatalkan ...')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", messageerre, true);
                        return;
                    }
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
                        cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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
                        cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
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

        protected void GVEditKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
        }

        protected void BtnDwnKrs_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadKRS", con);
                    //CmdCekMasa.Transaction = trans;
                    CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                    CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    CmdCekMasa.ExecuteNonQuery();

                    //Server.Transfer("~/am/KartuUTS.aspx", true);   
                    DoDownloadKRS("KRS-" + this.Session["Name"].ToString() + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);
                }
                catch (Exception ex)
                {
                    this.DLSemester.SelectedIndex = 0;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        public byte[] WKHtmlToPdf(string url)
        {
            var fileName = " - ";
            var wkhtmlDir = "C:\\Program Files\\wkhtmltopdf\\";
            var wkhtml = "C:\\Program Files\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
            var p = new Process();

            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = wkhtml;
            p.StartInfo.WorkingDirectory = wkhtmlDir;

            string switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 3mm --margin-bottom 0mm --margin-right 7mm --margin-left 7mm ";
            switches += "--page-size A4 ";
            switches += "--disable-smart-shrinking";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();


            //read output
            byte[] buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }

            // wait or exit
            p.WaitForExit(60000);

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();

            return returnCode == 0 ? file : null;
        }

        private void DoDownloadKRS(string FileName)
        {
            //var url = "http://localhost:2281/am/PrintKRS3.aspx?nim="+LbNPM.Text+"&semester="+Tahun+Semester+"";
            var url = Request.Url.AbsoluteUri;

            int IndMiring = url.LastIndexOf('/');
            var NewUrl = url.Substring(0, IndMiring + 1) + "PrintKRS.aspx?nim=" + this.Session["Name"].ToString() + "&semester=" + this.DLTahun.SelectedItem.Text + this.DLSemester.Text + "";

            //return;
            var file = WKHtmlToPdf(NewUrl);
            if (file != null)
            {
                Response.ContentType = "Application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");
                Response.BinaryWrite(file);
                Response.End();
            }
        }
    }
}