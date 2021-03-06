﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace akademik.am
{
    //public partial class AktifKRS : System.Web.UI.Page
    public partial class AktifKRS : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMhsKrs.Enabled = false;
                this.PanelMhsKrs.Visible = false;
            }
        }

        protected void BtnAktifKRS_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdJadwal = new SqlCommand("" +
                        " DECLARE @ColName NVARCHAR(MAX) = '' " +
                        " DECLARE @SQL NVARCHAR(max) = '' " +

                        "SELECT * INTO #TempMhsAktif FROM ( "+
	                        "SELECT MhsKrs.npm, MhsKrs.nama, MhsKrs.semester, MhsKrs.prog_study, MhsKrs.id_prog_study, MhsKrs.thn_angkatan FROM( "+
                                "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.id_prog_study, bak_mahasiswa.thn_angkatan "+
                                "FROM            bak_jadwal INNER JOIN "+
                                                        "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                        "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                        "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                                "WHERE (bak_jadwal.semester = @semester) "+
                                "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.id_prog_study, bak_mahasiswa.thn_angkatan "+
                            ") AS MhsKrs LEFT OUTER JOIN "+
                            "( "+
                                "SELECT npm, status FROM bak_cuti_nonaktif WHERE  semester = @semester "+
                            ") AS StatusBerjalan on MhsKRS.npm = StatusBerjalan.npm "+
                            "WHERE StatusBerjalan.status = 'A' OR StatusBerjalan.status IS NULL "+
                        ") AS MhsAktif "+

                        " Select prog_study AS PROGRAM_STUDI, thn_angkatan, COUNT(*) as jumlah " +
                        " INTO #TempPivot " +
                        " from #TempMhsAktif group by prog_study,thn_angkatan " +
                        " ORDER BY prog_study ASC, jumlah DESC " +

                        " SELECT @ColName += QUOTENAME(bak_mahasiswa.thn_angkatan) + ',' " +
                        " FROM            bak_jadwal INNER JOIN " +
                                                 " bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN " +
                                                 " bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN " +
                                                 " bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study " +
                        " WHERE(bak_jadwal.semester = @semester) " +
                        " GROUP BY bak_mahasiswa.thn_angkatan " +
                        " ORDER BY thn_angkatan ASC " +

                        " IF(@ColName = '') " +
                        " BEGIN " +
                            " RAISERROR ('MAHASISWA TIDAK DITEMUKAN',16,10) " +
                            " RETURN " +
                        " END " +

                        "SET @ColName = LEFT(@ColName, LEN(@ColName) - 1) " +
                        " SET @SQL = " +
                        " 'SELECT PROGRAM_STUDI,' + @ColName + ' FROM #TempPivot PIVOT " +
                        " ( " +
                            " SUM(jumlah) " +
                            " FOR thn_angkatan " +
                            " IN('+ @ColName +') " +
                        " ) AS PivotTable ' " +

                        " EXECUTE sp_executesql @SQL " +

                        " DROP TABLE #TempMhsAktif " +
                        " DROP TABLE #TempPivot " +
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);
                    SqlDataAdapter da = new SqlDataAdapter(CmdJadwal);

                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.PanelMhsKrs.Enabled = true;
                        this.PanelMhsKrs.Visible = true;

                        this.GvMhsAktifKrs.DataSource = ds;
                        this.GvMhsAktifKrs.DataBind();
                    }
                    else
                    {
                        this.PanelMhsKrs.Enabled = false;
                        this.PanelMhsKrs.Visible = false;
                    }
                }

            } catch (Exception ex)
            {
                this.PanelMhsKrs.Enabled = false;
                this.PanelMhsKrs.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GvMhsAktifKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}