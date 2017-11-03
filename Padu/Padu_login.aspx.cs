using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

namespace Padu
{

    public partial class Padu_login : System.Web.UI.Page
    {
        // prevent back after login on page init
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null && this.Session["jenjang"] == null)
            {
               
            }
            else if (this.Session["Name"] != null && this.Session["password"] != null && ((this.Session["jenjang"].ToString().Trim() == "S1") || (this.Session["jenjang"].ToString().Trim() == "D3")))
            {
                Response.Redirect("~/account/Keuangan.aspx");
            }
            else if (this.Session["Name"] != null && this.Session["password"] != null && (this.Session["jenjang"].ToString().Trim() == "S2"))
            {
                Response.Redirect("~/pasca/Keuangan.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; 

            // ---- avoid back after logout -----
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();  

            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            // --- End aviod back after logout -------

            if (Page.IsPostBack) // user must login first
            {
                if (this.Session["Name"] != null && this.Session["password"] != null && ((this.Session["jenjang"].ToString().Trim() == "S1") || (this.Session["jenjang"].ToString().Trim() == "D3")))
                {
                    Response.Redirect("~/account/home.aspx");
                }
                else if (this.Session["Name"] != null && this.Session["password"] != null && (this.Session["jenjang"].ToString().Trim() == "S2"))
                {
                    Response.Redirect("~/pasca/Keuangan.aspx");
                }
                else
                {
                    return;
                }
            }
        }

        private bool Autentication(string username, string password)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmdLogin = new SqlCommand("SpMhsLogin", con);
                    cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdLogin.Parameters.AddWithValue("@user", username);
                    cmdLogin.Parameters.AddWithValue("@passwd", password);

                    con.Open();
                    cmdLogin.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                this.LoginResult.Text = ex.Message.ToString();
                LoginResult.ForeColor = System.Drawing.Color.Red;
                //ex.Message.ToString();
                return false;
            }
        }

        // login button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null && this.Session["jenjang"] == null)
            {
                string usr = this.TextBoxUsername.Text.Trim();
                string password = this.TextBoxPasswd.Text.Trim();

                if (!Autentication(usr,password))
                {
                    return;
                }
                else
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdLogin = new SqlCommand(""+
                            "SELECT npm, jenjang FROM dbo.bak_mahasiswa INNER JOIN bak_prog_study ON  dbo.bak_prog_study.id_prog_study = dbo.bak_mahasiswa.id_prog_study "+
                            "WHERE npm = @npm "+
                            "", con);
                        cmdLogin.CommandType = System.Data.CommandType.Text;
                        cmdLogin.Parameters.AddWithValue("@npm", usr);

                        using (SqlDataReader rdr = cmdLogin.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    this.Session["jenjang"] = rdr["jenjang"].ToString();
                                }
                            }
                        }
                    }

                    this.Session["Name"] = this.TextBoxUsername.Text;
                    this.Session["password"] = this.TextBoxPasswd.Text;

                    if (this.Session["jenjang"].ToString().Trim() == "S1" || this.Session["jenjang"].ToString().Trim() == "D3")
                    {
                        Response.Redirect("~/account/Keuangan.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/pasca/Keuangan.aspx");
                    }
                        
                }
            }
            else if (this.Session["Name"] != null && this.Session["password"] != null && ((this.Session["jenjang"].ToString().Trim()== "S1") ||(this.Session["jenjang"].ToString().Trim() == "D3")))
            {
                Response.Redirect("~/account/Keuangan.aspx");
            } else
            {
                Response.Redirect("~/pasca/Keuangan.aspx");
            }
        }
    }
}