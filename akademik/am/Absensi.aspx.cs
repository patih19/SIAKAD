using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace akademik.am
{
    //public partial class WebForm3 : System.Web.UI.Page
    public partial class WebForm3 : Bak_staff
    {
        // Paremeter Absensi
        public string Kelas
        {
            get { return this.LbKelas.Text; }
        }
        public string Tahun
        {
            get { return this.DLTahun.SelectedItem.Text; }
        }
        public string Semester
        {
            get { return this.DLSemester.SelectedItem.Text; }
        }
        public string NIDN
        {
            get { return this.LbNIDN.Text; }
        }
        public string Prodi
        {
            get { return this.LbProdi.Text; }
        }
        public string Dosen
        {
            get { return this.LbDosen.Text; }
        }
        public string KodeMakul
        {
            get { return this.LbKdMakul.Text; }
        }
        public string Makul
        {
            get { return this.LbMakul.Text; }
        }
        public string IdProdi
        {
            get { return this.LbIdProdi.Text; }
        }
        public string jadwal
        {
            get { return this.LbJadwal.Text; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMakul.Enabled = false;
                this.PanelMakul.Visible = false;

                this.PanelPeserta.Enabled = false;
                this.PanelPeserta.Visible = false;

                PopulateProdi();
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetNoStore();
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

        protected void BtnCetak_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/am/CetakPresensi.aspx");
        }

        protected void DLProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //form validation
            if (this.DLTahun.SelectedValue == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedValue == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SPPesertaMakul2B", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);
                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("No Jadwal");
                TableMakul.Columns.Add("Kode Makul");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");
                TableMakul.Columns.Add("nidn");
                TableMakul.Columns.Add("Dosen");
                TableMakul.Columns.Add("Kelas");
                TableMakul.Columns.Add("Hari");
                TableMakul.Columns.Add("Mulai");
                TableMakul.Columns.Add("Selesai");
                TableMakul.Columns.Add("Ruang");
                TableMakul.Columns.Add("Jenis Kelas");
                TableMakul.Columns.Add("Quota");
                TableMakul.Columns.Add("Peserta");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelMakul.Enabled = true;
                        this.PanelMakul.Visible = true;

                        this.PanelPeserta.Enabled = false;
                        this.PanelPeserta.Visible = false;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["No Jadwal"] = rdr["no_jadwal"];
                            datarow["Kode Makul"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["nidn"] = rdr["nidn"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Hari"] = rdr["hari"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Quota"] = rdr["quota"];
                            datarow["Peserta"] = rdr["jumlah"];

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

                        this.PanelPeserta.Enabled = false;
                        this.PanelPeserta.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }
        }

        protected void CBSelect_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int rowIndex = gvRow.RowIndex;

            //Get the value of column from the DataKeys using the RowIndex.
            string KdMakul = GVMakul.DataKeys[rowIndex].Values[0].ToString();
            string NIDN = GVMakul.DataKeys[rowIndex].Values[1].ToString();
            string KELAS = GVMakul.DataKeys[rowIndex].Values[2].ToString();

            //select Kode dan Makul
            this.LbIdProdi.Text = this.DLProdi.SelectedValue;
            this.LbKdMakul.Text = KdMakul;
            this.LbMakul.Text = this.GVMakul.Rows[rowIndex].Cells[3].Text;
            this.LbNIDN.Text = NIDN;
            this.LbDosen.Text = this.GVMakul.Rows[rowIndex].Cells[6].Text;
            this.LbKelas.Text = this.GVMakul.Rows[rowIndex].Cells[7].Text;
            this.LbJadwal.Text = this.GVMakul.Rows[rowIndex].Cells[8].Text + "," + this.GVMakul.Rows[rowIndex].Cells[9].Text
                                + "-" + this.GVMakul.Rows[rowIndex].Cells[10].Text + "," + this.GVMakul.Rows[rowIndex].Cells[11].Text;

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBSelect");
                ch.Checked = false;
            }

            // set label prodi
            this.LbProdi.Text = this.DLProdi.SelectedItem.Text;

            //Select Drop Down List to Default
            this.DLProdi.SelectedIndex = 0;

            //hide panel
            this.PanelMakul.Enabled = false;
            this.PanelMakul.Visible = false;

            this.PanelProdi.Enabled = true;
            this.PanelProdi.Visible = true;

            this.PanelPeserta.Enabled = false;
            this.PanelPeserta.Visible = false;

            //form validation
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
                    }
                }
            }
        }

        protected void btnNewTab_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/am/CetakPesertaUjian.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/am/CetakPesertaUjian.aspx");
        }

        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //no jadwal
            e.Row.Cells[2].Visible = false; //kode makul
            e.Row.Cells[5].Visible = false; //nidn
        }
    }
}