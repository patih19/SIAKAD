using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    public partial class WebForm25 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                PopulateProdi();
                PopulateSemester();
                //JadwalPerhari();
            }

        }

        protected static DataTable NamaRuang(string IdProdi)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT no,nm_ruang FROM dbo.bak_ruang WHERE id_prog_study=@IdProdi AND status='1' ORDER BY nm_ruang ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;

                //cmd.Parameters.AddWithValue("@semester",);
                cmd.Parameters.AddWithValue("@IdProdi", IdProdi);
                //cmd.Parameters.AddWithValue("@Hari", Hari);

                DataTable TableData = new DataTable();
                TableData.Columns.Add("NoRuang");
                TableData.Columns.Add("Ruang");

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableData.NewRow();
                            datarow["NoRuang"] = rdr["no"];
                            datarow["Ruang"] = rdr["nm_ruang"];

                            TableData.Rows.Add(datarow);
                        }

                        ////Fill Gridview
                        //this.RepeaterRuang.DataSource = TableData;
                        //this.RepeaterRuang.DataBind();

                    }
                }

                return TableData;
            }
        }

        protected static DataTable ReadJadwalRuang(string Hari, int NoRuang, string Semester, string IdProdi)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT bak_jadwal.no_jadwal,bak_ruang.no,bak_ruang.nm_ruang, bak_ruang.id_prog_study, bak_jadwal.kelas, bak_jadwal.kode_makul, bak_makul.makul, bak_jadwal.hari, bak_ruang.kapasitas, " +
                                 "bak_ruang.status, bak_jadwal.semester, bak_jadwal.jm_awal_kuliah, bak_jadwal.jm_akhir_kuliah,bak_jadwal.jenis_kelas , bak_prog_study.id_prog_study, dbo.bak_prog_study.prog_study " +
                                 "FROM bak_jadwal INNER JOIN " +
                                                        "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN " +
                                                         "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN " +
                                                         "bak_ruang ON bak_jadwal.id_rng_kuliah = bak_ruang.no " +
                    "WHERE dbo.bak_jadwal.semester=@semester AND dbo.bak_ruang.id_prog_study=@IdProdi AND bak_jadwal.hari =@Hari AND bak_ruang.no= @NoRuang " +
                    //"where dbo.bak_jadwal.semester='20162' AND dbo.bak_jadwal.id_prog_study='60-201' AND bak_jadwal.hari =@Hari AND  bak_ruang.no= @NoRuang " +
                                "ORDER BY nm_ruang, jm_awal_kuliah ASC", con);
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@semester", Semester);
                cmd.Parameters.AddWithValue("@IdProdi", IdProdi);
                cmd.Parameters.AddWithValue("@NoRuang", NoRuang);
                cmd.Parameters.AddWithValue("@Hari", Hari);

                DataTable TableData = new DataTable();
                TableData.Columns.Add("Makul");
                TableData.Columns.Add("Program Studi");
                TableData.Columns.Add("Ruang");
                TableData.Columns.Add("Jam Mulai");
                TableData.Columns.Add("Jam Selesai");
                TableData.Columns.Add("Kelas");
                TableData.Columns.Add("Jenis Kelas");


                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableData.NewRow();
                            datarow["Makul"] = rdr["makul"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Jam Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Jam Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                            TableData.Rows.Add(datarow);
                        }

                        ////Fill Gridview
                        //this.RepeaterRuang.DataSource = TableData;
                        //this.RepeaterRuang.DataBind();

                    }
                }

                return TableData;
            }
        }

        protected void JadwalPerhari()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                                                        "CREATE TABLE #TempHari " +
                                                        "(no INT, hari varchar(100)) " +

                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 1, 'senin') " +
                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 2, 'selasa') " +
                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 3, 'rabu') " +
                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 4, 'kamis') " +
                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 5, 'jumat') " +
                                                        "INSERT INTO  #TempHari ( no, hari ) VALUES  ( 6, 'sabtu') " +

                                                        " SELECT no,hari FROM #TempHari " +
                                                        "ORDER BY no ASC", con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    DataTable TableData = new DataTable();
                    TableData.Columns.Add("NoHari");
                    TableData.Columns.Add("Hari");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableData.NewRow();
                                datarow["NoHari"] = rdr["hari"];
                                datarow["Hari"] = rdr["hari"].ToString().ToUpper();

                                TableData.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.RepeaterHari.DataSource = TableData;
                            this.RepeaterHari.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        protected void RepeaterHari_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.DLProdi.SelectedIndex == 0)
            {
                Response.Write("Error");
                return;
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //string NoHari = (e.Item.FindControl("NoHari") as HiddenField).Value;
                Repeater RptRuang = e.Item.FindControl("RepeaterRuang") as Repeater;
                RptRuang.DataSource = NamaRuang(this.DLProdi.SelectedValue.ToString().Trim());
                RptRuang.DataBind();
            }
        }

        protected void RepeaterRuang_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (this.DLProdi.SelectedIndex == 0)
            {
                Response.Write("Error");
                return;
            }

            if (this.DLSemester.SelectedIndex == 0)
            {
                Response.Write("Error");
                return;
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater innerRepeater = (Repeater)sender;
                RepeaterItem outerItem = (RepeaterItem)innerRepeater.NamingContainer;
                //Response.Write(((Label)outerItem.FindControl("LbHari")).Text);

                string NoRuang = (e.Item.FindControl("NoRuang") as HiddenField).Value;
                Repeater RptRuang = e.Item.FindControl("RepeaterJadwalRuang") as Repeater;
                RptRuang.DataSource = ReadJadwalRuang(((Label)outerItem.FindControl("LbHari")).Text, Convert.ToInt16(NoRuang), this.DLSemester.SelectedItem.Text.Trim(), this.DLProdi.SelectedValue.Trim()); // GetData(string.Format("SELECT TOP 3 * FROM Orders WHERE CustomerId='{0}'", customerId));
                RptRuang.DataBind();
            }
        }

        private void PopulateProdi()
        {
            try
            {
                // ------------------------------------------------------------------------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // ----------------- Jumlah Peserta Tiap Prodi ---------------------------------
                    using (SqlCommand CmdProdi = new SqlCommand("SELECT id_prog_study,prog_study FROM bak_prog_study", con))
                    {
                        CmdProdi.CommandType = System.Data.CommandType.Text;

                        this.DLProdi.DataSource = CmdProdi.ExecuteReader();
                        this.DLProdi.DataTextField = "prog_study";
                        this.DLProdi.DataValueField = "id_prog_study";
                        this.DLProdi.DataBind();
                    }

                    con.Close();
                    con.Dispose();

                    this.DLProdi.Items.Insert(0, new ListItem("--- Program Studi ---", "0"));
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void PopulateSemester()
        {
            try
            {
                // ------------------------------------------------------------------------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // ----------------- Jumlah Peserta Tiap Prodi ---------------------------------
                    using (SqlCommand CmdSemester = new SqlCommand("SELECT TOP 2 semester FROM bak_jadwal GROUP BY  semester ORDER BY semester DESC", con))
                    {
                        CmdSemester.CommandType = System.Data.CommandType.Text;

                        this.DLSemester.DataSource = CmdSemester.ExecuteReader();
                        this.DLSemester.DataTextField = "semester";
                        this.DLSemester.DataValueField = "semester";
                        this.DLSemester.DataBind();
                    }

                    con.Close();
                    con.Dispose();

                    this.DLSemester.Items.Insert(0, new ListItem("--- Semester ---", "0"));
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnOpenJadwal_Click(object sender, EventArgs e)
        {
            if (this.DLSemester.SelectedIndex == 0)
            {
                return;
            }

            if (this.DLProdi.SelectedIndex == 0)
            {
                return;
            }


            JadwalPerhari();
        }
    }
}