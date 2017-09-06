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
    public partial class WebForm37 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelKRSMhs.Enabled = false;
                this.PanelKRSMhs.Visible = false;

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


        protected void GVKrsMhs_PreRender(object sender, EventArgs e)
        {
            if (this.GVKrsMhs.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVKrsMhs.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVKrsMhs.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void BtnKRS_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdChPwd = new SqlCommand("SpFeederKRS", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@semester", this.TbSemester.Text.Trim());
                    CmdChPwd.Parameters.AddWithValue("@IdProdi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Id Prodi");
                    TableJadwal.Columns.Add("Kode Makul");
                    TableJadwal.Columns.Add("Mata Kuliah");
                    TableJadwal.Columns.Add("Kelas");
                    TableJadwal.Columns.Add("NPM");
                    TableJadwal.Columns.Add("Semester");

                    using (SqlDataReader rdr = CmdChPwd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.LbKRSResult.Text = "";

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                string IdProdi = rdr["id_prog_study"].ToString().Trim();
                                datarow["Id Prodi"] = IdProdi.Replace("-", "");
                                string KodeMakul = rdr["kode_makul"].ToString().ToUpper().Trim();
                               // datarow["Kode Makul"] = KodeMakul.Replace(" ", "").Trim();
                                datarow["Kode Makul"] = KodeMakul;
                                datarow["Mata Kuliah"] = rdr["makul"].ToString().ToUpper().Trim();
                                datarow["Kelas"] = rdr["kelas"].ToString().ToUpper().Trim();
                                datarow["NPM"] = rdr["npm"].ToString().ToUpper().Trim();
                                datarow["Semester"] = rdr["semester"].ToString().ToUpper().Trim();

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKrsMhs.DataSource = TableJadwal;
                            this.GVKrsMhs.DataBind();

                            // Show panel Maba
                            this.PanelKRSMhs.Enabled = true;
                            this.PanelKRSMhs.Visible = true;
                        }
                        else
                        {
                            this.LbKRSResult.Text = "Data Tidak Ditemukan";
                            this.LbKRSResult.ForeColor = System.Drawing.Color.Blue;

                            // hide panel Maba
                            this.PanelKRSMhs.Enabled = false;
                            this.PanelKRSMhs.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            this.GVKrsMhs.DataSource = TableJadwal;
                            GVKrsMhs.DataBind();
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