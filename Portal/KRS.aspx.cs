using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    //public partial class WebForm19 : System.Web.UI.Page
    public partial class WebForm19 : Tu
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        public int _TotalSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalSKS"].ToString()); }
            set { this.ViewState["TotalSKS"] = (object)value; }
        }

        public int _TotalEditSKS
        {
            get { return Convert.ToInt32(this.ViewState["TotalEditSKS"].ToString()); }
            set { this.ViewState["TotalEditSKS"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                this.PanelListKRS.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
                return;
            }
            if (this.RBList.Checked == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Aksi KRS');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }


            //  ------- Read Data Mahasiswa ----------
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbTahun.Text = mhs.thn_angkatan.ToString();
                LbNpm.Text = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();

                if (this.Session["Prodi"].ToString() != mhs.Prodi.ToString())
                {
                    PanelListKRS.Visible = false;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa Tidak Ditemukan');", true);
                    return;
                }
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Kelas";
                LbProdi.Text = "Program Studi";
                LbTahun.Text = "Tahun Angkatan";
                LbNpm.Text = "NPM";
                LbIdProdi.Text = "";

                //clear Gridview
                DataTable TableKP = new DataTable();
                TableKP.Rows.Clear();
                TableKP.Clear();
                //this.GVAmbilKRS.DataSource = TableKP;
                //this.GVAmbilKRS.DataBind();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }

            //--------------------------- LIHAT KRS ------------------------------ //
            if (this.RBList.Checked)
            {
                _TotalSKS = 0;
                _TotalEditSKS = 0;

                try
                {
                    //1. ---------- Gridview SKS ------------------
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        // --------------------- Fill Gridview  ------------------------
                        SqlCommand CmdListKRS = new SqlCommand("SpListKrsMhs2", con);
                        CmdListKRS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdListKRS.Parameters.AddWithValue("@npm", this.TBNpm.Text);
                        CmdListKRS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                        DataTable TableKRS = new DataTable();
                        TableKRS.Columns.Add("Key");
                        TableKRS.Columns.Add("Kode");
                        TableKRS.Columns.Add("Mata Kuliah");
                        TableKRS.Columns.Add("SKS");
                        TableKRS.Columns.Add("Dosen");
                        TableKRS.Columns.Add("Kelas");
                        TableKRS.Columns.Add("Hari");
                        TableKRS.Columns.Add("Mulai");
                        TableKRS.Columns.Add("Selesai");
                        TableKRS.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdListKRS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                this.PanelListKRS.Enabled = true;
                                this.PanelListKRS.Visible = true;

                                while (rdr.Read())
                                {
                                    DataRow datarow = TableKRS.NewRow();
                                    datarow["Key"] = rdr["no_jadwal"];
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    datarow["Dosen"] = rdr["nama"];
                                    datarow["Kelas"] = rdr["kelas"];
                                    datarow["Hari"] = rdr["hari"];
                                    datarow["Mulai"] = rdr["jm_awal_kuliah"];
                                    datarow["Selesai"] = rdr["jm_akhir_kuliah"];
                                    datarow["Ruang"] = rdr["nm_ruang"];

                                    TableKRS.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVListKrs.DataSource = TableKRS;
                                this.GVListKrs.DataBind();

                                this.DLSemester.SelectedIndex = 0;
                            }
                            else
                            {
                                //clear Gridview
                                TableKRS.Rows.Clear();
                                TableKRS.Clear();
                                GVListKrs.DataSource = TableKRS;
                                GVListKrs.DataBind();

                                this.PanelListKRS.Enabled = false;
                                this.PanelListKRS.Visible = false;

                                this.DLSemester.SelectedIndex = 0;

                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KRS Semester Ini Belum Ada');", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.PanelListKRS.Enabled = false;
                    this.PanelListKRS.Visible = false;

                    this.DLSemester.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        int TotalSKS = 0;
        protected void GVListKrs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int SKS = Convert.ToInt32(e.Row.Cells[3].Text);
                TotalSKS += SKS;

                // this._TotalSkripsi = TotalSKS;
                //string FormattedString1 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", SKS);
                //e.Row.Cells[1].Text = FormattedString1;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Jumlah";
                e.Row.Cells[3].Text = TotalSKS.ToString();
                int JumlahTotalSKS = Convert.ToInt32(e.Row.Cells[3].Text);

                //string FormattedString4 = string.Format
                //    (new System.Globalization.CultureInfo("id"), "{0:c}", JumlahTotalSKS);
                //e.Row.Cells[1].Text = FormattedString4;
            }
        }
    }
}