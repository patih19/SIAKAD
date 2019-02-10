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
    public partial class RekapPenawaran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(! Page.IsPostBack)
            {
                LoadRekap();
            }
        }

        protected void LoadRekap()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();

                    SqlCommand CmdRekapPemesanan = new SqlCommand(@"
                    DECLARE @TopSemRekapPemesanan VARCHAR(5)
                    DECLARE @TopNoPenawaran BIGINT
                    SELECT  TOP 1     @TopNoPenawaran=bak_penawaran_makul.no_penawaran,@TopSemRekapPemesanan=bak_penawaran_makul.semester
                    FROM            bak_makul INNER JOIN
                                             bak_penawaran_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                    WHERE id_prog_study=@ProdiRekapPemesanan
                    ORDER BY dbo.bak_penawaran_makul.semester DESC

                    SELECT         bak_makul.id_prog_study, bak_makul.kode_makul, bak_makul.makul,bak_makul.sks,bak_mahasiswa.kelas,bak_penawaran_makul.semester, COUNT(*) AS jumlah
                    FROM            bak_krs_penawaran INNER JOIN
                                             bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran INNER JOIN
                                             bak_makul ON bak_penawaran_makul.kode_makul = bak_makul.kode_makul INNER JOIN
                                             bak_mahasiswa ON bak_krs_penawaran.npm = bak_mahasiswa.npm
                    WHERE bak_makul.id_prog_study=@ProdiRekapPemesanan AND dbo.bak_penawaran_makul.semester=@TopSemRekapPemesanan
                    GROUP BY bak_penawaran_makul.kode_makul,bak_makul.sks, bak_penawaran_makul.semester, bak_makul.id_prog_study, bak_makul.makul, bak_mahasiswa.kelas, bak_makul.kode_makul
                    ORDER BY bak_penawaran_makul.kode_makul
                    ", con);

                    CmdRekapPemesanan.CommandType = System.Data.CommandType.Text;

                    CmdRekapPemesanan.Parameters.AddWithValue("@ProdiRekapPemesanan", this.Session["level"].ToString());

                    DataTable TablePenawaran = new DataTable();
                    TablePenawaran.Columns.Add("Kode Makul");
                    TablePenawaran.Columns.Add("Mata Kuliah");
                    TablePenawaran.Columns.Add("SKS");
                    TablePenawaran.Columns.Add("Kelas");
                    TablePenawaran.Columns.Add("Semester");
                    TablePenawaran.Columns.Add("Jumlah");

                    using (SqlDataReader rdr = CmdRekapPemesanan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TablePenawaran.NewRow();
                                datarow["Kode Makul"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Semester"] = rdr["semester"];
                                datarow["Jumlah"] = rdr["jumlah"];

                                TablePenawaran.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVRekapPenawaran.DataSource = TablePenawaran;
                            this.GVRekapPenawaran.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TablePenawaran.Rows.Clear();
                            TablePenawaran.Clear();
                            GVRekapPenawaran.DataSource = TablePenawaran;
                            GVRekapPenawaran.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Rekap Penawaran Tidak Ditemukan');", true);
                            return;
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

        protected void GVRekapPenawaran_PreRender(object sender, EventArgs e)
        {
            if (this.GVRekapPenawaran.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVRekapPenawaran.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVRekapPenawaran.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

    }
}