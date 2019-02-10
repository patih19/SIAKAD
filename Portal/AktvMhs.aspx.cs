using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Portal
{
    public partial class WebForm20 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DLProdi.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdi.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));

                this.PanelAktivitas.Enabled = false;
                this.PanelAktivitas.Visible = false;
            }
        }

        protected void BtnAktvMhs_Click(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }

            if (this.DLProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.DlSemester.SelectedValue == "semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();
                    SqlCommand CmdJadwal = new SqlCommand("SpGetAktvMhs", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);
                    CmdJadwal.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("No");
                    TableJadwal.Columns.Add("NPM");
                    TableJadwal.Columns.Add("Nama");
                    TableJadwal.Columns.Add("IPS");
                    TableJadwal.Columns.Add("SKS-Sem");
                    TableJadwal.Columns.Add("IPK");
                    TableJadwal.Columns.Add("SKS-Total");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["No"] = rdr["nomor"];
                                datarow["NPM"] = rdr["npm"];
                                datarow["Nama"] = rdr["nama"];
                                datarow["IPS"] = rdr["ips"];
                                datarow["SKS-Sem"] = rdr["sks_sem"];
                                datarow["IPK"] = rdr["ipk"];
                                datarow["SKS-Total"] = rdr["sks_total"];

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVAktvMhs.DataSource = TableJadwal;
                            this.GVAktvMhs.DataBind();

                            this.PanelAktivitas.Enabled = true;
                            this.PanelAktivitas.Visible = true;
                        }
                        else
                        {
                            // hide panel Edit Mata Kuliah
                            this.PanelAktivitas.Enabled = false;
                            this.PanelAktivitas.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVAktvMhs.DataSource = TableJadwal;
                            GVAktvMhs.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GVAktvMhs_PreRender(object sender, EventArgs e)
        {
            if (this.GVAktvMhs.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVAktvMhs.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVAktvMhs.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVAktvMhs.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}