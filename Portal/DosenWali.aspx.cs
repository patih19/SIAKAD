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

        protected void PopulateMhs(string nidn)
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
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand(" SELECT        bak_dosen.nidn, bak_mahasiswa.npm, bak_mahasiswa.nama, bak_mahasiswa.thn_angkatan, bak_mahasiswa.status "+
                                                     " FROM            bak_dosen INNER JOIN "+
                                                                            "bak_mahasiswa ON bak_dosen.nidn = bak_mahasiswa.id_wali "+
                                                    " WHERE  bak_mahasiswa.status != 'L' AND bak_dosen.nidn = @nidn", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@nidn", this.GVDosenAdd.Rows[index].Cells[1].Text);

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

                        this.LbNmDosen.Text = this.GVDosenAdd.Rows[index].Cells[2].Text.Trim();

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

            //populate 
            PopulateMhs(this.GVDosenAdd.Rows[index].Cells[1].Text.Trim());
        }
    }
}