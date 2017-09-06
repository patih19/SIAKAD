using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Keuangan.admin
{
    //public partial class WebForm6 : System.Web.UI.Page
    public partial class WebForm6 : Keu_Admin_Class
    {
        public decimal _BiayaKP
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["BiayaKP"].ToString());
            }
            set
            {
                this.ViewState["BiayaKP"] = (object)value;
            }
        }
        public decimal _BiayaPPLSD
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["BiayaPPLSD"].ToString());
            }
            set
            {
                this.ViewState["BiayaPPLSD"] = (object)value;
            }
        }
        public decimal _BiayaPPLSMA
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["BiayaPPLSMA"].ToString());
            }
            set
            {
                this.ViewState["BiayaPPLSMA"] = (object)value;
            }
        }
        public decimal _BiayaKKN
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["BiayaKKN"].ToString());
            }
            set
            {
                this.ViewState["BiayaKKN"] = (object)value;
            }
        }
        public decimal _BiayaWisuda
        {
            get
            {
                return Convert.ToDecimal(this.ViewState["BiayaWisuda"].ToString());
            }
            set
            {
                this.ViewState["BiayaWisuda"] = (object)value;
            }
        }

        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            //Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();
            this.Response.Redirect("~/keu-login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
                this.PanelBuat.Visible = false;
                this.PanelEdit.Visible = false;
                this.PanelGVBiaya.Visible = false;
            }

        }

        protected void GVBiaya_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    int KP = Convert.ToInt32(e.Row.Cells[1].Text);
                    string FormattedString = string.Format
                        (new System.Globalization.CultureInfo("id"), "{0:c}", KP);
                    e.Row.Cells[1].Text = FormattedString;
                }

                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    int PPLSD = Convert.ToInt32(e.Row.Cells[2].Text);
                    string FormattedString1 = string.Format
                        (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSD);
                    e.Row.Cells[2].Text = FormattedString1;
                }

                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    int PPLSMA = Convert.ToInt32(e.Row.Cells[3].Text);
                    string FormattedString2 = string.Format
                        (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSMA);
                    e.Row.Cells[3].Text = FormattedString2;
                }

                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    int KKN = Convert.ToInt32(e.Row.Cells[4].Text);
                    string FormattedString3 = string.Format
                        (new System.Globalization.CultureInfo("id"), "{0:c}", KKN);
                    e.Row.Cells[4].Text = FormattedString3;
                }

                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    int WISUDA = Convert.ToInt32(e.Row.Cells[5].Text);
                    string FormattedString4 = string.Format
                        (new System.Globalization.CultureInfo("id"), "{0:c}", WISUDA);
                    e.Row.Cells[5].Text = FormattedString4;
                }
            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (this.DLThnPelaksanaan.SelectedValue == "Tahun" || this.DLProgStudi.SelectedValue == "ALL" || this.DLProgStudi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun dan Program Studi');", true);
                return;
            }
            this.PanelBuat.Visible = true;

        }

        protected void BtnBuatOK_Click(object sender, EventArgs e)
        {
            if (this.DLThnPelaksanaan.SelectedValue == "Tahun" || this.DLProgStudi.SelectedValue == "ALL" || this.DLProgStudi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun dan Program Studi');", true);
                return;
            }

            if (this.DLProgStudi.SelectedValue == "-1" || this.DLProgStudi.SelectedValue == "ALL")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Salah Satu Program Studi');", true);
                return;
            }

            if (TbKP.Text == "")
            {
                TbKP.Text = "0";
            }
            if (this.TbPPLSD.Text == "")
            {
                this.TbPPLSD.Text = "0";
            }
            if (this.TbPPLSMA.Text == "")
            {
                this.TbPPLSMA.Text = "0";
            }
            if (this.TbKKN.Text == "")
            {
                this.TbKKN.Text = "0";
            }
            if (this.TbWisuda.Text== "")
            {
                this.TbWisuda.Text = "0";
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpInsertMasterBiayaAkhir", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@id_prog_study", this.DLProgStudi.SelectedValue);
                    cmd.Parameters.AddWithValue("@biaya_KP", this.TbKP.Text);
                    cmd.Parameters.AddWithValue("@biaya_PPLSD", this.TbPPLSD.Text);
                    cmd.Parameters.AddWithValue("@biaya_PPLSMA", this.TbPPLSMA.Text);
                    cmd.Parameters.AddWithValue("@biaya_WISUDA", this.TbWisuda.Text);
                    cmd.Parameters.AddWithValue("@biaya_KKN", this.TbKKN.Text);

                    cmd.ExecuteNonQuery();
                    TbKP.Text = "";
                    this.TbPPLSD.Text = "";
                    this.TbPPLSMA.Text = "";
                    this.TbKKN.Text = "";
                    this.TbWisuda.Text = "";
                    //clear Textbox


                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Biaya Akhir Berhasil ...');", true);

                    //DataTable Table = new DataTable();
                    //Table.Columns.Add("Program Studi");
                    //Table.Columns.Add("Kerja Praktik");
                    //Table.Columns.Add("PPL SD");
                    //Table.Columns.Add("PPL SMA");
                    //Table.Columns.Add("KKN");
                    //Table.Columns.Add("WISUDA");

                    //con.Open();
                    //using (SqlDataReader rdr = cmd.ExecuteReader())
                    //{
                    //    if (rdr.HasRows)
                    //    {
                    //        while (rdr.Read())
                    //        {
                    //            DataRow datarow = Table.NewRow();
                    //            datarow["Program Studi"] = rdr["prog_study"];
                    //            datarow["Kerja Praktik"] = rdr["kp"];
                    //            datarow["PPL SD"] = rdr["pplsd"];
                    //            datarow["PPL SMA"] = rdr["pplsma"];
                    //            datarow["KKN"] = rdr["kkn"];
                    //            datarow["WISUDA"] = rdr["wisuda"];
                    //            Table.Rows.Add(datarow);
                    //        }

                    //        //Fill Gridview
                    //        this.GVBiaya.DataSource = Table;
                    //        this.GVBiaya.DataBind();

                    //        LBResultFilter.Text = "";
                    //    }
                    //    else
                    //    {
                    //        LBResultFilter.Text = "data tidak ditemukan";
                    //        LBResultFilter.ForeColor = System.Drawing.Color.Red;

                    //        //clear Gridview
                    //        Table.Rows.Clear();
                    //        Table.Clear();
                    //        GVBiaya.DataSource = Table;
                    //        GVBiaya.DataBind();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }

        protected void GVKP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int KP = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", KP);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        protected void GVPPLSD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PPLSD = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSD);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        protected void GVPPLSMA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PPLSMA = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSMA);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        protected void GVKKN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int KKN = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", KKN);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        protected void GVWISUDA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Wisuda = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Wisuda);
                e.Row.Cells[1].Text = FormattedString1;
            }
        }

        //Button Edit Kerja Praktik
        protected void BtnEditKp_Click(object sender, EventArgs e)
        {
            //Clear viewstate biaya KP
            _BiayaKP = 0;

            this.PanelEdit.Enabled = true;
            this.PanelEdit.Visible = true;

            this.LbSUmberEdit.Text = "Kerja Praktik";

            //read from db record
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Record you want to update -------------------------------------------
                con.Open();
                SqlCommand cmdKP = new SqlCommand("SELECT id_kp,biaya FROM keu_biaya_kp where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                cmdKP.CommandType = System.Data.CommandType.Text;

                cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                string biaya = GVKP.Rows[0].Cells[1].Text; 
                string biaya2 = biaya.Replace("Rp", ""); 
                string biaya3 = biaya2.Replace(".","");

                _BiayaKP = Convert.ToDecimal(biaya3);

                cmdKP.Parameters.AddWithValue("@biaya", biaya3);

                using (SqlDataReader rdr = cmdKP.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TBEditBiaya.Text = rdr["biaya"].ToString();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Record Tidak Ditemukan, Proses dibatalkan');", true);
                    }
                }
            }
        }

        // DropDown Program Srudi Changed ==> Action ==> Filter
        protected void DLProgStudi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LBResultFilter.Text = "";
            this.PanelGVBiaya.Visible = true;
            this.PanelBuat.Visible = false;
            this.PanelEdit.Visible = false;

            if (DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            if (DLProgStudi.SelectedValue == "-1" || DLProgStudi.SelectedValue == "ALL")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- GRID VIEW KP -------------------------------------------
                con.Open();
                SqlCommand cmdKP = new SqlCommand("SELECT id_kp,biaya FROM keu_biaya_kp where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdKP.CommandType = System.Data.CommandType.Text;

                cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableKP = new DataTable();
                TableKP.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKP.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKP.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableKP.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKP.DataSource = TableKP;
                        this.GVKP.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKP.Rows.Clear();
                        TableKP.Clear();
                        GVKP.DataSource = TableKP;
                        GVKP.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW PPL SD -------------------------------------------
                SqlCommand cmdPPLSD = new SqlCommand("SELECT id_pplsd, biaya FROM keu_biaya_pplsd where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdPPLSD.CommandType = System.Data.CommandType.Text;

                cmdPPLSD.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSD.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TablePPLSD = new DataTable();
                TablePPLSD.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSD.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TablePPLSD.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSD.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSD.DataSource = TablePPLSD;
                        this.GVPPLSD.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSD.Rows.Clear();
                        TablePPLSD.Clear();
                        GVPPLSD.DataSource = TablePPLSD;
                        GVPPLSD.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW PPL SMA -------------------------------------------
                SqlCommand cmdPPLSMA = new SqlCommand("SELECT id_pplsma, biaya FROM keu_biaya_pplsma where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TablePPLSMA = new DataTable();
                TablePPLSMA.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TablePPLSMA.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSMA.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSMA.DataSource = TablePPLSMA;
                        this.GVPPLSMA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSMA.Rows.Clear();
                        TablePPLSMA.Clear();
                        GVPPLSMA.DataSource = TablePPLSMA;
                        GVPPLSMA.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW KKN -------------------------------------------
                SqlCommand cmdKKN = new SqlCommand("SELECT     id_kkn, biaya FROM keu_biaya_kkn where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdKKN.CommandType = System.Data.CommandType.Text;

                cmdKKN.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKKN.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableKKN = new DataTable();
                TableKKN.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKKN.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKKN.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableKKN.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKKN.DataSource = TableKKN;
                        this.GVKKN.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKKN.Rows.Clear();
                        TableKKN.Clear();
                        GVKKN.DataSource = TableKKN;
                        GVKKN.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW WISUDA -------------------------------------------
                SqlCommand cmdWisuda = new SqlCommand("SELECT id_wisuda, biaya FROM keu_biaya_wisuda where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdWisuda.CommandType = System.Data.CommandType.Text;

                cmdWisuda.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdWisuda.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableWisuda = new DataTable();
                TableWisuda.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdWisuda.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableWisuda.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableWisuda.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVWISUDA.DataSource = TableWisuda;
                        this.GVWISUDA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableWisuda.Rows.Clear();
                        TableWisuda.Clear();
                        GVWISUDA.DataSource = TableWisuda;
                        GVWISUDA.DataBind();
                    }
                }
            }
        }

        protected void TBEditBiayaUpdate_Click(object sender, EventArgs e)
        {
            //filter textbox edit
            if (TBEditBiaya.Text=="")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Biaya Tidak Boleh Kosong');", true);
                return;
            }
            try
            {
                // Case Edit
                if (this.LbSUmberEdit.Text == "Kerja Praktik")
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //----------------------------------------- UPDATE BIAYA KP -------------------------------------------
                        con.Open();
                        SqlCommand cmdKP = new SqlCommand("UPDATE keu_biaya_kp set biaya=@new_biaya where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                        cmdKP.CommandType = System.Data.CommandType.Text;

                        cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                        cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                        cmdKP.Parameters.AddWithValue("@biaya", _BiayaKP);
                        cmdKP.Parameters.AddWithValue("@new_biaya", this.TBEditBiaya.Text);

                        cmdKP.ExecuteNonQuery();

                        _BiayaKP = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Data Berhasil');", true);
                        return;
                    }
                }
                else if (this.LbSUmberEdit.Text == "PPL I")
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //----------------------------------------- UPDATE BIAYA PPL I -------------------------------------------
                        con.Open();
                        SqlCommand cmdKP = new SqlCommand("UPDATE keu_biaya_pplsd set biaya=@new_biaya where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                        cmdKP.CommandType = System.Data.CommandType.Text;

                        cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                        cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                        cmdKP.Parameters.AddWithValue("@biaya", _BiayaPPLSD);
                        cmdKP.Parameters.AddWithValue("@new_biaya", this.TBEditBiaya.Text);

                        cmdKP.ExecuteNonQuery();

                        _BiayaPPLSD = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Data Berhasil');", true);
                        return;
                    }

                }
                else if (this.LbSUmberEdit.Text == "PPL II")
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //----------------------------------------- UPDATE BIAYA PPL II -------------------------------------------
                        con.Open();
                        SqlCommand cmdKP = new SqlCommand("UPDATE keu_biaya_pplsma set biaya=@new_biaya where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                        cmdKP.CommandType = System.Data.CommandType.Text;

                        cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                        cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                        cmdKP.Parameters.AddWithValue("@biaya", _BiayaPPLSMA);
                        cmdKP.Parameters.AddWithValue("@new_biaya", this.TBEditBiaya.Text);

                        cmdKP.ExecuteNonQuery();

                        _BiayaPPLSMA = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Data Berhasil');", true);
                        return;
                    }

                }
                else if (this.LbSUmberEdit.Text == "KKN")
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //----------------------------------------- UPDATE BIAYA KKN -------------------------------------------
                        con.Open();
                        SqlCommand cmdKKN = new SqlCommand("UPDATE keu_biaya_kkn set biaya=@new_biaya where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                        cmdKKN.CommandType = System.Data.CommandType.Text;

                        cmdKKN.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                        cmdKKN.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                        cmdKKN.Parameters.AddWithValue("@biaya", _BiayaKKN);
                        cmdKKN.Parameters.AddWithValue("@new_biaya", this.TBEditBiaya.Text);

                        cmdKKN.ExecuteNonQuery();

                        _BiayaKKN = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Data Berhasil');", true);
                        return;
                    }
                }
                else if (this.LbSUmberEdit.Text == "Wisuda")
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //----------------------------------------- UPDATE BIAYA KKN -------------------------------------------
                        con.Open();
                        SqlCommand cmdWisuda = new SqlCommand("UPDATE keu_biaya_wisuda set biaya=@new_biaya where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                        cmdWisuda.CommandType = System.Data.CommandType.Text;

                        cmdWisuda.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                        cmdWisuda.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                        cmdWisuda.Parameters.AddWithValue("@biaya", _BiayaWisuda);
                        cmdWisuda.Parameters.AddWithValue("@new_biaya", this.TBEditBiaya.Text);

                        cmdWisuda.ExecuteNonQuery();

                        _BiayaWisuda = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Data Berhasil');", true);
                        return;
                    }
                }
            } catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                return;
            }
        }

        // Refresh After Update Biaya
        protected void BtnFrefresh_Click(object sender, EventArgs e)
        {
            this.LBResultFilter.Text = "";
            this.PanelGVBiaya.Visible = true;
            this.PanelBuat.Visible = false;
            this.PanelEdit.Visible = false;

            if (DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            if (DLProgStudi.SelectedValue == "-1" || DLProgStudi.SelectedValue == "ALL")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- GRID VIEW KP -------------------------------------------
                con.Open();
                SqlCommand cmdKP = new SqlCommand("SELECT id_kp,biaya FROM keu_biaya_kp where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdKP.CommandType = System.Data.CommandType.Text;

                cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKP.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableKP = new DataTable();
                TableKP.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKP.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKP.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableKP.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKP.DataSource = TableKP;
                        this.GVKP.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKP.Rows.Clear();
                        TableKP.Clear();
                        GVKP.DataSource = TableKP;
                        GVKP.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW PPL SD -------------------------------------------
                SqlCommand cmdPPLSD = new SqlCommand("SELECT id_pplsd, biaya FROM keu_biaya_pplsd where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdPPLSD.CommandType = System.Data.CommandType.Text;

                cmdPPLSD.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSD.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TablePPLSD = new DataTable();
                TablePPLSD.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSD.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TablePPLSD.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSD.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSD.DataSource = TablePPLSD;
                        this.GVPPLSD.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSD.Rows.Clear();
                        TablePPLSD.Clear();
                        GVPPLSD.DataSource = TablePPLSD;
                        GVPPLSD.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW PPL SMA -------------------------------------------
                SqlCommand cmdPPLSMA = new SqlCommand("SELECT id_pplsma, biaya FROM keu_biaya_pplsma where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TablePPLSMA = new DataTable();
                TablePPLSMA.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TablePPLSMA.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSMA.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSMA.DataSource = TablePPLSMA;
                        this.GVPPLSMA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSMA.Rows.Clear();
                        TablePPLSMA.Clear();
                        GVPPLSMA.DataSource = TablePPLSMA;
                        GVPPLSMA.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW KKN -------------------------------------------
                SqlCommand cmdKKN = new SqlCommand("SELECT     id_kkn, biaya FROM keu_biaya_kkn where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdKKN.CommandType = System.Data.CommandType.Text;

                cmdKKN.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKKN.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableKKN = new DataTable();
                TableKKN.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKKN.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKKN.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableKKN.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKKN.DataSource = TableKKN;
                        this.GVKKN.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKKN.Rows.Clear();
                        TableKKN.Clear();
                        GVKKN.DataSource = TableKKN;
                        GVKKN.DataBind();
                    }
                }

                // ----------------------------------------- GRID VIEW WISUDA -------------------------------------------
                SqlCommand cmdWisuda = new SqlCommand("SELECT id_wisuda, biaya FROM keu_biaya_wisuda where tahun=@tahun AND id_prog_study=@id_prodi", con);
                cmdWisuda.CommandType = System.Data.CommandType.Text;

                cmdWisuda.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdWisuda.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);

                DataTable TableWisuda = new DataTable();
                TableWisuda.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdWisuda.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableWisuda.NewRow();
                            datarow["Biaya"] = rdr["biaya"];

                            TableWisuda.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVWISUDA.DataSource = TableWisuda;
                        this.GVWISUDA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableWisuda.Rows.Clear();
                        TableWisuda.Clear();
                        GVWISUDA.DataSource = TableWisuda;
                        GVWISUDA.DataBind();
                    }
                }
            }
        }

        //Button Edit Biaya PPL SD
        protected void BtnEditPPLSD_Click(object sender, EventArgs e)
        {
            //Clear viewstate biaya PPL SD
            _BiayaPPLSD = 0;  

            this.PanelEdit.Enabled = true;
            this.PanelEdit.Visible = true;

            this.LbSUmberEdit.Text = "PPL I";

            //read from db record
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Record you want to update -------------------------------------------
                con.Open();
                SqlCommand cmdPPLSD = new SqlCommand("SELECT id_pplsd,biaya FROM keu_biaya_pplsd where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                cmdPPLSD.CommandType = System.Data.CommandType.Text;

                cmdPPLSD.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSD.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                string biaya = this.GVPPLSD.Rows[0].Cells[1].Text;
                string biaya2 = biaya.Replace("Rp", "");
                string biaya3 = biaya2.Replace(".", "");

                _BiayaPPLSD = Convert.ToDecimal(biaya3);

                cmdPPLSD.Parameters.AddWithValue("@biaya", biaya3);

                using (SqlDataReader rdr = cmdPPLSD.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TBEditBiaya.Text = rdr["biaya"].ToString();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Record Tidak Ditemukan, Proses dibatalkan');", true);
                    }
                }
            }
        }

        //Button Edit Biaya PPL SMA
        protected void BtnPPLSMA_Click(object sender, EventArgs e)
        {
            //Clear PPL SMA
            _BiayaPPLSMA = 0;

            this.PanelEdit.Enabled = true;
            this.PanelEdit.Visible = true;

            this.LbSUmberEdit.Text = "PPL II"; //Sebagai tanda

            //read from db record
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Record you want to update -------------------------------------------
                con.Open();
                SqlCommand cmdPPLSMA = new SqlCommand("SELECT id_pplsma,biaya FROM keu_biaya_pplsma where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                string biaya = this.GVPPLSMA.Rows[0].Cells[1].Text;
                string biaya2 = biaya.Replace("Rp", "");
                string biaya3 = biaya2.Replace(".", "");

                _BiayaPPLSMA = Convert.ToDecimal(biaya3);

                cmdPPLSMA.Parameters.AddWithValue("@biaya", biaya3);

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TBEditBiaya.Text = rdr["biaya"].ToString();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Record Tidak Ditemukan, Proses dibatalkan');", true);
                    }
                }
            }
        }
        //Button Edit Biaya KKN
        protected void BtnEditKKN_Click(object sender, EventArgs e)
        {
            //Clear KKN
            _BiayaKKN = 0;

            this.PanelEdit.Enabled = true;
            this.PanelEdit.Visible = true;

            this.LbSUmberEdit.Text = "KKN"; //Sebagai tanda

            //read from db record
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Record you want to update -------------------------------------------
                con.Open();
                SqlCommand cmdPPLSMA = new SqlCommand("SELECT id_kkn,biaya FROM keu_biaya_kkn where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                string biaya = this.GVKKN.Rows[0].Cells[1].Text;
                string biaya2 = biaya.Replace("Rp", "");
                string biaya3 = biaya2.Replace(".", "");

                _BiayaKKN = Convert.ToDecimal(biaya3);

                cmdPPLSMA.Parameters.AddWithValue("@biaya", biaya3);

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TBEditBiaya.Text = rdr["biaya"].ToString();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Record Tidak Ditemukan, Proses dibatalkan');", true);
                    }
                }
            }
        }

        //Button Edit Biaya Wisuda
        protected void BtnEditWisuda_Click(object sender, EventArgs e)
        {
            //Clear KKN
            _BiayaWisuda = 0;

            this.PanelEdit.Enabled = true;
            this.PanelEdit.Visible = true;

            this.LbSUmberEdit.Text = "Wisuda"; //Sebagai tanda

            //read from db record
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Record you want to update -------------------------------------------
                con.Open();
                SqlCommand cmdPPLSMA = new SqlCommand("SELECT id_wisuda,biaya FROM keu_biaya_wisuda where tahun=@tahun AND id_prog_study=@id_prodi AND biaya=@biaya", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@id_prodi", this.DLProgStudi.SelectedValue);
                string biaya = this.GVWISUDA.Rows[0].Cells[1].Text;
                string biaya2 = biaya.Replace("Rp", "");
                string biaya3 = biaya2.Replace(".", "");

                _BiayaWisuda = Convert.ToDecimal(biaya3);

                cmdPPLSMA.Parameters.AddWithValue("@biaya", biaya3);

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TBEditBiaya.Text = rdr["biaya"].ToString();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Record Tidak Ditemukan, Proses dibatalkan');", true);
                    }
                }
            }
        }
    }
}