using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class TU : System.Web.UI.MasterPage
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
            this.Session["Prodi"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("system");
            this.Session.Remove("level");
            this.Session.Remove("Prodi");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Log.aspx");
        }
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LbName.Text = this.Session["Prodi"].ToString();
        }
    }
}