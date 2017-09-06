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
    //public partial class WebForm10 : System.Web.UI.Page
      public partial class WebForm10 : Keu_Admin_Class
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
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();

            this.Response.Redirect("~/keu-login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // welcome name identity
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();


            if (!Page.IsPostBack)
            {
                // hide panel EDIT OFFLINE
                this.PanelOffline.Enabled = false;
                this.PanelOffline.Visible = false;

                //hine panel EDIT ONLINE
                this.PanelOnline.Enabled = false;
                this.PanelOnline.Visible = false;

                //hide panel update Offline
                this.PanelUpdateOffline.Enabled = false;
                this.PanelUpdateOffline.Visible = false;

                //hide panel update Online
                this.PanelUpdateOnline.Enabled = false;
                this.PanelUpdateOnline.Visible = false;
            }
        }

        protected void BtnCariOffline_Click(object sender, EventArgs e)
        {
            //validation
            if (this.TBNoBill.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No Billing Tidak Boleh Kosong');", true);
                return;
            }
            
            //set nobill color
            this.LbNoBill.ForeColor = System.Drawing.Color.Black;

            // set label NPM
            this.LbNoBill.Text = TBNoBill.Text;

            string CSPost = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CSPost))
            {
                con.Open();

                // -------------- A. POSTING OFFLINE ------------
                SqlCommand cmdvw = new SqlCommand("SpVwPosting6", con);
                cmdvw.CommandType = System.Data.CommandType.StoredProcedure;

                cmdvw.Parameters.AddWithValue("@billingNo", LbNoBill.Text);

                DataTable Table = new DataTable();
                Table.Columns.Add("Nomor");
                Table.Columns.Add("Semester");
                Table.Columns.Add("Angsuran");
                Table.Columns.Add("Cicilan");
                Table.Columns.Add("Tanggal Tagihan");
                //Table.Columns.Add("Tanggal Bayar");
                Table.Columns.Add("Jumlah");
                Table.Columns.Add("status bayar");

                using (SqlDataReader rdr = cmdvw.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        // show panel EDIT OFFLINE
                        this.PanelOffline.Enabled = true;
                        this.PanelOffline.Visible = true;

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
                        this.GVOfflinePeriodik.DataSource = Table;
                        this.GVOfflinePeriodik.DataBind();

                        Table.Dispose();
                    }
                    else
                    {
                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVOfflinePeriodik.DataSource = Table;
                        GVOfflinePeriodik.DataBind();
                    }
                }
                // -------------------------END -----------------------------------

                // ----------------- B. POSTING UNPAID ONLINE ------------------
                SqlCommand CmdOn = new SqlCommand("SpVwPosting7", con);
                CmdOn.CommandType = System.Data.CommandType.StoredProcedure;

                CmdOn.Parameters.AddWithValue("@billingNo", LbNoBill.Text);

                DataTable TableOn = new DataTable();
                TableOn.Columns.Add("Nomor");
                TableOn.Columns.Add("Semester");
                TableOn.Columns.Add("Angsuran");
                TableOn.Columns.Add("Cicilan");
                TableOn.Columns.Add("Tanggal Tagihan");
                //TableOn.Columns.Add("Tanggal Bayar");
                TableOn.Columns.Add("Jumlah");
                TableOn.Columns.Add("status bayar");

                using (SqlDataReader rdrOn = CmdOn.ExecuteReader())
                {
                    if (rdrOn.HasRows)
                    {
                        // show panel EDIT ONLINE
                        this.PanelOnline.Enabled = true;
                        this.PanelOnline.Visible = true;

                        while (rdrOn.Read())
                        {
                            DataRow datarow = TableOn.NewRow();

                            datarow["Nomor"] = rdrOn["billingNo"];
                            datarow["Semester"] = rdrOn["billRef4"];
                            datarow["Angsuran"] = rdrOn["billRef5"];
                            datarow["Cicilan"] = rdrOn["cicilan"];
                            datarow["Tanggal Tagihan"] = rdrOn["post_date"];
                            //datarow["Tanggal Bayar"] = rdr["biaya"];
                            datarow["Jumlah"] = rdrOn["amount_total"];
                            datarow["Status bayar"] = rdrOn["status"];
                            TableOn.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVOnline.DataSource = TableOn;
                        this.GVOnline.DataBind();

                        TableOn.Dispose();
                    }
                    else
                    {
                        //Set Information 
                        this.LbNoBill.Text = "";
                        this.LbNoBill.ForeColor = System.Drawing.Color.Red;

                        //clear Gridview
                        TableOn.Rows.Clear();
                        TableOn.Clear();
                        GVOnline.DataSource = TableOn;
                        GVOnline.DataBind();
                    }
                }
                // -------------------------END -----------------------------------
            }
        }

        //Edit Offline
        protected void BtnEditOffline_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            // clear textbox
            this.TbBiayaOnline.Text = "";

            this.LbBillNumber.Text = this.GVOfflinePeriodik.Rows[index].Cells[1].Text;

            //show panel update online
            this.PanelUpdateOffline.Enabled = true;
            this.PanelUpdateOffline.Visible = true;

        }

        // Format Rupiah
        protected void GVOfflinePeriodik_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Biaya = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[6].Text = FormattedString;
            }
        }
        
        // Update Biaya Offline
        protected void BtnUpdateOffline_Click(object sender, EventArgs e)
        {
            //validation
            if (this.TbBiayaOffline.Text == "" || this.TbBiayaOffline.Text == "0")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Biaya Tidak Boleh Kosong');", true);
                return;
            }

             string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
             using (SqlConnection con = new SqlConnection(CS))
             {
                 con.Open();

                 SqlCommand cmdUpdate = new SqlCommand("update keu_posting_bank set amount_total=@total where billingNo=@BillNumber", con);
                 cmdUpdate.CommandType = System.Data.CommandType.Text;

                 cmdUpdate.Parameters.AddWithValue("@total", TbBiayaOffline.Text);
                 cmdUpdate.Parameters.AddWithValue("@BillNumber", LbBillNumber.Text);

                 cmdUpdate.ExecuteNonQuery();

                 BtnCariOffline_Click(this,null);

                 this.TBNoBill.Text = "";

                 this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);
             }
        }

         // Button Edit Online
        protected void BtnEditOnline_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            // clear textbox
            this.TbBiayaOnline.Text = "";

            this.LbBillOnline.Text = this.GVOnline.Rows[index].Cells[1].Text;

            //show panel update online
            this.PanelUpdateOnline.Enabled = true;
            this.PanelUpdateOnline.Visible = true;
        }

        protected void GVOnline_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Biaya = Convert.ToInt32(e.Row.Cells[6].Text);
                string FormattedString = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Biaya);
                e.Row.Cells[6].Text = FormattedString;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //-============= CATATAN PENTING SEKALI YA :-) ================= --
            // Kalau dalam stored procedure menggunakan transaction maka
            // Coding juga sama 
            //-====================================================== --

            // ---- Validation -----
            if (this.TbBiayaOnline.Text == "" || this.TbBiayaOnline.Text == "0")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Biaya Tidak Boleh Kosong');", true);
                return;
            }

            if (this.DLCicilanOl.SelectedValue != "1" && this.DLCicilanOl.SelectedValue != "2")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Cicilan');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    SqlCommand cmdUpdate = new SqlCommand("SpUpdateUnpaid", con);
                    cmdUpdate.Transaction = trans;
                    cmdUpdate.CommandType = System.Data.CommandType.StoredProcedure;

                    cmdUpdate.Parameters.AddWithValue("@billingNo",this.LbBillOnline.Text);
                    cmdUpdate.Parameters.AddWithValue("@NewAmount",Convert.ToInt32(TbBiayaOnline.Text));
                    cmdUpdate.Parameters.AddWithValue("@cicilan", this.DLCicilanOl.SelectedValue);

                    cmdUpdate.ExecuteNonQuery();
                    trans.Commit();
                    trans.Dispose();

                    cmdUpdate.Dispose();
                    con.Close();

                    // Klick button
                    BtnCariOffline_Click(this, null);

                    // clear Billing number
                    this.LbNoBill.Text = "";
                    this.TBNoBill.Text = "";
                    this.DLCicilanOl.SelectedIndex = 0;

                    //hide update panel
                    this.PanelUpdateOnline.Enabled = false;
                    this.PanelUpdateOnline.Visible = false;

                    //BtnCariOffline_Click(this, null);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);
                }
                catch (Exception)
                {
                    PanelUpdateOnline.Enabled = false;
                    PanelUpdateOnline.Visible = false;
                    trans.Rollback();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Gagal');", true);
                }
            }
        }
    }
}