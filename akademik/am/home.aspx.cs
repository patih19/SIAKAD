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

namespace akademik.am
{
    //public partial class WebForm12 : System.Web.UI.Page
    public partial class WebForm12 : Bak_staff
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //this.PanelUji.Enabled = false;
                //this.PanelUji.Visible = false;

                _TotalAktif = 0;
                _TotalNonAktif = 0;
                _TotalCuti = 0;
                _TotalKeluar = 0;
                _TotalLulus = 0;

                this.PanelRekapAktif.Enabled = false;
                this.PanelRekapAktif.Visible = false;

                // tahun akademik //
                TahunAkademik();

                // open last semester //
                LastSemester();
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            _TotalAktif = 0;
            _TotalNonAktif = 0;
            _TotalCuti = 0;
            _TotalKeluar = 0;
            _TotalLulus = 0;

            PopulateMhsAktif(this.DLTahun.SelectedValue.ToString().Trim() + this.DLSemester.SelectedValue.ToString().Trim());
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
                        "WHERE jenjang not in ('s2') " +
                        "GROUP BY bak_jadwal.semester " +
                        "ORDER BY semester DESC " +
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                PopulateMhsAktif(rdr["semester"].ToString().Trim());
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

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 3 bak_kal.thn AS thn, CAST(bak_kal.thn  AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +1 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN ('S1') group by thn ORDER BY thn DESC", con);
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

        protected void PopulateMhsAktif( string semester)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand(""+
                        "SELECT A.id_prog_study, A.prog_study, A.jumlah_aktif, N.jumlah_nonaktif, C.jumlah_cuti, K.jumlah_keluar, L.jumlah_lulus FROM "+
                        "( "+
                            //-- ============== new perhitungan Aktif ================= -- 
                            "SELECT id_prog_study, prog_study, semester, setatus, COUNT(*) AS jumlah_aktif FROM "+
                            "( "+
                                "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study, 'A' AS setatus "+
                                "FROM            bak_jadwal INNER JOIN "+
                                                            "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                            "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                            "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                                "WHERE(dbo.bak_mahasiswa.status  IN('A')) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 2000) AND bak_prog_study.jenjang IN('S2') AND semester = @SemNewHitungJumlah "+
                                "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study "+
                                "UNION ALL "+
                                "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study, 'A' AS setatus "+
                                "FROM            bak_jadwal INNER JOIN "+
                                                            "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                            "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                            "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                                "WHERE (dbo.bak_mahasiswa.status  IN('A')) AND(LEFT(bak_mahasiswa.thn_angkatan, 4) > 2000) AND bak_prog_study.jenjang NOT IN ('S2') AND dbo.bak_jadwal.semester = @SemNewHitungJumlah "+
                                "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study "+
                            ") AS StatusMhs "+
                            "GROUP BY StatusMhs.id_prog_study, StatusMhs.prog_study, StatusMhs.semester, StatusMhs.setatus "+
                        ") AS A LEFT OUTER JOIN "+
                        "( "+
                            //-- ============== new perhitungan Non Aktif ================= --
                            "SELECT id_prog_study, prog_study, semester, setatus, COUNT(*) AS jumlah_nonaktif FROM "+
                            "( "+
                                "SELECT        bak_cuti_nonaktif.npm, bak_mahasiswa.nama, bak_cuti_nonaktif.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.status AS setatus "+
                                "FROM            bak_cuti_nonaktif INNER JOIN "+ 
                                                            "bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                            "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study AND bak_cuti_nonaktif.semester = @SemNewHitungJumlah AND dbo.bak_cuti_nonaktif.status = 'N' "+
                            ") AS StatusMhs "+
                            "GROUP BY StatusMhs.id_prog_study, prog_study, StatusMhs.semester, StatusMhs.setatus "+
                        ") AS N ON N.id_prog_study = A.id_prog_study LEFT OUTER JOIN "+
                        "( "+
                            //-- ============== new perhitungan Keluar ================= --
                            "SELECT id_prog_study, prog_study, semester, setatus, COUNT(*) AS jumlah_keluar FROM "+
                            "( "+
                                "SELECT        bak_cuti_nonaktif.npm, bak_mahasiswa.nama, bak_cuti_nonaktif.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.status AS setatus "+
                                "FROM            bak_cuti_nonaktif INNER JOIN "+
                                                            "bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                            "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study AND bak_cuti_nonaktif.semester = @SemNewHitungJumlah AND dbo.bak_cuti_nonaktif.status = 'K' "+
                            ") AS StatusMhs "+

                            "GROUP BY StatusMhs.id_prog_study, prog_study, StatusMhs.semester, StatusMhs.setatus "+
                        ") AS K ON K.id_prog_study = A.id_prog_study LEFT OUTER JOIN "+
                        "( "+
                            //-- ============== new perhitungan Cuti ================= --
                            "SELECT id_prog_study, prog_study, semester, setatus, COUNT(*) AS jumlah_cuti FROM "+
                            "( "+
                                "SELECT        bak_cuti_nonaktif.npm, bak_mahasiswa.nama, bak_cuti_nonaktif.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_cuti_nonaktif.status AS setatus "+
                                "FROM            bak_cuti_nonaktif INNER JOIN "+
                                                            "bak_mahasiswa ON bak_cuti_nonaktif.npm = bak_mahasiswa.npm INNER JOIN "+
                                                            "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study AND bak_cuti_nonaktif.semester = @SemNewHitungJumlah AND dbo.bak_cuti_nonaktif.status = 'C' "+
                            ") AS StatusMhs "+
                            "GROUP BY StatusMhs.id_prog_study, prog_study, StatusMhs.semester, StatusMhs.setatus "+
                        ") AS C ON C.id_prog_study = A.id_prog_study LEFT OUTER JOIN "+
                        "( "+
                            //-- =================== new perhitungan Lulus ===================
                            "SELECT M.id_prog_study, prog_study, COUNT(*) AS jumlah_lulus FROM dbo.bak_mahasiswa AS M "+
                            "INNER JOIN dbo.bak_prog_study AS P ON P.id_prog_study = M.id_prog_study "+
                            "WHERE M.status = 'L' AND M.smster_lls = @SemNewHitungJumlah "+
                            "GROUP BY M.id_prog_study, prog_study "+
                        ") AS L ON L.id_prog_study = A.id_prog_study "+
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@SemNewHitungJumlah", semester);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Program Studi");
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
                                datarow["Program Studi"] = rdr["prog_study"];
                                datarow["Aktif"] = rdr["jumlah_aktif"];
                                if (rdr["jumlah_nonaktif"] == DBNull.Value)
                                {
                                    datarow["Non Aktif"] = 0;
                                } else
                                {
                                    datarow["Non Aktif"] = rdr["jumlah_nonaktif"];
                                }

                                if (rdr["jumlah_cuti"] == DBNull.Value)
                                {
                                    datarow["Cuti"] = 0;
                                } else
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
    }
}