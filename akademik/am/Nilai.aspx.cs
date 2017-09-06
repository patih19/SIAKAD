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
    //public partial class WebForm7 : System.Web.UI.Page
    public partial class WebForm7 : Bak_Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMakul.Enabled = false;
                this.PanelMakul.Visible = false;

                this.PanelPeserta.Enabled = false;
                this.PanelPeserta.Visible = false;
            }
        }

        protected void DLProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //form validation
            if (this.DLTahun.SelectedValue == "Tahun")
            {
                this.DLProdi.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedValue == "Semester")
            {
                this.DLProdi.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            this.PanelPeserta.Enabled = false;
            this.PanelPeserta.Visible = false;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpListJadwal3", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.DLProdi.SelectedValue);
                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");
                TableMakul.Columns.Add("NIDN");
                TableMakul.Columns.Add("Dosen");
                TableMakul.Columns.Add("Kelas");
                TableMakul.Columns.Add("Hari");
                TableMakul.Columns.Add("Mulai");
                TableMakul.Columns.Add("Selesai");
                TableMakul.Columns.Add("Ruang");
                TableMakul.Columns.Add("Jenis Kelas");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelMakul.Enabled = true;
                        this.PanelMakul.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["NIDN"] = rdr["nidn"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Hari"] = rdr["hari"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVMakul.DataSource = TableMakul;
                        this.GVMakul.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVMakul.DataSource = TableMakul;
                        GVMakul.DataBind();

                        this.PanelMakul.Enabled = false;
                        this.PanelMakul.Visible = false;
                    }
                }

                this.LbDosen.Text = "";
                this.LbIdProdi.Text = "";
                this.LbKdMakul.Text = "";
                this.LbKelas.Text = "";
                this.LbMakul.Text = "";
                this.LbNIDN.Text = "";
                this.LBSKS.Text = "";
            }
        }

        protected void CBSelect_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            //select Kode dan Makul
            this.LbIdProdi.Text = this.DLProdi.SelectedValue;
            this.LbKdMakul.Text = this.GVMakul.Rows[index].Cells[1].Text;
            this.LbMakul.Text = this.GVMakul.Rows[index].Cells[2].Text;
            this.LBSKS.Text = this.GVMakul.Rows[index].Cells[3].Text;
            this.LbNIDN.Text = this.GVMakul.Rows[index].Cells[4].Text;
            this.LbDosen.Text = this.GVMakul.Rows[index].Cells[5].Text;
            this.LbKelas.Text = this.GVMakul.Rows[index].Cells[6].Text;

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBSelect");
                ch.Checked = false;
            }

            //label validation
            if (this.LbKdMakul.Text == "" || this.LbKelas.Text == "" || this.LbNIDN.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jadwal Kuliah');", true);
                return;
            }
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.LbIdProdi.Text);
                CmdMakul.Parameters.AddWithValue("@nidn", this.LbNIDN.Text);
                CmdMakul.Parameters.AddWithValue("@kode_makul", this.LbKdMakul.Text);
                CmdMakul.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("NPM");
                TableMakul.Columns.Add("Nama");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelPeserta.Enabled = true;
                        this.PanelPeserta.Visible = true;

                        //set label
                        this.MakulLb.Text = this.LbMakul.Text;
                        this.TahunLb.Text = this.DLTahun.SelectedItem.Text;
                        this.SemesterLb.Text = this.DLSemester.SelectedItem.Text;
                        this.DosenLb.Text = this.LbDosen.Text;

                        //set DLProdi
                        this.DLProdi.SelectedIndex = 0;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["NPM"] = rdr["npm"];
                            datarow["Nama"] = rdr["nama"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPeserta.DataSource = TableMakul;
                        this.GVPeserta.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        this.GVPeserta.DataSource = TableMakul;
                        this.GVPeserta.DataBind();

                        this.PanelPeserta.Enabled = false;
                        this.PanelPeserta.Visible = false;

                        return;
                    }

                    //hide panel makul
                    this.PanelMakul.Enabled = false;
                    this.PanelMakul.Visible = false;
                }

                //-- 3. loop Nilai yg sudah diambil berdasarkan npm, makul, semester
                for (int i = 0; i < this.GVPeserta.Rows.Count; i++)
                {
                    SqlCommand CmdChecked = new SqlCommand("SpGetNilai", con);
                    CmdChecked.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChecked.Parameters.AddWithValue("@npm", this.GVPeserta.Rows[i].Cells[0].Text);
                    CmdChecked.Parameters.AddWithValue("@kode_makul", this.LbKdMakul.Text);
                    CmdChecked.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    using (SqlDataReader rdrchecked = CmdChecked.ExecuteReader())
                    {
                        if (rdrchecked.HasRows)
                        {
                            while (rdrchecked.Read())
                            {
                                DropDownList DLNilai = (DropDownList)this.GVPeserta.Rows[i].FindControl("DLNilai");

                                if (rdrchecked["nilai"] == DBNull.Value)
                                {
                                    DLNilai.SelectedItem.Text = "Nilai";
                                }
                                else
                                {
                                    DLNilai.SelectedItem.Text = rdrchecked["nilai"].ToString();
                                }
                            }
                        }
                    }
                }
            }

            this.PanelPeserta.Enabled = true;
            this.PanelPeserta.Visible = true;
        }

        protected void GVPeserta_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVPeserta.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //// get row index
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;

            // Insert to Nilai to DB
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    for (int i = 0; i < this.GVPeserta.Rows.Count; i++)
                    {
                        SqlCommand CmdNilai = new SqlCommand("SpInNilai", con);
                        CmdNilai.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdNilai.Transaction = trans;

                        //----Parameter Nilai
                        string npm = this.GVPeserta.Rows[i].Cells[0].Text;

                        //----get textbox text
                        DropDownList DL = (DropDownList)this.GVPeserta.Rows[i].FindControl("DLNilai");

                        //----Nilai validation
                        if (DL.SelectedValue == "Nilai")
                        {
                            //continue;

                            SqlCommand CmdDel = new SqlCommand("SpInNilai", con);
                            CmdDel.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdDel.Transaction = trans;
                        }
                        string Nilai = DL.SelectedItem.Text;

                        //-----Parameter Nilai-----
                        CmdNilai.Parameters.AddWithValue("@npm", npm);
                        CmdNilai.Parameters.AddWithValue("@kode_makul", this.LbKdMakul.Text);
                        CmdNilai.Parameters.AddWithValue("@sks", Convert.ToInt32(this.LBSKS.Text));
                        CmdNilai.Parameters.AddWithValue("@nilai", Nilai);
                        CmdNilai.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CmdNilai.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    trans.Rollback();
                    trans.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }
    }
}