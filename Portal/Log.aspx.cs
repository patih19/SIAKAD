using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Portal
{
    public partial class Log : System.Web.UI.Page
    {
        //prevent back after login on page init
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null || this.Session["jenjang"] == null)
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
            else if (this.Session["system"].ToString() == "portalsiamik" && this.Session["level"] != null && this.Session["Name"] != null && this.Session["jenjang"] != null)
            {
                // kehalaman smuntidar staff
                Response.Redirect("~/home.aspx");
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
                if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null || this.Session["jenjang"] == null)
                {
                    return;
                }
                else if (this.Session["system"].ToString() == "portalsiamik" && this.Session["level"] != null && this.Session["Name"] != null && this.Session["jenjang"] != null)
                {
                    // kehalaman home
                    Response.Redirect("~/home.aspx");
                }
            }
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

            if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null || this.Session["jenjang"] == null)
            {
                if (!Autentication(this.TbUser.Text, this.TBPasswd.Text))
                {
                    return;
                }
                else
                {
                    this.Session["Name"] = this.TbUser.Text;
                    this.Session["system"] = "portalsiamik";

                    // ---------------------- Lihat Level ---------------------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        SqlCommand CmdLevel = new SqlCommand("SpTuLevel", con);
                        CmdLevel.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdLevel.Parameters.AddWithValue("@user", this.TbUser.Text);
                        CmdLevel.Parameters.AddWithValue("@passwd", this.TBPasswd.Text);

                        using (SqlDataReader rdr = CmdLevel.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["tingkatan"] != null && rdr["kelompok"] != null)
                                    {
                                        // id prodi
                                        this.Session["level"] = rdr["tingkatan"].ToString();
                                        //prodi
                                        this.Session["Prodi"] = rdr["kelompok"].ToString();
                                        //jenjang
                                        this.Session["jenjang"] = rdr["jenjang"].ToString();

                                        Response.Redirect("~/home.aspx");
                                    }
                                }
                            }
                            else
                            {
                                // -- Invalid Level
                                // -- tidak mungkin karena usernya ada, kecualai level belum diisi
                                Response.Write("Invalid User Level ...");
                                return;
                            }
                        }
                    }
                }
            }
            else if (this.Session["Name"] != null && this.Session["system"].ToString() == "portalsiamik" && this.Session["level"] != null && this.Session["jenjang"] != null)
            {
                Response.Redirect("~/home.aspx");
            }
            else
            {
                Response.Write("Invalid User Login ...");
            }
        }

        private bool Autentication(string user, string passwd)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand CmdSmLogin = new SqlCommand("SpTuLogin", con);
                    CmdSmLogin.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdSmLogin.Parameters.AddWithValue("@user", user);
                    CmdSmLogin.Parameters.AddWithValue("@passwd", passwd);

                    con.Open();
                    CmdSmLogin.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return false;
            }
        }
    }
}