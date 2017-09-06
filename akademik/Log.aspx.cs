using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace akademik
{
    public partial class Log : System.Web.UI.Page
    {
        //prevent back after login on page init
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null)
            {
                // ---- avoid back after logout -----
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.AppendCacheExtension("no-cache");
                Response.Expires = 0;

                Page.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);

                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                // --- End aviod back after logout -------
            }
            else if (this.Session["system"].ToString() == "siamik" && (this.Session["level"].ToString() == "admin" || this.Session["level"].ToString() == "super" || this.Session["level"].ToString() == "staff"))
            {
                // kehalaman smuntidar admin
                Response.Redirect("~/am/home.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- avoid back after logout -----
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-cache");
            Response.Expires = 0;

            Page.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);

            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            // --- End aviod back after logout -------

            if (Page.IsPostBack) // user must login first
            {
                if (this.Session["Name"] == null && this.Session["system"] == null)
                {
                    return;
                }
                else if (this.Session["system"].ToString() == "siamik" && (this.Session["level"].ToString() == "admin" || this.Session["level"].ToString() == "super" || this.Session["level"].ToString() == "staff"))
                {
                    // kehalaman smuntidar admin
                    Response.Redirect("~/am/home.aspx");
                }
            }
        }

        string DecrpPasswd;
        string Pengguna;
        private bool Autentication(string user, string passwd)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand CmdOldPass = new SqlCommand("SELECT password FROM system_bak_user WHERE username = @username", con);
                    CmdOldPass.CommandType = System.Data.CommandType.Text;
                    CmdOldPass.Parameters.AddWithValue("@username", user);

                    con.Open();
                    using (SqlDataReader rdr = CmdOldPass.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Pengguna = rdr["password"].ToString();
                                DecrpPasswd = Decrypt(Pengguna);
                            }
                        }
                        else
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Invalid Username Or Password');", true);
                        }
                    }                    

                    if (DecrpPasswd == passwd)
                    {

                        SqlCommand CmdSmLogin = new SqlCommand("SpBakLogin", con);
                        CmdSmLogin.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdSmLogin.Parameters.AddWithValue("@user", user);
                        CmdSmLogin.Parameters.AddWithValue("@passwd", Pengguna);

                        CmdSmLogin.ExecuteNonQuery();

                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert(' Login Gagal, " + ex.Message.ToString() + "');", true);
                return false;
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "PUSKOMINFOUNTIDAR2016123456,.?";
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

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "PUSKOMINFOUNTIDAR2016123456,.?";
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

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (this.TbUser.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Username');", true);
                return;
            }

            if (this.TBPasswd.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Password');", true);
                return;
            }

            if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null)
            {
                if (!Autentication(this.TbUser.Text, this.TBPasswd.Text))
                {
                    return;
                }
                else
                {
                    this.Session["Name"] = this.TbUser.Text;
                    this.Session["system"] = "siamik";

                    // ---------------------- Lihat Level ---------------------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        SqlCommand CmdLevel = new SqlCommand("SpBakLevel", con);
                        CmdLevel.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdLevel.Parameters.AddWithValue("@user", this.TbUser.Text);
                        CmdLevel.Parameters.AddWithValue("@passwd", Pengguna);

                        using (SqlDataReader rdr = CmdLevel.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                        this.Session["level"] = rdr["tingkatan"].ToString();
                                        Response.Redirect("~/am/home.aspx");
                                }
                            }
                            else
                            {
                                // -- Invalid Level
                                // -- tidak mungkin karena usernya ada, kecualai level belum diisi
                                return;
                            }
                        }
                    }
                }
            }
            else if (this.Session["Name"] != null && this.Session["system"].ToString() == "siamik" && (this.Session["level"].ToString() == "admin" || this.Session["level"].ToString() == "super" || this.Session["tingkatan"].ToString() == "staff"))
            {
                Response.Redirect("~/am/home.aspx");
            }
            else
            {
                Response.Write("Invalid User Login ...");
            }
        }
    }
}