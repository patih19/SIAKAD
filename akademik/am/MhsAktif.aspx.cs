using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace akademik.am
{
    //public partial class WebForm30 : System.Web.UI.Page Bak_staff
    public partial class WebForm30 : Bak_staff
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LbKetSelect.Text = "";

                PopulateProdi();
            }
        }

        private void PopulateProdi()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));

            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void DataBindRepeater(int pageIndex, int pageSize, int totalRows)
        {
            int totalPage = totalRows / pageSize;
            if ((totalRows % pageSize) != 0)
            {
                totalPage += 1;
            }

            List<ListItem> pages = new List<ListItem>();
            if (totalPage > 1)
            {
                for (int i = 1; i <= totalPage; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != (pageIndex + 1)));
                }
            }

            Repeater1.DataSource = pages;
            Repeater1.DataBind();
        }

        protected void GVMhs_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVMhs.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        private void LoadGVStatus(int PageIndex, int PageSize)
        {
            // TAHUN ANGKATAN
            if (this.TbAngkatan.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tahun Angkatan');", true);
                return;
            }

            // PRODI
            if (this.DLProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }

            //// ---------------- Mahasiswa CUTI/DO/DOUBLE DEGREE/KELUAR/NON-AKTIF ----------------
            int TotalRows = 0;
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // --------------------- Fill Gridview  ------------------------
                SqlCommand CmdLls = new SqlCommand("GetMhsByStatus2", con);
                CmdLls.CommandType = System.Data.CommandType.StoredProcedure;

                CmdLls.Parameters.AddWithValue("@index", PageIndex);
                CmdLls.Parameters.AddWithValue("@size", this.GVMhs.PageSize);
                CmdLls.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);
                CmdLls.Parameters.AddWithValue("@angkatan", this.TbAngkatan.Text);

                if (this.DLStatus.SelectedValue != "-1")
                {
                    if (this.DLStatus.SelectedValue != "All")
                    {
                        SqlParameter StatusParam = new SqlParameter("@status", this.DLStatus.SelectedValue);
                        CmdLls.Parameters.Add(StatusParam);
                    }
                }

                if (this.DLSex.SelectedValue != "-1")
                {
                    SqlParameter GenderParam = new SqlParameter("@gender", this.DLSex.SelectedItem.Text);
                    CmdLls.Parameters.Add(GenderParam);
                }

                if (this.DLReligi.SelectedValue != "-1")
                {
                    SqlParameter AgamaParam = new SqlParameter("@agama", this.DLReligi.SelectedValue);
                    CmdLls.Parameters.Add(AgamaParam);
                }

                // -------- FILTER PROV DAN KABUPATEN -----//
                // -------- FILTER KABUPATEN SAJA TIDAK DISEDIAKAN ------ //

                if (this.DLProv.SelectedItem.Text != string.Empty)
                {
                    // --- Filter By Provinsi dan Kabupaten --- //
                    if (this.DLKotaKab.SelectedItem.Text != string.Empty)
                    {
                        // validasi kriteria pencarian
                        if ((this.PilihanKotaKab.SelectedValue != "=" && this.PilihanKotaKab.SelectedValue != "!=") && (this.PilihanProv.SelectedValue != "=" && this.PilihanProv.SelectedValue != "!="))
                        {
                            con.Close();
                            con.Dispose();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JENIS PENCARIAN');", true);
                            return;
                        }

                        SqlParameter ProvParam = new SqlParameter("@provinsi", this.DLProv.SelectedItem.Text);
                        CmdLls.Parameters.Add(ProvParam);

                        SqlParameter KotaKabParam = new SqlParameter("@kotakab", this.DLKotaKab.SelectedItem.Text);
                        CmdLls.Parameters.Add(KotaKabParam);

                        SqlParameter VarProv = new SqlParameter("@varprov", this.PilihanProv.SelectedValue);
                        CmdLls.Parameters.Add(VarProv);

                        SqlParameter VarKotaKab = new SqlParameter("@varkota", this.PilihanKotaKab.SelectedValue);
                        CmdLls.Parameters.Add(VarKotaKab);

                    }
                    // --- Filter By Provinsi Saja --- //
                    else
                    {
                        // validasi kriteria pencarian
                        if  (this.PilihanProv.SelectedValue != "=" && this.PilihanProv.SelectedValue != "!=")
                        {
                            con.Close();
                            con.Dispose();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JENIS PENCARIAN PROVINSI');", true);
                            return;
                        }

                        SqlParameter ProvParam = new SqlParameter("@provinsi", this.DLProv.SelectedItem.Text);
                        CmdLls.Parameters.Add(ProvParam);

                        SqlParameter VarProv = new SqlParameter("@varprov", this.PilihanProv.SelectedValue);
                        CmdLls.Parameters.Add(VarProv);
                    }
                }
                // Filter Biasa (Tanpa Provinsi dan atau Kota/Kab)
                else                
                {

                }

                SqlParameter TotalRow = new SqlParameter();
                TotalRow.ParameterName = "@totalRow";
                TotalRow.SqlDbType = System.Data.SqlDbType.Int;
                TotalRow.Size = 20;
                TotalRow.Direction = System.Data.ParameterDirection.Output;
                CmdLls.Parameters.Add(TotalRow);

                DataTable TableKRS = new DataTable();
                TableKRS.Columns.Add("No");
                TableKRS.Columns.Add("NPM");
                TableKRS.Columns.Add("Nama");
                TableKRS.Columns.Add("Program Studi");
                TableKRS.Columns.Add("Tahun Angkatan");
                TableKRS.Columns.Add("Jenis Kelas");
                TableKRS.Columns.Add("Status");

                using (SqlDataReader rdr = CmdLls.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKRS.NewRow();
                            datarow["No"] = rdr["urutan"];
                            datarow["NPM"] = rdr["npm"];
                            datarow["Nama"] = rdr["nama"];
                            datarow["Program Studi"] = rdr["prog_study"];
                            datarow["Tahun Angkatan"] = rdr["thn_angkatan"];
                            datarow["Jenis Kelas"] = rdr["kelas"];

                            //----- status ------
                            string st = rdr["status"].ToString();
                            if (rdr["status"].ToString() == "A")
                            {
                                datarow["STATUS"] = "AKTIF";
                            }
                            else if (rdr["status"].ToString() == "C")
                            {
                                datarow["STATUS"] = "CUTI";
                            }
                            else if (rdr["status"].ToString() == "D")
                            {
                                datarow["STATUS"] = "DROP-OUT";
                            }
                            else if (rdr["status"].ToString() == "G")
                            {
                                datarow["STATUS"] = "DOUBLE DEGREE";
                            }
                            else if (rdr["status"].ToString() == "K")
                            {
                                datarow["STATUS"] = "KELUAR";
                            }
                            else if (rdr["status"].ToString() == "N")
                            {
                                datarow["STATUS"] = "NON-AKTIF";
                            }
                            else if (rdr["status"].ToString() == "L")
                            {
                                datarow["STATUS"] = "LULUS";
                            }

                            TableKRS.Rows.Add(datarow);
                        }

                        rdr.Close();
                        TotalRows = (int)CmdLls.Parameters["@totalRow"].Value;

                        //this.DLKelas.SelectedIndex = 0;

                        //Fill Gridview
                        this.GVMhs.DataSource = TableKRS;
                        this.GVMhs.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableKRS.Rows.Clear();
                        TableKRS.Clear();
                        GVMhs.DataSource = TableKRS;
                        GVMhs.DataBind();

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                    }
                }
            }

            //paging
            DataBindRepeater(PageIndex, this.GVMhs.PageSize, TotalRows);

        }

        private void LoadGridViewNama(int PageIndex, int PageSize)
        {
            //validation
            if (this.TbSrcNama2.Text != string.Empty)
            {
                if (this.TbSrcNama2.Text.Length < 4)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA LEBIH DARI 4 HURUF');", true);
                    return;
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('NPM/NAMA HARUS DIISI');", true);
                return;
            }


            int TotalRows = 0;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // --------------------- Fill Gridview  ------------------------
                SqlCommand CmdKRS = new SqlCommand("SpMhsByNamaNpm", con);
                CmdKRS.CommandType = System.Data.CommandType.StoredProcedure;

                CmdKRS.Parameters.AddWithValue("@index", PageIndex);
                CmdKRS.Parameters.AddWithValue("@size", this.GVMhs.PageSize);
                CmdKRS.Parameters.AddWithValue("@nama", this.TbSrcNama2.Text);

                SqlParameter TotalRow = new SqlParameter();
                TotalRow.ParameterName = "@totalRow";
                TotalRow.SqlDbType = System.Data.SqlDbType.Int;
                TotalRow.Size = 20;
                TotalRow.Direction = System.Data.ParameterDirection.Output;
                CmdKRS.Parameters.Add(TotalRow);

                DataTable TableKRS = new DataTable();
                TableKRS.Columns.Add("No");
                TableKRS.Columns.Add("NPM");
                TableKRS.Columns.Add("NAMA");
                TableKRS.Columns.Add("PROGRAM STUDI");
                TableKRS.Columns.Add("TAHUN ANGKATAN");
                TableKRS.Columns.Add("JENIS KELAS");
                TableKRS.Columns.Add("STATUS");

                using (SqlDataReader rdr = CmdKRS.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableKRS.NewRow();
                            datarow["No"] = rdr["urutan"];
                            datarow["NPM"] = rdr["npm"];
                            datarow["NAMA"] = rdr["nama"];
                            datarow["PROGRAM STUDI"] = rdr["prog_study"];
                            datarow["TAHUN ANGKATAN"] = rdr["thn_angkatan"];
                            datarow["JENIS KELAS"] = rdr["kelas"];

                            //----- status ------
                            string st = rdr["status"].ToString();
                            if (rdr["status"].ToString() == "A")
                            {
                                datarow["STATUS"] = "AKTIF";
                            }
                            else if (rdr["status"].ToString() == "C")
                            {
                                datarow["STATUS"] = "CUTI";
                            }
                            else if (rdr["status"].ToString() == "D")
                            {
                                datarow["STATUS"] = "DROP-OUT";
                            }
                            else if (rdr["status"].ToString() == "G")
                            {
                                datarow["STATUS"] = "DOUBLE DEGREE";
                            }
                            else if (rdr["status"].ToString() == "K")
                            {
                                datarow["STATUS"] = "KELUAR";
                            }
                            else if (rdr["status"].ToString() == "N")
                            {
                                datarow["STATUS"] = "NON-AKTIF";
                            }
                            else if (rdr["status"].ToString() == "L")
                            {
                                datarow["STATUS"] = "LULUS";
                            }

                            TableKRS.Rows.Add(datarow);
                        }

                        rdr.Close();
                        TotalRows = (int)CmdKRS.Parameters["@totalRow"].Value;

                        //this.DLKelas.SelectedIndex = 0;

                        //Fill Gridview
                        this.GVMhs.DataSource = TableKRS;
                        this.GVMhs.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableKRS.Rows.Clear();
                        TableKRS.Clear();
                        GVMhs.DataSource = TableKRS;
                        GVMhs.DataBind();
                    }
                }
            }

            //paging
            DataBindRepeater(PageIndex, this.GVMhs.PageSize, TotalRows);
        }

        protected void BtnSrc_Click(object sender, EventArgs e)
        {
            this.LbKetSelect.Text = "advance";
            LoadGVStatus(0, this.GVMhs.PageSize);
        }

        protected void BtnSrc2_Click(object sender, EventArgs e)
        {
            this.LbKetSelect.Text = "search";
            LoadGridViewNama(0, this.GVMhs.PageSize);
        }

        protected void PageButton_Click(object sender, EventArgs e)
        {
            if (this.LbKetSelect.Text == "advance")
            {
                int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                PageIndex -= 1;
                GVMhs.PageIndex = PageIndex;

                LoadGVStatus(PageIndex, this.GVMhs.PageSize);
            }
            else if (this.LbKetSelect.Text == "search")
            {
                int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                PageIndex -= 1;
                GVMhs.PageIndex = PageIndex;

                LoadGridViewNama(PageIndex, this.GVMhs.PageSize);
            }
        }

        string npm;
        string StringProv;
        string StringKota;
        string StringKab;
        protected void BtnLihat_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            this.LbNPM.Text = this.GVMhs.Rows[index].Cells[1].Text;
            npm = this.GVMhs.Rows[index].Cells[1].Text;

            // Keterangan atau Biodata Mahasiswa
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //SqlTransaction trans = con.BeginTransaction();

                SqlCommand CmdBiodata = new SqlCommand("SpBiodataMhs", con);
                //CmdPeriodik.Transaction = trans;
                CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                CmdBiodata.Parameters.AddWithValue("@npm", npm);
                // CmdBiodata.Parameters.AddWithValue("@no_tagihan", this.GVPendaftar.SelectedRow.Cells[1].Text);

                using (SqlDataReader rdr = CmdBiodata.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbNama.Text = rdr["nama"].ToString();
                            this.TbTmpLhir.Text = rdr["tmp_lahir"].ToString();
                            if (rdr["ttl"] == DBNull.Value)
                            {

                            }
                            else
                            {
                                DateTime TglLahir = Convert.ToDateTime(rdr["ttl"]);
                                this.TBTtl.Text = TglLahir.ToString("yyyy-MM-dd");
                            }

                            this.DLGender.SelectedItem.Text = rdr["gender"].ToString();
                            this.DLAgama.SelectedItem.Text = rdr["agama"].ToString();
                            this.DLDarah.SelectedItem.Text = rdr["darah"].ToString();
                            this.TbAlamat.Text = rdr["alamat"].ToString();
                            StringProv = rdr["prov"].ToString();
                            StringKota = rdr["kota"].ToString();
                            StringKab = rdr["kec"].ToString();

                            this.DropDownListKab.SelectedItem.Text = rdr["kota"].ToString();
                            this.DropDownListKec.SelectedItem.Text = rdr["kec"].ToString();
                            this.TbKdPOS.Text = rdr["kd_pos"].ToString();
                            this.TBHp.Text = rdr["hp"].ToString();
                            this.TbEmail.Text = rdr["email"].ToString();

                            this.TbSekolah.Text = rdr["sekolah"].ToString();
                            this.DLJurusan.SelectedItem.Text = rdr["jurusan"].ToString();
                            this.DLStatusSekolah.SelectedItem.Text = rdr["status_sekolah"].ToString();
                            this.TBThnLls.Text = rdr["thn_lls"].ToString();

                            this.TBAdik.Text = rdr["adik"].ToString();
                            this.TbKakak.Text = rdr["kakak"].ToString();
                            this.TbAyah.Text = rdr["ibu"].ToString();
                            this.TbIbu.Text = rdr["ayah"].ToString();
                            this.DLPendidikanAyah.SelectedItem.Text = rdr["pendidikan_ayah"].ToString();
                            this.DLPendidikanIbu.SelectedItem.Text = rdr["pendidikan_ibu"].ToString();
                            this.DLPekerjaanAyah.SelectedItem.Text = rdr["pekerjaan_ayah"].ToString();
                            this.DLPekerjaanIbu.SelectedItem.Text = rdr["pekerjaan_ibu"].ToString();
                            this.DLPenghasilan.Text = rdr["penghasilan_ortu"].ToString();
                        }
                    }
                }

                // ------- ID Province Information  -------
                SqlCommand CmdProv = new SqlCommand("select id_prov from daerah_provinsi where nama=@provinsi", con);
                CmdProv.CommandType = System.Data.CommandType.Text;
                CmdProv.Parameters.AddWithValue("@provinsi", StringProv);
                using (SqlDataReader rdrProv = CmdProv.ExecuteReader())
                {
                    if (rdrProv.HasRows)
                    {
                        while (rdrProv.Read())
                        {
                            CascadingDropDownProv.SelectedValue = rdrProv["id_prov"].ToString();
                        }
                    }
                    else
                    {
                        CascadingDropDownProv.SelectedValue = null;
                    }
                }

                // ------- ID City Information  -------
                SqlCommand CmdKota = new SqlCommand("select id_kotakab from daerah_kotakab where id_prov=@IdProv AND nama=@kotakab ", con);
                CmdKota.CommandType = System.Data.CommandType.Text;
                CmdKota.Parameters.AddWithValue("@IdProv", CascadingDropDownProv.SelectedValue.ToString());
                CmdKota.Parameters.AddWithValue("@kotakab", StringKota);

                using (SqlDataReader rdrKota = CmdKota.ExecuteReader())
                {
                    if (rdrKota.HasRows)
                    {
                        while (rdrKota.Read())
                        {
                            this.CascadingDropDownKotakab.SelectedValue = rdrKota["id_kotakab"].ToString();
                        }
                    }
                }
                // ---------------------------------

                // ------- ID Kecamatan Information  -------
                SqlCommand CmdKec = new SqlCommand("select id_kec from daerah_kecamatan where id_kotakab=@IdKotaKab AND nama=@kecamatan ", con);
                CmdKec.CommandType = System.Data.CommandType.Text;
                CmdKec.Parameters.AddWithValue("@IdKotaKab", CascadingDropDownKotakab.SelectedValue.ToString());
                CmdKec.Parameters.AddWithValue("@kecamatan", StringKab);

                using (SqlDataReader rdrKec = CmdKec.ExecuteReader())
                {
                    if (rdrKec.HasRows)
                    {
                        while (rdrKec.Read())
                        {
                            this.CascadingDropDownKec.SelectedValue = rdrKec["id_kec"].ToString();
                        }
                    }
                }
                // ---------------------------------
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.LbNPM.Text == "" || this.LbNPM.Text == null)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('GET NPM TIDAK BENAR');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbTmpLhir.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('TEMPAT LAHIR');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TBTtl.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TANGGAL LAHIR');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DLGender.SelectedItem.Text == "Jenis Kelamin" || DLGender.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JENIS KELAMIN');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DLAgama.SelectedItem.Text == "Agama" || DLAgama.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH AGAMA');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DLDarah.SelectedItem.Text == "Golongan Darah" || DLDarah.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH GOLONGAN DARAH');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DropDownListProv.SelectedItem.Text == "PROVINSI" || DropDownListProv.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PROVINSI');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DropDownListKab.SelectedItem.Text == "KOTA/KABUPATEN" || DropDownListKab.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KOTA/KABUPATEN');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DropDownListKec.SelectedItem.Text == "KECAMATAN" || DropDownListKec.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KECAMATAN');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (TbAlamat.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI ALAMAT');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbKdPOS.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI KODE POS');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TBHp.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NOMOR HP');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbEmail.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI ALAMAT EMAIL');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbSekolah.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI SEKOLAH ASAL');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLJurusan.SelectedItem.Text == "Jurusan" || DLJurusan.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JURUSAN SEKOLAH ASAL');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (DLStatusSekolah.SelectedItem.Text == "Status" || DLStatusSekolah.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH STATUS SEKOLAH ASAL');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TBThnLls.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TAHUN LULUS');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TBAdik.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI JUMLAH ADIK');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbKakak.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI JUMLAH KAKAK');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbAyah.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA AYAH');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbIbu.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA IBU');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLPendidikanAyah.SelectedItem.Text == "Pendidikan" || DLPendidikanAyah.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PENDIDIKAN AYAH');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLPendidikanIbu.SelectedItem.Text == "Pendidikan" || DLPendidikanIbu.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PENDIDIKAN IBU');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLPekerjaanAyah.SelectedItem.Text == "Pekerjaan" || DLPekerjaanAyah.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PEKERJAAN AYAH');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLPekerjaanIbu.SelectedItem.Text == "Pekerjaan" || DLPekerjaanIbu.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PEKERJAAN IBU');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.DLPenghasilan.SelectedItem.Text == "Penghasilan" || DLPenghasilan.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PENGHASILAN ORANG TUA');", true);
                ModalPopupExtender1.Show();
                return;
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //SqlTransaction trans = con.BeginTransaction();

                    SqlCommand CmdBiodata = new SqlCommand("SpUpdateMhs", con);
                    //CmdPeriodik.Transaction = trans;
                    CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBiodata.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    CmdBiodata.Parameters.AddWithValue("@gdr", DLGender.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@agm", DLAgama.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@drh", DLDarah.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@prov", DropDownListProv.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@kota", DropDownListKab.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@kec", DropDownListKec.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@alamat", TbAlamat.Text);
                    CmdBiodata.Parameters.AddWithValue("@tmplahir", TbTmpLhir.Text);
                    CmdBiodata.Parameters.AddWithValue("@ttl", Convert.ToDateTime(TBTtl.Text));
                    CmdBiodata.Parameters.AddWithValue("@pos", TbKdPOS.Text);
                    CmdBiodata.Parameters.AddWithValue("@hp", TBHp.Text);
                    CmdBiodata.Parameters.AddWithValue("@email", TbEmail.Text);
                    CmdBiodata.Parameters.AddWithValue("@sch", TbSekolah.Text);
                    CmdBiodata.Parameters.AddWithValue("@jrs", DLJurusan.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@stsch", DLStatusSekolah.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@thn_lls", TBThnLls.Text);
                    CmdBiodata.Parameters.AddWithValue("@kakak", Convert.ToInt32(TbKakak.Text));
                    CmdBiodata.Parameters.AddWithValue("@adk", Convert.ToInt32(TBAdik.Text));
                    CmdBiodata.Parameters.AddWithValue("@ibu", TbIbu.Text);
                    CmdBiodata.Parameters.AddWithValue("@ayah", TbAyah.Text);
                    CmdBiodata.Parameters.AddWithValue("@pnd_ayah", DLPendidikanAyah.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@pnd_ibu", DLPendidikanIbu.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@pkj_ayah", DLPekerjaanAyah.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@pkj_ibu", DLPekerjaanIbu.SelectedItem.Text);
                    CmdBiodata.Parameters.AddWithValue("@pghasil", DLPenghasilan.SelectedItem.Text);

                    CmdBiodata.ExecuteNonQuery();

                    this.LbNPM.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil ...');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }

        }

    }
}