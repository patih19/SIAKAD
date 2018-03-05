using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Hosting;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Portal
{
    public partial class MhsTunggu : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateMhsTunggu();
                CheckedMhsSelesaiKuliah(this.Session["level"].ToString());
                TahunAkademik();
            }
        }

        private void TahunAkademik()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand(""+
                    " DECLARE @sem5 VARCHAR(5) "+
                    " DECLARE @jenjang5 VARCHAR(2) "+

                    " SELECT        @jenjang5 = jenjang "+
                    " FROM            bak_prog_study WHERE id_prog_study = @id_prodi5 "+

                    " IF @jenjang5 = 'D3' "+
                    " BEGIN "+
                        " SET @jenjang5 = 'S1' "+
                    " END "+

                    " SELECT TOP 1 bak_kal.thn AS thn,RIGHT( semester,1) AS semester, CAST(bak_kal.thn AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +1 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN (@jenjang5) AND semester NOT IN ('new') group by thn,semester ORDER BY thn DESC,semester DESC " +
                    "", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;
                    CmdJadwal.Parameters.AddWithValue("@id_prodi5", this.Session["level"].ToString());

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if(rdr.HasRows)
                        {
                            while(rdr.Read())
                            {
                                this.LbThnAkademik.Text = rdr["ThnAkm"].ToString().Trim();

                                if (rdr["semester"].ToString().Trim() == "1")
                                {
                                    this.LbSemester.Text = "Gasal";
                                }
                                else if (rdr["semester"].ToString().Trim() == "2")
                                {
                                    this.LbSemester.Text = "Genap";
                                }
                                
                            }
                        }
                    }

                    con.Close();
                    con.Dispose();
                }


            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void PopulateMhsTunggu()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdTidakKRS = new SqlCommand("" +
                        "DECLARE @sem VARCHAR(5) " +
                        "DECLARE @jenjang VARCHAR(2) " +

                        "SELECT        @jenjang=jenjang "+
                        "FROM            bak_prog_study WHERE id_prog_study = @id_prodi "+

                        "IF @jenjang = 'D3' " +
                        "BEGIN "+
                            "SET @jenjang = 'S1' "+
                        "END "+

                        "SELECT TOP 1 @sem = semester "+
                        "FROM            bak_kal "+
                        "WHERE (jenjang = @jenjang) AND(semester NOT IN('new')) "+
                        "ORDER BY semester DESC "+

                        "SELECT ROW_NUMBER() over(ORDER BY MhsTidakKrs.npm DESC) AS nomor, MhsTidakKrs.npm, MhsTidakKrs.nama, MhsTidakKrs.id_prog_study, MhsTidakKrs.thn_angkatan, MhsTidakKrs.status, MhsTidakKrs.semester, MhsTunggu.id_tunggu FROM " +
                        "( "+ 
                            "SELECT  DataMhs.npm, DataMhs.nama, DataMhs.id_prog_study, DataMhs.thn_angkatan, DataMhs.status, @sem AS semester FROM ( "+
                            "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester "+
                            "FROM            bak_jadwal INNER JOIN "+
                                                     "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                     "bak_mahasiswa ON bak_krs.npm = bak_mahasiswa.npm "+
                            "WHERE semester = @sem AND bak_mahasiswa.id_prog_study = @id_prodi "+
                            "GROUP BY bak_mahasiswa.npm, bak_mahasiswa.nama, bak_jadwal.semester, bak_mahasiswa.status "+
                            ") AS MhsKrs RIGHT OUTER JOIN "+
                            "( "+
                                "SELECT        npm, nama, thn_angkatan, id_prog_study, status "+
                                "FROM            bak_mahasiswa "+
                                "WHERE status IN ('A', 'N') "+
                            ") AS DataMhs ON DataMhs.npm = MhsKrs.npm "+
                            "WHERE MhsKrs.npm IS NULL AND DataMhs.id_prog_study = @id_prodi "+

                        ") AS MhsTidakKrs LEFT OUTER JOIN "+
                        "( "+
                            "SELECT * FROM bak_mhs_tunggu_wisuda "+
                        ") AS MhsTunggu ON MhsTunggu.npm = MhsTidakKrs.npm AND MhsTunggu.semester = MhsTidakKrs.semester "+
                        "", con);
                    CmdTidakKRS.CommandType = System.Data.CommandType.Text;

                    CmdTidakKRS.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("No");
                    TableJadwal.Columns.Add("NPM");
                    TableJadwal.Columns.Add("Nama");
                    TableJadwal.Columns.Add("Tahun Angkatan");
                    TableJadwal.Columns.Add("Status Semester Ini");

                    using (SqlDataReader rdr = CmdTidakKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelRekapAktif.Enabled = true;
                            this.PanelRekapAktif.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["No"] = rdr["nomor"];
                                datarow["NPM"] = rdr["npm"];
                                datarow["Nama"] = rdr["nama"];
                                datarow["Tahun Angkatan"] = rdr["thn_angkatan"];
                                if (rdr["status"].ToString().Trim() == "N")
                                {
                                    datarow["Status Semester Ini"] = "Non Aktif";
                                }
                                else if (rdr["status"].ToString().Trim() == "A")
                                {
                                    datarow["Status Semester Ini"] = "Aktif";
                                }

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvMhsTunggu.DataSource = TableJadwal;
                            this.GvMhsTunggu.DataBind();

                        }
                        else
                        {
                            this.PanelRekapAktif.Enabled = false;
                            this.PanelRekapAktif.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GvMhsTunggu.DataSource = TableJadwal;
                            GvMhsTunggu.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void CheckedMhsSelesaiKuliah(string id_prodi)
        {
            string npm;

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    for (int i = 0; i < this.GvMhsTunggu.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GvMhsTunggu.Rows[i].FindControl("CBSelesai");
                        npm = this.GvMhsTunggu.Rows[i].Cells[1].Text.Trim();

                        SqlCommand CmdMhsSelesai = new SqlCommand("" +
                            "DECLARE @sem VARCHAR(5) "+
                            "DECLARE @jenjang VARCHAR(2) "+

                            "SELECT        @jenjang = jenjang "+
                            "FROM            bak_prog_study WHERE id_prog_study = @id_prodi "+

                            "IF @jenjang = 'D3' "+
                            "BEGIN "+
                                "SET @jenjang = 'S1' "+
                            "END "+

                            "SELECT TOP 1 @sem = semester "+
                            "FROM            bak_kal "+
                            "WHERE(jenjang = @jenjang) AND(semester NOT IN('new')) "+
                            "ORDER BY semester DESC "+

                            "SELECT        bak_mhs_tunggu_wisuda.id_tunggu, bak_mhs_tunggu_wisuda.npm, bak_mahasiswa.id_prog_study, bak_mhs_tunggu_wisuda.semester "+
                            "FROM            bak_mhs_tunggu_wisuda INNER JOIN "+
                                                    " bak_mahasiswa ON bak_mhs_tunggu_wisuda.npm = bak_mahasiswa.npm "+
                            "WHERE semester = @sem AND dbo.bak_mahasiswa.npm= @npm " +

                        "", con);
                        CmdMhsSelesai.CommandType = System.Data.CommandType.Text;

                        CmdMhsSelesai.Parameters.AddWithValue("@id_prodi", id_prodi);
                        CmdMhsSelesai.Parameters.AddWithValue("@npm", npm);

                        using (SqlDataReader rdr = CmdMhsSelesai.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    ch.Checked = true;
                                    string hex = "#f9f9b7";
                                    this.GvMhsTunggu.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                }
                            } else
                            {
                                ch.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void CBSelesai_CheckedChanged(object sender, EventArgs e)
        {
            string npm = "";

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            npm= this.GvMhsTunggu.Rows[index].Cells[1].Text.Trim();
            CheckBox ch = (CheckBox)GvMhsTunggu.Rows[index].FindControl("CBSelesai");

            if (ch.Checked == true)
            {
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //------------------------------------------------------------------------------------
                        con.Open();

                        SqlCommand CmdUpdateMhs = new SqlCommand("" +
                        " DECLARE @sem3 VARCHAR (5) " +
                        " DECLARE @jenjang3 VARCHAR(2) " +

                        " SELECT        @jenjang3 = jenjang " +
                        " FROM            bak_prog_study WHERE id_prog_study = @id_prodi3" +

                        " IF @jenjang3 = 'D3' " +
                        " BEGIN " +
                            " SET @jenjang3 = 'S1' " +
                        " END " +

                        " SELECT TOP 1 @sem3 = semester " +
                        " FROM            bak_kal " +
                        " WHERE (jenjang = @jenjang3) AND(semester NOT IN('new')) " +
                        " ORDER BY semester DESC " +

                        " SELECT * FROM bak_mhs_tunggu_wisuda WHERE npm = @npm3 AND semester = @sem3 " +
                        " IF @@ROWCOUNT < 1 " +
                        " BEGIN " +
                            " INSERT INTO bak_mhs_tunggu_wisuda(npm, semester, tgl_update) VALUES(@npm3, @sem3, GETDATE()) " +
                        " END " +
                        " ELSE " +
                        " BEGIN " +
                            " RAISERROR ('DATA SUDAH ADA', 16, 10) " +
                            " RETURN " +
                        " END " +
                        "", con);
                        CmdUpdateMhs.CommandType = System.Data.CommandType.Text;

                        CmdUpdateMhs.Parameters.AddWithValue("@id_prodi3", this.Session["level"].ToString());
                        CmdUpdateMhs.Parameters.AddWithValue("@npm3", npm);

                        CmdUpdateMhs.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }

            }
            else
            {
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        //------------------------------------------------------------------------------------
                        con.Open();

                        SqlCommand CmdUpdateMhs = new SqlCommand("" +
                        " DECLARE @sem4 VARCHAR(5) "+
                        " DECLARE @jenjang4 VARCHAR(2) "+

                        " SELECT        @jenjang4 = jenjang "+
                        " FROM            bak_prog_study WHERE id_prog_study = @id_prodi4 " +

                        " IF @jenjang4 = 'D3' "+
                        " BEGIN "+
                            " SET @jenjang4 = 'S1' "+
                        " END "+

                        " SELECT TOP 1 @sem4 = semester "+
                        " FROM            bak_kal "+
                        " WHERE (jenjang = @jenjang4) AND(semester NOT IN('new')) "+
                        " ORDER BY semester DESC "+

                        " SELECT * FROM bak_mhs_tunggu_wisuda WHERE npm = @npm4 AND semester = @sem4 "+
                        " IF @@ROWCOUNT > 0 "+
                        " BEGIN "+
                            " DELETE FROM bak_mhs_tunggu_wisuda WHERE npm = @npm4 AND semester = @sem4 "+
                        " END "+
                        "", con);
                        CmdUpdateMhs.CommandType = System.Data.CommandType.Text;

                        CmdUpdateMhs.Parameters.AddWithValue("@id_prodi4", this.Session["level"].ToString());
                        CmdUpdateMhs.Parameters.AddWithValue("@npm4", npm);

                        CmdUpdateMhs.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }

               
            }

            PopulateMhsTunggu();
            CheckedMhsSelesaiKuliah(this.Session["level"].ToString());
        }

        protected void GvMhsTunggu_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GvMhsTunggu.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void GvMhsTunggu_PreRender(object sender, EventArgs e)
        {
            if (this.GvMhsTunggu.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GvMhsTunggu.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GvMhsTunggu.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVAktif.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}