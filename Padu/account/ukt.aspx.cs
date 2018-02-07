using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Padu.account
{
    public partial class WebForm11 : Mhs_account
    {
        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }

        public string _Unpaid
        {
            get { return this.ViewState["unpaid"].ToString(); }
            set { this.ViewState["unpaid"] = (object)value; }
        }

        public string _Bidikmisi
        {
            get { return this.ViewState["Bidikmisi"].ToString(); }
            set { this.ViewState["Bidikmisi"] = (object)value; }
        }

        public string _ThnAngkatan
        {
            get { return this.ViewState["ThnAngkatan"].ToString(); }
            set { this.ViewState["ThnAngkatan"] = (object)value; }
        }

        public decimal _TotalBayar
        {
            get { return Convert.ToDecimal(this.ViewState["TotalBebanAwal"].ToString()); }
            set { this.ViewState["TotalBebanAwal"] = (object)value; }
        }

        //----------------- Log Out --------------------//
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
            this.Session["jenjang"] = (object)null;
            this.Session["prodi"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.Remove("prodi");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------


        private void TahunAkademik()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 1 bak_kal.thn AS thn, CAST(bak_kal.thn AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +1 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN ('S1') group by thn ORDER BY thn DESC", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLTahun.DataSource = CmdJadwal.ExecuteReader();
                    this.DLTahun.DataTextField = "ThnAkm";
                    this.DLTahun.DataValueField = "thn";
                    this.DLTahun.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLTahun.Items.Insert(0, new ListItem("-- Tahun Akademik --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _NPM = this.Session["Name"].ToString().Trim();

                _Unpaid = "kosong";

                this.PanelPembayaran.Enabled = false;
                this.PanelPembayaran.Visible = false;

                this.PanelMessage.Enabled = false;
                this.PanelMessage.Visible = false;

                this.PanelAktivasi.Enabled = false;
                this.PanelAktivasi.Visible = false;

                string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;

                SqlConnection ConUntidar = new SqlConnection(CSUntidar);
                SqlConnection ConUKT = new SqlConnection(CSUKT);

                ConUntidar.Open();
                ConUKT.Open();

                var TransUntidar = ConUntidar.BeginTransaction();
                var TransUKT = ConUKT.BeginTransaction();

                try
                {
                    // 1. ------------- Biodata Mahasiswa ------------
                    SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", ConUntidar, TransUntidar);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@npm", _NPM.ToString().Trim());

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelPembayaran.Enabled = true;
                            this.PanelPembayaran.Visible = true;

                            while (rdr.Read())
                            {
                                this.LbNPM.Text = _NPM.ToString().Trim();
                                this.LbNama.Text = rdr["nama"].ToString().Trim();
                                this.LbAngkatan.Text = rdr["thn_angkatan"].ToString().Trim();
                                this.LbUkt.Text = rdr["biaya"].ToString().Trim();

                                _ThnAngkatan = rdr["thn_angkatan"].ToString().Trim();
                                this.LbProdi.Text = rdr["prog_study"].ToString();
                                if (rdr["bdk"].ToString().Trim() == "1")
                                {
                                    this.LbBeasiswa.Text = "Bidikmisi";
                                    _Bidikmisi = "Y";
                                }
                                else
                                {
                                    this.LbBeasiswa.Text = "-";
                                    _Bidikmisi = "N";
                                }
                                if (rdr["dosen"] != DBNull.Value)
                                {
                                    this.LbDosenPa.Text = rdr["dosen"].ToString().Trim();
                                } else
                                {
                                    this.LbDosenPa.Text = "Belum memiliki Pembimbing Akademik";
                                }

                            }
                        }
                        else
                        {
                            this.PanelPembayaran.Enabled = false;
                            this.PanelPembayaran.Visible = false;

                            //string msg = "alert('Data Pembayaran Tidak Ditemukan')";
                            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Pembayaran Tidak Ditemukan');", true);
                            return;
                        }


                        string FormattedString4Ukt = string.Format
                            (new System.Globalization.CultureInfo("id"), "{0:c}", Convert.ToInt32(this.LbUkt.Text));
                        this.LbUkt.Text = FormattedString4Ukt;

                        rdr.Close();
                        rdr.Dispose();
                    }

                    // 2. ------------- History Pembayaran Lunas ------------
                    SqlCommand CmdHistory = new SqlCommand("SpHistoryBayarUkt", ConUKT, TransUKT);
                    CmdHistory.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdHistory.Parameters.AddWithValue("@npm", _NPM.ToString().Trim());


                    DataTable Table = new DataTable();
                    Table.Columns.Add("No Billing");
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Biaya");
                    Table.Columns.Add("Cicilan");
                    Table.Columns.Add("Tanggal Aktif");
                    Table.Columns.Add("Tanggal Bayar");
                    Table.Columns.Add("Status");
                    Table.Columns.Add("Keterangan");


                    using (SqlDataReader rdr = CmdHistory.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["No Billing"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Biaya"] = rdr["amount_total"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Aktif"] = rdr["post_date"];
                                datarow["Tanggal Bayar"] = rdr["pay_date"];
                                datarow["Status"] = rdr["status"];

                                if (rdr["status"].ToString().Trim() == "unpaid")
                                {
                                    _Unpaid = "ada";
                                }
                                else
                                {
                                    _Unpaid = "tidak ada";
                                }

                                datarow["Keterangan"] = rdr["keterangan"];

                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvHistoryPaid.DataSource = Table;
                            this.GvHistoryPaid.DataBind();

                            HitungTagihan(this.GvHistoryPaid);

                        }
                        rdr.Close();
                        rdr.Dispose();
                    }

                    //3. ------- Populate Tahun Akademik -----
                    TahunAkademik();

                    // 4.---------- Simpan Perubahan --------------
                    TransUntidar.Commit();
                    TransUKT.Commit();
                    TransUntidar.Dispose();
                    TransUKT.Dispose();
                    ConUKT.Close();
                    ConUntidar.Close();
                    ConUntidar.Dispose();
                    ConUKT.Dispose();

                } catch(Exception ex)
                {
                    // close connection
                    TransUntidar.Rollback();
                    TransUKT.Rollback();
                    TransUntidar.Dispose();
                    TransUKT.Dispose();
                    ConUntidar.Close();
                    ConUKT.Close();
                    ConUntidar.Dispose();
                    ConUKT.Dispose();

                    //string msg = "alert('" + ex.Message + "')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        int TotalLunas = 0;
        int TotalHutang = 0;
        public void HitungTagihan(GridView gridview)
        {
            for (int currentRow = 0; currentRow < gridview.Rows.Count; currentRow++)
            {
                bool hutang = true;

                //Lunas
                if (gridview.Rows[currentRow].Cells[6].Text.Trim() == "paid" || gridview.Rows[currentRow].Cells[6].Text.Trim() == "offline")
                {
                    hutang = false;

                    string StrBayar = gridview.Rows[currentRow].Cells[2].Text.Trim();
                    StrBayar = StrBayar.Replace("Rp","").Trim();
                    StrBayar = StrBayar.Replace(".", "").Trim();
                    int Bayar = Convert.ToInt32(StrBayar);

                    TotalLunas += Bayar;

                    string FormattedLunas = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", TotalLunas);
                    this.LbTerbayar.Text = FormattedLunas;

                }
                // Hutang
                else if (hutang)
                {
                    string StrHutang = gridview.Rows[currentRow].Cells[2].Text.Trim();
                    StrHutang = StrHutang.Replace("Rp", "").Trim();
                    StrHutang = StrHutang.Replace(".", "").Trim();
                    int Hutang = Convert.ToInt32(StrHutang);

                    TotalHutang += Hutang;

                    string FormattedHutang = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", TotalHutang);
                    this.LbKekurangan.Text = FormattedHutang;

                    gridview.Rows[currentRow].BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9494");
                }
            }
        }

        //int TotalBayar = 0;
        protected void GvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Beban = Convert.ToInt32(e.Row.Cells[2].Text);
                //TotalBayar += Beban;
                //this._TotalBayar = TotalBayar;
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Beban);
                e.Row.Cells[2].Text = FormattedString4;
            }
        }

        protected void GvHistoryPaid_PreRender(object sender, EventArgs e)
        {
            if (this.GvHistoryPaid.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GvHistoryPaid.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GvHistoryPaid.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                GvHistoryPaid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void BtnAktivasi_Click(object sender, EventArgs e)
        {

            if (this.DLTahun.SelectedItem.Text.Trim() == "")
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (this.DLTahun.SelectedItem.Text.Trim() == "-- Tahun Akademik --")
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (DLTahun.SelectedItem.Value.Length != 4)
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (this.DLSemester.SelectedValue != "1" && this.DLSemester.SelectedValue != "2")
            {
                string msg = "alert('Pilih Semester')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            // ------------ Filter Tagihan Unapid ---------------- //
            if (_Unpaid.ToString().Trim() == "ada")
            {

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Aktivasi Tidak Diperbolehkan Tagihan Anda Belum Lunas');", true);
                return;
            }

            //------------- Get Mahasiswa Tahun Paling Akhir/Baru  --------------------- //
            int MhsBaru = 0;
            string TahunMhs = "";

            string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSMaba))
            {
                con.Open();

                SqlCommand CmdCekStatus = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                CmdCekStatus.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            TahunMhs = rdr["thn_angkatan"].ToString();
                        }
                    }
                }
            }
            MhsBaru = Convert.ToInt16(TahunMhs.Substring(0, 4));

            //------------ Compare tahun angkatan ------------------//
            int MyThnAngkatan = Convert.ToInt32(_ThnAngkatan.Substring(0, 4));

            if (MhsBaru == MyThnAngkatan)
            {
                if (this.DLSemester.SelectedValue == "1")
                {
                    // ---- Maba Semester Gasal ---//
                    if (_Bidikmisi == "Y")
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Tagihan di Semester Gasal Bagi Mahasiswa Baru Bidikmisi');", true);
                        return;
                    }
                    else
                    {
                        //---- Mahasiswa baru tidak lolos bidikmisi
                        if (HistoriSudahBayarUkt() == false)
                        {
                            AktivasiMulaiSemesterSatu();
                        }
                    }
                }
                else if (this.DLSemester.SelectedValue == "2")
                {
                    // ---- Maba Semester Genap {Mulai Semester Dua}---//
                    AktivasiMulaiSemesterDua();
                }
            }
            else
            {
                // --------------==== BUKAN MABA ====-------------- //
                // ---- {Bukan Maba} <=> {Maba Semester Genap} --- //
                AktivasiMulaiSemesterDua();
            }
        }

        protected Boolean HistoriSudahBayarUkt()
        {
            bool Bayar = false;

            // ---- Cek Validasi Bidikmsi ---//
            string CSValBidikmisi = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSValBidikmisi))
            {
                con.Open();
                try
                {
                    SqlCommand CmdValBdk = new SqlCommand("SpHistoryBayarUkt", con);
                    CmdValBdk.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdValBdk.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    using (SqlDataReader rdr = CmdValBdk.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                if (rdr["status"].ToString().Trim() == "unpaid")
                                {
                                    Bayar = false;
                                }
                                else
                                {
                                    Bayar = true;
                                }
                            }
                            return Bayar;
                        }
                        else
                        {
                            return Bayar;
                        }
                    }
                }
                catch (Exception)
                {
                    return Bayar;
                }
            }
        }

        protected void AktivasiMulaiSemesterSatu()
        {
            // ---- Maba Semester Genap ---//
            string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;

            SqlConnection ConUntidar = new SqlConnection(CSUntidar);
            SqlConnection ConUKT = new SqlConnection(CSUKT);

            ConUntidar.Open();
            ConUKT.Open();

            var TransUntidar = ConUntidar.BeginTransaction();
            var TransUKT = ConUKT.BeginTransaction();

            try
            {
                // 1. -------- Get Biaya Semester (UKT) --------
                SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", ConUKT, TransUKT);
                cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                cmdBiayaUKT.Parameters.AddWithValue("@npm", _NPM.ToString().Trim());

                SqlParameter Biaya = new SqlParameter();
                Biaya.ParameterName = "@biaya";
                Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                Biaya.Size = 20;
                Biaya.Direction = System.Data.ParameterDirection.Output;
                cmdBiayaUKT.Parameters.Add(Biaya);

                cmdBiayaUKT.ExecuteNonQuery();

                decimal biaya;
                biaya = Convert.ToDecimal(Biaya.Value.ToString());


                // 2.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                // --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                // -----------------------------------------------------------------------------------------------------------------
                SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", ConUKT, TransUKT);
                CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
                CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Value.ToString().Trim() + this.DLSemester.SelectedValue.Trim());
                CmdPost.ExecuteNonQuery();

                this.PanelPembayaran.Enabled = false;
                this.PanelPembayaran.Visible = false;

                this.PanelMessage.Enabled = true;
                this.PanelMessage.Visible = true;

                // close connection
                TransUntidar.Commit();
                TransUKT.Commit();
                TransUntidar.Dispose();
                TransUKT.Dispose();
                ConUntidar.Close();
                ConUKT.Close();
                ConUntidar.Dispose();
                ConUKT.Dispose();

                string FormattedString9 = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                this.LbTagihan.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                this.LbTagihan.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                // close connection
                TransUntidar.Rollback();
                TransUKT.Rollback();
                TransUntidar.Dispose();
                TransUKT.Dispose();
                ConUntidar.Close();
                ConUKT.Close();
                ConUntidar.Dispose();
                ConUKT.Dispose();

                //string msg = "alert('" + ex.Message + "')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                return;
            }
        }

        protected void AktivasiMulaiSemesterDua()
        {
            // ---- Maba Semester Genap ---//
            string CSUntidar = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;

            SqlConnection ConUntidar = new SqlConnection(CSUntidar);
            SqlConnection ConUKT = new SqlConnection(CSUKT);

            ConUntidar.Open();
            ConUKT.Open();

            var TransUntidar = ConUntidar.BeginTransaction();
            var TransUKT = ConUKT.BeginTransaction();

            try
            {
                // 1. -------- Get Biaya Semester (UKT) --------
                SqlCommand cmdBiayaUKT = new SqlCommand("SpGetBiayaUktMhs", ConUKT, TransUKT);
                cmdBiayaUKT.CommandType = System.Data.CommandType.StoredProcedure;

                cmdBiayaUKT.Parameters.AddWithValue("@npm", _NPM.ToString().Trim());

                SqlParameter Biaya = new SqlParameter();
                Biaya.ParameterName = "@biaya";
                Biaya.SqlDbType = System.Data.SqlDbType.Decimal;
                Biaya.Size = 20;
                Biaya.Direction = System.Data.ParameterDirection.Output;
                cmdBiayaUKT.Parameters.Add(Biaya);

                cmdBiayaUKT.ExecuteNonQuery();

                decimal biaya;
                biaya = Convert.ToDecimal(Biaya.Value.ToString());


                // 2.) POSTING tahihan to BANK by using SpInsertPostingMhsUkt -----
                // --- Catatan : Untuk Awal Semester tidak Perlu Posting Tagihan Karena Tagihan Semester Awal sudah dibayarkan setelah mengisi form UKT (pada saat sebelum registrasi)
                // -----------------------------------------------------------------------------------------------------------------
                SqlCommand CmdPost = new SqlCommand("SpInsertPostingMhsUkt", ConUKT, TransUKT);
                CmdPost.CommandType = System.Data.CommandType.StoredProcedure;
                CmdPost.Parameters.AddWithValue("@Npm_Mhs", this.Session["Name"].ToString());
                CmdPost.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Value.ToString().Trim() + this.DLSemester.SelectedValue.Trim());
                CmdPost.ExecuteNonQuery();

                this.PanelPembayaran.Enabled = false;
                this.PanelPembayaran.Visible = false;

                this.PanelMessage.Enabled = true;
                this.PanelMessage.Visible = true;

                // close connection
                TransUntidar.Commit();
                TransUKT.Commit();
                TransUntidar.Dispose();
                TransUKT.Dispose();
                ConUntidar.Close();
                ConUKT.Close();
                ConUntidar.Dispose();
                ConUKT.Dispose();

                string FormattedString9 = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", biaya);
                this.LbTagihan.Text = "Tagihan Yang Harus Dibayar : " + FormattedString9;
                this.LbTagihan.ForeColor = System.Drawing.Color.Green;

            }
            catch (Exception ex)
            {
                // close connection
                TransUntidar.Rollback();
                TransUKT.Rollback();
                TransUntidar.Dispose();
                TransUKT.Dispose();
                ConUntidar.Close();
                ConUKT.Close();
                ConUntidar.Dispose();
                ConUKT.Dispose();

                //string msg = "alert('" + ex.Message + "')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                return;
            }
        }

        protected void BtnOpenAktv_Click(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedItem.Text.Trim() == "")
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (this.DLTahun.SelectedItem.Text.Trim() == "-- Tahun Akademik --")
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (DLTahun.SelectedItem.Value.Length != 4)
            {
                string msg = "alert('Pilih Tahun')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            if (this.DLSemester.SelectedValue != "1" && this.DLSemester.SelectedValue != "2")
            {
                string msg = "alert('Pilih Semester')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }

            this.PanelMessage.Enabled = false;
            this.PanelMessage.Visible = false;

            //------------- Get Mahasiswa Tahun Paling Akhir/Baru  --------------------- //
            int MhsBaru = 0;
            string TahunMhs = "";

            string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSMaba))
            {
                con.Open();

                SqlCommand CmdCekStatus = new SqlCommand("SELECT TOP 1 thn_angkatan FROM dbo.bak_mahasiswa GROUP BY thn_angkatan ORDER BY thn_angkatan DESC", con);
                CmdCekStatus.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdCekStatus.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            TahunMhs = rdr["thn_angkatan"].ToString();
                        }
                    }
                }
            }
            MhsBaru = Convert.ToInt16(TahunMhs.Substring(0, 4));

            //------------ Compare tahun angkatan ------------------//
            int MyThnAngkatan = Convert.ToInt32(_ThnAngkatan.Substring(0, 4));

            if (MhsBaru == MyThnAngkatan)
            {
                // ---------------=== MAHASISWA BARU ===------------------//
                if (this.DLSemester.SelectedValue == "1")
                {
                    // ----  Semester Gasal ---//
                    // ---- Semua pembayaran lunas di awal -----//
                    // ---- keculai bagi mahasiswa baru yg tidk lolos Bidikmisi ----//

                    if (_Bidikmisi == "Y")
                    {
                        this.PanelAktivasi.Enabled = false;
                        this.PanelAktivasi.Visible = false;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Tagihan di Semester Gasal Untuk Mahasiswa Baru Bidikmisi');", true);
                        return;
                    }
                    else
                    {
                        this.PanelAktivasi.Enabled = true;
                        this.PanelAktivasi.Visible = true;
                    }
                }
                else if (this.DLSemester.SelectedValue == "2")
                {
                    // ---- Maba Semester Genap ---//
                    // 1.--- Bidikmisi ---
                    if (_Bidikmisi == "Y")
                    {
                        ValPesertaBidikmisi();
                    }
                    // 2.--- Non Bidikmisi ---
                    else if (_Bidikmisi == "N")
                    {
                        this.PanelAktivasi.Enabled = true;
                        this.PanelAktivasi.Visible = true;
                    }
                }
            }
            else
            {
                // --------------==== BUKAN MABA ====-------------- //
                // ---- {Bukan Maba} <=> {Maba Semester Genap} --- //
                // 1.--- Bidikmisi ---
                if (_Bidikmisi == "Y")
                {
                    ValPesertaBidikmisi();
                }
                // 2.--- Non Bidikmisi ---
                else if (_Bidikmisi == "N")
                {
                    this.PanelAktivasi.Enabled = true;
                    this.PanelAktivasi.Visible = true;
                }

            }
        }

        protected void ValPesertaBidikmisi()
        {
            // ---- Cek Validasi Bidikmsi ---//
            string CSValBidikmisi = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSValBidikmisi))
            {
                con.Open();

                try
                {
                    SqlCommand CmdValBdk = new SqlCommand("SELECT id,semester,status,last_update FROM dbo.keu_aktivasi_bidikmisi WHERE semester=@semester ", con);
                    CmdValBdk.CommandType = System.Data.CommandType.Text;

                    CmdValBdk.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Value.ToString().Trim() + this.DLSemester.SelectedValue.Trim());

                    using (SqlDataReader rdr = CmdValBdk.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                if (rdr["status"].ToString().Trim() == "1")
                                {
                                    this.PanelAktivasi.Enabled = true;
                                    this.PanelAktivasi.Visible = true;

                                    this.LbSemesterBdk.Text = this.DLTahun.SelectedItem.Text.ToString().Trim() + this.DLSemester.SelectedValue.Trim();
                                    this.LbStatusBdk.Text = "Ready (aktivasi sekarang!)";
                                    this.LbStatusBdk.ForeColor = System.Drawing.Color.Green;

                                }
                            }
                        }
                        else
                        {
                            this.PanelAktivasi.Enabled = false;
                            this.PanelAktivasi.Visible = false;

                            this.PanelMessage.Enabled = true;
                            this.PanelMessage.Visible = true;

                            this.LbMsg.Text = "Peserta bidikmisi belum divalidasi, AKTIVASI GAGAL hubungi bagian Pelayanan Keuangan";
                            this.LbMsg.ForeColor = System.Drawing.Color.Red;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Peserta bidikmisi belum divalidasi, AKTIVASI GAGAL hubungi bagian Pelayanan Keuangan');", true);
                            return;
                        }
                    }

                } 
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                    return;
                }

            }
        }
    }
}