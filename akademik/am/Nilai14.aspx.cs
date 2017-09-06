using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace akademik.am
{
    public partial class WebForm24 : Bak_Admin
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        public string _IDProdi
        {
            get { return this.ViewState["IDProdi"].ToString(); }
            set { this.ViewState["IDProdi"] = (object)value; }
        }

        public string _Tahun
        {
            get { return this.ViewState["Tahun"].ToString(); }
            set { this.ViewState["Tahun"] = (object)value; }
        }

        public string _Semester
        {
            get { return this.ViewState["Semester"].ToString(); }
            set { this.ViewState["Semester"] = (object)value; }
        }

        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _IDProdi = "";
                _Tahun = "";
                _Semester = "";
                _NPM = "";

                this.PanelMataKuliah.Enabled = false;
                this.PanelMataKuliah.Visible = false;
            }
        }

        protected void GVPeserta_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVMakul.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void BtnCari_Click(object sender, EventArgs e)
        {
            //------ 1. form validation ----- //
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
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

            // ------- 2. Read Mahasiswa ---------- //
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbKelas.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbNpm.Text = mhs.npm.ToString();
                _NPM = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();
                LbAngkatan.Text = mhs.thn_angkatan.ToString();
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbKelas.Text = "Jenis Kelas";
                LbProdi.Text = "Program Studi";
                LbNpm.Text = "NPM";

                this.PanelMataKuliah.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa Tidak Ditemukan');", true);
                return;
            }

            // ---------- Cek Tahun ------------ //
            _NPM = TBNpm.Text;
            _Semester = this.DLSemester.SelectedValue;
            _Tahun = DLTahun.SelectedValue;

            if (_Tahun != "2014/2015")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Hanya Untuk Tahun Akademik 2014/2015');", true);
                return;
            }
            if (LbAngkatan.Text != "2014/2015")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Input Hanya Untuk Mahasiswa Tahun Angkatan 2014/2015');", true);
                return;
            }

            // ----------- 3. Display Mata Kuliah -------------//
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdMakul = new SqlCommand("SpGetMakul", con);
                CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                CmdMakul.Parameters.AddWithValue("@id_prodi", this.LbIdProdi.Text);

                DataTable TableMakul = new DataTable();
                TableMakul.Columns.Add("Kode");
                TableMakul.Columns.Add("Mata Kuliah");
                TableMakul.Columns.Add("SKS");

                using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.PanelMataKuliah.Enabled = true;
                        this.PanelMataKuliah.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableMakul.NewRow();
                            datarow["Kode"] = rdr["kode_makul"];
                            datarow["Mata Kuliah"] = rdr["makul"];
                            datarow["SKS"] = rdr["sks"];

                            TableMakul.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVMakul.DataSource = TableMakul;
                        this.GVMakul.DataBind();

                    }
                    else
                    {
                        //clear Gridview
                        TableMakul.Rows.Clear();
                        TableMakul.Clear();
                        GVMakul.DataSource = TableMakul;
                        GVMakul.DataBind();

                        this.PanelMataKuliah.Enabled = false;
                        this.PanelMataKuliah.Visible = false;
                    }
                }


                // ----------------- 4. Loop Nilai ------------------
                //-- loop Nilai yg sudah diambil berdasarkan npm, makul, semester
                for (int i = 0; i < this.GVMakul.Rows.Count; i++)
                {
                    SqlCommand CmdChecked = new SqlCommand("SpGetNilai", con);
                    CmdChecked.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdChecked.Parameters.AddWithValue("@npm", LbNpm.Text);
                    CmdChecked.Parameters.AddWithValue("@kode_makul", this.GVMakul.Rows[i].Cells[0].Text);
                    CmdChecked.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                    using (SqlDataReader rdrchecked = CmdChecked.ExecuteReader())
                    {
                        if (rdrchecked.HasRows)
                        {
                            while (rdrchecked.Read())
                            {
                                DropDownList DLNilai = (DropDownList)this.GVMakul.Rows[i].FindControl("DLNilai");

                                if (rdrchecked["nilai"] == DBNull.Value)
                                {
                                    DLNilai.SelectedItem.Text = "Nilai";
                                }
                                else
                                {
                                    DLNilai.SelectedItem.Text = rdrchecked["nilai"].ToString();
                                }
                            }
                        }
                    }
                }

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

        }
    }
}