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
    //public partial class WebForm9 : System.Web.UI.Page
    public partial class WebForm9 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;

                this.PanelDetailMakul.Enabled = false;
                this.PanelDetailMakul.Visible = false;
            }
        }

        protected void DLProdiMakul_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpGetMakul", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.DLProdiMakul.SelectedValue);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailMakul.Enabled = true;
                        this.PanelDetailMakul.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVMakul.DataSource = TableMakul;
                        this.GVMakul.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVMakul.DataSource = TableMakul;
                        GVMakul.DataBind();

                        this.PanelDetailMakul.Enabled = false;
                        this.PanelDetailMakul.Visible = false;
                    }
                }
            }
        }

        protected void CBMakul_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBMakul");
                if (ch.Checked == true)
                {

                    this.LbKodeMakul.Text = this.GVMakul.Rows[i].Cells[1].Text;
                    this.LbMakul.Text = this.GVMakul.Rows[i].Cells[2].Text;
                    //Response.Write("DataKeyName: " + GVMakul.DataKeys[i].Value.ToString());
                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVMakul.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVMakul.Rows[i].FindControl("CBMakul");
                ch.Checked = false;
            }

            // label prodi
            this.LbProdi.Text = this.DLProdiMakul.SelectedValue;

            //Select Drop Down List to Default
            this.DLProdiMakul.SelectedIndex = 0;

            //hide panel
            this.PanelDetailMakul.Enabled = false;
            this.PanelDetailMakul.Visible = false;
        }

        protected void DLProdiDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SpGetDosen", con);
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
            if (this.DLHari.SelectedItem.Text == "Hari")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Hari Kuliah');", true);
                return;
            }
            if (this.TbMulai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Mulai Perkuliahan');", true);
                return;
            }
            if (this.TbSelesai.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jam Selesai Perkuliahan');", true);
                return;
            }
            if (this.TbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Perkuliahan');", true);
                return;
            }
            if (this.DLJenisKelas.SelectedItem.Text == "Jenis Kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas Mahasiswa');", true);
                return;
            }
            if (this.TbQuota.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Quota / Kapasitas Mahasiswa');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpInJadwalKuliah", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CmdInJadwal.ExecuteNonQuery();

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