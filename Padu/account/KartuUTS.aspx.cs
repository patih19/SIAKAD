using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Padu.account
{
    public partial class KartuUts : System.Web.UI.Page
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //1. ----- set keterangan mahasiswa from last page
                // ------- Read Mahasiswa ----------
                //Server.UrlDecode(Request.QueryString["PID"]);
                mhs.ReadMahasiswa(Convert.ToString(Request.QueryString["nim"]));

                LbNama.Text = mhs.nama.ToString();
                LbJenisKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbSemester.Text = Server.UrlDecode(Request.QueryString["semester"]); //Convert.ToString(Request.QueryString["semester"])

                //------ Semester ------//
                string smstr =Server.UrlDecode(Request.QueryString["semester"]); 
                LbTahun.Text = smstr.Substring(0, 4);
                LbSemester.Text = smstr.Substring(4, 1);

                // ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    // --------------------- Fill Gridview UTS Makul ------------------------
                    SqlCommand CmdListUTS = new SqlCommand("SpListUTS", con);
                    CmdListUTS.CommandType = System.Data.CommandType.StoredProcedure;

                    //Convert.ToString(Request.QueryString["Name"]);
                    CmdListUTS.Parameters.AddWithValue("@npm", Convert.ToString( Request.QueryString["nim"]));
                    CmdListUTS.Parameters.AddWithValue("@semester",Server.UrlDecode(Request.QueryString["semester"]));

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

                    CmdUTSJadwal.Parameters.AddWithValue("@npm", Convert.ToString(Request.QueryString["nim"]));
                    CmdUTSJadwal.Parameters.AddWithValue("@semester",Server.UrlDecode(Request.QueryString["semester"]));

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