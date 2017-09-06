using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace akademik.am
{
    public partial class admin : System.Web.UI.MasterPage
    {
        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            // Old Code ... Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["system"] = (object)null;
            this.Session["level"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("system");
            this.Session.Remove("level");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Log.aspx");
        }
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LbName.Text = this.Session["Name"].ToString();

            // ------ PENGATURAN MENU-MENU UNTUK ADMIN DAN STAFF -------- //
            if (!Page.IsPostBack)
            {
                // MENU UNTUK STAFF
                if (this.Session["level"].ToString() == "staff")
                {
                    Kalender.Visible = false;
                    Nilai.Visible = false;
                    Ruang.Visible = false;

                }
                else
                // MENU UNTUK ADMIN
                {
                    Kalender.Visible = true;
                    Nilai.Visible = true;
                    Ruang.Visible = true;
                }
            }
        }
    }
}