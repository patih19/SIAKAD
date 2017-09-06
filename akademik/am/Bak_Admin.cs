using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace akademik.am
{
    public class Bak_Admin : System.Web.UI.Page
    {
        // ---------------- PAGE KHUSUS UNTUK ADMIN BAAK -------------------------------//
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] != null && this.Session["system"].ToString() == "siamik" && this.Session["level"].ToString() == "admin")
            {

            }
            else if (this.Session["Name"] != null && this.Session["system"].ToString() == "siamik" && this.Session["level"].ToString() == "staff")
            {
                // get current page
                //string url = HttpContext.Current.Request.Url.ToString();
                //string rawurl = Request.RawUrl.ToString();

                Response.Redirect("~/am/home.aspx");
            }
            else
            {
                Response.Redirect("~/Log.aspx");
            }
        }
    }
}