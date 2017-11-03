using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Padu.pasca
{
    public partial class Pasca : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(keluar_ServerClick);

            base.OnInit(e);
            logout.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void keluar_ServerClick(object sender, EventArgs e)
        {
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session["jenjang"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session["jenjang"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
    }
}