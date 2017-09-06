using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Portal
{
    //public partial class WebForm21 : System.Web.UI.Page
    public partial class WebForm21 : Tu
    {
        //View State KHS
        public decimal _TotalSKS
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString()); }
            set
            { this.ViewState["TotalSKS"] = (object)value; }
        }
        public decimal _TotalNilai
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalNilai"].ToString()); }
            set
            { this.ViewState["TotalNilai"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DLProdi.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdi.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));

                this.PanelRekapSKS.Enabled = false;
                this.PanelRekapSKS.Visible = false;
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SPGetMhsKRS", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.DLProdi.SelectedValue);
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("No");
                TableJadwal.Columns.Add("NPM");
                TableJadwal.Columns.Add("Nama");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbIdProdi.Text = this.DLProdi.SelectedValue;
                        this.LbProdi.Text = this.DLProdi.SelectedItem.Text;

                        this.LbTahun.Text = this.DLTahun.SelectedItem.Text;
                        this.LbSemester.Text = this.DLSemester.SelectedItem.Text;

                        this.PanelRekapSKS.Enabled = true;
                        this.PanelRekapSKS.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["No"] = rdr["urutan"];
                            datarow["NPM"] = rdr["npm"];
                            datarow["Nama"] = rdr["nama"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVRekapSKS.DataSource = TableJadwal;
                        this.GVRekapSKS.DataBind();
                    }
                    else
                    {
                        // hide panel Edit Mata Kuliah
                        this.PanelRekapSKS.Enabled = false;
                        this.PanelRekapSKS.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVRekapSKS.DataSource = TableJadwal;
                        GVRekapSKS.DataBind();
                    }
                }
            }
        }

        protected void GVRekapSKS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVRekapSKS.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void LnkKHS_Click(object sender, EventArgs e)
        {
            try
            {
                // get row index
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                //1. ----- set keterangan mahasiswa 
                this.LbNPM.Text = this.GVRekapSKS.Rows[index].Cells[1].Text;
                this.LbNama.Text = this.GVRekapSKS.Rows[index].Cells[2].Text;
                this.LbSem.Text = this.LbTahun.Text + this.LbSemester.Text;

                //2. ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    // --------------------- Fill Gridview  ------------------------
                    SqlCommand CmdListKRS = new SqlCommand("SpGetKHS", con);
                    CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;


                    CmdListKRS.Parameters.AddWithValue("@npm", this.GVRekapSKS.Rows[index].Cells[1].Text);
                    CmdListKRS.Parameters.AddWithValue("@semester", this.LbTahun.Text + this.LbSemester.Text);

                    DataTable TableKRS = new DataTable();
                    TableKRS.Columns.Add("Kode");
                    TableKRS.Columns.Add("Mata Kuliah");
                    TableKRS.Columns.Add("SKS");
                    TableKRS.Columns.Add("Nilai");
                    TableKRS.Columns.Add("Jumlah");

                    using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            // show modal
                            ModalPopupExtender1.Show();

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKRS.NewRow();
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                if (rdr["Nilai"] == DBNull.Value)
                                {
                                    //datarow["Nilai"] = "";
                                }
                                else
                                {
                                    datarow["Nilai"] = rdr["nilai"];
                                }

                                if (rdr["jumlah"] == DBNull.Value)
                                {
                                    
                                }
                                else
                                {
                                    datarow["Jumlah"] = rdr["jumlah"];
                                }

                                TableKRS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVKHS.DataSource = TableKRS;
                            this.GVKHS.DataBind();

                            //Set Label
                            this.LBSks.Text = _TotalSKS.ToString();
                            decimal IPS = _TotalNilai / _TotalSKS;
                            this.LbIPS.Text = IPS.ToString();
                        }
                        else
                        {
                            //clear Gridview
                            TableKRS.Rows.Clear();
                            TableKRS.Clear();
                            GVKHS.DataSource = TableKRS;
                            GVKHS.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LbNPM.Text = "";
                this.LbNama.Text = "";
                this.LbSem.Text = "";

                DataTable TblClear = new DataTable();
                TblClear.Rows.Clear();
                TblClear.Clear();
                GVKHS.DataSource = TblClear;
                GVKHS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        // hitung Jumlah SKS dan IP Semester
        int TotalSKS = 0;
        decimal TotalNilai = 0;
        protected void GVKHS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int SKS = Convert.ToInt32(e.Row.Cells[2].Text);
                    TotalSKS += SKS;
                    this._TotalSKS = TotalSKS;

                    if (e.Row.Cells[4].Text != "&nbsp;")
                    {

                        decimal Nilai = Convert.ToDecimal(e.Row.Cells[4].Text);
                        TotalNilai += Nilai;
                        this._TotalNilai = TotalNilai;
                    }
                    else
                    {
                        decimal Nilai = 0;
                        TotalNilai += Nilai;
                        this._TotalNilai = TotalNilai;
                    }
                }
            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error in GVKHS Row Data Baound');", true);
            }
        }
    }
}