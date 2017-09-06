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
    public partial class WebForm39 :Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelAktvMhs.Enabled = false;
                this.PanelAktvMhs.Visible = false;

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
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void BtnAktvMhs_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdChPwd = new SqlCommand("SpFeederAktvMhs", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@semester", this.TbSemester.Text.Trim());
                    CmdChPwd.Parameters.AddWithValue("@IdProdi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();

                    TableJadwal.Columns.Add("Semester");
                    TableJadwal.Columns.Add("NPM");
                    TableJadwal.Columns.Add("Status");
                    TableJadwal.Columns.Add("IPS");
                    TableJadwal.Columns.Add("SKS Semester");
                    TableJadwal.Columns.Add("IPK");
                    TableJadwal.Columns.Add("SKS Total");

                    using (SqlDataReader rdr = CmdChPwd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.LbBtnAktvMhsResult.Text = "";

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();

                                datarow["Semester"] = rdr["smster"].ToString().ToUpper().Trim();
                                datarow["NPM"] = rdr["npm"].ToString().ToUpper().Trim();
                                datarow["Status"] = rdr["status"].ToString().ToUpper().Trim();
                                datarow["IPS"] = rdr["ips"].ToString().ToUpper().Trim();
                                datarow["SKS Semester"] = rdr["sks_sem"].ToString().ToUpper().Trim();
                                datarow["IPK"] = rdr["ipk"].ToString().ToUpper().Trim();
                                datarow["SKS Total"] = rdr["sks_total"].ToString().ToUpper().Trim();

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVAktvMhs.DataSource = TableJadwal;
                            this.GVAktvMhs.DataBind();

                            // Show panel Maba
                            this.PanelAktvMhs.Enabled = true;
                            this.PanelAktvMhs.Visible = true;
                        }
                        else
                        {
                            this.LbBtnAktvMhsResult.Text = "Data Tidak Ditemukan";
                            this.LbBtnAktvMhsResult.ForeColor = System.Drawing.Color.Blue;

                            // hide panel Maba
                            this.PanelAktvMhs.Enabled = false;
                            this.PanelAktvMhs.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();

                            this.GVAktvMhs.DataSource = TableJadwal;
                            GVAktvMhs.DataBind();
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
    }
}