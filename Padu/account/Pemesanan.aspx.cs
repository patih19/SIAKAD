using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Padu.account
{
    public partial class Pemesanan : Mhs_account
    {
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

        public String _Tahun
        {
            get { return this.ViewState["tahun"].ToString(); }
            set { this.ViewState["tahun"] = (object)value; }
        }

        public String _Semester
        {
            get { return this.ViewState["semester"].ToString(); }
            set { this.ViewState["semester"] = (object)value; }
        }

        public decimal _PrevIPS
        {
            get { return Convert.ToDecimal(this.ViewState["PrevIPS"].ToString()); }
            set { this.ViewState["PrevIPS"] = (object)value; }
        }

        public int _MaxKRS
        {
            get { return Convert.ToInt32(this.ViewState["MaxKRS"].ToString()); }
            set { this.ViewState["MaxKRS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.LbSemester.Text = "";
                this.LbMaxSKS.Text = "";

                _PrevIPS = 0;
                _MaxKRS = 0;

                _Tahun = "";
                _Semester = "";

                this.PanelMakulDitawarkan.Enabled = false;
                this.PanelMakulDitawarkan.Visible = false;

                this.PanelMakulDipesan.Enabled = false;
                this.PanelMakulDipesan.Visible = false;

                this.PanelMsg.Enabled = false;
                this.PanelMsg.Visible = false;

                this.PanelListMakulDipesan.Enabled = false;
                this.PanelListMakulDipesan.Visible = false;

                TahunAkademik();
            }
        }

        private void TahunAkademik()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 2 bak_kal.thn +1 AS thn, CAST(bak_kal.thn+1 AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +2 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN ('S1') group by thn ORDER BY thn DESC", con);
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if ((this.RbPesan.Checked == false) && (this.RbLihatPesanan.Checked == false))
            {
                string message = "alert('Pilih Jenis Pesanan')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (this.DLTahun.SelectedValue == "-1")
            {
                string message = "alert('Pilih Tahun Akademik')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (this.DLSemester.SelectedValue == "-1")
            {
                string message = "alert('Pilih Semester')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            _Tahun = this.DLTahun.SelectedValue.Trim();
            _Semester = this.DLSemester.SelectedValue.Trim();

            // ====== PEMESANAN MAKUL ==========
            if (RbPesan.Checked)
            {
                // ----------- CEK MASA KEGIATAN PENAWARAN ----
                string MasKeg = CekMasaKeg();
                if (MasKeg != "ok")
                {
                    this.PanelMakulDitawarkan.Enabled = false;
                    this.PanelMakulDitawarkan.Visible = false;

                    this.PanelMakulDipesan.Enabled = false;
                    this.PanelMakulDipesan.Visible = false;

                    this.PanelMsg.Enabled = false;
                    this.PanelMsg.Visible = false;

                    this.PanelListMakulDipesan.Enabled = false;
                    this.PanelListMakulDipesan.Visible = false;

                    string message = MasKeg;
                    string mes = "alert('" + message + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mes, true);
                    return;
                }

                // ---------- CEK TAGIHAN MAHASISWA -----
                string CekTagihan = CekPembayaran();
                if (CekTagihan != "ok")
                {
                    this.PanelMakulDitawarkan.Enabled = false;
                    this.PanelMakulDitawarkan.Visible = false;

                    this.PanelMakulDipesan.Enabled = false;
                    this.PanelMakulDipesan.Visible = false;

                    this.PanelMsg.Enabled = false;
                    this.PanelMsg.Visible = false;

                    this.PanelListMakulDipesan.Enabled = false;
                    this.PanelListMakulDipesan.Visible = false;

                    string message = CekTagihan;
                    string mes = "alert('" + message + "')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mes, true);
                    return;
                }

                this.LbSemester.Text = "Pemesanan Untuk Tahun Akademik " + this.DLTahun.SelectedItem.Text.Trim() + " Semester " + this.DLSemester.SelectedItem.Text.Trim() + " (" + _Tahun + _Semester + ")";
                HitungMaxSks();
                PopulateMakulDitawarkan();
                PopulateMakulDipesan();
            }
            // ============ LIHAT PESANAN =========== //
            else if (this.RbLihatPesanan.Checked)
            {
                this.PanelMakulDitawarkan.Enabled = false;
                this.PanelMakulDitawarkan.Visible = false;

                this.PanelMakulDipesan.Enabled = false;
                this.PanelMakulDipesan.Visible = false;

                this.PanelMsg.Enabled = false;
                this.PanelMsg.Visible = false;

                DaftarMakulDipesan();
            }            
        }

        protected void DaftarMakulDipesan()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakulDiPesan = new SqlCommand(@"
                    SELECT        bak_krs_penawaran.no, bak_krs_penawaran.npm, bak_penawaran_makul.no_penawaran, bak_penawaran_makul.kode_makul, bak_makul.makul, bak_makul.id_prog_study,bak_makul.sks, 
                                             bak_penawaran_makul.semester
                    FROM            bak_krs_penawaran INNER JOIN
                                             bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran INNER JOIN
                                             bak_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                    WHERE   (bak_krs_penawaran.npm=@npm) AND (bak_penawaran_makul.semester=@semester) ", con);

                    CmdMakulDiPesan.CommandType = System.Data.CommandType.Text;

                    CmdMakulDiPesan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdMakulDiPesan.Parameters.AddWithValue("@semester", _Tahun + _Semester);

                    DataTable TableMakulDipesan = new DataTable();
                    TableMakulDipesan.Columns.Add("Kode Mata Kuliah");
                    TableMakulDipesan.Columns.Add("Mata Kuliah");
                    TableMakulDipesan.Columns.Add("SKS");

                    using (SqlDataReader rdr = CmdMakulDiPesan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelListMakulDipesan.Enabled = true;
                            this.PanelListMakulDipesan.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableMakulDipesan.NewRow();

                                datarow["Kode Mata Kuliah"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                TableMakulDipesan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvListMakulDipesan.DataSource = TableMakulDipesan;
                            this.GvListMakulDipesan.DataBind();
                        }
                        else
                        {
                            this.PanelListMakulDipesan.Enabled = false;
                            this.PanelListMakulDipesan.Visible = false;

                            //clear Gridview
                            TableMakulDipesan.Rows.Clear();
                            TableMakulDipesan.Clear();
                            GvListMakulDipesan.DataSource = TableMakulDipesan;
                            GvListMakulDipesan.DataBind();

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('TIDAK ADA PEMESANAN DI SEMESTER INI');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected string CekPembayaran()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand(@"
                    DECLARE @NoNpmCekBayar VARCHAR(12)
                    DECLARE @SemMulaiMhs VARCHAR(5)
                    DECLARE @Bdk INT
                    SELECT @NoNpmCekBayar=npm,@SemMulaiMhs=smster_mulai,@Bdk=bdk FROM dbo.bak_mahasiswa WHERE npm=@NpmCekBayar

                    IF @SemMulaiMhs IS NULL 
                    BEGIN
	                    RAISERROR('PERBAIKI DATA SEMESTER MASUK, HUBUNGI BAKPK',16,10)
	                    RETURN
                    END
                    ELSE
                    BEGIN
	                    IF @SemMulaiMhs >=20151
	                    BEGIN
		                    IF (@Bdk=1 OR @Bdk=2 OR @Bdk=3)
		                    BEGIN
			                    PRINT 'Mahasiswa Bidikmisi'
		                    END
		                    ELSE	
		                    BEGIN
			                    DECLARE @CekAktivasi VARCHAR(12)
			                    SELECT @CekAktivasi=status FROM ukt.dbo.keu_posting_bank WHERE (payeeId= @NpmCekBayar) AND (billRef4=@SemCekBayar) 
			                    IF @CekAktivasi IS NULL
			                    BEGIN
				                    RAISERROR('ANDA TERCATAT BELUM MELAKUKAN AKTIVASI PEMBAYARAN',16,10)
				                    RETURN
			                    END
			                    ELSE
			                    BEGIN
				                    DECLARE @NoBilingPembayaran VARCHAR(12)
				                    SELECT @NoBilingPembayaran=billingNo FROM ukt.dbo.keu_posting_bank WHERE (payeeId= @NpmCekBayar) AND (status ='unpaid')
				                    IF @@ROWCOUNT >=1
				                    BEGIN
					                    RAISERROR('ANDA TERCATAT BELUM MELUNASI TAGIHAN',16,10)
					                    RETURN
				                    END
				                    ELSE
				                    BEGIN
					                    DECLARE @NoBilingPembayaran2 VARCHAR(12)
					                    SELECT @NoBilingPembayaran2=billingNo FROM ukt.dbo.keu_posting_bank WHERE (billRef4=@SemCekBayar) AND (cicilan IS NULL OR cicilan ='1') AND (payeeId=@NpmCekBayar) AND (status='unpaid')
					                    IF @@ROWCOUNT >= 1
					                    BEGIN
						                    RAISERROR('ANDA TERCATAT BELUM MELUNASI TAGIHAN SEMESTER INI',16,10)
						                    RETURN
					                    END
				                    END
			                    END
		                    END
	                    END
	                    ELSE
	                    BEGIN
		                    PRINT 'MAHASISWA POLA PEMBAYARAN YAYASAN'
	                    END
                    END ", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@NpmCekBayar", this.Session["Name"].ToString());
                    CmdJadwal.Parameters.AddWithValue("@SemCekBayar", _Tahun + _Semester);

                    CmdJadwal.ExecuteNonQuery();

                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected string CekMasaKeg()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // ------ Cek Masa KRS -------
                    SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                    //CmdCekMasa.Transaction = trans;
                    CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekMasa.Parameters.AddWithValue("@semester", _Tahun + _Semester);
                    CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "PenawaranMakul");
                    CmdCekMasa.Parameters.AddWithValue("@jenjang", this.Session["jenjang"].ToString());

                    SqlParameter Status = new SqlParameter();
                    Status.ParameterName = "@output";
                    Status.SqlDbType = System.Data.SqlDbType.VarChar;
                    Status.Size = 20;
                    Status.Direction = System.Data.ParameterDirection.Output;
                    CmdCekMasa.Parameters.Add(Status);

                    CmdCekMasa.ExecuteNonQuery();

                    if (Status.Value.ToString() == "OUT")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('TIDAK ADA JADWAL PENAWARAN');", true);
                        return "TIDAK ADA JADWAL PEMESANAN MATA KULIAH";
                    }
                    else
                    {
                        return "ok";
                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(" + ex.Message.ToString() + "');", true);
                return ex.Message;
            }
        }

        protected void PopulateMakulDitawarkan()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakulDitawarkan = new SqlCommand(@"
                        DECLARE @Smster_Mhs varchar(5)
                        DECLARE @NoMhs varchar(20)
                        SELECT @NoMhs=npm, @Smster_Mhs=smster_mulai FROM bak_mahasiswa WHERE npm = @npm_tawar

                        IF (@Smster_Mhs IS NULL)
                        BEGIN
	                        RAISERROR('ERROR, DATA SEMESTER MASUK MAHASISWA TIDAK TERDATAR !!!!!',16,10)
	                        RETURN
                        END

                        SELECT        bak_penawaran_makul.no_penawaran, bak_penawaran_makul.kode_makul, bak_makul.makul, bak_makul.id_prog_study, bak_kurikulum.semester_mulai, bak_kurikulum.semester_selesai, bak_makul.sks,
                                                 bak_penawaran_makul.semester
                        FROM            bak_penawaran_makul INNER JOIN
                                                 bak_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul INNER JOIN
                                                 bak_kurikulum ON bak_makul.id_kurikulum = bak_kurikulum.id_kurikulum
                        WHERE        (bak_kurikulum.semester_mulai <= @Smster_Mhs) AND (bak_kurikulum.semester_selesai >= @Smster_Mhs) AND (bak_makul.id_prog_study = @IdProdi_tawar) AND (bak_penawaran_makul.semester=@semester_tawar)                       
                    ", con);
                    CmdMakulDitawarkan.CommandType = System.Data.CommandType.Text;

                    CmdMakulDitawarkan.Parameters.AddWithValue("@npm_tawar",this.Session["Name"].ToString());
                    CmdMakulDitawarkan.Parameters.AddWithValue("@semester_tawar", _Tahun + _Semester);
                    CmdMakulDitawarkan.Parameters.AddWithValue("@IdProdi_tawar", this.Session["prodi"].ToString());

                    DataTable TableMakulDitawarkan = new DataTable();
                    TableMakulDitawarkan.Columns.Add("No");
                    TableMakulDitawarkan.Columns.Add("Kode");
                    TableMakulDitawarkan.Columns.Add("Mata Kuliah");
                    TableMakulDitawarkan.Columns.Add("SKS");

                    using (SqlDataReader rdr = CmdMakulDitawarkan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            if (_MaxKRS > 0)
                            {
                                this.PanelMsg.Enabled = true;
                                this.PanelMsg.Visible = true;

                                this.LbMaxSKS.Text = _MaxKRS.ToString().Trim();
                            } else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ERROR DATA IP SEMESTER');", true);
                                return;
                            }

                            this.PanelMakulDitawarkan.Enabled = true;
                            this.PanelMakulDitawarkan.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableMakulDitawarkan.NewRow();

                                datarow["No"] = rdr["no_penawaran"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                TableMakulDitawarkan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvMakulDitawarkan.DataSource = TableMakulDitawarkan;
                            this.GvMakulDitawarkan.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableMakulDitawarkan.Rows.Clear();
                            TableMakulDitawarkan.Clear();
                            GvMakulDitawarkan.DataSource = TableMakulDitawarkan;
                            GvMakulDitawarkan.DataBind();

                            this.PanelMakulDitawarkan.Enabled = false;
                            this.PanelMakulDitawarkan.Visible = false;

                            this.PanelMakulDipesan.Enabled = false;
                            this.PanelMakulDipesan.Visible = false;

                            this.PanelMsg.Enabled = false;
                            this.PanelMsg.Visible = false;

                            this.LbSemester.Text = "";
                            this.LbMaxSKS.Text = "";

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('MATA KULIAH BELUM DI TAWARKAN, HUBUNGI PRODI !');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void PopulateMakulDipesan()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakulDipesan = new SqlCommand(@"
                    SELECT        bak_krs_penawaran.no, bak_krs_penawaran.npm, bak_penawaran_makul.no_penawaran, bak_penawaran_makul.kode_makul, bak_makul.makul, bak_makul.id_prog_study,bak_makul.sks, 
                                             bak_penawaran_makul.semester
                    FROM            bak_krs_penawaran INNER JOIN
                                             bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran INNER JOIN
                                             bak_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                    WHERE   (bak_krs_penawaran.npm=@npm) AND (bak_penawaran_makul.semester=@semester) ", con);

                    CmdMakulDipesan.CommandType = System.Data.CommandType.Text;

                    CmdMakulDipesan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdMakulDipesan.Parameters.AddWithValue("@semester", _Tahun + _Semester);

                    DataTable TableMakulDipesan = new DataTable();
                    TableMakulDipesan.Columns.Add("No Krs");
                    TableMakulDipesan.Columns.Add("Kode");
                    TableMakulDipesan.Columns.Add("Mata Kuliah");
                    TableMakulDipesan.Columns.Add("SKS");

                    using (SqlDataReader rdr = CmdMakulDipesan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelMakulDipesan.Enabled = true;
                            this.PanelMakulDipesan.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableMakulDipesan.NewRow();
                                datarow["No Krs"] = rdr["no"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                TableMakulDipesan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvMakulDiPesan.DataSource = TableMakulDipesan;
                            this.GvMakulDiPesan.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableMakulDipesan.Rows.Clear();
                            TableMakulDipesan.Clear();
                            GvMakulDiPesan.DataSource = TableMakulDipesan;
                            GvMakulDiPesan.DataBind();

                            this.PanelMakulDipesan.Enabled = false;
                            this.PanelMakulDipesan.Visible = false;

                            this.LbSemester.Text = "";

                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Belum Memesan Mata Kuliah, Silahkan Pesan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void HitungMaxSks()
        {
            if (_Tahun == string.Empty)
            {
                _Tahun = this.DLTahun.SelectedValue.Trim();
                _Semester = this.DLSemester.SelectedValue.Trim();
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    _PrevIPS = Convert.ToDecimal(0);
                    _MaxKRS = 0;

                    SqlCommand CmdCekIPS = new SqlCommand("SpGetPrevIPS", con);
                    //CmdCekMasa.Transaction = trans;
                    CmdCekIPS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekIPS.Parameters.AddWithValue("@ThisSemester", _Tahun+_Semester);
                    CmdCekIPS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                    SqlParameter CekIPS = new SqlParameter();
                    CekIPS.ParameterName = "@IPS";
                    CekIPS.SqlDbType = System.Data.SqlDbType.Decimal;
                    CekIPS.Precision = 4; // jumlah angka decimal
                    CekIPS.Scale = 2; // jumlah digit setelah koma
                    CekIPS.Direction = System.Data.ParameterDirection.Output;
                    CmdCekIPS.Parameters.Add(CekIPS);

                    CmdCekIPS.ExecuteNonQuery();

                    _PrevIPS = Convert.ToDecimal(CekIPS.Value);

                    if (_PrevIPS < Convert.ToDecimal(2.00))
                    {
                        _MaxKRS = 18;
                    }
                    else if ((_PrevIPS >= Convert.ToDecimal(2.00)) && (_PrevIPS < Convert.ToDecimal(2.50)))
                    {
                        _MaxKRS = 20;
                    }
                    else if ((_PrevIPS >= Convert.ToDecimal(2.50)) && (_PrevIPS < Convert.ToDecimal(3.00)))
                    {
                        _MaxKRS = 22;
                    }
                    else if (_PrevIPS >= Convert.ToDecimal(3.00))
                    {
                        _MaxKRS = 24;
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void Btn_Pesan_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Int32 no_penawaran = Convert.ToInt32(this.GvMakulDitawarkan.Rows[index].Cells[1].Text.Trim());
            string Kode = this.GvMakulDitawarkan.Rows[index].Cells[2].Text.ToString().Trim();
            int SKS = Convert.ToInt32(this.GvMakulDitawarkan.Rows[index].Cells[4].Text.Trim());

            //string mess = "alert('" + Kode + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    // --------- CEK MAKSIMAL JUMLAH SKS ----------- //
                    int SksSudahDipesan = 0;

                    SqlCommand CmdMakulDipesan = new SqlCommand(@"
                    SELECT        SUM(sks) AS total
                    FROM            bak_krs_penawaran INNER JOIN
                                                bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran INNER JOIN
                                                bak_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                    WHERE   (bak_krs_penawaran.npm=@npm) AND (bak_penawaran_makul.semester=@semester) ", con);

                    CmdMakulDipesan.CommandType = System.Data.CommandType.Text;

                    CmdMakulDipesan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdMakulDipesan.Parameters.AddWithValue("@semester", _Tahun + _Semester);

                    DataTable TableMakulDipesan = new DataTable();
                    TableMakulDipesan.Columns.Add("No Krs");
                    TableMakulDipesan.Columns.Add("Kode");
                    TableMakulDipesan.Columns.Add("Mata Kuliah");
                    TableMakulDipesan.Columns.Add("SKS");

                    using (SqlDataReader rdr = CmdMakulDipesan.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelMakulDipesan.Enabled = true;
                            this.PanelMakulDipesan.Visible = true;

                            while (rdr.Read())
                            {
                                if (rdr["total"] != DBNull.Value)
                                {
                                    SksSudahDipesan = Convert.ToInt16(rdr["total"]);
                                }
                                else
                                {
                                    SksSudahDipesan = 0;
                                }
                            }
                        }
                        else
                        {
                            con.Close();
                            con.Dispose();

                            string msg = "alert('Data Jumlah SKS Sudah Dipesan Tidak Ditemukan')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                            return;
                        }
                    }

                    if (SKS + SksSudahDipesan > _MaxKRS)
                    {
                        con.Close();
                        con.Dispose();

                        string mes = "alert('Melebihi Batas Jumlah Pengambilan SKS')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mes, true);
                        return;
                    }

                    // --------- INPUT PESAN KRS -------- //
                    SqlCommand CmdPesanMakul = new SqlCommand(@"
                    DECLARE @NoPenawaran BIGINT
                    DECLARE @KodeMakul VARCHAR(12)

                    SELECT        @NoPenawaran=bak_penawaran_makul.no_penawaran, @KodeMakul=bak_penawaran_makul.kode_makul
                    FROM            bak_penawaran_makul INNER JOIN
                                             bak_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                    WHERE        (bak_penawaran_makul.semester = @semester) AND (bak_penawaran_makul.kode_makul = @kode_makul) AND (bak_penawaran_makul.no_penawaran = @no_penawaran)
                    IF @@ROWCOUNT = 1
                    BEGIN
	                    DECLARE @NoKrsPenawaran BIGINT
	                    SELECT @NoKrsPenawaran=no FROM bak_krs_penawaran WHERE npm=@npm AND no_penawaran=@NoPenawaran
	                    IF (@@ROWCOUNT = 0)
	                    BEGIN
		                    INSERT INTO dbo.bak_krs_penawaran
				                    ( npm, no_penawaran, tgl_update )
		                    VALUES  ( @npm, -- npm - nvarchar(10)
				                      @NoPenawaran, -- no_penawaran - bigint
				                      GETDATE()  -- tgl_update - datetime
				                      )
	                    END
	                    ELSE
	                    BEGIN
		                    RAISERROR('ERROR, MATA KULIAH SUDAH DIPESAN !',16,10)
		                    RETURN
	                    END
                    END
                    ELSE
                    BEGIN
	                    RAISERROR('DATA MAKUL ERROR !!!!!',16,10)
	                    RETURN
                    END                       
                    ", con);
                    CmdPesanMakul.CommandType = System.Data.CommandType.Text;

                    CmdPesanMakul.Parameters.AddWithValue("@npm", this.Session["Name"].ToString().Trim());
                    CmdPesanMakul.Parameters.AddWithValue("@semester", _Tahun + _Semester);
                    CmdPesanMakul.Parameters.AddWithValue("@kode_makul", Kode);
                    CmdPesanMakul.Parameters.AddWithValue("@no_penawaran", no_penawaran);

                    CmdPesanMakul.ExecuteNonQuery();

                    PopulateMakulDipesan();

                    string mess = "alert('PEMESANAN MAKUL BERHASIL :-)')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);
                }
            }
            catch (Exception ex)
            {
                //this.PanelMakulDipesan.Enabled = false;
                //this.PanelMakulDipesan.Visible = false;

                string mess = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);
                return;
            }
        }

        protected void BtnHapus_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Int32 no_krs_penawaran = Convert.ToInt32(this.GvMakulDiPesan.Rows[index].Cells[1].Text.Trim());
            string kode_makul = this.GvMakulDiPesan.Rows[index].Cells[2].Text.Trim();

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //-------------------------------------------
                    con.Open();

                    // --------- INPUT PESAN KRS -------- //
                    SqlCommand CmdHapusPemesanan = new SqlCommand(@"
                    DELETE FROM bak_krs_penawaran WHERE no = (
                    SELECT        bak_krs_penawaran.no
                    FROM            bak_krs_penawaran INNER JOIN
                                             bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran
                    WHERE bak_krs_penawaran.npm = @npm AND bak_penawaran_makul.kode_makul = @kode_makul AND semester = @semester AND bak_krs_penawaran.no = @no_krs
                    ) ", con);

                    CmdHapusPemesanan.CommandType = System.Data.CommandType.Text;

                    CmdHapusPemesanan.Parameters.AddWithValue("@npm", this.Session["Name"].ToString().Trim());
                    CmdHapusPemesanan.Parameters.AddWithValue("@semester", _Tahun + _Semester);
                    CmdHapusPemesanan.Parameters.AddWithValue("@kode_makul", kode_makul);
                    CmdHapusPemesanan.Parameters.AddWithValue("@no_krs", no_krs_penawaran);

                    CmdHapusPemesanan.ExecuteNonQuery();

                    PopulateMakulDipesan();

                    string mess = "alert('MATA KULIA BERHASIL DIHAPUS :-)')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);
                }
            }
            catch (Exception ex)
            {
                string mess = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);
                return;
            }
        }

        protected void GvMakulDitawarkan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //No Penawaran
        }

        protected void GvMakulDiPesan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //No KRS Penawaran
        }

    }
}