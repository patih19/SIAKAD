using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simuktpasca
{
    public class User : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (this.Session["Name"] == null || this.Session["password"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}