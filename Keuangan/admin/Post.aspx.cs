using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Keuangan.admin
{
    //public partial class WebForm5 : System.Web.UI.Page
      public partial class WebForm5 : Keu_Admin_Class
    {
        mahasiswa mhs = new mahasiswa();

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
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }

        private void ReadMahasiswa()
        {
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            this.PanelPosting.Enabled = false;
            this.PanelPosting.Visible = false;

            this.PanelPosting2.Enabled = false;
            this.PanelPosting2.Visible = false;
        }

        protected void GVPost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Jumlah = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Jumlah);
                e.Row.Cells[5].Text = FormattedString1;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            // FIlter TextBOX NPM
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM Mahasiswa');", true);
                return;
            }

            //read mahasiswa
            try
            {
                ReadMahasiswa();
            }
            catch (Exception)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error saat membaca data mahasiswa');", true);
                return;
            }

            this.PanelPosting.Enabled = true;
            this.PanelPosting.Visible = true;

            this.PanelPosting2.Enabled = true;
            this.PanelPosting2.Visible = true;

            try
            {
                //---------------------Fill Grid View Tagihan Periodik ------------------------------
                // --- Fill Grid View Posting Mahasiswa Terbayar ---
                string CSPost = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CSPost))
                {
                    SqlCommand cmdvw = new SqlCommand("SpVwPosting5", con);
                    cmdvw.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdvw.Parameters.AddWithValue("@npm", TBNpm.Text);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Nomor");
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Angsuran");
                    Table.Columns.Add("Cicilan");
                    Table.Columns.Add("Tanggal Tagihan");
                    //Table.Columns.Add("Tanggal Bayar");
                    Table.Columns.Add("Jumlah");
                    Table.Columns.Add("status bayar");

                    con.Open();
                    using (SqlDataReader rdr = cmdvw.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                //datarow["Tanggal Bayar"] = rdr["biaya"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status bayar"] = rdr["status"];
                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVPost.DataSource = Table;
                            this.GVPost.DataBind();

                            Table.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVPost.DataSource = Table;
                            GVPost.DataBind();
                        }
                    }

                    // --- Fill Grid View Posting Mahasiswa Belum Terbayar ---
                    SqlCommand cmdvw2 = new SqlCommand("SpVwPosting4", con);
                    cmdvw2.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdvw2.Parameters.AddWithValue("@npm", TBNpm.Text);

                    DataTable Table2 = new DataTable();
                    Table2.Columns.Add("Nomor");
                    Table2.Columns.Add("Semester");
                    Table2.Columns.Add("Angsuran");
                    Table2.Columns.Add("Cicilan");
                    Table2.Columns.Add("Tanggal Tagihan");
                    //Table.Columns.Add("Tanggal Bayar");
                    Table2.Columns.Add("Jumlah");
                    Table2.Columns.Add("Status Bayar");

                    //con.Open();
                    using (SqlDataReader rdr = cmdvw2.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table2.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                //datarow["Tanggal Bayar"] = rdr["biaya"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status Bayar"] = rdr["status"];
                                Table2.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVDelayPost.DataSource = Table2;
                            this.GVDelayPost.DataBind();

                            Table2.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            Table2.Rows.Clear();
                            Table2.Clear();
                            this.GVDelayPost.DataSource = Table2;
                            this.GVDelayPost.DataBind();
                        }
                    }


                    //---------------------Fill Grid View Tagihan Non Periodik / Akhir ------------------------------
                    // --- Fill Grid View Posting Biaya Akhir Mahasiswa Sudah Terbayar ---
                    SqlCommand cmdAkhirLunas = new SqlCommand("SpVwPostingAkhir2", con);
                    cmdAkhirLunas.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdAkhirLunas.Parameters.AddWithValue("@npm", TBNpm.Text);

                    DataTable TableAkhirLunas = new DataTable();
                    TableAkhirLunas.Columns.Add("Nomor");
                    TableAkhirLunas.Columns.Add("Semester");
                    TableAkhirLunas.Columns.Add("Angsuran");
                    TableAkhirLunas.Columns.Add("Cicilan");
                    TableAkhirLunas.Columns.Add("Tanggal Tagihan");
                    //TableAkhirLunas.Columns.Add("Tanggal Bayar");
                    TableAkhirLunas.Columns.Add("Jumlah");
                    TableAkhirLunas.Columns.Add("Status Bayar");

                    //con.Open();
                    using (SqlDataReader rdr = cmdAkhirLunas.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableAkhirLunas.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                //datarow["Tanggal Bayar"] = rdr["biaya"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status Bayar"] = rdr["status"];
                                TableAkhirLunas.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVPostAkhir.DataSource = TableAkhirLunas;
                            this.GVPostAkhir.DataBind();

                            TableAkhirLunas.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableAkhirLunas.Rows.Clear();
                            TableAkhirLunas.Clear();
                            this.GVPostAkhir.DataSource = TableAkhirLunas;
                            this.GVPostAkhir.DataBind();
                        }
                    }

                    //---------------------Fill Grid View Tagihan Non Periodik / Akhir ------------------------------
                    // --- Fill Grid View Posting Biaya Akhir Mahasiswa Belum Terbayar ---
                    SqlCommand cmdAkhirDelay = new SqlCommand("SpVwPostingAkhir1", con);
                    cmdAkhirDelay.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdAkhirDelay.Parameters.AddWithValue("@npm", TBNpm.Text);

                    DataTable TableAkhirDelay = new DataTable();
                    TableAkhirDelay.Columns.Add("Nomor");
                    TableAkhirDelay.Columns.Add("Semester");
                    TableAkhirDelay.Columns.Add("Angsuran");
                    TableAkhirDelay.Columns.Add("Cicilan");
                    TableAkhirDelay.Columns.Add("Tanggal Tagihan");
                    //TableAkhirLunas.Columns.Add("Tanggal Bayar");
                    TableAkhirDelay.Columns.Add("Jumlah");
                    TableAkhirDelay.Columns.Add("Status Bayar");

                    //con.Open();
                    using (SqlDataReader rdr = cmdAkhirDelay.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableAkhirDelay.NewRow();

                                datarow["Nomor"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Angsuran"] = rdr["billRef5"];
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Tagihan"] = rdr["post_date"];
                                //datarow["Tanggal Bayar"] = rdr["biaya"];
                                datarow["Jumlah"] = rdr["amount_total"];
                                datarow["Status Bayar"] = rdr["status"];
                                TableAkhirDelay.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVDelayPostAkhir.DataSource = TableAkhirDelay;
                            this.GVDelayPostAkhir.DataBind();

                            TableAkhirDelay.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            TableAkhirDelay.Rows.Clear();
                            TableAkhirDelay.Clear();
                            this.GVDelayPostAkhir.DataSource = TableAkhirDelay;
                            this.GVDelayPostAkhir.DataBind();
                        }
                    }
                }
            }catch (Exception ex)
            {
                 this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GVPostAkhir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Jumlah = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Jumlah);
                e.Row.Cells[5].Text = FormattedString1;
            }
        }

        protected void GVDelayPostAkhir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Jumlah = Convert.ToInt32(e.Row.Cells[5].Text);
                string FormattedString1 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Jumlah);
                e.Row.Cells[5].Text = FormattedString1;
            }
        }
    }
}