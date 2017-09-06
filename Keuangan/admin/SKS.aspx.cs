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
    //public partial class WebForm7 : System.Web.UI.Page
    public partial class WebForm7 : Keu_Admin_Class
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Label masterlbl = (Label)Master.FindControl("LabelUsername");
            masterlbl.Text = this.Session["Name"].ToString();

            if (!Page.IsPostBack)
            {
                this.PanelSKS.Enabled = false;
                this.PanelSKS.Visible = false;
                this.BtnEditSKS.Visible = false;
                this.PanelEditSKS.Enabled = false;
                this.PanelEditSKS.Visible = false;
            }
        }

        protected void FillGVSKS()
        {
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();

                //BtnInsertSKS.Visible = true;
                //BtnPosting.Visible = true;

                //ISI Grdview 
                string CSSKS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CSSKS))
                {
                    SqlCommand cmd = new SqlCommand("SpViewSks", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@npm", TBNpm.Text);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("SKS");
                    Table.Columns.Add("Biaya");

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelSKS.Enabled = true;
                            this.PanelSKS.Visible = true;
                            this.BtnEditSKS.Enabled = true;
                            this.BtnEditSKS.Visible = true;

                            this.LbFilterMhs.Text = "";
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["Semester"] = rdr["semester"];
                                datarow["SKS"] = rdr["sks"];
                                datarow["Biaya"] = rdr["biaya"];
                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVSKS.DataSource = Table;
                            this.GVSKS.DataBind();

                            Table.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVSKS.DataSource = Table;
                            GVSKS.DataBind();
                            this.LbFilterMhs.Text = "";
                            this.PanelEditSKS.Visible = false;
                            this.PanelSKS.Visible = false;
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data KRS tidak ditemukan');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                //LbFilterMhs.Text = ex.Message.ToString();
                //LbFilterMhs.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //filter Textbox NPM
            if (this.TBNpm.Text == "")
            {
                this.LbFilterMhs.Text = "Input NPM";
                LbFilterMhs.ForeColor = System.Drawing.Color.Red;
                this.PanelSKS.Enabled = false;
                this.PanelSKS.Visible = false;
                this.BtnEditSKS.Visible = false;
                this.PanelEditSKS.Enabled = false;
                return;
            }
            FillGVSKS();
        }

        protected void GVSKS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[3].Text);
                string FormattedString4 = string.Format
                    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                e.Row.Cells[3].Text = FormattedString4;
            }
        }

        protected void BtnEditSKS_Click(object sender, EventArgs e)
        {
            // hitung checkbox selected
            int cnt = 0;
            for (int i = 0; i < GVSKS.Rows.Count; i++)
            {
                CheckBox CB = (CheckBox)GVSKS.Rows[i].FindControl("CBPilih");
                if (CB.Checked == true)
                {
                    cnt += 1;
                }
            }
            // checkbox selected
            if (cnt > 1 || cnt == 0)
            {
                //client message check list lebih dari 1 item atau belum pilih check list.....
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Piliah Salah Satu Saja');", true);
                //ScriptManager.RegisterStartupScript((Control)this.BtnEditSKS, this.GetType(), "redirectMe", "alert('Piliah Salah Satu Biaya Angsuran');", true);
                return;
            }
            else
            {
                PanelEditSKS.Enabled = true;
                PanelEditSKS.Visible = true;

                for (int i = 0; i < GVSKS.Rows.Count; i++)
                {
                    CheckBox CB = (CheckBox)GVSKS.Rows[i].FindControl("CBPilih");
                    if (CB.Checked == true)
                    {
                        string semester = GVSKS.Rows[i].Cells[1].Text;
                        string SKS = GVSKS.Rows[i].Cells[2].Text;

                        this.TBSemester.Text = semester;
                        this.TBSKS.Text = SKS;
                    }
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (this.TBSKS.Text == "" || this.TBSKS.Text == "0" || Convert.ToInt32 (this.TBSKS.Text) > 24)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jumlah SKS tidak boleh kosong Atau lebih dari 24 SKS');", true);
                return;
            }

            if (this.LbThnAngkatan.Text == "2014/2015")
            {
                decimal biaya = Convert.ToDecimal(this.TBSKS.Text) * 40000;

                // EDIT SKS
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {   
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", TBSKS.Text);
                        cmd.Parameters.AddWithValue("@biaya", biaya);
                        cmd.Parameters.AddWithValue("@npm",this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", this.TBSemester.Text);
                        cmd.Parameters.AddWithValue("@update",DateTime.Now);
                        cmd.ExecuteNonQuery();

                        this.TBSKS.Text = "";

                        trans.Commit();
                        trans.Dispose();
                        cmd.Dispose();
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();

                        //Refresh Gridview by calling button click event 
                        BtnFilter_Click(this, null);

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('UPDATE SKS BERHASIL');", true);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else if (this.LbThnAngkatan.Text == "2013/2014")
            {
                decimal biaya = Convert.ToDecimal(this.TBSKS.Text) * 35000;

                // EDIT SKS
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //open connection and begin transaction
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        SqlCommand cmd = new SqlCommand("update keu_sks set sks=@sks,biaya=@biaya, tgl_update=@update where npm=@npm AND semester=@semester ", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@sks", TBSKS.Text);
                        cmd.Parameters.AddWithValue("@biaya", biaya);
                        cmd.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        cmd.Parameters.AddWithValue("@semester", this.TBSemester.Text);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);
                        cmd.ExecuteNonQuery();

                        this.TBSKS.Text = "";

                        trans.Commit();
                        trans.Dispose();
                        cmd.Dispose();
                        cmd.Dispose();
                        con.Close();
                        con.Dispose();

                        //Refresh Gridview by calling button click event 
                        BtnFilter_Click(this, null);

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('UPDATE SKS BERHASIL');", true);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else
            {
            }
        }
    }
}