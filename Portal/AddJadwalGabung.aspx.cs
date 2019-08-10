using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class AddJadwalGabung : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.Write(this.Session["Semester"].ToString().Trim());

                this.DLProdiMakul.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdiMakul.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));

                this.DLProdiMakul2.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdiMakul2.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));

                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;

                this.PanelDetailMakul.Enabled = false;
                this.PanelDetailMakul.Visible = false;

                this.PanelDetailMakul2.Enabled = false;
                this.PanelDetailMakul2.Visible = false;

                this.PanelDetailRuang.Enabled = false;
                this.PanelDetailRuang.Visible = false;

                this.PanelUpdate.Enabled = false;
                this.PanelUpdate.Visible = false;

                this.PanelListJadwal.Enabled = false;
                this.PanelListJadwal.Visible = false;

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

        protected void DLProdiMakul_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpGetMakul", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.DLProdiMakul.SelectedValue);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("KURIKULUM");
                TableMakul.Columns.Add("SKS");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailMakul.Enabled = true;
                        this.PanelDetailMakul.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["KURIKULUM"] = rdr["kurikulum"];
                            datarow["SKS"] = rdr["sks"];

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

                        this.PanelDetailMakul.Enabled = false;
                        this.PanelDetailMakul.Visible = false;
                    }
                }
            }
        }

        protected void CBMakul_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBMakul");
                if (ch.Checked == true)
                {

                    this.LbKodeMakul.Text = this.GVMakul.Rows[i].Cells[1].Text;
                    this.LbMakul.Text = this.GVMakul.Rows[i].Cells[2].Text;
                    //Response.Write("DataKeyName: " + GVMakul.DataKeys[i].Value.ToString());
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBMakul");
                ch.Checked = false;
            }

            // label prodi
            this.LbProdi.Text = this.DLProdiMakul.SelectedValue;

            //Select Drop Down List to Default
            this.DLProdiMakul.SelectedIndex = 0;

            //hide panel
            this.PanelDetailMakul.Enabled = false;
            this.PanelDetailMakul.Visible = false;
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

                        this.PanelDetailDosen.Enabled = false;
                        this.PanelDetailDosen.Visible = false;
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

                    this.LbDosen2.Text = this.GVDosen.Rows[i].Cells[2].Text;
                    this.LbNidn2.Text = this.GVDosen.Rows[i].Cells[1].Text;
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
            //Form Validation MAKUL PERTAMA
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
            if (this.LbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Ruang Perkuliahan');", true);
                return;
            }
            if (this.TbQuota.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Quota / Kapasitas Mahasiswa');", true);
                return;
            }
            if (this.DLJenisKelas.SelectedItem.Text == "Jenis Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas');", true);
                return;
            }

            //Form Validation MAKUL KEDUA
            if (this.LbProdi2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.LbKodeMakul2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }
            if (this.LbNidn2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Dosen Pengampu');", true);
                return;
            }
            if (this.LbKelas.Text == "Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Pembagian Kelas Mahasiswa');", true);
                return;
            }
            if (this.LbHari.Text == "Hari")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Hari Kuliah');", true);
                return;
            }
            if (this.LbMulai.Text == "00:00")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Mulai Perkuliahan');", true);
                return;
            }
            if (this.LbSelesai.Text == "00:00")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Selesai Perkuliahan');", true);
                return;
            }
            if (this.LbRuang2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Ruang Perkuliahan');", true);
                return;
            }
            if (this.TbQuota2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Quota / Kapasitas Mahasiswa');", true);
                return;
            }
            if (this.DLJenisKelas2.SelectedItem.Text == "Jenis Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas');", true);
                return;
            }

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand(@"
                    DECLARE @NoGabung INT = 0
                    DECLARE @NoJadwal BIGINT
                    SELECT TOP 1 @NoJadwal=no_jadwal,@NoGabung=gabung FROM dbo.bak_jadwal WHERE gabung IS NOT NULL AND semester=@sem AND id_prog_study=@IdProgStudy ORDER BY gabung DESC
                    IF @@ROWCOUNT = 0
                    BEGIN
	                    SET @NoGabung =1
	
	                    DECLARE @FirstKurikulum1 VARCHAR(4)
	                    DECLARE @SecondKurikulum1 VARCHAR(4)

	                    SELECT        @FirstKurikulum1=bak_makul.kurikulum
	                    FROM            bak_kurikulum INNER JOIN
							                     bak_makul ON bak_kurikulum.id_kurikulum = bak_makul.id_kurikulum INNER JOIN
							                     bak_jadwal ON bak_makul.kode_makul = bak_jadwal.kode_makul
	                    WHERE dbo.bak_jadwal.id_prog_study =  '63201' AND dbo.bak_makul.kode_makul = 'IN103305'

	                    SELECT        @SecondKurikulum1=bak_makul.kurikulum
	                    FROM            bak_kurikulum INNER JOIN
							                     bak_makul ON bak_kurikulum.id_kurikulum = bak_makul.id_kurikulum INNER JOIN
							                     bak_jadwal ON bak_makul.kode_makul = bak_jadwal.kode_makul
	                    WHERE dbo.bak_jadwal.id_prog_study =  @id_prodi2 AND dbo.bak_makul.kode_makul = @kode_makul2

	                    IF @FirstKurikulum1 = @SecondKurikulum1
	                    BEGIN
		                    RAISERROR ('KURIKULUM HARUS BERBEDA', 16,10)
		                    RETURN
	                    END

	                    INSERT INTO dbo.bak_jadwal
	                    ( id_prog_study ,
		                    kode_makul ,
		                    nidn ,
		                    kelas ,
		                    hari ,
		                    jm_awal_kuliah ,
		                    jm_akhir_kuliah ,
		                    id_rng_kuliah ,
		                    jenis_kelas ,
		                    quota ,
		                    semester,
		                    gabung 
	                    )
	                    VALUES  ( @id_prodi , -- id_prog_study - varchar(6)
		                    @kode_makul , -- kode_makul - varchar(10)
		                    @nidn , -- nidn - varchar(10)
		                    @kelas , -- kelas - varchar(2)
		                    @hari , -- hari - varchar(6)
		                    @jam_mulai , -- jam_mulai - varchar(5)
		                    @jam_selesai , -- jam_selesai - varchar(5)
		                    @id_ruang , -- ruang - varchar(20)
		                    @jenis_kelas , -- jenis_kelas - varchar(15)
		                    @quota , -- quota - int
		                    @semester,  -- semester - varchar(5)
		                    @NoGabung )

	                    INSERT INTO dbo.bak_jadwal
	                    ( id_prog_study ,
		                    kode_makul ,
		                    nidn ,
		                    kelas ,
		                    hari ,
		                    jm_awal_kuliah ,
		                    jm_akhir_kuliah ,
		                    id_rng_kuliah ,
		                    jenis_kelas ,
		                    quota ,
		                    semester,
		                    gabung )
	                    VALUES  (@id_prodi2 , -- id_prog_study - varchar(6)
		                    @kode_makul2 , -- kode_makul - varchar(10)
		                    @nidn2 , -- nidn - varchar(10)
		                    @kelas2 , -- kelas - varchar(2)
		                    @hari2 , -- hari - varchar(6)
		                    @jam_mulai2 , -- jam_mulai - varchar(5)
		                    @jam_selesai2 , -- jam_selesai - varchar(5)
		                    @id_ruang2 , -- ruang - varchar(20)
		                    @jenis_kelas2 , -- jenis_kelas - varchar(15)
		                    @quota2 , -- quota - int
		                    @semester2,  -- semester - varchar(5)
		                    @NoGabung )

                    END
                    ELSE
                    BEGIN
	                    SET @NoGabung = @NoGabung + 1

	                    DECLARE @FirstKurikulum2 VARCHAR(4)
	                    DECLARE @SecondKurikulum2 VARCHAR(4)

	                    SELECT        @FirstKurikulum2=bak_makul.kurikulum
	                    FROM            bak_kurikulum INNER JOIN
							                     bak_makul ON bak_kurikulum.id_kurikulum = bak_makul.id_kurikulum INNER JOIN
							                     bak_jadwal ON bak_makul.kode_makul = bak_jadwal.kode_makul
	                    WHERE dbo.bak_jadwal.id_prog_study =  @id_prodi AND dbo.bak_makul.kode_makul = @kode_makul

	                    SELECT        @SecondKurikulum2=bak_makul.kurikulum
	                    FROM            bak_kurikulum INNER JOIN
							                     bak_makul ON bak_kurikulum.id_kurikulum = bak_makul.id_kurikulum INNER JOIN
							                     bak_jadwal ON bak_makul.kode_makul = bak_jadwal.kode_makul
	                    WHERE dbo.bak_jadwal.id_prog_study =  @id_prodi2 AND dbo.bak_makul.kode_makul = @kode_makul2

	                    IF @FirstKurikulum2 = @SecondKurikulum2
	                    BEGIN
		                    RAISERROR ('KURIKULUM HARUS BERBEDA', 16,10)
		                    RETURN
	                    END

	                    INSERT INTO dbo.bak_jadwal
	                    ( id_prog_study ,
		                    kode_makul ,
		                    nidn ,
		                    kelas ,
		                    hari ,
		                    jm_awal_kuliah ,
		                    jm_akhir_kuliah ,
		                    id_rng_kuliah ,
		                    jenis_kelas ,
		                    quota ,
		                    semester,
		                    gabung 
	                    )
	                    VALUES  ( @id_prodi , -- id_prog_study - varchar(6)
		                    @kode_makul , -- kode_makul - varchar(10)
		                    @nidn , -- nidn - varchar(10)
		                    @kelas , -- kelas - varchar(2)
		                    @hari , -- hari - varchar(6)
		                    @jam_mulai , -- jam_mulai - varchar(5)
		                    @jam_selesai , -- jam_selesai - varchar(5)
		                    @id_ruang , -- ruang - varchar(20)
		                    @jenis_kelas , -- jenis_kelas - varchar(15)
		                    @quota , -- quota - int
		                    @semester,  -- semester - varchar(5)
		                    @NoGabung )

	                    INSERT INTO dbo.bak_jadwal
	                    ( id_prog_study ,
		                    kode_makul ,
		                    nidn ,
		                    kelas ,
		                    hari ,
		                    jm_awal_kuliah ,
		                    jm_akhir_kuliah ,
		                    id_rng_kuliah ,
		                    jenis_kelas ,
		                    quota ,
		                    semester,
		                    gabung )
	                    VALUES  (@id_prodi2 , -- id_prog_study - varchar(6)
		                    @kode_makul2 , -- kode_makul - varchar(10)
		                    @nidn2 , -- nidn - varchar(10)
		                    @kelas2 , -- kelas - varchar(2)
		                    @hari2 , -- hari - varchar(6)
		                    @jam_mulai2 , -- jam_mulai - varchar(5)
		                    @jam_selesai2 , -- jam_selesai - varchar(5)
		                    @id_ruang2 , -- ruang - varchar(20)
		                    @jenis_kelas2 , -- jenis_kelas - varchar(15)
		                    @quota2 , -- quota - int
		                    @semester2,  -- semester - varchar(5)
		                    @NoGabung )
                    END ", con);

                    CmdInJadwal.CommandType = System.Data.CommandType.Text;

                    CmdInJadwal.Parameters.AddWithValue("@sem", this.Session["Semester"].ToString().Trim());
                    CmdInJadwal.Parameters.AddWithValue("@IdProgStudy", this.LbProdi.Text.Trim());
                    
                    CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text.Trim());
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@id_ruang", this.LbNo.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

                    CmdInJadwal.Parameters.AddWithValue("@id_prodi2", this.LbProdi2.Text.Trim());
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul2", this.LbKodeMakul2.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn2", this.LbNidn2.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas2", this.LbKelas.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari2", this.LbHari.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai2", this.LbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai2", this.LbSelesai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@id_ruang2", this.LbNo2.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas2", this.DLJenisKelas2.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota2", this.TbQuota2.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester2", this.Session["Semester"].ToString().Trim());

                    CmdInJadwal.ExecuteNonQuery();

                    // ------------- Display Jadwal ----------------------------
                    SqlCommand CmdJadwal = new SqlCommand(@"
	                SELECT     bak_jadwal.no_jadwal, bak_jadwal.id_prog_study, bak_makul.kode_makul, bak_makul.makul, bak_makul.sks, bak_jadwal.quota, bak_dosen.nama, bak_jadwal.kelas, bak_jadwal.hari, 
						                  bak_jadwal.jenis_kelas, bak_dosen.nidn, bak_jadwal.jm_awal_kuliah, bak_jadwal.jm_akhir_kuliah, bak_ruang.nm_ruang
	                FROM         bak_jadwal INNER JOIN
						                  bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN
						                  bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN
						                  bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN
						                  bak_ruang ON bak_jadwal.id_rng_kuliah = bak_ruang.no
	                WHERE     (bak_jadwal.id_prog_study = @id_prodi) AND (bak_jadwal.semester = @semester) AND (makul_univ IS NULL OR makul_univ != 'yes') AND (dbo.bak_jadwal.gabung IS NOT NULL)
	                ORDER BY bak_jadwal.no_jadwal ", con);

                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

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
                            this.PanelListJadwal.Enabled = true;
                            this.PanelListJadwal.Visible = true;

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
                            //this.PanelEditJadwal.Enabled = false;
                            //this.PanelEditJadwal.Visible = false;
                        }
                        else
                        {
                            this.PanelListJadwal.Enabled = false;
                            this.PanelListJadwal.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVJadwal.DataSource = TableJadwal;
                            GVJadwal.DataBind();
                        }
                    }

                    // ------------------ Clear Data 1 -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    this.LbRuang.Text = "";
                    this.LbNo.Text = "";

                    DLJenisKelas.SelectedIndex = 0;
                    TbQuota.Text = "";

                    // ------------------ Clear Data 2 -----------------
                    LbProdi2.Text = "";
                    LbKodeMakul2.Text = "";
                    LbMakul2.Text = "";

                    DLProdiMakul2.SelectedIndex = 0;
                    LbNidn2.Text = "";
                    LbDosen2.Text = "";

                    LbDosen2.Text = "";
                    LbKelas.Text = "00:00";
                    LbHari.Text = "00:00";

                    LbMulai.Text = "";
                    LbSelesai.Text = "";

                    this.LbRuang2.Text = "";
                    this.LbNo2.Text = "";

                    DLJenisKelas2.SelectedIndex = 0;
                    TbQuota2.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //return;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //this.PanelMakul.Enabled = false;
            //this.PanelMakul.Visible = false;

            //this.PanelUpdate.Enabled = true;
            //this.PanelUpdate.Visible = true;

            //this.PanelTambah.Enabled = false;
            //this.PanelTambah.Visible = false;

            //// get row index
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;
            ////Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            ////set lb no jadwal
            //this.lbno_jadwal.Text = this.GVJadwal.Rows[index].Cells[2].Text;

            ////hide panel jadwal
            ////this.PanelJadwal.Enabled = false;
            ////this.PanelJadwal.Visible = false;

            ////unhide panel edit jadwal
            ////this.PanelEditJadwal.Enabled = true;
            ////this.PanelEditJadwal.Visible = true;

            ////read record you want to display
            //string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    //----------------------------------------- -------------------------------------------
            //    con.Open();
            //    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal2", con);
            //    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

            //    CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[2].Text);

            //    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
            //    {
            //        if (rdr.HasRows)
            //        {
            //            while (rdr.Read())
            //            {
            //                this.LbProdi.Text = rdr["id_prog_study"].ToString();
            //                this.LbKodeMakul.Text = rdr["kode_makul"].ToString();
            //                this.LbMakul.Text = rdr["makul"].ToString();
            //                this.LbNidn.Text = rdr["nidn"].ToString();
            //                this.LbDosen.Text = rdr["nama"].ToString();
            //                this.DLKelas.SelectedItem.Text = rdr["kelas"].ToString();
            //                this.DLHari.SelectedItem.Text = rdr["hari"].ToString();
            //                this.TbMulai.Text = rdr["jam_mulai"].ToString();
            //                this.TbSelesai.Text = rdr["jam_selesai"].ToString();
            //               // this.TbRuang.Text = rdr["ruang"].ToString();
            //                this.DLJenisKelas.SelectedItem.Text = rdr["jenis_kelas"].ToString();
            //                this.TbQuota.Text = rdr["quota"].ToString();
            //                // ----------- semester ----------------
            //                string sms = rdr["semester"].ToString();
            //                //this.DLTahun2.SelectedValue = sms.Substring(0, 4);
            //                //this.DLSemester2.SelectedValue = sms.Substring(4, 1);

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //}
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //edit
            e.Row.Cells[1].Visible = false; //delete
            e.Row.Cells[2].Visible = false; //no jadwal
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
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
            //if (this.TbRuang.Text == "")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Perkuliahan');", true);
            //    return;
            //}
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
                    SqlCommand CmdInJadwal = new SqlCommand("SpUpJadwalKuliah2", con);
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
                    // CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

                    CmdInJadwal.ExecuteNonQuery();

                    this.lbno_jadwal.Text = "";

                    //BtnJadwal_Click(this, null);

                    //this.PanelEditJadwal.Enabled = false;
                    //this.PanelEditJadwal.Visible = false;

                    this.PanelUpdate.Enabled = false;
                    this.PanelUpdate.Visible = false;

                    this.PanelTambah.Enabled = true;
                    this.PanelTambah.Visible = true;

                    // ------------------ Reload DGV ------------------------
                    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

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
                                datarow["Mulai"] = rdr["jam_mulai"];
                                datarow["Selesai"] = rdr["jam_selesai"];
                                datarow["Ruang"] = rdr["ruang"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwal;
                            this.GVJadwal.DataBind();

                            // hide panel Edit Mata Kuliah

                        }
                        else
                        {
                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVJadwal.DataSource = TableJadwal;
                            GVJadwal.DataBind();
                        }
                    }

                    this.PanelMakul.Enabled = true;
                    this.PanelMakul.Visible = true;

                    // ------------------ Clear Data -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    //TbRuang.Text = "";

                    DLJenisKelas.SelectedIndex = 0;
                    TbQuota.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void DLRuangProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SELECT * FROM  bak_ruang WHERE id_prog_study=@id_prodi AND status = 1", con);
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

                    this.LbRuang2.Text = this.GVRuang.Rows[i].Cells[2].Text;
                    this.LbNo2.Text = this.GVRuang.Rows[i].Cells[1].Text;

                    //Response.Write(_idruang.ToString());
                    //Response.Write("DataKeyName: " + GVMakul.DataKeys[i].Value.ToString());
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

        protected void DLProdiMakul2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpGetMakul", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.DLProdiMakul2.SelectedValue);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("KURIKULUM");
                TableMakul.Columns.Add("SKS");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailMakul2.Enabled = true;
                        this.PanelDetailMakul2.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["KURIKULUM"] = rdr["kurikulum"];
                            datarow["SKS"] = rdr["sks"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVMakul2.DataSource = TableMakul;
                        this.GVMakul2.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVMakul2.DataSource = TableMakul;
                        GVMakul2.DataBind();

                        this.PanelDetailMakul2.Enabled = false;
                        this.PanelDetailMakul2.Visible = false;
                    }
                }
            }
        }

        protected void GVMakul2_PreRender(object sender, EventArgs e)
        {
            if (this.GVMakul2.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVMakul2.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVMakul2.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVMakul.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void CBMakul2_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVMakul2.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul2.Rows[i].FindControl("CBMakul2");
                if (ch.Checked == true)
                {

                    this.LbKodeMakul2.Text = this.GVMakul2.Rows[i].Cells[1].Text;
                    this.LbMakul2.Text = this.GVMakul2.Rows[i].Cells[2].Text;
                    //Response.Write("DataKeyName: " + GVMakul.DataKeys[i].Value.ToString());
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul2.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul2.Rows[i].FindControl("CBMakul2");
                ch.Checked = false;
            }

            // label prodi
            this.LbProdi2.Text = this.DLProdiMakul2.SelectedValue;

            //Select Drop Down List to Default
            this.DLProdiMakul2.SelectedIndex = 0;

            //hide panel
            this.PanelDetailMakul2.Enabled = false;
            this.PanelDetailMakul2.Visible = false;
        }

        protected void DLKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LbKelas.Text = this.DLKelas.SelectedItem.Text.Trim();
        }

        protected void DLHari_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LbHari.Text = this.DLHari.SelectedItem.Text.Trim();
        }

        protected void TbMulai_TextChanged(object sender, EventArgs e)
        {
            this.LbMulai.Text = TbMulai.Text.Trim();
        }

        protected void TbSelesai_TextChanged(object sender, EventArgs e)
        {
            this.LbSelesai.Text = TbSelesai.Text.Trim();
        }
    }
}