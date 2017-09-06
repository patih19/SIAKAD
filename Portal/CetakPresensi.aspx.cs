using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Portal
{
    //public partial class CetakPresensi : System.Web.UI.Page
    public partial class CetakPresensi : Tu
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is WebForm1)
                {
                    try
                    {
                        //1. ----- set keterangan mahasiswa from last page
                        this.LbMakul.Text = ((WebForm1)lastpage).Makul;
                        this.LbTahun.Text = ((WebForm1)lastpage).Tahun;
                        this.LbSemester.Text = ((WebForm1)lastpage).Semester;
                        this.LbDosen.Text = ((WebForm1)lastpage).Dosen;
                        this.LbNIDN.Text = ((WebForm1)lastpage).NIDN;
                        this.LbKelas.Text = ((WebForm1)lastpage).Kelas;
                        this.LbKdMakul.Text = ((WebForm1)lastpage).KodeMakul;
                        this.LbJadwal.Text = ((WebForm1)lastpage).jadwal;
                        this.LbTtdDosen.Text = ((WebForm1)lastpage).Dosen;

                        //2. ---------- Gridview Peserta ------------------
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
                            CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdMakul.Parameters.AddWithValue("@id_prodi", ((WebForm1)lastpage).IdProdi);
                            CmdMakul.Parameters.AddWithValue("@nidn", this.LbNIDN.Text);
                            CmdMakul.Parameters.AddWithValue("@kode_makul", this.LbKdMakul.Text);
                            CmdMakul.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdMakul.Parameters.AddWithValue("@semester", this.LbTahun.Text + this.LbSemester.Text);

                            using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    System.Text.StringBuilder htmlTableString = new StringBuilder();
                                    htmlTableString.AppendLine("<table class=\"table-condensed table-bordered\" border=\"1\" style=\"font-size:11px; border-color:Black\">");
                                    htmlTableString.AppendLine("<tr><td rowspan=\"2\"><strong>No.</strong></td><td rowspan=\"2\"><strong>NPM</strong></td><td rowspan=\"2\"><strong>NAMA</strong></td><td colspan=\"16\" align=\"center\"><strong>TANGGAL PERKULIAHAN</strong></td></tr>");
                                    htmlTableString.AppendLine("<tr><td height=\"35\" style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td> <td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td> <td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td> <td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td><td style=\"color:White; vertical-align:bottom\">..............</td></tr>");
                                    while (rdr.Read())
                                    {
                                        htmlTableString.AppendLine("<tr>");
                                        htmlTableString.AppendLine("<td height=\"50\">" + rdr["nomor"].ToString() + "</td>");
                                        htmlTableString.AppendLine("<td height=\"50\">" + rdr["npm"].ToString() + "</td>");
                                        htmlTableString.AppendLine("<td height=\"50\">" + rdr["nama"].ToString() + "</td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<td height=\"50\"></td>");
                                        htmlTableString.AppendLine("<tr>");
                                    }

                                    htmlTableString.AppendLine("</table>");
                                    literal1.Text = htmlTableString.ToString();

                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
                                }
                                else
                                {

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
            }
        }
    }
}