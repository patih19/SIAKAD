using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    public partial class WebForm10 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMakul.Enabled = false;
                this.PanelMakul.Visible = false;
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetNoStore();
            }
        }

        protected void DLSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            //form validation
            if (this.DLTahun.SelectedValue == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedValue == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SPPesertaMakul", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@idprodi", this.Session["level"].ToString());
                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("No Jadwal");
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");
                TableMakul.Columns.Add("NIDN");
                TableMakul.Columns.Add("Dosen");
                TableMakul.Columns.Add("Kelas");
                TableMakul.Columns.Add("Jenis Kelas");
                TableMakul.Columns.Add("Peserta");


                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelMakul.Enabled = true;
                        this.PanelMakul.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["No Jadwal"] = rdr["no_jadwal"].ToString();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["NIDN"] = rdr["nidn"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Peserta"] = rdr["jumlah"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVAktif.DataSource = TableMakul;
                        this.GVAktif.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVAktif.DataSource = TableMakul;
                        GVAktif.DataBind();

                        this.PanelMakul.Enabled = false;
                        this.PanelMakul.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }
        }

        protected void GVAktif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //No Jadwal
            e.Row.Cells[4].Visible = false; //NIDN
        }

        protected void GVAktif_PreRender(object sender, EventArgs e)
        {
            if (this.GVAktif.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVAktif.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVAktif.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVAktif.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}