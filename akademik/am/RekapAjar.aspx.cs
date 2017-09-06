using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace akademik.am
{
    public partial class WebForm42 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDaftarSKS.Enabled = false;
                this.PanelDaftarSKS.Visible = false;
            }
        }

        protected void BtnSksDosen_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdSKSDosen = new SqlCommand("SpRekapSKSDosen", con);
                CmdSKSDosen.CommandType = System.Data.CommandType.StoredProcedure;

                CmdSKSDosen.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text.ToString().Trim() + this.DlSemester.SelectedValue.ToString().Trim());

                DataTable TableSKSDosen = new DataTable();
                TableSKSDosen.Columns.Add("NIDN/NIDK/NUP");
                TableSKSDosen.Columns.Add("Nama");
                TableSKSDosen.Columns.Add("Semester");
                TableSKSDosen.Columns.Add("Program Studi");
                TableSKSDosen.Columns.Add("SKS Total");

                using (SqlDataReader rdr = CmdSKSDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDaftarSKS.Enabled = true;
                        this.PanelDaftarSKS.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableSKSDosen.NewRow();
                            datarow["NIDN/NIDK/NUP"] = rdr["nidn"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["Semester"] = rdr["semester"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            datarow["SKS Total"] = rdr["sks_total"];

                            TableSKSDosen.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVSKSDosen.DataSource = TableSKSDosen;
                        this.GVSKSDosen.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableSKSDosen.Rows.Clear();
                        TableSKSDosen.Clear();

                        GVSKSDosen.DataSource = TableSKSDosen;
                        GVSKSDosen.DataBind();

                        this.PanelDaftarSKS.Enabled = false;
                        this.PanelDaftarSKS.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                    }
                }
            }
        }
    }
}