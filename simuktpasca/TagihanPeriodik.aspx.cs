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

namespace simuktpasca
{
    public partial class TagihanPeriodik : User
    {
        public string _IdProdi
        {
            get { return this.ViewState["IdProdi"].ToString(); }
            set { this.ViewState["IdProdi"] = (object)value; }
        }

        public int _Page
        {
            get { return Convert.ToInt32(this.ViewState["Page"].ToString()); }
            set { this.ViewState["Page"] = (object)value; }
        }

        public int _IdBiaya
        {
            get { return Convert.ToInt32(this.ViewState["IdBiaya"].ToString()); }
            set { this.ViewState["IdBiaya"] = (object)value; }
        }

        private int PageSize = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavMasterTagihan");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavMasterTagihan");
                control2.Attributes.Add("style", "display: block;");

                PanelMessage.Enabled = false;
                PanelMessage.Visible = false;

                this.PanelAddTagihan.Enabled = false;
                this.PanelAddTagihan.Visible = false;

                this.PanelEditTagihan.Enabled = false;
                this.PanelEditTagihan.Visible = false;

                this.PanelContentTagihan.Enabled = false;
                this.PanelContentTagihan.Visible = false;

                //GetPrestasiPmdk(1);
                //this.Lbpage.Text = "1";
                _Page = 1;

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

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study WHERE jenjang='S2'", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLTagihanProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLTagihanProdi.DataTextField = "prog_study";
                    this.DLTagihanProdi.DataValueField = "id_prog_study";
                    this.DLTagihanProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }
                this.DLTagihanProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GetTagihanPeriodik(int pageIndex, string IdProdi)
        {
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpBiayaPeriodikPaging", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@IdProdi", IdProdi);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                    DataTable TableData = new DataTable();
                    TableData.Columns.Add("NO");
                    TableData.Columns.Add("ID BIAYA");
                    TableData.Columns.Add("ID PRODI");
                    TableData.Columns.Add("SPP");
                    TableData.Columns.Add("SARDIK");
                    TableData.Columns.Add("MATRIKULASI");
                    TableData.Columns.Add("SPI");
                    TableData.Columns.Add("TAHUN ANGKATAN");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelContentTagihan.Visible = true;
                            this.PanelContentTagihan.Enabled = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableData.NewRow();
                                datarow["NO"] = rdr["nomor"];
                                datarow["ID BIAYA"] = rdr["id_biaya"];
                                datarow["ID PRODI"] = rdr["id_prog_study"];

                                int SPP = Convert.ToInt32 ( rdr["spp"].ToString());
                                string SPPFormatted = string.Format
                                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                                datarow["SPP"] = SPPFormatted;

                                int SARDIK = Convert.ToInt32(rdr["sardik"].ToString());
                                string SardikFormatted = string.Format
                                    (new System.Globalization.CultureInfo("id"), "{0:c}", SARDIK);
                                datarow["SARDIK"] = SardikFormatted;

                                int MATRIKULASI = Convert.ToInt32(rdr["matrikulasi"].ToString());
                                string MatrikulasiFormatted = string.Format
                                    (new System.Globalization.CultureInfo("id"), "{0:c}", MATRIKULASI);
                                datarow["MATRIKULASI"] = MatrikulasiFormatted;

                                int SPI = Convert.ToInt32(rdr["spi"].ToString());
                                string SPIFormatted = string.Format
                                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPI);
                                datarow["SPI"] = SPIFormatted;

                                datarow["TAHUN ANGKATAN"] = rdr["thn_angkatan"];

                                TableData.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.RptTagihanPeriodik.DataSource = TableData;
                            this.RptTagihanPeriodik.DataBind();
                        } else
                        {
                            this.PanelContentTagihan.Visible = false;
                            this.PanelContentTagihan.Enabled = false;
                        }
                    }

                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    this.PopulatePager(recordCount, pageIndex);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            this.Lbpage.Text = currentPage.ToString().Trim();

            List<ListItem> pages = new List<ListItem>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new ListItem("First", "1"));
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new ListItem("Last", pageCount.ToString()));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            this.PanelMessage.Visible = false;
            this.PanelMessage.Enabled = false;

            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            _Page = pageIndex;
            this.GetTagihanPeriodik(_Page, _IdProdi);
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            PanelMessage.Enabled = false;
            PanelMessage.Visible = false;

            this.PanelAddTagihan.Enabled = true;
            this.PanelAddTagihan.Visible = true;

            this.PanelListTagihan.Enabled = false;
            this.PanelListTagihan.Visible = false;
        }

        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            if(this.TbThnAngkatan.Text == string.Empty || this.TbAddSPP.Text == string.Empty || this.tbAddSPI.Text == string.Empty || this.TbAddSardik.Text == string.Empty || this.TbAddMatrik.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI SEMUA TAGIHAN');", true);
                return;
            }

            this.PanelMessage.Visible = false;
            this.PanelEditTagihan.Visible = false;
            this.PanelAddTagihan.Visible = false;
            this.PanelListTagihan.Visible = true;
            this.PanelListTagihan.Enabled = true;
            this.PanelContentTagihan.Visible = true;
            this.PanelContentTagihan.Enabled = true;

            this.DLTagihanProdi.SelectedValue = _IdProdi;
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        " SELECT id_biaya FROM keu_biaya WHERE id_prog_study = @IdProdi AND thn_angkatan = @ThnAngkatan "+
                        " IF @@ROWCOUNT >= 1 "+
                        " BEGIN "+
                            " RAISERROR('TAGIHAN SUDAH ADA', 16, 10); "+
                            " RETURN "+
                          "END "+
                        " ELSE "+
                        " BEGIN "+
                            " INSERT INTO keu_biaya(id_prog_study, thn_angkatan, spp, spi,sardik, matrikulasi, tgl_update) "+
                            " VALUES(@IdProdi, @ThnAngkatan, @spp, @spi, @sardik, @matrikulasi, GETDATE()) "+
                        "END "+
                        "", con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdProdi", _IdProdi);
                    cmd.Parameters.AddWithValue("@ThnAngkatan",this.TbThnAngkatan.Text.Trim());
                    cmd.Parameters.AddWithValue("@spp", this.TbAddSPP.Text.Trim());
                    cmd.Parameters.AddWithValue("@spi", this.tbAddSPI.Text.Trim());
                    cmd.Parameters.AddWithValue("@sardik", this.TbAddSardik.Text.Trim());
                    cmd.Parameters.AddWithValue("@matrikulasi", this.TbAddMatrik.Text.Trim());

                    cmd.ExecuteNonQuery();

                    this.TbAddSPP.Text = "";
                    this.tbAddSPI.Text = "";
                    this.TbAddSardik.Text = "";
                    this.TbAddMatrik.Text = "";
                    this.TbThnAngkatan.Text = "";
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }

            GetTagihanPeriodik(1, _IdProdi);
        }

        // --- Edit Tagihan Periodik ----
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            this.PanelEditTagihan.Enabled = true;
            this.PanelEditTagihan.Visible = true;

            this.PanelMessage.Visible = false;
            this.PanelAddTagihan.Visible = false;
            this.PanelListTagihan.Visible = false;

            // Get the command argument
            Button button = (sender as Button);
            string[] CommandArgument = button.CommandArgument.Split(',');

            // id biaya
            _IdBiaya = Convert.ToInt32(CommandArgument[0].ToString().Trim());
            
            // id prodi
            // Response.Write(CommandArgument[1].ToString().Trim());
            // last page tidak dipakai
            // => _page

            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_biaya,spp,spi,sardik,matrikulasi FROM keu_biaya WHERE id_biaya=@IdBiaya", con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdBiaya", CommandArgument[0].ToString().Trim());

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this.TbEditSPP.Text = rdr["spp"].ToString().Trim();
                                this.TbEditSPI.Text = rdr["spi"].ToString().Trim();
                                this.TbEditSARDIK.Text = rdr["sardik"].ToString().Trim();
                                this.TbEditMATRIK.Text = rdr["matrikulasi"].ToString().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        protected void BtnBatal_Click(object sender, EventArgs e)
        {
            this.PanelMessage.Visible = false;
            this.PanelEditTagihan.Visible = false;
            this.PanelAddTagihan.Visible = false;
            this.PanelListTagihan.Visible = true;
            this.PanelListTagihan.Enabled = true;
            this.PanelContentTagihan.Visible = true;
            this.PanelContentTagihan.Enabled = true;


            this.DLTagihanProdi.SelectedValue = _IdProdi;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            this.PanelMessage.Visible = false;
            this.PanelEditTagihan.Visible = false;
            this.PanelAddTagihan.Visible = false;
            this.PanelListTagihan.Visible = true;
            this.PanelListTagihan.Enabled = true;
            this.PanelContentTagihan.Visible = true;
            this.PanelContentTagihan.Enabled = true;

            this.DLTagihanProdi.SelectedValue = _IdProdi;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            this.PanelMessage.Visible = false;
            this.PanelEditTagihan.Visible = false;
            this.PanelAddTagihan.Visible = false;
            this.PanelListTagihan.Visible = true;
            this.PanelContentTagihan.Visible = true;

            this.DLTagihanProdi.SelectedValue = _IdProdi;
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE keu_biaya SET spp=@spp,spi=@spi,sardik=@sardik,matrikulasi=@matrik WHERE id_biaya=@IdBiaya", con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdBiaya", _IdBiaya);
                    cmd.Parameters.AddWithValue("@spp", this.TbEditSPP.Text.Trim());
                    cmd.Parameters.AddWithValue("@spi", this.TbEditSPI.Text.Trim());
                    cmd.Parameters.AddWithValue("@sardik",this.TbEditSARDIK.Text.Trim());
                    cmd.Parameters.AddWithValue("@matrik", this.TbEditMATRIK.Text.Trim());

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }

            GetTagihanPeriodik(_Page, _IdProdi);
        }

        //protected void BtnDel_Click(object sender, EventArgs e)
        //{
        //    Response.Write("http://wwww.google.com");
        //}

        protected void BtnOpenTagihan_Click(object sender, EventArgs e)
        {
            this._IdProdi = this.DLTagihanProdi.SelectedValue.Trim();
            GetTagihanPeriodik(1, _IdProdi);


        }
    }


}