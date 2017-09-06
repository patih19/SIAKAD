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
    //public partial class WebForm23 : System.Web.UI.Page
    public partial class WebForm23 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.Write(this.Session["Semester"].ToString().Trim());

                this.DLProdiMakul.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdiMakul.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));

                this.PanelDetailDosen.Enabled = false;
                this.PanelDetailDosen.Visible = false;

                this.PanelDetailMakul.Enabled = false;
                this.PanelDetailMakul.Visible = false;

                this.PanelDetailRuang.Enabled = false;
                this.PanelDetailRuang.Visible = false;

                this.PanelUpdate.Enabled = false;
                this.PanelUpdate.Visible = false;

                PanelRuangAktif.Enabled = false;
                PanelRuangAktif.Visible = false;

                PanelJamMengajar.Enabled = false;
                PanelJamMengajar.Visible = false;

                PopulateProdi();
                PopulateProdi2();
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

                    this.DLProdiDosen.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdiDosen.DataTextField = "prog_study";
                    this.DLProdiDosen.DataValueField = "id_prog_study";
                    this.DLProdiDosen.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdiDosen.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void PopulateProdi2()
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

                    this.DLRuangProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLRuangProdi.DataTextField = "prog_study";
                    this.DLRuangProdi.DataValueField = "id_prog_study";
                    this.DLRuangProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLRuangProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
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
                TableMakul.Columns.Add("KURIKULUM");
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
                            datarow["KURIKULUM"] = rdr["kurikulum"];
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

                        this.PanelDetailDosen.Enabled = false;
                        this.PanelDetailDosen.Visible = false;
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
            //if (this.TbRuang.Text == "")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Perkuliahan');", true);
            //    return;
            //}
            if (this.LbRuang.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Ruang Perkuliahan');", true);
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

            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpInJadwalKuliah2", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                    //CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    CmdInJadwal.Parameters.AddWithValue("@id_ruang", this.LbNo.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

                    CmdInJadwal.ExecuteNonQuery();

                    // ------------- Display Jadwal ----------------------------
                    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal3", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwal.Parameters.AddWithValue("@semester",this.Session["Semester"].ToString().Trim() );

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Key");
                    TableJadwal.Columns.Add("Kode");
                    TableJadwal.Columns.Add("Mata Kuliah");
                    TableJadwal.Columns.Add("SKS");
                    TableJadwal.Columns.Add("Quota");
                    TableJadwal.Columns.Add("Dosen");
                    TableJadwal.Columns.Add("Kelas");
                    TableJadwal.Columns.Add("Hari");
                    TableJadwal.Columns.Add("Mulai");
                    TableJadwal.Columns.Add("Selesai");
                    TableJadwal.Columns.Add("Ruang");
                    TableJadwal.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            //this.LbSmstr.Text = this.DlSemester.SelectedItem.Text;
                            //this.LbThn.Text = this.DLTahun.SelectedItem.Text;

                            //this.LbJadwalResult.Text = "";

                            //this.PanelJadwal.Enabled = true;
                            //this.PanelJadwal.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Quota"] = rdr["quota"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari"];
                                datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                datarow["Ruang"] = rdr["nm_ruang"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwal;
                            this.GVJadwal.DataBind();

                            // hide panel Edit Mata Kuliah
                            //this.PanelEditJadwal.Enabled = false;
                            //this.PanelEditJadwal.Visible = false;
                        }
                        else
                        {
                            //this.LbJadwalResult.Text = "Jadwal Belum Ada ";
                            //this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                            // hide panel Edit Mata Kuliah
                            //this.PanelEditJadwal.Enabled = false;
                            //this.PanelEditJadwal.Visible = false;

                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVJadwal.DataSource = TableJadwal;
                            GVJadwal.DataBind();
                        }
                    }

                    // ------------------ Clear Data -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    this.LbRuang.Text = "";
                    this.LbNo.Text = "";

                    DLJenisKelas.SelectedIndex = 0;
                    TbQuota.Text = "";

                    //this.PanelRuangAktif.Enabled = false;
                    //this.PanelRuangAktif.Visible = false;

                    //if (this.Session["Semester"] != null)
                    //{
                    //    this.Session["Semester"] = (object)null;
                    //    this.Session.Remove("Semester");
                    //}

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);


                }
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //return;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //this.PanelMakul.Enabled = false;
            //this.PanelMakul.Visible = false;

            //this.PanelUpdate.Enabled = true;
            //this.PanelUpdate.Visible = true;

            //this.PanelTambah.Enabled = false;
            //this.PanelTambah.Visible = false;

            //// get row index
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;
            ////Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            ////set lb no jadwal
            //this.lbno_jadwal.Text = this.GVJadwal.Rows[index].Cells[2].Text;

            ////hide panel jadwal
            ////this.PanelJadwal.Enabled = false;
            ////this.PanelJadwal.Visible = false;

            ////unhide panel edit jadwal
            ////this.PanelEditJadwal.Enabled = true;
            ////this.PanelEditJadwal.Visible = true;

            ////read record you want to display
            //string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    //----------------------------------------- -------------------------------------------
            //    con.Open();
            //    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal2", con);
            //    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

            //    CmdJadwal.Parameters.AddWithValue("@no_jadwal", this.GVJadwal.Rows[index].Cells[2].Text);

            //    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
            //    {
            //        if (rdr.HasRows)
            //        {
            //            while (rdr.Read())
            //            {
            //                this.LbProdi.Text = rdr["id_prog_study"].ToString();
            //                this.LbKodeMakul.Text = rdr["kode_makul"].ToString();
            //                this.LbMakul.Text = rdr["makul"].ToString();
            //                this.LbNidn.Text = rdr["nidn"].ToString();
            //                this.LbDosen.Text = rdr["nama"].ToString();
            //                this.DLKelas.SelectedItem.Text = rdr["kelas"].ToString();
            //                this.DLHari.SelectedItem.Text = rdr["hari"].ToString();
            //                this.TbMulai.Text = rdr["jam_mulai"].ToString();
            //                this.TbSelesai.Text = rdr["jam_selesai"].ToString();
            //               // this.TbRuang.Text = rdr["ruang"].ToString();
            //                this.DLJenisKelas.SelectedItem.Text = rdr["jenis_kelas"].ToString();
            //                this.TbQuota.Text = rdr["quota"].ToString();
            //                // ----------- semester ----------------
            //                string sms = rdr["semester"].ToString();
            //                //this.DLTahun2.SelectedValue = sms.Substring(0, 4);
            //                //this.DLSemester2.SelectedValue = sms.Substring(4, 1);

            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //}
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //edit
            e.Row.Cells[1].Visible = false; //delete
            e.Row.Cells[2].Visible = false; //no jadwal
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
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
            //if (this.TbRuang.Text == "")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Ruang Perkuliahan');", true);
            //    return;
            //}
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


            try
            {
                // SpInJadwalKuliah
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdInJadwal = new SqlCommand("SpUpJadwalKuliah2", con);
                    CmdInJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdInJadwal.Parameters.AddWithValue("@no_jadwal", this.lbno_jadwal.Text);
                    CmdInJadwal.Parameters.AddWithValue("@id_prodi", this.LbProdi.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kode_makul", this.LbKodeMakul.Text);
                    CmdInJadwal.Parameters.AddWithValue("@nidn", this.LbNidn.Text);
                    CmdInJadwal.Parameters.AddWithValue("@kelas", this.DLKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jenis_kelas", this.DLJenisKelas.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_mulai", this.TbMulai.Text);
                    CmdInJadwal.Parameters.AddWithValue("@jam_selesai", this.TbSelesai.Text);
                   // CmdInJadwal.Parameters.AddWithValue("@ruang", this.TbRuang.Text);
                    CmdInJadwal.Parameters.AddWithValue("@quota", this.TbQuota.Text);
                    CmdInJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

                    CmdInJadwal.ExecuteNonQuery();

                    this.lbno_jadwal.Text = "";

                    //BtnJadwal_Click(this, null);

                    //this.PanelEditJadwal.Enabled = false;
                    //this.PanelEditJadwal.Visible = false;

                    this.PanelUpdate.Enabled = false;
                    this.PanelUpdate.Visible = false;

                    this.PanelTambah.Enabled = true;
                    this.PanelTambah.Visible = true;

                    // ------------------ Reload DGV ------------------------
                    SqlCommand CmdJadwal = new SqlCommand("SpListJadwal", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwal.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Key");
                    TableJadwal.Columns.Add("Kode");
                    TableJadwal.Columns.Add("Mata Kuliah");
                    TableJadwal.Columns.Add("SKS");
                    TableJadwal.Columns.Add("Quota");
                    TableJadwal.Columns.Add("Dosen");
                    TableJadwal.Columns.Add("Kelas");
                    TableJadwal.Columns.Add("Hari");
                    TableJadwal.Columns.Add("Mulai");
                    TableJadwal.Columns.Add("Selesai");
                    TableJadwal.Columns.Add("Ruang");
                    TableJadwal.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Quota"] = rdr["quota"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari"];
                                datarow["Mulai"] = rdr["jam_mulai"];
                                datarow["Selesai"] = rdr["jam_selesai"];
                                datarow["Ruang"] = rdr["ruang"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwal.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwal;
                            this.GVJadwal.DataBind();

                            // hide panel Edit Mata Kuliah

                        }
                        else
                        {
                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVJadwal.DataSource = TableJadwal;
                            GVJadwal.DataBind();
                        }
                    }

                    this.PanelMakul.Enabled = true;
                    this.PanelMakul.Visible = true;

                    // ------------------ Clear Data -----------------
                    LbProdi.Text = "";
                    LbKodeMakul.Text = "";
                    LbMakul.Text = "";

                    DLProdiMakul.SelectedIndex = 0;
                    LbNidn.Text = "";
                    LbDosen.Text = "";

                    DLProdiDosen.SelectedIndex = 0;
                    DLKelas.SelectedIndex = 0;
                    DLHari.SelectedIndex = 0;

                    TbMulai.Text = "";
                    TbSelesai.Text = "";

                    //TbRuang.Text = "";

                    DLJenisKelas.SelectedIndex = 0;
                    TbQuota.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void DLRuangProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SELECT * FROM  bak_ruang WHERE id_prog_study=@id_prodi AND status = 1", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.DLRuangProdi.SelectedValue);

                DataTable TableRuang = new DataTable();
                TableRuang.Columns.Add("No");
                TableRuang.Columns.Add("Ruang");
                TableRuang.Columns.Add("Kapasitas");


                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDetailRuang.Enabled = true;
                        this.PanelDetailRuang.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableRuang.NewRow();
                            datarow["No"] = rdr["no"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Kapasitas"] = rdr["kapasitas"];                            

                            TableRuang.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVRuang.DataSource = TableRuang;
                        this.GVRuang.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableRuang.Rows.Clear();
                        TableRuang.Clear();
                        GVRuang.DataSource = TableRuang;
                        GVRuang.DataBind();

                        this.PanelDetailRuang.Enabled = false;
                        this.PanelDetailRuang.Visible = false;
                    }
                }
            }
        }

        protected void GVRuang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //no ruang
        }

        protected void CbRuang_CheckedChanged(object sender, EventArgs e)
        {
            // Get Kode Mata Kuliah dan Mata Kuliah
            for (int i = 0; i < this.GVRuang.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVRuang.Rows[i].FindControl("CbRuang");
                if (ch.Checked == true)
                {
                    this.LbRuang.Text = this.GVRuang.Rows[i].Cells[2].Text;
                    this.LbNo.Text = this.GVRuang.Rows[i].Cells[1].Text;

                    //Response.Write(_idruang.ToString());
                    //Response.Write("DataKeyName: " + GVMakul.DataKeys[i].Value.ToString());



                }
            }

            // Clear selected checkbox
            for (int i = 0; i < this.GVRuang.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVRuang.Rows[i].FindControl("CbRuang");
                ch.Checked = false;
            }


            //Select Drop Down List to Default
            this.DLRuangProdi.SelectedIndex = 0;

            //hide panel
            this.PanelDetailRuang.Enabled = false;
            this.PanelDetailRuang.Visible = false;



        }

        protected void GVDosen_PreRender(object sender, EventArgs e)
        {
            if (this.GVDosen.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVDosen.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVDosen.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void GVMakul_PreRender(object sender, EventArgs e)
        {
            if (this.GVMakul.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVMakul.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVMakul.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVMakul.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void DLHari_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LbNidn.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Dosen Pengampu !!!');", true);
                return;
            }

            if (this.DLKelas.SelectedItem.Text == "kelas")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelas !!!');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SELECT        bak_jadwal.no_jadwal, bak_jadwal.hari, bak_jadwal.jm_awal_kuliah, bak_jadwal.jm_akhir_kuliah, bak_prog_study.prog_study, bak_makul.makul, bak_ruang.nm_ruang "+
                                                     "FROM            bak_jadwal INNER JOIN "+
                                                                             "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study INNER JOIN "+
                                                                             "bak_makul ON bak_jadwal.kode_makul = bak_makul.kode_makul INNER JOIN "+
                                                                             "bak_ruang ON bak_jadwal.id_rng_kuliah = bak_ruang.no  "+
                                                    "WHERE        (bak_jadwal.semester = @semester) AND (bak_jadwal.nidn = @nidn) AND (bak_jadwal.hari=@hari) " +
                                                    "ORDER BY bak_jadwal.jm_awal_kuliah", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());
                CmdDosen.Parameters.AddWithValue("@nidn", this.LbNidn.Text.Trim());
                CmdDosen.Parameters.AddWithValue("@hari", this.DLHari.SelectedItem.Text.Trim());

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("Hari");                
                TableDosen.Columns.Add("Mulai");
                TableDosen.Columns.Add("Selesai");
                TableDosen.Columns.Add("Ruang");
                TableDosen.Columns.Add("Makul");
                TableDosen.Columns.Add("Program Studi");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelJamMengajar.Enabled = true;
                        PanelJamMengajar.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["Hari"] = rdr["hari"];                            
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Makul"] = rdr["makul"];
                            datarow["Program Studi"] = rdr["prog_study"];

                            TableDosen.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVJamMengajar.DataSource = TableDosen;
                        this.GVJamMengajar.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVJamMengajar.DataSource = TableDosen;
                        GVJamMengajar.DataBind();

                        PanelJamMengajar.Enabled = false;
                        PanelJamMengajar.Visible = false;
                    }
                }



                SqlCommand CmdRuang = new SqlCommand("SpRuangAktif", con);
                CmdRuang.CommandType = System.Data.CommandType.StoredProcedure;

                CmdRuang.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());
                CmdRuang.Parameters.AddWithValue("@hari", this.DLHari.SelectedValue.Trim());
                CmdRuang.Parameters.AddWithValue("@idruang", this.LbNo.Text.Trim());

                DataTable TableRuang = new DataTable();
                TableRuang.Columns.Add("Hari");
                TableRuang.Columns.Add("Mulai");
                TableRuang.Columns.Add("Selesai");
                TableRuang.Columns.Add("Ruang");
                TableRuang.Columns.Add("Makul");

                TableRuang.Columns.Add("Jenis Kelas");
                TableRuang.Columns.Add("Program Studi");


                using (SqlDataReader rdr = CmdRuang.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        PanelRuangAktif.Enabled = true;
                        PanelRuangAktif.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableRuang.NewRow();
                            datarow["Hari"] = rdr["hari"];
                            datarow["Ruang"] = rdr["nm_ruang"];
                            datarow["Makul"] = rdr["makul"];
                            datarow["Mulai"] = rdr["jm_awal_kuliah"];
                            datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Program Studi"] = rdr["prog_study"];

                            TableRuang.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVRuangAktif.DataSource = TableRuang;
                        this.GVRuangAktif.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableRuang.Rows.Clear();
                        TableRuang.Clear();
                        GVRuangAktif.DataSource = TableRuang;
                        GVRuangAktif.DataBind();

                        PanelRuangAktif.Enabled = false;
                        PanelRuangAktif.Visible = false;
                    }
                }
            }


            ////--------- List Ruang Aktif -----------//
            //string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS2))
            //{
            //    con.Open();
            //    SqlCommand CmdRuang = new SqlCommand("SpRuangAktif", con);
            //    CmdRuang.CommandType = System.Data.CommandType.StoredProcedure;

            //    CmdRuang.Parameters.AddWithValue("@semester", this.Session["Semester"].ToString().Trim());
            //    CmdRuang.Parameters.AddWithValue("@hari", this.DLHari.SelectedValue.Trim());
            //    CmdRuang.Parameters.AddWithValue("@idruang", this.LbNo.Text.Trim());

            //    DataTable TableRuang = new DataTable();
            //    TableRuang.Columns.Add("Hari");
            //    TableRuang.Columns.Add("Mulai");
            //    TableRuang.Columns.Add("Selesai");
            //    TableRuang.Columns.Add("Ruang");
            //    TableRuang.Columns.Add("Makul");

            //    TableRuang.Columns.Add("Jenis Kelas");
            //    TableRuang.Columns.Add("Program Studi");


            //    using (SqlDataReader rdr = CmdRuang.ExecuteReader())
            //    {
            //        if (rdr.HasRows)
            //        {
            //            PanelRuangAktif.Enabled = true;
            //            PanelRuangAktif.Visible = true;

            //            while (rdr.Read())
            //            {
            //                DataRow datarow = TableRuang.NewRow();
            //                datarow["Hari"] = rdr["hari"];
            //                datarow["Ruang"] = rdr["nm_ruang"];
            //                datarow["Makul"] = rdr["makul"];
            //                datarow["Mulai"] = rdr["jm_awal_kuliah"];
            //                datarow["Selesai"] = rdr["jm_akhir_kuliah"];
            //                datarow["Jenis Kelas"] = rdr["jenis_kelas"];
            //                datarow["Program Studi"] = rdr["prog_study"];

            //                TableRuang.Rows.Add(datarow);
            //            }

            //            //Fill Gridview
            //            this.GVRuangAktif.DataSource = TableRuang;
            //            this.GVRuangAktif.DataBind();
            //        }
            //        else
            //        {
            //            //clear Gridview
            //            TableRuang.Rows.Clear();
            //            TableRuang.Clear();
            //            GVRuangAktif.DataSource = TableRuang;
            //            GVRuangAktif.DataBind();

            //            PanelRuangAktif.Enabled = false;
            //            PanelRuangAktif.Visible = false;
            //        }
            //    }
            //}

        }
    }
}