using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.HtmlControls;


namespace Padu.pasca
{
    public partial class password : MhsPasca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavPassword");
                control.Attributes.Add("class", "dropdown active opened");
            }
        }

        protected void BtGanti_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TbOldPw.Text == "" || this.TbNewPw.Text == "" || this.TbReNewPw.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Password Lama & Baru Wajib Diisi');", true);
                return;
            }

            if (this.TbNewPw.Text != this.TbReNewPw.Text)
            {
                this.TbNewPw.Text = "";
                this.TbReNewPw.Text = "";

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Ulangi Password Baru');", true);
                return;
            }

            //Change Password
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdChPwd = new SqlCommand("SpChangePwdMhs", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdChPwd.Parameters.AddWithValue("@oldpw", this.TbOldPw.Text);
                    CmdChPwd.Parameters.AddWithValue("@newpw", this.TbNewPw.Text);
                    CmdChPwd.Parameters.AddWithValue("@renewpw", this.TbReNewPw.Text);

                    CmdChPwd.ExecuteNonQuery();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Ganti Password Berhasil');", true);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }
    }
}