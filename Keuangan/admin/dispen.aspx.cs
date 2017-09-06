using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Keuangan.admin
{
    //public partial class WebForm4 : System.Web.UI.Page
    public partial class WebForm4 :Keu_Admin_Class
    {
        mahasiswa mhs = new mahasiswa();

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

        private void ReadMahasiswa()
        {
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();

               // BtnInsertSKS.Visible = true;
                BtnPosting.Visible = true;
            }
            catch (Exception ex)
            {
                LbNama.Text = "Nama";
                LbClass.Text = "Kelas";
                LbProdi.Text ="Program Studi";
                LbThnAngkatan.Text = "Tahun Angkatan";
                LbNPM.Text = "NPM";

                this.PanelSKS_Angsuran.Enabled = false;
                this.PanelSKS_Angsuran.Visible = false;
                this.PanelCicilan.Enabled = false;
                this.PanelCicilan.Visible = false;
                this.PanelBebanAwal.Enabled = false;
                this.PanelBebanAwal.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
                //LbFilterMhs.Text = ex.Message.ToString();
                //LbFilterMhs.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
                //BtnInsertSKS.Visible = false;
                BtnPosting.Visible = false;

                PanelSKS_Angsuran.Visible = false;

                LbNama.Text = "Nama";
                LbClass.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbThnAngkatan.Text = "Tahun Angkatan";
                LbNPM.Text = "NPM";

                this.PanelCicilan.Enabled = false;
                this.PanelCicilan.Visible = false;
                this.PanelSKS_Angsuran.Enabled = false;
                this.PanelSKS_Angsuran.Visible = false;
                this.PanelBebanAwal.Enabled = false;
                this.PanelBebanAwal.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //Clear ALL Grid View Content
            DataTable Table = new DataTable();
            Table.Rows.Clear();
            Table.Clear();
            GVAngsuran.DataSource = Table;
            GVAngsuran.DataBind();
            GVBebanAwal.DataSource = Table;
            GVBebanAwal.DataBind();

            //Clear All Label Result
            this.LbPostResult.Text = "";
            this.LbPostResult2.Text = "";
            this.LbPostResult3.Text = "";
            this.LbFilterMhs.Text = "";
            this.LbHargaSKS.Text = "";

            //filter Textbox NPM
            if (this.TBNpm.Text == "")
            {
                this.LbFilterMhs.Text = "Input NPM";
                LbFilterMhs.ForeColor = System.Drawing.Color.Red;
                this.PanelSKS_Angsuran.Visible = false;
                return;
            } 
            
            //Radio Button SKS & Angsuran 1
            if (this.RBSksAngsuran1.Checked == true)
            {
                //read mahasiswa
                try
                {
                    this.PanelSKS_Angsuran.Enabled = true;
                    this.PanelSKS_Angsuran.Visible = true;
                    this.PanelCicilan.Enabled = false;
                    this.PanelCicilan.Visible = false;
                    this.PanelBebanAwal.Enabled = false;
                    this.PanelBebanAwal.Visible = false;

                    ReadMahasiswa();

                    this.LbFilterMhs.Text = "";

                    if (this.LbThnAngkatan.Text == "2013/2014" || this.LbThnAngkatan.Text == "2014/2015")
                    {
                        this.TBSKS.Enabled = true;
                    }
                    else
                    {
                        this.TBSKS.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.PanelSKS_Angsuran.Enabled = false;
                    this.PanelSKS_Angsuran.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error saat membaca data mahasiswa');", true);
                    return;
                }

            //Radio Button Angsuran 1 atau 2
            }else if (this.RBAngsuran1.Checked)
            {
                //read mahasiswa
                try
                {
                    this.PanelCicilan.Enabled = true;
                    this.PanelCicilan.Visible = true;
                    this.PanelSKS_Angsuran.Enabled = false;
                    this.PanelSKS_Angsuran.Visible = false;
                    this.PanelBebanAwal.Enabled = false;
                    this.PanelBebanAwal.Visible = false;

                    ReadMahasiswa();

                    this.LbFilterMhs.Text = "";
                }
                catch (Exception)
                {
                    this.PanelCicilan.Enabled = false;
                    this.PanelCicilan.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error saat membaca data mahasiswa');", true);
                    return;
                }
            }  //Radio Button Beban Awal 
            else if (this.RBBebanAwl.Checked)
            {
                //read mahasiswa
                try
                {
                    this.PanelSKS_Angsuran.Enabled = false;
                    this.PanelSKS_Angsuran.Visible = false;
                    this.PanelCicilan.Enabled = false;
                    this.PanelCicilan.Visible = false;
                    this.PanelBebanAwal.Enabled = true;
                    this.PanelBebanAwal.Visible = true;

                    ReadMahasiswa();

                    this.LbFilterMhs.Text = "";
                }
                catch (Exception)
                {
                    this.PanelBebanAwal.Enabled = false;
                    this.PanelBebanAwal.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error saat membaca data mahasiswa');", true);
                    return;
                }
            }
            else
            {
                this.LbFilterMhs.Text = "Pilih Salah Satu Jenis Angsuran";
                LbFilterMhs.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        //posting tagihan 1 dan SKS Online
        protected void BtnPosting_Click(object sender, EventArgs e)
        {
            //Target ==> -- Insert SKS & posting to bank ---
            if (this.DLAngsuran.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Angsuran');", true);
                return;
            }

            if (DLCicilan.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Cicilan');", true);
                return;
            }
            if (TBDispen.Text == "")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                return;
            }
            if (DLSemester.SelectedItem.Text == "" || DLSemester.SelectedValue == "-1" || DLSemester.SelectedItem.Text == "semester")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }

            if (LbThnAngkatan.Text == "2014/2015")
            {
                // cek sks > 24 SKS
                if (Convert.ToInt32(TBSKS.Text) > 24)
                {
                    //client message check list lebih dari 1 item atau belum pilih check list.....
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('SKS Tidak Boleh Lebih Dari 24');", true);
                    return;
                }

                if (TBSKS.Text == "" || TBSKS.Text == "0")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Isi Jumlah SKS');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {   // Procedure Posting Tagihan ke Bank :
                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@biaya", Convert.ToInt32(this.TBSKS.Text) * 40000);
                        cmd.Parameters.AddWithValue("@sks", this.TBSKS.Text);
                        cmd.Parameters.AddWithValue("@dispen", "yes");
                        cmd.ExecuteNonQuery();


                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", (Convert.ToInt32(this.TBDispen.Text)));
                        CmdPost.Parameters.AddWithValue("@angsuran", "1");
                        CmdPost.Parameters.AddWithValue("@cicilan", "1");
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        cmd.Dispose();
                        CmdPeriodik.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        TBSKS.Text = "";
                        TBDispen.Text = "";
                        this.LbPostResult.Text = "";

                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else if (LbThnAngkatan.Text == "2013/2014")
            {
                // cek sks > 24 SKS
                if (Convert.ToInt32(TBSKS.Text) > 24)
                {
                    //client message check list lebih dari 1 item atau belum pilih check list.....
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('SKS Tidak Boleh Lebih Dari 24');", true);
                    return;
                }

                if (TBSKS.Text == "" || TBSKS.Text == "0")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Isi Jumlah SKS');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {   // Procedure Posting Tagihan ke Bank :
                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya",DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@biaya", Convert.ToInt32(this.TBSKS.Text) * 35000);
                        cmd.Parameters.AddWithValue("@sks", this.TBSKS.Text);
                        cmd.Parameters.AddWithValue("@dispen", "yes");
                        cmd.ExecuteNonQuery();


                        //3.) POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester",  DLSemester.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", (Convert.ToInt32(this.TBDispen.Text)));
                        CmdPost.Parameters.AddWithValue("@angsuran", "1");
                        CmdPost.Parameters.AddWithValue("@cicilan", "1");
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();

                        cmd.Dispose();
                        CmdPeriodik.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        TBSKS.Text = "";
                        TBDispen.Text = "";
                        LbPostResult.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else // Mahasiswa aktif sampai dengan Angkatan 2012/2013 / (<= 2012/2013)   ==> Bayar Angsuran saja, tidak ada SKS
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // ------------ Mahasiswa sudah bayar di BPD & Kwitansi diserahkan ke Bag Keuangan -----
                        // Cek SKS Mahasiswa Tahun Angkatan --> Ini tidak Ada
                        //Insert Biaya SKS Mahasiswa Tahun Angkatan --> Ini tidak Ada

                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();
                        
                        //----- POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester.SelectedValue.ToString());
                        CmdPost.Parameters.AddWithValue("@total_tagihan", Convert.ToInt32(this.TBDispen.Text));
                        CmdPost.Parameters.AddWithValue("@angsuran", DLAngsuran.SelectedItem.ToString());
                        CmdPost.Parameters.AddWithValue("@cicilan", "1");
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();

                        CmdPeriodik.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        TBDispen.Text = "";
                        TBSKS.Text = "";

                        TBSKS.Text = "";
                        TBDispen.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                        // this.LbPostResult.Text = ex.Message.ToString();
                        // LbPostResult.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        //posting tagihan 1 dan SKS (Offline)
        protected void BtnPostOffline_Click(object sender, EventArgs e)
        {
            //Target ==> -- Insert SKS & posting to bank ---
            if (this.DLAngsuran.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Angsuran');", true);
                return;
            }

            if (DLCicilan.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Cicilan');", true);
                return;
            }
            if (TBDispen.Text == "")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                return;
            }
            if (DLSemester.SelectedItem.Text == "" || DLSemester.SelectedValue == "-1" || DLSemester.SelectedItem.Text == "semester")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }

            if (LbThnAngkatan.Text == "2014/2015")
            {
                if (TBSKS.Text == "" || TBSKS.Text == "0")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Isi Jumlah SKS');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {   // Procedure Posting Tagihan OFFLINE :
                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@biaya", Convert.ToInt32(this.TBSKS.Text) * 40000);
                        cmd.Parameters.AddWithValue("@sks", this.TBSKS.Text);
                        cmd.Parameters.AddWithValue("@dispen", "yes");
                        cmd.ExecuteNonQuery();


                        //3.) POSTING tahihan OFFLINE by using SpInsertPostingPerMhsOffline -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhsOffline", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", (Convert.ToInt32(this.TBDispen.Text)));
                        CmdPost.Parameters.AddWithValue("@angsuran", this.DLAngsuran.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@cicilan", this.DLCicilan.SelectedItem.Text);
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        cmd.Dispose();
                        CmdPeriodik.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        TBSKS.Text = "";
                        TBDispen.Text = "";

                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else if (LbThnAngkatan.Text == "2013/2014")
            {
                if (TBSKS.Text == "" || TBSKS.Text == "0")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Isi Jumlah SKS');", true);
                    return;
                }

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {   // Procedure Posting Tagihan OFFLINE:
                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        //2.) Insert SKS into DB by using SpInsertSksMhs ----
                        SqlCommand cmd = new SqlCommand("SpInsertSksMhs1", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@biaya", Convert.ToInt32(this.TBSKS.Text) * 35000);
                        cmd.Parameters.AddWithValue("@sks", this.TBSKS.Text);
                        cmd.Parameters.AddWithValue("@dispen", "yes");
                        cmd.ExecuteNonQuery();


                        //3.) POSTING tahihan OFFLINE by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhsOffline", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@total_tagihan", (Convert.ToInt32(this.TBDispen.Text)));
                        CmdPost.Parameters.AddWithValue("@angsuran", this.DLAngsuran.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@cicilan",this.DLCicilan.SelectedItem.Text);
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        cmd.Dispose();
                        CmdPeriodik.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        TBSKS.Text = "";
                        TBDispen.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else // Mahasiswa aktif sampai dengan Angkatan 2012/2013 / (<= 2012/2013), bayar Angsuran Saja, bayar SKS Tidak
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    try
                    {
                        // Cek SKS Mahasiswa

                        //Insert Biaya SKS Mahasiswa Tahun Angkatan Ini tidak Ada

                        // 1.) Insert Tagihan Periodik Mhs (mjd msh aktif) by using SpInsertTagihanPeriodikMhs ---
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        //----- POSTING tahihan OFFLINE using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhsOffline", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester.SelectedValue.ToString());
                        CmdPost.Parameters.AddWithValue("@total_tagihan", Convert.ToInt32(this.TBDispen.Text));
                        CmdPost.Parameters.AddWithValue("@angsuran",this.DLAngsuran.SelectedItem.Text);
                        CmdPost.Parameters.AddWithValue("@cicilan",this.DLCicilan.SelectedItem.Text);
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        int Tagihan = Convert.ToInt32(this.TBDispen.Text);
                        string FormattedString9 = string.Format
                              (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                        this.LbPostResult.Text = "Posting tagihan Berhasil " + FormattedString9;
                        LbPostResult.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();

                        TBDispen.Text = "";
                        TBSKS.Text = "";

                        TBSKS.Text = "";
                        TBDispen.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                        // this.LbPostResult.Text = ex.Message.ToString();
                        // LbPostResult.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        // Posting Angsuran 1 atau 2 dengan Cicilan >= 2 (Online)
        // Cicilan Ke dua hanya  
        protected void Posting2_Click(object sender, EventArgs e)
        {
            this.LbPostResult2.Text = "";

            // --validation --
            if (DLSemester2.SelectedItem.Text == "" || DLSemester2.SelectedValue == "-1" || DLSemester2.SelectedItem.Text == "semester")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }
            if (this.DLAngsuran2.SelectedValue == "-1" || this.DLAngsuran2.SelectedItem.Text == "Angsuran")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Angsuran');", true);
                return;
            }

            if (DLCicilan2.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Cicilan');", true);
                return;
            }
            if (TbDispen2.Text == "" || TbDispen2.Text == "0")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                return;
            }

            // Insert to DB
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    ReadMahasiswa();
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }

                //open connection and begin transaction
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // Insert Tagihan Periodik Mhs (msh mjd aktif) by using SpInsertTagihanPeriodikMhs ---
                    // ----- tagihan periodik update jika sudah ada & insert jika belum ------
                    SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                    CmdPeriodik.Transaction = trans;
                    CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                    CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                    CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                    CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester2.SelectedItem.Text);
                    CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    CmdPeriodik.ExecuteNonQuery();

                    // Untuk Tahun angkatan 2013 atau 2014, Cek SKSnya / sudah ada tagihannya belum
                    if (LbThnAngkatan.Text == "2013/2014" || LbThnAngkatan.Text == "2014/2015")
                    {
                        // Cek SKS Mahasiswa
                        SqlCommand CmdSKS = new SqlCommand("SpVwSksSemester", con);
                        CmdSKS.Transaction = trans;
                        CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdSKS.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdSKS.Parameters.AddWithValue("@semester", this.DLSemester2.SelectedValue.ToString());
                        CmdSKS.ExecuteNonQuery();

                        CmdSKS.Dispose();
                    }

                    //----- POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                    SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhs2", con);
                    CmdPost.Transaction = trans;
                    CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                    CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                    CmdPost.Parameters.AddWithValue("@semester", DLSemester2.SelectedValue.ToString());
                    CmdPost.Parameters.AddWithValue("@total_tagihan", Convert.ToInt32(this.TbDispen2.Text));
                    CmdPost.Parameters.AddWithValue("@angsuran", DLAngsuran2.SelectedItem.ToString());
                    CmdPost.Parameters.AddWithValue("@cicilan", DLCicilan2.SelectedItem.ToString());
                    CmdPost.ExecuteNonQuery();

                    trans.Commit();
                    trans.Dispose();
                    CmdPost.Dispose();
                    con.Close();
                    con.Dispose();

                    int Tagihan = Convert.ToInt32(this.TbDispen2.Text);
                    string FormattedString9 = string.Format
                          (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                    this.LbPostResult2.Text = "Posting tagihan Berhasil " + FormattedString9;
                    LbPostResult2.ForeColor = System.Drawing.Color.Green;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    con.Close();
                    con.Dispose();
                    this.LbPostResult2.Text = ex.Message.ToString();
                    LbPostResult2.ForeColor = System.Drawing.Color.Red;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "__FormHasBeenModified", "alert('Error accessing file" + ex.Message + ".');", true);
                }
            }
        }

        // Posting Cicilan Angsuran 1 atau 2 dengan Cicilan >= 2 Offline (dianggap sudah membayar, ada permaslahan sistem bank dengan puskom shg tidak connect)
        protected void BtnCicilanOffline_Click(object sender, EventArgs e)
        {
            {
                if (DLSemester2.SelectedItem.Text == "" || DLSemester2.SelectedValue == "-1" || DLSemester2.SelectedItem.Text == "semester")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                    return;
                }
                if (this.DLAngsuran2.SelectedValue == "-1" || this.DLAngsuran2.SelectedItem.Text == "Angsuran")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Angsuran');", true);
                    return;
                }

                if (DLCicilan2.SelectedValue == "-1")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Cicilan');", true);
                    return;
                }
                if (TbDispen2.Text == "" || TbDispen2.Text == "0")
                {
                    ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                    return;
                }

                // Insert to DB
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    try
                    {
                        ReadMahasiswa();
                    }
                    catch (Exception ex)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }

                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        // Insert Tagihan Periodik Mhs (msh mjd aktif) by using SpInsertTagihanPeriodikMhs ---
                        // ----- tagihan periodik update jika sudah ada & insert jika belum ------
                        SqlCommand CmdPeriodik = new SqlCommand("SpInsertTagihanPeriodikTiapMhs2", con);
                        CmdPeriodik.Transaction = trans;
                        CmdPeriodik.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdPeriodik.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                        CmdPeriodik.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                        CmdPeriodik.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                        CmdPeriodik.Parameters.AddWithValue("@semester_biaya", DLSemester2.SelectedItem.Text);
                        CmdPeriodik.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdPeriodik.ExecuteNonQuery();

                        // Untuk Tahun angkatan 2013 atau 2014, Cek SKSnya / sudah ada tagihannya belum
                        if (LbThnAngkatan.Text == "2013/2014" || LbThnAngkatan.Text == "2014/2015")
                        {
                            // Cek SKS Mahasiswa
                            SqlCommand CmdSKS = new SqlCommand("SpVwSksSemester", con);
                            CmdSKS.Transaction = trans;
                            CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;
                            CmdSKS.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                            CmdSKS.Parameters.AddWithValue("@semester", this.DLSemester2.SelectedValue.ToString());
                            CmdSKS.ExecuteNonQuery();

                            CmdSKS.Dispose();
                        }

                        //----- POSTING tahihan to BANK by using SpInsertPostingPerMhs -----
                        SqlCommand CmdPost = new SqlCommand("SpInsertPostingPerMhsOffline", con);
                        CmdPost.Transaction = trans;
                        CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.LbNPM.Text);
                        CmdPost.Parameters.AddWithValue("@semester", DLSemester2.SelectedValue.ToString());
                        CmdPost.Parameters.AddWithValue("@total_tagihan", Convert.ToInt32(this.TbDispen2.Text));
                        CmdPost.Parameters.AddWithValue("@angsuran", DLAngsuran2.SelectedItem.ToString());
                        CmdPost.Parameters.AddWithValue("@cicilan", DLCicilan2.SelectedItem.ToString());
                        CmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        CmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        this.LbPostResult2.Text = "Posting tagihan Berhasil";
                        LbPostResult2.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "__FormHasBeenModified", "alert('Error accessing file" + ex.Message + ".');", true);
                    }
                }
            }
        }

        //Posting beban Awal Online / via Bank
        protected void BtnPostBbnAwal_Click(object sender, EventArgs e)
        {
            if (DLSemester3.SelectedItem.Text == "" || DLSemester3.SelectedValue == "-1" || DLSemester3.SelectedItem.Text == "semester")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }
            if (TbBbnAwal.Text == "" || TbBbnAwal.Text == "0")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //open connection and begin transaction
                con.Open();
                //SqlTransaction trans = con.BeginTransaction();

                try
                {
                   //Procedure Posting Tagihan ke Bank :
                    SqlCommand CmdPostAwal = new SqlCommand("SpInsertPostingBebanAwal", con);
                   //CmdPostAwal.Transaction = trans;
                    CmdPostAwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdPostAwal.Parameters.AddWithValue("@Npm_Mhs", this.TBNpm.Text);
                    CmdPostAwal.Parameters.AddWithValue("@semester", DLSemester3.SelectedItem.Text);
                    CmdPostAwal.Parameters.AddWithValue("@total_tagihan", TbBbnAwal.Text);
                    CmdPostAwal.ExecuteNonQuery();

                    //commit transaction & close connection
                    //trans.Commit();
                    //trans.Dispose();
                    CmdPostAwal.Dispose();
                    con.Close();
                    con.Dispose();

                    int Tagihan = Convert.ToInt32(TbBbnAwal.Text);
                    string FormattedString9 = string.Format
                          (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                    this.LbPostResult3.Text = "Posting Berhasil, Tagihan : " + FormattedString9;
                    LbPostResult3.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    con.Close();
                    con.Dispose();
                    TbBbnAwal.Text = "";

                    this.LbPostResult3.Text = ex.Message.ToString();
                    this.LbPostResult3.ForeColor = System.Drawing.Color.Red;
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        //Posting beban Awal Offline
        protected void BtnAwalOffline_Click(object sender, EventArgs e)
        {
            if (DLSemester3.SelectedItem.Text == "" || DLSemester3.SelectedValue == "-1" || DLSemester3.SelectedItem.Text == "semester")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }
            if (TbBbnAwal.Text == "" || TbBbnAwal.Text == "0")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Jumlah Pembayaran Harus Diisi !!!');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //open connection and begin transaction
                con.Open();
                //SqlTransaction trans = con.BeginTransaction();

                try
                {
                    // Procedure Posting Tagihan ke Bank :
                    SqlCommand CmdSKS = new SqlCommand("SpInsertPostingBebanAwalOffline", con);
                    //CmdSKS.Transaction = trans;
                    CmdSKS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdSKS.Parameters.AddWithValue("@Npm_Mhs", this.TBNpm.Text);
                    CmdSKS.Parameters.AddWithValue("@semester", DLSemester3.SelectedItem.Text);
                    CmdSKS.Parameters.AddWithValue("@total_tagihan", TbBbnAwal.Text);
                    CmdSKS.ExecuteNonQuery();

                    //commit transaction & close connection
                    //trans.Commit();
                    //trans.Dispose();
                    CmdSKS.Dispose();
                    con.Close();
                    con.Dispose();

                    int Tagihan = Convert.ToInt32(TbBbnAwal.Text);
                    string FormattedString9 = string.Format
                          (new System.Globalization.CultureInfo("id"), "{0:c}", Tagihan);
                    this.LbPostResult3.Text = "Posting Berhasil, Tagihan : " + FormattedString9;
                    LbPostResult3.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    con.Close();
                    con.Dispose();
                    TbBbnAwal.Text = "";

                    this.LbPostResult3.Text = ex.Message.ToString();
                    this.LbPostResult3.ForeColor = System.Drawing.Color.Red;
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnAngsuran_Click(object sender, EventArgs e)
        {
            //Filter Semester Dropdown
            if (this.DLSem.SelectedItem.Text == "Semester" || this.DLSem.SelectedItem.Text == "" || this.DLSem.SelectedValue == "-1")
            {
                ScriptManager.RegisterStartupScript((Control)this.BtnAngsuran, this.GetType(), "redirectMe", "alert('Pilih Semester');", true);
                return;
            }

            if (this.LbThnAngkatan.Text == "2013/2014")
            {
                LbHargaSKS.Text = "Rp35.000";
            }
            else if (this.LbThnAngkatan.Text == "2014/2015")
            {
                LbHargaSKS.Text = "Rp40.000";
            }
            else if (this.LbThnAngkatan.Text == "2013/2014" || this.LbThnAngkatan.Text == "2014/2015")
            {
                LbHargaSKS.Text = "";
            }

            //this.DLSemster2.SelectedItem.Text;
            if (this.LbThnAngkatan.Text == "2013/2014" || this.LbThnAngkatan.Text == "2014/2015")
            {
                this.TBSKS.ReadOnly = false;
            }
            else
            {
                this.TBSKS.ReadOnly = true;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("SpVwAngsuranBiayaStudi", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@thn_angkatan", this.LbThnAngkatan.Text);
                cmd.Parameters.AddWithValue("@prodi", this.LbProdi.Text);
                cmd.Parameters.AddWithValue("@kelas", this.LbClass.Text);
                cmd.Parameters.AddWithValue("@semester",this.DLSem.SelectedItem.Text);

                DataTable Table = new DataTable();
                Table.Columns.Add("Angsuran");
                Table.Columns.Add("Semester");
                Table.Columns.Add("Kelas");
                Table.Columns.Add("SPP");
                Table.Columns.Add("BOP");
                Table.Columns.Add("KMhs");
                Table.Columns.Add("Total Per Angsuran");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["Angsuran"] = rdr["no_angsuran"];
                            datarow["Semester"] = rdr["semester"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["SPP"] = rdr["SPP"];
                            datarow["BOP"] = rdr["BOP"];
                            datarow["KMhs"] = rdr["kemhs"];
                            datarow["Total Per Angsuran"] = rdr["biaya_angsuran"];
                            Table.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVAngsuran.DataSource = Table;
                        this.GVAngsuran.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVAngsuran.DataSource = Table;
                        GVAngsuran.DataBind();

                        ScriptManager.RegisterStartupScript((Control)this.BtnPosting, this.GetType(), "redirectMe", "alert('Tabel angsuran Belum Dibuat ...');", true);
                        return;
                       // LbViewAngsuran2.Text = "Tabel angsuran Belum Dibuat ...";
                       // LbViewAngsuran2.ForeColor = System.Drawing.Color.OrangeRed;
                    }
                }
            }
        }

        protected void BtnBbnAwal_Click(object sender, EventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT no, npm, beban_awal FROM keu_keu_mhs where npm=@npm", con);
                cmd.Parameters.AddWithValue("@npm", TBNpm.Text);

                DataTable Table = new DataTable();
                Table.Columns.Add("NPM");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["NPM"] = rdr["npm"];
                            if (rdr["beban_awal"] == DBNull.Value)
                            {
                                //BtnViewBiaya2.Enabled = false;
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tagihan Awal belum diinput');", true);
                                return;
                            }
                            else
                            {
                                datarow["Biaya"] = rdr["beban_awal"];
                                Table.Rows.Add(datarow);
                            }
                        }

                        //Fill Gridview
                        this.GVBebanAwal.DataSource = Table;
                        this.GVBebanAwal.DataBind();

                        LbPostResult3.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVBebanAwal.DataSource = Table;
                        GVBebanAwal.DataBind();

                    }
                }
            }
        }

        protected void GVAngsuran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SPP = Convert.ToInt32(e.Row.Cells[3].Text);
                string FormattedString3 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SPP);
                e.Row.Cells[3].Text = FormattedString3;
                int BOP = Convert.ToInt32(e.Row.Cells[4].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", BOP);
                e.Row.Cells[4].Text = FormattedString4;
                int KMhs = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString5 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", KMhs);
                e.Row.Cells[5].Text = FormattedString5;
                int Biaya = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString6 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[6].Text = FormattedString6;
            }
        }
    }
}