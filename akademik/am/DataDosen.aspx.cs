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
    public partial class DataDosen : System.Web.UI.Page
    {
        public Int32 _TotalDosen
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                _TotalDosen = 0;

                PopulateRekapDosen();
            }
        }

        protected void PopulateDosen ()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("" +
                        "SELECT     bak_dosen.no, bak_dosen.nama, bak_dosen.nidn, bak_dosen.nik, bak_dosen.nip, bak_dosen.pangkat, bak_dosen.jabatan, bak_dosen.tmlahir, bak_dosen.tglahir, bak_dosen.pendidikan, " +
                                              "bak_dosen.prodi, bak_dosen.hp, bak_dosen.alamat, bak_dosen.aktif, bak_prog_study.prog_study " +
                        "FROM         bak_dosen INNER JOIN " +
                                              "bak_prog_study ON bak_dosen.prodi = bak_prog_study.id_prog_study " +
                        "WHERE       prodi = @prodi AND aktif = 'yes' AND tim = '0' ORDER BY nama ASC" +
                        "", con);
                    CmdDosen.CommandType = System.Data.CommandType.Text;

                    CmdDosen.Parameters.AddWithValue("@prodi", this.Session["level"].ToString());

                    DataTable TableDosen = new DataTable();
                    TableDosen.Columns.Add("NIDN/NUP/NIDK");
                    TableDosen.Columns.Add("NAMA");
                    TableDosen.Columns.Add("AKTIF");

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableDosen.NewRow();
                                datarow["NIDN/NUP/NIDK"] = rdr["nidn"];
                                datarow["NAMA"] = rdr["nama"];
                                datarow["AKTIF"] = rdr["aktif"];

                                TableDosen.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvRekapDosen.DataSource = TableDosen;
                            this.GvRekapDosen.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableDosen.Rows.Clear();
                            TableDosen.Clear();
                            GvRekapDosen.DataSource = TableDosen;
                            GvRekapDosen.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void PopulateRekapDosen()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("" +
                        "SELECT        bak_dosen.prodi, bak_prog_study.jenjang, bak_prog_study.prog_study, COUNT(*) AS jumlah "+
                        "FROM            bak_dosen INNER JOIN "+
                                                 "bak_prog_study ON bak_dosen.prodi = bak_prog_study.id_prog_study "+
                        "WHERE aktif = 'yes' AND tim = '0' "+
                        "GROUP BY  bak_dosen.prodi, bak_prog_study.jenjang, bak_prog_study.prog_study "+
                        "ORDER BY jenjang, prog_study asc" +
                        "", con);
                    CmdDosen.CommandType = System.Data.CommandType.Text;

                    CmdDosen.Parameters.AddWithValue("@prodi", this.Session["level"].ToString());

                    DataTable TableDosen = new DataTable();
                    TableDosen.Columns.Add("KODE");
                    TableDosen.Columns.Add("PROGRAM STUDI");
                    TableDosen.Columns.Add("JUMLAH");

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableDosen.NewRow();
                                datarow["KODE"] = rdr["prodi"];
                                datarow["PROGRAM STUDI"] = rdr["prog_study"];
                                datarow["JUMLAH"] = rdr["jumlah"];

                                TableDosen.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvRekapDosen.DataSource = TableDosen;
                            this.GvRekapDosen.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableDosen.Rows.Clear();
                            TableDosen.Clear();
                            GvRekapDosen.DataSource = TableDosen;
                            GvRekapDosen.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void GvRekapDosen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 SKS = Convert.ToInt32(e.Row.Cells[2].Text);
                _TotalDosen += SKS;

                // this._TotalSkripsi = TotalSKS;
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                //e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "JUMLAH";

                e.Row.Cells[2].Text = _TotalDosen.ToString();

                //string FormattedString4 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTotalSKS);
                //e.Row.Cells[1].Text = FormattedString4;
            }
        }
    }
}