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
    public partial class ViewKrs : MhsPasca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavMasterKRS");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavMasterKRS");
                control2.Attributes.Add("style", "display: block;");

                this.PanelListKRS.Enabled = false;
                this.PanelListKRS.Visible = false;
            }
        }

        protected void BtnViewKRS_Click(object sender, EventArgs e)
        {
            try
            {
                //1. ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // --------------------- Fill Gridview  ------------------------
                    SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                    CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdListKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    DataTable TableKRS = new DataTable();
                    TableKRS.Columns.Add("Key");
                    TableKRS.Columns.Add("Kode");
                    TableKRS.Columns.Add("Mata Kuliah");
                    TableKRS.Columns.Add("SKS");
                    TableKRS.Columns.Add("Dosen");
                    TableKRS.Columns.Add("Kelas");
                    TableKRS.Columns.Add("Hari");
                    TableKRS.Columns.Add("Mulai");
                    TableKRS.Columns.Add("Selesai");
                    TableKRS.Columns.Add("Ruang");

                    using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelListKRS.Enabled = true;
                            this.PanelListKRS.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKRS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari"];
                                datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                datarow["Ruang"] = rdr["nm_ruang"];

                                TableKRS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVListKrs.DataSource = TableKRS;
                            this.GVListKrs.DataBind();

                            //this.DLSemester.SelectedValue = "Semester";
                        }
                        else
                        {
                            //clear Gridview
                            TableKRS.Rows.Clear();
                            TableKRS.Clear();
                            GVListKrs.DataSource = TableKRS;
                            GVListKrs.DataBind();

                            this.PanelListKRS.Enabled = false;
                            this.PanelListKRS.Visible = false;

                            this.DLSemester.SelectedValue = "Semester";

                            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                            string message = "alert('KRS Semester Ini Belum Ada')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.PanelListKRS.Enabled = false;
                this.PanelListKRS.Visible = false;

                this.DLSemester.SelectedValue = "Semester";

                string message = "alert('" + ex.Message.ToString() + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //return;
            }

        }

        protected void GVListKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void BtnDwnKrs_Click(object sender, EventArgs e)
        {

        }
    }
}