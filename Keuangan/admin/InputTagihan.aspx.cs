using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Keuangan.admin
{
    public partial class InputTagihan : Keu_Admin_Class
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
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnPostKekurangan_Click(object sender, EventArgs e)
        {

            if (this.DLAngsuran.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Angsuran');", true);
                return;
            }


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //open connection and begin transaction
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {   // Procedure Posting Tagihan ke Bank :
                    // Insert Pelunasan Tagihan  ---
                    SqlCommand CmdKekurangan = new SqlCommand("SpInsertKekurangan", con);
                    CmdKekurangan.Transaction = trans;
                    CmdKekurangan.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdKekurangan.Parameters.AddWithValue("@Npm_Mhs", this.TbNpm.Text.Trim());
                    CmdKekurangan.Parameters.AddWithValue("@semester", this.TbSemester.Text.Trim());
                    CmdKekurangan.Parameters.AddWithValue("@total_tagihan", this.TbBiaya.Text.Trim());
                    CmdKekurangan.Parameters.AddWithValue("@angsuran", this.DLAngsuran.SelectedValue.Trim());
                    CmdKekurangan.ExecuteNonQuery();

                    trans.Commit();
                    trans.Dispose();
                    CmdKekurangan.Dispose();

                    con.Close();
                    con.Dispose();

                    this.TbSemester.Text = "";
                    this.TbNpm.Text = "";
                    this.TbBiaya.Text = "";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    con.Close();
                    con.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }
    }
}