using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Portal
{
    //public partial class WebForm7 : System.Web.UI.Page
    public partial class WebForm7 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;

                this.PanelEditJadwal.Enabled = false;
                this.PanelEditJadwal.Visible = false;
            }
        }

        protected void BtnJadwal_Click(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DlSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Jadwal -------------------------------------------
                con.Open();
                //SqlCommand CmdJadwal = new SqlCommand("SpJadwalUTS", con);
                SqlCommand CmdJadwal = new SqlCommand("SpJadwalUTS2", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("Key");
                TableJadwal.Columns.Add("Kode");
                TableJadwal.Columns.Add("Mata Kuliah");
                TableJadwal.Columns.Add("Dosen");
                TableJadwal.Columns.Add("Kelas");
                TableJadwal.Columns.Add("Hari");
                TableJadwal.Columns.Add("Tanggal");
                TableJadwal.Columns.Add("Mulai");
                TableJadwal.Columns.Add("Selesai");
                TableJadwal.Columns.Add("Ruang");
                TableJadwal.Columns.Add("Jenis Kelas");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbJadwalResult.Text = "";

                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["Key"] = rdr["no_jadwal"];
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Hari"] = rdr["hari_uts"];
                            if (rdr["tgl_uts"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uts"]);
                                datarow["Tanggal"] = TglUjian.ToString("yyyy-MM-dd");
                            }
                            datarow["Mulai"] = rdr["jam_mulai_uts"];
                            datarow["Selesai"] = rdr["jam_sls_uts"];
                            datarow["Ruang"] = rdr["ruang_uts"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJadwalUTS.DataSource = TableJadwal;
                        this.GVJadwalUTS.DataBind();

                        // hide panel Edit Jadwal
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Jadwal UTS Belum Ada ";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        //hide panel Jadwal
                        this.PanelJadwal.Enabled = false;
                        this.PanelJadwal.Visible = false;

                        // hide panel Edit Jadwal
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVJadwalUTS.DataSource = TableJadwal;
                        GVJadwalUTS.DataBind();
                    }
                }
            }
        }

        protected void BnUpdateJadwal_Click(object sender, EventArgs e)
        {
            // form validation
            if (this.DLHari.SelectedItem.Text == "Hari")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Hari Ujian');", true);
                return;
            }

            if (Request.Form[this.TbTanggal.UniqueID] == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Ujian');", true);
                return;
            }

            if (this.TbMulai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Mulai Ujian');", true);
                return;
            }

            if (this.TbSelesai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Selesai Ujian');", true);
                return;
            }

            if (this.TbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Ujian');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SpUpJadwalUTS", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.LbIdJadwal.Text);
                CmdJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text.Trim());
                CmdJadwal.Parameters.AddWithValue("@tanggal", Request.Form[this.TbTanggal.UniqueID]);
                CmdJadwal.Parameters.AddWithValue("@mulai", this.TbMulai.Text);
                CmdJadwal.Parameters.AddWithValue("@selesai", this.TbSelesai.Text);
                CmdJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);

                CmdJadwal.ExecuteNonQuery();

                // hide panel Edit Jadwal
                this.PanelEditJadwal.Enabled = false;
                this.PanelEditJadwal.Visible = false;

                this.DLHari.SelectedIndex = 0;

                BtnJadwal_Click(this, null);
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //enable tb tanggal
            this.TbTanggal.ReadOnly = false;

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            this.PanelEditJadwal.Enabled = true;
            this.PanelEditJadwal.Visible = true;

            this.PanelJadwal.Visible = false;
            this.PanelJadwal.Enabled = false;

            this.LbIdJadwal.Text = this.GVJadwalUTS.Rows[index].Cells[1].Text;
            this.LbMakul.Text = this.GVJadwalUTS.Rows[index].Cells[3].Text;
            this.LbDosen.Text = this.GVJadwalUTS.Rows[index].Cells[4].Text;
            this.LbKelas.Text = this.GVJadwalUTS.Rows[index].Cells[5].Text;
            if (this.GVJadwalUTS.Rows[index].Cells[6].Text == "&nbsp;")
            {
                //this.DLHari.SelectedItem.Text = "Hari";
            }
            else
            {
                this.DLHari.SelectedItem.Text = this.GVJadwalUTS.Rows[index].Cells[6].Text;
            }
            if (this.GVJadwalUTS.Rows[index].Cells[7].Text == "&nbsp;")
            {
                this.TbTanggal.Text = "";
            }
            else
            {
                this.TbTanggal.Text = this.GVJadwalUTS.Rows[index].Cells[7].Text;
            }

            if (this.GVJadwalUTS.Rows[index].Cells[8].Text == "&nbsp;")
            {
                this.TbMulai.Text = "";
            }
            else
            {
                this.TbMulai.Text = this.GVJadwalUTS.Rows[index].Cells[8].Text;
            }
            if (this.GVJadwalUTS.Rows[index].Cells[9].Text == "&nbsp;")
            {
                this.TbSelesai.Text = "";
            }
            else
            {
                this.TbSelesai.Text = this.GVJadwalUTS.Rows[index].Cells[9].Text;
            }
            if (this.GVJadwalUTS.Rows[index].Cells[10].Text == "&nbsp;")
            {
                this.TbRuang.Text = "";
            }
            else
            {
                this.TbRuang.Text = this.GVJadwalUTS.Rows[index].Cells[10].Text;
            }
            this.LbJenisKelas.Text = this.GVJadwalUTS.Rows[index].Cells[11].Text;

            //disable tb tanggal
            this.TbTanggal.ReadOnly = true;

        }

        protected void GVJadwalUTS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

        protected void GVJadwalUTS_PreRender(object sender, EventArgs e)
        {
            if (this.GVJadwalUTS.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVJadwalUTS.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVJadwalUTS.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}