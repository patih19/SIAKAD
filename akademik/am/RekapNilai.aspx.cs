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
    public partial class WebForm28 : Bak_staff
    {
        private int NomorJadwal;
        public int NoJadwal
        {
            get { return NomorJadwal; }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateProdi();
            }
        }

        private void PopulateProdi()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void DLProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedValue == "Tahun" || this.DLTahun.SelectedValue == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DlSemester.SelectedValue == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }
            if (this.DLProdi.SelectedItem.Text == "Program Studi")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SPPesertaMakul2", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@idprodi", DLProdi.SelectedValue);
                CmdMakul.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DlSemester.SelectedItem.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("No Jadwal");
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");
                TableMakul.Columns.Add("Dosen");
                TableMakul.Columns.Add("Kelas");
                TableMakul.Columns.Add("Jenis Kelas");
                TableMakul.Columns.Add("Peserta");


                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["No Jadwal"] = rdr["no_jadwal"].ToString();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            datarow["Dosen"] = rdr["nama"];
                            datarow["Kelas"] = rdr["kelas"];
                            datarow["Jenis Kelas"] = rdr["jenis_kelas"];
                            datarow["Peserta"] = rdr["jumlah"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVAktif.DataSource = TableMakul;
                        this.GVAktif.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVAktif.DataSource = TableMakul;
                        GVAktif.DataBind();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jawal Belum Ada');", true);
                        return;
                    }
                }
            }
        }

        protected void GVAktif_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;  //No Jadwal

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int BiayaSks = Convert.ToInt32(e.Row.Cells[1].Text);
                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // ------ Cek Sisa Quota -------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekNilai", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[0].Text);

                        SqlParameter Quota = new SqlParameter();
                        Quota.ParameterName = "@NotInput";
                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                        Quota.Size = 20;
                        Quota.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Quota);

                        CmdCekMasa.ExecuteNonQuery();
                        Label LbBlmInput = (Label)e.Row.Cells[1].FindControl("LbNotReady");

                        if (Quota.Value.ToString() != "0")
                        {
                            LbBlmInput.Text = "-"+Quota.Value.ToString();
                            LbBlmInput.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            LbBlmInput.ForeColor = System.Drawing.Color.Green;
                            LbBlmInput.Text = "Lengkap";
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah");
                }
            }
        }

        protected void GVAktif_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVAktif.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }



        protected void LnkLihat_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            NomorJadwal = Convert.ToInt32(this.GVAktif.Rows[index].Cells[0].Text);

            Server.Transfer("~/am/ListNilai.aspx");
        }

        protected void GVAktif_PreRender(object sender, EventArgs e)
        {
            if (this.GVAktif.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVAktif.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVAktif.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVAktif.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}