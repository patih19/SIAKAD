using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Keuangan.admin
{
    //public partial class WebForm1 : System.Web.UI.Page
    public partial class WebForm8 : Keu_Admin_Class
    {
        mahasiswa mhs = new mahasiswa();

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

            this.Response.Redirect("~/keu-login.aspx");
        }
        // -------------- End Logout ----------------------------

        // -------- Viewstate variable ---------------
        public string _thn_angkatan
        {
            get
            {
                return this.ViewState["tahun_angkatan"].ToString();
            }
            set
            {
                this.ViewState["tahun_angkatan"] = (object)value;
            }
        }
        public string _prodi
        {
            get
            {
                return this.ViewState["program_studi"].ToString();
            }
            set
            {
                this.ViewState["program_studi"] = (object)value;
            }
        }
        public string _kelas
        {
            get
            {
                return this.ViewState["kelas"].ToString();
            }
            set
            {
                this.ViewState["kelas"] = (object)value;
            }
        }
        public string _gelombang
        {
            get
            {
                return this.ViewState["gelombang"].ToString();
            }
            set
            {
                this.ViewState["gelombang"] = (object)value;
            }
        }
        public string _semester
        {
            get
            {
                return this.ViewState["semester"].ToString();
            }
            set
            {
                this.ViewState["semester"] = (object)value;
            }
        }
        public decimal _biaya
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["biaya"].ToString());
            }
            set
            {
                this.ViewState["biaya"] = (object)value;
            }
        }
        public decimal _biayaSKS
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["biayaSKS"].ToString());
            }
            set
            {
                this.ViewState["biayaSKS"] = (object)value;
            }
        }


        //Viewstate Biaya ....
        public decimal _TotalBebanAwal
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalBebanAwal"].ToString());
            }
            set
            {
                this.ViewState["TotalBebanAwal"] = (object)value;
            }
        }
        public decimal _TotalSKS
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString());
            }
            set
            {
                this.ViewState["TotalSKS"] = (object)value;
            }
        }
        public decimal _TotalSkripsi
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalSkripsi"].ToString());
            }
            set
            {
                this.ViewState["TotalSkripsi"] = (object)value;
            }
        }
        public decimal _TotalSPP
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalSPP"].ToString());
            }
            set
            {
                this.ViewState["TotalSPP"] = (object)value;
            }
        }
        public decimal _TotalBOP
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalBOP"].ToString());
            }
            set
            {
                this.ViewState["TotalBOP"] = (object)value;
            }
        }
        public decimal _TotalKmhs
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalKmhs"].ToString());
            }
            set
            {
                this.ViewState["TotalKmhs"] = (object)value;
            }
        }
        public decimal _TotalLab
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalLab"].ToString());
            }
            set
            {
                this.ViewState["TotalLab"] = (object)value;
            }
        }
        public decimal _TotalBayar
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalBayar"].ToString());
            }
            set
            {
                this.ViewState["TotalBayar"] = (object)value;
            }
        }
        public decimal _TotalBayarAkhir
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalBayarAkhir"].ToString());
            }
            set
            {
                this.ViewState["TotalBayarAkhir"] = (object)value;
            }
        }

         public decimal _TotalWisuda
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["TotalWisuda"].ToString());
            }
            set
            {
                this.ViewState["TotalWisuda"] = (object)value;
            }
        }

         public decimal _TotalKKN
         {
             get
             {
                 return Convert.ToDecimal(this.ViewState["TotalKKN"].ToString());
             }
             set
             {
                 this.ViewState["TotalKKN"] = (object)value;
             }
         }
         public decimal _TotalKP
         {
             get
             {
                 return Convert.ToDecimal(this.ViewState["TotalKP"].ToString());
             }
             set
             {
                 this.ViewState["TotalKP"] = (object)value;
             }
         }
        // ------------ End Viewstate -----------------

        protected void Page_Load(object sender, EventArgs e)
        {
            // User identity
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            // page load for the first time
            if (!Page.IsPostBack) 
            {
                this.PanelContent.Enabled = false;
                this.PanelContent.Visible = false;

                // hide panel unpaid
                this.PanelUnpaid.Visible = false;
                this.PanelUnpaid.Enabled = false;

                // hide panel unpaid2
                this.PanelUnpaid2.Visible = false;
                this.PanelUnpaid2.Enabled = false;
                
                // hide panel paid
                this.PanelPaid.Enabled = false;
                this.PanelPaid.Visible = false;

                // hide panel paid2
                this.PanelPaid2.Enabled = false;
                this.PanelPaid2.Visible = false;

                //hide panel tagihan akhir
                this.PanelAkhir.Enabled = false;
                this.PanelAkhir.Visible = false;

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {

                    SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@npm", this.TBNPM2.Text);

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
                //disable btn view biaya
                this.BtnViewBiaya2.Enabled = false;
                //hide button
                BtnOk2.Visible = false;
                // hide SKS bayar
                PanelTblByrSKS2.Visible = false;
            }
            else // page is Postback
            {

            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            //enable panel
            this.PanelContent.Enabled = true;
            this.PanelContent.Visible = true;

            // clear result angsuran
            LbViewAngsuran2.Text = "";

            //clear result POsting Tagihan
            this.LbPostSuccess.Text = "";

            // hide Bayar SKS
            PanelTblByrSKS2.Visible = false;

            // ------ Firset Set all of viewstate values to zero ----------
            //Beban Awal
            _TotalBebanAwal = 0;

            //Beban Periodik
            _TotalSKS = 0;
            _TotalSPP = 0;
            _TotalBOP = 0;
            _TotalKmhs = 0;
            _TotalLab = 0;

            _TotalSkripsi = 0;

            //Beban Non Periodik
            _TotalWisuda = 0;
            _TotalKKN = 0;
            _TotalKP = 0;

            // Total Bayar
            _TotalBayarAkhir = 0;
            _TotalBayar = 0;


            mhs.ReadMahasiswa(this.TBNPM2.Text);

            _kelas = mhs.kelas;
            _prodi = mhs.Prodi;
            _thn_angkatan = mhs.thn_angkatan;

            LbNama2.Text = mhs.nama;
            LbNPM2.Text = this.TBNPM2.Text;
            LBKelas2.Text = mhs.kelas;
            LBTahun2.Text = mhs.thn_angkatan;
            LbProdi.Text = mhs.Prodi;


            string ConString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    con.Open();

                    // --- A. Fill GV Beban Awal ---
                    SqlCommand cmd = new SqlCommand("SELECT no, npm, beban_awal FROM keu_keu_mhs where npm=@npm", con);
                    cmd.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableAwal = new DataTable();
                    TableAwal.Columns.Add("NPM");
                    TableAwal.Columns.Add("Biaya");


                    using (SqlDataReader rdrAwal = cmd.ExecuteReader())
                    {
                        if (rdrAwal.HasRows)
                        {
                            while (rdrAwal.Read())
                            {
                                DataRow datarow = TableAwal.NewRow();

                                datarow["NPM"] = rdrAwal["npm"];
                                if (rdrAwal["beban_awal"] == DBNull.Value)
                                {
                                    BtnViewBiaya2.Enabled = false;
                                    con.Close();
                                    con.Dispose();

                                    //disable panel content
                                    this.PanelContent.Enabled = false;
                                    this.PanelContent.Visible = false;

                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tagihan Awal belum diinput');", true);
                                    return;
                                }
                                else
                                {
                                    datarow["Biaya"] = rdrAwal["beban_awal"];
                                    TableAwal.Rows.Add(datarow);
                                }
                            }

                            //Fill Gridview
                            this.GVBebanAwal2.DataSource = TableAwal;
                            this.GVBebanAwal2.DataBind();

                            //aktifkan btn view biaya
                            this.BtnViewBiaya2.Enabled = true;
                        }
                        else
                        {
                            //clear Gridview
                            TableAwal.Rows.Clear();
                            TableAwal.Clear();
                            GVBebanAwal2.DataSource = TableAwal;
                            GVBebanAwal2.DataBind();

                            //nonaktifkan btn view biaya
                            this.BtnViewBiaya2.Enabled = false;
                        }
                    }

                    //---- B. Fill GV Tagihan Total ---
                    SqlCommand cmdTotal = new SqlCommand("SpViewTagihanMhsPerSemester", con);
                    cmdTotal.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdTotal.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableTotal = new DataTable();
                    TableTotal.Columns.Add("Key");
                    TableTotal.Columns.Add("Semester");
                    TableTotal.Columns.Add("SPP");
                    TableTotal.Columns.Add("BOP");
                    TableTotal.Columns.Add("Kemahasiswaan");
                    TableTotal.Columns.Add("Lab.");


                    using (SqlDataReader rdrTotal = cmdTotal.ExecuteReader())
                    {
                        if (rdrTotal.HasRows)
                        {
                            while (rdrTotal.Read())
                            {
                                DataRow datarow = TableTotal.NewRow();

                                datarow["Key"] = rdrTotal["id_biaya"];
                                datarow["Semester"] = rdrTotal["semester"];
                                datarow["SPP"] = rdrTotal["SPP"];
                                datarow["BOP"] = rdrTotal["BOP"];
                                datarow["Kemahasiswaan"] = rdrTotal["kemhs"];
                                datarow["Lab."] = rdrTotal["lab"];
                                TableTotal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVTagihanTotal2.DataSource = TableTotal;
                            this.GVTagihanTotal2.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableTotal.Rows.Clear();
                            TableTotal.Clear();
                            GVTagihanTotal2.DataSource = TableTotal;
                            GVTagihanTotal2.DataBind();
                        }
                    }

                    // --- C. Fill Grid View SKS Mahasiswa ---
                    SqlCommand cmdSKS = new SqlCommand("SpVwBiayaSks", con);
                    cmdSKS.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdSKS.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableSKS = new DataTable();
                    TableSKS.Columns.Add("Semester");
                    TableSKS.Columns.Add("Biaya");

                    using (SqlDataReader rdrSKS = cmdSKS.ExecuteReader())
                    {
                        if (rdrSKS.HasRows)
                        {
                            while (rdrSKS.Read())
                            {
                                DataRow datarow = TableSKS.NewRow();

                                datarow["Semester"] = rdrSKS["semester"];
                                datarow["Biaya"] = rdrSKS["biaya"];
                                TableSKS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVSks2.DataSource = TableSKS;
                            this.GVSks2.DataBind();

                            TableSKS.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableSKS.Rows.Clear();
                            TableSKS.Clear();
                            GVSks2.DataSource = TableSKS;
                            GVSks2.DataBind();
                        }
                    }

                    // --- Fill Grid View SKRIPSI Mahasiswa ---
                    SqlCommand cmdSkripsi = new SqlCommand("SpVwBiayaSkripsi", con);
                    cmdSkripsi.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdSkripsi.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Biaya");

                    using (SqlDataReader rdrSkripsi = cmdSkripsi.ExecuteReader())
                    {
                        if (rdrSkripsi.HasRows)
                        {
                            while (rdrSkripsi.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["Semester"] = rdrSkripsi["semester"];
                                datarow["Biaya"] = rdrSkripsi["biaya"];
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


                    // --- D. Fill Grid View Posting Mahasiswa LUNAS (PAID) ---
                    SqlCommand cmdPaid = new SqlCommand("SpVwPosting5", con);
                    cmdPaid.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPaid.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TablePaid = new DataTable();
                    TablePaid.Columns.Add("Nomor");
                    TablePaid.Columns.Add("Semester");
                    TablePaid.Columns.Add("Angsuran");
                    TablePaid.Columns.Add("Cicilan");
                    TablePaid.Columns.Add("Tanggal Tagihan");
                    TablePaid.Columns.Add("Tanggal Bayar");
                    TablePaid.Columns.Add("Jumlah");
                    TablePaid.Columns.Add("Status Bayar");

                    using (SqlDataReader rdrPaid = cmdPaid.ExecuteReader())
                    {
                        if (rdrPaid.HasRows)
                        {
                            // show panel paid
                            this.PanelPaid.Enabled = true;
                            this.PanelPaid.Visible = true;

                            while (rdrPaid.Read())
                            {
                                DataRow datarow = TablePaid.NewRow();

                                datarow["Nomor"] = rdrPaid["billingNo"];
                                datarow["Semester"] = rdrPaid["billRef4"];
                                datarow["Angsuran"] = rdrPaid["billRef5"];
                                datarow["Cicilan"] = rdrPaid["cicilan"];
                                datarow["Tanggal Tagihan"] = rdrPaid["post_date"];
                                datarow["Tanggal Bayar"] = rdrPaid["pay_date"];
                                datarow["Jumlah"] = rdrPaid["amount_total"];
                                datarow["Status Bayar"] = rdrPaid["status"];
                                TablePaid.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVPost2.DataSource = TablePaid;
                            this.GVPost2.DataBind();

                            TablePaid.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TablePaid.Rows.Clear();
                            TablePaid.Clear();
                            GVPost2.DataSource = TablePaid;
                            GVPost2.DataBind();

                            // hide panel paid
                            this.PanelPaid.Enabled = false;
                            this.PanelPaid.Visible = false;
                        }
                    }

                    // ---- E. GV Belum Bayar (unpaid) Tagihan Periodik -----------------------//
                    SqlCommand CmdUp = new SqlCommand("SpVwPosting4", con);
                    CmdUp.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdUp.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableUp = new DataTable();
                    TableUp.Columns.Add("Nomor");
                    TableUp.Columns.Add("Semester");
                    TableUp.Columns.Add("Angsuran");
                    TableUp.Columns.Add("Cicilan");
                    TableUp.Columns.Add("Tanggal Tagihan");
                    //TableUp.Columns.Add("Tanggal Bayar");
                    TableUp.Columns.Add("Jumlah");
                    TableUp.Columns.Add("Status");

                    using (SqlDataReader rdrUp = CmdUp.ExecuteReader())
                    {
                        if (rdrUp.HasRows)
                        {
                            //Show panel
                            this.PanelUnpaid.Enabled = true;
                            this.PanelUnpaid.Visible = true;

                            while (rdrUp.Read())
                            {
                                DataRow datarow = TableUp.NewRow();

                                datarow["Nomor"] = rdrUp["billingNo"];
                                datarow["Semester"] = rdrUp["billRef4"];
                                datarow["Angsuran"] = rdrUp["billRef5"];
                                datarow["Cicilan"] = rdrUp["cicilan"];
                                datarow["Tanggal Tagihan"] = rdrUp["post_date"];
                                // datarow["Tanggal Bayar"] = rdr["pay_date"];
                                datarow["Jumlah"] = rdrUp["amount_total"];
                                datarow["Status"] = rdrUp["status"];
                                TableUp.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVUnpaid.DataSource = TableUp;
                            this.GVUnpaid.DataBind();

                            TableUp.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableUp.Rows.Clear();
                            TableUp.Clear();
                            GVUnpaid.DataSource = TableUp;
                            GVUnpaid.DataBind();

                            //hide panel
                            this.PanelUnpaid.Enabled = false;
                            this.PanelUnpaid.Visible = false;
                        }
                    }

                    //--F. Fill GV Tagihan Akhir Belum Bayar (unpaid) -----------------------//
                    SqlCommand cmdAkhirDelay = new SqlCommand("SpVwPostingAkhir1", con);
                    cmdAkhirDelay.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdAkhirDelay.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableAkhirDelay = new DataTable();
                    TableAkhirDelay.Columns.Add("Nomor");
                    TableAkhirDelay.Columns.Add("Semester");
                    TableAkhirDelay.Columns.Add("Angsuran");
                    TableAkhirDelay.Columns.Add("Cicilan");
                    TableAkhirDelay.Columns.Add("Tanggal Tagihan");
                    //TableAkhirLunas.Columns.Add("Tanggal Bayar");
                    TableAkhirDelay.Columns.Add("Jumlah");
                    TableAkhirDelay.Columns.Add("Status Bayar");

                    //con.Open();
                    using (SqlDataReader rdr = cmdAkhirDelay.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel
                            this.PanelUnpaid2.Visible = true;
                            this.PanelUnpaid2.Enabled = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableAkhirDelay.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                //datarow["Tanggal Bayar"] = rdr["biaya"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status Bayar"] = rdr["status"];
                                TableAkhirDelay.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVUnpaidAkhir.DataSource = TableAkhirDelay;
                            this.GVUnpaidAkhir.DataBind();

                            TableAkhirDelay.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableAkhirDelay.Rows.Clear();
                            TableAkhirDelay.Clear();
                            this.GVUnpaidAkhir.DataSource = TableAkhirDelay;
                            this.GVUnpaidAkhir.DataBind();

                            //hide panel
                            this.PanelUnpaid2.Visible = false;
                            this.PanelUnpaid2.Enabled = false;
                        }
                    }


                    //--F. Fill GV Tagihan Akhir Lunas Bayar (paid) -----------------------//
                    SqlCommand cmdAkhirLunas = new SqlCommand("SpVwPostingAkhir2", con);
                    cmdAkhirLunas.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdAkhirLunas.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableAkhirLunas = new DataTable();
                    TableAkhirLunas.Columns.Add("Nomor");
                    TableAkhirLunas.Columns.Add("Semester");
                    TableAkhirLunas.Columns.Add("Angsuran");
                    TableAkhirLunas.Columns.Add("Cicilan");
                    TableAkhirLunas.Columns.Add("Tanggal Tagihan");
                    TableAkhirLunas.Columns.Add("Tanggal Bayar");
                    TableAkhirLunas.Columns.Add("Jumlah");
                    TableAkhirLunas.Columns.Add("Status Bayar");

                    //con.Open();
                    using (SqlDataReader rdr = cmdAkhirLunas.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel
                            this.PanelPaid2.Enabled = true;
                            this.PanelPaid2.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableAkhirLunas.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                datarow["Tanggal Bayar"] = rdr["pay_date"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status Bayar"] = rdr["status"];
                                TableAkhirLunas.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVAkhirPaid.DataSource = TableAkhirLunas;
                            this.GVAkhirPaid.DataBind();

                            TableAkhirLunas.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableAkhirLunas.Rows.Clear();
                            TableAkhirLunas.Clear();
                            this.GVAkhirPaid.DataSource = TableAkhirLunas;
                            this.GVAkhirPaid.DataBind();

                            //hide panel
                            this.PanelPaid2.Enabled = false;
                            this.PanelPaid2.Visible = false;
                        }
                    }


                    //--G. Fill GV Tagihan Wisuda -----------------------//
                    SqlCommand CmdWisuda = new SqlCommand("SpVwTagihanWisudaMhs", con);
                    CmdWisuda.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdWisuda.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableWisuda = new DataTable();
                    TableWisuda.Columns.Add("Tahun");
                    TableWisuda.Columns.Add("Biaya");

                    //con.Open();
                    using (SqlDataReader rdr = CmdWisuda.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel tagihan akhir
                            this.PanelAkhir.Enabled = true;
                            this.PanelAkhir.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableWisuda.NewRow();

                                datarow["Tahun"] = rdr["tahun"];
                                datarow["Biaya"] = rdr["biaya"];
                                TableWisuda.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVWisuda.DataSource = TableWisuda;
                            this.GVWisuda.DataBind();

                            TableWisuda.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableWisuda.Rows.Clear();
                            TableWisuda.Clear();
                            this.GVWisuda.DataSource = TableWisuda;
                            this.GVWisuda.DataBind();

                        }
                    }

                    //--H. Fill GV Tagihan KKN -----------------------//
                    SqlCommand CmdKKN = new SqlCommand("SpVwTagihanKKNMhs", con);
                    CmdKKN.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdKKN.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableKKN = new DataTable();
                    TableKKN.Columns.Add("Tahun");
                    TableKKN.Columns.Add("Biaya");


                    //con.Open();
                    using (SqlDataReader rdr = CmdKKN.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel tagihan akhir
                            this.PanelAkhir.Enabled = true;
                            this.PanelAkhir.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKKN.NewRow();

                                datarow["Tahun"] = rdr["tahun"];
                                datarow["Biaya"] = rdr["biaya"];
                                TableKKN.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKkn.DataSource = TableKKN;
                            this.GVKkn.DataBind();

                            TableKKN.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableKKN.Rows.Clear();
                            TableKKN.Clear();
                            this.GVKkn.DataSource = TableKKN;
                            this.GVKkn.DataBind();
                        }
                    }

                    //--I. Fill GV Tagihan KP -----------------------//
                    SqlCommand CmdKP = new SqlCommand("SpVwTagihanKPMhs", con);
                    CmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdKP.Parameters.AddWithValue("@npm", TBNPM2.Text);

                    DataTable TableKP = new DataTable();
                    TableKP.Columns.Add("Tahun");
                    TableKP.Columns.Add("Biaya");


                    //con.Open();
                    using (SqlDataReader rdr = CmdKP.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //show panel tagihan akhir
                            this.PanelAkhir.Enabled = true;
                            this.PanelAkhir.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKP.NewRow();

                                datarow["Tahun"] = rdr["tahun"];
                                datarow["Biaya"] = rdr["biaya"];
                                TableKP.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKP.DataSource = TableKP;
                            this.GVKP.DataBind();

                            TableKP.Dispose();
                        }
                        else
                        {
                            this.PanelAkhir.Enabled = false;
                            this.PanelAkhir.Visible = false;

                            //clear Gridview
                            TableKP.Rows.Clear();
                            TableKP.Clear();
                            this.GVKP.DataSource = TableKP;
                            this.GVKP.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();

                    //enable panel
                    this.PanelContent.Enabled = false;
                    this.PanelContent.Visible = false;

                    ScriptManager.RegisterStartupScript((Control)this.BtnOk2, this.GetType(), "redirectMe", "alert('"+ ex.Message+"');", true);
                    return;
                }

            } // End Connection 


            //clear Gridview Biaya Angsuran SEMESTER
            DataTable Table2 = new DataTable();
            DataRow datarow2 = Table2.NewRow();
            Table2.Rows.Clear();
            Table2.Clear();
            GVAngsuran2.DataSource = Table2;
            GVAngsuran2.DataBind();

            // label kewajiban dalam format rupiah Rp
            decimal Kewajiban = _TotalBebanAwal + _TotalSKS + _TotalSPP + _TotalBOP + _TotalKmhs + _TotalLab + _TotalKKN + _TotalWisuda + _TotalKP + _TotalSkripsi;
            string FormattedString5 = string.Format
                (new System.Globalization.CultureInfo("id"), "{0:c}", Kewajiban);
            this.LbKewajiban2.Text = FormattedString5;

            //label terbayar dalam format rupiah Rp
            decimal Terbayar = _TotalBayar + _TotalBayarAkhir;
            string FormattedString6 = string.Format
                (new System.Globalization.CultureInfo("id"), "{0:c}", Terbayar);
            this.LbTerbayar.Text = FormattedString6; 

            //Label Kekurangan
            decimal Kekurangan = Kewajiban - Terbayar;
            string FormattedString7 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kekurangan);
            this.LbKurang.Text = FormattedString7;
        }

        // View List Biaya Angsuran Mahasiswa
        protected void BtnViewBiaya_Click(object sender, EventArgs e)
        {
            LbResultBayar2.Text = "";
            LbViewAngsuran2.Text = "";

            this._semester = this.DLSemster2.SelectedItem.Text;
            if (_thn_angkatan == "2013/2014" || _thn_angkatan == "2014/2015")
            {
                this.TbTotalSKS2.ReadOnly = false;
            }
            else
            {
                this.TbTotalSKS2.ReadOnly = true;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                this.LbResultBayar2.Text = "";

                SqlCommand cmd = new SqlCommand("SpVwAngsuranBiayaStudi", con);
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

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        //show panel angsuran
                        

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

                        //Show button
                        BtnOk2.Visible = true;
                        // Show Bayar SKS
                        PanelTblByrSKS2.Visible = true;

                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVAngsuran2.DataSource = Table;
                        GVAngsuran2.DataBind();

                        //hide button
                        BtnOk2.Visible = false;
                        //hide panel bayar SKS
                        PanelTblByrSKS2.Visible = false;

                        LbViewAngsuran2.Text = "Tabel angsuran Belum Dibuat ...";
                        LbViewAngsuran2.ForeColor = System.Drawing.Color.OrangeRed;
                    }
                }
            }
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
                                        CmdPeriodik.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                                        cmd.Transaction = trans;
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        cmd.Parameters.AddWithValue("@semester", _semester);
                                        cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.TbTotalSKS2.Text) * 40000);
                                        cmd.Parameters.AddWithValue("@sks", TbTotalSKS2.Text);
                                        cmd.Parameters.AddWithValue("@dispen", "no");
                                        cmd.ExecuteNonQuery();

                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.TBNPM2.Text);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        // biaya angsuran + separuh biaya SKS dalm 1 semester
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text))* 40000)/2);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        cmd.Dispose();
                                        CmdPeriodik.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        decimal BSks = ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 40000)/2;
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
                                        CmdPeriodik.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                                        cmd.Transaction = trans;
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        cmd.Parameters.AddWithValue("@semester", _semester);
                                        cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.TbTotalSKS2.Text) * 35000);
                                        cmd.Parameters.AddWithValue("@sks", TbTotalSKS2.Text);
                                        cmd.Parameters.AddWithValue("@dispen", "no");
                                        cmd.ExecuteNonQuery();


                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.TBNPM2.Text);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        // biaya angsuran + separuh harga SKS
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text)) * 35000)/2 );
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        cmd.Dispose();
                                        CmdPeriodik.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        decimal total = _biaya + ((Convert.ToDecimal(this.TbTotalSKS2.Text))* 35000)/2;

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
                                        CmdPeriodik.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
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

                                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.TBNPM2.Text);
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
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                    }
                                }
                            }


                        }

                        else if (angsuran == "2")
                        {
                            // Angsuran Kedua tahun 2013/14 dan 2014/15
                            // tetapi pastikan untuk angkatan 2013/14 dan 2014/15 sudah mengambil KRS

                            this.TbTotalSKS2.Text = "";
                            if (_thn_angkatan == "2013/2014" || _thn_angkatan == "2014/2015")
                            {

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

                                        CmdSKS.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
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
                                        CmdPeriodik.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        CmdPeriodik.ExecuteNonQuery();

                                        //3.) Get Setengah harga SKS
                                        SqlCommand CmdSks = new SqlCommand("SpSeparuhSKS", con);
                                        CmdSks.Transaction = trans;
                                        CmdSks.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdSks.Parameters.AddWithValue("@semester", _semester);
                                        CmdSks.Parameters.AddWithValue("@npm", this.TBNPM2.Text);

                                        decimal HalfSks = 0;
                                        using (SqlDataReader rdr = CmdSks.ExecuteReader())
                                        {
                                            if (rdr.HasRows)
                                            {
                                                while (rdr.Read())
                                                {
                                                    HalfSks = (Convert.ToDecimal(rdr["biaya"]))/2;
                                                }
                                            }
                                        }

                                        //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.TBNPM2.Text);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        // biaya angsuran ke dua + setengah harga SKS
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya + HalfSks);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        CmdSKS.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        string FormattedString9 = string.Format
                                              (new System.Globalization.CultureInfo("id"), "{0:c}", _biaya + HalfSks);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        //clear Textbox SKS dan Checkbox
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
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                        return;
                                    }
                                }
                            }
                            else
                            // angkatan 2012/2013 dan di bawahnya ==> Biaya saja
                            {
                                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(CS))
                                {
                                    //open connection and begin transaction
                                    con.Open();
                                    SqlTransaction trans = con.BeginTransaction();
                                    try
                                    {
                                        //Procedure Posting Tagihan ke Bank :

                                        //1.) Karena di angsuran ke 2 Check dulu SKS ==> memastikan sudah KRS belum ---
                                        // Thn 12/13 ke bawah, Sementara tidak dulu menunggu bisa connect dengan sistem Akademik
                                        // -------------------------------------------------------------------
                                        //SqlCommand CmdSKS = new SqlCommand("SpVwSksSemester", con);
                                        //CmdSKS.Transaction = trans;
                                        //CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;

                                        //CmdSKS.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
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
                                        CmdPeriodik.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                                        CmdPeriodik.ExecuteNonQuery();


                                        //2.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                                        CmdPost.Transaction = trans;
                                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.TBNPM2.Text);
                                        CmdPost.Parameters.AddWithValue("@semester", _semester);
                                        CmdPost.Parameters.AddWithValue("@total_tagihan", _biaya);
                                        CmdPost.Parameters.AddWithValue("@angsuran", angsuran);
                                        CmdPost.ExecuteNonQuery();

                                        //clear Textbox SKS dan Checkbox
                                        this.TbTotalSKS2.Text = "";
                                        CB.Checked = false;

                                        //commit transaction & close connection
                                        trans.Commit();
                                        trans.Dispose();
                                        //CmdSKS.Dispose();
                                        CmdPost.Dispose();
                                        con.Close();
                                        con.Dispose();

                                        string FormattedString9 = string.Format
                                              (new System.Globalization.CultureInfo("id"), "{0:c}", _biaya);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;
                                    }
                                    catch (Exception ex)
                                    {
                                        trans.Rollback();
                                        con.Close();
                                        con.Dispose();
                                        LbResultBayar2.Text = ex.Message.ToString();
                                        LbResultBayar2.ForeColor = System.Drawing.Color.Red;
                                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                        return;
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
                this._TotalBOP = TotalBOP;
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
        // Format Rupiah Tagihan Awal
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
        // Format Rupiah Biaya SKS Mahasiswa
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

        int TotalBayar = 0;
        protected void GVPost2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[6].Text);
                TotalBayar += jumlah;
                this._TotalBayar = TotalBayar;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[6].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[6].Text = TotalBayar.ToString();

                int JumlahTerbayar = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTerbayar);
                e.Row.Cells[6].Text = FormattedString1;
            }
        }

        // Fotmat Rupiah Gridview Unpaid
        protected void GVUnpaid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Biaya = Convert.ToInt32(e.Row.Cells[7].Text);
                string FormattedString = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[7].Text = FormattedString;
            }
        }

        protected void BtnOff_Click(object sender, EventArgs e)
        {
            // hitung checkbox selected
            int cnt = 0;
            for (int i = 0; i < GVUnpaid.Rows.Count; i++)
            {
                CheckBox CB = (CheckBox)GVUnpaid.Rows[i].FindControl("CbSelect");
                if (CB.Checked == true)
                {
                    cnt += 1;
                }
            }
            // checkbox selected
            if (cnt == 0)
            {
                //client message belum pilih check list.....
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Data ...');", true);
                //ScriptManager.RegisterStartupScript((Control)this.BtnEditSKS, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
                return;
            }
            else
            {
                for (int i = 0; i < GVUnpaid.Rows.Count; i++)
                {
                    CheckBox CB = (CheckBox)GVUnpaid.Rows[i].FindControl("CBSelect");
                    if (CB.Checked == true)
                    {
                        //Get Billing Number
                        string BillNo = GVUnpaid.Rows[i].Cells[1].Text;
                        //Get NPM
                        // ...........

                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            //open connection and begin transaction
                            con.Open();
                            SqlTransaction trans = con.BeginTransaction();
                            try
                            {
                                SqlCommand CmdOff = new SqlCommand("SpUpdateToOff", con);
                                CmdOff.Transaction = trans;
                                CmdOff.CommandType = System.Data.CommandType.StoredProcedure;

                                CmdOff.Parameters.AddWithValue("@BillNo",BillNo);
                                CmdOff.Parameters.AddWithValue("@payeeid", LbNPM2.Text);

                                CmdOff.ExecuteNonQuery();

                                trans.Commit();
                                trans.Dispose();

                                CmdOff.Dispose();

                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                trans.Dispose();
                                con.Close();
                                con.Dispose();
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                            }
                        }
                    }
                }
            }
            //reload page / by kliking view button
            BtnView_Click(this, null);

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);
        }

        // Biaya Akhir Terbayar
        int TotalAkhir = 0;
        protected void GVAkhirPaid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[6].Text);
                TotalAkhir += jumlah;
                this._TotalBayarAkhir = TotalAkhir;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[6].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah :";
                e.Row.Cells[6].Text = TotalAkhir.ToString();

                int JumlahAkhir = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahAkhir);
                e.Row.Cells[6].Text = FormattedString1;
            }
        }

        //Biaya Wisuda
        int TotalWisuda = 0;
        protected void GVAkhir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalWisuda += jumlah;
                this._TotalWisuda = TotalWisuda;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        //Biaya KKN
        int TotalKKN = 0;
        protected void GVKkn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalKKN += jumlah;
                this._TotalKKN = TotalKKN;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        //Biaya KP
        int TotalKP = 0;
        protected void GVKP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int jumlah = Convert.ToInt32(e.Row.Cells[1].Text);
                TotalKP += jumlah;
                this._TotalKP = TotalKP;
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", jumlah);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        protected void GVUnpaidAkhir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Biaya = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[6].Text = FormattedString;
            }
        }

        protected void DelUnpaid_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            string billnumber = this.GVUnpaid.Rows[index].Cells[2].Text;

            //Del Unpaid
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //open connection and begin transaction
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {   
                    // Delete Tagihan Unpaid pada sistem BPD dan Untidar
                    SqlCommand CmdDelUnpaid = new SqlCommand("SpDelUnpaid", con);
                    CmdDelUnpaid.Transaction = trans;
                    CmdDelUnpaid.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDelUnpaid.Parameters.AddWithValue("@billingNo",billnumber);
                    CmdDelUnpaid.ExecuteNonQuery();

                    trans.Commit();
                    trans.Dispose();

                    CmdDelUnpaid.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Hapus Berhasil ...');", true);
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

        protected void BtnOffline_Click(object sender, EventArgs e)
        {
            //// hitung checkbox selected
            //int cnt = 0;
            //for (int i = 0; i < GVUnpaid.Rows.Count; i++)
            //{
            //    CheckBox CB = (CheckBox)GVUnpaid.Rows[i].FindControl("CbSelect");
            //    if (CB.Checked == true)
            //    {
            //        cnt += 1;
            //    }
            //}
            //// checkbox selected
            //if (cnt == 0)
            //{
            //    //client message belum pilih check list.....
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Data ...');", true);
            //    //ScriptManager.RegisterStartupScript((Control)this.BtnEditSKS, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
            //    return;
            //}
            //else
            //{
            //    for (int i = 0; i < GVUnpaid.Rows.Count; i++)
            //    {
            //        CheckBox CB = (CheckBox)GVUnpaid.Rows[i].FindControl("CBSelect");
            //        if (CB.Checked == true)
            //        {
            //            //Get Billing Number
            //            string BillNo = GVUnpaid.Rows[i].Cells[1].Text;
            //            //Get NPM
            //            // ...........

            //            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //            using (SqlConnection con = new SqlConnection(CS))
            //            {
            //                //open connection and begin transaction
            //                con.Open();
            //                SqlTransaction trans = con.BeginTransaction();
            //                try
            //                {
            //                    SqlCommand CmdOff = new SqlCommand("SpUpdateToOff", con);
            //                    CmdOff.Transaction = trans;
            //                    CmdOff.CommandType = System.Data.CommandType.StoredProcedure;

            //                    CmdOff.Parameters.AddWithValue("@BillNo", BillNo);
            //                    CmdOff.Parameters.AddWithValue("@payeeid", LbNPM2.Text);

            //                    CmdOff.ExecuteNonQuery();

            //                    trans.Commit();
            //                    trans.Dispose();

            //                    CmdOff.Dispose();

            //                }
            //                catch (Exception ex)
            //                {
            //                    trans.Rollback();
            //                    trans.Dispose();
            //                    con.Close();
            //                    con.Dispose();
            //                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
            //                }
            //            }
            //        }
            //    }
            //}
            ////reload page / by kliking view button
            //BtnView_Click(this, null);

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);


            // ---------------------------------------------
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            //Get Billing Number
            string BillNumber = GVUnpaid.Rows[index].Cells[2].Text;
            //Get NPM
            // ...........

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //open connection and begin transaction
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand CmdOff = new SqlCommand("SpUpdateToOff", con);
                    CmdOff.Transaction = trans;
                    CmdOff.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdOff.Parameters.AddWithValue("@BillNo", BillNumber);
                    CmdOff.Parameters.AddWithValue("@payeeid", LbNPM2.Text);

                    CmdOff.ExecuteNonQuery();

                    trans.Commit();
                    trans.Dispose();

                    CmdOff.Dispose();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    con.Close();
                    con.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }






    }
}