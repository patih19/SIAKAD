using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padu.pasca
{
    public class MhsPasca : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null && this.Session["password"] == null && this.Session["jenjang"] == null)
            {
                Response.Redirect("~/Padu_login.aspx");
            }
            else if (this.Session["Name"] == null && this.Session["password"] == null && (this.Session["jenjang"].ToString().Trim() != "S2"))
            {
                Response.Redirect("~/Padu_login.aspx");
            }
            else if (this.Session["Name"] != null && this.Session["password"] != null && ((this.Session["jenjang"].ToString().Trim() == "S1")|| (this.Session["jenjang"].ToString().Trim() == "D3")))
            {
                Response.Redirect("~/account/keuangan.aspx");
            }
        }
    }
}