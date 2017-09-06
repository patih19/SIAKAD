using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padu.account
{
    public class Mhs_account : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null)
            {
                Response.Redirect("~/Padu_login.aspx");
            }
            //base.OnInit(e);
        }
    }
}