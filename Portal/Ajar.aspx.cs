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
    public partial class WebForm15 : Tu
    {
        private int NomorJadwal;
        public int NoJadwal
        {
            get { return NomorJadwal; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DLProdiDosen.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdiDosen.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));
            }
        }

        protected void DLProdiDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DlSemester.SelectedValue == "Semester")
            {
                this.DLProdiDosen.SelectedIndex = 0;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SpGetDosen", con);
                CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.DLProdiDosen.SelectedValue);

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("NIDN");
                TableDosen.Columns.Add("Nama");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailDosen.Enabled = true;
                        this.PanelDetailDosen.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["NIDN"] = rdr["nidn"];
                            datarow["Nama"] = rdr["nama"];

                            TableDosen.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVDosen.DataSource = TableDosen;
                        this.GVDosen.DataBind();

                        //clear GVAjar
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVAjar.DataSource = TableDosen;
                        GVAjar.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVDosen.DataSource = TableDosen;
                        GVDosen.DataBind();

                        this.PanelDetailDosen.Enabled = true;
                        this.PanelDetailDosen.Visible = true;
                    }
                }
            }
        }

        protected void CbDosen_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVDosen.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CbDosen");
                if (ch.Checked == true)
                {
                    this.LbDosen.Text = this.GVDosen.Rows[i].Cells[2].Text;
                    this.LbNidn.Text = this.GVDosen.Rows[i].Cells[1].Text;

                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand CmdJadwal = new SqlCommand("SPPesertaMakulDosen", con);
                        CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdJadwal.Parameters.AddWithValue("@dosen", this.GVDosen.Rows[i].Cells[2].Text);
                        CmdJadwal.Parameters.AddWithValue("@nidn", this.GVDosen.Rows[i].Cells[1].Text);
                        CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                        DataTable TableJadwal = new DataTable();
                        TableJadwal.Columns.Add("No Jadwal");
                        TableJadwal.Columns.Add("Kode");
                        TableJadwal.Columns.Add("Mata Kuliah");
                        TableJadwal.Columns.Add("Mengampu");
                        TableJadwal.Columns.Add("Kelas");
                        TableJadwal.Columns.Add("Jenis Kelas");
                        TableJadwal.Columns.Add("SKS");
                        TableJadwal.Columns.Add("Peserta");

                        using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow = TableJadwal.NewRow();
                                    datarow["No Jadwal"] = Convert.ToString(rdr["no_jadwal"]);
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["Mengampu"] = rdr["prog_study"];
                                    datarow["Kelas"] = rdr["kelas"];
                                    datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                                    datarow["SKS"] = rdr["sks"];
                                    datarow["Peserta"] = rdr["jumlah"];

                                    TableJadwal.Rows.Add(datarow);
                                }
                                //Fill Gridview
                                this.GVAjar.DataSource = TableJadwal;
                                this.GVAjar.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableJadwal.Rows.Clear();
                                TableJadwal.Clear();
                                GVAjar.DataSource = TableJadwal;
                                GVAjar.DataBind();

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Tidak Ditemukan');", true);
                            }
                        }
                    }
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVDosen.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CbDosen");
                ch.Checked = false;
            }

            //Select Drop Down List to Default
            this.DLProdiDosen.SelectedIndex = 0;

            //hide panel
            this.PanelDetailDosen.Enabled = false;
            this.PanelDetailDosen.Visible = false;
        }

        protected void LnkLihat_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            NomorJadwal = Convert.ToInt32(this.GVAjar.Rows[index].Cells[0].Text);

            Server.Transfer("~/ListNilai.aspx");
        }

        protected void GVAjar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVAjar.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        int TotalSKS = 0;
        int TotalPeserta = 0;
        protected void GVAjar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //no_jadwal

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // ---------- Hitung SKS dan Peserta -----------------
                int SKS = Convert.ToInt32(e.Row.Cells[6].Text);
                TotalSKS += SKS;

                int Peserta = Convert.ToInt32(e.Row.Cells[7].Text);
                TotalPeserta += Peserta;

                // ------------------------ Nilai --------------------------
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // ------ Cek Sisa Quota -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekNilai", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[0].Text);

                        SqlParameter Quota = new SqlParameter();
                        Quota.ParameterName = "@NotInput";
                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                        Quota.Size = 20;
                        Quota.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Quota);

                        CmdCekMasa.ExecuteNonQuery();
                        Label LbBlmInput = (Label)e.Row.Cells[1].FindControl("LbNotReady");

                        if (Quota.Value.ToString() != "0")
                        {
                            LbBlmInput.Text = "-" + Quota.Value.ToString();
                            LbBlmInput.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            LbBlmInput.ForeColor = System.Drawing.Color.Green;
                            LbBlmInput.Text = "Lengkap";
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah");
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah";
                e.Row.Cells[6].Text = TotalSKS.ToString();
                e.Row.Cells[7].Text = TotalPeserta.ToString();

            }
        }
    }
}