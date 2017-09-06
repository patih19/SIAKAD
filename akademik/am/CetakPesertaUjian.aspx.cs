using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace akademik.am
{
    //public partial class CetakPesertaUjian : System.Web.UI.Page
    public partial class CetakPesertaUjian : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // -- ===============================================================
            // ------ Sebelum Fitur Download --------
            // -- ===============================================================
            //if (!Page.IsPostBack)
            //{
            //    Page lastpage = (Page)Context.Handler;
            //    if (lastpage is akademik.am.WebForm10)
            //    {
            //        // set label
            //        this.LbProdi.Text = ((akademik.am.WebForm10)lastpage).Prodi;
            //        this.LbIdProdi.Text = ((akademik.am.WebForm10)lastpage).Id_Prodi;
            //        this.LbKelas.Text = ((akademik.am.WebForm10)lastpage).Kelas;
            //        this.LbJadwal.Text = ((akademik.am.WebForm10)lastpage).Jadwal;
            //        this.LbMakul.Text = ((akademik.am.WebForm10)lastpage).Makul;


            //        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //        using (SqlConnection con = new SqlConnection(CS))
            //        {
            //            con.Open();
            //            SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
            //            CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

            //            CmdMakul.Parameters.AddWithValue("@id_prodi", ((akademik.am.WebForm10)lastpage).Id_Prodi);
            //            CmdMakul.Parameters.AddWithValue("@nidn", ((akademik.am.WebForm10)lastpage).NIDN);
            //            CmdMakul.Parameters.AddWithValue("@kode_makul", ((akademik.am.WebForm10)lastpage).Kode_Makul);
            //            CmdMakul.Parameters.AddWithValue("@kelas", ((akademik.am.WebForm10)lastpage).Kelas);
            //            CmdMakul.Parameters.AddWithValue("@semester", ((akademik.am.WebForm10)lastpage).Tahun + ((akademik.am.WebForm10)lastpage).Semester);

            //            DataTable TableMakul = new DataTable();
            //            TableMakul.Columns.Add("NPM");
            //            TableMakul.Columns.Add("Nama");
            //            TableMakul.Columns.Add("Nilai");
            //            TableMakul.Columns.Add("Tanda Tangan");

            //            using (SqlDataReader rdr = CmdMakul.ExecuteReader())
            //            {
            //                if (rdr.HasRows)
            //                {
            //                    this.LbJenisUjian.Text = ((WebForm10)lastpage).JenisUjian;

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

            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is akademik.am.WebForm10)
                {
                    // set label
                    this.LbProdi.Text = ((akademik.am.WebForm10)lastpage).Prodi;
                    this.LbIdProdi.Text = ((akademik.am.WebForm10)lastpage).Id_Prodi;
                    this.LbKelas.Text = ((akademik.am.WebForm10)lastpage).Kelas;
                    this.LbJadwal.Text = ((akademik.am.WebForm10)lastpage).Jadwal;
                    this.LbMakul.Text = ((akademik.am.WebForm10)lastpage).Makul;


                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand CmdMakul = new SqlCommand("SpPesertaUjian", con);
                        CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdMakul.Parameters.AddWithValue("@id_prodi", ((akademik.am.WebForm10)lastpage).Id_Prodi);
                        CmdMakul.Parameters.AddWithValue("@nidn", ((akademik.am.WebForm10)lastpage).NIDN);
                        CmdMakul.Parameters.AddWithValue("@kode_makul", ((akademik.am.WebForm10)lastpage).Kode_Makul);
                        CmdMakul.Parameters.AddWithValue("@kelas", ((akademik.am.WebForm10)lastpage).Kelas);
                        CmdMakul.Parameters.AddWithValue("@semester", ((akademik.am.WebForm10)lastpage).Tahun + ((akademik.am.WebForm10)lastpage).Semester);

                        DataTable TableMakul = new DataTable();
                        TableMakul.Columns.Add("NPM");
                        TableMakul.Columns.Add("Nama");
                        TableMakul.Columns.Add("Nilai");
                        TableMakul.Columns.Add("Tanda Tangan");

                        using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.LbJenisUjian.Text = ((WebForm10)lastpage).JenisUjian;

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
    }
}