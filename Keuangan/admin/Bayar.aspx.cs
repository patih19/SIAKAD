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
    //public partial class WebForm9 : System.Web.UI.Page
    public partial class WebForm9 : Keu_Admin_Class
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
            this.Session["Name"] = null;
            this.Session["Passwd"] = null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }
        // ---------------------------------------------------- //


        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
                // hide panel Penuh
                this.PanelPenuh.Enabled = false;
                this.PanelPenuh.Visible = false;

                //hide panel tidak penuh
                this.PanelTdkPenuh.Enabled = false;
                this.PanelTdkPenuh.Visible = false;

                //hide panel biaya
                this.PanelBiaya.Enabled = false;
                this.PanelBiaya.Visible = false;
            }
        }

        protected void GVKP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int KP = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", KP);
                e.Row.Cells[2].Text = FormattedString1;
            }
        }

        protected void GVPPLSD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PPLSD = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSD);
                e.Row.Cells[2].Text = FormattedString1;
            }
        }

        protected void GVPPLSMA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PPLSMA = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", PPLSMA);
                e.Row.Cells[2].Text = FormattedString1;
            }
        }

        protected void GVKKN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int KKN = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", KKN);
                e.Row.Cells[2].Text = FormattedString1;
            }
        }

        protected void GVWISUDA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Wisuda = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Wisuda);
                e.Row.Cells[2].Text = FormattedString1;
            }
        }

        protected void BtnViewMhs_Click(object sender, EventArgs e)
        {
            // radio validation
            if (this.RbPosting.Checked == false && this.RbPostManual.Checked == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Pembayaran');", true);
                return;
            }

            //read Mahasiswa
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();
            }
            catch
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('NPM Tidak Ditemukan');", true);
                return;
            }

            // Bayar Manual
            if (this.RbPostManual.Checked == true)
            {
                this.PanelTdkPenuh.Enabled = true;
                this.PanelTdkPenuh.Visible = true;

                this.PanelPenuh.Enabled = false;
                this.PanelPenuh.Visible = false;
            }
                // Bayar Utuh (sesuai tagihan)
            else if (this.RbPosting.Checked == true)
            {
                this.PanelPenuh.Enabled = true;
                this.PanelPenuh.Visible = true;

                this.PanelTdkPenuh.Enabled = false;
                this.PanelTdkPenuh.Visible = false;
            }
        }

        // Bayar KP
        protected void BtnBayarKP_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --------------- 1.) Insert Tagihan Akhir KP ------------------------
                    SqlCommand cmdKP = new SqlCommand("SpInsertTagihanBiayaAkhir", con);
                    cmdKP.Transaction = trans;
                    cmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdKP.Parameters.AddWithValue("@npm", LbNPM.Text);
                    cmdKP.Parameters.AddWithValue("@jenis_bayar", "KP");
                    cmdKP.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                    cmdKP.ExecuteNonQuery();

                    // --------------- 2.) Posting Tagihan Akhir KP to bank ------------------
                    SqlCommand cmdPostKP = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                    cmdPostKP.Transaction = trans;
                    cmdPostKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPostKP.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                    cmdPostKP.Parameters.AddWithValue("@jenis_biaya", "KP");
                    string biaya = GVKP.Rows[0].Cells[2].Text;
                    string biaya2 = biaya.Replace("Rp", "");
                    string biaya3 = biaya2.Replace(".", "");
                    cmdPostKP.Parameters.AddWithValue("@total_biaya", biaya3);

                    cmdPostKP.ExecuteNonQuery();

                    // --------------- 3.) Commit Transaction ------------------
                    trans.Commit();
                    trans.Dispose();
                    cmdKP.Dispose();
                    cmdPostKP.Dispose();
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        //Bayar PPL I / PPL SD
        protected void BtnBayarPPLI_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --------------- 1.) Insert Tagihan Akhir PPL I ------------------------
                    SqlCommand cmdKP = new SqlCommand("SpInsertTagihanBiayaAkhir", con);
                    cmdKP.Transaction = trans;
                    cmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdKP.Parameters.AddWithValue("@npm", LbNPM.Text);
                    cmdKP.Parameters.AddWithValue("@jenis_bayar", "PPL I");
                    cmdKP.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                    cmdKP.ExecuteNonQuery();

                    // --------------- 2.) Posting Tagihan Akhir PPL I to bank ------------------
                    SqlCommand cmdPostKP = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                    cmdPostKP.Transaction = trans;
                    cmdPostKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPostKP.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                    cmdPostKP.Parameters.AddWithValue("@jenis_biaya", "PPL I");
                    string biaya = this.GVPPLSD.Rows[0].Cells[2].Text;
                    string biaya2 = biaya.Replace("Rp", "");
                    string biaya3 = biaya2.Replace(".", "");
                    cmdPostKP.Parameters.AddWithValue("@total_biaya", biaya3);

                    cmdPostKP.ExecuteNonQuery();

                    // --------------- 3.) Commit Transaction ------------------
                    trans.Commit();
                    trans.Dispose();
                    cmdKP.Dispose();
                    cmdPostKP.Dispose();
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        //Bayar PPL II / PPL SMA
        protected void BtnBayarPPLII_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --------------- 1.) Insert Tagihan Akhir PPL I ------------------------
                    SqlCommand cmdKP = new SqlCommand("SpInsertTagihanBiayaAkhir", con);
                    cmdKP.Transaction = trans;
                    cmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdKP.Parameters.AddWithValue("@npm", LbNPM.Text);
                    cmdKP.Parameters.AddWithValue("@jenis_bayar", "PPL II");
                    cmdKP.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                    cmdKP.ExecuteNonQuery();

                    // --------------- 2.) Posting Tagihan Akhir PPL I to bank ------------------
                    SqlCommand cmdPostKP = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                    cmdPostKP.Transaction = trans;
                    cmdPostKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPostKP.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                    cmdPostKP.Parameters.AddWithValue("@jenis_biaya", "PPL II");
                    string biaya = this.GVPPLSMA.Rows[0].Cells[2].Text;
                    string biaya2 = biaya.Replace("Rp", "");
                    string biaya3 = biaya2.Replace(".", "");
                    cmdPostKP.Parameters.AddWithValue("@total_biaya", biaya3);

                    cmdPostKP.ExecuteNonQuery();

                    // --------------- 3.) Commit Transaction ------------------
                    trans.Commit();
                    trans.Dispose();
                    cmdKP.Dispose();
                    cmdPostKP.Dispose();
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }
        
        // Bayar KKN
        protected void BtnBayarKKN_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --------------- 1.) Insert Tagihan Akhir PPL I ------------------------
                    SqlCommand cmdKP = new SqlCommand("SpInsertTagihanBiayaAkhir", con);
                    cmdKP.Transaction = trans;
                    cmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdKP.Parameters.AddWithValue("@npm", LbNPM.Text);
                    cmdKP.Parameters.AddWithValue("@jenis_bayar", "KKN");
                    cmdKP.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                    cmdKP.ExecuteNonQuery();

                    // --------------- 2.) Posting Tagihan Akhir PPL I to bank ------------------
                    SqlCommand cmdPostKP = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                    cmdPostKP.Transaction = trans;
                    cmdPostKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPostKP.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                    cmdPostKP.Parameters.AddWithValue("@jenis_biaya", "KKN");
                    string biaya = this.GVKKN.Rows[0].Cells[2].Text;
                    string biaya2 = biaya.Replace("Rp", "");
                    string biaya3 = biaya2.Replace(".", "");
                    cmdPostKP.Parameters.AddWithValue("@total_biaya", biaya3);

                    cmdPostKP.ExecuteNonQuery();

                    // --------------- 3.) Commit Transaction ------------------
                    trans.Commit();
                    trans.Dispose();
                    cmdKP.Dispose();
                    cmdPostKP.Dispose();
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                    
                    //hide panel biaya
                    this.PanelBiaya.Enabled = false;
                    this.PanelBiaya.Visible = false;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        /// <summary>
        /// Bayar Wisuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBayarWisuda_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // --------------- 1.) Insert Tagihan Akhir PPL I ------------------------
                    SqlCommand cmdKP = new SqlCommand("SpInsertTagihanBiayaAkhir", con);
                    cmdKP.Transaction = trans;
                    cmdKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdKP.Parameters.AddWithValue("@npm", LbNPM.Text);
                    cmdKP.Parameters.AddWithValue("@jenis_bayar", "Wisuda");
                    cmdKP.Parameters.AddWithValue("@tahun", DLThnPelaksanaan.SelectedItem.Text);
                    cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                    cmdKP.ExecuteNonQuery();

                    // --------------- 2.) Posting Tagihan Akhir PPL I to bank ------------------
                    SqlCommand cmdPostKP = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                    cmdPostKP.Transaction = trans;
                    cmdPostKP.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdPostKP.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                    cmdPostKP.Parameters.AddWithValue("@jenis_biaya", "Wisuda");
                    string biaya = this.GVWISUDA.Rows[0].Cells[2].Text;
                    string biaya2 = biaya.Replace("Rp", "");
                    string biaya3 = biaya2.Replace(".", "");
                    cmdPostKP.Parameters.AddWithValue("@total_biaya", biaya3);

                    cmdPostKP.ExecuteNonQuery();

                    // --------------- 3.) Commit Transaction ------------------
                    trans.Commit();
                    trans.Dispose();
                    cmdKP.Dispose();
                    cmdPostKP.Dispose();
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);

                    //hide panel biaya
                    this.PanelBiaya.Enabled = false;
                    this.PanelBiaya.Visible = false;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        protected void DLThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LBResultFilter.Text = "";

            if (this.DLThnPelaksanaan.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            //---------Fill GRID VIEW BIAYA AKHIR --------------------
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                //------------------ Fill GV Biaya KP ----------------
                SqlCommand cmdKP = new SqlCommand(" SELECT bak_prog_study.prog_study, keu_biaya_kp.id_kp, keu_biaya_kp.tahun, keu_biaya_kp.biaya " +
                                                    " FROM  bak_prog_study INNER JOIN " +
                                                    " keu_biaya_kp ON bak_prog_study.id_prog_study = keu_biaya_kp.id_prog_study " +
                                                    " WHERE     (bak_prog_study.prog_study = @prodi) AND (keu_biaya_kp.tahun = @tahun) ", con);
                cmdKP.CommandType = System.Data.CommandType.Text;

                cmdKP.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKP.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                DataTable TableKP = new DataTable();
                TableKP.Columns.Add("No.");
                TableKP.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKP.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        //show panel biaya
                        this.PanelBiaya.Enabled = true;
                        this.PanelBiaya.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableKP.NewRow();
                            datarow["No."] = rdr["id_kp"];
                            datarow["Biaya"] = rdr["biaya"];

                            TableKP.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKP.DataSource = TableKP;
                        this.GVKP.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKP.Rows.Clear();
                        TableKP.Clear();
                        GVKP.DataSource = TableKP;
                        GVKP.DataBind();
                    }
                }

                //------------------ Fill GV Biaya PPLSD ----------------
                SqlCommand cmdPPLSD = new SqlCommand("SELECT bak_prog_study.prog_study, keu_biaya_pplsd.id_pplsd, keu_biaya_pplsd.biaya, keu_biaya_pplsd.tahun " +
                                                    " FROM bak_prog_study INNER JOIN " +
                                                    " keu_biaya_pplsd ON bak_prog_study.id_prog_study = keu_biaya_pplsd.id_prog_study " +
                                                    " WHERE (bak_prog_study.prog_study = @prodi) AND (keu_biaya_pplsd.tahun = @tahun)", con);
                cmdPPLSD.CommandType = System.Data.CommandType.Text;

                cmdPPLSD.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSD.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                DataTable TablePPLSD = new DataTable();
                TablePPLSD.Columns.Add("No.");
                TablePPLSD.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSD.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        //show panel biaya
                        this.PanelBiaya.Enabled = true;
                        this.PanelBiaya.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TablePPLSD.NewRow();
                            datarow["No."] = rdr["id_pplsd"];
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSD.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSD.DataSource = TablePPLSD;
                        this.GVPPLSD.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSD.Rows.Clear();
                        TablePPLSD.Clear();
                        GVPPLSD.DataSource = TablePPLSD;
                        GVPPLSD.DataBind();
                    }
                }

                //------------------ Fill GV Biaya PPLSMA ----------------
                SqlCommand cmdPPLSMA = new SqlCommand(" SELECT bak_prog_study.prog_study, keu_biaya_pplsma.id_pplsma, keu_biaya_pplsma.tahun, keu_biaya_pplsma.biaya " +
                                                    " FROM bak_prog_study INNER JOIN " +
                                                    " keu_biaya_pplsma ON bak_prog_study.id_prog_study = keu_biaya_pplsma.id_prog_study " +
                                                    " WHERE (bak_prog_study.prog_study = @prodi) AND (keu_biaya_pplsma.tahun = @tahun)", con);
                cmdPPLSMA.CommandType = System.Data.CommandType.Text;

                cmdPPLSMA.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdPPLSMA.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                DataTable TablePPLSMA = new DataTable();
                TablePPLSMA.Columns.Add("No.");
                TablePPLSMA.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdPPLSMA.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            //show panel biaya
                            this.PanelBiaya.Enabled = true;
                            this.PanelBiaya.Visible = true;

                            DataRow datarow = TablePPLSMA.NewRow();
                            datarow["No."] = rdr["id_pplsma"];
                            datarow["Biaya"] = rdr["biaya"];

                            TablePPLSMA.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVPPLSMA.DataSource = TablePPLSMA;
                        this.GVPPLSMA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TablePPLSMA.Rows.Clear();
                        TablePPLSMA.Clear();
                        GVPPLSMA.DataSource = TablePPLSMA;
                        GVPPLSMA.DataBind();
                    }
                }

                //------------------ Fill GV Biaya KKN ----------------
                SqlCommand cmdKKN = new SqlCommand(" SELECT keu_biaya_kkn.id_kkn, keu_biaya_kkn.tahun, keu_biaya_kkn.biaya, bak_prog_study.prog_study " +
                                                    " FROM  keu_biaya_kkn INNER JOIN " +
                                                    " bak_prog_study ON keu_biaya_kkn.id_prog_study = bak_prog_study.id_prog_study " +
                                                    " WHERE (keu_biaya_kkn.tahun = @tahun) AND (bak_prog_study.prog_study =@prodi)", con);
                cmdKKN.CommandType = System.Data.CommandType.Text;

                cmdKKN.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdKKN.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                DataTable TableKKN = new DataTable();
                TableKKN.Columns.Add("No.");
                TableKKN.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdKKN.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        //show panel biaya
                        this.PanelBiaya.Enabled = true;
                        this.PanelBiaya.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableKKN.NewRow();
                            datarow["No."] = rdr["id_kkn"];
                            datarow["Biaya"] = rdr["biaya"];

                            TableKKN.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVKKN.DataSource = TableKKN;
                        this.GVKKN.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableKKN.Rows.Clear();
                        TableKKN.Clear();
                        GVKKN.DataSource = TableKKN;
                        GVKKN.DataBind();
                    }
                }

                //------------------ Fill GV Biaya WISUDA ----------------
                SqlCommand cmdWisuda = new SqlCommand("SELECT bak_prog_study.prog_study, keu_biaya_wisuda.id_wisuda, keu_biaya_wisuda.tahun, keu_biaya_wisuda.biaya " +
                                                    " FROM bak_prog_study INNER JOIN " +
                                                    " keu_biaya_wisuda ON bak_prog_study.id_prog_study = keu_biaya_wisuda.id_prog_study " +
                                                    " WHERE (bak_prog_study.prog_study = @prodi) AND (keu_biaya_wisuda.tahun = @tahun)", con);
                cmdWisuda.CommandType = System.Data.CommandType.Text;

                cmdWisuda.Parameters.AddWithValue("@tahun", this.DLThnPelaksanaan.SelectedItem.Text);
                cmdWisuda.Parameters.AddWithValue("@prodi", this.LbProdi.Text);

                DataTable TableWisuda = new DataTable();
                TableWisuda.Columns.Add("No.");
                TableWisuda.Columns.Add("Biaya");

                using (SqlDataReader rdr = cmdWisuda.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            //show panel biaya
                            this.PanelBiaya.Enabled = true;
                            this.PanelBiaya.Visible = true;

                            DataRow datarow = TableWisuda.NewRow();
                            datarow["No."] = rdr["id_wisuda"];
                            datarow["Biaya"] = rdr["biaya"];

                            TableWisuda.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVWISUDA.DataSource = TableWisuda;
                        this.GVWISUDA.DataBind();

                        LBResultFilter.Text = "";
                    }
                    else
                    {
                        //clear Gridview
                        TableWisuda.Rows.Clear();
                        TableWisuda.Clear();
                        GVWISUDA.DataSource = TableWisuda;
                        GVWISUDA.DataBind();
                    }
                }
            }
        }

        //bayar biaya akhir tidak penuh
        protected void BtnBayar_Click(object sender, EventArgs e)
        {
            //Filter Tahun Pelaksanaan
            if (this.DLthn.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Pelaksanaan');", true);
                return;
            }

            //Filter Jenis Pembayaran
            if (this.DLPembayaran.SelectedItem.Text == "Pembayaran")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Pembayaran');", true);
                return;
            }

            //Filter Jumlah Pembayaran
            if (this.TbBiaya.Text == "0" || this.TbBiaya.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Biaya Pembayaran');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    // ----------- 1.) Get Tagihan Akhir Wisuda / KKN -------------------
                    SqlCommand CmdBeban = new SqlCommand("GetBiayaAkhir", con);
                    CmdBeban.Transaction = trans;
                    CmdBeban.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBeban.Parameters.AddWithValue("@jenis", this.DLPembayaran.SelectedItem.Text);
                    //karena tiap2 prodi besarnya tagihan sama, ambil salah satu prodi saja
                    CmdBeban.Parameters.AddWithValue("@idprodi", "20-201");
                    CmdBeban.Parameters.AddWithValue("@tahun", this.DLthn.SelectedItem.Text);

                    SqlParameter BiayaAkhir = new SqlParameter();
                    BiayaAkhir.ParameterName = "@biaya";
                    BiayaAkhir.SqlDbType = System.Data.SqlDbType.Decimal;
                    BiayaAkhir.Direction = System.Data.ParameterDirection.Output;
                    BiayaAkhir.Size = 18;
                    BiayaAkhir.Precision = 18;
                    BiayaAkhir.Scale = 0;
                    CmdBeban.Parameters.Add(BiayaAkhir);

                    CmdBeban.ExecuteNonQuery();

                    //------------- 2.) jika biaya Nol atau Null / tidak didapatkan --------------
                    if (Convert.ToDecimal(BiayaAkhir.Value) == 0 || Convert.ToString(BiayaAkhir.Value) == "")
                    {
                        trans.Rollback();
                        trans.Dispose();
                        con.Close();
                        con.Dispose();
                        return;
                    }

                    //------------- 3.)  Get Beban Awal -------------------
                    Decimal BAwal = 0;
                    SqlCommand CmdAwal = new SqlCommand("SELECT NO,beban_awal FROM dbo.keu_keu_mhs WHERE npm=@npm", con);
                    CmdAwal.Transaction = trans;
                    CmdAwal.CommandType = System.Data.CommandType.Text;

                    CmdAwal.Parameters.AddWithValue("@npm", this.LbNPM.Text);

                    using (SqlDataReader rdr = CmdAwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                BAwal = Convert.ToDecimal(rdr["beban_awal"]);
                            }
                        }
                    }

                    // ------------ 4.) if Bebab Awal Kurang/Menunggak/Hutang ataupun Sisa ---------------
                    // ------- BEBAN AWAL HARUS DIUPDATE ----- 
                    if (BAwal > 0 || BAwal < 0)
                    {
                        // a. -------- Update Beban Awal ----------------
                        // beban awal = beban awal + tagihan akhir
                        SqlCommand CmdSisa = new SqlCommand("SpUpdateBbnAwal1", con);
                        CmdSisa.Transaction = trans;
                        CmdSisa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdSisa.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdSisa.Parameters.AddWithValue("@TAkhir", Convert.ToDecimal(BiayaAkhir.Value));

                        CmdSisa.ExecuteNonQuery();

                        // b. --------- Post bill to Bank -------------
                        SqlCommand cmdPost = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                        cmdPost.Transaction = trans;
                        cmdPost.CommandType = System.Data.CommandType.StoredProcedure;

                        cmdPost.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                        cmdPost.Parameters.AddWithValue("@jenis_biaya", this.DLPembayaran.SelectedItem.Text);
                        cmdPost.Parameters.AddWithValue("@total_biaya", this.TbBiaya.Text);

                        cmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        CmdBeban.Dispose();
                        CmdAwal.Dispose();
                        CmdSisa.Dispose();
                        cmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                    }
                    else if (BAwal == Convert.ToDecimal(BiayaAkhir.Value))
                    // ------------ 5.) If Beban Awal = Tagihan Akhir ------------
                    {
                        //--------- Tidak Perlu Update Beban Awal ----------
                        //--------- Post bill to Bank -------------
                        SqlCommand cmdPost = new SqlCommand("SpInsertPostingBiayaAkhir", con);
                        cmdPost.Transaction = trans;
                        cmdPost.CommandType = System.Data.CommandType.StoredProcedure;

                        cmdPost.Parameters.AddWithValue("@Npm_Mhs", LbNPM.Text);
                        cmdPost.Parameters.AddWithValue("@jenis_biaya", this.DLPembayaran.SelectedItem.Text);
                        cmdPost.Parameters.AddWithValue("@total_biaya", this.TbBiaya.Text);

                        cmdPost.ExecuteNonQuery();

                        trans.Commit();
                        trans.Dispose();
                        cmdPost.Dispose();
                        con.Close();
                        con.Dispose();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Berhasil');", true);
                    }
                    else if (BAwal == 0)
                    {
                        trans.Rollback();
                        trans.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilihlah Fasilitas Bayar Penuh, proses dibatalkan ...');", true);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    trans.Dispose();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }
    }
}