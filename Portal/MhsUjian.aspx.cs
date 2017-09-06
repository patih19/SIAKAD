using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;

namespace Portal
{
    public class Pesertaujian
    {
        public int no { get; set; }
        public string npm { get; set; }
        public string nama { get; set; }
        public string Prodi { get; set; }
        public string Id_Prodi { get; set; }
        public string JenisKelas { get; set; }
        public string Kelas { get; set; }
        public string Jadwal { get; set; }
        public string KdMakul { get; set; }
        public string Makul { get; set; }
        public string Dosen { get; set; }
        public string NIDN { get; set; }
        public string Tahun { get; set; }
        public string Semester { get; set; }
        public string JenisUjian { get; set; }        
    }

    //public partial class WebForm12 : System.Web.UI.Page
    public partial class WebForm12 : Tu
    {
        //------------------- Identifikasi Mahasiswa ------------------ //
        public string Kelas
        {
            get { return this.LbKelas.Text; }
        }
        public string Tahun
        {
            get { return this.DLTahun.SelectedItem.Text; }
        }
        public string Semester
        {
            get { return this.DLSemester.SelectedItem.Text; }
        }
        public string Id_Prodi
        {
            get { return this.LbIdProdi.Text; }
        }
        public string Prodi
        {
            get { return this.LbProdi.Text; }
        }
        public string NIDN
        {
            get { return this.LbNIDN.Text; }
        }
        public string Dosen
        {
            get { return this.LbDosen.Text; }
        }
        public string Kode_Makul
        {
            get { return this.LbKdMakul.Text; }
        }
        public string Makul
        {
            get { return this.LbMakul.Text; }
        }
        public string JenisUjian
        {
            get { return this.DLUjian.SelectedItem.Text; }
        }
        public string Jadwal
        {
            get { return this.LbJadwal.Text; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMakul.Enabled = false;
                this.PanelMakul.Visible = false;

                this.PanelJadwalUjian.Enabled = false;
                this.PanelJadwalUjian.Visible = false;
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.LbKdMakul.Text == "" || this.LbKelas.Text == "" || this.LbNIDN.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jadwal Kuliah');", true);
                return;
            }
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Ujian');", true);
                return;
            }

            //this.LbUjian.Text = this.DLUjian.SelectedItem.Text;
            Server.Transfer("~/am/CetakPesertaUjian.aspx");
        }

        protected void DLUjian_SelectedIndexChanged(object sender, EventArgs e)
        {
            //form validation
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.DLUjian.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }

            if (this.DLTahun.SelectedValue == "Tahun")
            {
                this.DLUjian.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun Akademik');", true);
                return;
            }
            if (this.DLSemester.SelectedValue == "Semester")
            {
                this.DLUjian.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }
            if (this.DLUjian.SelectedValue == "Jenis Ujian")
            {
                this.DLUjian.SelectedIndex = 0;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Ujian');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //// ----------------- Jadwal UTS ------------------------ ////
                if (this.DLUjian.SelectedValue == "uts")
                {
                    //----- Read Jadwal ----
                    con.Open();
                    //SqlCommand CmdJadwalUTS = new SqlCommand("SpJadwalUTS", con);
                    SqlCommand CmdJadwalUTS = new SqlCommand("SpJadwalUTS2", con);
                    CmdJadwalUTS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwalUTS.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwalUTS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                    DataTable TableJadwalUTS = new DataTable();
                    TableJadwalUTS.Columns.Add("Key");
                    TableJadwalUTS.Columns.Add("Kode");
                    TableJadwalUTS.Columns.Add("Mata Kuliah");
                    TableJadwalUTS.Columns.Add("NIDN");
                    TableJadwalUTS.Columns.Add("Dosen");
                    TableJadwalUTS.Columns.Add("Kelas");
                    TableJadwalUTS.Columns.Add("Hari");
                    TableJadwalUTS.Columns.Add("Tanggal");
                    TableJadwalUTS.Columns.Add("Mulai");
                    TableJadwalUTS.Columns.Add("Selesai");
                    TableJadwalUTS.Columns.Add("Ruang");
                    TableJadwalUTS.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwalUTS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelMakul.Enabled = true;
                            this.PanelMakul.Visible = true;

                            this.LbIdProdi.Text = this.Session["level"].ToString();
                            this.LbProdi.Text = this.Session["Prodi"].ToString();

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwalUTS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari_uts"];
                                if (rdr["tgl_uts"] == DBNull.Value)
                                {

                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uts"]);
                                    datarow["Tanggal"] = TglUjian.ToString("dd-MMMM-yyyy");
                                }
                                datarow["Mulai"] = rdr["jam_mulai_uts"];
                                datarow["Selesai"] = rdr["jam_sls_uts"];
                                datarow["Ruang"] = rdr["ruang_uts"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwalUTS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwalUTS;
                            this.GVJadwal.DataBind();
                        }
                        else
                        {
                            this.DLUjian.SelectedIndex = 0;

                            //clear Gridview
                            TableJadwalUTS.Rows.Clear();
                            TableJadwalUTS.Clear();
                            GVJadwal.DataSource = TableJadwalUTS;
                            GVJadwal.DataBind();

                            this.PanelMakul.Enabled = false;
                            this.PanelMakul.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Belum Ada');", true);
                        }
                    }
                }
                //// ----------------- Jadwal UAS ------------------------ ////
                else if (this.DLUjian.SelectedValue == "uas")
                {
                    //----- Read Jadwal ----
                    con.Open();
                    SqlCommand CmdJadwalUTS = new SqlCommand("SpJadwalUAS2", con);
                    CmdJadwalUTS.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwalUTS.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString());
                    CmdJadwalUTS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DLSemester.SelectedItem.Text);

                    DataTable TableJadwalUTS = new DataTable();
                    TableJadwalUTS.Columns.Add("Key");
                    TableJadwalUTS.Columns.Add("Kode");
                    TableJadwalUTS.Columns.Add("Mata Kuliah");
                    TableJadwalUTS.Columns.Add("NIDN");
                    TableJadwalUTS.Columns.Add("Dosen");
                    TableJadwalUTS.Columns.Add("Kelas");
                    TableJadwalUTS.Columns.Add("Hari");
                    TableJadwalUTS.Columns.Add("Tanggal");
                    TableJadwalUTS.Columns.Add("Mulai");
                    TableJadwalUTS.Columns.Add("Selesai");
                    TableJadwalUTS.Columns.Add("Ruang");
                    TableJadwalUTS.Columns.Add("Jenis Kelas");

                    using (SqlDataReader rdr = CmdJadwalUTS.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelMakul.Enabled = true;
                            this.PanelMakul.Visible = true;

                            this.LbIdProdi.Text = this.Session["level"].ToString();
                            this.LbProdi.Text = this.Session["Prodi"].ToString();

                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwalUTS.NewRow();
                                datarow["Key"] = rdr["no_jadwal"];
                                datarow["Kode"] = rdr["kode_makul"];
                                datarow["Mata Kuliah"] = rdr["makul"];
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["Dosen"] = rdr["nama"];
                                datarow["Kelas"] = rdr["kelas"];
                                datarow["Hari"] = rdr["hari_uas"];
                                if (rdr["tgl_uas"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uas"]);
                                    datarow["Tanggal"] = TglUjian.ToString("dd-MMMM-yyyy");
                                }
                                datarow["Mulai"] = rdr["jam_mulai_uas"];
                                datarow["Selesai"] = rdr["jam_sls_uas"];
                                datarow["Ruang"] = rdr["ruang_uas"];
                                datarow["Jenis Kelas"] = rdr["jenis_kelas"];

                                TableJadwalUTS.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVJadwal.DataSource = TableJadwalUTS;
                            this.GVJadwal.DataBind();
                        }
                        else
                        {
                            this.DLUjian.SelectedIndex = 0;

                            //clear Gridview
                            TableJadwalUTS.Rows.Clear();
                            TableJadwalUTS.Clear();
                            GVJadwal.DataSource = TableJadwalUTS;
                            GVJadwal.DataBind();

                            this.PanelMakul.Enabled = false;
                            this.PanelMakul.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Jadwal Belum Ada');", true);
                        }
                    }
                }
            }
        }

        protected void BtnCetak_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //Response.Write( this.GVJadwal.Rows[index].Cells[3].Text);

            //select Kode dan Makul
            this.LbIdProdi.Text = this.Session["level"].ToString();
            this.LbProdi.Text = this.Session["Prodi"].ToString();
            this.LbKdMakul.Text = this.GVJadwal.Rows[index].Cells[1].Text;
            this.LbMakul.Text = this.GVJadwal.Rows[index].Cells[2].Text;
            this.LbNIDN.Text = this.GVJadwal.Rows[index].Cells[3].Text;
            this.LbDosen.Text = this.GVJadwal.Rows[index].Cells[4].Text;
            this.LbKelas.Text = this.GVJadwal.Rows[index].Cells[5].Text;
            this.LbJadwal.Text = this.GVJadwal.Rows[index].Cells[6].Text + ", " + this.GVJadwal.Rows[index].Cells[7].Text + "," + this.GVJadwal.Rows[index].Cells[8].Text + "-" +
                                this.GVJadwal.Rows[index].Cells[9].Text + ", Ruang " + this.GVJadwal.Rows[index].Cells[10].Text;

            this.DLTahun.SelectedItem.Text = this.DLTahun.SelectedValue;
            this.DLSemester.SelectedItem.Text = this.DLSemester.SelectedValue;
            this.DLUjian.SelectedItem.Text = this.DLUjian.SelectedItem.Text.ToUpper();

            Server.Transfer("~/CetakPesertaUjian.aspx");
        }

        protected void GVJadwal_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVJadwal.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void GVJadwal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false; //Key
            e.Row.Cells[1].Visible = false; //Kode Makul
            e.Row.Cells[3].Visible = false; //NIDN 
        }

        protected void GVJadwal_PreRender(object sender, EventArgs e)
        {
            if (this.GVJadwal.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVJadwal.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVJadwal.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }

        protected void LnkDownExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // get row index
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    if (this.DLUjian.SelectedItem.Text.ToUpper() == "UJIAN TENGAH SEMESTER")
                    {
                        List<Pesertaujian> PesertaUTS = new List<Pesertaujian>();
                        Pesertaujian JadwalPeserta = new Pesertaujian();

                        JadwalPeserta.Id_Prodi = this.Session["level"].ToString().Trim();
                        JadwalPeserta.Prodi = this.Session["Prodi"].ToString().Trim();
                        JadwalPeserta.KdMakul = this.GVJadwal.Rows[index].Cells[1].Text.Trim();
                        JadwalPeserta.Makul = this.GVJadwal.Rows[index].Cells[2].Text.Trim();
                        JadwalPeserta.NIDN = this.GVJadwal.Rows[index].Cells[3].Text.Trim();
                        JadwalPeserta.Dosen = this.GVJadwal.Rows[index].Cells[4].Text.Trim();
                        JadwalPeserta.Kelas = this.GVJadwal.Rows[index].Cells[5].Text.Trim();
                        JadwalPeserta.JenisKelas = this.GVJadwal.Rows[index].Cells[11].Text.Trim();
                        JadwalPeserta.Jadwal = this.GVJadwal.Rows[index].Cells[6].Text.Trim() + ", " + this.GVJadwal.Rows[index].Cells[7].Text.Trim() + "," + this.GVJadwal.Rows[index].Cells[8].Text.Trim() + "-" +
                                            this.GVJadwal.Rows[index].Cells[9].Text.Trim() + ", Ruang " + this.GVJadwal.Rows[index].Cells[10].Text.Trim();
                        JadwalPeserta.JenisUjian = this.DLUjian.SelectedItem.Text.ToUpper().Trim();
                        JadwalPeserta.Tahun = this.DLTahun.SelectedValue.Trim();
                        JadwalPeserta.Semester = this.DLSemester.SelectedValue.Trim();


                        SqlCommand CmdUTS = new SqlCommand("SpPesertaUTS", con);
                        CmdUTS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUTS.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString().Trim());
                        CmdUTS.Parameters.AddWithValue("@nidn", JadwalPeserta.NIDN);
                        CmdUTS.Parameters.AddWithValue("@kode_makul", JadwalPeserta.KdMakul);
                        CmdUTS.Parameters.AddWithValue("@kelas", JadwalPeserta.Kelas);
                        CmdUTS.Parameters.AddWithValue("@semester", JadwalPeserta.Tahun + JadwalPeserta.Semester);

                        using (SqlDataReader rdr = CmdUTS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    Pesertaujian NewPeserta = new Pesertaujian();
                                    NewPeserta.npm = rdr["npm"].ToString();
                                    NewPeserta.nama = rdr["nama"].ToString();

                                    PesertaUTS.Add(NewPeserta);
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Peserta Ujian');", true);
                                return;
                            }
                        }
                        ToExcel(PesertaUTS, JadwalPeserta);

                    }
                    else if (this.DLUjian.SelectedItem.Text.ToUpper() == "UJIAN AKHIR SEMESTER")
                    {
                        List<Pesertaujian> PesertaUAS = new List<Pesertaujian>();
                        Pesertaujian JadwalPeserta = new Pesertaujian();

                        JadwalPeserta.Id_Prodi = this.Session["level"].ToString().Trim();
                        JadwalPeserta.Prodi = this.Session["Prodi"].ToString().Trim();
                        JadwalPeserta.KdMakul = this.GVJadwal.Rows[index].Cells[1].Text.Trim();
                        JadwalPeserta.Makul = this.GVJadwal.Rows[index].Cells[2].Text.Trim();
                        JadwalPeserta.NIDN = this.GVJadwal.Rows[index].Cells[3].Text.Trim();
                        JadwalPeserta.Dosen = this.GVJadwal.Rows[index].Cells[4].Text.Trim();
                        JadwalPeserta.Kelas = this.GVJadwal.Rows[index].Cells[5].Text.Trim();
                        JadwalPeserta.JenisKelas = this.GVJadwal.Rows[index].Cells[11].Text.Trim();
                        JadwalPeserta.Jadwal = this.GVJadwal.Rows[index].Cells[6].Text.Trim() + ", " + this.GVJadwal.Rows[index].Cells[7].Text.Trim() + "," + this.GVJadwal.Rows[index].Cells[8].Text.Trim() + "-" +
                                            this.GVJadwal.Rows[index].Cells[9].Text.Trim() + ", Ruang " + this.GVJadwal.Rows[index].Cells[10].Text.Trim();
                        JadwalPeserta.JenisUjian = this.DLUjian.SelectedItem.Text.ToUpper().Trim();
                        JadwalPeserta.Tahun = this.DLTahun.SelectedValue.Trim();
                        JadwalPeserta.Semester = this.DLSemester.SelectedValue.Trim();


                        SqlCommand CmdUAS = new SqlCommand("SpPesertaUAS", con);
                        CmdUAS.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUAS.Parameters.AddWithValue("@id_prodi", this.Session["level"].ToString().Trim());
                        CmdUAS.Parameters.AddWithValue("@nidn", JadwalPeserta.NIDN);
                        CmdUAS.Parameters.AddWithValue("@kode_makul", JadwalPeserta.KdMakul);
                        CmdUAS.Parameters.AddWithValue("@kelas", JadwalPeserta.Kelas);
                        CmdUAS.Parameters.AddWithValue("@semester", JadwalPeserta.Tahun + JadwalPeserta.Semester);

                        using (SqlDataReader rdr = CmdUAS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    Pesertaujian NewPeserta = new Pesertaujian();
                                    NewPeserta.npm = rdr["npm"].ToString();
                                    NewPeserta.nama = rdr["nama"].ToString();

                                    PesertaUAS.Add(NewPeserta);
                                }
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tidak Ada Peserta Ujian');", true);
                                return;
                            }
                        }
                        ToExcel(PesertaUAS, JadwalPeserta);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                return;
            }
        }

        public void ToExcel(List<Pesertaujian> PesertaUjian, Pesertaujian JadwalPeserta)
        {
            // ------------ to excel ---------------------
            // build excel apps
            Excel.Application app = null;
            Excel.Workbooks books = null;
            Excel.Workbook book = null;
            Excel.Sheets sheets = null;

            // create apps object
            var excelApp = new Excel.Application();
            excelApp.DefaultSaveFormat = Excel.XlFileFormat.xlOpenXMLWorkbook;

            try
            {
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                excelApp.Workbooks.Add();
                Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // text keterangan
                workSheet.Cells[1, "A"] = "DAFTAR PESERTA UJIAN "+ JadwalPeserta.JenisUjian.ToUpper();

                workSheet.Cells[3, "A"] = "Mata Kuliah/Kelas";
                workSheet.Cells[4, "A"] = "Tahun/Semester";
                workSheet.Cells[5, "A"] = "Jadwal";
                workSheet.Cells[6, "A"] = "Dosen Pengampu";

                workSheet.Cells[3, "C"] = JadwalPeserta.Makul + "/" + JadwalPeserta.Kelas;
                workSheet.Cells[4, "C"] = JadwalPeserta.Tahun + "/" + JadwalPeserta.Semester; ;
                workSheet.Cells[5, "C"] = JadwalPeserta.Jadwal;
                workSheet.Cells[6, "C"] = JadwalPeserta.Dosen;

                workSheet.Cells[8, "A"] = "NO";
                workSheet.Cells[8, "B"] = "NPM";
                workSheet.Cells[8, "C"] = "NAMA";
                workSheet.Cells[8, "D"] = "NILAI ANGKA";
                workSheet.Cells[8, "E"] = "NILAI HURUF";
                workSheet.Cells[8, "F"] = "TANDA TANGAN";

                // loop 
                var row = 8;
                foreach (var mhs in PesertaUjian)
                {
                    row++;
                    workSheet.Cells[row, "A"] = "";
                    workSheet.Cells[row, "B"] = mhs.npm;
                    workSheet.Cells[row, "C"] = mhs.nama;
                }

                //tanda tangan
                workSheet.Cells[row + 3, "B"] = "Ketua Jurusan";
                workSheet.Cells[row + 7, "B"] = "...";
                workSheet.Cells[row + 2, "E"] = "Magelang, ...";
                workSheet.Cells[row + 3, "E"] = "Dosen Pengampu,";
                workSheet.Cells[row + 7, "E"] = JadwalPeserta.Dosen.Trim();

                // autofit
                workSheet.Columns[1].ColumnWidth = 3;
                workSheet.Columns[2].Autofit();
                workSheet.Columns[3].Autofit();
                workSheet.Columns[4].Autofit();
                //workSheet.Columns[5].Autofit();
                workSheet.Columns[6].Autofit();

                // text align left
                workSheet.get_Range("A1", "D" + row.ToString()).Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                // border nilai
                var range = workSheet.get_Range("A8", "F" + row.ToString());
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

                // save excel file
                //string excelFile = "C:\\PesertaUjian\\" + JadwalPeserta.Makul.Trim() + "-" + JadwalPeserta.Kelas.Trim() + "-" + JadwalPeserta.JenisKelas.Trim() + "-" + JadwalPeserta.Tahun.Trim() + JadwalPeserta.Semester.Trim();
                string excelFile = Server.MapPath("~/PesertaUjian/") + JadwalPeserta.Makul.Trim() + "-" + JadwalPeserta.Kelas.Trim() + "-" + JadwalPeserta.JenisKelas.Trim() + "-" + JadwalPeserta.Tahun.Trim() + JadwalPeserta.Semester.Trim();
                workSheet.SaveAs(excelFile, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);

                // quit excel apps
                excelApp.Workbooks.Close();
                excelApp.Quit();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());

                // quit excel apps
                excelApp.Workbooks.Close();
                excelApp.Quit();

                throw ex;
            }
            finally
            {
                // release excel object 
                if (sheets != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                if (book != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                if (books != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
                if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }


            try
            {
                // FileInfo fileInfo = new FileInfo(Server.MapPath("~/Nilai/coba.xlsx"));
                // C:\inetpub\wwwroot\portal
                // FileInfo fileInfo = new FileInfo("C:\\PesertaUjian\\" + JadwalPeserta.Makul.Trim() + "-" + JadwalPeserta.Kelas.Trim() + "-" + JadwalPeserta.JenisKelas.Trim() + "-" + JadwalPeserta.Tahun.Trim() + JadwalPeserta.Semester.Trim() + ".xlsx");
                FileInfo fileInfo = new FileInfo(Server.MapPath("~/PesertaUjian/") + JadwalPeserta.Makul.Trim() + "-" + JadwalPeserta.Kelas.Trim() + "-" + JadwalPeserta.JenisKelas.Trim() + "-" + JadwalPeserta.Tahun.Trim() + JadwalPeserta.Semester.Trim() + ".xlsx");
                Response.Clear();
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileInfo.Name));//"attachment;filename=" + fileInfo.Name);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(fileInfo.FullName);
                Response.End();

                // kill excel proccess
                Process[] pProcess;
                pProcess = Process.GetProcessesByName("Excel");
                pProcess[0].Kill();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                throw ex;

            }
        }

    }
}