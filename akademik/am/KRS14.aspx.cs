using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace akademik.am
{
    public partial class WebForm23 : Bak_staff
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        public int _TotalSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        public int _TotalEditSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalEditSKS"].ToString()); }
            set { this.ViewState["TotalEditSKS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                this.PanelEditKRS.Visible = false;
                this.PanelKRS.Visible = false;
                this.PanelListKRS.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
                return;
            }
            if (this.RBInputKRS.Checked == false && this.RBEditKRS.Checked == false && this.RBList.Checked == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Menu KRS');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }


            //  ------- Read Data Mahasiswa ----------
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbTahun.Text = mhs.thn_angkatan.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();

                if (LbTahun.Text != "2014/2015")
                {
                    LbNama.Text = "Nama";
                    LbKelas.Text = "Kelas";
                    LbProdi.Text = "Program Studi";
                    LbTahun.Text = "Tahun Angkatan";
                    LbNpm.Text = "NPM";
                    LbIdProdi.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mahasiswa Tidak Ditemukan');", true);
                    return;
                }
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbTahun.Text = "Tahun Angkatan";
                LbNpm.Text = "NPM";
                LbIdProdi.Text = "";

                //clear Gridview
                DataTable TableKP = new DataTable();
                TableKP.Rows.Clear();
                TableKP.Clear();
                this.GVAmbilKRS.DataSource = TableKP;
                this.GVAmbilKRS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }

            //--------------------- INPUT KRS ------------------------------//
            if (RBInputKRS.Checked)
            {
                try
                {
                    _TotalSKS = 0;
                    _TotalEditSKS = 0;

                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        //// 1. ------------ Cek Masa KRS ------------
                        //SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                        ////CmdCekMasa.Transaction = trans;
                        //CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        //CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        //CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRS");

                        //SqlParameter Status = new SqlParameter();
                        //Status.ParameterName = "@output";
                        //Status.SqlDbType = System.Data.SqlDbType.VarChar;
                        //Status.Size = 20;
                        //Status.Direction = System.Data.ParameterDirection.Output;
                        //CmdCekMasa.Parameters.Add(Status);

                        //CmdCekMasa.ExecuteNonQuery();

                        //if (Status.Value.ToString() == "OUT")
                        //{
                        //    this.PanelEditKRS.Visible = false;
                        //    this.PanelKRS.Visible = false;

                        //    this.DLSemester.SelectedIndex = 0;

                        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                        //    return;
                        //}

                        // 2. --------------------- Cek KRS Semester Ini, error jika Sudah ada -----------------------
                        SqlCommand CekKRS = new SqlCommand("SpCekKrs", con);
                        CekKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CekKRS.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                        CekKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CekKRS.ExecuteNonQuery();

                        // --------------------- Fill Gridview  ------------------------
                        SqlCommand CmdKRS = new SqlCommand("SpListKRS2", con);
                        CmdKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdKRS.Parameters.AddWithValue("@id_prodi", LbIdProdi.Text);
                        CmdKRS.Parameters.AddWithValue("@jenis_kelas", LbKelas.Text);
                        CmdKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        DataTable TableKRS = new DataTable();
                        TableKRS.Columns.Add("Key");
                        TableKRS.Columns.Add("Kode");
                        TableKRS.Columns.Add("Mata Kuliah");
                        TableKRS.Columns.Add("SKS");
                        TableKRS.Columns.Add("Quota");
                        TableKRS.Columns.Add("Dosen");
                        TableKRS.Columns.Add("Kelas");
                        TableKRS.Columns.Add("Hari");
                        TableKRS.Columns.Add("Mulai");
                        TableKRS.Columns.Add("Selesai");
                        TableKRS.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdKRS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.PanelKRS.Enabled = true;
                                this.PanelKRS.Visible = true;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                while (rdr.Read())
                                {
                                    DataRow datarow = TableKRS.NewRow();
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

                                    TableKRS.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVAmbilKRS.DataSource = TableKRS;
                                this.GVAmbilKRS.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableKRS.Rows.Clear();
                                TableKRS.Clear();
                                GVAmbilKRS.DataSource = TableKRS;
                                GVAmbilKRS.DataBind();

                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Tidak Temukan');", true);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
            // --------------------------------- EDIT KRS --------------------------------------// 
            else if (this.RBEditKRS.Checked)
            {
                try
                {
                    //1. --------------- Gridview EDIT KRS Semester ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        ////1. ------ cek masa KRS -------
                        //SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                        ////CmdCekMasa.Transaction = trans;
                        //CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        //CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        //CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "KRS");

                        //SqlParameter Status = new SqlParameter();
                        //Status.ParameterName = "@output";
                        //Status.SqlDbType = System.Data.SqlDbType.VarChar;
                        //Status.Size = 20;
                        //Status.Direction = System.Data.ParameterDirection.Output;
                        //CmdCekMasa.Parameters.Add(Status);

                        //CmdCekMasa.ExecuteNonQuery();

                        //if (Status.Value.ToString() == "OUT")
                        //{
                        //    this.DLSemester.SelectedIndex = 0;

                        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Jadwal KRS, Proses dibatalkan ...');", true);
                        //    return;
                        //}

                        // 2. ----- Get status edit KRS ----------------
                        SqlCommand CmdCntKRS = new SqlCommand("SpCntEditKRS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCntKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCntKRS.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                        CmdCntKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                        CmdCntKRS.ExecuteNonQuery();


                        // 3. ----- Cek KRS Semester Ini, error jika belum ada -----------
                        SqlCommand CekKRS2 = new SqlCommand("SpCekKrs2", con);
                        CekKRS2.CommandType = System.Data.CommandType.StoredProcedure;

                        CekKRS2.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                        CekKRS2.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        CekKRS2.ExecuteNonQuery();

                        //------------- Fill Gridview Edit KRS ------------------------
                        SqlCommand CmdEdit = new SqlCommand("SpListKRS2", con);
                        CmdEdit.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdEdit.Parameters.AddWithValue("@id_prodi", LbIdProdi.Text);
                        CmdEdit.Parameters.AddWithValue("@jenis_kelas", this.LbKelas.Text);
                        CmdEdit.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        DataTable TableEdit = new DataTable();
                        TableEdit.Columns.Add("Key");
                        TableEdit.Columns.Add("Kode");
                        TableEdit.Columns.Add("Mata Kuliah");
                        TableEdit.Columns.Add("SKS");
                        TableEdit.Columns.Add("Quota");
                        TableEdit.Columns.Add("Dosen");
                        TableEdit.Columns.Add("Kelas");
                        TableEdit.Columns.Add("Hari");
                        TableEdit.Columns.Add("Mulai");
                        TableEdit.Columns.Add("Selesai");
                        TableEdit.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdEdit.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = true;
                                this.PanelEditKRS.Visible = true;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                while (rdr.Read())
                                {
                                    DataRow datarow = TableEdit.NewRow();
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

                                    TableEdit.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVEditKRS.DataSource = TableEdit;
                                this.GVEditKRS.DataBind();

                            }
                            else
                            {
                                //clear Gridview
                                TableEdit.Rows.Clear();
                                TableEdit.Clear();
                                GVEditKRS.DataSource = TableEdit;
                                GVEditKRS.DataBind();

                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Mahasiswa Tidak Ditemukan');", true);
                                return;
                            }
                        }

                        //-- 3. loop id jadwal yg sudah diambil berdasarkan no jadwal
                        //-- jika ada checked = true
                        int TotalSKS = 0;
                        for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                        {
                            SqlCommand CmdChecked = new SqlCommand("SpGetIdJadwalMhs", con);
                            CmdChecked.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdChecked.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);
                            CmdChecked.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                            CmdChecked.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            using (SqlDataReader rdrchecked = CmdChecked.ExecuteReader())
                            {
                                // ------ Cek Quota Makul (CHECKED) -------
                                if (rdrchecked.HasRows)
                                {
                                    while (rdrchecked.Read())
                                    {
                                        CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                                        ch.Checked = true;
                                        TotalSKS += Convert.ToInt32(this.GVEditKRS.Rows[i].Cells[5].Text);

                                        this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                                        try
                                        {
                                            string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                            using (SqlConnection con2 = new SqlConnection(CS))
                                            {
                                                con2.Open();

                                                SqlCommand CmdCekQuota = new SqlCommand("SpQuotaJadwal", con2);
                                                //CmdCekMasa.Transaction = trans;
                                                CmdCekQuota.CommandType = System.Data.CommandType.StoredProcedure;

                                                CmdCekQuota.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                                SqlParameter Quota = new SqlParameter();
                                                Quota.ParameterName = "@quota";
                                                Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                                                Quota.Size = 20;
                                                Quota.Direction = System.Data.ParameterDirection.Output;
                                                CmdCekQuota.Parameters.Add(Quota);

                                                SqlParameter Sisa = new SqlParameter();
                                                Sisa.ParameterName = "@sisa";
                                                Sisa.SqlDbType = System.Data.SqlDbType.Int;
                                                Sisa.Size = 20;
                                                Sisa.Direction = System.Data.ParameterDirection.Output;
                                                CmdCekQuota.Parameters.Add(Sisa);

                                                CmdCekQuota.ExecuteNonQuery();


                                                if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                                                {
                                                    Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                    LbSisa.Text = Sisa.Value.ToString();

                                                    LbSisa.ForeColor = System.Drawing.Color.Green;
                                                }
                                                else
                                                {
                                                    Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                    LbSisa.Text = "Penuh";

                                                    LbSisa.ForeColor = System.Drawing.Color.Red;

                                                    CheckBox ch2 = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");

                                                    //ch2.Visible = false;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah Checked");
                                        }
                                    }
                                }
                                else
                                {
                                    // ------ Cek Quota Makul (UNCHECKED)-------
                                    try
                                    {
                                        string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                                        using (SqlConnection con2 = new SqlConnection(CS))
                                        {
                                            con2.Open();

                                            SqlCommand CmdCekQuo = new SqlCommand("SpQuotaJadwal", con2);
                                            //CmdCekMasa.Transaction = trans;
                                            CmdCekQuo.CommandType = System.Data.CommandType.StoredProcedure;

                                            CmdCekQuo.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                            SqlParameter Quota = new SqlParameter();
                                            Quota.ParameterName = "@quota";
                                            Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                                            Quota.Size = 20;
                                            Quota.Direction = System.Data.ParameterDirection.Output;
                                            CmdCekQuo.Parameters.Add(Quota);

                                            SqlParameter Sisa = new SqlParameter();
                                            Sisa.ParameterName = "@sisa";
                                            Sisa.SqlDbType = System.Data.SqlDbType.Int;
                                            Sisa.Size = 20;
                                            Sisa.Direction = System.Data.ParameterDirection.Output;
                                            CmdCekQuo.Parameters.Add(Sisa);

                                            CmdCekQuo.ExecuteNonQuery();


                                            if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                                            {
                                                Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                LbSisa.Text = Sisa.Value.ToString();

                                                LbSisa.ForeColor = System.Drawing.Color.Green;
                                            }
                                            else
                                            {
                                                Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");
                                                LbSisa.Text = "Penuh";

                                                LbSisa.ForeColor = System.Drawing.Color.Red;

                                                CheckBox ch2 = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");

                                                ch2.Visible = false;
                                            }

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah Unchecked");
                                    }
                                }
                            }
                        }
                        this.LbJumlahEditSKS.Text = TotalSKS.ToString();
                    }
                }
                catch (Exception ex)
                {
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
            //--------------------------- LIHAT KRS ------------------------------ //
            else if (this.RBList.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                try
                {
                    //1. ---------- Gridview SKS ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // --------------------- Fill Gridview  ------------------------
                        SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                        CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdListKRS.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                        CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        DataTable TableKRS = new DataTable();
                        TableKRS.Columns.Add("Key");
                        TableKRS.Columns.Add("Kode");
                        TableKRS.Columns.Add("Mata Kuliah");
                        TableKRS.Columns.Add("SKS");
                        TableKRS.Columns.Add("Dosen");
                        TableKRS.Columns.Add("Kelas");
                        TableKRS.Columns.Add("Hari");
                        TableKRS.Columns.Add("Mulai");
                        TableKRS.Columns.Add("Selesai");
                        TableKRS.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = true;
                                this.PanelListKRS.Visible = true;

                                while (rdr.Read())
                                {
                                    DataRow datarow = TableKRS.NewRow();
                                    datarow["Key"] = rdr["no_jadwal"];
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    datarow["Dosen"] = rdr["nama"];
                                    datarow["Kelas"] = rdr["kelas"];
                                    datarow["Hari"] = rdr["hari"];
                                    datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                    datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                    datarow["Ruang"] = rdr["nm_ruang"];

                                    TableKRS.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVListKrs.DataSource = TableKRS;
                                this.GVListKrs.DataBind();

                                this.DLSemester.SelectedIndex = 0;
                            }
                            else
                            {
                                //clear Gridview
                                TableKRS.Rows.Clear();
                                TableKRS.Clear();
                                GVListKrs.DataSource = TableKRS;
                                GVListKrs.DataBind();

                                this.PanelKRS.Enabled = false;
                                this.PanelKRS.Visible = false;

                                this.PanelEditKRS.Enabled = false;
                                this.PanelEditKRS.Visible = false;

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.PanelKRS.Enabled = false;
                    this.PanelKRS.Visible = false;

                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        int PrevTotalSKS;
        int JumlahSKS;
        protected void CBMakul_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[index].FindControl("CBMakul");
            if (ch.Checked == true)
            {
                PrevTotalSKS = _TotalSKS;
                _TotalSKS += Convert.ToInt16(this.GVAmbilKRS.Rows[index].Cells[5].Text);
                this.GVAmbilKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                this.LbJumlahSKS.Text = _TotalSKS.ToString();

                //----- background makul checked ----- //
                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                    if (ch2.Checked == true)
                    {
                        this.GVAmbilKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                    }
                }

                // batas maximal KRS
                if (_TotalSKS > 24)
                {
                    ch.Checked = false;
                    this.LbJumlahSKS.Text = PrevTotalSKS.ToString();
                    this.GVAmbilKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFCEC1");
                    _TotalSKS = PrevTotalSKS;
                }
            }
            else
            {
                //----- hitung ulang sks berdasarkan makul checked ----- //
                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                    if (ch2.Checked == true)
                    {
                        JumlahSKS += Convert.ToInt32(this.GVAmbilKRS.Rows[i].Cells[5].Text);
                        _TotalSKS = JumlahSKS;
                        this.LbJumlahSKS.Text = JumlahSKS.ToString();
                    }
                }

                //----- background makul checked ----- //
                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                    if (ch2.Checked == true)
                    {
                        this.GVAmbilKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                    }
                }
            }
        }

        protected void GVAmbilKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            this.LbJumlahSKS.Text = "0";

            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
            //e.Row.Cells[8].Visible = false; 
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int BiayaSks = Convert.ToInt32(e.Row.Cells[1].Text);
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // ------ Cek Sisa Quota -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpQuotaJadwal", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[2].Text);

                        SqlParameter Quota = new SqlParameter();
                        Quota.ParameterName = "@quota";
                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                        Quota.Size = 20;
                        Quota.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Quota);

                        SqlParameter Sisa = new SqlParameter();
                        Sisa.ParameterName = "@sisa";
                        Sisa.SqlDbType = System.Data.SqlDbType.Int;
                        Sisa.Size = 20;
                        Sisa.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Sisa);

                        CmdCekMasa.ExecuteNonQuery();


                        if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = Sisa.Value.ToString();

                            LbSisa.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = "Penuh";

                            LbSisa.ForeColor = System.Drawing.Color.Red;

                            CheckBox ch = (CheckBox)e.Row.Cells[0].FindControl("CBMakul");

                            ch.Visible = false;
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah");
                }
            }
        }

        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            //  ------- Read Data Mahasiswa ----------
            //  -- hanya mahasiswa angkatan tahun 14/15 yang diperbolehkan --- 
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbTahun.Text = mhs.thn_angkatan.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();

                if (LbTahun.Text != "2014/2015")
                {
                    LbNama.Text = "Nama";
                    LbKelas.Text = "Kelas";
                    LbProdi.Text = "Program Studi";
                    LbTahun.Text = "Tahun Angkatan";
                    LbNpm.Text = "NPM";
                    LbIdProdi.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mahasiswa Tidak Ditemukan');", true);
                    return;
                }
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbTahun.Text = "Tahun Angkatan";
                LbNpm.Text = "NPM";
                LbIdProdi.Text = "";

                //clear Gridview
                DataTable TableKP = new DataTable();
                TableKP.Rows.Clear();
                TableKP.Clear();
                this.GVAmbilKRS.DataSource = TableKP;
                this.GVAmbilKRS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }

            // cek checkbox pilih matakuliah
            Boolean val = false;

            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                if (ch.Checked == true)
                {
                    val = true;
                    break;
                }
            }

            // all checkbox did not check
            if (val == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }

            if (this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013" || this.LbTahun.Text == "2013/2014" || this.LbTahun.Text == "2014/2015")
            {
                // insert KRS mahasiswa
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // ----============ Ambil Skripsi ================-----
                        int TotalChecked = 0;
                        for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            if (ch.Checked == true)
                            {
                                TotalChecked += 1;
                            }
                        }

                        if (TotalChecked == 1)
                        {
                            try
                            {
                                for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                                {
                                    CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                    if (ch.Checked == true)
                                    {
                                        // ---- =========== INSERT TAGIHAN SKRIPSI =============== ----
                                        SqlCommand CmdSkripsi = new SqlCommand("SpAmbilSkripsi", con);
                                        CmdSkripsi.Transaction = trans;
                                        CmdSkripsi.CommandType = System.Data.CommandType.StoredProcedure;

                                        CmdSkripsi.Parameters.AddWithValue("@kd_makul", GVAmbilKRS.Rows[i].Cells[3].Text);
                                        CmdSkripsi.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                                        CmdSkripsi.Parameters.AddWithValue("@npm", this.LbNpm.Text);

                                        SqlParameter Biaya = new SqlParameter();
                                        Biaya.ParameterName = "@TotalBiaya";
                                        Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                                        Biaya.Size = 20;
                                        Biaya.Direction = System.Data.ParameterDirection.Output;
                                        CmdSkripsi.Parameters.Add(Biaya);

                                        CmdSkripsi.ExecuteNonQuery();

                                        decimal TotalBiaya;
                                        TotalBiaya = Convert.ToDecimal(Biaya.Value.ToString());

                                        string FormattedString9 = string.Format
                                            (new System.Globalization.CultureInfo("id"), "{0:c}", TotalBiaya);
                                        this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                                        this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                                        // ---- =========== INSERT KRS SKRIPSI =============== ----
                                        // ---- Ambil KRS BIASA 1 Mata Kuliah / Jadwal Tidak Disarankan
                                        //----- ===================================== -----
                                        SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                        cmdInKRS.Transaction = trans;
                                        cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                        cmdInKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                                        cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);
                                        cmdInKRS.ExecuteNonQuery();

                                        trans.Commit();
                                        trans.Dispose();

                                        con.Close();
                                        con.Dispose();

                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Skripsi Berhasil ...');", true);

                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                con.Close();
                                con.Dispose();
                                Response.Write(ex.ToString());
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                                return;
                            }
                            return;
                        }
                        else
                        {
                            // ---- =========== KRS BIASA, LEBIH DARI 1 MATA KULIAH =============== ----

                            // ---- loop insert no jadwal ---
                            for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                            {
                                CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                                if (ch.Checked == true)
                                {
                                    SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                    cmdInKRS.Transaction = trans;

                                    cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmdInKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                                    cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                    cmdInKRS.ExecuteNonQuery();
                                }
                            }
                        }

                        //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2013 
                        if (this.LbTahun.Text == "2013/2014")
                        {
                            // Procedure Posting Tagihan ke Bank :
                            // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbTahun.Text);
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.LbJumlahSKS.Text) * 35000);
                            cmd.Parameters.AddWithValue("@sks", this.LbJumlahSKS.Text);
                            cmd.Parameters.AddWithValue("@dispen", "no");
                            cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            // biaya angsuran + separuh harga SKS
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 35000) / 2));
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            decimal total = biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 35000) / 2);

                            string FormattedString9 = string.Format
                                  (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;

                        }
                        //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2014
                        else if (this.LbTahun.Text == "2014/2015")
                        {
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", "2014/2015");
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            cmd.Transaction = trans;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@biaya", Convert.ToDecimal(this.LbJumlahSKS.Text) * 40000);
                            cmd.Parameters.AddWithValue("@sks", this.LbJumlahSKS.Text);
                            cmd.Parameters.AddWithValue("@dispen", "no");
                            cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());


                            //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            // biaya angsuran + separuh biaya SKS dalm 1 semester
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 40000) / 2));
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            decimal total = biaya + (((Convert.ToDecimal(this.LbJumlahSKS.Text)) * 40000) / 2);

                            string FormattedString9 = string.Format
                                  (new System.Globalization.CultureInfo("id"), "{0:c}", total);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;
                        }

                        // Angsuran 1 Angkatan mulai 2012/2013 ke bawah .... ==> Biaya Saja
                        else if (this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013")
                        {
                            // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                            SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                            CmdPeriodik.Transaction = trans;
                            CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                            CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", LbTahun.Text);
                            CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            CmdPeriodik.Parameters.AddWithValue("@semester_biaya", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            CmdPeriodik.ExecuteNonQuery();

                            //2.) Insert SKS into DB by using SpInsertSksMhs ----
                            // --- insert SKS tidak diperlukan 
                            // --------------------------------------------------
                            //SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                            //cmd.Transaction = trans;
                            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            //cmd.Parameters.AddWithValue("@npm", this.TBNPM2.Text);
                            //cmd.Parameters.AddWithValue("@semester", _semester);
                            //cmd.Parameters.AddWithValue("@biaya", 0);
                            //cmd.Parameters.AddWithValue("@sks", 0);
                            //cmd.Parameters.AddWithValue("@dispen", "no");
                            //cmd.ExecuteNonQuery();

                            // 3.) Get Biaya Angsuran
                            SqlCommand cmdAngsuran = new SqlCommand("SpGetBiayaAngsuran", con);
                            cmdAngsuran.Transaction = trans;
                            cmdAngsuran.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdAngsuran.Parameters.AddWithValue("@angkatan", this.LbTahun.Text);
                            cmdAngsuran.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                            cmdAngsuran.Parameters.AddWithValue("@kelas", this.LbKelas.Text);
                            cmdAngsuran.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                            SqlParameter Biaya = new SqlParameter();
                            Biaya.ParameterName = "@biaya";
                            Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                            Biaya.Size = 20;
                            Biaya.Direction = System.Data.ParameterDirection.Output;
                            cmdAngsuran.Parameters.Add(Biaya);

                            cmdAngsuran.ExecuteNonQuery();

                            decimal biaya;
                            biaya = Convert.ToDecimal(Biaya.Value.ToString());

                            //4.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                            SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                            CmdPost.Transaction = trans;
                            CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                            CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                            CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                            CmdPost.Parameters.AddWithValue("@angsuran", "1");
                            CmdPost.ExecuteNonQuery();

                            string FormattedString9 = string.Format
                                (new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                            this.LbPostSuccess.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                            this.LbPostSuccess.ForeColor = System.Drawing.Color.Green;
                        }

                        trans.Commit();
                        trans.Dispose();

                        con.Close();
                        con.Dispose();

                        // hide panel
                        // this.PanelKRS.Enabled = false;
                        // this.PanelKRS.Visible = false;

                        this.LbJumlahSKS.Text = "";
                        this.DLSemester.SelectedIndex = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                    }
                    catch (Exception ex)
                    {
                        this.LbJumlahSKS.Text = " ";
                        this.DLSemester.SelectedIndex = 0;

                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                }
            }
            else
            // BIAYA UKT (UTM NEGERI)
            {
                // insert KRS mahasiswa
                string CS2 = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS2))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // ---- loop insert no jadwal ---
                        for (int i = 0; i < GVAmbilKRS.Rows.Count; i++)
                        {
                            CheckBox ch = (CheckBox)GVAmbilKRS.Rows[i].FindControl("CBMakul");
                            if (ch.Checked == true)
                            {
                                SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                cmdInKRS.Transaction = trans;

                                cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                cmdInKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                                cmdInKRS.Parameters.AddWithValue("@no_jadwal", GVAmbilKRS.Rows[i].Cells[2].Text);

                                cmdInKRS.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        trans.Dispose();

                        con.Close();
                        con.Dispose();

                        this.DLSemester.SelectedIndex = 0;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil KRS ...');", true);
                    }
                    catch (Exception ex)
                    {
                        this.LbJumlahSKS.Text = " ";
                        this.DLSemester.SelectedIndex = 0;

                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                }


                // --------------------- Untuk awal semester scripts ini tidak dieksekusi --------------------
                // karena semua procedure sudah dilakukan sampai dengan mahasiswa registrasi
                // ------------------------------------------------------------------------------------------

                //string CS = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(CS))
                //{
                //    con.Open();
                //    SqlTransaction trans = con.BeginTransaction();
                //    try
                //    {
                //        // 1.) Insert Tagihan Periodik Mhs (UKT)
                //        SqlCommand CmdPeriodik = new SqlCommand("SpTagihanPeriodikUkt", con);
                //        CmdPeriodik.Transaction = trans;
                //        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                //        CmdPeriodik.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                //        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                //        CmdPeriodik.ExecuteNonQuery();

                //        // 2.) Insert SKS into DB by using SpInsertSksMhs tidak diperlukan ----

                //        // 3.) Get Biaya Semester (UKT)
                //        SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", con);
                //        cmdBiayaUKT.Transaction = trans;
                //        cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                //        cmdBiayaUKT.Parameters.AddWithValue("@npm", this.LbNpm.Text);

                //        SqlParameter Biaya = new SqlParameter();
                //        Biaya.ParameterName = "@biaya";
                //        Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                //        Biaya.Size = 20;
                //        Biaya.Direction = System.Data.ParameterDirection.Output;
                //        cmdBiayaUKT.Parameters.Add(Biaya);

                //        cmdBiayaUKT.ExecuteNonQuery();

                //        decimal biaya;
                //        biaya = Convert.ToDecimal(Biaya.Value.ToString());

                //        // 4.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                //        // --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                //        // -----------------------------------------------------------------

                //        SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", con);
                //        CmdPost.Transaction = trans;
                //        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                //        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNpm.Text);
                //        CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                //        CmdPost.Parameters.AddWithValue("@total_tagihan", biaya);
                //        CmdPost.ExecuteNonQuery();

                //        trans.Commit();
                //        trans.Dispose();
                //    }
                //    catch (Exception ex)
                //    {
                //        trans.Rollback();
                //        con.Close();
                //        con.Dispose();
                //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //        return;
                //    }
                //}

                // --------------------------- End Script Awal --------------------------------------------

            }
        }

        int PrevTotalEditSKS;
        int JumlahEditSKS;
        protected void CBEdit_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            CheckBox ch = (CheckBox)GVEditKRS.Rows[index].FindControl("CBEdit");
            if (ch.Checked == true)
            {
                PrevTotalEditSKS = Convert.ToInt32(this.LbJumlahEditSKS.Text);
                _TotalEditSKS = PrevTotalEditSKS;
                this.GVEditKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFFFB7");

                this.LbJumlahEditSKS.Text = _TotalEditSKS.ToString();


                //----- background makul checked ----- //
                _TotalEditSKS = 0;
                for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                    if (ch2.Checked == true)
                    {
                        this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                        _TotalEditSKS += Convert.ToInt16(this.GVEditKRS.Rows[i].Cells[5].Text);
                        this.LbJumlahEditSKS.Text = Convert.ToString(_TotalEditSKS);
                    }
                }

                // batas maximal KRS
                if (_TotalEditSKS > 24)
                {
                    ch.Checked = false;
                    this.LbJumlahEditSKS.Text = PrevTotalEditSKS.ToString();
                    this.GVEditKRS.Rows[index].BackColor = System.Drawing.Color.FromName("#FFCEC1");
                    _TotalEditSKS = PrevTotalEditSKS;
                }
            }
            else
            {
                //----- hitung ulang sks berdasarkan makul checked ----- //
                for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                    if (ch2.Checked == true)
                    {
                        JumlahEditSKS += Convert.ToInt32(this.GVEditKRS.Rows[i].Cells[5].Text);
                        _TotalEditSKS = JumlahEditSKS;
                        this.LbJumlahEditSKS.Text = JumlahEditSKS.ToString();
                    }
                }

                //----- background makul checked ----- //
                for (int i = 0; i < GVEditKRS.Rows.Count; i++)
                {
                    CheckBox ch2 = (CheckBox)GVEditKRS.Rows[i].FindControl("CBEdit");
                    if (ch2.Checked == true)
                    {
                        this.GVEditKRS.Rows[i].BackColor = System.Drawing.Color.FromName("#FFFFB7");
                    }
                }
            }
        }

        protected void GVEditKRS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false; //id_jadwal
            e.Row.Cells[6].Visible = false; //Quota
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //  ------- Read Data Mahasiswa ----------
            //  -- hanya mahasiswa angkatan tahun 14/15 yang diperbolehkan --- 
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbTahun.Text = mhs.thn_angkatan.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();

                if (LbTahun.Text != "2014/2015")
                {
                    LbNama.Text = "Nama";
                    LbKelas.Text = "Kelas";
                    LbProdi.Text = "Program Studi";
                    LbTahun.Text = "Tahun Angkatan";
                    LbNpm.Text = "NPM";
                    LbIdProdi.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mahasiswa Tidak Ditemukan');", true);
                    return;
                }
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbTahun.Text = "Tahun Angkatan";
                LbNpm.Text = "NPM";
                LbIdProdi.Text = "";

                //clear Gridview
                DataTable TableKP = new DataTable();
                TableKP.Rows.Clear();
                TableKP.Clear();
                this.GVAmbilKRS.DataSource = TableKP;
                this.GVAmbilKRS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }


            // cek checkbox pilih matakuliah
            Boolean val = false;

            for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                if (ch.Checked == true)
                {
                    val = true;
                    //exit loop completely
                    break;
                }
            }

            // all checkbox did not check
            if (val == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Mata Kuliah');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --- ============================================================================
                    //  ------ Get status edit KRS ----------------
                    SqlCommand CmdCntKRS = new SqlCommand("SpCntEditKRS", con);
                    CmdCntKRS.Transaction = trans;
                    CmdCntKRS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCntKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                    CmdCntKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                    CmdCntKRS.ExecuteNonQuery();


                    // ------ jika status edit blum ada do batal tambah operation -------
                    // -- Batal Tambah / Edit hanya boleh dilakukan SATU KALI SAJA
                    SqlCommand CmdBatal = new SqlCommand("SpInBatalKRS", con);
                    CmdBatal.Transaction = trans;
                    CmdBatal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBatal.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                    CmdBatal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    CmdBatal.ExecuteNonQuery();
                    // --- ==============================================================================


                    // ---- loop insert no jadwal ---
                    for (int i = 0; i < this.GVEditKRS.Rows.Count; i++)
                    {
                        CheckBox ch = (CheckBox)this.GVEditKRS.Rows[i].FindControl("CBEdit");
                        Label LbSisa = (Label)this.GVEditKRS.Rows[i].FindControl("LbSisa");

                        if (ch.Checked == true)
                        {
                            //Response.Write(GV.Rows[i].Cells[1].Text);
                            //Response.Write("DataKeyName:" + GV.DataKeys[i].Value.ToString());

                            if (LbSisa.Text == "Penuh")
                            {
                                //ignore
                                continue;
                            }
                            else
                            {
                                SqlCommand cmdInKRS = new SqlCommand("SpInKRS", con);
                                cmdInKRS.Transaction = trans;

                                cmdInKRS.CommandType = System.Data.CommandType.StoredProcedure;

                                cmdInKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                                cmdInKRS.Parameters.AddWithValue("@no_jadwal", this.GVEditKRS.Rows[i].Cells[2].Text);

                                cmdInKRS.ExecuteNonQuery();
                            }
                        }
                        else
                        {

                            SqlCommand cmdDelKRS = new SqlCommand("SpDelKRS", con);
                            cmdDelKRS.Transaction = trans;

                            cmdDelKRS.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdDelKRS.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                            cmdDelKRS.Parameters.AddWithValue("@no_jadwal", GVEditKRS.Rows[i].Cells[2].Text);

                            cmdDelKRS.ExecuteNonQuery();

                        }
                    }

                    //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2013 
                    if (this.LbTahun.Text == "2013/2014")
                    {
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", this.LbJumlahEditSKS.Text);
                        decimal BIAYA;
                        BIAYA = Convert.ToDecimal(this.LbJumlahEditSKS.Text.ToString());
                        cmd.Parameters.AddWithValue("@biaya", BIAYA * 35000);
                        cmd.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                        cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);

                        cmd.ExecuteNonQuery();

                    }
                    //Insert jumlah SKS khusus untuk Mhs Yayasan Tahun 2014
                    else if (this.LbTahun.Text == "2014/2015")
                    {
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", this.LbJumlahEditSKS.Text);
                        decimal BIAYA;
                        BIAYA = Convert.ToDecimal(this.LbJumlahEditSKS.Text.ToString());
                        cmd.Parameters.AddWithValue("@biaya", BIAYA * 40000);
                        cmd.Parameters.AddWithValue("@npm", this.LbNpm.Text);
                        cmd.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);

                        cmd.ExecuteNonQuery();

                    }

                    // Angsuran 1 Angkatan mulai 2012/2013 ke bawah & UKT
                    else if (this.LbTahun.Text == "2005/2006" || this.LbTahun.Text == "2006/2007" || this.LbTahun.Text == "2007/2008" || this.LbTahun.Text == "2008/2009" || this.LbTahun.Text == "2009/2010" || this.LbTahun.Text == "2010/2011" || this.LbTahun.Text == "2011/2012" || this.LbTahun.Text == "2012/2013")
                    {
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Edit Berhasil KRS ...');", true);
                    }

                    trans.Commit();
                    trans.Dispose();

                    con.Close();
                    con.Dispose();

                    // hide panel
                    this.PanelEditKRS.Enabled = false;
                    this.PanelEditKRS.Visible = false;

                    //semester
                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Edit KRS Berhasil ...');", true);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    con.Close();
                    con.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void GVListKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}