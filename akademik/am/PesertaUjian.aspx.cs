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
    //public partial class WebForm10 : System.Web.UI.Page
    public partial class WebForm10 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelProdi.Enabled = false;
                this.PanelProdi.Visible = false;

                this.PanelJadwalUjian.Enabled = false;
                this.PanelJadwalUjian.Visible = false;

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

                    this.DLProgramStudi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProgramStudi.DataTextField = "prog_study";
                    this.DLProgramStudi.DataValueField = "id_prog_study";
                    this.DLProgramStudi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProgramStudi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        //------------------- Identifikasi Mahasiswa ------------------ //

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
        public string Id_Prodi
        {
            get { return this.LbIdProdi.Text; }
        }
        public string Prodi
        {
            get { return this.LbProdi.Text; }
        }
        public string NIDN
        {
            get { return this.LbNIDN.Text; }
        }
        public string Kode_Makul
        {
            get { return this.LbKdMakul.Text; }
        }
        public string Makul
        {
            get { return this.LbMakul.Text; }
        }
        public string JenisUjian
        {
            get { return this.DLUjian.SelectedItem.Text; }
        }
        public string Jadwal
        {
            get { return this.LbJadwal.Text; }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.LbKdMakul.Text == "" || this.LbKelas.Text == "" || this.LbNIDN.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jadwal Kuliah');", true);
                return;
            }
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Ujian');", true);
                return;
            }

            //this.LbUjian.Text = this.DLUjian.SelectedItem.Text;

            Server.Transfer("~/am/CetakPesertaUjian.aspx");
        }

        protected void BtnCetak_Click(object sender, EventArgs e)
        {

        }

        protected void DLProgramStudi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hide panel
            //PanelJadwalUjian.Enabled = false;
            //PanelJadwalUjian.Visible = false;

            //form validation
            if (this.DLTahun.SelectedValue == "Tahun")
            {
                this.DLProgramStudi.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedValue == "Semester")
            {
                this.DLProgramStudi.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.DLProgramStudi.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Ujian');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //// ----------------- Jadwal UTS ------------------------ ////
                if (this.DLUjian.SelectedValue == "uts")
                {
                    //----- Read Jadwal ----
                    con.Open();
                    SqlCommand CmdJadwalUTS = new SqlCommand("SpJadwalUTS2", con);
                    CmdJadwalUTS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwalUTS.Parameters.AddWithValue("@id_prodi", this.DLProgramStudi.SelectedValue);
                    CmdJadwalUTS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                    DataTable TableJadwalUTS = new DataTable();
                    TableJadwalUTS.Columns.Add("Key");
                    TableJadwalUTS.Columns.Add("Kode");
                    TableJadwalUTS.Columns.Add("Mata Kuliah");
                    TableJadwalUTS.Columns.Add("NIDN");
                    TableJadwalUTS.Columns.Add("Dosen");
                    TableJadwalUTS.Columns.Add("Kelas");
                    TableJadwalUTS.Columns.Add("Hari");
                    TableJadwalUTS.Columns.Add("Tanggal");
                    TableJadwalUTS.Columns.Add("Mulai");
                    TableJadwalUTS.Columns.Add("Selesai");
                    TableJadwalUTS.Columns.Add("Ruang");
                    TableJadwalUTS.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwalUTS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelProdi.Enabled = true;
                            this.PanelProdi.Visible = true;

                            this.LbIdProdi.Text = this.DLProgramStudi.SelectedValue;
                            this.LbProdi.Text = this.DLProgramStudi.SelectedItem.Text;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwalUTS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari_uts"];
                                if (rdr["tgl_uts"] == DBNull.Value)
                                {

                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uts"]);
                                    datarow["Tanggal"] = TglUjian.ToString("dd-MMMM-yyyy");
                                }
                                datarow["Mulai"] = rdr["jam_mulai_uts"];
                                datarow["Selesai"] = rdr["jam_sls_uts"];
                                datarow["Ruang"] = rdr["ruang_uts"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwalUTS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwalUTS;
                            this.GVJadwal.DataBind();
                        }
                        else
                        {
                            this.DLProgramStudi.SelectedIndex = 0;

                            //clear Gridview
                            TableJadwalUTS.Rows.Clear();
                            TableJadwalUTS.Clear();
                            GVJadwal.DataSource = TableJadwalUTS;
                            GVJadwal.DataBind();

                            this.PanelProdi.Enabled = false;
                            this.PanelProdi.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Belum Ada');", true);
                        }
                    }
                }
                //// ----------------- Jadwal UAS ------------------------ ////
                else if (this.DLUjian.SelectedValue == "uas")
                {
                    //----- Read Jadwal ----
                    con.Open();
                    SqlCommand CmdJadwalUTS = new SqlCommand("SpJadwalUAS2", con);
                    CmdJadwalUTS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwalUTS.Parameters.AddWithValue("@id_prodi", this.DLProgramStudi.SelectedValue);
                    CmdJadwalUTS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                    DataTable TableJadwalUTS = new DataTable();
                    TableJadwalUTS.Columns.Add("Key");
                    TableJadwalUTS.Columns.Add("Kode");
                    TableJadwalUTS.Columns.Add("Mata Kuliah");
                    TableJadwalUTS.Columns.Add("NIDN");
                    TableJadwalUTS.Columns.Add("Dosen");
                    TableJadwalUTS.Columns.Add("Kelas");
                    TableJadwalUTS.Columns.Add("Hari");
                    TableJadwalUTS.Columns.Add("Tanggal");
                    TableJadwalUTS.Columns.Add("Mulai");
                    TableJadwalUTS.Columns.Add("Selesai");
                    TableJadwalUTS.Columns.Add("Ruang");
                    TableJadwalUTS.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwalUTS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelProdi.Enabled = true;
                            this.PanelProdi.Visible = true;

                            this.LbIdProdi.Text = this.DLProgramStudi.SelectedValue;
                            this.LbProdi.Text = this.DLProgramStudi.SelectedItem.Text;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwalUTS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari_uas"];
                                if (rdr["tgl_uas"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uas"]);
                                    datarow["Tanggal"] = TglUjian.ToString("dd-MMMM-yyyy");
                                }
                                datarow["Mulai"] = rdr["jam_mulai_uas"];
                                datarow["Selesai"] = rdr["jam_sls_uas"];
                                datarow["Ruang"] = rdr["ruang_uas"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwalUTS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwalUTS;
                            this.GVJadwal.DataBind();
                        }
                        else
                        {
                            this.DLProgramStudi.SelectedIndex = 0;

                            //clear Gridview
                            TableJadwalUTS.Rows.Clear();
                            TableJadwalUTS.Clear();
                            GVJadwal.DataSource = TableJadwalUTS;
                            GVJadwal.DataBind();

                            this.PanelProdi.Enabled = false;
                            this.PanelProdi.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Belum Ada');", true);
                        }
                    }
                }
            }
        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void BtnLihat_Click(object sender, EventArgs e)
        {

        }

        //button cetak ...
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Ujian');", true);
                return;
            }

            // hitung checkbox selected
            int cnt = 0;
            int rowchecked = 0;
            for (int i = 0; i < this.GVJadwal.Rows.Count; i++)
            {
                CheckBox CB = (CheckBox)this.GVJadwal.Rows[i].FindControl("CBSelect");
                if (CB.Checked == true)
                {
                    cnt += 1;
                    rowchecked = i;
                }
            }
            // checkbox selected
            if (cnt != 1)
            {
                //client message belum pilih check list.....
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Salah Satu Jadwal');", true);
                //ScriptManager.RegisterStartupScript((Control)this.BtnEditSKS, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
                return;
            }
            else
            {
                for (int i = 0; i < this.GVJadwal.Rows.Count; i++)
                {
                    CheckBox CB = (CheckBox)this.GVJadwal.Rows[i].FindControl("CBSelect");
                    if (CB.Checked == true)
                    {
                        //jadwal validation
                        if (this.LbKdMakul.Text == "&nbsp;" || this.LbKelas.Text == "&nbsp;" || this.LbNIDN.Text == "&nbsp;")
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jadwal Kuliah');", true);
                            continue;
                        }

                        //select Kode dan Makul
                        this.LbIdProdi.Text = this.DLProgramStudi.SelectedValue;
                        this.LbProdi.Text = this.DLProgramStudi.SelectedItem.Text;
                        this.LbKdMakul.Text = this.GVJadwal.Rows[i].Cells[2].Text;
                        this.LbMakul.Text = this.GVJadwal.Rows[i].Cells[3].Text;
                        this.LbNIDN.Text = this.GVJadwal.Rows[i].Cells[4].Text;
                        this.LbDosen.Text = this.GVJadwal.Rows[i].Cells[5].Text;
                        this.LbKelas.Text = this.GVJadwal.Rows[i].Cells[6].Text;
                        this.LbJadwal.Text = this.GVJadwal.Rows[i].Cells[7].Text + ", " + this.GVJadwal.Rows[i].Cells[8].Text + "," + this.GVJadwal.Rows[i].Cells[9].Text + "-" +
                                            this.GVJadwal.Rows[i].Cells[10].Text + ", Ruang " + this.GVJadwal.Rows[i].Cells[11].Text;
                    }
                }
                this.DLTahun.SelectedItem.Text = this.DLTahun.SelectedValue;
                this.DLSemester.SelectedItem.Text = this.DLSemester.SelectedValue;
                this.DLUjian.SelectedItem.Text = this.DLUjian.SelectedValue;
                this.DLProgramStudi.SelectedItem.Text = this.DLProgramStudi.SelectedValue;

                Server.Transfer("~/am/CetakPesertaUjian.aspx");
            }
        }
    }
}