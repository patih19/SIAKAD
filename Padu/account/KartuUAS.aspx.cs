﻿using System;
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
    public partial class KartuUAS : System.Web.UI.Page
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //1. ----- set keterangan mahasiswa from last page
                // ------- Read Mahasiswa ----------
                mhs.ReadMahasiswa(Convert.ToString(Request.QueryString["nim"]));

                LbNama.Text = mhs.nama.ToString();
                LbJenisKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbSemester.Text =Server.UrlDecode(Request.QueryString["semester"]);

                //------ Semester ------//
                string smstr =Server.UrlDecode(Request.QueryString["semester"]);
                LbTahun.Text = smstr.Substring(0, 4);
                LbSemester.Text = smstr.Substring(4, 1);


                //this.LbNama.Text = ((akademik.am.WebForm2)lastpage).Name;
                //this.LbNpm.Text = ((akademik.am.WebForm2)lastpage).NPM;
                //this.LbJenisKelas.Text = ((akademik.am.WebForm2)lastpage).JenisKelas;
                //this.LbTahun.Text = ((akademik.am.WebForm2)lastpage).Tahun;
                //this.LbSemester.Text = ((akademik.am.WebForm2)lastpage).Semester;
                //this.LbProdi.Text = ((akademik.am.WebForm2)lastpage).Prodi;
                //this.LbNama.Text = ((akademik.am.WebForm2)lastpage).Name;

                // ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    // --------------------- Fill Gridview UAS Makul ------------------------
                    SqlCommand CmdListUAS = new SqlCommand("SpListUAS", con);
                    CmdListUAS.CommandType = System.Data.CommandType.StoredProcedure;


                    CmdListUAS.Parameters.AddWithValue("@npm", Convert.ToString(Request.QueryString["nim"]));
                    CmdListUAS.Parameters.AddWithValue("@semester",Server.UrlDecode(Request.QueryString["semester"]));

                    DataTable TableUASMakul = new DataTable();
                    TableUASMakul.Columns.Add("Kode");
                    TableUASMakul.Columns.Add("Mata kuliah");
                    TableUASMakul.Columns.Add("SKS");
                    TableUASMakul.Columns.Add("Tanggal Ujian");
                    TableUASMakul.Columns.Add("Tanda Tangan");

                    using (SqlDataReader rdr = CmdListUAS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableUASMakul.NewRow();
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                //--- format tanggal ---
                                if (rdr["tgl_uas"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uas"]);
                                    datarow["Tanggal Ujian"] = TglUjian.ToString("dd-MMMM-yyyy");
                                }

                                TableUASMakul.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwalUAS.DataSource = TableUASMakul;
                            this.GVJadwalUAS.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableUASMakul.Rows.Clear();
                            TableUASMakul.Clear();
                            GVJadwalUAS.DataSource = TableUASMakul;
                            GVJadwalUAS.DataBind();
                        }
                    }

                    //------------------------- GV UAS JADWAL ---------------------
                    SqlCommand CmdUASJadwal = new SqlCommand("SpListUAS", con);
                    CmdUASJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdUASJadwal.Parameters.AddWithValue("@npm", Convert.ToString(Request.QueryString["nim"]));
                    CmdUASJadwal.Parameters.AddWithValue("@semester",Server.UrlDecode(Request.QueryString["semester"]));

                    DataTable TableUASJadwal = new DataTable();
                    TableUASJadwal.Columns.Add("Hari");
                    TableUASJadwal.Columns.Add("Mata Kuliah");
                    TableUASJadwal.Columns.Add("Dosen");
                    TableUASJadwal.Columns.Add("Mulai");
                    TableUASJadwal.Columns.Add("Selesai");
                    TableUASJadwal.Columns.Add("Ruang");

                    using (SqlDataReader rdr = CmdUASJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow2 = TableUASJadwal.NewRow();
                                datarow2["Hari"] = rdr["hari_uas"];
                                datarow2["Mata Kuliah"] = rdr["makul"];
                                datarow2["Mulai"] = rdr["jam_mulai_uas"];
                                datarow2["Selesai"] = rdr["jam_sls_uas"];
                                datarow2["Ruang"] = rdr["ruang_uas"];
                                datarow2["Dosen"] = rdr["nama"];

                                TableUASJadwal.Rows.Add(datarow2);
                            }

                            //Fill Gridview
                            this.GVUASDetail.DataSource = TableUASJadwal;
                            this.GVUASDetail.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableUASJadwal.Rows.Clear();
                            TableUASJadwal.Clear();
                            GVUASDetail.DataSource = TableUASJadwal;
                            GVUASDetail.DataBind();
                        }
                    }
                }
            }
        }
    }
}