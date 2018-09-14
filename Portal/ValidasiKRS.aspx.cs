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
    public partial class ValidasiKRS : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                PopulateDosenProdi();
            }
        }

        protected void PopulateDosenProdi()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand(" "+
                " DECLARE @TopSemester VARCHAR(5) " +
                " SELECT TOP 1 @TopSemester = semester "+
                " FROM            bak_jadwal WHERE id_prog_study = @id_prodi " +
                " GROUP BY id_prog_study, semester "+
                " ORDER BY semester DESC "+

                " SELECT        ROW_NUMBER() over(ORDER BY dbo.bak_dosen.nama ASC) AS nomor, bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.id_prog_study, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status, bak_dosen.nidn, bak_dosen.nama AS dosen, bak_persetujuan_krs.semester, " +
                                        " bak_persetujuan_krs.val, bak_persetujuan_krs.tgl "+
                " FROM            bak_dosen INNER JOIN "+
                                         " bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali LEFT OUTER JOIN "+
                                         " bak_persetujuan_krs ON bak_mahasiswa.npm = bak_persetujuan_krs.npm "+
                " WHERE (bak_mahasiswa.id_prog_study = @id_prodi) AND(bak_mahasiswa.status NOT IN('K', 'L')) AND semester = @TopSemester "+
                " GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.id_prog_study, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status, bak_dosen.nidn, bak_dosen.nama, bak_persetujuan_krs.semester, "+
                                         " bak_persetujuan_krs.val, bak_persetujuan_krs.tgl "+
                " ORDER BY dbo.bak_dosen.nama ASC "+
                " ", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("No");
                TableDosen.Columns.Add("Dosen");
                TableDosen.Columns.Add("NPM");
                TableDosen.Columns.Add("Nama");
                TableDosen.Columns.Add("Semester");
                TableDosen.Columns.Add("Keterangan");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["No"] = rdr["nomor"];
                            datarow["Dosen"] = rdr["dosen"];
                            datarow["NPM"] = rdr["npm"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["Semester"] = rdr["semester"];

                            if (rdr["val"].ToString().Trim() == "1")
                            {
                                datarow["Keterangan"] = "Sudah Divalidasi";
                            } else
                            {
                                datarow["Keterangan"] = "Dalam Proses";
                            }
                            

                            TableDosen.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GvMonitorValidKRS.DataSource = TableDosen;
                        this.GvMonitorValidKRS.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GvMonitorValidKRS.DataSource = TableDosen;
                        GvMonitorValidKRS.DataBind();
                    }
                }
            }
        }

        protected void GvMonitorValidKRS_PreRender(object sender, EventArgs e)
        {
            if (this.GvMonitorValidKRS.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GvMonitorValidKRS.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GvMonitorValidKRS.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
}