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
    public partial class Transkrip : Tu
    {
        // Create Mahasiswa Object
        Mhs mhs = new Mhs();

        public decimal _TotalSKS
        {
            get { return Convert.ToDecimal(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        public decimal _TotalBobot
        {
            get { return Convert.ToDecimal(this.ViewState["TotalBobot"].ToString()); }
            set { this.ViewState["TotalBobot"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelNilai.Enabled = false;
                this.PanelNilai.Visible = false;

                _TotalBobot = 0;
                _TotalSKS = 0;
            }
        }

        protected void BtnFilterMhs_Click(object sender, EventArgs e)
        {
            //// ------- Read Thn Mahasiswa ----------
            //mhs.ReadMahasiswa(this.Session["Name"].ToString());

            //string ThnMhs = (mhs.thn_angkatan.ToString().Replace("/", "_"));

            //if (Convert.ToInt32(mhs.thn_angkatan.ToString().Substring(0, 4)) < 2015)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tahun '" + ThnMhs + "' Belum Dapat Ditampilkan Online');", true);
            //    return;
            //}
            _TotalBobot = 0;
            _TotalSKS = 0;


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                string StrTopSemester = "";

                // --- Top Semester 
                // --- D3 sama dengan S1 --
                SqlCommand TopSemester = new SqlCommand("SELECT TOP 1 semester FROM dbo.bak_kal WHERE jenjang='S1' AND semester <> 'new' ORDER BY semester DESC", con);
                TopSemester.CommandType = CommandType.Text;

                using (SqlDataReader rdr = TopSemester.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            StrTopSemester = rdr["semester"].ToString().Trim();
                        }
                    }
                }

                // -- Cek Masa INPUT NILAI
                // -- Tidak Diperbolehkan Pada Saat Masa Input NILAI --
                SqlCommand CmdCekMasa = new SqlCommand("SpCekMasaKeg", con);
                CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                CmdCekMasa.Parameters.AddWithValue("@semester", StrTopSemester);
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

                // --------------------- Read Biodata Mahasiswa ------------------- //
                mhs.ReadMahasiswa(this.TBNpm.Text.Trim());

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();
                //LbIdProdi.Text = mhs.id_prodi.ToString();

                if (Convert.ToInt32(mhs.thn_angkatan.ToString().Trim().Substring(0, 4)) < 2015)
                {
                    this.PanelNilai.Enabled = false;
                    this.PanelNilai.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Transkrip Nilai Mahasiswa Lama (PTS) Tidak Dapat Ditamplkan');", true);
                    return;
                }

                if (this.Session["level"].ToString().Trim() != mhs.id_prodi.ToString().Trim())
                {
                    this.PanelNilai.Enabled = false;
                    this.PanelNilai.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Program Studi Tidak Terdaftar');", true);
                    return;
                }

                // --------------------- Fill Gridview  ------------------------
                SqlCommand CmdKRS = new SqlCommand("SpGetTranskrip", con);
                CmdKRS.CommandType = System.Data.CommandType.StoredProcedure;

                CmdKRS.Parameters.AddWithValue("@npm", this.TBNpm.Text.Trim());

                DataTable TableKRS = new DataTable();
                TableKRS.Columns.Add("KODE");
                TableKRS.Columns.Add("MATA KULIAH");
                TableKRS.Columns.Add("SKS");
                TableKRS.Columns.Add("NILAI");
                TableKRS.Columns.Add("BOBOT");

                using (SqlDataReader rdr = CmdKRS.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {

                        this.PanelNilai.Enabled = true;
                        this.PanelNilai.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableKRS.NewRow();
                            datarow["KODE"] = rdr["kode_makul"];
                            datarow["MATA KULIAH"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];
                            if (rdr["Nilai"] == DBNull.Value)
                            {
                                //datarow["Nilai"] = "";
                            }
                            else
                            {
                                datarow["Nilai"] = rdr["nilai"];
                            }
                            datarow["BOBOT"] = rdr["jumlah"];
                            TableKRS.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVTrans.DataSource = TableKRS;
                        this.GVTrans.DataBind();

                        //Set Label
                        decimal Trans = (_TotalBobot / _TotalSKS);
                        this.LbIPK.Text = String.Format("{0:0.##}", Trans);

                        //this.BtnPrint.Enabled = true;
                        //this.BtnPrint.Visible = true;

                    }
                    else
                    {
                        //clear Gridview
                        TableKRS.Rows.Clear();
                        TableKRS.Clear();
                        GVTrans.DataSource = TableKRS;
                        GVTrans.DataBind();

                        this.PanelNilai.Enabled = false;
                        this.PanelNilai.Visible = false;

                        //this.PanelTranskrip.Visible = false;

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Nilai Tidak Ditemukan');", true);
                        return;
                    }
                }
            }
        }

        protected void GVTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal SKS = Convert.ToInt32(e.Row.Cells[2].Text);
                _TotalSKS += SKS;

                // nilai kosong
                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    decimal Bobot = Convert.ToDecimal(e.Row.Cells[4].Text);
                    _TotalBobot += Bobot;
                }

                // this._TotalSkripsi = TotalSKS;
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                //e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Jumlah";

                e.Row.Cells[2].Text = _TotalSKS.ToString();
                e.Row.Cells[4].Text = _TotalBobot.ToString();


                //string FormattedString4 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTotalSKS);
                //e.Row.Cells[1].Text = FormattedString4;
            }
        }
    }
}