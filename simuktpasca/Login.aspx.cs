using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simuktpasca
{
    public partial class Login : System.Web.UI.Page
    {
        // prevent back after login on page init
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {

            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    Response.Redirect("~/Home.aspx");
                }
                else
                {
                    return;
                }
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {
                string usr = this.TbUsername.Text.Trim();
                string password = this.TbPassword.Text.Trim();

                if (!Autentication(usr, password))
                {
                    return;
                }
                else
                {
                    this.Session["Name"] = usr;
                    this.Session["password"] = password;
                    Response.Redirect("~/Home.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        private bool Autentication(string user, string passwd)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand CmdOldPass = new SqlCommand("SELECT username,password FROM system_users WHERE username = @username AND password=@password", con);
                    CmdOldPass.CommandType = System.Data.CommandType.Text;
                    CmdOldPass.Parameters.AddWithValue("@username", user);
                    CmdOldPass.Parameters.AddWithValue("@password", passwd);

                    con.Open();
                    using (SqlDataReader rdr = CmdOldPass.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert(' Login Gagal, " + ex.Message.ToString() + "');", true);
                return false;
            }
        }
    }
}