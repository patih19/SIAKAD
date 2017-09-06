using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.Diagnostics;
using System.IO;

namespace akademik.am
{
    public partial class PrintKRS3 : System.Web.UI.Page
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    // ------- Read Mahasiswa ----------
                    mhs.ReadMahasiswa(Request.QueryString["nim"]);

                    LbNama.Text = mhs.nama.ToString();
                    LbJenisKelas.Text = mhs.kelas.ToString();
                    LbProdi.Text = mhs.Prodi.ToString();
                    LbNpm.Text = mhs.npm.ToString();
                    LbMhsName.Text = mhs.nama.ToString();

                    //------ Semester ------//
                    string smstr = Request.QueryString["semester"].ToString();
                    LbTahun.Text = smstr.Substring(0, 4);
                    LbSemester.Text = smstr.Substring(4, 1);

                    //2. ---------- Gridview SKS ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        // --------------------- Fill Gridview  ------------------------
                        SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                        CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdListKRS.Parameters.AddWithValue("@npm", Request.QueryString["nim"]);
                        CmdListKRS.Parameters.AddWithValue("@semester", Request.QueryString["semester"]);

                        DataTable TableKRS = new DataTable();
                        TableKRS.Columns.Add("Kode");
                        TableKRS.Columns.Add("Mata Kuliah");
                        TableKRS.Columns.Add("SKS");
                        TableKRS.Columns.Add("Dosen");
                        TableKRS.Columns.Add("Kelas");
                        TableKRS.Columns.Add("Hari");
                        TableKRS.Columns.Add("Mulai");
                        TableKRS.Columns.Add("Selesai");
                        TableKRS.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow = TableKRS.NewRow();
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    datarow["Dosen"] = rdr["nama"];
                                    datarow["Kelas"] = rdr["kelas"];
                                    datarow["Hari"] = rdr["hari"];
                                    datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                    datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                    datarow["Ruang"] = rdr["nm_ruang"];

                                    TableKRS.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVMakul.DataSource = TableKRS;
                                this.GVMakul.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableKRS.Rows.Clear();
                                TableKRS.Clear();
                                GVMakul.DataSource = TableKRS;
                                GVMakul.DataBind();
                            }
                        }
                    }

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);

                }
                catch (Exception ex)
                {
                    LbNama.Text = "Nama";
                    LbJenisKelas.Text = "Jenis Kelas";
                    LbProdi.Text = "Program Studi";
                    LbTahun.Text = "Tahun Angkatan";
                    LbNpm.Text = "NPM";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        int TotalSKS = 0;
        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[2].Text);
                TotalSKS += SKS;

                // this._TotalSkripsi = TotalSKS;
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                //e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah";
                e.Row.Cells[2].Text = TotalSKS.ToString();
                int JumlahTotalSKS = Convert.ToInt32(e.Row.Cells[2].Text);

                //string FormattedString4 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTotalSKS);
                //e.Row.Cells[1].Text = FormattedString4;
            }
        }
    }
}