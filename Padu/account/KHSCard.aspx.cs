using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Padu.account
{
    //public partial class KHSCard : System.Web.UI.Page
    public partial class KHSCard : Mhs_account
    {
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
                //Page lastpage = (Page)Context.Handler;
                //if (lastpage is Padu.account.WebForm5)
                //{
                    try
                    {
                        ////1. ----- set keterangan mahasiswa from last page
                        //this.LbNpm.Text = ((Padu.account.WebForm5)lastpage).NPM;
                        //this.LbJenisKelas.Text = ((Padu.account.WebForm5)lastpage).JenisKelas;
                        //this.LbTahun.Text = ((Padu.account.WebForm5)lastpage).Tahun;
                        //this.LbSemester.Text = ((Padu.account.WebForm5)lastpage).Semester;
                        //this.LbProdi.Text = ((Padu.account.WebForm5)lastpage).Prodi;
                        //this.LbNama.Text = ((Padu.account.WebForm5)lastpage).Name;
                        ////this.LbProdi.Text = ((Padu.account.WebForm5)lastpage).Id_Prodi;

                        //2. ---------- Gridview SKS ------------------
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();

                            //Read Data Mahasiswa
                            SqlCommand CmdDtMhs = new SqlCommand("SELECT bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.id_prog_study,bak_prog_study.prog_study, bak_mahasiswa.thn_angkatan,bak_mahasiswa.kelas "+
                                                                    "FROM bak_mahasiswa INNER JOIN "+
                                                                    "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                                                                    "WHERE (bak_mahasiswa.npm=@npm)", con);
                            CmdDtMhs.CommandType = System.Data.CommandType.Text;

                            CmdDtMhs.Parameters.AddWithValue("@npm", Decrypt(HttpUtility.UrlDecode(Request.QueryString["Npm"])));

                            using (SqlDataReader reader = CmdDtMhs.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        this.LbNpm.Text = reader["npm"].ToString();
                                        this.LbJenisKelas.Text = reader["kelas"].ToString();
                                        this.LbTahun.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Tahun"]));
                                        this.LbSemester.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Semester"]));
                                        this.LbProdi.Text = reader["prog_study"].ToString();
                                        this.LbNama.Text = reader["nama"].ToString();
                                    }
                                }
                            }


                            // --------------------- Fill Gridview  ------------------------
                            SqlCommand CmdListKRS = new SqlCommand("SpGetKHS", con);
                            CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;
                           
                            CmdListKRS.Parameters.AddWithValue("@npm", Decrypt(HttpUtility.UrlDecode(Request.QueryString["Npm"])));
                            CmdListKRS.Parameters.AddWithValue("@semester", Decrypt(HttpUtility.UrlDecode(Request.QueryString["Semester"])));

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
                                    this.GVMakul.DataSource = TableKRS;
                                    this.GVMakul.DataBind();

                                    //Set Label
                                    this.LBSks.Text = _TotalSKS.ToString();
                                    decimal IPS = _TotalNilai / _TotalSKS;
                                    this.LbIPS.Text = String.Format("{0:0.##}", IPS);

                                }
                                else
                                {
                                    //clear Gridview
                                    TableKRS.Rows.Clear();
                                    TableKRS.Clear();
                                    GVMakul.DataSource = TableKRS;
                                    GVMakul.DataBind();
                                }
                            }
                        }

                       Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
                    }
                    catch (Exception ex)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                //}
            }
        }

        // hitung Jumlah SKS dan IP Semester
        int TotalSKS = 0;
        decimal TotalNilai = 0;
        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
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

        //Decrypting the QueryString Parameter Values
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "##UniversitasTidar123##";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}