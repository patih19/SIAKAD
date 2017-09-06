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
    //public partial class WebForm13 : System.Web.UI.Page
    public partial class WebForm13 : Tu
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DLProdi.Items.Insert(0, new ListItem("Program Studi", "-1"));
                this.DLProdi.Items.Insert(1, new ListItem(this.Session["Prodi"].ToString(), this.Session["level"].ToString()));
            }

            this.PanelRekapSKS.Enabled = false;
            this.PanelRekapSKS.Visible = false;
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SpRekapSKS", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@id_prodi", this.DLProdi.SelectedValue);
                CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("NPM");
                TableJadwal.Columns.Add("Nama");
                TableJadwal.Columns.Add("SKS");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbIdProdi.Text = this.DLProdi.SelectedValue;
                        this.LbProdi.Text = this.DLProdi.SelectedItem.Text;

                        this.LbTahun.Text = this.DLTahun.SelectedItem.Text;
                        this.LbSemester.Text = this.DLSemester.SelectedItem.Text;

                        this.PanelRekapSKS.Enabled = true;
                        this.PanelRekapSKS.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["NPM"] = rdr["npm"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["SKS"] = rdr["total_sks"];

                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVRekapSKS.DataSource = TableJadwal;
                        this.GVRekapSKS.DataBind();
                    }
                    else
                    {
                        // hide panel Edit Mata Kuliah
                        this.PanelRekapSKS.Enabled = false;
                        this.PanelRekapSKS.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVRekapSKS.DataSource = TableJadwal;
                        GVRekapSKS.DataBind();
                    }
                }
            }
        }
    }
}