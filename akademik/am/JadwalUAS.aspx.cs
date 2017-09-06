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
    //public partial class WebForm5 : System.Web.UI.Page
    public partial class WebForm5 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;

                //this.PanelEditJadwal.Enabled = false;
                //this.PanelEditJadwal.Visible = false;

                PopulateProdi();
            }
        }

        private void PopulateProdi()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnJadwalUAS_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- Read Jadwal -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SpJadwalUAS", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.DLProdi.SelectedValue);
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

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
                            datarow["Hari"] = rdr["hari_uas"];
                            if (rdr["tgl_uas"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uas"]);
                                datarow["Tanggal"] = TglUjian.ToString("yyyy-MM-dd");
                            }
                            datarow["Mulai"] = rdr["jam_mulai_uas"];
                            datarow["Selesai"] = rdr["jam_sls_uas"];
                            datarow["Ruang"] = rdr["ruang_uas"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJadwalUAS.DataSource = TableJadwal;
                        this.GVJadwalUAS.DataBind();

                        //// hide panel Edit Jadwal
                        //this.PanelEditJadwal.Enabled = false;
                        //this.PanelEditJadwal.Visible = false;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Jadwal UTS Belum Ada ";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        //hide panel Jadwal
                        this.PanelJadwal.Enabled = false;
                        this.PanelJadwal.Visible = false;

                        // hide panel Edit Jadwal
                        //this.PanelEditJadwal.Enabled = false;
                        //this.PanelEditJadwal.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVJadwalUAS.DataSource = TableJadwal;
                        GVJadwalUAS.DataBind();
                    }
                }
            }
        }


        protected void BtnEditUAS_Click(object sender, EventArgs e)
        {
        //    //enable tb tanggal
        //    this.TbTanggal.ReadOnly = false;

        //    // get row index
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    int index = gvRow.RowIndex;

        //    this.PanelEditJadwal.Enabled = true;
        //    this.PanelEditJadwal.Visible = true;

        //    this.PanelJadwal.Visible = false;
        //    this.PanelJadwal.Enabled = false;

        //    this.LbIdJadwal.Text = this.GVJadwalUAS.Rows[index].Cells[1].Text;
        //    this.LbMakul.Text = this.GVJadwalUAS.Rows[index].Cells[3].Text;
        //    this.LbDosen.Text = this.GVJadwalUAS.Rows[index].Cells[4].Text;
        //    this.LbKelas.Text = this.GVJadwalUAS.Rows[index].Cells[5].Text;
        //    if (this.GVJadwalUAS.Rows[index].Cells[6].Text == "&nbsp;")
        //    {
        //        //this.DLHari.SelectedItem.Text = "Hari";
        //    }
        //    else
        //    {
        //        this.DLHari.SelectedItem.Text = this.GVJadwalUAS.Rows[index].Cells[6].Text;
        //    }
        //    if (this.GVJadwalUAS.Rows[index].Cells[7].Text == "&nbsp;")
        //    {
        //        this.TbTanggal.Text = "";
        //    }
        //    else
        //    {
        //        this.TbTanggal.Text = this.GVJadwalUAS.Rows[index].Cells[7].Text;
        //    }

        //    if (this.GVJadwalUAS.Rows[index].Cells[8].Text == "&nbsp;")
        //    {
        //        this.TbMulai.Text = "";
        //    }
        //    else
        //    {
        //        this.TbMulai.Text = this.GVJadwalUAS.Rows[index].Cells[8].Text;
        //    }
        //    if (this.GVJadwalUAS.Rows[index].Cells[9].Text == "&nbsp;")
        //    {
        //        this.TbSelesai.Text = "";
        //    }
        //    else
        //    {
        //        this.TbSelesai.Text = this.GVJadwalUAS.Rows[index].Cells[9].Text;
        //    }
        //    if (this.GVJadwalUAS.Rows[index].Cells[10].Text == "&nbsp;")
        //    {
        //        this.TbRuang.Text = "";
        //    }
        //    else
        //    {
        //        this.TbRuang.Text = this.GVJadwalUAS.Rows[index].Cells[10].Text;
        //    }
        //    this.LbJenisKelas.Text = this.GVJadwalUAS.Rows[index].Cells[11].Text;

        //    //disable tb tanggal
        //    this.TbTanggal.ReadOnly = true;
        }

        protected void GVJadwalUAS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
}