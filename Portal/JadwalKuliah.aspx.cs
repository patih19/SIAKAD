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
    //public partial class WebForm5 : System.Web.UI.Page
    public partial class WebForm5 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Redirect("~/JadwalKuliah3.aspx", false);

                this.LbJadwalResult.Text = "";

                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;

                this.PanelEditJadwal.Enabled = false;
                this.PanelEditJadwal.Visible = false;

                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;


                //// ---------------------------
                //if (!Page.IsPostBack)
                //{
                //    Page Lastpage = (Page)Context.Handler;
                //    if (Lastpage is WebForm2)
                //    {
                //        this.DLTahun.SelectedItem.Text = ((WebForm2)Lastpage).Nama;
                //        this.DlSemester.SelectedItem.Text = ((WebForm2)Lastpage).NoPendaftar;
                //    }
                //}

                //this.DLTahun2.Attributes.Add("disabled", "disabled");
                //this.DLSemester2.Attributes.Add("disabled", "disabled");

                //this.DLProdi.SelectedValue = Session["level"].ToString();
                //this.LbProgramStudi.Text = Session["level"].ToString();
            }
        }

        protected void BtnJadwal_Click(object sender, EventArgs e)
        {
            //set label thn & semester
            this.LbSmstr.Text = "";
            this.LbThn.Text = "";

            //form validation
            //if (this.DLProdi.SelectedItem.Text == "Program Studi")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
            //    return;
            //}

            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }

            if (this.DlSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand(@"
	            SELECT     bak_jadwal.no_jadwal, bak_jadwal.id_prog_study, bak_makul.kode_makul, bak_makul.makul, bak_makul.sks, bak_jadwal.quota, bak_dosen.nama, bak_jadwal.kelas, bak_jadwal.hari, 
						              bak_jadwal.jenis_kelas, bak_dosen.nidn, bak_jadwal.jm_awal_kuliah, bak_jadwal.jm_akhir_kuliah, bak_ruang.nm_ruang
	            FROM         bak_jadwal INNER JOIN
						              bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN
						              bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN
						              bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN
						              bak_ruang ON bak_jadwal.id_rng_kuliah = bak_ruang.no
	            WHERE     (bak_jadwal.id_prog_study = @id_prodi) AND (bak_jadwal.semester = @semester) 
	            ORDER BY bak_jadwal.no_jadwal 
                ", con);
                CmdJadwal.CommandType = System.Data.CommandType.Text;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("Key");
                TableJadwal.Columns.Add("Kode");
                TableJadwal.Columns.Add("Mata Kuliah");
                TableJadwal.Columns.Add("SKS");
                TableJadwal.Columns.Add("Quota");
                TableJadwal.Columns.Add("Dosen");
                TableJadwal.Columns.Add("Kelas");
                TableJadwal.Columns.Add("Hari");
                TableJadwal.Columns.Add("Mulai");
                TableJadwal.Columns.Add("Selesai");
                TableJadwal.Columns.Add("Ruang");
                TableJadwal.Columns.Add("Jenis Kelas");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbSmstr.Text = this.DlSemester.SelectedItem.Text;
                        this.LbThn.Text = this.DLTahun.SelectedItem.Text;

                        this.LbJadwalResult.Text = "";

                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["Key"] = rdr["no_jadwal"];
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["Quota"] = rdr["quota"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Hari"] = rdr["hari"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJadwal.DataSource = TableJadwal;
                        this.GVJadwal.DataBind();

                        // hide panel Edit Mata Kuliah
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Jadwal Belum Ada ";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        // hide panel Edit Mata Kuliah
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVJadwal.DataSource = TableJadwal;
                        GVJadwal.DataBind();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddJadwalKuliah.aspx");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            //set lb no jadwal
            this.lbno_jadwal.Text = this.GVJadwal.Rows[index].Cells[3].Text;

            //hide panel jadwal
            this.PanelJadwal.Enabled = false;
            this.PanelJadwal.Visible = false;

            //unhide panel edit jadwal
            this.PanelEditJadwal.Enabled = true;
            this.PanelEditJadwal.Visible = true;

            //read record you want to display
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();

                try
                {
                    // -- Cek Masa INPUT NILAI
                    // -- Edit Jadwal Kuliah Tidak Diperbolehkan Pada Saat Masa Input NILAI --

                    SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                    CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DlSemester.SelectedValue);
                    CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "Nilai");

                    SqlParameter Status = new SqlParameter();
                    Status.ParameterName = "@output";
                    Status.SqlDbType = System.Data.SqlDbType.VarChar;
                    Status.Size = 20;
                    Status.Direction = System.Data.ParameterDirection.Output;
                    CmdCekMasa.Parameters.Add(Status);

                    CmdCekMasa.ExecuteNonQuery();

                    if (Status.Value.ToString() == "IN")
                    {
                        con.Close();
                        con.Dispose();

                        //hide panel jadwal
                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        //unhide panel edit jadwal
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pada Masa Input Nilai Edit Jadwal SUDAH DITOLAK ... ');", true);
                        return;
                    }


                    // --- List Jadwal ---- //
                    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal2", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[3].Text);

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this.LbProdi.Text = rdr["id_prog_study"].ToString();
                                this.LbKodeMakul.Text = rdr["kode_makul"].ToString();
                                this.LbMakul.Text = rdr["makul"].ToString();
                                this.LbNidn.Text = rdr["nidn"].ToString();
                                this.LbDosen.Text = rdr["nama"].ToString();
                                this.DLKelas.SelectedItem.Text = rdr["kelas"].ToString();
                                this.DLHari.SelectedItem.Text = rdr["hari"].ToString();
                                this.TbMulai.Text = rdr["jam_mulai"].ToString();
                                this.TbSelesai.Text = rdr["jam_selesai"].ToString();
                                this.TbRuang.Text = rdr["ruang"].ToString();
                                this.DLJenisKelas.SelectedItem.Text = rdr["jenis_kelas"].ToString();
                                this.TbQuota.Text = rdr["quota"].ToString();
                                // ----------- semester ----------------
                                string sms = rdr["semester"].ToString();
                                //this.DLTahun2.SelectedValue = sms.Substring(0, 4);
                                //this.DLSemester2.SelectedValue = sms.Substring(4, 1);
                            }
                        }
                        else
                        {

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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            int no_jadwal = Convert.ToInt32(this.GVJadwal.Rows[index].Cells[3].Text);

            //delete jadwal kuliah
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                try
                {
                    con.Open();
                    SqlCommand CmdDelJadwal = new SqlCommand("SpDelJadwal", con);
                    CmdDelJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDelJadwal.Parameters.AddWithValue("@no_jadwal", no_jadwal);
                    CmdDelJadwal.Parameters.AddWithValue("@semester", this.LbThn.Text + this.LbSmstr.Text);

                    CmdDelJadwal.ExecuteNonQuery();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Delete Berhasil ...');", true);

                    BtnJadwal_Click(this, null);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnMakul_Click(object sender, ImageClickEventArgs e)
        {
            this.PanelDosen.Enabled = false;
            this.PanelDosen.Visible = false;
        }

        protected void DLProdiDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //Form Validation
            if (this.lbno_jadwal.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('id jadwal does not set correctly');", true);
                return;
            }

            if (this.LbProdi.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.LbKodeMakul.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }
            if (this.LbNidn.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Dosen Pengampu');", true);
                return;
            }
            if (this.DLKelas.SelectedItem.Text == "Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Pembagian Kelas Mahasiswa');", true);
                return;
            }
            if (this.DLHari.SelectedItem.Text == "Hari")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Hari Kuliah');", true);
                return;
            }
            if (this.TbMulai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Mulai Perkuliahan');", true);
                return;
            }
            if (this.TbSelesai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Selesai Perkuliahan');", true);
                return;
            }
            if (this.TbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Perkuliahan');", true);
                return;
            }
            if (this.DLJenisKelas.SelectedItem.Text == "Jenis Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas Mahasiswa');", true);
                return;
            }
            if (this.TbQuota.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Quota / Kapasitas Mahasiswa');", true);
                return;
            }

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpUpJadwalKuliah", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@no_jadwal", this.lbno_jadwal.Text);
                    CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.LbThn.Text + this.LbSmstr.Text);

                    CmdInJadwal.ExecuteNonQuery();

                    this.lbno_jadwal.Text = "";

                    BtnJadwal_Click(this, null);

                    this.PanelEditJadwal.Enabled = false;
                    this.PanelEditJadwal.Visible = false;

                    // ------------------ Clear Data -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    //DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    TbRuang.Text = "";

                    DLJenisKelas.SelectedIndex = 0;
                    TbQuota.Text = "";
                    DLTahun.SelectedIndex = 0;
                    //DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void GVJadwal_PreRender(object sender, EventArgs e)
        {
            if (this.GVJadwal.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVJadwal.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVJadwal.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}