using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Keuangan.admin
{
    //public partial class WebForm11 : System.Web.UI.Page
    public partial class WebForm11 : Keu_Admin_Class
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
            this.Session["Name"] = null;
            this.Session["Passwd"] = null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        } 
        protected void load()
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmdvw = new SqlCommand(" SELECT bak_prog_study.id_prog_study, bak_prog_study.prog_study, keu_masa_bayar.no, keu_masa_bayar.open_date, keu_masa_bayar.close_date " +
                                                    " FROM bak_prog_study INNER JOIN " +
                                                    " keu_masa_bayar ON bak_prog_study.id_prog_study = keu_masa_bayar.id_prog_study", con);
                cmdvw.CommandType = System.Data.CommandType.Text;

                DataTable Table = new DataTable();
                Table.Columns.Add("Kode");
                Table.Columns.Add("Program Studi");
                Table.Columns.Add("Tanggal Buka");
                Table.Columns.Add("Tanggal Tutup");

                con.Open();
                using (SqlDataReader rdr = cmdvw.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();
                            datarow["Kode"] = rdr["id_prog_study"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            DateTime DateAwal = Convert.ToDateTime(rdr["open_date"]);
                            DateTime DateAkhir = Convert.ToDateTime(rdr["close_date"]);
                            datarow["Tanggal Buka"] = DateAwal.ToString("dd MMMM, yyyy");
                            datarow["Tanggal Tutup"] = DateAkhir.ToString("dd MMMM, yyyy");
                            Table.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVProdi.DataSource = Table;
                        this.GVProdi.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVProdi.DataSource = Table;
                        GVProdi.DataBind();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
                load();
            }
        }

        protected void GVProdi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    var Awal =e.Row.Cells[2].Text;
            //    string FormattedString0 = string.Format
            //        (new System.Globalization.CultureInfo("id"), "{0:d}", Awal);
            //    e.Row.Cells[2].Text = FormattedString0;
            //}

        }

        protected void BtnUpdateTgl_Click(object sender, EventArgs e)
        {
            // hitung checkbox selected
            int cnt = 0;
            for (int i = 0; i < GVProdi.Rows.Count; i++)
            {
                CheckBox CB = (CheckBox)GVProdi.Rows[i].FindControl("CBSelect");
                if (CB.Checked == true)
                {
                    cnt += 1;
                }
            }
            // checkbox selected
            if (cnt == 0)
            {
                //client message belum pilih check list.....
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Data ...');", true);
                //ScriptManager.RegisterStartupScript((Control)this.BtnEditSKS, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
                return;
            }
            else
            {

                for (int i = 0; i < GVProdi.Rows.Count; i++)
                {
                    CheckBox CB = (CheckBox)GVProdi.Rows[i].FindControl("CBSelect");
                    if (CB.Checked == true)
                    {
                        //Get Kode Program Studi
                        string kode_prodi = GVProdi.Rows[i].Cells[1].Text;
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();

                            SqlCommand cmdvw = new SqlCommand("UPDATE keu_masa_bayar set  open_date=@mulai, close_date=@akhir WHERE id_prog_study=@id_prodi", con);
                            cmdvw.CommandType = System.Data.CommandType.Text;

                            //DateTime mulai = DateTime.ParseExact(this.TBTglMulai.Text, "yyyy/M/d", CultureInfo.InvariantCulture);
                            //DateTime akhir = DateTime.ParseExact(this.TBTglAkhir.Text, "yyyy/M/d", CultureInfo.InvariantCulture);
                            cmdvw.Parameters.AddWithValue("@mulai", this.TBTglMulai.Text);
                            cmdvw.Parameters.AddWithValue("@akhir", this.TBTglAkhir.Text);
                            cmdvw.Parameters.AddWithValue("@id_prodi", this.GVProdi.Rows[i].Cells[1].Text);

                            cmdvw.ExecuteNonQuery();
                        }
                    }
                }
            }

            load();
        }
    }
}