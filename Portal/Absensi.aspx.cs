using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Portal
{
    //public partial class WebForm1 : System.Web.UI.Page
    public partial class WebForm1 : Tu
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
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetNoStore();
            }
        }

        protected void BtnCetak_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/CetakPresensi.aspx");
        }

        protected void CBSelect_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            //select Kode dan Makul
            this.LbIdProdi.Text = this.Session["level"].ToString(); 
            this.LbKdMakul.Text = this.GVMakul.Rows[index].Cells[1].Text;
            this.LbMakul.Text = this.GVMakul.Rows[index].Cells[2].Text;
            this.LbNIDN.Text = this.GVMakul.Rows[index].Cells[4].Text;
            this.LbDosen.Text = this.GVMakul.Rows[index].Cells[5].Text;
            this.LbKelas.Text = this.GVMakul.Rows[index].Cells[6].Text;
            this.LbJadwal.Text = this.GVMakul.Rows[index].Cells[7].Text + "," + this.GVMakul.Rows[index].Cells[8].Text
                                + "-" + this.GVMakul.Rows[index].Cells[9].Text + "," + this.GVMakul.Rows[index].Cells[10].Text;

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBSelect");
                ch.Checked = false;
            }

            // set label prodi
            this.LbProdi.Text = this.Session["Prodi"].ToString();

            //Select Drop Down List to Default
            //this.DLProdi.SelectedIndex = 0;

            //hide panel
            //this.PanelMakul.Enabled = false;
            //this.PanelMakul.Visible = false;

            this.PanelProdi.Enabled = true;
            this.PanelProdi.Visible = true;

            this.PanelPeserta.Enabled = false;
            this.PanelPeserta.Visible = false;


            // -------------
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

        protected void DLSemester_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand CmdMakul = new SqlCommand("SpListJadwal3", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
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

                        this.PanelPeserta.Enabled = false;
                        this.PanelPeserta.Visible = false;

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

                        this.PanelPeserta.Enabled = false;
                        this.PanelPeserta.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }
        }

        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }

        protected void GVMakul_PreRender(object sender, EventArgs e)
        {
            if (this.GVMakul.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVMakul.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVMakul.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVMakul.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}