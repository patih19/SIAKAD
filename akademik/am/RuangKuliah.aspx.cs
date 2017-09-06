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
   //public partial class WebForm41 : System.Web.UI.Page
    public partial class WebForm41 : Bak_Admin
    {
        public int _IdRuang
        {
            get { return Convert.ToInt32(this.ViewState["No"].ToString()); }
            set { this.ViewState["No"] = (object)value; }
        }

        protected void BtnAddRuang_Click(object sender, EventArgs e)
        {
            this.PanelDataRuang.Visible = false;
            this.PanelDataRuang.Enabled = false;

            this.PanelAddRuang.Visible = true;
            this.PanelAddRuang.Enabled = true;

            this.PanelEditRuang.Visible = false;
            this.PanelEditRuang.Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDataRuang.Visible = false;
                this.PanelDataRuang.Enabled = false;

                this.PanelAddRuang.Visible = false;
                this.PanelAddRuang.Enabled = false;

                this.PanelEditRuang.Visible = false;
                this.PanelEditRuang.Enabled = false;

                PopulateProdi();
                PopulateProdi2();
                PopulateProdi3();
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

                    this.DLMasterProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLMasterProdi.DataTextField = "prog_study";
                    this.DLMasterProdi.DataValueField = "id_prog_study";
                    this.DLMasterProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLMasterProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void PopulateProdi2()
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

                    this.DLEditProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLEditProdi.DataTextField = "prog_study";
                    this.DLEditProdi.DataValueField = "id_prog_study";
                    this.DLEditProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLEditProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void PopulateProdi3()
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

                    this.DLAddProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLAddProdi.DataTextField = "prog_study";
                    this.DLAddProdi.DataValueField = "id_prog_study";
                    this.DLAddProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLAddProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void ListRuangByProdi()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //-----------------------------------------List All Ruang Kuliah -------------------------------------------
                con.Open();
                SqlCommand CmdRuang = new SqlCommand("SpRuangByProdi", con);
                CmdRuang.CommandType = System.Data.CommandType.StoredProcedure;

                if (this.DLMasterProdi.SelectedValue == "00-000")
                {
                   // All Prodi
                    this.PanelDataRuang.Visible = true;
                    this.PanelDataRuang.Enabled = true;

                    this.PanelAddRuang.Visible = false;
                    this.PanelAddRuang.Enabled = false;

                    this.PanelEditRuang.Visible = false;
                    this.PanelEditRuang.Enabled = false;
                }
                else if (this.DLMasterProdi.SelectedValue == "-1")
                {
                    this.PanelDataRuang.Visible = false;
                    this.PanelDataRuang.Enabled = false;

                    this.PanelAddRuang.Visible = false;
                    this.PanelAddRuang.Enabled = false;

                    this.PanelEditRuang.Visible = false;
                    this.PanelEditRuang.Enabled = false;

                    return;
                }
                else
                {
                    // By Prodi
                    CmdRuang.Parameters.AddWithValue("@IdProdi", this.DLMasterProdi.SelectedValue);

                    this.PanelDataRuang.Visible = true;
                    this.PanelDataRuang.Enabled = true;

                    this.PanelAddRuang.Visible = false;
                    this.PanelAddRuang.Enabled = false;

                    this.PanelEditRuang.Visible = false;
                    this.PanelEditRuang.Enabled = false;
                }


                DataTable TableRuang = new DataTable();
                TableRuang.Columns.Add("No");
                TableRuang.Columns.Add("Id Ruang");
                TableRuang.Columns.Add("Ruang");
                TableRuang.Columns.Add("Kapasitas");
                TableRuang.Columns.Add("Program Studi");
                TableRuang.Columns.Add("Keterangan");

                using (SqlDataReader rdr = CmdRuang.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDataRuang.Visible = true;
                        this.PanelDataRuang.Enabled = true;
                        
                        while (rdr.Read())
                        {
                            DataRow datarow = TableRuang.NewRow();
                            datarow["No"] = rdr["nomor"];
                            datarow["Id Ruang"] = rdr["no"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Kapasitas"] = rdr["kapasitas"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            if (rdr["status"].ToString() == "1")
                            {
                                datarow["Keterangan"] = "Aktif";
                            }
                            else
                            {
                                datarow["Keterangan"] = "Non Aktif";
                            }

                            TableRuang.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVRuang.DataSource = TableRuang;
                        this.GVRuang.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableRuang.Rows.Clear();
                        TableRuang.Clear();
                        GVRuang.DataSource = TableRuang;
                        GVRuang.DataBind();
                    }
                }
            }
        }

        protected void GVRuang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; // id ruang kuliah
        }

        protected void GVRuang_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVRuang.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            _IdRuang = Convert.ToInt32(this.GVRuang.Rows[index].Cells[1].Text);

            this.PanelDataRuang.Visible = false;
            this.PanelDataRuang.Enabled = false;

            this.PanelAddRuang.Visible = false;
            this.PanelAddRuang.Enabled = false;

            this.PanelEditRuang.Visible = true;
            this.PanelEditRuang.Enabled = true;


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //-----------------------------------------List Ruang Kuliah By Id-------------------------------------------
                con.Open();
                SqlCommand CmdEditRuang = new SqlCommand("SpRuangGetByID", con);
                CmdEditRuang.CommandType = System.Data.CommandType.StoredProcedure;

                CmdEditRuang.Parameters.AddWithValue("@IdRuang", _IdRuang);

                using (SqlDataReader rdr = CmdEditRuang.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbEditNamaRuang.Text = rdr["nm_ruang"].ToString();
                            this.TbEditKapasitas.Text = rdr["kapasitas"].ToString();
                            this.DLEditProdi.SelectedValue = rdr["id_prog_study"].ToString();
                            if (rdr["status"].ToString() == "1")
                            {
                                this.RbEditAktif.Checked = true;
                                this.RbEditNonAktif.Checked = false;
                            }
                            else
                            {
                                this.RbEditAktif.Checked = false;
                                this.RbEditNonAktif.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Write("Data Ruang Tidak Ditemukan");
                    }
                }
            }

        }

        protected void BtnEditSave_Click(object sender, EventArgs e)
        {
            if (this.TbEditNamaRuang.Text == string.Empty)
            {
                return;
            }

            if (this.TbEditKapasitas.Text == string.Empty)
            {
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    //-----------------------------------------Update Ruang Kuliah By Id-------------------------------------------
                    con.Open();
                    SqlCommand CmdSaveEditRuang = new SqlCommand("Update bak_ruang set nm_ruang=@namaruang, kapasitas=@kapasitas, id_prog_study=@idprodi, status=@status, last_update=@update where no=@IdRuang", con);
                    CmdSaveEditRuang.CommandType = System.Data.CommandType.Text;

                    CmdSaveEditRuang.Parameters.AddWithValue("@IdRuang", _IdRuang);
                    CmdSaveEditRuang.Parameters.AddWithValue("@namaruang", this.TbEditNamaRuang.Text.ToString().Trim());
                    CmdSaveEditRuang.Parameters.AddWithValue("@kapasitas", this.TbEditKapasitas.Text.ToString().Trim());
                    CmdSaveEditRuang.Parameters.AddWithValue("@idprodi", this.DLEditProdi.SelectedValue.ToString().Trim());
                    if (this.RbEditAktif.Checked == true)
                    {
                        CmdSaveEditRuang.Parameters.AddWithValue("@status", "1");
                    }
                    else if (this.RbEditNonAktif.Checked == true)
                    {
                        CmdSaveEditRuang.Parameters.AddWithValue("@status", "0");
                    }
                    CmdSaveEditRuang.Parameters.AddWithValue("@update", DateTime.Now);

                    CmdSaveEditRuang.ExecuteNonQuery();

                    DLMasterProdi_SelectedIndexChanged(this.DLMasterProdi, null);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                }
                catch (Exception)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Gagal');", true);
                }

            }
        }

        protected void BtnEditBatal_Click(object sender, EventArgs e)
        {
            DLMasterProdi_SelectedIndexChanged(this.DLMasterProdi, null);
        }

        protected void BtnAddSave_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    //-----------------------------------------Inset New Ruang Kuliah-------------------------------------------
                    con.Open();
                    SqlCommand CmdAddRuang = new SqlCommand("INSERT INTO bak_ruang (nm_ruang, kapasitas, id_prog_study, status, tgl_add) values(@ruang, @kapasitas, @IdProdi, @status, getdate() )", con);
                    CmdAddRuang.CommandType = System.Data.CommandType.Text;

                    CmdAddRuang.Parameters.AddWithValue("@ruang",this.TbAddRuang.Text.Trim() );
                    CmdAddRuang.Parameters.AddWithValue("@kapasitas",this.TbAddKapasitas.Text.Trim() );
                    CmdAddRuang.Parameters.AddWithValue("@idprodi",this.DLAddProdi.SelectedValue.Trim() );
                    CmdAddRuang.Parameters.AddWithValue("@status","1" );

                    CmdAddRuang.ExecuteNonQuery();

                    DLMasterProdi_SelectedIndexChanged(this.DLMasterProdi, null);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);

                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('"+ex.Message.ToString() +"');", true);
                }

            }
        }

        protected void BtnAddBatal_Click(object sender, EventArgs e)
        {
            DLMasterProdi_SelectedIndexChanged(this.DLMasterProdi, null);
        }

        protected void DLMasterProdi_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListRuangByProdi();

        }



    }
}