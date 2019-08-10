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
    //public partial class WebForm24 : System.Web.UI.Page
    public partial class WebForm24 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LbJadwalResult.Text = "";

                PanelJamMengajar.Enabled = false;
                PanelJamMengajar.Visible = false;

                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;

                this.PanelEditJadwal.Enabled = false;
                this.PanelEditJadwal.Visible = false;

                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;

                this.PanelRuangAktif.Enabled = false;
                this.PanelRuangAktif.Visible = false;

                this.DLKelas.Attributes.Add("disabled", "disabled");

                this.DLHari.SelectedIndex = 0;

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

                PopulateProdi();
                PopulateProdi2();
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

                    this.DLProdiDosen.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdiDosen.DataTextField = "prog_study";
                    this.DLProdiDosen.DataValueField = "id_prog_study";
                    this.DLProdiDosen.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdiDosen.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

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

                    this.DLRuangProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLRuangProdi.DataTextField = "prog_study";
                    this.DLRuangProdi.DataValueField = "id_prog_study";
                    this.DLRuangProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLRuangProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnJadwal_Click(object sender, EventArgs e)
        {
            //set session semester
            if (this.Session["Semester"] != null)
            {
                this.Session["Semester"] = (object)null;
                this.Session.Remove("Semester");

                this.Session["Semester"] = this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text;
            }
            else
            {
                this.Session["Semester"] = this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text;
            }

            //validation
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
                SqlCommand CmdJadwal = new SqlCommand("SpListJadwal3", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("Key");
                TableJadwal.Columns.Add("Kode");
                TableJadwal.Columns.Add("Mata Kuliah");
                TableJadwal.Columns.Add("SKS");
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

                        //this.BtnNewJadwal.Enabled = false;
                        //this.BtnNewJadwal.Visible = false;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["Key"] = rdr["no_jadwal"];
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
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

                        // hide panel di dalam edit perkuliahan
                        this.PanelDetailRuang.Enabled =false;
                        this.PanelDetailRuang.Visible = false;
                        this.PanelDetailDosen.Enabled = false;
                        this.PanelDetailDosen.Visible = false;
                        this.PanelRuangAktif.Enabled = false;
                        this.PanelRuangAktif.Visible = false;
                        this.DLProdiDosen.SelectedIndex = 0;
                        this.DLRuangProdi.SelectedIndex = 0;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Jadwal Belum Ada ";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        // hide panel Edit Mata Kuliah
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;

                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        //this.BtnNewJadwal.Enabled = true;
                        //this.BtnNewJadwal.Visible = true;

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
            //Server.Transfer("~/AddJadwalKuliah3.aspx");
            Response.Redirect("~/AddJadwalKuliah3.aspx");
            
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Menu Edit Sedang Dalam Perbaikan');", true);
            //return;

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            //set lb no jadwal
            this.lbno_jadwal.Text = this.GVJadwal.Rows[index].Cells[4].Text;

            DLHari.SelectedItem.Text = this.GVJadwal.Rows[index].Cells[10].Text;

            //hide panel jadwal
            this.PanelJadwal.Enabled = false;
            this.PanelJadwal.Visible = false;

            //unhide panel edit jadwal
            this.PanelEditJadwal.Enabled = true;
            this.PanelEditJadwal.Visible = true;

            this.PanelRuangAktif.Visible = false;
            this.PanelJamMengajar.Visible = false;

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
                    CmdCekMasa.Parameters.AddWithValue("@jenjang", this.Session["jenjang"].ToString());
                    
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
                    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal2B", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[4].Text);

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
                                //this.DLKelas.SelectedItem.Text = rdr["kelas"].ToString();
                                this.DLHari.SelectedItem.Text = rdr["hari"].ToString();

                                TimeSpan Mulai = (TimeSpan)rdr["jm_awal_kuliah"];
                                this.TbMulai.Text = Mulai.ToString(@"hh\:mm");
                                //this.TbMulai.Text = rdr["jm_awal_kuliah"].ToString();

                                TimeSpan Selesai = (TimeSpan)rdr["jm_akhir_kuliah"];
                                this.TbSelesai.Text = Selesai.ToString(@"hh\:mm");
                               //this.TbSelesai.Text = rdr["jm_akhir_kuliah"].ToString();

                                this.LbRuang.Text = rdr["nm_ruang"].ToString();
                                this.LbNo.Text = rdr["no"].ToString();
                                //this.DLJenisKelas.SelectedItem.Text = rdr["jenis_kelas"].ToString();
                                //this.TbQuota.Text = rdr["quota"].ToString();
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

            int no_jadwal = Convert.ToInt32(this.GVJadwal.Rows[index].Cells[4].Text);

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
            //if (this.DLKelas.SelectedItem.Text == "Kelas")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Pembagian Kelas Mahasiswa');", true);
            //    return;
            //}
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
            if (this.LbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tentukan Ruang Perkuliahan');", true);
                return;
            }
            if (this.LbNo.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tentukan Ruang Perkuliahan');", true);
                return;
            }
            //if (this.DLJenisKelas.SelectedItem.Text == "Jenis Kelas")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas Mahasiswa');", true);
            //    return;
            //}
            //if (this.TbQuota.Text == "")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Quota / Kapasitas Mahasiswa');", true);
            //    return;
            //}

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpEditJadwalKuliah", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@NomorJadwal", this.lbno_jadwal.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@NewNidn", this.LbNidn.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@NewKelas", this.DLKelas.SelectedItem.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@NewHari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@NewJam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@NewJam_selesai", this.TbSelesai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@NewId_ruang", this.LbNo.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@NewQuota", this.TbQuota.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@semester", this.LbThn.Text + this.LbSmstr.Text);

                    CmdInJadwal.ExecuteNonQuery();

                    this.lbno_jadwal.Text = "";

                    this.PanelEditJadwal.Enabled = false;
                    this.PanelEditJadwal.Visible = false;

                    this.PanelJadwal.Enabled = true;
                    this.PanelJadwal.Visible = true;

                    BtnJadwal_Click(this, null);


                    // ------------------ Clear Data -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    //DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    //DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    //TbRuang.Text = "";
                    this.LbRuang.Text = "";
                    this.LbNo.Text = "";

                    //DLJenisKelas.SelectedIndex = 0;
                    //TbQuota.Text = "";
                    DLTahun.SelectedIndex = 0;
                    //DLSemester.SelectedIndex = 0;

                    //if (this.Session["Semester"] != null)
                    //{
                    //    this.Session["Semester"] = (object)null;
                    //    this.Session.Remove("Semester");
                    //}


                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
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
            e.Row.Cells[0].Visible = false; //add jadwal
            e.Row.Cells[4].Visible = false; //no jadwal
            //e.Row.Cells[5].Visible = false; // kode makul

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // ------------------------ Nilai --------------------------
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // ------ Cek Sisa Quota -------
                        SqlCommand CmdCekMasa = new SqlCommand("SELECT no_jadwal,quota FROM bak_jadwal WHERE no_jadwal=@no_jadwal", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.Text;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[4].Text);

                        using (SqlDataReader rdr = CmdCekMasa.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    TextBox TBQuotaJadwal = (TextBox)e.Row.Cells[3].FindControl("TbQuota");
                                    TBQuotaJadwal.Text = rdr["quota"].ToString();
                                }

                            }
                            else
                            {
                                TextBox TBQuotaJadwal = (TextBox)e.Row.Cells[3].FindControl("TbQuota");
                                TBQuotaJadwal.Text = "";
                            }
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

            }

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

        protected void DLRuangProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SELECT * FROM  bak_ruang WHERE id_prog_study=@id_prodi", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.DLRuangProdi.SelectedValue);

                DataTable TableRuang = new DataTable();
                TableRuang.Columns.Add("No");
                TableRuang.Columns.Add("Ruang");
                TableRuang.Columns.Add("Kapasitas");


                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailRuang.Enabled = true;
                        this.PanelDetailRuang.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableRuang.NewRow();
                            datarow["No"] = rdr["no"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Kapasitas"] = rdr["kapasitas"];

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

                        this.PanelDetailRuang.Enabled = false;
                        this.PanelDetailRuang.Visible = false;
                    }
                }
            }
        }

        protected void GVRuang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //no ruang
        }

        protected void CbRuang_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVRuang.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVRuang.Rows[i].FindControl("CbRuang");
                if (ch.Checked == true)
                {
                    this.LbRuang.Text = this.GVRuang.Rows[i].Cells[2].Text;
                    this.LbNo.Text = this.GVRuang.Rows[i].Cells[1].Text;
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVRuang.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVRuang.Rows[i].FindControl("CbRuang");
                ch.Checked = false;
            }


            //Select Drop Down List to Default
            this.DLRuangProdi.SelectedIndex = 0;

            //hide panel
            this.PanelDetailRuang.Enabled = false;
            this.PanelDetailRuang.Visible = false;
        }

        protected void GVDosen_PreRender(object sender, EventArgs e)
        {
            if (this.GVDosen.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVDosen.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVDosen.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            this.DLHari.SelectedIndex = 0;

            this.PanelEditJadwal.Enabled = false;
            this.PanelEditJadwal.Visible = false;

            this.PanelJadwal.Visible = true;
            this.PanelJadwal.Enabled = true;

            Page_Load(this,e);


        }

        protected void BtnNewJadwal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddJadwalKuliah3.aspx");
        }

        protected void DLHari_SelectedIndexChanged(object sender, EventArgs e)
        {

            //--------- List Ruang Aktif -----------//
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdDosen = new SqlCommand("SELECT        bak_jadwal.no_jadwal, bak_jadwal.hari, bak_jadwal.jm_awal_kuliah, bak_jadwal.jm_akhir_kuliah, bak_prog_study.prog_study, bak_makul.makul, bak_ruang.nm_ruang " +
                                     "FROM            bak_jadwal INNER JOIN " +
                                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN " +
                                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN " +
                                                             "bak_ruang ON bak_jadwal.id_rng_kuliah = bak_ruang.no  " +
                                    "WHERE        (bak_jadwal.semester = @semester) AND (bak_jadwal.nidn = @nidn) AND (bak_jadwal.hari=@hari) " +
                                    "ORDER BY bak_jadwal.jm_awal_kuliah", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());
                CmdDosen.Parameters.AddWithValue("@nidn", this.LbNidn.Text.Trim());
                CmdDosen.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text.Trim());

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("Hari");
                TableDosen.Columns.Add("Mulai");
                TableDosen.Columns.Add("Selesai");
                TableDosen.Columns.Add("Ruang");
                TableDosen.Columns.Add("Makul");
                TableDosen.Columns.Add("Program Studi");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelJamMengajar.Enabled = true;
                        this.PanelJamMengajar.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["Hari"] = rdr["hari"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Makul"] = rdr["makul"];
                            datarow["Program Studi"] = rdr["prog_study"];

                            TableDosen.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJamMengajar.DataSource = TableDosen;
                        this.GVJamMengajar.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVJamMengajar.DataSource = TableDosen;
                        GVJamMengajar.DataBind();

                        PanelJamMengajar.Enabled = false;
                        PanelJamMengajar.Visible = false;
                    }
                }


                SqlCommand CmdRuang = new SqlCommand("SpRuangAktif", con);
                CmdRuang.CommandType = System.Data.CommandType.StoredProcedure;

                CmdRuang.Parameters.AddWithValue("@semester", this.LbThn.Text.Trim() + this.LbSmstr.Text.Trim());
                CmdRuang.Parameters.AddWithValue("@hari", this.DLHari.SelectedValue.Trim());
                CmdRuang.Parameters.AddWithValue("@idruang", this.LbNo.Text.Trim());

                DataTable TableRuang = new DataTable();
                TableRuang.Columns.Add("Hari");
                TableRuang.Columns.Add("Mulai");
                TableRuang.Columns.Add("Selesai");
                TableRuang.Columns.Add("Ruang");
                TableRuang.Columns.Add("Makul");

                TableRuang.Columns.Add("Jenis Kelas");
                TableRuang.Columns.Add("Program Studi");

                using (SqlDataReader rdr = CmdRuang.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        PanelRuangAktif.Enabled = true;
                        PanelRuangAktif.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableRuang.NewRow();
                            datarow["Hari"] = rdr["hari"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Makul"] = rdr["makul"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Program Studi"] = rdr["prog_study"];

                            TableRuang.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVRuangAktif.DataSource = TableRuang;
                        this.GVRuangAktif.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableRuang.Rows.Clear();
                        TableRuang.Clear();
                        GVRuangAktif.DataSource = TableRuang;
                        GVRuangAktif.DataBind();

                        PanelRuangAktif.Enabled = false;
                        PanelRuangAktif.Visible = false;
                    }
                }
            
            }
        }

        int Quota;
        protected void TbQuota_TextChanged(object sender, EventArgs e)
        {

            GridViewRow gvr = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox tb = (TextBox)gvr.FindControl("TbQuota");

            if (tb.Text != string.Empty)
            {
                try
                {
                    Quota = Convert.ToInt16(tb.Text);

                    if (Quota <= 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Quota Tidak Valid');", true);
                        return;
                    }
                }
                catch (Exception)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Quota Tidak Valid');", true);
                    return;
                }
            }

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                try
                {
                    SqlCommand CmdDosen = new SqlCommand(" "+
                        "DECLARE @JumlahPeserta INT "+
                        "DECLARE @NomorJadwal INT "+

                        "SELECT        @NomorJadwal = bak_jadwal.no_jadwal, @jumlahPeserta = COUNT(*) "+
                        "FROM            bak_jadwal INNER JOIN "+
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal "+
                        "WHERE dbo.bak_jadwal.no_jadwal=@no_jadwal "+
                        "GROUP BY bak_jadwal.no_jadwal "+

                        "IF (@quota <= @JumlahPeserta) "+
                        "BEGIN "+
                            "RAISERROR('Jumlah Peserta Saat Ini Melebihi Quota Diinput, PERMINTAAN DITOLAK !!!', 16, 10) "+
                            "RETURN "+
                        "END "+
                        "ELSE "+
                        "BEGIN "+
                            "UPDATE bak_jadwal SET quota = @quota where no_jadwal = @no_jadwal "+
                        "END "+
                        "", con);
                    CmdDosen.CommandType = System.Data.CommandType.Text;

                    CmdDosen.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[4].Text.Trim());
                    CmdDosen.Parameters.AddWithValue("@quota", Quota);

                    //TextBox TBQuotaJadwal = (TextBox)e.Row.Cells[3].FindControl("TbQuota");

                    CmdDosen.ExecuteNonQuery();

                    Page_Load(this, null);
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message.ToString());
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('"+ex.Message.ToString()+ "');", true);
                    return;
                }
            }
        }

    }
}