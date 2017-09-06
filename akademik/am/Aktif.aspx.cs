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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DLProdiDosen_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand CmdMakul = new SqlCommand("SPPesertaMakul", con);
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
                        SqlCommand CmdCekMasa = new SqlCommand("SpQuotaJadwal", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@no_jadwal", e.Row.Cells[2].Text);

                        SqlParameter Quota = new SqlParameter();
                        Quota.ParameterName = "@quota";
                        Quota.SqlDbType = System.Data.SqlDbType.VarChar;
                        Quota.Size = 20;
                        Quota.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Quota);

                        SqlParameter Sisa = new SqlParameter();
                        Sisa.ParameterName = "@sisa";
                        Sisa.SqlDbType = System.Data.SqlDbType.Int;
                        Sisa.Size = 20;
                        Sisa.Direction = System.Data.ParameterDirection.Output;
                        CmdCekMasa.Parameters.Add(Sisa);

                        CmdCekMasa.ExecuteNonQuery();


                        if (Convert.ToInt32(Sisa.Value.ToString()) != 0)
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = Sisa.Value.ToString();

                            LbSisa.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            Label LbSisa = (Label)e.Row.Cells[1].FindControl("Lbsisa");
                            LbSisa.Text = "Penuh";

                            LbSisa.ForeColor = System.Drawing.Color.Red;
                            CheckBox ch = (CheckBox)e.Row.Cells[0].FindControl("CBMakul");

                            ch.Visible = false;
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("Error Reading Satus/ Sisa Quota Jadwal Mata Kuliah");
                }
            }
        }
    }
}