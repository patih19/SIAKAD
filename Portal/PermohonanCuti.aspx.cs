using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Portal
{
    public partial class PermohonanCuti : Tu
    {
        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }

        public string _SEMESTER
        {
            get { return this.ViewState["SEMESTER"].ToString(); }
            set { this.ViewState["SEMESTER"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelPersetujuan.Enabled = false;
                this.PanelPersetujuan.Visible = false;

                PengajuanCuti();
            }
        }

        private void PengajuanCuti()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdStatusCuti = new SqlCommand("" +
                    "SELECT        bak_mahasiswa.npm, bak_mahasiswa.id_prog_study, bak_mahasiswa.nama, bak_pengajuan_cuti.tgl_pengajuan, bak_pengajuan_cuti.semester, bak_pengajuan_cuti.berkas, bak_pengajuan_cuti.status " +
                    "FROM            bak_mahasiswa INNER JOIN " +
                                             "bak_pengajuan_cuti ON bak_mahasiswa.npm = bak_pengajuan_cuti.npm " +
                    "WHERE id_prog_study =@IdProdi AND dbo.bak_pengajuan_cuti.status IS NULL " +
                    "ORDER BY tgl_pengajuan DESC   " +
                     "", con);

                    CmdStatusCuti.CommandType = System.Data.CommandType.Text;
                    CmdStatusCuti.Parameters.AddWithValue("@IdProdi", this.Session["level"].ToString());

                    DataTable TablePermohonan = new DataTable();
                    TablePermohonan.Columns.Add("NPM");
                    TablePermohonan.Columns.Add("Nama");
                    TablePermohonan.Columns.Add("Semester Cuti");
                    TablePermohonan.Columns.Add("Tanggal Pengajuan");
                    TablePermohonan.Columns.Add("Berkas");
                    TablePermohonan.Columns.Add("Status");

                    using (SqlDataReader rdr = CmdStatusCuti.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TablePermohonan.NewRow();
                                datarow["NPM"] = rdr["npm"];
                                datarow["Nama"] = rdr["nama"];
                                datarow["Semester Cuti"] = rdr["semester"];
                                datarow["Tanggal Pengajuan"] = rdr["tgl_pengajuan"];
                                if (rdr["berkas"] == DBNull.Value)
                                {
                                    datarow["Berkas"] = "-";
                                }
                                else
                                {
                                    datarow["Berkas"] = "Ada";
                                }
                                if (rdr["status"] == DBNull.Value)
                                {
                                    datarow["Status"] = "Belum diproses";
                                }

                                TablePermohonan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVCuti.DataSource = TablePermohonan;
                            this.GVCuti.DataBind();
                        }
                        else
                        {
                            Response.Write("tidak ada data");

                            TablePermohonan.Rows.Clear();
                            TablePermohonan.Clear();
                            this.GVCuti.DataSource = TablePermohonan;
                            this.GVCuti.DataBind();
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

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (this.FPBerkas.FileName.Length > 35)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Karakter Nama File Melebihi Ketentuan');", true);
                return;
            }

            //--------------- Filter Ukuran Foto --------------------
            if (!FPBerkas.HasFile)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('File Surat Cuti Belum Dipilih');", true);
                return;
            }

            if ((FPBerkas.PostedFile.ContentLength > 204800) && (FPBerkas.PostedFile.ContentLength <= 450000))
            {
                //// Read the file and convert it to Byte Array
                //string filePath = FileUpload1.PostedFile.FileName;
                //string filename = Path.GetFileName(filePath);
                //string ext = Path.GetExtension(filename);
                //string contenttype = String.Empty;

                string FileName = Path.GetFileName(FPBerkas.PostedFile.FileName);
                string ext = Path.GetExtension(FileName);
                string contenttype = String.Empty;


                //Set the contenttype based on File Extension
                switch (ext)
                {
                    case ".jpg":
                        contenttype = "image/jpg";
                        break;
                    case ".jpeg":
                        contenttype = "image/jpeg";
                        break;
                    case ".png":
                        contenttype = "image/png";
                        break;
                    case ".JPG":
                        contenttype = "image/jpeg";
                        break;
                    case ".JPEG":
                        contenttype = "image/jpeg";
                        break;
                    case ".PNG":
                        contenttype = "image/png";
                        break;
                }

                if (contenttype != String.Empty)
                {
                    // ------- delete old image from server  ------------
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand CmdDisplay = new SqlCommand("select npm, berkas from bak_pengajuan_cuti where npm=@npm AND semester=@semester AND berkas IS NOT NULL", connection);
                            CmdDisplay.Parameters.AddWithValue("@npm", _NPM); // this.Session["NoDaftar"].ToString());
                            CmdDisplay.Parameters.AddWithValue("@semester", _SEMESTER); // this.Session["NoDaftar"].ToString());

                            SqlDataReader rdr = CmdDisplay.ExecuteReader();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    string old = "~/" + rdr["berkas"].ToString();
                                    System.IO.File.Delete(Server.MapPath("~/cuti/" + rdr["berkas"].ToString()));
                                }
                            }
                            else
                            {

                            }

                            rdr.Close();
                        }
                        catch (Exception ex)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        }
                    }

                    //insert the file into database
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
                    {
                        try
                        {
                            connection.Open();

                            if (Exsist())
                            {
                                //UPDATE
                                SqlCommand cmd = new SqlCommand("UPDATE bak_pengajuan_cuti SET berkas=@berkas  WHERE npm=@npm AND semester=@semester", connection);
                                cmd.CommandType = System.Data.CommandType.Text;

                                //--------------- save path to DB --------------------------
                                cmd.Parameters.AddWithValue("@npm", _NPM);
                                cmd.Parameters.AddWithValue("@semester", _SEMESTER);
                                cmd.Parameters.AddWithValue("@berkas", _NPM + "_"+_SEMESTER+"_" + FileName);
                                cmd.ExecuteNonQuery();

                                //------------- InsertUpdateData(cmd) -----------;
                                cmd.Dispose();

                                //---------- Save files to disk -------------------
                                FPBerkas.SaveAs(Server.MapPath("~/cuti/" + _NPM + "_" + _SEMESTER + "_" + FileName));

                                connection.Close();
                                connection.Dispose();

                                FotoSuratCuti(_NPM,_SEMESTER);

                            }
                            else
                            {
                                Response.Write("data pengajuan cuti tidak ditemukan");
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        }
                    }
                }
                else // type file tidak diperbolehkan
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('File Tidak Diperbolehkan');", true);
                    return;
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Ukuran File Tidak Sesuai Ketentuan');", true);
                return;
            }
        }

        protected Boolean Exsist()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    //========================== READER NO DAFTAR FROM DB =========================
                    SqlCommand CmdDisplay = new SqlCommand("select npm,berkas from bak_pengajuan_cuti where npm=@npm AND semester=@semester", connection);
                    CmdDisplay.Parameters.AddWithValue("@npm", _NPM); // this.Session["NoDaftar"].ToString());
                    CmdDisplay.Parameters.AddWithValue("@semester", _SEMESTER); // this.Session["NoDaftar"].ToString());
                    SqlDataReader reader = CmdDisplay.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        return false;
                    }
                    //======================== END READER =========================
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        protected void FotoSuratCuti(string npm, string semester)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    //========================== READER IMAGE FROM DB =========================
                    SqlCommand CmdDisplay = new SqlCommand("select npm,berkas from bak_pengajuan_cuti where npm=@npm AND semester=@semester", connection);

                    CmdDisplay.Parameters.AddWithValue("@npm", _NPM); 
                    CmdDisplay.Parameters.AddWithValue("@semester", _SEMESTER);

                    SqlDataReader reader = CmdDisplay.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["berkas"] == DBNull.Value)
                            {
                                this.PanelIzinCuti.Enabled = false;
                                this.PanelIzinCuti.Visible = false;
                            }
                            else
                            {
                                this.ImageSuratCuti.ImageUrl = "~/cuti/" + reader["berkas"].ToString();

                                this.PanelIzinCuti.Enabled = true;
                                this.PanelIzinCuti.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Image found.");
                    }
                    reader.Close();
                    //======================== END READER =========================
                }
                catch (Exception ex)
                {
                    this.ImageSuratCuti.ImageUrl = null;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnAction_Click(object sender, EventArgs e)
        {
            this.PanelPersetujuan.Enabled = true;
            this.PanelPersetujuan.Visible = true;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            _NPM = this.GVCuti.Rows[index].Cells[1].Text.Trim();
            _SEMESTER = this.GVCuti.Rows[index].Cells[3].Text.Trim();

            AktivitasMhs();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand CmdDisplay = new SqlCommand("select npm,berkas from bak_pengajuan_cuti where npm=@npm AND semester=@semester", connection);

                    CmdDisplay.Parameters.AddWithValue("@npm", _NPM);
                    CmdDisplay.Parameters.AddWithValue("@semester", _SEMESTER);

                    SqlDataReader reader = CmdDisplay.ExecuteReader();
                    if (reader.HasRows)
                    {
                        this.PanelPersetujuan.Enabled = true;
                        this.PanelPersetujuan.Visible = true;

                        FotoSuratCuti(_NPM, _SEMESTER);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    this.ImageSuratCuti.ImageUrl = null;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnPersetujuan_Click(object sender, EventArgs e)
        {
            if ((this.RbSetuju.Checked == false) && (this.RbTolak.Checked == false))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Persetujuan');", true);
                return;
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();
                try
                {
                    SqlCommand CmdDisplay = new SqlCommand("UPDATE bak_pengajuan_cuti SET tgl_persetujuan=GETDATE(), status=@status where npm=@npm AND semester=@semester", connection,tran);
                    CmdDisplay.Parameters.AddWithValue("@npm", _NPM);
                    CmdDisplay.Parameters.AddWithValue("@semester", _SEMESTER);

                    if (this.RbSetuju.Checked)
                    {
                        CmdDisplay.Parameters.AddWithValue("@status", "ok");
                    }
                    else if (this.RbTolak.Checked)
                    {
                        CmdDisplay.Parameters.AddWithValue("@status", "no");
                    }

                    SqlCommand CmdUpdateMhs = new SqlCommand(""+
                        "UPDATE dbo.bak_mahasiswa SET status = 'C' WHERE npm = @npm "+

                        "SELECT id FROM dbo.bak_cuti_nonaktif WHERE npm =@npm AND semester =@semester AND status IN('N', 'C') "+
                        "IF(@@ROWCOUNT = 0) "+
                        "BEGIN "+
                            "INSERT INTO dbo.bak_cuti_nonaktif "+
                                    "(npm, status, semester, tgl_update) "+
                            "VALUES(@npm, "+
                                      "'C',  "+
                                      "@semester,  "+
                                      "GETDATE() "+
                                      ") "+
                        "END "+
                        "ELSE "+
                        "BEGIN "+
                            "UPDATE  dbo.bak_cuti_nonaktif SET status = 'C', tgl_update = GETDATE() WHERE semester = @semester AND npm = @npm "+
                        "END "+
                        "", connection,tran);
                    CmdUpdateMhs.Parameters.AddWithValue("@npm", _NPM);
                    CmdUpdateMhs.Parameters.AddWithValue("@semester", _SEMESTER);

                    CmdDisplay.ExecuteNonQuery();
                    CmdUpdateMhs.ExecuteNonQuery();

                    tran.Commit();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Persetujuan Berhasil');", true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    tran.Dispose();
                    connection.Close();
                    this.ImageSuratCuti.ImageUrl = null;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
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

                    SqlCommand CmdAktvMhs = new SqlCommand("" +
                    "SELECT * INTO #TempStatusMhs FROM " +
                    "( " +
                        "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study AS PROGRAM_STUDI, 'A' AS setatus " +
                        "FROM            bak_jadwal INNER JOIN " +
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN " +
                                                 "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN " +
                                                 "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study " +
                        "WHERE (bak_mahasiswa.status NOT IN('L')) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 2014) AND bak_prog_study.jenjang = 'S1' " +
                        "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study " +
                        "UNION ALL " +
                        "SELECT        bak_cuti_nonaktif.npm, bak_mahasiswa.nama, bak_cuti_nonaktif.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study AS PROGRAM_STUDI, bak_cuti_nonaktif.status AS setatus " +
                        "FROM            bak_cuti_nonaktif INNER JOIN " +
                                                 "bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN " +
                                                 "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                    ") AS StatusMhs " +
                    "WHERE StatusMhs.npm = @npm AND StatusMhs.semester <= ( " +
                        "SELECT TOP 1       bak_jadwal.semester " +
                        "FROM            bak_jadwal INNER JOIN " +
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal " +
                        "GROUP BY semester ORDER BY semester DESC " +
                    ") " +
                     "ORDER BY StatusMhs.npm,StatusMhs.semester ASC " +

                    "DECLARE @ColName2 NVARCHAR(MAX) = '' " +
                    "DECLARE @SQL2 NVARCHAR(max) = '' " +

                    "SELECT @ColName2 += QUOTENAME( #TempStatusMhs.semester) + ',' FROM #TempStatusMhs GROUP BY semester ORDER BY  semester ASC	" +
                    "SET @ColName2 = LEFT(@ColName2, LEN(@ColName2) - 1) " +

                     "IF(@ColName2 = '') " +
                     "BEGIN " +
                         "RAISERROR('KRS MAHASISWA TIDAK DITEMUKAN', 16, 10) " +
                         "RETURN " +
                     "END " +

                     "SET @SQL2 = " +
                     "'SELECT' + @ColName2 + ' FROM #TempStatusMhs PIVOT " +
                     "( " +
                         "max(setatus) " +
                         "FOR semester " +
                         "IN('+ @ColName2 +') " +
                     ") AS PivotTable1 '" +

                     "EXECUTE sp_executesql @SQL2 " +
                     "DROP  table #TempStatusMhs" +
                     "", con);

                    CmdAktvMhs.CommandType = System.Data.CommandType.Text;
                    CmdAktvMhs.Parameters.AddWithValue("@npm", _NPM);

                    this.GvStatusKuliah.DataSource = CmdAktvMhs.ExecuteReader();
                    this.GvStatusKuliah.DataBind();

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
    }
}