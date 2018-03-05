using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal
{
    public class Tu : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null || this.Session["system"] == null || this.Session["level"] == null || this.Session["Prodi"] == null || this.Session["jenjang"] == null)
            {
                Response.Redirect("~/Log.aspx");
            }
        }
    }
}