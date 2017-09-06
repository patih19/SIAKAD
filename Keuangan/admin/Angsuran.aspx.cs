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
    // public partial class WebForm3 : System.Web.UI.Page
    public partial class WebForm3 : Keu_Admin_Class
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
            // Old Code ... Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            // Insert Biaya Studi dengan Stored Procedure
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //Filter Biaya Angsuran 
                SqlCommand cmd = new SqlCommand("ListBiayaAngsuranMhs", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@angkatan", DLThnAngkatan.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@prodi", DLProgStudi.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@kelas", DLKelas.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@semester", DLSemster.SelectedItem.Text);

                DataTable Table = new DataTable();
                Table.Columns.Add("Angsuran");
                Table.Columns.Add("Program Studi");
                Table.Columns.Add("Tahun Angkatan");
                Table.Columns.Add("Kelas");
                Table.Columns.Add("Semester");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Angsuran"] = rdr["no_angsuran"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            datarow["Tahun Angkatan"] = rdr["angkatan"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Semester"] = rdr["semester"];
                            datarow["Biaya"] = rdr["biaya_angsuran"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVAngsuran.DataSource = Table;
                        this.GVAngsuran.DataBind();

                        LBResult.Text = "data ditemukan";
                        LBResult.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        LBResult.Text = "data tidak ditemukan";
                        LBResult.ForeColor = System.Drawing.Color.Red;

                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVAngsuran.DataSource = Table;
                        GVAngsuran.DataBind();
                    }
                }
            }
        }

        protected void GVAngsuran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Biaya = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString5 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[5].Text = FormattedString5;
            }
        }
    }
}