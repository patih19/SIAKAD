using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Hosting;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Portal
{
    //public partial class WebForm4 : System.Web.UI.Page
    public partial class WebForm4 : Tu
    {
        public Int32 _TotalAktif
        {
            get { return Convert.ToInt32(this.ViewState["TotalAktif"].ToString()); }
            set { this.ViewState["TotalAktif"] = (object)value; }
        }

        public Int32 _TotalNonAktif
        {
            get { return Convert.ToInt32(this.ViewState["TotalNonAktif"].ToString()); }
            set { this.ViewState["TotalNonAktif"] = (object)value; }
        }

        public Int32 _TotalCuti
        {
            get { return Convert.ToInt32(this.ViewState["TotalCuti"].ToString()); }
            set { this.ViewState["TotalCuti"] = (object)value; }
        }

        public Int32 _TotalKeluar
        {
            get { return Convert.ToInt32(this.ViewState["TotalKeluar"].ToString()); }
            set { this.ViewState["TotalKeluar"] = (object)value; }
        }

        public Int32 _TotalLulus
        {
            get { return Convert.ToInt32(this.ViewState["TotalLulus"].ToString()); }
            set { this.ViewState["TotalLulus"] = (object)value; }
        }

        public string _Jenjang
        {
            get { return Convert.ToString(this.ViewState["Jenjang"].ToString()); }
            set { this.ViewState["Jenjang"] = (object)value; }
        }

        public string _TopSemester
        {
            get { return Convert.ToString(this.ViewState["TopSemester"].ToString()); }
            set { this.ViewState["TopSemester"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.PanelUji.Enabled = false;
                //this.PanelUji.Visible = false;

                _TotalAktif = 0;
                _TotalNonAktif = 0;
                _TotalCuti = 0;
                _TotalKeluar = 0;
                _TotalLulus = 0;
                _Jenjang = "";

                this.PanelRekapAktif.Enabled = false;
                this.PanelRekapAktif.Visible = false;

                GetJenjang();
                TahunAkademik();
                LastSemester();

                this.LbSemAktif1.Text = "("+ _TopSemester +")";
                this.LbSemAktif2.Text = "("+_TopSemester +")";

                PopulateMhsAktif(_TopSemester, this.Session["level"].ToString().Trim());
                PopulateRekapBimbinganKRS(_TopSemester, this.Session["level"].ToString().Trim());
            }
        }

        protected void LastSemester()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("" +
                        "SELECT top 1    bak_jadwal.semester " +
                        "FROM         bak_jadwal INNER JOIN " +
                                              "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN " +
                                              "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study " +
                        "WHERE jenjang =@jenjang " +
                        "GROUP BY bak_jadwal.semester " +
                        "ORDER BY semester DESC " +
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;
                    CmdJadwal.Parameters.AddWithValue("@jenjang", _Jenjang);

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                _TopSemester = rdr["semester"].ToString().Trim();
                            }
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

        protected void TahunAkademik()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    if (_Jenjang == "D3" || _Jenjang == "S1")
                    {
                        _Jenjang = "S1";
                    }

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 3 bak_kal.thn AS thn, CAST(bak_kal.thn  AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +1 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang = @jenjang group by thn ORDER BY thn DESC", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@jenjang", _Jenjang);

                    SqlDataReader RdrJadwal = CmdJadwal.ExecuteReader();

                    this.DLTahun.DataSource = RdrJadwal;
                    this.DLTahun.DataTextField = "ThnAkm";
                    this.DLTahun.DataValueField = "thn";
                    this.DLTahun.DataBind();

                    RdrJadwal.Close();
                    CmdJadwal.Dispose();

                    SqlCommand CmdBimbingan = new SqlCommand("select TOP 3 bak_kal.thn AS thn, CAST(bak_kal.thn  AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +1 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang = @jenjang group by thn ORDER BY thn DESC", con);
                    CmdBimbingan.CommandType = System.Data.CommandType.Text;

                    CmdBimbingan.Parameters.AddWithValue("@jenjang", _Jenjang);

                    SqlDataReader RdrBimbingan = CmdBimbingan.ExecuteReader();

                    this.DLTahun2.DataSource = RdrBimbingan;
                    this.DLTahun2.DataTextField = "ThnAkm";
                    this.DLTahun2.DataValueField = "thn";
                    this.DLTahun2.DataBind();

                    RdrBimbingan.Close();
                    CmdBimbingan.Dispose();

                    con.Close();
                    con.Dispose();
                }

                this.DLTahun.Items.Insert(0, new ListItem("-- Tahun Akademik --", "-1"));

                this.DLTahun2.Items.Insert(0, new ListItem("-- Tahun Akademik --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void PopulateMhsAktif(string TopSemester, string IdProdi)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand(""+
                        " SELECT ProdiMhs.thn_angkatan, MhsA.jumlah_aktif, MhsNonAktif.jumlah_nonaktif, MhsCuti.jumlah_cuti, MhsK.jumlah_keluar, MhsLulus.jumlah_lulus from "+
                        " ( "+
                            //--Cuti NonAktif Keluar --
                            " SELECT     bak_mahasiswa.thn_angkatan "+
                            " FROM         bak_cuti_nonaktif INNER JOIN "+
                                                  " bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                  " bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+
                            " where bak_mahasiswa.id_prog_study = @IdMhsProdi and bak_cuti_nonaktif.semester = @SemNewHitungJumlah AND bak_cuti_nonaktif.status in ('N', 'C', 'K') "+

                            " GROUP BY bak_mahasiswa.thn_angkatan "+
                            " UNION "+
                            
                            // -- Lulus --
                            " select thn_angkatan from bak_mahasiswa where status = 'L' and id_prog_study = @IdMhsProdi and smster_lls = @SemNewHitungJumlah "+
                            " group by thn_angkatan "+
                            " UNION "+
                            
                            //-- Aktif --
                            " SELECT        bak_mahasiswa.thn_angkatan "+
                            " FROM            bak_jadwal INNER JOIN "+
                                                        " bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                        " bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                        " bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                            " WHERE(dbo.bak_mahasiswa.status  IN('A')) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 1998)  AND semester = @SemNewHitungJumlah AND bak_prog_study.id_prog_study = @IdMhsProdi "+
                            " GROUP BY  bak_mahasiswa.thn_angkatan "+
                        " ) as ProdiMhs LEFT OUTER JOIN "+
                        " ( "+
                            " SELECT MahasiswaAktif.thn_angkatan, COUNT(*) as jumlah_aktif  FROM "+
                            " ( "+
                                " SELECT      bak_mahasiswa.thn_angkatan, bak_mahasiswa.npm "+
                                " FROM         bak_jadwal INNER JOIN "+
                                                      " bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+ 
                                                      " bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                                      " bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm "+
                                " WHERE (bak_jadwal.semester = @SemNewHitungJumlah) AND (bak_prog_study.id_prog_study = @IdMhsProdi) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 1998) AND(bak_mahasiswa.status = 'A') "+
                                " group by  bak_mahasiswa.thn_angkatan, bak_mahasiswa.npm, bak_jadwal.semester, bak_prog_study.id_prog_study "+
                            " ) AS MahasiswaAktif "+
                            " group by MahasiswaAktif.thn_angkatan "+
                        " ) as MhsA on ProdiMhs.thn_angkatan = MhsA.thn_angkatan LEFT OUTER JOIN " +
                        " ( "+
                             " SELECT      bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan, COUNT(*) as jumlah_keluar "+
                             " FROM         bak_cuti_nonaktif INNER JOIN "+
                                                    " bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                    " bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+  
                              " WHERE semester = @SemNewHitungJumlah AND bak_mahasiswa.id_prog_study = @IdMhsProdi AND bak_cuti_nonaktif.status in ('K') "+
                              " GROUP BY  bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan "+
                        " ) as MhsK on ProdiMhs.thn_angkatan = MhsK.thn_angkatan LEFT OUTER JOIN "+
                        " ( "+
                              " SELECT      bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan, COUNT(*) as jumlah_nonaktif "+   
                              " FROM         bak_cuti_nonaktif INNER JOIN "+ 
                                                    " bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+ 
                                                    " bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+   
                              " WHERE semester = @SemNewHitungJumlah AND bak_mahasiswa.id_prog_study = @IdMhsProdi AND bak_cuti_nonaktif.status in ('N') "+    
                              " GROUP BY  bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan "+
                         " ) as MhsNonAktif on ProdiMhs.thn_angkatan = MhsNonAktif.thn_angkatan LEFT OUTER JOIN "+
                         " ( "+
                              " SELECT      bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan, COUNT(*) as jumlah_cuti "+    
                              " FROM         bak_cuti_nonaktif INNER JOIN "+
                                                    " bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+    
                                                    " bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study "+    
                              " WHERE semester = @SemNewHitungJumlah AND bak_mahasiswa.id_prog_study = @IdMhsProdi AND bak_cuti_nonaktif.status in ('C') "+    
                              " GROUP BY  bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.semester, bak_mahasiswa.thn_angkatan "+
                         " ) as MhsCuti on ProdiMhs.thn_angkatan = MhsCuti.thn_angkatan LEFT OUTER JOIN "+
                         " ( " +
                             " select thn_angkatan, COUNT(*) as jumlah_lulus  from bak_mahasiswa where status = 'L' AND smster_lls = @SemNewHitungJumlah AND id_prog_study = @IdMhsProdi  " +    
                             " group by thn_angkatan "+
                         " ) as MhsLulus  on ProdiMhs.thn_angkatan = MhsLulus.thn_angkatan "+
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@SemNewHitungJumlah", TopSemester);
                    CmdJadwal.Parameters.AddWithValue("@IdMhsProdi", IdProdi);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Tahun Angkatan");
                    TableJadwal.Columns.Add("Aktif");
                    TableJadwal.Columns.Add("Non Aktif");
                    TableJadwal.Columns.Add("Cuti");
                    TableJadwal.Columns.Add("Keluar");
                    TableJadwal.Columns.Add("Lulus");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelRekapAktif.Enabled = true;
                            this.PanelRekapAktif.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Tahun Angkatan"] = rdr["thn_angkatan"];

                                if (rdr["jumlah_aktif"] == DBNull.Value)
                                {
                                    datarow["Aktif"] = 0;
                                } else
                                {
                                    datarow["Aktif"] = rdr["jumlah_aktif"];
                                }     
                                                          
                                if (rdr["jumlah_nonaktif"] == DBNull.Value)
                                {
                                    datarow["Non Aktif"] = 0;
                                }
                                else
                                {
                                    datarow["Non Aktif"] = rdr["jumlah_nonaktif"];
                                }

                                if (rdr["jumlah_cuti"] == DBNull.Value)
                                {
                                    datarow["Cuti"] = 0;
                                }
                                else
                                {
                                    datarow["Cuti"] = rdr["jumlah_cuti"];
                                }

                                if (rdr["jumlah_keluar"] == DBNull.Value)
                                {
                                    datarow["Keluar"] = 0;
                                }
                                else
                                {
                                    datarow["Keluar"] = rdr["jumlah_keluar"];
                                }

                                if (rdr["jumlah_lulus"] == DBNull.Value)
                                {
                                    datarow["Lulus"] = 0;
                                }
                                else
                                {
                                    datarow["Lulus"] = rdr["jumlah_lulus"];
                                }

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvMhsAktif.DataSource = TableJadwal;
                            this.GvMhsAktif.DataBind();

                        }
                        else
                        {
                            this.PanelRekapAktif.Enabled = false;
                            this.PanelRekapAktif.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GvMhsAktif.DataSource = TableJadwal;
                            GvMhsAktif.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
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

        protected void PopulateRekapBimbinganKRS(string TopSemester, string IdProdi)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdBimbingan = new SqlCommand(@"
                        SELECT ROW_NUMBER() OVER( ORDER by MhsDibimbng.dosen ASC) AS Nomor, MhsDibimbng.no,MhsDibimbng.id_prog_study,MhsDibimbng.nidn,MhsDibimbng.dosen,MhsDibimbng.jumlah as JumlhMhsKRS,MhsDivalidasi.jumlah as MhsValid,MhsBlmValid.jumlah as MhsBlmValid FROM 
                        (
	                        SELECT       bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama AS dosen,bak_dosen.aktif, bak_persetujuan_krs.semester, COUNT (*) as jumlah
	                        FROM            bak_dosen INNER JOIN 
						                          bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali LEFT OUTER JOIN 
						                          bak_persetujuan_krs ON bak_mahasiswa.npm = bak_persetujuan_krs.npm 
	                        WHERE (bak_mahasiswa.status NOT IN('K', 'L')) AND (semester = @TopSemester ) AND (id_prog_study=@IdMhsProdi)
	                        GROUP BY bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama,bak_dosen.aktif, bak_persetujuan_krs.semester
                        ) AS MhsDibimbng LEFT OUTER JOIN
                        (
	                        SELECT       bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama AS dosen,bak_dosen.aktif, bak_persetujuan_krs.semester,bak_persetujuan_krs.val, COUNT (*) as jumlah
	                        FROM            bak_dosen INNER JOIN 
						                          bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali LEFT OUTER JOIN 
						                          bak_persetujuan_krs ON bak_mahasiswa.npm = bak_persetujuan_krs.npm 
	                        WHERE (bak_mahasiswa.status NOT IN('K', 'L')) AND (semester = @TopSemester )AND (val=1) AND (id_prog_study=@IdMhsProdi) 
	                        GROUP BY bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama,bak_dosen.aktif, bak_persetujuan_krs.semester,bak_persetujuan_krs.val
                        )AS MhsDivalidasi on MhsDibimbng.no = MhsDivalidasi.no LEFT OUTER JOIN
                        (
	                        SELECT       bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama AS dosen,bak_dosen.aktif, bak_persetujuan_krs.semester,bak_persetujuan_krs.val, COUNT (*) as jumlah
	                        FROM            bak_dosen INNER JOIN 
						                          bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali LEFT OUTER JOIN 
						                          bak_persetujuan_krs ON bak_mahasiswa.npm = bak_persetujuan_krs.npm 
	                        WHERE (bak_mahasiswa.status NOT IN('K', 'L')) AND (semester = @TopSemester ) AND (val=0 OR val is null) AND (id_prog_study=@IdMhsProdi) 
	                        GROUP BY bak_dosen.no,bak_dosen.nidn, bak_mahasiswa.id_prog_study, bak_dosen.nama,bak_dosen.aktif, bak_persetujuan_krs.semester,bak_persetujuan_krs.val
                        ) AS MhsBlmValid on MhsDibimbng.no = MhsBlmValid.no                    
                    ", con);
                    CmdBimbingan.CommandType = System.Data.CommandType.Text;

                    CmdBimbingan.Parameters.AddWithValue("@TopSemester", TopSemester);
                    CmdBimbingan.Parameters.AddWithValue("@IdMhsProdi", IdProdi);

                    DataTable TableBimbingan = new DataTable();
                    TableBimbingan.Columns.Add("No");
                    TableBimbingan.Columns.Add("NIDN");
                    TableBimbingan.Columns.Add("Nama");
                    TableBimbingan.Columns.Add("Jumlah Mahasiswa KRS");
                    TableBimbingan.Columns.Add("Sudah Di Validasi");
                    TableBimbingan.Columns.Add("Belum Di Validasi");

                    using (SqlDataReader rdr = CmdBimbingan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableBimbingan.NewRow();

                                datarow["No"] = rdr["Nomor"];
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["Nama"] = rdr["dosen"];
                                datarow["Jumlah Mahasiswa KRS"] = rdr["JumlhMhsKRS"];
                                datarow["Sudah Di Validasi"] = rdr["MhsValid"];
                                datarow["Belum Di Validasi"] = rdr["MhsBlmValid"];

                                TableBimbingan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVBimbinganKRS.DataSource = TableBimbingan;
                            this.GVBimbinganKRS.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableBimbingan.Rows.Clear();
                            TableBimbingan.Clear();
                            GVBimbinganKRS.DataSource = TableBimbingan;
                            GVBimbinganKRS.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
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

        protected void GetJenjang()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("" +
                    "select jenjang from bak_prog_study where id_prog_study=@IdProdi" +
                    "", con);

                    CmdJadwal.CommandType = System.Data.CommandType.Text;
                    CmdJadwal.Parameters.AddWithValue("IdProdi", this.Session["level"].ToString().Trim());

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                _Jenjang = rdr["jenjang"].ToString().Trim();
                            }
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
            this.LbSemAktif1.Text ="("+ this.DLTahun.SelectedValue.ToString().Trim() + this.DLSemester.SelectedValue.ToString().Trim() +")";

            _TotalAktif = 0;
            _TotalNonAktif = 0;
            _TotalCuti = 0;
            _TotalKeluar = 0;
            _TotalLulus = 0;

            PopulateMhsAktif(this.DLTahun.SelectedValue.ToString().Trim() + this.DLSemester.SelectedValue.ToString().Trim(), this.Session["level"].ToString().Trim());
        }

        protected void GvMhsAktif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 Aktif = Convert.ToInt32(e.Row.Cells[1].Text);
                _TotalAktif += Aktif;

                Int32 NonAktif = Convert.ToInt32(e.Row.Cells[2].Text);
                _TotalNonAktif += NonAktif;

                Int32 Cuti = Convert.ToInt32(e.Row.Cells[3].Text);
                _TotalCuti += Cuti;

                Int32 Keluar = Convert.ToInt32(e.Row.Cells[4].Text);
                _TotalKeluar += Keluar;

                Int32 Lulus = Convert.ToInt32(e.Row.Cells[5].Text);
                _TotalLulus += Lulus;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "JUMLAH";

                e.Row.Cells[1].Text = _TotalAktif.ToString();
                e.Row.Cells[2].Text = _TotalNonAktif.ToString();
                e.Row.Cells[3].Text = _TotalCuti.ToString();
                e.Row.Cells[4].Text = _TotalKeluar.ToString();
                e.Row.Cells[5].Text = _TotalLulus.ToString();

            }
        }

        protected void BtnBimKRS_Click(object sender, EventArgs e)
        {
            this.LbSemAktif2.Text = "("+ this.DLTahun2.SelectedValue.ToString().Trim() + this.DLSemester2.SelectedValue.ToString().Trim() +")";

            PopulateRekapBimbinganKRS(this.DLTahun2.SelectedValue.ToString().Trim() + this.DLSemester2.SelectedValue.ToString().Trim(), this.Session["level"].ToString().Trim());
        }
    }
}