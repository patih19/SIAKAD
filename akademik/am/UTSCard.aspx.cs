using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace akademik
{
    public partial class UTSCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is akademik.am.WebForm2)
                {
                    //1. ----- set mahasiswa identity from last page
                    this.LbNama.Text = ((akademik.am.WebForm2)lastpage).Name;
                    this.LbNpm.Text = ((akademik.am.WebForm2)lastpage).NPM;
                    this.LbJenisKelas.Text = ((akademik.am.WebForm2)lastpage).JenisKelas;
                    this.LbTahun.Text = ((akademik.am.WebForm2)lastpage).Tahun;
                    this.LbSemester.Text = ((akademik.am.WebForm2)lastpage).Semester;
                    this.LbProdi.Text = ((akademik.am.WebForm2)lastpage).Prodi;
                    this.LbNama.Text = ((akademik.am.WebForm2)lastpage).Name;

                    // ---------- Gridview SKS ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        // --------------------- Fill Gridview UTS Makul ------------------------
                        SqlCommand CmdListUTS = new SqlCommand("SpListUTS", con);
                        CmdListUTS.CommandType = System.Data.CommandType.StoredProcedure;


                        CmdListUTS.Parameters.AddWithValue("@npm", ((akademik.am.WebForm2)lastpage).NPM);
                        CmdListUTS.Parameters.AddWithValue("@semester", ((akademik.am.WebForm2)lastpage).Tahun + ((akademik.am.WebForm2)lastpage).Semester);

                        DataTable TableUTSMakul = new DataTable();
                        TableUTSMakul.Columns.Add("Kode");
                        TableUTSMakul.Columns.Add("Mata kuliah");
                        TableUTSMakul.Columns.Add("SKS");
                        TableUTSMakul.Columns.Add("Tanggal Ujian");
                        TableUTSMakul.Columns.Add("Tanda Tangan");

                        using (SqlDataReader rdr = CmdListUTS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow = TableUTSMakul.NewRow();
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    //--- format tanggal ---
                                    if (rdr["tgl_uts"] == DBNull.Value)
                                    {
                                    }
                                    else
                                    {
                                        DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uts"]);
                                        datarow["Tanggal Ujian"] = TglUjian.ToString("dd-MMMM-yyyy");
                                    }
                                    TableUTSMakul.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVJadwalUTS.DataSource = TableUTSMakul;
                                this.GVJadwalUTS.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUTSMakul.Rows.Clear();
                                TableUTSMakul.Clear();
                                GVJadwalUTS.DataSource = TableUTSMakul;
                                GVJadwalUTS.DataBind();
                            }
                        }

                        //------------------------- GV UTS JADWAL ---------------------
                        SqlCommand CmdUTSJadwal = new SqlCommand("SpListUTS", con);
                        CmdUTSJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUTSJadwal.Parameters.AddWithValue("@npm", ((akademik.am.WebForm2)lastpage).NPM);
                        CmdUTSJadwal.Parameters.AddWithValue("@semester", ((akademik.am.WebForm2)lastpage).Tahun + ((akademik.am.WebForm2)lastpage).Semester);

                        DataTable TableUTSJadwal = new DataTable();
                        TableUTSJadwal.Columns.Add("Hari");
                        TableUTSJadwal.Columns.Add("Mata Kuliah");
                        TableUTSJadwal.Columns.Add("Dosen");
                        TableUTSJadwal.Columns.Add("Mulai");
                        TableUTSJadwal.Columns.Add("Selesai");
                        TableUTSJadwal.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdUTSJadwal.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow2 = TableUTSJadwal.NewRow();
                                    datarow2["Hari"] = rdr["hari_uts"];
                                    datarow2["Mata Kuliah"] = rdr["makul"];
                                    datarow2["Mulai"] = rdr["jam_mulai_uts"];
                                    datarow2["Selesai"] = rdr["jam_sls_uts"];
                                    datarow2["Ruang"] = rdr["ruang_uts"];
                                    datarow2["Dosen"] = rdr["nama"];

                                    TableUTSJadwal.Rows.Add(datarow2);
                                }

                                //Fill Gridview
                                this.GVUTSDetail.DataSource = TableUTSJadwal;
                                this.GVUTSDetail.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUTSJadwal.Rows.Clear();
                                TableUTSJadwal.Clear();
                                GVUTSDetail.DataSource = TableUTSJadwal;
                                GVUTSDetail.DataBind();
                            }
                        }
                    }
                }
            }
        }
    }
}