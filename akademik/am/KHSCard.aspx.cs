using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace akademik
{
    public partial class KHSCard : System.Web.UI.Page
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        // string id_prodi
        string id_prodi;

        //View State KHS
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
            if (!Page.IsPostBack)
            {
                Page lastpage = (Page)Context.Handler;
                if (lastpage is akademik.am.WebForm11)
                {
                    try
                    {
                        //1. ----- set keterangan mahasiswa from last page
                        this.LbNpm.Text = ((akademik.am.WebForm11)lastpage).NPM;
                        this.LbJenisKelas.Text = ((akademik.am.WebForm11)lastpage).JenisKelas;
                        this.LbTahun.Text = ((akademik.am.WebForm11)lastpage).Tahun;
                        this.LbSemester.Text = ((akademik.am.WebForm11)lastpage).Semester;
                        this.LbProdi.Text = ((akademik.am.WebForm11)lastpage).Prodi;
                        this.LbNama.Text = ((akademik.am.WebForm11)lastpage).Name;
                        this.id_prodi = ((akademik.am.WebForm11)lastpage).Id_Prodi;

                        //2. ---------- Gridview SKS ------------------
                        string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(CS))
                        {
                            con.Open();
                            // --------------------- Fill Gridview  ------------------------
                            SqlCommand CmdListKRS = new SqlCommand("SpGetKHS", con);
                            CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;


                            CmdListKRS.Parameters.AddWithValue("@npm", ((akademik.am.WebForm11)lastpage).NPM);
                            CmdListKRS.Parameters.AddWithValue("@semester", ((akademik.am.WebForm11)lastpage).Tahun + ((akademik.am.WebForm11)lastpage).Semester);

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
                                    this.GVMakul.DataSource = TableKRS;
                                    this.GVMakul.DataBind();

                                    //Set Label
                                    this.LBSks.Text = _TotalSKS.ToString();
                                    decimal IPS = _TotalNilai / _TotalSKS;
                                    this.LbIPS.Text = String.Format("{0:0.##}", IPS); 

                                }
                                else
                                {
                                    //clear Gridview
                                    TableKRS.Rows.Clear();
                                    TableKRS.Clear();
                                    GVMakul.DataSource = TableKRS;
                                    GVMakul.DataBind();
                                }
                            }
                        }

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
                    }
                    catch (Exception ex)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                }
            }
        }

        // hitung Jumlah SKS dan IP Semester
        int TotalSKS = 0;
        decimal TotalNilai = 0;
        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
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