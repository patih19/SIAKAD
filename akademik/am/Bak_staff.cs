using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace akademik.am
{

    // ---------------- PAGE UNTUK ADMIN DAN STAFF -------------------------------//
    public class Bak_staff : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null || this.Session["system"].ToString() != "siamik" || this.Session["level"] == null)
            {
                Response.Redirect("~/Log.aspx");
            }
        }
    }
}