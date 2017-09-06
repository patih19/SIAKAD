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
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {
               
            }
            else
            {
                Response.Redirect("~/account/Keuangan.aspx");
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
                if (this.Session["Name"] != null && this.Session["password"] != null)
                {
                    Response.Redirect("~/account/home.aspx");
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
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {
                string usr = this.TextBoxUsername.Text.Trim();
                string password = this.TextBoxPasswd.Text.Trim();

                if (!Autentication(usr,password))
                {
                    return;
                }
                else
                {
                    this.Session["Name"] = this.TextBoxUsername.Text;
                    this.Session["password"] = this.TextBoxPasswd.Text;
                    Response.Redirect("~/account/Keuangan.aspx");
                }
            }
            else
            {
                Response.Redirect("~/account/Keuangan.aspx");
            }
        }
    }
}