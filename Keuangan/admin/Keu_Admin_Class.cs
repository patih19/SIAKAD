using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Keuangan.admin
{
    public class Keu_Admin_Class : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {
                Response.Redirect("~/keu-login.aspx");
            }
            //base.OnInit(e);
        }
    }
}