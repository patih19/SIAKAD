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
        // -------------- End Logout ----------------------------//

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

        protected bool CegahBayarSmesterSatu(string semester)
        {
            string CSUKT = ConfigurationManager.ConnectionStrings["UKTDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSUKT))
            {
                string SemesterSatu = "";

                con.Open();
                try
                {
                    SqlCommand CmdValBdk = new SqlCommand(@" 
                    DECLARE @ThnAngkatan VARCHAR(4) 
                    SELECT @ThnAngkatan =LEFT(thn_angkatan,4) FROM UntidarDb.dbo.bak_mahasiswa WHERE npm=@npm 
                    DECLARE @gasal VARCHAR(1) = '1' 
                    SELECT (@ThnAngkatan+@gasal) AS SemesterSatu 
                    ", con);

                    CmdValBdk.CommandType = System.Data.CommandType.Text;

                    CmdValBdk.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    using (SqlDataReader rdr = CmdValBdk.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                SemesterSatu = rdr["SemesterSatu"].ToString();
                            }
                        }
                    }

                    if (semester == SemesterSatu)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
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

        protected void AktivasiBayarJateng()
        {
            string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSMaba))
            {
                con.Open();
                try
                {
                    SqlCommand CmdAktivasiJateng = new SqlCommand(@"
                    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
				    SET XACT_ABORT ON;
				    DECLARE @trans int;

				    BEGIN TRY
					    SET @trans = @@TRANCOUNT
					    IF @trans = 0
					    BEGIN TRANSACTION;

                        -- ================== CEK KALDIK SEMESTER BERJALAN =================== --
                        DECLARE @NoNpm VARCHAR(12)
                        DECLARE @IdProdi VARCHAR(12)
                        DECLARE @Jenjang VARCHAR(3)
                        DECLARE @TopSemester VARCHAR(5)
                        DECLARE @Semester VARCHAR(5) 
                        DECLARE @SemesterMulai VARCHAR(5)

                        SELECT @NoNpm=bak_mahasiswa.npm, @SemesterMulai=bak_mahasiswa.smster_mulai, @IdProdi=bak_mahasiswa.id_prog_study, @Jenjang=bak_prog_study.jenjang
                        FROM            bak_mahasiswa INNER JOIN
                                        bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study
                        WHERE npm=@npm

                        SELECT TOP 1 @Semester=semester FROM dbo.bak_kal WHERE semester=@GetSemester
                        IF @Semester IS NULL
                        BEGIN
                            RAISERROR('Kalender Akademik Semester Ini Belum Aktif, Proses Dibatalkan ...', 16, 10)  
                            RETURN 
                        END

                        IF (@Jenjang ='S1' OR @Jenjang='D3')
                        BEGIN
                            SELECT TOP 1 @TopSemester=semester FROM dbo.bak_kal 
                            WHERE jenjang='S1' AND semester != 'new'
                            GROUP BY semester,jenjang
                            ORDER BY semester DESC
                        END
                        ELSE
                        BEGIN
                            SELECT TOP 1 @TopSemester=semester FROM dbo.bak_kal 
                            WHERE jenjang='S2' AND semester != 'new'
                            GROUP BY semester,jenjang
                            ORDER BY semester DESC
                        END

                        IF (@TopSemester <> @Semester)
                        BEGIN
                            RAISERROR('Aktivasi Pembayaran Tidak Sesuai Dengan Semester Aktif, Proses Dibatalkan ...', 16, 10)  
                            RETURN 
                        END

                        -- ==================== CEK APAKAH PEMBAYARN UNTUK SEMESTER SATU =================== --
                        IF @SemesterMulai = @GetSemester
                        BEGIN
                            RAISERROR('SEMESTER SATU SUDAH LUNAS', 16, 10)  
                            RETURN 
                        END

                        -- ====================== POST TAGIHAN (JATENG) ======================== --
                        exec ukt.dbo.SpInsertPostingMhsUkt @npm,@GetSemester 

                        IF @trans = 0
					    COMMIT TRANSACTION;
				    END TRY 
				    BEGIN CATCH
					    --Something went wrong. Better undo...
					    IF XACT_STATE() <> 0 AND @trans = 0
					    BEGIN
						    ROLLBACK TRANSACTION;
					    END;
					    DECLARE
					    @err_msg nvarchar(4000) = ERROR_MESSAGE(),
					    @err_sev int = ERROR_SEVERITY(),
					    @err_st int = ERROR_STATE();
					    RAISERROR(@err_msg,@err_sev,@err_st);
				    END CATCH;
                    ", con);

                    CmdAktivasiJateng.CommandType = System.Data.CommandType.Text;
                    CmdAktivasiJateng.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdAktivasiJateng.Parameters.AddWithValue("@GetSemester", this.DLTahun.SelectedItem.Value.ToString().Trim() + this.DLSemester.SelectedValue.Trim());

                    CmdAktivasiJateng.ExecuteNonQuery();

                    Page_Load(this, null);
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();

                    //this.PanelMessage.Enabled = true;
                    //this.PanelMessage.Visible = true;
                    //this.LbMsg.Text = ex.Message;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void AktivasiBayarBtn()
        {
            string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSMaba))
            {
                con.Open();
                try
                {
                    SqlCommand CmdAktivasiBtn = new SqlCommand(@"

                    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
				    SET XACT_ABORT ON;
				    DECLARE @trans int;

				    BEGIN TRY
					    SET @trans = @@TRANCOUNT
					    IF @trans = 0
					    BEGIN TRANSACTION;

                        -- ================== CEK KALDIK SEMESTER BERJALAN =================== --
                        DECLARE @NoNpm VARCHAR(12)
                        DECLARE @Nama VARCHAR(75)
                        DECLARE @Prodi VARCHAR(75)
                        DECLARE @Angkatan VARCHAR(12)
                        DECLARE @SemesterMulai VARCHAR(5)
                        DECLARE @IdProdi VARCHAR(12)
                        DECLARE @Jenjang VARCHAR(3)
                        DECLARE @TopSemester VARCHAR(5)
                        DECLARE @Semester VARCHAR(5) 
                        DECLARE @Bdk VARCHAR(3)

                        SELECT @NoNpm=bak_mahasiswa.npm,@Nama=bak_mahasiswa.nama,@Bdk=bak_mahasiswa.bdk,@Angkatan=bak_mahasiswa.thn_angkatan,@SemesterMulai=bak_mahasiswa.smster_mulai, @IdProdi=bak_mahasiswa.id_prog_study, @Prodi=bak_prog_study.prog_study, @Jenjang=bak_prog_study.jenjang
                        FROM            bak_mahasiswa INNER JOIN
                                        bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study
                        WHERE npm=@npm

                        SELECT TOP 1 @Semester=semester FROM dbo.bak_kal WHERE semester=@GetSemester
                        IF @Semester IS NULL
                        BEGIN
                            RAISERROR('Kalender Akademik Semester Ini Belum Aktif, Proses Dibatalkan ...', 16, 10)  
                            RETURN 
                        END

                        IF (@Jenjang ='S1' OR @Jenjang='D3')
                        BEGIN
                            SELECT TOP 1 @TopSemester=semester FROM dbo.bak_kal 
                            WHERE jenjang='S1' AND semester != 'new'
                            GROUP BY semester,jenjang
                            ORDER BY semester DESC
                        END
                        ELSE
                        BEGIN
                            SELECT TOP 1 @TopSemester=semester FROM dbo.bak_kal 
                            WHERE jenjang='S2' AND semester != 'new'
                            GROUP BY semester,jenjang
                            ORDER BY semester DESC
                        END

                        IF (@TopSemester <> @Semester)
                        BEGIN
                            RAISERROR('Aktivasi Pembayaran Tidak Sesuai Dengan Semester Aktif, Proses Dibatalkan ...', 16, 10)  
                            RETURN 
                        END

                        -- ==================== CEK APAKAH PEMBAYARN UNTUK SEMESTER SATU =================== --
                        IF @SemesterMulai = @GetSemester
                        BEGIN
                            RAISERROR('SEMESTER SATU SUDAH LUNAS', 16, 10)  
                            RETURN 
                        END

                        --========================= GET BIAYA UKT ========================== --
                        DECLARE @Biaya DECIMAL
                        DECLARE @NomorMhs VARCHAR(12)

                        SELECT @NomorMhs=UntidarDb.dbo.bak_mahasiswa.npm, @Biaya=ukt.dbo.ukt_master.biaya
                        FROM    UntidarDb.dbo.bak_mahasiswa INNER JOIN
			                        ukt.dbo.ukt_master ON ukt.dbo.ukt_master.idprodi = bak_mahasiswa.id_prog_study AND 
			                        ukt.dbo.ukt_master.thn_ukt = bak_mahasiswa.thn_ukt AND 
			                        ukt.dbo.ukt_master.kategori = bak_mahasiswa.ukt
                        WHERE  UntidarDb.dbo.bak_mahasiswa.npm =@npm

                        -- ======================= BAYAR UKT ========================== --
                        DECLARE @NoBayar BIGINT
                        SELECT @NoBayar=nomor FROM ukt.dbo.keu_posting_bank WHERE payeeId=@npm AND billRef4=@GetSemester 


                        IF @NoBayar IS NOT NULL
                        BEGIN
	                        raiserror ('Biaya UKT Semester Ini Sudah Ada, Proses Dibatalkan... ', 16, 10)
	                        return
                        END
                        ELSE
                        BEGIN
	                        IF @Bdk = '1'
	                        BEGIN
		                        -- Billing Number --
		                        declare @tgl_bdk varchar(10)
		                        declare @tglstring_bdk varchar(10) 
		                        declare @tglprefix_bdk varchar(6) 
		                        select @tgl_bdk= CONVERT(varchar,getdate(),23)
		                        select @tglstring_bdk=REPLACE(@tgl_bdk,'-','')
		                        select @tglprefix_bdk =RIGHT(@tglstring_bdk,6)

		                        declare @BillNumber_bdk varchar(12)
		                        declare @RegNomor_bdk BIGINT
		                        declare @Prefix_bdk varchar(2) = '01'
		                        select @RegNomor_bdk= COUNT(*)+1 from ukt.dbo.keu_posting_bank where CAST(post_date as date) = @tgl_bdk AND bank ='btn'

		                        select @BillNumber_bdk=@Prefix_bdk +@tglprefix_bdk+RIGHT('0000'+CAST(@RegNomor_bdk as varchar(4)),4)
		                        --print @BillNumber 

		                        -- ukt db --
		                        insert into ukt.dbo.keu_posting_bank (billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,post_date,status,bank,keterangan) values
		                        (@BillNumber_bdk,@npm,@Nama,@Prodi,NULL,@Angkatan,@GetSemester,NULL,@biaya,GETDATE(),'paid','btn','BIAYA SEMESTER UKT')
	                        END
	                        ELSE
	                        BEGIN
		                        -- Billing Number --
		                        declare @tgl varchar(10)
		                        declare @tglstring varchar(10) 
		                        declare @tglprefix varchar(6) 
		                        select @tgl= CONVERT(varchar,getdate(),23)
		                        select @tglstring=REPLACE(@tgl,'-','')
		                        select @tglprefix =RIGHT(@tglstring,6)

		                        declare @BillNumber varchar(12)
		                        declare @RegNomor BIGINT
		                        declare @Prefix varchar(2) = '01'
		                        select @RegNomor= COUNT(*)+1 from ukt.dbo.keu_posting_bank where CAST(post_date as date) = @tgl AND bank ='btn'

		                        select @BillNumber=@Prefix +@tglprefix+RIGHT('0000'+CAST(@RegNomor as varchar(4)),4)
		                        --print @BillNumber 

		                        -- ukt db --
		                        insert into ukt.dbo.keu_posting_bank (billingNo,payeeId,name,billRef1,billRef2,billRef3,billRef4,billRef5,amount_total,post_date,status,bank,keterangan) values
		                        (@BillNumber,@npm,@Nama,@Prodi,NULL,@Angkatan,@GetSemester,NULL,@biaya,GETDATE(),'unpaid','btn','BIAYA SEMESTER UKT')

		                        -- BTN db --
		                        INSERT INTO btn.dbo.keu_posting_bank
				                        ( billingNo ,
				                          payeeId ,
				                          name ,
				                          billRef1 ,
				                          billRef2 ,
				                          billRef3 ,
				                          billRef4,
				                          billRef5 ,
				                          amount_total ,
				                          post_date ,
				                          status ,
				                          keterangan
				                        )
		                        VALUES  ( @BillNumber , -- billingNo - nvarchar(12)
				                          @npm , -- payeeId - nvarchar(10)
				                          @Nama , -- name - nvarchar(45)
				                          @Prodi , -- billRef1 - nvarchar(50)
				                          NULL , -- billRef2 - nvarchar(25)
				                          @Angkatan , -- billRef3 - nvarchar(25)
				                          @GetSemester,
				                          NULL, -- billRef5 - nvarchar(10)
				                          @biaya , -- amount_total - decimal
				                          GETDATE() , -- post_date - datetime
				                          'unpaid' , -- status - nvarchar(7)
				                          'BIAYA SEMESTER UKT'  -- keterangan - varchar(50)
				                        )		
	                        END
                        END

                        IF @trans = 0
					    COMMIT TRANSACTION;
				    END TRY 
				    BEGIN CATCH
					    --Something went wrong. Better undo...
					    IF XACT_STATE() <> 0 AND @trans = 0
					    BEGIN
						    ROLLBACK TRANSACTION;
					    END;
					    DECLARE
					    @err_msg nvarchar(4000) = ERROR_MESSAGE(),
					    @err_sev int = ERROR_SEVERITY(),
					    @err_st int = ERROR_STATE();
					    RAISERROR(@err_msg,@err_sev,@err_st);
				    END CATCH;
                    ", con);

                    CmdAktivasiBtn.CommandType = System.Data.CommandType.Text;
                    CmdAktivasiBtn.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdAktivasiBtn.Parameters.AddWithValue("@GetSemester", this.DLTahun.SelectedItem.Value.ToString().Trim() + this.DLSemester.SelectedValue.Trim());

                    CmdAktivasiBtn.ExecuteNonQuery();

                    Page_Load(this,null);
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();

                    //this.PanelMessage.Enabled = true;
                    //this.PanelMessage.Visible = true;
                    //this.LbMsg.Text = ex.Message;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                    return;
                }
            }
        }

        protected void ButtonAktivasi_Click(object sender, EventArgs e)
        {

            try
            {

                string jenis_bank = "";

                string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CSMaba))
                {
                    con.Open();

                    SqlCommand CekAktivasi = new SqlCommand(@"
                DECLARE @semester_mulai VARCHAR(5)
                DECLARE @kode_fakultas VARCHAR(8)

                DECLARE @no_npm VARCHAR(12)
                SELECT        @no_npm=bak_mahasiswa.npm, @kode_fakultas=bak_prog_study.id_fakultas,@semester_mulai=smster_mulai
                FROM            bak_mahasiswa INNER JOIN
                                         bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study WHERE npm=@npm

                SELECT nomor,bank FROM keu_master_bank WHERE kode_fakultas=@kode_fakultas AND 
                mhs_mulai_semester <= @semester_mulai AND sampai_semester >= @semester_mulai ", con);

                    CekAktivasi.CommandType = System.Data.CommandType.Text;
                    CekAktivasi.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    using (SqlDataReader rdr = CekAktivasi.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                jenis_bank = rdr["bank"].ToString();
                            }
                        }
                    }

                    if (jenis_bank == "jateng")
                    {
                        AktivasiBayarJateng();
                    }
                    else if (jenis_bank == "btn")
                    {
                        AktivasiBayarBtn();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                return;
            }

        }
    }
}