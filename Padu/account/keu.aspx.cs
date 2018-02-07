using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Padu.account
{
    //public partial class WebForm1 : System.Web.UI.Page
    public partial class WebForm1 : Mhs_account
    {
        mhs person = new mhs();

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
            this.Session["jenjang"] = (object)null;
            this.Session["prodi"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.Remove("prodi");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------
        
        // -------- Viewstate variable ---------------
        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }
        public string _thn_angkatan
        {
            get { return this.ViewState["tahun_angkatan"].ToString(); }
            set { this.ViewState["tahun_angkatan"] = (object)value; }
        }
        public string _prodi
        {
            get { return this.ViewState["program_studi"].ToString(); }
            set { this.ViewState["program_studi"] = (object)value; }
        }
        public string _kelas
        {
            get { return this.ViewState["kelas"].ToString(); }
            set { this.ViewState["kelas"] = (object)value; }
        }
        public string _gelombang
        {
            get { return this.ViewState["gelombang"].ToString(); }
            set { this.ViewState["gelombang"] = (object)value; }
        }
        public string _semester
        {
            get{return this.ViewState["semester"].ToString();}
            set{this.ViewState["semester"] = (object)value;}
        }
        public decimal _biaya
        {
            get { return Convert.ToDecimal(this.ViewState["biaya"].ToString()); }
            set { this.ViewState["biaya"] = (object)value; }
        }
        public decimal _biayaSKS
        {
            get{return Convert.ToDecimal(this.ViewState["biayaSKS"].ToString());}
            set{this.ViewState["biayaSKS"] = (object)value;}
        }

        //Viewstate Biaya ....
        public decimal _TotalBebanAwal
        {
            get{ return Convert.ToDecimal(this.ViewState["TotalBebanAwal"].ToString()); }
            set{ this.ViewState["TotalBebanAwal"] = (object)value;}
        }
        public decimal _TotalSKS
        {
            get { return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }
        public decimal _TotalSkripsi
        {
            get { return Convert.ToDecimal(this.ViewState["TotalSkripsi"].ToString()); }
            set { this.ViewState["TotalSkripsi"] = (object)value; }
        }
        public decimal _TotalSPP
        {
            get { return Convert.ToDecimal(this.ViewState["TotalSPP"].ToString()); }
            set { this.ViewState["TotalSPP"] = (object)value; }
        }
        public decimal _TotalBOP
        {
            get { return Convert.ToDecimal(this.ViewState["TotalBOP"].ToString()); }
            set { this.ViewState["TotalBOP"] = (object)value; }
        }
        public decimal _TotalKmhs
        {
            get { return Convert.ToDecimal(this.ViewState["TotalKmhs"].ToString()); }
            set { this.ViewState["TotalKmhs"] = (object)value; }
        }
        public decimal _TotalLab
        {
            get { return Convert.ToDecimal(this.ViewState["TotalLab"].ToString()); }
            set { this.ViewState["TotalLab"] = (object)value; }
        }
        public decimal _TotalKP
        {
            get { return Convert.ToDecimal(this.ViewState["TotalKP"].ToString()); }
            set { this.ViewState["TotalKP"] = (object)value; }
        }
        public decimal _TotalKKN
        {
            get { return Convert.ToDecimal(this.ViewState["TotalKKN"].ToString()); }
            set { this.ViewState["TotalKKN"] = (object)value; }
        }
        public decimal _TotalWisuda
        {
            get { return Convert.ToDecimal(this.ViewState["TotalWisuda"].ToString()); }
            set { this.ViewState["TotalWisuda"] = (object)value; }
        }
        public decimal _TotalPPLI
        {
            get { return Convert.ToDecimal(this.ViewState["TotalPPLI"].ToString()); }
            set { this.ViewState["TotalPPLI"] = (object)value; }
        }
        public decimal _TotalPPLII
        {
            get { return Convert.ToDecimal(this.ViewState["TotalPPLII"].ToString()); }
            set { this.ViewState["TotalPPLII"] = (object)value; }
        }
        public decimal _TotalBayarPeriodik
        {
            get{return Convert.ToDecimal(this.ViewState["TotalBayar"].ToString());}
            set{this.ViewState["TotalBayar"] = (object)value;}
        }

        public decimal _TotalBayarNonPeriodik
        {
            get { return Convert.ToDecimal(this.ViewState["TotalBayarNonPeriodik"].ToString()); }
            set { this.ViewState["TotalBayarNonPeriodik"] = (object)value; }
        }

        //_Kekurangan
        public decimal _Kekurangan
        {
            get { return Convert.ToDecimal(this.ViewState["Kekurangan"].ToString()); }
            set { this.ViewState["Kekurangan"] = (object)value; }
        }

        // ------------ End Viewstate -----------------

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) // page load for the first time
            {
                _NPM = this.Session["Name"].ToString();
                
                // hide panel Bayar SKS
                PanelTblByrSKS2.Visible = false;
                PanelTblByrSKS2.Enabled = false;

                //hide panel transaksi pembayaran
                this.PanelBayar.Enabled = false;
                this.PanelBayar.Visible = false;

                //hide panel SKS
                this.PanelSKS.Enabled = false;
                this.PanelSKS.Visible = false;

                //hide panel angsuran
                PanelAngsuran.Enabled = false;
                PanelAngsuran.Visible = false;

                //click button view
                BtnView_Click(this, null);

                //hide aktifasi
                this.PanelAngsuran.Enabled = false;
                this.PanelAngsuran.Visible = false;

                // AKTIVASI PEMBAYARAN
                LbResultBayar2.Text = "";
                LbViewAngsuran2.Text = "";

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // Top Semester
                    SqlCommand CmdSmstr = new SqlCommand("select TOP 1 semester from keu_biaya group by semester ORDER BY semester DESC", con);
                    CmdSmstr.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader rdr = CmdSmstr.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this._semester = rdr["semester"].ToString();
                                this.LblSmster.Text = rdr["semester"].ToString();
                            }
                        }
                    }


                    SqlCommand cmd = new SqlCommand("SpVwAngsuranBiayaStudi2", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                    cmd.Parameters.AddWithValue("@prodi", _prodi);
                    cmd.Parameters.AddWithValue("@kelas", _kelas);
                    cmd.Parameters.AddWithValue("@semester", _semester);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Angsuran Ke");
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Kelas");
                    Table.Columns.Add("SPP");
                    Table.Columns.Add("BOP");
                    Table.Columns.Add("SKS");
                    Table.Columns.Add("Kemahasiswaan");
                    Table.Columns.Add("Lab.");
                    Table.Columns.Add("Biaya");


                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel angsuran
                            PanelAngsuran.Enabled = true;
                            PanelAngsuran.Visible = true;

                            //this._semester = this.DLSemster2.SelectedItem.Text;
                            if (_thn_angkatan == "2013/2014" || _thn_angkatan == "2014/2015")
                            {
                                // show panel bayar dan SKS
                                this.PanelSKS.Enabled = true;
                                this.PanelSKS.Visible = true;

                                this.PanelBayar.Enabled = true;
                                this.PanelBayar.Enabled = true;
                            }
                            else
                            {
                                // show panel Bayar 
                                this.PanelSKS.Enabled = false;
                                this.PanelSKS.Visible = false;

                                this.PanelBayar.Enabled = true;
                                this.PanelBayar.Enabled = true;
                            }

                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["Angsuran Ke"] = rdr["no_angsuran"];
                                datarow["Semester"] = rdr["semester"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["SPP"] = rdr["SPP"];
                                datarow["BOP"] = rdr["BOP"];
                                datarow["SKS"] = rdr["SKS"];
                                datarow["Kemahasiswaan"] = rdr["kemhs"];
                                datarow["Lab."] = rdr["lab"];
                                datarow["Biaya"] = rdr["biaya_angsuran"];
                                Table.Rows.Add(datarow);
                            }
                            //Fill Gridview
                            this.GVAngsuran2.DataSource = Table;
                            this.GVAngsuran2.DataBind();

                            this.PanelBayar.Enabled = true;
                            this.PanelBayar.Visible = true;

                        }
                        else
                        {
                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVAngsuran2.DataSource = Table;
                            GVAngsuran2.DataBind();

                            this.PanelBayar.Enabled = false;
                            this.PanelBayar.Visible = false;


                            LbViewAngsuran2.Text = "Tabel angsuran Belum Dibuat ...";
                            LbViewAngsuran2.ForeColor = System.Drawing.Color.OrangeRed;
                        }
                    }
                }
            }
            else // page is Postback
            {
                
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            // clear result angsuran
            LbViewAngsuran2.Text = "";

            // hide Bayar SKS
            //PanelTblByrSKS2.Visible = false;

            //Set all of viewstate to zero
            _TotalBebanAwal = 0;

            _TotalSKS = 0;
            _TotalSPP = 0;
            _TotalBOP = 0;
            _TotalKmhs = 0;
            _TotalLab = 0;

            _TotalSkripsi = 0;

            _TotalKP = 0;
            _TotalKKN = 0;
            _TotalWisuda = 0;
            _TotalPPLI = 0;
            _TotalPPLII = 0;

            _TotalBayarPeriodik = 0;
            _TotalBayarNonPeriodik = 0;

            person.ReadMahasiswa(_NPM);

            _gelombang = person.gelombang;
            _kelas = person.kelas;
            _prodi = person.Prodi;
            _thn_angkatan = person.thn_angkatan;

            LbNama2.Text = person.nama;
            LbNPM2.Text = _NPM;
            LBKelas2.Text = person.kelas;
            LBTahun2.Text = person.thn_angkatan;
            LbDsenPA.Text = person.DosenPA;

            
            //---Fill GV Beban Awal ---
            string ConString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT no, npm, beban_awal FROM keu_keu_mhs where npm=@npm", con);
                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("NPM");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["NPM"] = rdr["npm"];
                            if (rdr["beban_awal"] == DBNull.Value)
                            {
                                //BtnViewBiaya2.Enabled = false;
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tagihan Awal belum diinput');", true);
                                return;
                        
                            }
                            else
                            {
                                datarow["Biaya"] = rdr["beban_awal"];
                                Table.Rows.Add(datarow);
                            }
                        }

                        //Fill Gridview
                        this.GVBebanAwal2.DataSource = Table;
                        this.GVBebanAwal2.DataBind();

                        //aktifkan btn view biaya
                        //this.BtnViewBiaya2.Enabled = true;
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVBebanAwal2.DataSource = Table;
                        GVBebanAwal2.DataBind();

                        //nonaktifkan btn view biaya
                        //this.BtnViewBiaya2.Enabled = false;
                    }
                }
            }

            //----Fill GV Tagihan Total ---
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpViewTagihanMhsPerSemester", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Key"); 
                Table.Columns.Add("Semester");
                Table.Columns.Add("SPP");
                Table.Columns.Add("BOP");
                Table.Columns.Add("Kemahasiswaan");
                Table.Columns.Add("Lab.");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Key"] = rdr["id_biaya"];
                            datarow["Semester"] = rdr["semester"];
                            datarow["SPP"] = rdr["SPP"];
                            datarow["BOP"] = rdr["BOP"];
                            datarow["Kemahasiswaan"] = rdr["kemhs"];
                            datarow["Lab."] = rdr["lab"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVTagihanTotal2.DataSource = Table;
                        this.GVTagihanTotal2.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVTagihanTotal2.DataSource = Table;
                        GVTagihanTotal2.DataBind();
                    }
                }
            }

            // --- Fill Grid View SKS Mahasiswa ---
            string CSSKS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSSKS))
            {
                SqlCommand cmd = new SqlCommand("SpVwBiayaSks", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Semester");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Semester"] = rdr["semester"];
                            datarow["Biaya"] = rdr["biaya"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVSks2.DataSource = Table;
                        this.GVSks2.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVSks2.DataSource = Table;
                        GVSks2.DataBind();
                    }
                }
            }

            // --- Fill Grid View SKRIPSI Mahasiswa ---
            string CsSkripsi = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CsSkripsi))
            {
                SqlCommand cmd = new SqlCommand("SpVwBiayaSkripsi", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Semester");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Semester"] = rdr["semester"];
                            datarow["Biaya"] = rdr["biaya"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVSkripsi.DataSource = Table;
                        this.GVSkripsi.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVSkripsi.DataSource = Table;
                        GVSkripsi.DataBind();
                    }
                }
            }

            // ------ Fill Grid View Tagihan KP ------
            string CSKP = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSKP))
            {
                SqlCommand cmd = new SqlCommand("SpBbnKp", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Kerja Praktik");
                Table.Columns.Add("Tahun Pelaksanaan");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Kerja Praktik"] = rdr["kp"];
                            datarow["Tahun Pelaksanaan"] = rdr["thn_pelak"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVTgKp.DataSource = Table;
                        this.GVTgKp.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVTgKp.DataSource = Table;
                        GVTgKp.DataBind();
                    }
                }
            }

            // ------ Fill Grid View Tagihan KKN ------
            string CSKKN = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSKKN))
            {
                SqlCommand cmd = new SqlCommand("SpBbnKkn", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("KKN");
                Table.Columns.Add("Tahun Pelaksanaan");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["KKN"] = rdr["kkn"];
                            datarow["Tahun Pelaksanaan"] = rdr["thn_pelak"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKkn.DataSource = Table;
                        this.GVKkn.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVKkn.DataSource = Table;
                        GVKkn.DataBind();
                    }
                }
            }

            // ------ Fill Grid View Tagihan Wisuda ------
            string CSWisuda = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSWisuda))
            {
                SqlCommand cmd = new SqlCommand("SpBbnWisuda", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Wisuda");
                Table.Columns.Add("Tahun Pelaksanaan");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Wisuda"] = rdr["wisuda"];
                            datarow["Tahun Pelaksanaan"] = rdr["thn_pelak"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVWisudua.DataSource = Table;
                        this.GVWisudua.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVWisudua.DataSource = Table;
                        GVWisudua.DataBind();
                    }
                }
            }

            // ------ Fill Grid View Tagihan PPL I / PPL SD ------
            string CSPplSd = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSPplSd))
            {
                SqlCommand cmd = new SqlCommand("SpBbnPplSd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("PPL I");
                Table.Columns.Add("Tahun Pelaksanaan");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["PPL I"] = rdr["pplsd"];
                            datarow["Tahun Pelaksanaan"] = rdr["thn_pelak"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPplSd.DataSource = Table;
                        this.GVPplSd.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVPplSd.DataSource = Table;
                        GVPplSd.DataBind();
                    }
                }
            }

            // ------ Fill Grid View Tagihan PPL II / PPL SMA ------
            string CSPplSma = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSPplSma))
            {
                SqlCommand cmd = new SqlCommand("SpBbnPplSma", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("PPL II");
                Table.Columns.Add("Tahun Pelaksanaan");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["PPL II"] = rdr["pplsma"];
                            datarow["Tahun Pelaksanaan"] = rdr["thn_pelak"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPplSma.DataSource = Table;
                        this.GVPplSma.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVPplSma.DataSource = Table;
                        GVPplSma.DataBind();
                    }
                }
            }


            // --- Fill Grid View Posting Mahasiswa Sudah & Belum Bayar ---
            string CSPost = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSPost))
            {
                con.Open();

                // ----------------- A. GV Belum Bayar (unpaid) tagihan periodik -----------------------//
                SqlCommand cmdpost = new SqlCommand("SpVwPosting4", con);
                cmdpost.CommandType = System.Data.CommandType.StoredProcedure;

                cmdpost.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table = new DataTable();
                Table.Columns.Add("Nomor");
                Table.Columns.Add("Semester");
                Table.Columns.Add("Angsuran");
                Table.Columns.Add("Cicilan");
                Table.Columns.Add("Tanggal Tagihan");
                //Table.Columns.Add("Tanggal Bayar");
                Table.Columns.Add("Jumlah");
                Table.Columns.Add("Status");

                using (SqlDataReader rdr = cmdpost.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Nomor"] = rdr["billingNo"];
                            datarow["Semester"] = rdr["billRef4"];
                            datarow["Angsuran"] = rdr["billRef5"];
                            datarow["Cicilan"] = rdr["cicilan"];
                            datarow["Tanggal Tagihan"] = rdr["post_date"];
                            // datarow["Tanggal Bayar"] = rdr["pay_date"];
                            datarow["Jumlah"] = rdr["amount_total"];
                            datarow["Status"] = rdr["status"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPost2.DataSource = Table;
                        this.GVPost2.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVPost2.DataSource = Table;
                        GVPost2.DataBind();
                    }
                }

                // ----------------- B. GV Belum Lunas (unpaid) Tagihan Non Periodik -----------------------//
                SqlCommand cmdpost2 = new SqlCommand("SpVwPostingAkhir1", con);
                cmdpost2.CommandType = System.Data.CommandType.StoredProcedure;

                cmdpost2.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table2 = new DataTable();
                Table2.Columns.Add("Nomor");
                Table2.Columns.Add("Angsuran");
                Table2.Columns.Add("Tanggal Tagihan");
                //Table.Columns.Add("Tanggal Bayar");
                Table2.Columns.Add("Jumlah");
                Table2.Columns.Add("Status");

                using (SqlDataReader rdr = cmdpost2.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table2.NewRow();

                            datarow["Nomor"] = rdr["billingNo"];
                            datarow["Angsuran"] = rdr["billRef5"];
                            datarow["Tanggal Tagihan"] = rdr["post_date"];
                            // datarow["Tanggal Bayar"] = rdr["pay_date"];
                            datarow["Jumlah"] = rdr["amount_total"];
                            datarow["Status"] = rdr["status"];
                            Table2.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVUnAkhir.DataSource = Table2;
                        this.GVUnAkhir.DataBind();

                        Table2.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table2.Rows.Clear();
                        Table2.Clear();
                        GVUnAkhir.DataSource = Table2;
                        GVUnAkhir.DataBind();
                    }
                }

                // ------------- C. GV Posting Lunas (paid) Periodik ---------------------//
                SqlCommand cmdpost3 = new SqlCommand("SpVwPosting5", con);
                cmdpost3.CommandType = System.Data.CommandType.StoredProcedure;

                cmdpost3.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table3 = new DataTable();
                Table3.Columns.Add("Nomor");
                Table3.Columns.Add("Semester");
                Table3.Columns.Add("Angsuran");
                Table3.Columns.Add("Cicilan");
                Table3.Columns.Add("Tanggal Tagihan");
                Table3.Columns.Add("Tanggal Bayar");
                Table3.Columns.Add("Jumlah");
                Table3.Columns.Add("Status");

                using (SqlDataReader rdr2 = cmdpost3.ExecuteReader())
                {
                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                            DataRow datarow = Table3.NewRow();

                            datarow["Nomor"] = rdr2["billingNo"];
                            datarow["Semester"] = rdr2["billRef4"];
                            datarow["Angsuran"] = rdr2["billRef5"];
                            datarow["Cicilan"] = rdr2["cicilan"];
                            datarow["Tanggal Tagihan"] = rdr2["post_date"];
                            datarow["Tanggal Bayar"] = rdr2["pay_date"];
                            datarow["Jumlah"] = rdr2["amount_total"];
                            datarow["Status"] = rdr2["status"];
                            Table3.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVBayar.DataSource = Table3;
                        this.GVBayar.DataBind();

                        Table3.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        _TotalBayarPeriodik = 0;

                        Table3.Rows.Clear();
                        Table3.Clear();
                        GVBayar.DataSource = Table3;
                        GVBayar.DataBind();
                    }
                }

                // ------------- D. GV Posting Lunas (paid) Non Periodik ---------------------//
                SqlCommand cmdpost4 = new SqlCommand("SpVwPostingAkhir2", con);
                cmdpost4.CommandType = System.Data.CommandType.StoredProcedure;

                cmdpost4.Parameters.AddWithValue("@npm", _NPM);

                DataTable Table4 = new DataTable();
                Table4.Columns.Add("Nomor");
                Table4.Columns.Add("Semester");
                Table4.Columns.Add("Angsuran");
                Table4.Columns.Add("Cicilan");
                Table4.Columns.Add("Tanggal Tagihan");
                Table4.Columns.Add("Tanggal Bayar");
                Table4.Columns.Add("Jumlah");
                Table4.Columns.Add("Status");

                using (SqlDataReader rdr2 = cmdpost4.ExecuteReader())
                {
                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                            DataRow datarow = Table4.NewRow();

                            datarow["Nomor"] = rdr2["billingNo"];
                            datarow["Semester"] = rdr2["billRef4"];
                            datarow["Angsuran"] = rdr2["billRef5"];
                            datarow["Cicilan"] = rdr2["cicilan"];
                            datarow["Tanggal Tagihan"] = rdr2["post_date"];
                            datarow["Tanggal Bayar"] = rdr2["pay_date"];
                            datarow["Jumlah"] = rdr2["amount_total"];
                            datarow["Status"] = rdr2["status"];
                            Table4.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVLunasAkhir.DataSource = Table4;
                        this.GVLunasAkhir.DataBind();

                        Table3.Dispose();
                    }
                    else
                    {
                        //clear Gridview

                        Table4.Rows.Clear();
                        Table4.Clear();
                        GVLunasAkhir.DataSource = Table4;
                        GVLunasAkhir.DataBind();
                    }
                }
            }


            // label kewajiban dalam format rupiah Rp
            decimal Kewajiban = _TotalBebanAwal + _TotalSKS + _TotalSPP + _TotalBOP + _TotalKmhs + _TotalLab + _TotalKP + _TotalKKN + _TotalPPLI + _TotalPPLII + _TotalWisuda + _TotalSkripsi;
            string FormattedString5 = string.Format
                (new System.Globalization.CultureInfo("id"), "{0:c}", Kewajiban);
            this.LbKewajiban2.Text = FormattedString5;

            //label terbayar dalam format rupiah Rp
            decimal Terbayar = _TotalBayarPeriodik + _TotalBayarNonPeriodik;
            string FormattedString6 = string.Format
                (new System.Globalization.CultureInfo("id"), "{0:c}", Terbayar);
            this.LbTerbayar.Text = FormattedString6;

            //Label Kekurangan
            decimal Kekurangan = Kewajiban - Terbayar;
            _Kekurangan = Kekurangan;
            string FormattedString7 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kekurangan);
            this.LbKurang.Text = FormattedString7;
            this.LbKekurangan2.Text = FormattedString7;

            if (this.LbKekurangan2.Text != "" && this.LbNama2.Text !="")
            {
                this.PanelTblByrSKS2.Enabled = true;
                this.PanelTblByrSKS2.Visible = true;
            }
            if (_Kekurangan < 0)
            {
                this.PanelTblByrSKS2.Enabled = false;
                this.PanelTblByrSKS2.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak ada kekurangan pembayaran proses aktifasi dibatalkan');", true);
                return;
            }

            // ============= control beban awal harus nol & matikan aktifasi ===============
            ////Cek Beban Awal Lunas
            //if (_TotalBebanAwal == 0)
            //{
            //    this.PanelTblByrSKS2.Enabled = true;
            //    this.PanelTblByrSKS2.Visible = true;
            //}

            //// Ada Kekurangan Pembayaran Aktifasi tidak diizinkan
            //if (_TotalBebanAwal != 0)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Anda Masih Memiliki Kekurangan, Aktifasi Gagal, Hubungi Admin Puskominfo');", true);
            //    return;
            //}
            // =================================================================================
        }

        // View List Biaya Angsuran Mahasiswa
        protected void BtnViewBiaya_Click(object sender, EventArgs e)
        {
            ////this._semester = this.DLSemster2.SelectedItem.Text;

            //LbResultBayar2.Text = "";
            //LbViewAngsuran2.Text = "";

            //string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{

            //    SqlCommand cmd = new SqlCommand("SpVwAngsuranBiayaStudi2", con);
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    cmd.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
            //    cmd.Parameters.AddWithValue("@prodi", _prodi);
            //    cmd.Parameters.AddWithValue("@kelas", _kelas);
            //    cmd.Parameters.AddWithValue("@semester",_semester);

            //    DataTable Table = new DataTable();
            //    Table.Columns.Add("Angsuran Ke");
            //    Table.Columns.Add("Semester");
            //    Table.Columns.Add("Kelas");
            //    Table.Columns.Add("SPP");
            //    Table.Columns.Add("BOP");
            //    Table.Columns.Add("SKS");
            //    Table.Columns.Add("Kemahasiswaan");
            //    Table.Columns.Add("Lab.");
            //    Table.Columns.Add("Biaya");

            //    con.Open();
            //    using (SqlDataReader rdr = cmd.ExecuteReader())
            //    {
            //        if (rdr.HasRows)
            //        {
            //            //show panel angsuran
            //            PanelAngsuran.Enabled = true;
            //            PanelAngsuran.Visible = true;

            //           // this._semester = this.DLSemster2.SelectedItem.Text;
            //            if (_thn_angkatan == "2013/2014" || _thn_angkatan == "2014/2015")
            //            {
            //                // show panel bayar dan SKS
            //                this.PanelSKS.Enabled = true;
            //                this.PanelSKS.Visible = true;

            //                this.PanelBayar.Enabled = true;
            //                this.PanelBayar.Enabled = true;
            //            }
            //            else
            //            {
            //                // show panel Bayar 
            //                this.PanelSKS.Enabled = false;
            //                this.PanelSKS.Visible = false;

            //                this.PanelBayar.Enabled = true;
            //                this.PanelBayar.Enabled = true;
            //            }

            //            while (rdr.Read())
            //            {
            //                DataRow datarow = Table.NewRow();

            //                datarow["Angsuran Ke"] = rdr["no_angsuran"];
            //                datarow["Semester"] = rdr["semester"];
            //                datarow["Kelas"] = rdr["kelas"];
            //                datarow["SPP"] = rdr["SPP"];
            //                datarow["BOP"] = rdr["BOP"];
            //                datarow["SKS"] = rdr["SKS"];
            //                datarow["Kemahasiswaan"] = rdr["kemhs"];
            //                datarow["Lab."] = rdr["lab"];
            //                datarow["Biaya"] = rdr["biaya_angsuran"];
            //                Table.Rows.Add(datarow);
            //            }
            //            //Fill Gridview
            //            this.GVAngsuran2.DataSource = Table;
            //            this.GVAngsuran2.DataBind();

            //            this.PanelBayar.Enabled = true;
            //            this.PanelBayar.Visible = true;

            //        }
            //        else
            //        {
            //            //clear Gridview
            //            Table.Rows.Clear();
            //            Table.Clear();
            //            GVAngsuran2.DataSource = Table;
            //            GVAngsuran2.DataBind();

            //            this.PanelBayar.Enabled = false;
            //            this.PanelBayar.Visible = false;
                       

            //            LbViewAngsuran2.Text = "Tabel angsuran Belum Dibuat ...";
            //            LbViewAngsuran2.ForeColor = System.Drawing.Color.OrangeRed;
            //        }
            //    }
            //}
        }

        // Button Bayar Click
        protected void BtnOk_Click(object sender, EventArgs e)
        {
            LbResultBayar2.Text = "";

            // hitung checkbox selected
            int cnt = 0;
            for (int i = 0; i < GVAngsuran2.Rows.Count; i++)
            {
                CheckBox CB = (CheckBox)GVAngsuran2.Rows[i].FindControl("CBPilih");
                if (CB.Checked == true)
                {
                    cnt += 1;
                }
            }
            // checkbox selected
            if (cnt > 1 || cnt == 0)
            {
                //client message check list lebih dari 1 item atau belum pilih check list.....
                ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
                return;
            }
            else
            {
                for (int i = 0; i < GVAngsuran2.Rows.Count; i++)
                {
                    // Cek satus Unpaid
                    // jika masih ada tagihan Unpaid proses dibatalkan...
                    string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS2))
                    {
                        con.Open();
                        try
                        {
                            SqlCommand cmd = new SqlCommand("CekUnpaid", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@npm", _NPM);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            con.Dispose();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                            return;
                        }
                    }

                    CheckBox CB = (CheckBox)GVAngsuran2.Rows[i].FindControl("CBPilih");
                    if (CB.Checked == true)
                    {
                        string angsuran = GVAngsuran2.Rows[i].Cells[1].Text;
                        if (angsuran == "1")
                        {
                            // Angsuran Pertama tahun 2014/15 ==> Biaya dan SKS
                            if (_thn_angkatan == "2014/2015")
                            {
                                //// cek sks > 24 SKS
                                //if (Convert.ToInt32(TbTotalSKS2.Text) > 24)
                                //{
                                //    //client message check list lebih dari 1 item atau belum pilih check list.....
                                //    ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('SKS Tidak Boleh Lebih Dari 24');", true);
                                //    return;
                                //}

                                if (this.TbTotalSKS2.Text == "" || this.TbTotalSKS2.Text == "0")
                                {
                                    ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('Jumlah SKS Harus Diisi');", true);
                                    return;
                                }

                                // Insert into DB
                                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(CS))
                                {
                                    //open connection and begin transaction
                                    con.Open();
                                    SqlTransaction trans = con.BeginTransaction();

                                    try
                                    {   // Procedure Posting Tagihan ke Bank :
                                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                                        CmdPeriodik.Transaction = trans;
                                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                                        CmdPeriodik.Parameters.AddWithValue("@prodi", _prodi);
                                        CmdPeriodik.Parameters.AddWithValue("@kelas", _kelas);
                                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", _semester);
                                        CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                                        cmd.Transaction = trans;
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@npm", _NPM);
                                        cmd.Parameters.AddWithValue("@semester", _semester);
                                        cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.TbTotalSKS2.Text) * 40000);
                                        cmd.Parameters.AddWithValue("@sks", TbTotalSKS2.Text);
                                        cmd.Parameters.AddWithValue("@dispen", "no");
                                        cmd.ExecuteNonQuery();

                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        // biaya angsuran + separuh biaya SKS dalm 1 semester
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 40000) / 2);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        cmd.Dispose();
                                        CmdPeriodik.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        decimal BSks = ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 40000) / 2;
                                        decimal total = _biaya + BSks;

                                        string FormattedString9 = string.Format
                                              (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;


                                        this.TbTotalSKS2.Text = "";
                                        CB.Checked = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        trans.Rollback();
                                        con.Close();
                                        con.Dispose();
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                    }
                                }
                            }
                            else if (_thn_angkatan == "2013/2014")
                            {
                                // cek sks > 24 SKS
                                //if (Convert.ToInt32(TbTotalSKS2.Text) > 24)
                                //{
                                //    //client message check list lebih dari 1 item atau belum pilih check list.....
                                //    ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('SKS Tidak Boleh Lebih Dari 24');", true);
                                //    return;
                                //}

                                if (this.TbTotalSKS2.Text == "" || this.TbTotalSKS2.Text == "0")
                                {
                                    ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('Jumlah SKS Harus Diisi');", true);
                                    return;
                                }

                                // Insert into DB
                                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(CS))
                                {
                                    //open connection and begin transaction
                                    con.Open();
                                    SqlTransaction trans = con.BeginTransaction();

                                    try
                                    {   // Procedure Posting Tagihan ke Bank :
                                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                                        CmdPeriodik.Transaction = trans;
                                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                                        CmdPeriodik.Parameters.AddWithValue("@prodi", _prodi);
                                        CmdPeriodik.Parameters.AddWithValue("@kelas", _kelas);
                                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", _semester);
                                        CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                                        cmd.Transaction = trans;
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@npm", _NPM);
                                        cmd.Parameters.AddWithValue("@semester", _semester);
                                        cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.TbTotalSKS2.Text) * 35000);
                                        cmd.Parameters.AddWithValue("@sks", TbTotalSKS2.Text);
                                        cmd.Parameters.AddWithValue("@dispen", "no");
                                        cmd.ExecuteNonQuery();


                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        // biaya angsuran + separuh harga SKS
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 35000) / 2);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        cmd.Dispose();
                                        CmdPeriodik.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        decimal total = _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 35000) / 2;

                                        string FormattedString9 = string.Format
                                              (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        this.TbTotalSKS2.Text = "";
                                        CB.Checked = false;

                                    }
                                    catch (Exception ex)
                                    {
                                        trans.Rollback();
                                        con.Close();
                                        con.Dispose();
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                    }
                                }
                            }
                            else // Angsuran 1 Angkatan mulai 2012/2013 ke bawah .... ==> Biaya Saja
                            {
                                // Insert into DB
                                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(CS))
                                {
                                    //open connection and begin transaction
                                    con.Open();
                                    SqlTransaction trans = con.BeginTransaction();

                                    try
                                    {   // Procedure Posting Tagihan ke Bank :
                                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                                        CmdPeriodik.Transaction = trans;
                                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                                        CmdPeriodik.Parameters.AddWithValue("@prodi", _prodi);
                                        CmdPeriodik.Parameters.AddWithValue("@kelas", _kelas);
                                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", _semester);
                                        CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                                        // --- insert SKS tidak diperlukan 
                                        // --------------------------------------------------
                                        //SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                                        //cmd.Transaction = trans;
                                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        //cmd.Parameters.AddWithValue("@npm", this._NPM);
                                        //cmd.Parameters.AddWithValue("@semester", _semester);
                                        //cmd.Parameters.AddWithValue("@biaya", 0);
                                        //cmd.Parameters.AddWithValue("@sks", 0);
                                        //cmd.Parameters.AddWithValue("@dispen", "no");
                                        //cmd.ExecuteNonQuery();

                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();
                                        //cmd.Dispose();
                                        CmdPeriodik.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        string FormattedString9 = string.Format
                                              (new System.Globalization.CultureInfo("id"), "{0:c}", _biaya);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        this.TbTotalSKS2.Text = "";
                                        CB.Checked = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        trans.Rollback();
                                        con.Close();
                                        con.Dispose();
                                        LbResultBayar2.Text = ex.Message.ToString();
                                        LbResultBayar2.ForeColor = System.Drawing.Color.Red;
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //Format Grid View Angsuran Mahasiswa
        protected void GVAngsuran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SPP = Convert.ToInt32(e.Row.Cells[4].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                e.Row.Cells[4].Text = FormattedString4;

                int BOP = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString5 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BOP);
                e.Row.Cells[5].Text = FormattedString5;

               //_biayaSKS = Convert.ToDecimal(e.Row.Cells[6].Text);

                int SKS = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString6 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                e.Row.Cells[6].Text = FormattedString6;

                int Kmhs = Convert.ToInt32(e.Row.Cells[7].Text);
                string FormattedString7 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kmhs);
                e.Row.Cells[7].Text = FormattedString7;

                int Lab = Convert.ToInt32(e.Row.Cells[8].Text);
                string FormattedString8 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Lab);
                e.Row.Cells[8].Text = FormattedString8;

                _biaya = Convert.ToDecimal(e.Row.Cells[9].Text);

                int Biaya = Convert.ToInt32(e.Row.Cells[9].Text);
                string FormattedString9 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[9].Text = FormattedString9;
            }
        }

       // Format And Count Total Item in Grid VIew Tagihan Mahasiswa
        int TotalSPP = 0;
        int TotalBOP = 0;
        int TotalKmhs = 0;
        int TotalLab = 0;
        protected void GVTagihanTotal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[5].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SPP = Convert.ToInt32(e.Row.Cells[2].Text);
                TotalSPP += SPP;
                this._TotalSPP = TotalSPP;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                e.Row.Cells[2].Text = FormattedString4;

                int BOP = Convert.ToInt32(e.Row.Cells[3].Text);
                TotalBOP += BOP;
                this._TotalBOP=TotalBOP;
                string FormattedString5 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BOP);
                e.Row.Cells[3].Text = FormattedString5;

                int Kmhs = Convert.ToInt32(e.Row.Cells[4].Text);
                TotalKmhs += Kmhs;
                this._TotalKmhs = TotalKmhs;
                string FormattedString6 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kmhs);
                e.Row.Cells[4].Text = FormattedString6;

                int Lab = Convert.ToInt32(e.Row.Cells[5].Text);
                TotalLab += Lab;
                this._TotalLab = TotalLab;
                string FormattedString7 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Lab);
                e.Row.Cells[5].Text = FormattedString7;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Jumlah :";
                e.Row.Cells[2].Text = TotalSPP.ToString();
                e.Row.Cells[3].Text = TotalBOP.ToString();
                e.Row.Cells[4].Text = TotalKmhs.ToString();
                e.Row.Cells[5].Text = TotalLab.ToString();

                int SPP = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                e.Row.Cells[2].Text = FormattedString4;

                int BOP = Convert.ToInt32(e.Row.Cells[3].Text);
                string FormattedString5 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BOP);
                e.Row.Cells[3].Text = FormattedString5;

                int Kmhs = Convert.ToInt32(e.Row.Cells[4].Text);
                string FormattedString6 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kmhs);
                e.Row.Cells[4].Text = FormattedString6;

                int Lab = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString7 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Lab);
                e.Row.Cells[5].Text = FormattedString7;
            }
        }
       
        //Tagihan & Format Rupiah Tagihan Awal
        int TotalBebanAwal = 0;
        protected void GVBebanAwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Beban = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalBebanAwal += Beban;
                this._TotalBebanAwal = TotalBebanAwal;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Beban);
                e.Row.Cells[1].Text = FormattedString4;
            }
        }
        
        // Tagihan & Format Rupiah Biaya SKS Mahasiswa
        int TotalSKS = 0;
        protected void GVSks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BiayaSks = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalSKS += BiayaSks;
                this._TotalSKS = TotalSKS;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BiayaSks);
                e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[1].Text = TotalSKS.ToString();

                int BiayaSKS = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BiayaSKS);
                e.Row.Cells[1].Text = FormattedString4;
            }
        }

        int TotalSkripsi = 0;
        protected void GVSkripsi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BiayaSkripsi = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalSkripsi += BiayaSkripsi;
                this._TotalSkripsi = TotalSkripsi;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BiayaSkripsi);
                e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[1].Text = TotalSkripsi.ToString();

                int BiayaSkripsi = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BiayaSkripsi);
                e.Row.Cells[1].Text = FormattedString4;
            }
        }


        // GV Belum Bayar (UNPAID)
        protected void GVPost2_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[5].Text);
                //TotalUnpaid += jumlah;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[5].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[0].Text = "Jumlah :";
                //e.Row.Cells[5].Text = TotalUnpaid.ToString();

                //int JumlahUnpaid = Convert.ToInt32(e.Row.Cells[5].Text);
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahUnpaid);
                //e.Row.Cells[5].Text = FormattedString1;
            }
        }

        //GV Sudah Bayar (PAID) Tagihan Periodik
        int TotalBayarPeriodik = 0;
        protected void GVBayar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[6].Text);
                TotalBayarPeriodik += jumlah;
                this._TotalBayarPeriodik = TotalBayarPeriodik;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[6].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[6].Text = TotalBayarPeriodik.ToString();

                int JumlahTerbayar = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTerbayar);
                e.Row.Cells[6].Text = FormattedString1;
            }
        }

        //GV Sudah Bayar (PAID) Tagihan Non periodik
        int TotalBayarNonPeriodik = 0;
        protected void GVLunasAkhir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[6].Text);
                TotalBayarNonPeriodik += jumlah;
                this._TotalBayarNonPeriodik = TotalBayarNonPeriodik;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[6].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[6].Text = TotalBayarNonPeriodik.ToString();

                int JumlahTerbayar = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTerbayar);
                e.Row.Cells[6].Text = FormattedString1;
            }
        }



        //Tagihan & format Rupaih KP
        int TagihanKP = 0;
        protected void GVTgKp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BebanKP = Convert.ToInt32(e.Row.Cells[0].Text);
                TagihanKP += BebanKP;
                this._TotalKP = TagihanKP;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BebanKP);
                e.Row.Cells[0].Text = FormattedString4;
            }
        }

        //Tagihan & format Rupaih KKN
        int TagihanKKN = 0;
        protected void GVKkn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BebanKKN = Convert.ToInt32(e.Row.Cells[0].Text);
                TagihanKKN += BebanKKN;
                this._TotalKKN = TagihanKKN;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BebanKKN);
                e.Row.Cells[0].Text = FormattedString4;
            }
        }

        //Tagihan & format Rupaih KKN
        int TagihanWisuda = 0;
        protected void GVWisudua_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BebanWisuda = Convert.ToInt32(e.Row.Cells[0].Text);
                TagihanWisuda += BebanWisuda;
                this._TotalWisuda = TagihanWisuda;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BebanWisuda);
                e.Row.Cells[0].Text = FormattedString4;
            }
        }

        //Tagihan & format Rupaih PPL SD / PPL I
        int TagihanPPLI = 0;
        protected void GVPplSd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BebanPPLI = Convert.ToInt32(e.Row.Cells[0].Text);
                TagihanPPLI += BebanPPLI;
                this._TotalPPLI = TagihanPPLI;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BebanPPLI);
                e.Row.Cells[0].Text = FormattedString4;
            }
        }

        //Tagihan & format Rupaih PPL SMA / PPL II
        int TagihanPPLII = 0;
        protected void GVPplSma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int BebanPPLII = Convert.ToInt32(e.Row.Cells[0].Text);
                TagihanPPLII += BebanPPLII;
                this._TotalPPLII = TagihanPPLII;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BebanPPLII);
                e.Row.Cells[0].Text = FormattedString4;
            }
        }

        protected void BtnBayar2_Click(object sender, EventArgs e)
        {
            // Cek tagiha Unpaid
            // jika masih ada tagihan unpaid proses dibatalkan ...

            string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS2))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("CekUnpaid", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@npm", _NPM);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }

            // ------------------------------------------------------------------------- //
            //// Angsuran Kedua tahun 2013/14 dan 2014/15
            //// tetapi pastikan untuk angkatan 2013/14 dan 2014/15 sudah mengambil KRS

            this.TbTotalSKS2.Text = "";
            if (_thn_angkatan == "2013/2014" || _thn_angkatan == "2014/2015")
            {
                if (_Kekurangan == 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Anda Tidak Memiliki Kekurangan');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        // Procedure Posting Tagihan ke Bank :
                        //1.) Check SKS ==> memastikan sudah KRS belum ---
                        SqlCommand CmdSKS = new SqlCommand("SpVwSksSemester", con);
                        CmdSKS.Transaction = trans;
                        CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdSKS.Parameters.AddWithValue("@npm", _NPM);
                        CmdSKS.Parameters.AddWithValue("@semester", _semester);
                        CmdSKS.ExecuteNonQuery();

                        // 2.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", _prodi);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", _kelas);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", _semester);
                        CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                        CmdPeriodik.ExecuteNonQuery();

                        ////3.) Get Setengah harga SKS
                        //SqlCommand CmdSks = new SqlCommand("SpSeparuhSKS", con);
                        //CmdSks.Transaction = trans;
                        //CmdSks.CommandType = System.Data.CommandType.StoredProcedure;

                        //CmdSks.Parameters.AddWithValue("@semester", _semester);
                        //CmdSks.Parameters.AddWithValue("@npm", _NPM);

                        //decimal HalfSks = 0;
                        //using (SqlDataReader rdr = CmdSks.ExecuteReader())
                        //{
                        //    if (rdr.HasRows)
                        //    {
                        //        while (rdr.Read())
                        //        {
                        //            HalfSks = (Convert.ToDecimal(rdr["biaya"])) / 2;
                        //        }
                        //    }
                        //}

                        //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", _Kekurangan);
                        CmdPost.Parameters.AddWithValue("@angsuran", "2");
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();

                        CmdSKS.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", _Kekurangan);
                        this.LbPostSuccess2.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                        this.LbPostSuccess2.ForeColor = System.Drawing.Color.Green;

                        //clear Textbox SKS dan Checkbox
                        this.TbTotalSKS2.Text = "";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        LbResultBayar3.Text = ex.Message.ToString();
                        LbResultBayar3.ForeColor = System.Drawing.Color.Red;
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                }
            }
            else
            // angkatan 2012/2013 dan di bawahnya ==> Biaya saja
            {
                if (_Kekurangan == 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Anda Tidak Memiliki Kekurangan');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // --- Procedure Posting Tagihan ke Bank --- :

                        //1.) Karena di angsuran ke 2 Check dulu SKS ==> memastikan sudah KRS belum ---
                        // Thn 12/13 ke bawah, Sementara tidak dulu menunggu bisa connect dengan sistem Akademik
                        // -------------------------------------------------------------------
                        //SqlCommand CmdSKS = new SqlCommand("SpVwSksSemester", con);
                        //CmdSKS.Transaction = trans;
                        //CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;

                        //CmdSKS.Parameters.AddWithValue("@npm", _NPM);
                        //CmdSKS.Parameters.AddWithValue("@semester", _semester);
                        //CmdSKS.ExecuteNonQuery();

                        // 2.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", _thn_angkatan);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", _prodi);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", _kelas);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", _semester);
                        CmdPeriodik.Parameters.AddWithValue("@npm", _NPM);
                        CmdPeriodik.ExecuteNonQuery();

                        //2.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", _NPM);
                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", _Kekurangan);
                        CmdPost.Parameters.AddWithValue("@angsuran", "2");
                        CmdPost.ExecuteNonQuery();

                        //clear Textbox SKS dan Checkbox
                        this.TbTotalSKS2.Text = "";

                        //commit transaction & close connection
                        trans.Commit();
                        trans.Dispose();
                        //CmdSKS.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", _Kekurangan);
                        this.LbPostSuccess2.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                        this.LbPostSuccess2.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        LbResultBayar3.Text = ex.Message.ToString();
                        LbResultBayar3.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            // ----------------------------------------------------------
        }











    }
}