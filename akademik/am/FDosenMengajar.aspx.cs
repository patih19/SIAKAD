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
    public partial class WebForm36 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDosenMengajar.Enabled = false;
                this.PanelDosenMengajar.Visible = false;

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

        protected void BtnMengajar_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdChPwd = new SqlCommand("SpFeederDosenMengajar", con);
                    CmdChPwd.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChPwd.Parameters.AddWithValue("@semester", this.TbSemester.Text.Trim());
                    CmdChPwd.Parameters.AddWithValue("@IdProdi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Semester");
                    TableJadwal.Columns.Add("NIDN/NIDK");
                    TableJadwal.Columns.Add("Kode Makul");
                    TableJadwal.Columns.Add("Mata Kuliah");
                    TableJadwal.Columns.Add("SKS");
                    TableJadwal.Columns.Add("Tatap Muka");
                    TableJadwal.Columns.Add("Id Prodi");
                    TableJadwal.Columns.Add("Kelas");

                    using (SqlDataReader rdr = CmdChPwd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.LbMengajarResult.Text = "";

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Semester"] = rdr["semester"].ToString().ToUpper().Trim();
                                datarow["NIDN/NIDK"] = rdr["nidn"].ToString().ToUpper().Trim();
                                string KodeMakul = rdr["kode_makul"].ToString().ToUpper().Trim();
                                datarow["Kode Makul"] = KodeMakul.Replace(" ", "").Trim();
                                datarow["Mata Kuliah"] = rdr["makul"].ToString().ToUpper().Trim();
                                datarow["SKS"] = rdr["sks"].ToString().ToUpper().Trim();
                                datarow["Tatap Muka"] = "16";
                                string IdProdi = rdr["id_prog_study"].ToString().Trim();
                                datarow["Id Prodi"] = IdProdi.Replace("-", "");
                                datarow["Kelas"] = rdr["kelas"].ToString().ToUpper().Trim();

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVDosenMengajar.DataSource = TableJadwal;
                            this.GVDosenMengajar.DataBind();

                            // Show panel Maba
                            this.PanelDosenMengajar.Enabled = true;
                            this.PanelDosenMengajar.Visible = true;
                        }
                        else
                        {
                            this.LbMengajarResult.Text = "Data Tidak Ditemukan";
                            this.LbMengajarResult.ForeColor = System.Drawing.Color.Blue;

                            // hide panel Maba
                            this.PanelDosenMengajar.Enabled = false;
                            this.PanelDosenMengajar.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVDosenMengajar.DataSource = TableJadwal;
                            GVDosenMengajar.DataBind();
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

        protected void GVDosenMengajar_PreRender(object sender, EventArgs e)
        {
            if (this.GVDosenMengajar.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVDosenMengajar.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVDosenMengajar.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

    }
}