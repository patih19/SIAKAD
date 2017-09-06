using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace akademik.am
{
    //public partial class WebForm14 : System.Web.UI.Page
    public partial class WebForm14 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                    SqlCommand CmdChPwd = new SqlCommand("SpChangePwd", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@user", this.Session["Name"].ToString());
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