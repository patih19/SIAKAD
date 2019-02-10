using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class Penawaran : Tu
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TahunAkademik();

                this.PanelMakul.Visible = false;
                this.PanelMakul.Enabled = false;

                this.PanelMakulDitawarkan.Visible = false;
                this.PanelMakulDitawarkan.Enabled = false;

                _Tahun = "";
                _Semester = "";
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

                    SqlCommand CmdJadwal = new SqlCommand("select TOP 2 bak_kal.thn+1 AS thn, CAST(bak_kal.thn+1 AS VARCHAR(50)) + '/' +CAST(bak_kal.thn +2 AS VARCHAR(50) ) AS ThnAkm  FROM bak_kal WHERE jenjang IN ('S1') group by thn ORDER BY thn DESC", con);
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

            string CekSmsPenawaran = "";
            CekSmsPenawaran = CekSemesterPenawaran();
            if (CekSmsPenawaran != "ok")
            {
                this.PanelMakul.Visible = false;
                this.PanelMakul.Enabled = false;

                this.PanelMakulDitawarkan.Visible = false;
                this.PanelMakulDitawarkan.Enabled = false;

                string message = "alert('" + CekSmsPenawaran + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            this.PanelMakul.Visible = true;
            this.PanelMakul.Enabled = true;

            PopulateMakul();
            PopulatePenawaran();
        }

        protected string CekSemesterPenawaran()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //------------------------------------------------------------------------------------
                con.Open();
                try
                {
                    SqlCommand CmdCekSemesterPenawaran = new SqlCommand(@"
                    -- ===== SEMESTER PENAWARAN LOMPAT ===== --
                    DECLARE @TopSem VARCHAR(5)
                    DECLARE @TopNoJadwal BIGINT	
                    SELECT    TOP 1    @TopNoJadwal=bak_jadwal.no_jadwal, @TopSem=bak_jadwal.semester
                    FROM            bak_jadwal INNER JOIN
                                             bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal
                    WHERE	id_prog_study=@IdProdiSmstrPenawaran ORDER BY semester DESC

                    DECLARE @CrntThn VARCHAR(4) = LEFT(@TopSem, 4) 
                    DECLARE @CrntDigit VARCHAR(1) = RIGHT(@TopSem, 1) 
                    DECLARE @MaxThn INT = CAST(@CrntThn AS INT) + 1 
                    DECLARE @MaxSemester int = CAST(CAST(@MaxThn AS VARCHAR(5))+ @CrntDigit as int) 

                    IF( @SemCekPenawaran >= @MaxSemester)
                    BEGIN
	                    RAISERROR('PENAWARAN PADA SEMESTER TERSEBUT BELUM DIBUKA !!',16,10)
	                    RETURN
                    END

                    -- ====  CEK KRS TERAKHIR ====
                    DECLARE @NoJadwal BIGINT
                    SELECT    TOP 1    @NoJadwal=bak_jadwal.no_jadwal
                    FROM            bak_jadwal INNER JOIN
                                             bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal
                    WHERE bak_jadwal.semester=@SemCekPenawaran AND id_prog_study=@IdProdiSmstrPenawaran
                    IF @@ROWCOUNT >= 1
                    BEGIN
	                    RAISERROR('KRS PADA SEMESTER INI SUDAH TERLAKSANA !!',16,10)
	                    RETURN
                    END ", con);

                    CmdCekSemesterPenawaran.CommandType = System.Data.CommandType.Text;

                    CmdCekSemesterPenawaran.Parameters.AddWithValue("@SemCekPenawaran", _Tahun + _Semester);
                    CmdCekSemesterPenawaran.Parameters.AddWithValue("@IdProdiSmstrPenawaran", this.Session["level"].ToString().Trim());

                    CmdCekSemesterPenawaran.ExecuteNonQuery();

                    return "ok";
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    return ex.Message.ToString().Trim();
                }
            }
        }

        protected void PopulateMakul()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakul = new SqlCommand(@"
                            SELECT  bak_makul.no, bak_makul.id_prog_study, bak_makul.kode_makul, bak_makul.makul, bak_makul.sks, bak_makul.kurikulum, bak_makul.aktif, bak_makul.semester
                            FROM            bak_kurikulum INNER JOIN
                                                     bak_makul ON bak_kurikulum.id_kurikulum = bak_makul.id_kurikulum
                            WHERE        (bak_makul.id_prog_study = @IdProdi)
                            ORDER BY bak_makul.kurikulum                 
                    ", con);
                    CmdMakul.CommandType = System.Data.CommandType.Text;

                    CmdMakul.Parameters.AddWithValue("@IdProdi", this.Session["level"].ToString().Trim());

                    DataTable TableMakul = new DataTable();
                    TableMakul.Columns.Add("No Makul");
                    TableMakul.Columns.Add("Kode");
                    TableMakul.Columns.Add("Mata Kuliah");
                    TableMakul.Columns.Add("SKS");
                    TableMakul.Columns.Add("Kurikulum");

                    using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableMakul.NewRow();

                                datarow["No Makul"] = rdr["no"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Kurikulum"] = rdr["kurikulum"];

                                TableMakul.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvMakul.DataSource = TableMakul;
                            this.GvMakul.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableMakul.Rows.Clear();
                            TableMakul.Clear();
                            GvMakul.DataSource = TableMakul;
                            GvMakul.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void PopulatePenawaran()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakul = new SqlCommand(@"
                        SELECT        bak_penawaran_makul.no_penawaran,bak_penawaran_makul.kode_makul, bak_makul.makul, bak_makul.sks, bak_penawaran_makul.tgl_update, bak_penawaran_makul.semester, bak_makul.id_prog_study
                        FROM            bak_makul INNER JOIN
                                                 bak_penawaran_makul ON bak_makul.kode_makul = bak_penawaran_makul.kode_makul
                        WHERE        (bak_makul.id_prog_study = @IdProdi) AND (dbo.bak_penawaran_makul.semester=@semester) ", con);
                    CmdMakul.CommandType = System.Data.CommandType.Text;

                    CmdMakul.Parameters.AddWithValue("@IdProdi", this.Session["level"].ToString().Trim());
                    CmdMakul.Parameters.AddWithValue("@semester", _Tahun+_Semester);

                    DataTable TablPenawaran = new DataTable();
                    TablPenawaran.Columns.Add("Nomor");
                    TablPenawaran.Columns.Add("Kode");
                    TablPenawaran.Columns.Add("Mata Kuliah");
                    TablPenawaran.Columns.Add("SKS");
                    TablPenawaran.Columns.Add("Semester");

                    using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelMakulDitawarkan.Visible = true;
                            this.PanelMakulDitawarkan.Enabled = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TablPenawaran.NewRow();

                                datarow["Nomor"] = rdr["no_penawaran"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Semester"] = rdr["semester"];

                                TablPenawaran.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVPenawaran.DataSource = TablPenawaran;
                            this.GVPenawaran.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TablPenawaran.Rows.Clear();
                            TablPenawaran.Clear();
                            GVPenawaran.DataSource = TablPenawaran;
                            GVPenawaran.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GvMakul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           e.Row.Cells[1].Visible = false; //hide No Jadwal
        }

        protected void BtnPenawaran_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Int32 no_makul = Convert.ToInt32(this.GvMakul.Rows[index].Cells[1].Text.Trim());
            string Kode = this.GvMakul.Rows[index].Cells[2].Text.Trim();

            //string mess = "alert('" + no_makul + "')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", mess, true);

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdMakul = new SqlCommand(@"

                    DECLARE @KdMakul VARCHAR(12)
                    DECLARE @NoMakul BIGINT

                    SELECT        @NoMakul=no, @KdMakul=kode_makul
                    FROM            bak_makul
                    WHERE no=@No_makul AND kode_makul=@kode_makul AND id_prog_study=@IdProdi

                    IF @@ROWCOUNT = 1
                    BEGIN
	                    DECLARE @NoPenawaran BIGINT
       
	                    SELECT no_penawaran FROM bak_penawaran_makul WHERE kode_makul=@kode_makul AND semester=@Semester
	                    IF @@ROWCOUNT >=1
	                    BEGIN
		                    RAISERROR('MATA KULIAH SUDAH DITAWARKAN ...',16,10)
		                    RETURN
	                    END
	                    ELSE
	                    BEGIN
		                    INSERT INTO dbo.bak_penawaran_makul
		                            ( kode_makul, semester, tgl_update )
		                    VALUES  ( @KdMakul, -- kode_makul - nvarchar(12)
		                              @Semester, -- semester - varchar(5)
		                              GETDATE()  -- tgl_update - datetime
		                              )
	                    END
                    END
                    ELSE
                    BEGIN
	                    RAISERROR('MAKUL ERROR !!!!!',16,10)
	                    RETURN
                    END ", con);

                    CmdMakul.Parameters.AddWithValue("@kode_makul", Kode);
                    CmdMakul.Parameters.AddWithValue("@No_makul", no_makul);
                    CmdMakul.Parameters.AddWithValue("@IdProdi", this.Session["level"].ToString().Trim());
                    CmdMakul.Parameters.AddWithValue("@Semester", _Tahun + _Semester);

                    CmdMakul.ExecuteNonQuery();

                    PopulatePenawaran();

                    string message = "alert('Input Berhasil :-)')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message + "')";
                string x= message.Replace("\\", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", x, true);
            }
        }

        protected void GvMakul_PreRender(object sender, EventArgs e)
        {
            if (this.GvMakul.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GvMakul.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GvMakul.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void BtnHapusPenawaran_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(500);

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            string Kode = this.GVPenawaran.Rows[index].Cells[2].Text.Trim();
            Int32 NoPenawaran = Convert.ToInt32(this.GVPenawaran.Rows[index].Cells[1].Text.Trim());

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //------------------------------------------------------------------------------------
                con.Open();
                try
                {
                    SqlCommand CmdCekSemesterPenawaran = new SqlCommand(@"
                    DECLARE @NoPenawaranDel BIGINT
                    SELECT  @NoPenawaranDel = bak_penawaran_makul.no_penawaran
                    FROM            bak_krs_penawaran INNER JOIN
                                             bak_penawaran_makul ON bak_krs_penawaran.no_penawaran = bak_penawaran_makul.no_penawaran INNER JOIN
                                             bak_makul ON bak_penawaran_makul.kode_makul = bak_makul.kode_makul
                    WHERE bak_penawaran_makul.no_penawaran = @NoPenawaran AND bak_makul.id_prog_study = @IdProdi AND bak_penawaran_makul.kode_makul = @KodeMakul
                    IF(@@ROWCOUNT = 0)
                    BEGIN
                        DELETE FROM bak_penawaran_makul WHERE no_penawaran = @NoPenawaran AND kode_makul = @KodeMakul
                    END
                    ELSE
                    BEGIN
                        RAISERROR('MAHASISWA SUDAH MEMESAN MATA KULIAH INI !!!', 16, 10)
                        RETURN
                    END ", con);

                    CmdCekSemesterPenawaran.CommandType = System.Data.CommandType.Text;

                    CmdCekSemesterPenawaran.Parameters.AddWithValue("@NoPenawaran", NoPenawaran);
                    CmdCekSemesterPenawaran.Parameters.AddWithValue("@IdProdi", this.Session["level"].ToString().Trim());
                    CmdCekSemesterPenawaran.Parameters.AddWithValue("@KodeMakul", Kode);

                    CmdCekSemesterPenawaran.ExecuteNonQuery();

                    PopulatePenawaran();

                    string message = "alert('HAPUS BERHASIL')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();

                    string message = "alert('"+ ex.Message +"')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
        }

        protected void GVPenawaran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //hide No Jadwal
        }
    }
}