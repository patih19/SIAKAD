using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace akademik.am
{
    public partial class WebForm35 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelKelasKuliah.Enabled = false;
                this.PanelKelasKuliah.Visible = false;

                PopulateProdi();
            }
        }

        private void PopulateProdi()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }
                this.DLProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }


        protected void GVKelasKuluah_PreRender(object sender, EventArgs e)
        {
            if (this.GVKelasKuluah.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVKelasKuluah.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVKelasKuluah.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void BtnKelasKuliah_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdChPwd = new SqlCommand("SpFeederKelasKuliah", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@semester", this.TbSemester.Text.Trim());
                    CmdChPwd.Parameters.AddWithValue("@IdProdi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Semester");
                    TableJadwal.Columns.Add("Kode Makul");
                    TableJadwal.Columns.Add("Mata Kuliah");
                    TableJadwal.Columns.Add("Kelas");
                    TableJadwal.Columns.Add("Id Prodi");

                    using (SqlDataReader rdr = CmdChPwd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.LbKelasResult.Text = "";

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Semester"] = rdr["semester"].ToString().ToUpper().Trim();
                                string KodeMakul = rdr["kode_makul"].ToString().ToUpper().Trim();
                                datarow["Kode Makul"] = KodeMakul.Replace(" ", "").Trim();
                                datarow["Mata Kuliah"] = rdr["makul"].ToString().ToUpper().Trim();
                                datarow["Kelas"] = rdr["kelas"].ToString().ToUpper().Trim();
                                string IdProdi = rdr["id_prog_study"].ToString().Trim();
                                datarow["Id Prodi"] = IdProdi.Replace("-", "");

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKelasKuluah.DataSource = TableJadwal;
                            this.GVKelasKuluah.DataBind();

                            HighlightDuplicate(GVKelasKuluah);

                            // Show panel Maba
                            this.PanelKelasKuliah.Enabled = true;
                            this.PanelKelasKuliah.Visible = true;
                        }
                        else
                        {
                            this.LbKelasResult.Text = "Data Tidak Ditemukan";
                            this.LbKelasResult.ForeColor = System.Drawing.Color.Blue;

                            // hide panel Maba
                            this.PanelKelasKuliah.Enabled = false;
                            this.PanelKelasKuliah.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVKelasKuluah.DataSource = TableJadwal;
                            GVKelasKuluah.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }


        }

        public void HighlightDuplicate(GridView gridview)
        {
            for (int currentRow = 0; currentRow < gridview.Rows.Count - 1; currentRow++)
            {
                GridViewRow rowToCompare = gridview.Rows[currentRow];
                for (int otherRow = currentRow + 1; otherRow < gridview.Rows.Count; otherRow++)
                {
                    GridViewRow row = gridview.Rows[otherRow];
                    bool duplicateRow = true;

                    //check Duplicate on Kode Makul dan Kelas
                    if ((rowToCompare.Cells[1].Text.Trim() + rowToCompare.Cells[3].Text.Trim()) != (row.Cells[1].Text.Trim() + row.Cells[3].Text.Trim()))
                    {
                        duplicateRow = false;
                    }
                    else if (duplicateRow)
                    {                        
                        //rowToCompare.BackColor = Color.Yellow;
                        rowToCompare.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9494");
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9494");
                    }
                }
            }
        }
    }


}