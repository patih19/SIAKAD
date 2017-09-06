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
    //public partial class PrintKRS : Bak_staff
    public partial class PrintKRS : System.Web.UI.Page
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is WebForm11)
                {
                    try
                    {
                        //1. ----- set keterangan mahasiswa from last page
                        this.LbMhsName.Text = ((WebForm11)lastpage).Name;
                        this.LbNpm.Text = ((WebForm11)lastpage).NPM;
                        this.LbJenisKelas.Text = ((WebForm11)lastpage).JenisKelas;
                        this.LbTahun.Text = ((WebForm11)lastpage).Tahun;
                        this.LbSemester.Text = ((WebForm11)lastpage).Semester;
                        this.LbProdi.Text = ((WebForm11)lastpage).Prodi;
                        this.LbNama.Text = ((WebForm11)lastpage).Name;

                        //2. ---------- Gridview SKS ------------------
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            // --------------------- Fill Gridview  ------------------------
                            SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                            CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;


                            CmdListKRS.Parameters.AddWithValue("@npm", ((WebForm11)lastpage).NPM);
                            CmdListKRS.Parameters.AddWithValue("@semester", ((WebForm11)lastpage).Tahun + ((WebForm11)lastpage).Semester);

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

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);

                    }
                    catch (Exception ex)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
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

        private void DoDownload()
        {
            //var url = Request.Url.GetLeftPart(UriPartial.Authority) + "/CPCDownload.aspx?IsPDF=False?UserID=" + this.CurrentUser.UserID.ToString();
            var url = "http://localhost:2281/am/PrintKRS.aspx";
            //var url = Request.Url.AbsoluteUri;
            var file = WKHtmlToPdf(url);
            if (file != null)
            {
                Response.ContentType = "Application/pdf";
                Response.BinaryWrite(file);
                Response.End();
            }
        }

        public byte[] WKHtmlToPdf(string url)
        {
            var fileName = " - ";
            var wkhtmlDir = "C:\\Program Files\\wkhtmltopdf\\";
            var wkhtml = "C:\\Program Files\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
            var p = new Process();

            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = wkhtml;
            p.StartInfo.WorkingDirectory = wkhtmlDir;

            string switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
            switches += "--page-size A4 ";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();


            //read output
            byte[] buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }

            // wait or exit
            p.WaitForExit(60000);

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();

            return returnCode == 0 ? file : null;
        }
    }
}