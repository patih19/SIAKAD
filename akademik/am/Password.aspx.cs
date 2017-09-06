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
    //public partial class WebForm15 : System.Web.UI.Page
    public partial class WebForm15 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelPassword.Enabled = false;
                this.PanelPassword.Visible = false;

                this.LbResult.Text = "";
            }
        }

        protected void BtnCari_Click(object sender, EventArgs e)
        {
            //clear label result
            this.LbResult.Text = "";

            if (this.TbNPM.Text == "")
            {
                this.PanelPassword.Enabled = false;
                this.PanelPassword.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
                return;
            }

            //read Password
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdPasswd = new SqlCommand("SpVwPasswdMhs", con);
                CmdPasswd.CommandType = System.Data.CommandType.StoredProcedure;

                CmdPasswd.Parameters.AddWithValue("@npm", this.TbNPM.Text);


                using (SqlDataReader rdr = CmdPasswd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelPassword.Enabled = true;
                        this.PanelPassword.Visible = true;

                        this.LbResult.Text = "Password Ditemukan";
                        this.LbResult.ForeColor = System.Drawing.Color.Green;

                        while (rdr.Read())
                        {
                            this.LbNama.Text = rdr["nama"].ToString();
                            this.LbPasswd.Text = rdr["passwd"].ToString();
                        }
                    }
                    else
                    {
                        this.LbResult.Text = "Password Tidak Ditemukan";
                        this.LbResult.ForeColor = System.Drawing.Color.Red;

                        this.PanelPassword.Enabled = false;
                        this.PanelPassword.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan, PERIKSA NPM');", true);
                        return;
                    }
                }
            }

        }
    }
}