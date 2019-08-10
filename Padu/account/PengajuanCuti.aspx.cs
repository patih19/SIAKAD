using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Padu.account
{
    public partial class PengajuanCuti : Mhs_account
    {
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
            this.Session["jenjang"] = (object)null;
            this.Session["prodi"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.Remove("prodi");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelStatusCuti.Visible = false;
                this.PanelStatusCuti.Enabled = false;

                TahunAkademik();
                AktivitasMhs();
                StatusCuti();
            }
        }

        private void TahunAkademik()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 2 bak_kal.thn+1 AS thn, CAST(bak_kal.thn +1 AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +2 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN ('S1') group by thn ORDER BY thn DESC", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLTahun.DataSource = CmdJadwal.ExecuteReader();
                    this.DLTahun.DataTextField = "ThnAkm";
                    this.DLTahun.DataValueField = "thn";
                    this.DLTahun.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLTahun.Items.Insert(0, new ListItem("-- Tahun Akademik --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void AktivitasMhs()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdAktvMhs = new SqlCommand(""+
                    "SELECT * INTO #TempStatusMhs FROM "+
                    "( "+
                        "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study AS PROGRAM_STUDI, 'A' AS setatus "+
                        "FROM            bak_jadwal INNER JOIN "+
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                 "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                 "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                        "WHERE (bak_mahasiswa.status NOT IN('L')) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 2014) AND bak_prog_study.jenjang IN ( 'S1','D3' )"+
                        "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study "+
                        "UNION ALL "+
                        "SELECT        bak_cuti_nonaktif.npm, bak_mahasiswa.nama, bak_cuti_nonaktif.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study AS PROGRAM_STUDI, bak_cuti_nonaktif.status AS setatus "+
                        "FROM            bak_cuti_nonaktif INNER JOIN "+
                                                 "bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                 "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                    ") AS StatusMhs "+
                    "WHERE StatusMhs.npm = @npm AND StatusMhs.semester <= ( "+
                        "SELECT TOP 1       bak_jadwal.semester "+
                        "FROM            bak_jadwal INNER JOIN "+
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal "+
                        "GROUP BY semester ORDER BY semester DESC "+
                    ") "+
                     "ORDER BY StatusMhs.npm,StatusMhs.semester ASC "+
                     
                    "DECLARE @ColName2 NVARCHAR(MAX) = '' "+
                    "DECLARE @SQL2 NVARCHAR(max) = '' "+

                    "SELECT @ColName2 += QUOTENAME( #TempStatusMhs.semester) + ',' FROM #TempStatusMhs GROUP BY semester ORDER BY  semester ASC	"+
                    "SET @ColName2 = LEFT(@ColName2, LEN(@ColName2) - 1) "+

                     "IF(@ColName2 = '') "+
                     "BEGIN "+
                         "RAISERROR('KRS MAHASISWA TIDAK DITEMUKAN', 16, 10) "+
                         "RETURN "+
                     "END "+

                     "SET @SQL2 = "+
                     "'SELECT' + @ColName2 + ' FROM #TempStatusMhs PIVOT "+
                     "( "+
                         "max(setatus) "+
                         "FOR semester "+
                         "IN('+ @ColName2 +') "+
                     ") AS PivotTable1 '"+

                     "EXECUTE sp_executesql @SQL2 "+
                     "DROP  table #TempStatusMhs" +
                     "", con);

                    CmdAktvMhs.CommandType = System.Data.CommandType.Text;
                    CmdAktvMhs.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    this.GvAktivitasKuliah.DataSource = CmdAktvMhs.ExecuteReader();
                    this.GvAktivitasKuliah.DataBind();

                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void StatusCuti()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdStatusCuti = new SqlCommand("" +
                     "SELECT * FROM bak_pengajuan_cuti WHERE npm=@npm"+
                     "", con);

                    CmdStatusCuti.CommandType = System.Data.CommandType.Text;
                    CmdStatusCuti.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    DataTable TablePengajuan = new DataTable();
                    TablePengajuan.Columns.Add("Semester");
                    TablePengajuan.Columns.Add("Tanggal Pengajuan");
                    TablePengajuan.Columns.Add("Satus");

                    using (SqlDataReader rdr = CmdStatusCuti.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelStatusCuti.Enabled = true;
                            this.PanelStatusCuti.Visible = true;

                            this.PanelNilai.Visible = false;
                            this.PanelNilai.Enabled = false;

                            while (rdr.Read())
                            {
                                DataRow datarow = TablePengajuan.NewRow();
                                datarow["Semester"] = rdr["semester"];
                                datarow["Tanggal Pengajuan"] = rdr["tgl_pengajuan"];
                                if(rdr["status"] == DBNull.Value)
                                {
                                    datarow["Satus"] = "Belum diproses";
                                    this.PanelDwnFormCuti.Visible = true;
                                }
                                else if (rdr["status"].ToString().Trim() == "ok")
                                {
                                    datarow["Satus"] = "Diizinkan";
                                    this.PanelDwnFormCuti.Visible = false;

                                    this.PanelNilai.Visible = true;
                                    this.PanelNilai.Enabled = true;

                                }
                                else if (rdr["status"].ToString().Trim() == "no")
                                {
                                    datarow["Satus"] = "Ditolak";
                                    this.PanelDwnFormCuti.Visible = true;
                                }
                                
                                TablePengajuan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVPengajuanCuti.DataSource = TablePengajuan;
                            this.GVPengajuanCuti.DataBind();
                        }
                        else
                        {
                            TablePengajuan.Rows.Clear();
                            TablePengajuan.Clear();
                            this.GVPengajuanCuti.DataSource = TablePengajuan;
                            this.GVPengajuanCuti.DataBind();

                            this.PanelStatusCuti.Enabled = false;
                            this.PanelStatusCuti.Visible = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdPengajuanCuti = new SqlCommand("" +

                    "DECLARE @SemBerjalan VARCHAR(5) = '' "+
                    "SELECT * FROM dbo.bak_cuti_nonaktif WHERE status = 'C' AND npm = @NpmSkripsi "+
                    "ORDER BY semester DESC "+
                    "IF (@@ROWCOUNT <= 2) "+
                    "BEGIN "+

                        //--a. Get Semester Aktif Berjalan
                        "SELECT        TOP (1) @SemBerjalan=bak_jadwal.semester " +
                        "FROM            bak_jadwal INNER JOIN "+
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                 "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                        "WHERE (bak_prog_study.jenjang NOT IN('S2')) AND(bak_prog_study.id_prog_study = @prodi) "+
                        "GROUP BY bak_jadwal.semester, bak_prog_study.id_prog_study "+
                        "ORDER BY bak_jadwal.semester DESC " +

                        //"SELECT id_update FROM dbo.bak_update_status_mhs WHERE semester = @SemCuti " +
                        //"IF(@@ROWCOUNT = 0) "+
                        //"BEGIN "+
                        //    "RAISERROR('ERROR UPADTE DATA MAHASISWA SEMESTER INI BELUM DILAKUKAN HUBUNGI BAKPK', 16, 10) "+
                        //    "RETURN "+
                        //"END "+

                        //--b. Pastikan semester cuti yg diilih data status mhs BELUM diupdate
                        "SELECT id_update FROM dbo.bak_update_status_mhs WHERE semester = @SemCuti " +
                        "IF(@@ROWCOUNT != 0) "+

                        "BEGIN "+
                            "DECLARE @msg VARCHAR(500) = 'ERROR, DATA MAHASISWA SEMESTER ' + @SemCuti + ' SUDAH DIUPDATE BAKPK, CUTI PADA SEMESTER TERSEBUT TIDAK DAPAT DILAKUKAN HUBUNGI TU PRODI' "+
                            "RAISERROR(@msg, 16, 10) "+
                            "RETURN " +
                        "END "+

                        //--c. Cek permintaan semester cuti loncat
                        "DECLARE @CrntSem VARCHAR(5) = '' "+
                        "SELECT TOP 1       @CrntSem = bak_jadwal.semester "+
                        "FROM            bak_jadwal INNER JOIN "+
                                                    "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal "+
                        "GROUP BY semester ORDER BY semester DESC "+

                        "DECLARE @CrntThn VARCHAR(4) = LEFT(@CrntSem, 4) "+
                        "DECLARE @CrntDigit VARCHAR(1) = RIGHT(@CrntSem, 1) "+
                        "DECLARE @MaxThn INT = CAST(@CrntThn AS INT) + 1 "+
                        "DECLARE @MaxSemester int = CAST(CAST(@MaxThn AS VARCHAR(5)) + @CrntDigit as int) "+

                        "IF(@SemCuti >= @MaxSemester) "+
                        "BEGIN "+
                            "RAISERROR('ERROR, CUTI PADA SEMESTER TERSEBUT BELUM DIPERBOLEHKAN !!', 16, 10) "+
                            "RETURN "+
                        "END "+

                        //--d.Cuti Semester Mundur
                        "IF(@SemCuti < @SemBerjalan) "+
                        "BEGIN "+
                            "RAISERROR('ERROR, CUTI MUNDUR TIDAK DIIZINKAN !!', 16, 10) "+
                            "RETURN "+
                        "END "+

                        // --e.hanya boleh satu pengajuan
                        "SELECT id_pengajuan FROM bak_pengajuan_cuti WHERE status IS NULL AND npm = @NpmSkripsi "+
                        "IF(@@ROWCOUNT != 0) "+
                        "BEGIN "+
                            "RAISERROR('ERROR, ADA PENGAJUAN CUTI KULIAH BELUM DIPROSES, HUBUNGI TU PRODI', 16, 10) "+
                            "RETURN "+
                        "END "+

                        //--f.Cek sudah mengajukan cuti--
                        "SELECT id_pengajuan FROM bak_pengajuan_cuti WHERE semester = @SemCuti AND npm=@NpmSkripsi " +
                        "IF(@@ROWCOUNT = 0) "+
                        "BEGIN "+
                            "INSERT INTO dbo.bak_pengajuan_cuti "+
                                    "(npm, semester, tgl_pengajuan, status, tgl_persetujuan, berkas) "+
                            "VALUES(@NpmSkripsi, @SemCuti, GETDATE(),NULL,NULL,NULL) "+
                        "END "+
                        "ELSE "+
                        "BEGIN "+
                            "RAISERROR('ERROR, DATA PENGAJUAN CUTI SUDAH ADA', 16, 10) "+
                            "RETURN "+
                        "END "+
                    "END "+
                    "ELSE "+
                    "BEGIN "+
                         "RAISERROR('ERROR, ANDA SUDAH TERCATAT DUA KALI CUTI', 16, 10) "+
                         "RETURN "+
                    "END "+
                    "", con);

                    CmdPengajuanCuti.CommandType = System.Data.CommandType.Text;

                    CmdPengajuanCuti.Parameters.AddWithValue("@NpmSkripsi", this.Session["Name"].ToString());
                    CmdPengajuanCuti.Parameters.AddWithValue("@SemCuti", this.DLTahun.SelectedValue.Trim() + this.DLSemester.SelectedValue.Trim());
                    CmdPengajuanCuti.Parameters.AddWithValue("@prodi", this.Session["prodi"].ToString());

                    CmdPengajuanCuti.ExecuteNonQuery();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PENGAJUAN CUTI BERHASIL');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }
    }
}