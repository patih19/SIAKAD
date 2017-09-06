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
    public partial class ListNilai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is WebForm28)
                {
                    try
                    {
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            // ---- Get Daftar Mhs Peserta Matakuliah ---- //
                            SqlCommand cmd = new SqlCommand("SpGetNilaiByJadwal", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@no_jadwal", ((WebForm28)lastpage).NoJadwal);

                            DataTable TableNilai = new DataTable();
                            TableNilai.Columns.Add("No");
                            TableNilai.Columns.Add("NPM");
                            TableNilai.Columns.Add("NAMA");
                            TableNilai.Columns.Add("NILAI");

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        this.LbMakul.Text = rdr["makul"].ToString();
                                        this.LbSmster.Text = rdr["semester"].ToString();
                                        this.LbDosen.Text = rdr["dosen"].ToString();
                                        this.LbKls.Text = rdr["kelas"].ToString();
                                        this.LbJenisKelas.Text = rdr["jenis_kelas"].ToString();

                                        DataRow datarow = TableNilai.NewRow();
                                        datarow["No"] = rdr["nomor"];
                                        datarow["NPM"] = rdr["npm"];
                                        datarow["NAMA"] = rdr["nama"];
                                        datarow["NILAI"] = rdr["nilai"];

                                        TableNilai.Rows.Add(datarow);
                                    }

                                    //Fill Gridview
                                    this.GridView1.DataSource = TableNilai;
                                    this.GridView1.DataBind();
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
                else if (lastpage is WebForm21)
                {
                    try
                    {
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            // ---- Get Daftar Mhs Peserta Matakuliah ---- //
                            SqlCommand cmd = new SqlCommand("SpGetNilaiByJadwal", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@no_jadwal", ((WebForm21)lastpage).NoJadwal);

                            DataTable TableNilai = new DataTable();
                            TableNilai.Columns.Add("No");
                            TableNilai.Columns.Add("NPM");
                            TableNilai.Columns.Add("NAMA");
                            TableNilai.Columns.Add("NILAI");

                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        this.LbMakul.Text = rdr["makul"].ToString();
                                        this.LbSmster.Text = rdr["semester"].ToString();
                                        this.LbDosen.Text = rdr["dosen"].ToString();
                                        this.LbKls.Text = rdr["kelas"].ToString();
                                        this.LbJenisKelas.Text = rdr["jenis_kelas"].ToString();

                                        DataRow datarow = TableNilai.NewRow();
                                        datarow["No"] = rdr["nomor"];
                                        datarow["NPM"] = rdr["npm"];
                                        datarow["NAMA"] = rdr["nama"];
                                        datarow["NILAI"] = rdr["nilai"];

                                        TableNilai.Rows.Add(datarow);
                                    }

                                    //Fill Gridview
                                    this.GridView1.DataSource = TableNilai;
                                    this.GridView1.DataBind();
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