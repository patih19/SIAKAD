using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.HtmlControls;

namespace Padu.pasca
{
    public partial class khs : MhsPasca
    {
        public decimal _TotalSKS
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString()); }
            set
            { this.ViewState["TotalSKS"] = (object)value; }
        }
        public decimal _TotalNilai
        {
            get
            { return Convert.ToDecimal(this.ViewState["TotalNilai"].ToString()); }
            set
            { this.ViewState["TotalNilai"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavKHS");
                control.Attributes.Add("class", "dropdown active opened");
                //HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavKHS");
                //control2.Attributes.Add("style", "display: block;");

                this.PanelKHS.Visible = false;
            }
        }

        protected void BtnKHS_Click(object sender, EventArgs e)
        {
            try
            {
                // ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    // -- Cek Masa INPUT NILAI
                    // -- Tidak Diperbolehkan Pada Saat Masa Input NILAI --
                    SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                    CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedValue);
                    CmdCekMasa.Parameters.AddWithValue("@jenis_keg", "Nilai");
                    CmdCekMasa.Parameters.AddWithValue("@jenjang", this.Session["jenjang"].ToString());

                    SqlParameter Status = new SqlParameter();
                    Status.ParameterName = "@output";
                    Status.SqlDbType = System.Data.SqlDbType.VarChar;
                    Status.Size = 20;
                    Status.Direction = System.Data.ParameterDirection.Output;
                    CmdCekMasa.Parameters.Add(Status);

                    CmdCekMasa.ExecuteNonQuery();

                    if (Status.Value.ToString() == "IN")
                    {
                        con.Close();
                        con.Dispose();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Masa input nilai sedang berlangsung');", true);
                        return;
                    }

                    // --------------------- Fill Gridview  ------------------------
                    SqlCommand CmdListKRS = new SqlCommand("SpGetKHS", con);
                    CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;


                    CmdListKRS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);

                    DataTable TableKRS = new DataTable();
                    TableKRS.Columns.Add("Kode");
                    TableKRS.Columns.Add("Mata Kuliah");
                    TableKRS.Columns.Add("SKS");
                    TableKRS.Columns.Add("Nilai");
                    TableKRS.Columns.Add("Jumlah");

                    using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelKHS.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableKRS.NewRow();
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["SKS"] = rdr["sks"];

                                if (rdr["Nilai"] == DBNull.Value)
                                {
                                    //datarow["Nilai"] = "";
                                }
                                else
                                {
                                    datarow["Nilai"] = rdr["nilai"];
                                }
                                datarow["Jumlah"] = rdr["jumlah"];

                                TableKRS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvKHS.DataSource = TableKRS;
                            this.GvKHS.DataBind();

                            //Set Label
                            this.LBSks.Text = _TotalSKS.ToString();
                            decimal IPS = _TotalNilai / _TotalSKS;
                            this.LbIPS.Text = String.Format("{0:0.##}", IPS);

                        }
                        else
                        {
                            this.PanelKHS.Visible = false;

                            //clear Gridview
                            TableKRS.Rows.Clear();
                            TableKRS.Clear();
                            GvKHS.DataSource = TableKRS;
                            GvKHS.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data tidak ditemukan');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        // hitung Jumlah SKS dan IP Semester
        int TotalSKS = 0;
        decimal TotalNilai = 0;
        protected void GvKHS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[3].Text);
                TotalSKS += SKS;
                this._TotalSKS = TotalSKS;

                if (e.Row.Cells[5].Text != "&nbsp;")
                {

                    decimal Nilai = Convert.ToDecimal(e.Row.Cells[5].Text);
                    TotalNilai += Nilai;
                    this._TotalNilai = TotalNilai;
                }
                else
                {
                    decimal Nilai = 0;
                    TotalNilai += Nilai;
                    this._TotalNilai = TotalNilai;
                }
            }
        }
    }
}