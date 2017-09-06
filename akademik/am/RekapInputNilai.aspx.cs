using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace akademik.am
{
    public partial class WebForm43 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelFakultas.Enabled = false;
                this.PanelFakultas.Visible = false;

                this.PanelListNilai.Enabled = false;
                this.PanelListNilai.Visible = false;
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedValue == "Tahun" || this.DLTahun.SelectedValue == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DlSemester.SelectedValue == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand(" "+
                    "SELECT        bak_fakultas.kode, bak_fakultas.fak_name, bak_jadwal.semester "+
                    "INTO	#TempFakultas	"+
                    "FROM            bak_jadwal INNER JOIN "+
                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN "+
                                            " bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN "+
                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                             "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                             "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                             "bak_fakultas ON bak_prog_study.id_fakultas = bak_fakultas.kode "+
                    "WHERE        (bak_jadwal.semester = @Semester) "+
                    "GROUP BY bak_jadwal.no_jadwal, bak_jadwal.kode_makul, bak_makul.makul, bak_makul.sks, bak_dosen.nidn, bak_dosen.nama, bak_jadwal.kelas, bak_jadwal.jenis_kelas, bak_jadwal.semester, "+
                                             "bak_prog_study.prog_study, bak_jadwal.id_prog_study, bak_prog_study.id_prog_study, bak_fakultas.kode, bak_fakultas.fak_name "+

                    "SELECT        bak_jadwal.no_jadwal, bak_nilai.nilai, COUNT(*) AS jumlah_kurang, bak_jadwal.id_prog_study, bak_prog_study.prog_study, bak_fakultas.kode, bak_fakultas.fak_name, bak_jadwal.semester "+
                    "INTO #TempFakultasKurang "+
                    "FROM            bak_jadwal INNER JOIN "+
                                             "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN "+
                                             "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                             "bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN "+
                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                             "bak_fakultas ON bak_prog_study.id_fakultas = bak_fakultas.kode LEFT OUTER JOIN "+
                                             "bak_nilai ON bak_jadwal.kode_makul = bak_nilai.kode_makul AND bak_mahasiswa.npm = bak_nilai.npm AND bak_nilai.semester = bak_jadwal.semester "+
                    "WHERE        (bak_nilai.nilai IS NULL) AND (bak_jadwal.semester = @semester) "+
                    "GROUP BY bak_jadwal.no_jadwal, bak_nilai.nilai, bak_prog_study.prog_study, bak_jadwal.semester, bak_jadwal.id_prog_study, bak_fakultas.kode, bak_fakultas.fak_name "+

                    "SELECT x.kode, x.fak_name, x.jumlah_rombel, y.jumlah_kurang, CAST((CAST(jumlah_kurang AS DECIMAL)/CAST(jumlah_rombel AS DECIMAL))*100 AS DECIMAL (4,2)) AS kurang, CAST(100 - ((CAST(jumlah_kurang AS DECIMAL)/CAST(jumlah_rombel AS DECIMAL))*100) AS DECIMAL (4,2)) AS lengkap, x.semester  FROM "+
                    "( "+
	                    "SELECT kode,fak_name,semester, COUNT(*) AS jumlah_rombel FROM #TempFakultas GROUP BY kode,fak_name,semester "+
                    ") AS x "+
                    "LEFT OUTER JOIN "+
                    "( "+
	                    "SELECT kode,fak_name, COUNT(*) jumlah_kurang FROM #TempFakultasKurang GROUP BY kode,fak_name "+
                    ") AS y "+
                    "ON y.kode = x.kode "+

                    "DROP TABLE #TempFakultasKurang "+
                    "DROP TABLE #TempFakultas "+
               "", con);
                CmdMakul.CommandType = System.Data.CommandType.Text;

                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DlSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Fakultas");
                TableMakul.Columns.Add("Jumlah Rombel");
                TableMakul.Columns.Add("Rombel Kurang");
                TableMakul.Columns.Add("Kurang (%)");
                TableMakul.Columns.Add("Lengkap (%)");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelFakultas.Enabled = true;
                        this.PanelFakultas.Visible = true;

                        this.PanelListNilai.Enabled = false;
                        this.PanelListNilai.Visible = false;
                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode"].ToString();
                            datarow["Fakultas"] = rdr["fak_name"];
                            datarow["Jumlah Rombel"] = rdr["jumlah_rombel"];
                            datarow["Rombel Kurang"] = rdr["jumlah_kurang"];
                            datarow["Kurang (%)"] = rdr["kurang"];
                            datarow["Lengkap (%)"] = rdr["lengkap"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVFakultas.DataSource = TableMakul;
                        this.GVFakultas.DataBind();

                    }
                    else
                    {
                        this.PanelFakultas.Enabled = false;
                        this.PanelFakultas.Visible = false;

                        this.PanelListNilai.Enabled = false;
                        this.PanelListNilai.Visible = false;

                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVFakultas.DataSource = TableMakul;
                        GVFakultas.DataBind();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }
        }

        protected void LnkLihat_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            //Response.Write(this.GVFakultas.Rows[index].Cells[0].Text);

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand(""+
                    "SELECT d.no_jadwal, d.kode_makul, d.makul, d.sks, d.nidn, d.nama, d.kelas, d.jenis_kelas, d.jumlah, "+
							                     "d.id_prog_study, d.prog_study, e.jumlah_kurang FROM "+
                    "( "+
                    "SELECT        bak_jadwal.no_jadwal, bak_jadwal.kode_makul, bak_makul.makul, bak_makul.sks, bak_dosen.nidn, bak_dosen.nama, bak_jadwal.kelas, bak_jadwal.jenis_kelas, COUNT(bak_jadwal.no_jadwal) AS jumlah, "+
                                             "bak_prog_study.id_prog_study, bak_prog_study.prog_study, bak_fakultas.kode, bak_fakultas.fak_name "+
                    "FROM            bak_jadwal INNER JOIN "+
                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN "+
                                             "bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN "+
                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                             "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                             "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                             "bak_fakultas ON bak_prog_study.id_fakultas = bak_fakultas.kode "+
                    "WHERE        (bak_jadwal.semester = @semester) AND (bak_fakultas.kode = @KodeFakultas) " +
                    "GROUP BY bak_jadwal.no_jadwal, bak_jadwal.kode_makul, bak_makul.makul, bak_makul.sks, bak_dosen.nidn, bak_dosen.nama, bak_jadwal.kelas, bak_jadwal.jenis_kelas, bak_jadwal.semester, "+
                                             "bak_prog_study.prog_study, bak_jadwal.id_prog_study, bak_prog_study.id_prog_study, bak_fakultas.kode, bak_fakultas.fak_name "+
                    ") AS d "+
                    "LEFT OUTER JOIN "+
                    "( "+
                    "SELECT        bak_jadwal.no_jadwal, bak_nilai.nilai, COUNT(*) AS jumlah_kurang, bak_jadwal.semester, bak_jadwal.id_prog_study, bak_prog_study.prog_study "+
                    "FROM            bak_jadwal INNER JOIN "+
                                             "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN "+
                                             "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                             "bak_dosen ON bak_jadwal.nidn = bak_dosen.nidn INNER JOIN "+
                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                             "bak_fakultas ON bak_prog_study.id_fakultas = bak_fakultas.kode LEFT OUTER JOIN "+
                                             "bak_nilai ON bak_jadwal.kode_makul = bak_nilai.kode_makul AND bak_mahasiswa.npm = bak_nilai.npm AND bak_nilai.semester = bak_jadwal.semester "+
                    "WHERE        (bak_nilai.nilai IS NULL) AND (bak_jadwal.semester = @semester) AND (bak_fakultas.kode = @KodeFakultas) "+
                    "GROUP BY bak_jadwal.no_jadwal, bak_nilai.nilai, bak_prog_study.prog_study, bak_jadwal.semester, bak_jadwal.id_prog_study "+
                    ") AS e "+
                    "ON e.no_jadwal = d.no_jadwal "+
                    "WHERE e.jumlah_kurang IS NOT NULL "+
                    "ORDER BY d.id_prog_study,d.nama  ASC "+                    
                    "", con);
                CmdMakul.CommandType = System.Data.CommandType.Text;

                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DlSemester.SelectedItem.Text);
                CmdMakul.Parameters.AddWithValue("@KodeFakultas", this.GVFakultas.Rows[index].Cells[0].Text.Trim());

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");
                TableMakul.Columns.Add("Dosen");
                TableMakul.Columns.Add("Kelas");
                TableMakul.Columns.Add("Peserta");
                TableMakul.Columns.Add("Kurang");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelListNilai.Enabled = true;
                        this.PanelListNilai.Visible = true;

                        this.LbFakultas.Text = this.GVFakultas.Rows[index].Cells[1].Text.Trim();

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"].ToString();
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Peserta"] = rdr["jumlah"];
                            datarow["Kurang"] = rdr["jumlah_kurang"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVListNilai.DataSource = TableMakul;
                        this.GVListNilai.DataBind();
                    }
                    else
                    {
                        this.PanelListNilai.Enabled = false;
                        this.PanelListNilai.Visible = false;

                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVListNilai.DataSource = TableMakul;
                        GVListNilai.DataBind();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }

        }

        protected void GVListNilai_PreRender(object sender, EventArgs e)
        {
            if (this.GVListNilai.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVListNilai.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVListNilai.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVListNilai.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void GVFakultas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVFakultas.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void GVFakultas_PreRender(object sender, EventArgs e)
        {
            if (this.GVFakultas.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVFakultas.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVFakultas.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVAktif.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

    }
}