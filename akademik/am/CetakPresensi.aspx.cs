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

namespace akademik.am
{
    //public partial class CetakPresensi : System.Web.UI.Page
    public partial class CetakPresensi : Bak_staff
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is akademik.am.WebForm3)
                {
                    try
                    {
                        ////1. ----- set keterangan mahasiswa from last page
                        //this.LbMakul.Text = ((akademik.am.WebForm3)lastpage).Makul;
                        //this.LbTahun.Text = ((akademik.am.WebForm3)lastpage).Tahun;
                        //this.LbSemester.Text = ((akademik.am.WebForm3)lastpage).Semester;
                        //this.LbDosen.Text = ((akademik.am.WebForm3)lastpage).Dosen;
                        //this.LbNIDN.Text = ((akademik.am.WebForm3)lastpage).NIDN;
                        //this.LbKelas.Text = ((akademik.am.WebForm3)lastpage).Kelas;
                        //this.LbKdMakul.Text = ((akademik.am.WebForm3)lastpage).KodeMakul;
                        //this.LbJadwal.Text = ((akademik.am.WebForm3)lastpage).jadwal;

                        ////2. ---------- Gridview Peserta ------------------
                        //string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        //using (SqlConnection con = new SqlConnection(CS))
                        //{
                        //    con.Open();
                        //    SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
                        //    CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                        //    CmdMakul.Parameters.AddWithValue("@id_prodi", ((akademik.am.WebForm3)lastpage).IdProdi);
                        //    CmdMakul.Parameters.AddWithValue("@nidn", this.LbNIDN.Text);
                        //    CmdMakul.Parameters.AddWithValue("@kode_makul", this.LbKdMakul.Text);
                        //    CmdMakul.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                        //    CmdMakul.Parameters.AddWithValue("@semester", this.LbTahun.Text + this.LbSemester.Text);

                        //    DataTable TableMakul = new DataTable();
                        //    TableMakul.Columns.Add("NPM");
                        //    TableMakul.Columns.Add("Nama");
                        //    for (int i = 1; i <= 16; i++)
                        //    {
                        //        TableMakul.Columns.Add("(" + i.ToString() + ")" + "..........");
                        //    }

                        //    using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                        //    {
                        //        if (rdr.HasRows)
                        //        {
                        //            while (rdr.Read())
                        //            {
                        //                DataRow datarow = TableMakul.NewRow();
                        //                datarow["NPM"] = rdr["npm"];
                        //                datarow["Nama"] = rdr["nama"];

                        //                TableMakul.Rows.Add(datarow);
                        //            }
                        //            //Fill Gridview
                        //            this.GVAbsen.DataSource = TableMakul;
                        //            this.GVAbsen.DataBind();

                        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
                        //        }
                        //        else
                        //        {
                        //            //clear Gridview
                        //            TableMakul.Rows.Clear();
                        //            TableMakul.Clear();
                        //            this.GVAbsen.DataSource = TableMakul;
                        //            this.GVAbsen.DataBind();
                        //        }
                        //    }
                        //}

                        //1. ----- set keterangan mahasiswa from last page
                        this.LbMakul.Text = ((akademik.am.WebForm3)lastpage).Makul;
                        this.LbTahun.Text = ((akademik.am.WebForm3)lastpage).Tahun;
                        this.LbSemester.Text = ((akademik.am.WebForm3)lastpage).Semester;
                        this.LbDosen.Text = ((akademik.am.WebForm3)lastpage).Dosen;
                        this.LbNIDN.Text = ((akademik.am.WebForm3)lastpage).NIDN;
                        this.LbKelas.Text = ((akademik.am.WebForm3)lastpage).Kelas;
                        this.LbKdMakul.Text = ((akademik.am.WebForm3)lastpage).KodeMakul;
                        this.LbJadwal.Text = ((akademik.am.WebForm3)lastpage).jadwal;
                        this.LbTtdDosen.Text = ((akademik.am.WebForm3)lastpage).Dosen;

                        //2. ---------- Gridview Peserta ------------------
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
                            CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdMakul.Parameters.AddWithValue("@id_prodi", ((akademik.am.WebForm3)lastpage).IdProdi);
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