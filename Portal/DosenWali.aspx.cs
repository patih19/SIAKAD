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
    public partial class DosenWali : Tu
    {
        public String _NIDN
        {
            get { return this.ViewState["nidn"].ToString(); }
            set { this.ViewState["nidn"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelDaftarMhs.Visible = false;
                this.PanelDaftarMhs.Enabled = false;

                this.PanelMhs.Enabled = false;
                this.PanelMhs.Visible = false;

                PopulateDosenProdi();                
            }
        }

        protected void PopulateDosenProdi()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand("SpGetDosen", con);
                CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());

                DataTable TableDosen = new DataTable();
                TableDosen.Columns.Add("NIDN");
                TableDosen.Columns.Add("Nama");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableDosen.NewRow();
                            datarow["NIDN"] = rdr["nidn"];
                            datarow["Nama"] = rdr["nama"];

                            TableDosen.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVDosenAdd.DataSource = TableDosen;
                        this.GVDosenAdd.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableDosen.Rows.Clear();
                        TableDosen.Clear();
                        GVDosenAdd.DataSource = TableDosen;
                        GVDosenAdd.DataBind();
                    }
                }
            }
        }

        protected void PopulateMhsAktif(string nidn)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand(   "SELECT        bak_mahasiswa.id_wali, bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status "+
                                                        "FROM            bak_dosen RIGHT OUTER JOIN "+
                                                                                 "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali "+
                                                        "WHERE bak_mahasiswa.status = 'A' AND bak_mahasiswa.id_prog_study = @id_prodi AND(id_wali in "+
                                                        "( "+
                                                            "SELECT        id_wali "+
                                                            "FROM            bak_mahasiswa "+
                                                            "WHERE(id_wali is null OR id_wali = @nidn) "+
                                                        ") OR id_wali is null) "+
                                                        "order by thn_angkatan desc", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                CmdDosen.Parameters.AddWithValue("@nidn", nidn);

                DataTable TabelMhs = new DataTable();
                TabelMhs.Columns.Add("NPM");
                TabelMhs.Columns.Add("Nama");
                TabelMhs.Columns.Add("Tahun");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelMhs.Enabled = true;
                        this.PanelMhs.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TabelMhs.NewRow();
                            datarow["NPM"] = rdr["NPM"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["Tahun"] = rdr["thn_angkatan"];

                            TabelMhs.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GvMhsAdd.DataSource = TabelMhs;
                        this.GvMhsAdd.DataBind();
                    }
                    else
                    {
                        this.PanelMhs.Enabled = false;
                        this.PanelMhs.Visible = false;

                        //clear Gridview
                        TabelMhs.Rows.Clear();
                        TabelMhs.Clear();
                        GvMhsAdd.DataSource = TabelMhs;
                        GvMhsAdd.DataBind();
                    }
                }


            }
        }

        protected void PopulateDaftarMhs (string nidn)
        {           
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand(" SELECT        bak_dosen.nidn, bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status " +
                                                     " FROM            bak_dosen INNER JOIN " +
                                                                            "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali " +
                                                    " WHERE  bak_mahasiswa.status = 'A' AND bak_dosen.nidn = @nidn", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@nidn",nidn);

                DataTable TabelMhs = new DataTable();
                TabelMhs.Columns.Add("NPM");
                TabelMhs.Columns.Add("Nama");
                TabelMhs.Columns.Add("Tahun");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelDaftarMhs.Visible = true;
                        this.PanelDaftarMhs.Enabled = true;

                        

                        while (rdr.Read())
                        {
                            DataRow datarow = TabelMhs.NewRow();
                            datarow["NPM"] = rdr["NPM"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["Tahun"] = rdr["thn_angkatan"];

                            TabelMhs.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVPeserta.DataSource = TabelMhs;
                        this.GVPeserta.DataBind();
                    }
                    else
                    {
                        this.PanelDaftarMhs.Visible = false;
                        this.PanelDaftarMhs.Enabled = false;

                        //clear Gridview
                        TabelMhs.Rows.Clear();
                        TabelMhs.Clear();
                        GVPeserta.DataSource = TabelMhs;
                        GVPeserta.DataBind();
                    }
                }
            }
        }

        protected void CheckedMhsDibimbing (string nidn)
        {
            string npm;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                for (int i = 0; i < this.GvMhsAdd.Rows.Count; i++)
                {
                    CheckBox ch = (CheckBox)this.GvMhsAdd.Rows[i].FindControl("CbMhs");
                    npm = this.GvMhsAdd.Rows[i].Cells[1].Text.Trim();   

                    SqlCommand CmdDosen = new SqlCommand(   "SELECT        bak_mahasiswa.id_wali, bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status "+
                                                            "FROM            bak_dosen RIGHT OUTER JOIN "+
                                                                                     "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali "+
                                                            "WHERE bak_mahasiswa.status = 'A' AND bak_mahasiswa.id_prog_study = @id_prodi AND bak_mahasiswa.npm=@npm AND id_wali in " +
                                                            "( "+
                                                                "SELECT        id_wali "+
                                                                "FROM            bak_mahasiswa "+
                                                                "WHERE(id_wali is null OR id_wali = @nidn) "+
                                                            ") "+
                                                            "order by thn_angkatan desc "+
                                                            "", con);

                    CmdDosen.CommandType = System.Data.CommandType.Text;
                    CmdDosen.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdDosen.Parameters.AddWithValue("@nidn", nidn);
                    CmdDosen.Parameters.AddWithValue("@npm", npm);

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                ch.Checked = true;

                                string hex = "#f1f17e";
                                this.GvMhsAdd.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                            }
                        }
                    }
                }
            }
        }

        protected void GVDosenAdd_PreRender(object sender, EventArgs e)
        {
            if (this.GVDosenAdd.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVDosenAdd.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVDosenAdd.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void GvMhsAdd_PreRender(object sender, EventArgs e)
        {
            if (this.GvMhsAdd.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GvMhsAdd.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GvMhsAdd.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void CbDosen_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            _NIDN = this.GVDosenAdd.Rows[index].Cells[1].Text.Trim();
            this.LbNmDosen.Text = this.GVDosenAdd.Rows[index].Cells[2].Text.Trim();

            // Clear selected checkbox
            for (int i = 0; i < this.GVDosenAdd.Rows.Count; i++)
            {
                CheckBox ch = (CheckBox)this.GVDosenAdd.Rows[i].FindControl("CbDosen");
                ch.Checked = false;
            }

            // Select Old Checkbox 
            CheckBox CbOld = (CheckBox)this.GVDosenAdd.Rows[index].FindControl("CbDosen");
            CbOld.Checked = true;

            //Populate Mhs By Wali
            PopulateDaftarMhs(this.GVDosenAdd.Rows[index].Cells[1].Text.Trim());

            //populate Mhs Aktif
            PopulateMhsAktif(this.GVDosenAdd.Rows[index].Cells[1].Text.Trim());

            //tandai mahasiswa 
            CheckedMhsDibimbing(this.GVDosenAdd.Rows[index].Cells[1].Text.Trim());
        }

        protected void CbMhs_CheckedChanged(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            CheckBox ch = (CheckBox)GvMhsAdd.Rows[index].FindControl("CbMhs");
            if (ch.Checked == true)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("update bak_mahasiswa SET id_wali = @nidn WHERE npm=@npm", con);
                    CmdDosen.CommandType = System.Data.CommandType.Text;

                    CmdDosen.Parameters.AddWithValue("@nidn", _NIDN);
                    CmdDosen.Parameters.AddWithValue("@npm", this.GvMhsAdd.Rows[index].Cells[1].Text.Trim());

                    CmdDosen.ExecuteNonQuery();

                    SqlCommand CmdDosen2 = new SqlCommand(@"
                    DECLARE @Jenjang VARCHAR(2)
                    DECLARE @NoNpm VARCHAR(12)
                    DECLARE @IdProdi VARCHAR(8)
                    DECLARE @TopSemester VARCHAR(5)

                    -- ===== GET TOP SEMESTER KALENDER AKADEMIK BY JENJANG =======
                    SELECT @NoNpm=bak_mahasiswa.npm, @IdProdi=bak_mahasiswa.id_prog_study, @Jenjang=bak_prog_study.jenjang
                    FROM            bak_mahasiswa INNER JOIN
                                    bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study
                    WHERE npm=@npm

                    IF (@Jenjang ='S1' OR @Jenjang='D3')
                    BEGIN
	                    SELECT TOP 1 @TopSemester=semester FROM dbo.bak_kal 
	                    WHERE jenjang='S1' AND semester != 'new'
	                    GROUP BY semester,jenjang
	                    ORDER BY semester DESC
                    END

                    UPDATE bak_persetujuan_krs SET nidn=@nidn where semester=@TopSemester AND npm=@npm
                        ", con);
                    CmdDosen2.CommandType = System.Data.CommandType.Text;

                    CmdDosen2.Parameters.AddWithValue("@nidn", _NIDN);
                    CmdDosen2.Parameters.AddWithValue("@npm", this.GvMhsAdd.Rows[index].Cells[1].Text.Trim());

                    CmdDosen2.ExecuteNonQuery();
                }

                //refresh data mahasiswa aktif
                PopulateMhsAktif(_NIDN);
                
                //refresh daftar mahasiswa dan PA
                PopulateDaftarMhs(_NIDN);

                //tandai mahasiswa 
                CheckedMhsDibimbing(_NIDN);

                // checked 
                ch.Checked = true;
                ch.Dispose();
            }
            else
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    try
                    {
                        SqlCommand CmdGantiDosenPA = new SqlCommand(@"
                        DECLARE @Jenjang VARCHAR(2)
                        DECLARE @NoNpm VARCHAR(12)
                        DECLARE @IdProdi VARCHAR(8)
                        DECLARE @TopSemester VARCHAR(5)

                        -- ===== GET TOP SEMESTER KALENDER AKADEMIK BY JENJANG =======
                        SELECT @NoNpm=bak_mahasiswa.npm, @IdProdi=bak_mahasiswa.id_prog_study, @Jenjang=bak_prog_study.jenjang
                        FROM            bak_mahasiswa INNER JOIN
                                        bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study
                        WHERE npm=@npm

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

                        -- ======= GET PERSETUJUAN (VALIDASI) KRS OLEH DOSEN ======
                        DECLARE @no_persetujuan BIGINT
                        DECLARE @validasi INT

                        SELECT @no_persetujuan=id_persetujuan,@validasi=val FROM dbo.bak_persetujuan_krs 
                        WHERE npm=@npm AND semester=@TopSemester

                        IF (@no_persetujuan IS NOT NULL AND @validasi = 1)
                        BEGIN
	                        RAISERROR('KRS Semester Ini Sudah Divalidasi, Proses Dibatalkan ...', 16, 10)  
	                        RETURN 
                        END	
                        ELSE
                        BEGIN
	                        DELETE FROM bak_bimbingan_krs WHERE id_persetujuan=@no_persetujuan
	                        --DELETE FROM dbo.bak_persetujuan_krs WHERE npm=@npm AND semester=@TopSemester
                        END	
                        ", con);
                        CmdGantiDosenPA.CommandType = System.Data.CommandType.Text;

                        CmdGantiDosenPA.Parameters.AddWithValue("@npm", this.GvMhsAdd.Rows[index].Cells[1].Text.Trim());
                        CmdGantiDosenPA.ExecuteNonQuery();


                        SqlCommand CmdDosen = new SqlCommand("update bak_mahasiswa SET id_wali = NULL WHERE npm=@npm", con);
                        CmdDosen.CommandType = System.Data.CommandType.Text;

                        CmdDosen.Parameters.AddWithValue("@nidn", _NIDN);
                        CmdDosen.Parameters.AddWithValue("@npm", this.GvMhsAdd.Rows[index].Cells[1].Text.Trim());
                        CmdDosen.ExecuteNonQuery();

                        //refresh data mahasiswa aktif
                        PopulateMhsAktif(_NIDN);

                        //refresh daftar mahasiswa dan PA
                        PopulateDaftarMhs(_NIDN);

                        //tandai mahasiswa 
                        CheckedMhsDibimbing(_NIDN);

                        // uncheck 
                        ch.Checked = false;
                        ch.Dispose();

                    }
                    catch (Exception ex)
                    {
                        ch.Checked = true;

                        string message = "alert('" + ex.Message.ToString() + "')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
 
                }


            }
        }
    }
}