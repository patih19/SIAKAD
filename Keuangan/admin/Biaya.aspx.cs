using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Keuangan.admin
{
    // public partial class WebForm2 : System.Web.UI.Page
    public partial class WebForm2 :Keu_Admin_Class
    {
        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            //Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();
            this.Response.Redirect("~/keu-login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            //Label masterlbl = (Label)Master.FindControl("LabelUsername");
            //masterlbl.Text = this.Session["Name"].ToString();

            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id");
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            LBResultFilter.Text = "";

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT keu_biaya.SPP, keu_biaya.SKS, keu_biaya.BOP, keu_biaya.kemhs, keu_biaya.lab" +
                                                " FROM bak_prog_study INNER JOIN keu_biaya ON bak_prog_study.id_prog_study = keu_biaya.id_prog_study" +
                                                " WHERE keu_biaya.angkatan=@angkatan AND bak_prog_study.prog_study=@prodi AND keu_biaya.kelas=@kelas AND keu_biaya.semester=@semester", con);
                
                cmd.Parameters.AddWithValue("@angkatan",DLThnAngkatan.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@prodi", DLProgStudi.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@kelas",DLKelas.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@semester", DLSemster.SelectedItem.Text);


                DataTable Table = new DataTable();
                Table.Columns.Add("SPP");
                Table.Columns.Add("BOP");
                Table.Columns.Add("SKS");
                Table.Columns.Add("Kemahasiswaan");
                Table.Columns.Add("Laboratorium");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();
                            datarow["SPP"] = rdr["SPP"];
                            datarow["BOP"] = rdr["BOP"];
                            datarow["SKS"] = rdr["SKS"];
                            datarow["Kemahasiswaan"] = rdr["kemhs"];
                            datarow["Laboratorium"] = rdr["lab"];
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVBiayaStudi.DataSource = Table;
                        this.GVBiayaStudi.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        LBResultFilter.Text = "data tidak ditemukan";
                        LBResultFilter.ForeColor = System.Drawing.Color.Red;

                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVBiayaStudi.DataSource = Table;
                        GVBiayaStudi.DataBind();
                    }
                }
            }
        }
        // Menambahkan simbol rupiah
        protected void GVBiayaStudi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SPP = Convert.ToInt32(e.Row.Cells[0].Text);
                string FormattedString0 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                e.Row.Cells[0].Text = FormattedString0;
                
                int BOP = Convert.ToInt32(e.Row.Cells[1].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BOP);
                e.Row.Cells[1].Text = FormattedString1;

                int SKS = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString2 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                e.Row.Cells[2].Text = FormattedString2;

                int Kmhs = Convert.ToInt32(e.Row.Cells[3].Text);
                string FormattedString3 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Kmhs);
                e.Row.Cells[3].Text = FormattedString3;

                int Lab = Convert.ToInt32(e.Row.Cells[4].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Lab);
                e.Row.Cells[4].Text = FormattedString4;
            }
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            LBResultFilter.Text = "";
            try
            {
                // filter Text Box dan Drop Down
                if (DLThnAngkatan.SelectedValue == "-1")
                {
                    LBResultFilter.Text = "Pilih Tahun Angkatan";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                if ( DLProgStudi.SelectedValue == "-1" )
                {
                    LBResultFilter.Text = "Pilih Program Studi";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                if (DLKelas.SelectedValue == "-1")
                {
                    LBResultFilter.Text = "Pilih Kelas";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                if (TBSemester.Text == "" || TBSemester.Text == "0")
                {
                    LBResultFilter.Text = "Isi Semester";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                if (TBSPP.Text == "" || TBSPP.Text == "0")
                {
                    LBResultFilter.Text = "SPP Harus Diisi";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                if (TBBOP.Text == "" || TBBOP.Text == "0")
                {
                    LBResultFilter.Text = "BOP Harus Diisi";
                    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                    clearGridView();
                    return;
                }
                ////filter SKS
                //if (DLThnAngkatan.SelectedValue == "2010/2011" || DLThnAngkatan.SelectedValue == "2011/2012" || DLThnAngkatan.SelectedValue == "2012/2013" || DLThnAngkatan.SelectedValue == "2013/2014" || DLThnAngkatan.SelectedValue == "2014/2015")
                //{
                //    if (TBSks.Text == "" || TBSks.Text == "0" || TBSks.Text != "" )
                //    {
                //        TBSks.Text = "0";
                //    } 
                //}
                TBSks.Text = "0";

                //Filter Kemahasiswaan
                if (TBSemester.Text.Substring(4,1) == "1") // ==> semester ganjil, kemahasiswaan harus diisi
                {
                    if (TBKmhs.Text == "" || TBKmhs.Text == "0")
                    {
                        LBResultFilter.Text = "Kemahasiswaan Tidak Boleh Kosong";
                        LBResultFilter.ForeColor = System.Drawing.Color.Red;
                        clearGridView();
                        return;
                    }
                }
                else if (TBSemester.Text.Substring(4, 1) == "2") // ==> semester genap, kemahasiswaan = NOL
                {
                    TBKmhs.Text = "0";
                }

                ////Filter LAB
                //if (TbLab.Text == "" || TbLab.Text == "0")
                //{
                //    LBResultFilter.Text = "Laboratorium Tidak Boleh Kosong";
                //    LBResultFilter.ForeColor = System.Drawing.Color.Red;
                //    clearGridView();
                //    return;
                //}
                TbLab.Text = "0";

                // Insert Biaya Studi dengan Stored Procedure
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpInsertMasterBiaya", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@angkatan", DLThnAngkatan.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@id_prog_study",DLProgStudi.SelectedValue);
                    cmd.Parameters.AddWithValue("@kelas", DLKelas.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@semester", TBSemester.Text);
                    cmd.Parameters.AddWithValue("@SPP", TBSPP.Text);
                    cmd.Parameters.AddWithValue("@SKS", 0);
                    cmd.Parameters.AddWithValue("@BOP", TBBOP.Text);
                    cmd.Parameters.AddWithValue("@kemhs", TBKmhs.Text);
                    cmd.Parameters.AddWithValue("@lab", 0);

                    cmd.ExecuteNonQuery();

                    //clear gridview
                    clearGridView();

                    //Display biaya you just created
                    SqlCommand cmdview = new SqlCommand("SELECT keu_biaya.SPP, keu_biaya.SKS, keu_biaya.BOP, keu_biaya.kemhs, keu_biaya.lab" +
                                " FROM bak_prog_study INNER JOIN keu_biaya ON bak_prog_study.id_prog_study = keu_biaya.id_prog_study" +
                                " WHERE keu_biaya.angkatan=@angkatan AND bak_prog_study.prog_study=@prodi AND keu_biaya.kelas=@kelas AND keu_biaya.semester=@semester", con);

                    cmdview.Parameters.AddWithValue("@angkatan", DLThnAngkatan.SelectedItem.Text);
                    cmdview.Parameters.AddWithValue("@prodi", DLProgStudi.SelectedItem.Text);
                    cmdview.Parameters.AddWithValue("@kelas", DLKelas.SelectedItem.Text);
                    cmdview.Parameters.AddWithValue("@semester", TBSemester.Text);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("SPP");
                    Table.Columns.Add("BOP");
                    Table.Columns.Add("SKS");
                    Table.Columns.Add("Kemahasiswaan");
                    Table.Columns.Add("Laboratorium");

                    //con.Open();
                    using (SqlDataReader rdr = cmdview.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();
                                datarow["SPP"] = rdr["SPP"];
                                datarow["BOP"] = rdr["BOP"];
                                datarow["SKS"] = rdr["SKS"];
                                datarow["Kemahasiswaan"] = rdr["kemhs"];
                                datarow["Laboratorium"] = rdr["lab"];
                                Table.Rows.Add(datarow);
                            }
                            //Fill Gridview
                            this.GVBiayaStudi.DataSource = Table;
                            this.GVBiayaStudi.DataBind();

                            LBResultFilter.Text = "";
                        }
                        else
                        {
                            LBResultFilter.Text = "data tidak ditemukan";
                            LBResultFilter.ForeColor = System.Drawing.Color.Red;

                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVBiayaStudi.DataSource = Table;
                            GVBiayaStudi.DataBind();
                        }
                    }
                    // Set Label Success
                    LBResultFilter.Text = "Input Biaya Studi Berhasil";
                    LBResultFilter.ForeColor = System.Drawing.Color.Green;
                }
                //Clear Textbox
                TBSemester.Text = "";
                TBSPP.Text = "";
                TBBOP.Text = "";
                TBSks.Text = "";
                TBKmhs.Text = "";
                TbLab.Text = "";
            }

            catch (Exception ex)
            {
                LBResultFilter.Text = ex.Message.ToString();
                LBResultFilter.ForeColor = System.Drawing.Color.Red;

                //Clear Textbox
                TBSemester.Text = "";
                TBSPP.Text = "";
                TBBOP.Text = "";
                TBKmhs.Text = "";
                TbLab.Text = "";
            }
        }
        protected void clearGridView()
        {
            DataTable Table = new DataTable();
            Table.Rows.Clear();
            Table.Clear();
            GVBiayaStudi.DataSource = Table;
            GVBiayaStudi.DataBind();
            Table.Dispose();
            GVBiayaStudi.Dispose();
        }
    }
}