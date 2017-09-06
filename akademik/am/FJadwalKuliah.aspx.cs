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
    public partial class WebForm40 :Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LbJadwalResult.Text = "";

                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;

                this.PanelEditJadwal.Enabled = false;
                this.PanelEditJadwal.Visible = false;

                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;

                this.PanelJadwalKuliah.Enabled = false;
                this.PanelJadwalKuliah.Visible = false;

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

        protected void BtnJadwal_Click(object sender, EventArgs e)
        {
            //set label thn & semester
            this.LbSmstr.Text = "";
            this.LbThn.Text = "";

            //form validation
            if (this.DLProdi.SelectedItem.Text == "Program Studi")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }

            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }

            if (this.DlSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SPPesertaMakulFeeder", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("Key");
                TableJadwal.Columns.Add("Kode");
                TableJadwal.Columns.Add("Mata Kuliah");
                TableJadwal.Columns.Add("SKS");
                TableJadwal.Columns.Add("Dosen");
                TableJadwal.Columns.Add("Kelas");
                TableJadwal.Columns.Add("Hari");
                TableJadwal.Columns.Add("Jenis Kelas");
                TableJadwal.Columns.Add("Quota");
                TableJadwal.Columns.Add("Peserta");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbSmstr.Text = this.DlSemester.SelectedItem.Text;
                        this.LbThn.Text = this.DLTahun.SelectedItem.Text;

                        this.LbJadwalResult.Text = "";

                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["Key"] = rdr["no_jadwal"];
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Hari"] = rdr["hari"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Quota"] = rdr["quota"];
                            datarow["Peserta"] = rdr["jumlah"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJadwal.DataSource = TableJadwal;
                        this.GVJadwal.DataBind();

                        this.PanelJadwalKuliah.Enabled = true;
                        this.PanelJadwalKuliah.Visible = true;

                        // hide panel Edit Mata Kuliah
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Jadwal Belum Ada ";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        // hide panel Edit Mata Kuliah
                        this.PanelEditJadwal.Enabled = false;
                        this.PanelEditJadwal.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVJadwal.DataSource = TableJadwal;
                        GVJadwal.DataBind();
                    }
                }
            }
        }

        protected void DLProdiDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SpFeederGetDosen", con);
                CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.DLProdiDosen.SelectedValue);

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("NIDN");
                TableDosen.Columns.Add("Nama");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailDosen.Enabled = true;
                        this.PanelDetailDosen.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["NIDN"] = rdr["nidn"];
                            datarow["Nama"] = rdr["nama"];

                            TableDosen.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVDosen.DataSource = TableDosen;
                        this.GVDosen.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVDosen.DataSource = TableDosen;
                        GVDosen.DataBind();

                        this.PanelDetailDosen.Enabled = true;
                        this.PanelDetailDosen.Visible = true;
                    }
                }
            }
        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

        }

        protected void GVJadwal_PreRender(object sender, EventArgs e)
        {
            if (GVJadwal.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVJadwal.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVJadwal.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            //set lb no jadwal
            this.lbno_jadwal.Text = this.GVJadwal.Rows[index].Cells[3].Text;

            //hide panel jadwal
            this.PanelJadwal.Enabled = false;
            this.PanelJadwal.Visible = false;

            //unhide panel edit jadwal
            this.PanelEditJadwal.Enabled = true;
            this.PanelEditJadwal.Visible = true;

            //read record you want to display
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SpFeederListJadwal2B", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[3].Text);

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.LbProdi.Text = rdr["id_prog_study"].ToString();
                            this.LbKodeMakul.Text = rdr["kode_makul"].ToString();
                            this.LbMakul.Text = rdr["makul"].ToString();
                            this.LbNidn.Text = rdr["nidn"].ToString();
                            this.LbDosen.Text = rdr["nama"].ToString();
                            this.DLKelas.SelectedItem.Text = rdr["kelas"].ToString();
                            //this.DLHari.SelectedItem.Text = rdr["hari"].ToString();
                            //this.TbMulai.Text = rdr["jam_mulai"].ToString();
                            //this.TbSelesai.Text = rdr["jam_selesai"].ToString();
                            //this.TbRuang.Text = rdr["ruang"].ToString();
                            //this.DLJenisKelas.SelectedItem.Text = rdr["jenis_kelas"].ToString();
                            //this.TbQuota.Text = rdr["quota"].ToString();
                            //// ----------- semester ----------------
                            //string sms = rdr["semester"].ToString();
                            //this.DLTahun2.SelectedValue = sms.Substring(0, 4);
                            //this.DLSemester2.SelectedValue = sms.Substring(4, 1);

                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        protected void CbDosen_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVDosen.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CbDosen");
                if (ch.Checked == true)
                {
                    this.LbDosen.Text = this.GVDosen.Rows[i].Cells[2].Text;
                    this.LbNidn.Text = this.GVDosen.Rows[i].Cells[1].Text;
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVDosen.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CbDosen");
                ch.Checked = false;
            }

            //Select Drop Down List to Default
            this.DLProdiDosen.SelectedIndex = 0;

            //hide panel
            this.PanelDetailDosen.Enabled = false;
            this.PanelDetailDosen.Visible = false;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //Form Validation
            if (this.lbno_jadwal.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('id jadwal does not set correctly');", true);
                return;
            }

            if (this.LbProdi.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.LbKodeMakul.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }
            if (this.LbNidn.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Dosen Pengampu');", true);
                return;
            }
            if (this.DLKelas.SelectedItem.Text == "Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Pembagian Kelas Mahasiswa');", true);
                return;
            }

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpFeederUpJadwalKuliah2A", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@no_jadwal", this.lbno_jadwal.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@semester", this.LbThn.Text + this.LbSmstr.Text);

                    CmdInJadwal.ExecuteNonQuery();

                    this.lbno_jadwal.Text = "";

                    this.PanelEditJadwal.Enabled = false;
                    this.PanelEditJadwal.Visible = false;

                    this.DLKelas.SelectedIndex = 0;

                    DLKelas.ClearSelection();

                    BtnJadwal_Click(this, null);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
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