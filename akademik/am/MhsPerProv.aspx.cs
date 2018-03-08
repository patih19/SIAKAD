using System;
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
    public partial class MhsPerProv : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMhsProv.Enabled = false;
                this.PanelMhsProv.Visible = false;
            }
        }

        protected void BtnProvMhsAktif_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdJadwal = new SqlCommand("" +
                        "SELECT * FROM( "+
                            "SELECT StatusMhs.prov AS Provinsi, COUNT(*) AS Jumlah FROM "+
                            "( "+
                                //-- === Mhs KRS === --
                                "SELECT MhsKRS.id_prog_study, MhsKRS.nama, MhsKRS.npm, MhsKRS.prov, MhsKRS.prog_study, MhsKRS.semester, StatusBerjalan.status as setatus FROM( "+
                                    "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.prov, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study "+
                                    "FROM            bak_jadwal INNER JOIN "+
                                                                "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                                "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                                "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                                    "WHERE (LEFT(bak_mahasiswa.thn_angkatan, 4) > 1990) AND bak_prog_study.jenjang IN('S2') AND semester = @semester " +
                                    "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.prov, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study "+
                                    "UNION ALL "+
                                    "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.prov, bak_jadwal.semester, bak_prog_study.id_prog_study, bak_prog_study.prog_study "+
                                    "FROM            bak_jadwal INNER JOIN "+
                                                                "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                                "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm INNER JOIN "+
                                                                "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                                    "WHERE (LEFT(bak_mahasiswa.thn_angkatan, 4) > 1990) AND bak_prog_study.jenjang NOT IN ('S2') AND dbo.bak_jadwal.semester = @semester " +
                                    "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.prov, bak_jadwal.semester, bak_prog_study.prog_study, bak_mahasiswa.status, bak_mahasiswa.thn_angkatan, bak_prog_study.id_prog_study "+
                                ") AS MhsKRS LEFT OUTER JOIN "+
                                "( "+
                                    //-- == Status Berjalan == --
                                    "SELECT npm, status FROM bak_cuti_nonaktif WHERE  semester = @semester " +
                                ") AS StatusBerjalan on MhsKRS.npm = StatusBerjalan.npm "+
                            ") AS StatusMhs "+
                            "WHERE StatusMhs.setatus = 'A' or StatusMhs.setatus is NULL "+
                            "GROUP BY StatusMhs.prov "+
                        ") as MhsAktifKRS" +
                        "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                    SqlDataAdapter da = new SqlDataAdapter(CmdJadwal);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.PanelMhsProv.Enabled = true;
                        this.PanelMhsProv.Visible = true;

                        this.GvMhsAktifPerProv.DataSource = ds;
                        this.GvMhsAktifPerProv.DataBind();
                    }
                    else
                    {
                        this.PanelMhsProv.Enabled = false;
                        this.PanelMhsProv.Visible = false;
                    }

                    //using (SqlDataAdapter sda = new SqlDataAdapter(CmdJadwal))
                    //{

                    //    using (DataTable dt = new DataTable())
                    //    {
                    //        sda.Fill(dt);
                    //        GvMhsAktifPerProv.DataSource = dt;
                    //        GvMhsAktifPerProv.DataBind();

                    //        //Calculate Sum and display in Footer Row
                    //        decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Jumlah"));
                    //        GvMhsAktifPerProv.FooterRow.Cells[0].Text = "Total";
                    //        GvMhsAktifPerProv.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    //        GvMhsAktifPerProv.FooterRow.Cells[1].Text = total.ToString("N2");
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                this.PanelMhsProv.Enabled = false;
                this.PanelMhsProv.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        int totalUnitPrice = 0;

        protected void GvMhsAktifPerProv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Loop thru each data row and compute total unit price and quantity sold
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalUnitPrice += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Jumlah"));
            }
            // Display totals in the gridview footer
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = totalUnitPrice.ToString();
                e.Row.Cells[1].Font.Bold = true;

            }
        }
    }
}