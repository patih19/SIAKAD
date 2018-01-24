using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    public partial class WebForm22 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_Dosen();
            }
        }

        protected void Load_Dosen()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand(""+
                        "SELECT     bak_dosen.no, bak_dosen.nama, bak_dosen.nidn, bak_dosen.nik, bak_dosen.nip, bak_dosen.pangkat, bak_dosen.jabatan, bak_dosen.tmlahir, bak_dosen.tglahir, bak_dosen.pendidikan, "+
                                              "bak_dosen.prodi, bak_dosen.hp, bak_dosen.alamat, bak_dosen.aktif, bak_prog_study.prog_study "+
                        "FROM         bak_dosen INNER JOIN "+
                                              "bak_prog_study ON bak_dosen.prodi = bak_prog_study.id_prog_study "+
                        "WHERE       prodi = @prodi AND aktif = 'yes' AND (dosen_tim IS NULL OR dosen_tim != 'yes') ORDER BY nama ASC" +
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
                            this.GVDosen.DataSource = TableDosen;
                            this.GVDosen.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableDosen.Rows.Clear();
                            TableDosen.Clear();
                            GVDosen.DataSource = TableDosen;
                            GVDosen.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                            return;
                        }
                    }

                    //// loop to cek status dosen
                    //for (int i = 0; i < this.GVDosen.Rows.Count; i++)
                    //{
                    //    SqlCommand CmdAktif = new SqlCommand("SpGetDosenByProdi", con);
                    //    CmdAktif.CommandType = System.Data.CommandType.StoredProcedure;

                    //    CmdAktif.Parameters.AddWithValue("@prodi", this.DLProdi.SelectedValue);

                    //    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    //    {
                    //        if (rdr.HasRows)
                    //        {
                    //            while (rdr.Read())
                    //            {
                    //                if (rdr["aktif"].ToString() == "yes")
                    //                {
                    //                    CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                    //                    ch.Checked = true;
                    //                }
                    //                else if (rdr["aktif"].ToString() != "yes")
                    //                {
                    //                    CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                    //                    ch.Checked = false;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void GVDosen_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GVDosen_PreRender(object sender, EventArgs e)
        {
            if (this.GVDosen.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVDosen.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVDosen.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void GVDosen_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}