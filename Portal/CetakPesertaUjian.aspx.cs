using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Portal
{
    //public partial class CetakPesertaUjian : System.Web.UI.Page
    public partial class CetakPesertaUjian : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// ============================================================================//
            //// Sebelum Peserta Difilter Berdasarkan Pembayaran
            //// ============================================================================//
            //if (!Page.IsPostBack)
            //{
            //    Page lastpage = (Page)Context.Handler;
            //    if (lastpage is WebForm12)
            //    {
            //        // set label
            //        //this.LbProdi.Text = ((WebForm12)lastpage).Prodi;
            //        //this.LbIdProdi.Text = ((WebForm12)lastpage).Id_Prodi;
            //        this.LbKelas.Text = ((WebForm12)lastpage).Kelas;
            //        this.LbJadwal.Text = ((WebForm12)lastpage).Jadwal;
            //        this.LbMakul.Text = ((WebForm12)lastpage).Makul;
            //        this.LbDosen.Text = ((WebForm12)lastpage).Dosen;
            //        this.LbTahun.Text = ((WebForm12)lastpage).Tahun;
            //        this.LbSemester.Text = ((WebForm12)lastpage).Semester;


            //        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //        using (SqlConnection con = new SqlConnection(CS))
            //        {
            //            con.Open();
            //            SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
            //            CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

            //            CmdMakul.Parameters.AddWithValue("@id_prodi", ((WebForm12)lastpage).Id_Prodi);
            //            CmdMakul.Parameters.AddWithValue("@nidn", ((WebForm12)lastpage).NIDN);
            //            CmdMakul.Parameters.AddWithValue("@kode_makul", ((WebForm12)lastpage).Kode_Makul);
            //            CmdMakul.Parameters.AddWithValue("@kelas", ((WebForm12)lastpage).Kelas);
            //            CmdMakul.Parameters.AddWithValue("@semester", ((WebForm12)lastpage).Tahun + ((WebForm12)lastpage).Semester);

            //            DataTable TableMakul = new DataTable();
            //            TableMakul.Columns.Add("NPM");
            //            TableMakul.Columns.Add("Nama");
            //            TableMakul.Columns.Add("Nilai");
            //            TableMakul.Columns.Add("Tanda Tangan");

            //            using (SqlDataReader rdr = CmdMakul.ExecuteReader())
            //            {
            //                if (rdr.HasRows)
            //                {
            //                    this.LbJenisUjian.Text = ((WebForm12)lastpage).JenisUjian;

            //                    while (rdr.Read())
            //                    {
            //                        DataRow datarow = TableMakul.NewRow();
            //                        datarow["NPM"] = rdr["npm"];
            //                        datarow["Nama"] = rdr["nama"];

            //                        TableMakul.Rows.Add(datarow);
            //                    }

            //                    //Fill Gridview
            //                    this.GVPeserta.DataSource = TableMakul;
            //                    this.GVPeserta.DataBind();
            //                }
            //                else
            //                {
            //                    //clear Gridview
            //                    TableMakul.Rows.Clear();
            //                    TableMakul.Clear();
            //                    this.GVPeserta.DataSource = TableMakul;
            //                    this.GVPeserta.DataBind();
            //                }
            //            }
            //        }
            //    }
            //}


            // ============================================================================//
            // SETELAH Peserta Difilter Berdasarkan Pembayaran
            // ============================================================================//
            if (!Page.IsPostBack)
            {
                try
                {
                    Page lastpage = (Page)Context.Handler;
                    if (lastpage is WebForm12)
                    {
                        // set label
                        //this.LbProdi.Text = ((WebForm12)lastpage).Prodi;
                        //this.LbIdProdi.Text = ((WebForm12)lastpage).Id_Prodi;
                        this.LbKelas.Text = ((WebForm12)lastpage).Kelas;
                        this.LbJadwal.Text = ((WebForm12)lastpage).Jadwal;
                        this.LbMakul.Text = ((WebForm12)lastpage).Makul;
                        this.LbDosen.Text = ((WebForm12)lastpage).Dosen;
                        this.LbTahun.Text = ((WebForm12)lastpage).Tahun;
                        this.LbSemester.Text = ((WebForm12)lastpage).Semester;
                        this.LbTtdDosen.Text = ((WebForm12)lastpage).Dosen;

                        //Jenis Ujian
                        // string JenisUjian = ((WebForm12)lastpage).JenisUjian;

                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();

                            if (((WebForm12)lastpage).JenisUjian == "UJIAN TENGAH SEMESTER")
                            {
                                SqlCommand CmdUTS = new SqlCommand("SpPesertaUTS", con);
                                CmdUTS.CommandType = System.Data.CommandType.StoredProcedure;

                                CmdUTS.Parameters.AddWithValue("@id_prodi", ((WebForm12)lastpage).Id_Prodi);
                                CmdUTS.Parameters.AddWithValue("@nidn", ((WebForm12)lastpage).NIDN);
                                CmdUTS.Parameters.AddWithValue("@kode_makul", ((WebForm12)lastpage).Kode_Makul);
                                CmdUTS.Parameters.AddWithValue("@kelas", ((WebForm12)lastpage).Kelas);
                                CmdUTS.Parameters.AddWithValue("@semester", ((WebForm12)lastpage).Tahun + ((WebForm12)lastpage).Semester);

                                DataTable TableMakul = new DataTable();
                                TableMakul.Columns.Add("NPM");
                                TableMakul.Columns.Add("Nama");
                                TableMakul.Columns.Add("Nilai Angka");
                                TableMakul.Columns.Add("Nilai Huruf");
                                TableMakul.Columns.Add("Tanda Tangan");

                                using (SqlDataReader rdr = CmdUTS.ExecuteReader())
                                {
                                    if (rdr.HasRows)
                                    {
                                        this.LbJenisUjian.Text = ((WebForm12)lastpage).JenisUjian;

                                        while (rdr.Read())
                                        {
                                            DataRow datarow = TableMakul.NewRow();
                                            datarow["NPM"] = rdr["npm"];
                                            datarow["Nama"] = rdr["nama"];

                                            TableMakul.Rows.Add(datarow);
                                        }

                                        //Fill Gridview
                                        this.GVPeserta.DataSource = TableMakul;
                                        this.GVPeserta.DataBind();
                                    }
                                    else
                                    {
                                        //clear Gridview
                                        TableMakul.Rows.Clear();
                                        TableMakul.Clear();
                                        this.GVPeserta.DataSource = TableMakul;
                                        this.GVPeserta.DataBind();
                                    }
                                }
                            }
                            else if (((WebForm12)lastpage).JenisUjian == "UJIAN AKHIR SEMESTER")
                            {
                                SqlCommand CmdUAS = new SqlCommand("SpPesertaUAS", con);
                                CmdUAS.CommandType = System.Data.CommandType.StoredProcedure;

                                CmdUAS.Parameters.AddWithValue("@id_prodi", ((WebForm12)lastpage).Id_Prodi);
                                CmdUAS.Parameters.AddWithValue("@nidn", ((WebForm12)lastpage).NIDN);
                                CmdUAS.Parameters.AddWithValue("@kode_makul", ((WebForm12)lastpage).Kode_Makul);
                                CmdUAS.Parameters.AddWithValue("@kelas", ((WebForm12)lastpage).Kelas);
                                CmdUAS.Parameters.AddWithValue("@semester", ((WebForm12)lastpage).Tahun + ((WebForm12)lastpage).Semester);

                                DataTable TableMakul = new DataTable();
                                TableMakul.Columns.Add("NPM");
                                TableMakul.Columns.Add("Nama");
                                TableMakul.Columns.Add("Nilai Angka");
                                TableMakul.Columns.Add("Nilai Huruf");
                                TableMakul.Columns.Add("Tanda Tangan");

                                using (SqlDataReader rdr = CmdUAS.ExecuteReader())
                                {
                                    if (rdr.HasRows)
                                    {
                                        this.LbJenisUjian.Text = ((WebForm12)lastpage).JenisUjian;

                                        while (rdr.Read())
                                        {
                                            DataRow datarow = TableMakul.NewRow();
                                            datarow["NPM"] = rdr["npm"];
                                            datarow["Nama"] = rdr["nama"];

                                            TableMakul.Rows.Add(datarow);
                                        }

                                        //Fill Gridview
                                        this.GVPeserta.DataSource = TableMakul;
                                        this.GVPeserta.DataBind();
                                    }
                                    else
                                    {
                                        //clear Gridview
                                        TableMakul.Rows.Clear();
                                        TableMakul.Clear();
                                        this.GVPeserta.DataSource = TableMakul;
                                        this.GVPeserta.DataBind();
                                    }
                                }
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