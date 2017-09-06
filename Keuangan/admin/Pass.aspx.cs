using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Keuangan.admin
{
    public partial class WebForm13 :Keu_Admin_Class
    {
        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            //Your Code here....
            this.Session["Name"] = null;
            this.Session["Passwd"] = null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }
        // ---------------------------------------------------- //

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();
        }

        string CekOldPass;
        protected void BtnSavePassword_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TbOldPassword.Text == System.String.Empty || this.TbNewPassword.Text == System.String.Empty || this.TbReNewPassword.Text == System.String.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Password Lama & Baru Wajib Diisi');", true);
                return;
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // --------------- Cek Old Password -----------------------
                    string OldPass = this.TbOldPassword.Text.Trim();


                    SqlCommand CmdOldPwd = new SqlCommand("select Password from system_users where username=@user", con);
                    CmdOldPwd.CommandType = System.Data.CommandType.Text;

                    CmdOldPwd.Parameters.AddWithValue("@user", this.Session["Name"].ToString());

                    using (SqlDataReader rdr = CmdOldPwd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                CekOldPass = rdr["Password"].ToString();
                            }
                        }
                    }

                    if (OldPass != CekOldPass)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Password Tidak Benar');", true);
                        return;
                    }

                    // --------------- Cek New Password -----------------
                    if (this.TbNewPassword.Text != this.TbReNewPassword.Text)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Password Baru Tidak Sama');", true);
                        return;
                    }

                    //---------------- Update password ------------------------
                    SqlCommand cmdLogin = new SqlCommand("Update system_users set Password=@pass where username=@user", con);
                    cmdLogin.CommandType = System.Data.CommandType.Text;

                    cmdLogin.Parameters.AddWithValue("@user", this.Session["Name"].ToString());
                    cmdLogin.Parameters.AddWithValue("@pass",this.TbReNewPassword.Text);

                    cmdLogin.ExecuteNonQuery();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
            }
        }
    }
}