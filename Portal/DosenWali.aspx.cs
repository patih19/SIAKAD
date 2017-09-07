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
                PopulateDosenProdi();
                PopulateMhs();
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

        protected void PopulateMhs()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdDosen = new SqlCommand(   "SELECT npm, nama, thn_angkatan "+
                                                        "FROM bak_mahasiswa where id_prog_study = @id_prodi AND status = 'A' AND thn_angkatan = ( "+
                                                            "select top 1  thn_angkatan from bak_mahasiswa order by thn_angkatan desc "+
                                                           " )", con);
                CmdDosen.CommandType = System.Data.CommandType.Text;

                CmdDosen.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());

                DataTable TabelMhs = new DataTable();
                TabelMhs.Columns.Add("NPM");
                TabelMhs.Columns.Add("Nama");
                TabelMhs.Columns.Add("Tahun");

                using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
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
                        //clear Gridview
                        TabelMhs.Rows.Clear();
                        TabelMhs.Clear();
                        GvMhsAdd.DataSource = TabelMhs;
                        GvMhsAdd.DataBind();
                    }
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

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
    }
}