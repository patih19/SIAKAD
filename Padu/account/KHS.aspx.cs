using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Padu.account
{
    //public partial class WebForm5 : System.Web.UI.Page
    public partial class WebForm5 : Mhs_account
    {
        public string NPM
        {
            get { return this.LbNpm.Text; }
        }
        public string JenisKelas
        {
            get { return this.LbKelas.Text; }
        }
        public string Tahun
        {
            get { return this.DLTahun.SelectedItem.Text; }
        }
        public string Semester
        {
            get { return this.DLSemester.SelectedItem.Text; }
        }
        public string Name
        {
            get { return this.LbNama.Text; }
        }
        public string Id_Prodi
        {
            get { return this.LbKdProdi.Text; }
        }
        public string Prodi
        {
            get { return this.LbProdi.Text; }
        }
        //public string Angkatan
        //{
        //    get { return this.LbThnAngkatan.Text; }
        //}

        //instance object mahasiswa 
        Mhs mhs = new Mhs();

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


        //View State KHS
        public decimal _TotalSKS
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString()); }
            set
            { this.ViewState["TotalSKS"] = (object)value; }
        }
        public decimal _TotalNilai
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalNilai"].ToString()); }
            set
            { this.ViewState["TotalNilai"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMhs.Visible = false;
                this.PanelListKRS.Visible = false;

                //read Mahasiswa
                try
                {
                    mhs.ReadMahasiswa(this.Session["Name"].ToString());

                    LbNpm.Text = this.Session["Name"].ToString();
                    LbNama.Text = mhs.nama.ToString();
                    LbKelas.Text = mhs.kelas.ToString();
                    LbKdProdi.Text = mhs.id_prodi.ToString();
                    LbProdi.Text = mhs.Prodi.ToString();
                }
                catch (Exception)
                {
                    LbNama.Text = "";
                    LbKelas.Text = "";
                    LbProdi.Text = "";
                    LbNpm.Text = "";
                    LbKdProdi.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                    return;
                }
            }
        }

        protected void DLSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // -- Cek Masa INPUT NILAI
                    // -- Tidak Diperbolehkan Pada Saat Masa Input NILAI --
                    SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                    CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                    CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "Nilai");
                    CmdCekMasa.Parameters.AddWithValue("@jenjang", this.Session["jenjang"].ToString());

                    SqlParameter Status = new SqlParameter();
                    Status.ParameterName = "@output";
                    Status.SqlDbType = System.Data.SqlDbType.VarChar;
                    Status.Size = 20;
                    Status.Direction = System.Data.ParameterDirection.Output;
                    CmdCekMasa.Parameters.Add(Status);

                    CmdCekMasa.ExecuteNonQuery();

                    if (Status.Value.ToString() == "IN")
                    {
                        con.Close();
                        con.Dispose();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Masa input nilai sedang berlangsung');", true);
                        return;
                    }

                    // --------------------- Fill Gridview  ------------------------
                    SqlCommand CmdListKRS = new SqlCommand("SpGetKHS", con);
                    CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;


                    CmdListKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                    DataTable TableKRS = new DataTable();
                    TableKRS.Columns.Add("Kode");
                    TableKRS.Columns.Add("Mata Kuliah");
                    TableKRS.Columns.Add("SKS");
                    TableKRS.Columns.Add("Nilai");
                    TableKRS.Columns.Add("Jumlah");

                    using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelListKRS.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKRS.NewRow();
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                if (rdr["Nilai"] == DBNull.Value)
                                {
                                    //datarow["Nilai"] = "";
                                }
                                else
                                {
                                    datarow["Nilai"] = rdr["nilai"];
                                }
                                datarow["Jumlah"] = rdr["jumlah"];

                                TableKRS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKHS.DataSource = TableKRS;
                            this.GVKHS.DataBind();

                            //Set Label
                            this.LBSks.Text = _TotalSKS.ToString();
                            decimal IPS = _TotalNilai / _TotalSKS;
                            this.LbIPS.Text = String.Format("{0:0.##}", IPS);

                        }
                        else
                        {
                            this.PanelListKRS.Visible = false;

                            //clear Gridview
                            TableKRS.Rows.Clear();
                            TableKRS.Clear();
                            GVKHS.DataSource = TableKRS;
                            GVKHS.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data tidak ditemukan');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        // hitung Jumlah SKS dan IP Semester
        int TotalSKS = 0;
        decimal TotalNilai = 0;
        protected void GVKHS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[3].Text);
                TotalSKS += SKS;
                this._TotalSKS = TotalSKS;

                if (e.Row.Cells[5].Text != "&nbsp;")
                {

                    decimal Nilai = Convert.ToDecimal(e.Row.Cells[5].Text);
                    TotalNilai += Nilai;
                    this._TotalNilai = TotalNilai;
                }
                else
                {
                    decimal Nilai = 0;
                    TotalNilai += Nilai;
                    this._TotalNilai = TotalNilai;
                }
            }
        }

        protected void BtnDwnKHS_Click1(object sender, EventArgs e)
        {
            //Server.Transfer("~/account/KHSCard.aspx");
            //Response.Redirect("~/account/KHSCard.aspx?Npm="+this.NPM.ToString()+"&Semester="+this.Tahun+this.Semester);


            string npm = HttpUtility.UrlEncode(Encrypt(this.NPM.ToString().Trim()));
            string semester = HttpUtility.UrlEncode(Encrypt(this.Tahun.ToString().Trim() + this.Semester.ToString().Trim())); ;
            string tahun = HttpUtility.UrlEncode(Encrypt(this.DLTahun.SelectedItem.Text.Trim())); ;

            //string url = "KHSCard.aspx?Npm={0}" + npm + "&Semester={1}" + semester + "&Tahun={2}" + tahun;

            string one = string.Format("KHSCard.aspx?Npm={0}", npm);
            string two = string.Format("&Semester={0}", semester);
            string three = string.Format("&Tahun={0}", tahun);
            string url = one + two + three;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("','_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());


            //string url = "KHSCard.aspx?Npm=" + this.NPM.ToString() + "&Semester=" + this.Tahun + this.Semester + "&Tahun=" + this.DLTahun.SelectedItem.Text;
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("var rediredtWindow = window.open('");
            //sb.Append(url);
            //sb.Append("','_blank');");
            //sb.Append("rediredtWindow.location;");
            //sb.Append("</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());


            //var popUp = window.open('/popup-page.php', windowName, 'width=1000, height=700, left=24, top=24, scrollbars, resizable');
            //if (popUp == null || typeof(popUp)=='undefined') { 	
            //    alert('Please disable your pop-up blocker and click the "Open" link again.'); 
            //} 
            //else { 	
            //    popUp.focus();
            //}


            ////window.open('KHSCard.aspx?Npm=1110501002&Semester=20151&Tahun=2015','_blank',true);
            //string strJavascript="<script>window.open('KHSCard.aspx?Npm=1110501002&Semester=20151&Tahun=2015','_blank',true);</script>";
            //Response.Write(strJavascript);
        }

        // Encrypting the QueryString Parameter Values
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "##UniversitasTidar123##";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

    }
}