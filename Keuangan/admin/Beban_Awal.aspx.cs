using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Keuangan.admin
{
    //public partial class WebForm12 : System.Web.UI.Page
    public partial class WebForm12 : Keu_Admin_Class
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
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
               // hide panel Beban
                this.PanelBeban.Enabled = false;
                this.PanelBeban.Visible = false;

                //hide pabel update
                this.PanelUpdate.Enabled = false;
                this.PanelUpdate.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //validation
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('NPM Belum Diisi');", true);
                return;
            }

            mhs.ReadMahasiswa(this.TBNpm.Text);

            LbNama.Text = mhs.nama;
            LbNPM.Text = LbNPM.Text;
            LbClass.Text = mhs.kelas;
            LbThnAngkatan.Text = mhs.thn_angkatan;
            LbProdi.Text = mhs.Prodi;


            //---Fill GV Beban Awal ---
            string ConString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand("SELECT no, npm, beban_awal FROM keu_keu_mhs where npm=@npm", con);
                cmd.Parameters.AddWithValue("@npm", TBNpm.Text);

                DataTable Table = new DataTable();
                Table.Columns.Add("NPM");
                Table.Columns.Add("Biaya");

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        // show panel Beban
                        this.PanelBeban.Enabled = true;
                        this.PanelBeban.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = Table.NewRow();

                            datarow["NPM"] = rdr["npm"];
                            if (rdr["beban_awal"] == DBNull.Value)
                            {
                                //BtnViewBiaya2.Enabled = false;
                                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tagihan Awal belum diinput');", true);
                                //return;
                            }
                            else
                            {
                                datarow["Biaya"] = rdr["beban_awal"];
                                Table.Rows.Add(datarow);
                            }
                        }

                        //Fill Gridview
                        this.GVBeban.DataSource = Table;
                        this.GVBeban.DataBind();

                        this.LbNIM.Text = TBNpm.Text;
                    }
                    else
                    {
                        this.LbNIM.Text = "";
                        this.TBNpm.Text = "";

                        LbNama.Text = "Nama";
                        LbNPM.Text = "NPM";
                        LbClass.Text = "Jenis Kelas";
                        LbThnAngkatan.Text = "Tahun Angkatan";
                        LbProdi.Text = "Prograsm Studi";

                        //clear Gridview
                        Table.Rows.Clear();
                        Table.Clear();
                        GVBeban.DataSource = Table;
                        GVBeban.DataBind();

                        this.PanelBeban.Enabled = false;
                        this.PanelBeban.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mahasiswa Pada Table Kuangan Mahasiswa Tidak Ditemukan');", true);
                        return;
                    }
                }
            }
        }
        // Klik Button Edit
        protected void BtnUbah_Click(object sender, EventArgs e)
        {
            this.TbBeban.Text = "";

            //show panel update
            this.PanelUpdate.Enabled = true;
            this.PanelUpdate.Visible = true;
        }

        //update tagihan to Zero
        protected void Button2_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();

                    SqlCommand cmdUpdate = new SqlCommand("UPDATE keu_keu_mhs SET beban_awal= 0 WHERE npm=@npm", con);
                    cmdUpdate.CommandType = System.Data.CommandType.Text;

                    cmdUpdate.Parameters.AddWithValue("@npm", TBNpm.Text);

                    cmdUpdate.ExecuteNonQuery();

                    cmdUpdate.Dispose();
                    con.Close();

                    // Klick button
                    BtnFilter_Click(this, null);

                    // clear Billing number
                    this.TbBeban.Text = "";

                    //hide update panel
                    this.PanelUpdate.Enabled = false;
                    this.PanelUpdate.Visible = false;

                    //BtnCariOffline_Click(this, null);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);
                }
                catch ( Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }

            }
        }

        //Edit Beban Awal
        protected void BtnUpdateOffline_Click(object sender, EventArgs e)
        {
            //validation
            if (this.TbBeban.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Beban Awal Baru');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();

                    SqlCommand cmdUpdate = new SqlCommand("UPDATE keu_keu_mhs SET beban_awal=@beban WHERE npm=@npm", con);
                    cmdUpdate.CommandType = System.Data.CommandType.Text;

                    cmdUpdate.Parameters.AddWithValue("@npm", this.LbNIM.Text);
                    cmdUpdate.Parameters.AddWithValue("@beban", this.TbBeban.Text);

                    cmdUpdate.ExecuteNonQuery();

                    cmdUpdate.Dispose();
                    con.Close();

                    // Klick button
                    BtnFilter_Click(this, null);

                    // clear Billing number
                    this.LbNIM.Text = "";
                    this.TbBeban.Text = "";

                    //hide update panel
                    this.PanelUpdate.Enabled = false;
                    this.PanelUpdate.Visible = false;

                    //BtnCariOffline_Click(this, null);
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Success');", true);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        // Format Rupiah Tagihan Awal
        protected void GVBeban_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Beban = Convert.ToInt32(e.Row.Cells[2].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", Beban);
                e.Row.Cells[2].Text = FormattedString4;
            }
        }
    }
}