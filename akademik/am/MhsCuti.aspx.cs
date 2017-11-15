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
    public partial class WebForm19 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LbKetSelect.Text = "";
                PopulateProdi();
            }
            else
            {
                TabName.Value = Request.Form[TabName.UniqueID];
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

        protected void TbAngkatan_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DLSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DLReligi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TbSrcNama_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BtnLihat_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            // clear temp data
            this.TbTglLulus.Text = string.Empty;
            this.TbSKS.Text = string.Empty;
            this.TbIPK.Text = string.Empty;
            this.TBNoSK.Text = string.Empty;
            this.TbTglSK.Text = string.Empty;
            this.TbNoIjazah.Text = string.Empty;
            this.DLJalur.SelectedValue = "-1";
            this.TbThnLls.Text = string.Empty;
            this.TbSmstrLls.Text = string.Empty;
            this.DLJalur.SelectedValue = "-1";
            this.TbJudul.Text = string.Empty;

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            this.LbNPM.Text = this.GVMhs.Rows[index].Cells[1].Text;
            this.LbStatus.Text = this.GVMhs.Rows[index].Cells[6].Text;

            // ---------------- Mahasiswa CUTI/DO/DOUBLE DEGREE/KELUAR/NON-AKTIF ----------------
            // --- table bak_mahasiswa ----
            if (this.LbStatus.Text != "LULUS")
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //SqlTransaction trans = con.BeginTransaction();

                    SqlCommand CmdBiodata = new SqlCommand("SpBiodataMhs", con);
                    //CmdPeriodik.Transaction = trans;
                    CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBiodata.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    // CmdBiodata.Parameters.AddWithValue("@no_tagihan", this.GVPendaftar.SelectedRow.Cells[1].Text);

                    using (SqlDataReader rdr = CmdBiodata.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                //this.TbNama.Text = rdr["nama"].ToString();
                                //this.DLProdi2.SelectedValue = rdr["id_prog_study"].ToString();
                                //this.TbAngkatan2.Text = rdr["thn_angkatan"].ToString();

                                //----- status ------
                                string st = rdr["status"].ToString();
                                if (rdr["status"].ToString() == "A")
                                {
                                    this.DLStatus2.SelectedValue = "A";
                                }
                                else if (rdr["status"].ToString() == "C")
                                {
                                    this.DLStatus2.SelectedValue = "C";
                                }
                                else if (rdr["status"].ToString() == "D")
                                {
                                    this.DLStatus2.SelectedValue = "D";
                                }
                                else if (rdr["status"].ToString() == "G")
                                {
                                    this.DLStatus2.SelectedValue = "G";
                                }
                                else if (rdr["status"].ToString() == "K")
                                {
                                    this.DLStatus2.SelectedValue = "K";
                                }
                                else if (rdr["status"].ToString() == "N")
                                {
                                    this.DLStatus2.SelectedValue = "N";
                                }
                                else if (rdr["status"].ToString() == "L")
                                {
                                    this.DLStatus2.SelectedValue = "L";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // ------------ Identitas Data Mahasiswa LULUS ------------
                // --- table bak_lulus ---
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //SqlTransaction trans = con.BeginTransaction();

                    SqlCommand CmdBiodata = new SqlCommand("SpBiodataMhsLulus", con);
                    //CmdPeriodik.Transaction = trans;
                    CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBiodata.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    // CmdBiodata.Parameters.AddWithValue("@no_tagihan", this.GVPendaftar.SelectedRow.Cells[1].Text);

                    using (SqlDataReader rdr = CmdBiodata.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                //this.TbNama.Text = rdr["nama"].ToString();
                                //this.DLProdi2.SelectedValue = rdr["id_prog_study"].ToString();
                                //this.TbAngkatan2.Text = rdr["thn_angkatan"].ToString();

                                //----- status ------
                                if (rdr["status"].ToString() == "A")
                                {
                                    this.DLStatus2.SelectedValue = "A";
                                }
                                else if (rdr["status"].ToString() == "C")
                                {
                                    this.DLStatus2.SelectedValue = "C";
                                }
                                else if (rdr["status"].ToString() == "D")
                                {
                                    this.DLStatus2.SelectedValue = "D";
                                }
                                else if (rdr["status"].ToString() == "G")
                                {
                                    this.DLStatus2.SelectedValue = "G";
                                }
                                else if (rdr["status"].ToString() == "K")
                                {
                                    this.DLStatus2.SelectedValue = "K";
                                }
                                else if (rdr["status"].ToString() == "N")
                                {
                                    this.DLStatus2.SelectedValue = "N";
                                }
                                else if (rdr["status"].ToString() == "L")
                                {
                                    this.DLStatus2.SelectedValue = "L";
                                }

                                if (rdr["tgl_lls"] != DBNull.Value)
                                {
                                    //this.TbTglLulus.Text = rdr["tgl_lls"].ToString();
                                    DateTime TglLls = Convert.ToDateTime(rdr["tgl_lls"]);
                                    this.TbTglLulus.Text = TglLls.ToString("yyyy-MM-dd");
                                }
                                this.TbSKS.Text = rdr["sks"].ToString();
                                this.TbIPK.Text = rdr["ipk"].ToString();
                                this.TBNoSK.Text = rdr["no_sk"].ToString();

                                if (rdr["tgl_sk"] != DBNull.Value)
                                {
                                    //this.TbTglSK.Text = rdr["tgl_sk"].ToString();
                                    DateTime TglSK = Convert.ToDateTime(rdr["tgl_sk"]);
                                    this.TbTglSK.Text = TglSK.ToString("yyyy-MM-dd");
                                }
                                this.TbNoIjazah.Text = rdr["no_ijazah"].ToString();

                                //jalur lulus
                                if (rdr["jalur"].ToString() == "N")
                                {
                                    this.DLJalur.SelectedValue = "N";
                                }
                                else if (rdr["jalur"].ToString() == "S")
                                {
                                    this.DLJalur.SelectedValue = "S";
                                }
                                //Pelaksanaan
                                if (rdr["plsk"].ToString() == "I")
                                {
                                    this.DLPelaksanaan.SelectedValue = "I";
                                }
                                else if (rdr["plsk"].ToString() == "K")
                                {
                                    this.DLPelaksanaan.SelectedValue = "K";
                                }

                                this.TbThnLls.Text = rdr["thn_lls"].ToString();
                                this.TbSmstrLls.Text = rdr["smster_lls"].ToString();
                                this.TbJudul.Text = rdr["judul"].ToString();
                            }
                        }
                    }
                }
            }

            //npm = this.GVMhs.Rows[index].Cells[1].Text;
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

        protected void BtnSrc_Click(object sender, EventArgs e)
        {
            // PRODI
            if (this.DLProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }

            // KRITERIA PENCARIAN
            if (this.TbAngkatan.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tahun Angkatan');", true);
                return;
            }

            if (this.DLProdi.SelectedValue == "ALL" && this.DLStatus.SelectedValue == "A")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pencarian Mahasiswa AKTIF Seluruh Prodi TIDAK DIPERBOLEHKAN');", true);
                return;
            }

            this.LbKetSelect.Text = "advance";
            LoadGVStatus(0, this.GVMhs.PageSize);
        }

        protected void BtnSrc2_Click(object sender, EventArgs e)
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


            this.LbKetSelect.Text = "search";
            LoadGridViewNama(0, this.GVMhs.PageSize);
        }

        private void LoadGVStatus(int PageIndex, int PageSize)
        {
            //// ---------------- Mahasiswa CUTI/DO/DOUBLE DEGREE/KELUAR/NON-AKTIF ----------------
            //// --- table bak_mahasiswa ----

            int TotalRows = 0;
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // --------------------- Fill Gridview  ------------------------
                SqlCommand CmdLls = new SqlCommand("GetMhsByStatus", con);
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

                if (this.TbSrcNama.Text != string.Empty)
                {
                    if (this.TbSrcNama.Text.Length >= 4)
                    {
                        SqlParameter NamaParam = new SqlParameter("@nama", this.TbSrcNama.Text);
                        CmdLls.Parameters.Add(NamaParam);
                    }
                    else
                    {
                        con.Close();
                        con.Dispose();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM/NAMA Minimal 4 Huruf');", true);
                        return;
                    }
                }

                //if (this.DLReligi.SelectedValue != "-1")
                //{
                //    SqlParameter AgamaParam = new SqlParameter("@agama", this.DLReligi.SelectedItem.Text);
                //    CmdLls.Parameters.Add(AgamaParam);
                //}

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
            //}
            //else
            //{
            //    // ------------ Identitas Data Mahasiswa LULUS ------------
            //    // --- table bak_lulus ---

            //    if (this.DLSex.SelectedValue != "-1")
            //    {
            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Gender Tidak Digunakan');", true);
            //        return;
            //    }

            //    if (this.DLReligi.SelectedValue != "-1")
            //    {
            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Agama Tidak Digunakan');", true);
            //        return;
            //    }


            //    int TotalRows = 0;
            //    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(CS))
            //    {
            //        con.Open();
            //        // --------------------- Fill Gridview  ------------------------
            //        SqlCommand CmdLls = new SqlCommand("SpSrcMhsLulus", con);
            //        CmdLls.CommandType = System.Data.CommandType.StoredProcedure;

            //        CmdLls.Parameters.AddWithValue("@index", PageIndex);
            //        CmdLls.Parameters.AddWithValue("@size", this.GVMhs.PageSize);
            //        CmdLls.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);

            //        if (this.TbAngkatan.Text != string.Empty)
            //        {
            //            SqlParameter AngkatanParam = new SqlParameter("@angkatan", this.TbAngkatan.Text);
            //            CmdLls.Parameters.Add(AngkatanParam);
            //        }

            //        if (this.DLStatus.SelectedValue != "-1")
            //        {
            //            SqlParameter StatusParam = new SqlParameter("@status", this.DLStatus.SelectedValue);
            //            CmdLls.Parameters.Add(StatusParam);
            //        }

            //        if (this.TbSrcNama.Text != string.Empty)
            //        {
            //            SqlParameter NamaParam = new SqlParameter("@nama", this.TbSrcNama.Text);
            //            CmdLls.Parameters.Add(NamaParam);
            //        }

            //        SqlParameter TotalRow = new SqlParameter();
            //        TotalRow.ParameterName = "@totalRow";
            //        TotalRow.SqlDbType = System.Data.SqlDbType.Int;
            //        TotalRow.Size = 20;
            //        TotalRow.Direction = System.Data.ParameterDirection.Output;
            //        CmdLls.Parameters.Add(TotalRow);

            //        DataTable TableKRS = new DataTable();
            //        TableKRS.Columns.Add("No");
            //        TableKRS.Columns.Add("NPM");
            //        TableKRS.Columns.Add("Nama");
            //        TableKRS.Columns.Add("Program Studi");
            //        TableKRS.Columns.Add("Tahun Angkatan");
            //        TableKRS.Columns.Add("Jenis Kelas");
            //        TableKRS.Columns.Add("Status");

            //        using (SqlDataReader rdr = CmdLls.ExecuteReader())
            //        {
            //            if (rdr.HasRows)
            //            {
            //                while (rdr.Read())
            //                {
            //                    DataRow datarow = TableKRS.NewRow();
            //                    datarow["No"] = rdr["urutan"];
            //                    datarow["NPM"] = rdr["npm"];
            //                    datarow["Nama"] = rdr["nama"];
            //                    datarow["Program Studi"] = rdr["id_prog_study"];
            //                    datarow["Tahun Angkatan"] = rdr["thn_angkatan"];
            //                    datarow["Jenis Kelas"] = rdr["jenis_kelas"];

            //                    //----- status ------
            //                    string st = rdr["status"].ToString();
            //                    if (rdr["status"].ToString() == "A")
            //                    {
            //                        datarow["STATUS"] = "AKTIF";
            //                    }
            //                    else if (rdr["status"].ToString() == "C")
            //                    {
            //                        datarow["STATUS"] = "CUTI";
            //                    }
            //                    else if (rdr["status"].ToString() == "D")
            //                    {
            //                        datarow["STATUS"] = "DROP-OUT";
            //                    }
            //                    else if (rdr["status"].ToString() == "G")
            //                    {
            //                        datarow["STATUS"] = "DOUBLE DEGREE";
            //                    }
            //                    else if (rdr["status"].ToString() == "K")
            //                    {
            //                        datarow["STATUS"] = "KELUAR";
            //                    }
            //                    else if (rdr["status"].ToString() == "N")
            //                    {
            //                        datarow["STATUS"] = "NON-AKTIF";
            //                    }
            //                    else if (rdr["status"].ToString() == "L")
            //                    {
            //                        datarow["STATUS"] = "LULUS";
            //                    }

            //                    TableKRS.Rows.Add(datarow);
            //                }

            //                rdr.Close();
            //                TotalRows = (int)CmdLls.Parameters["@totalRow"].Value;

            //                //this.DLKelas.SelectedIndex = 0;

            //                //Fill Gridview
            //                this.GVMhs.DataSource = TableKRS;
            //                this.GVMhs.DataBind();
            //            }
            //            else
            //            {
            //                //clear Gridview
            //                TableKRS.Rows.Clear();
            //                TableKRS.Clear();
            //                GVMhs.DataSource = TableKRS;
            //                GVMhs.DataBind();

            //                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
            //            }
            //        }
            //    }

            //    //paging
            //    //DataBindRepeater(PageIndex, this.GVMhs.PageSize, TotalRows);
            //}

        }

        private void LoadGVStatusLls(int PageIndex, int PageSize)
        {
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
                CmdLls.Parameters.AddWithValue("@status", this.DLStatus3.SelectedValue);
                
                if (this.TbSemLls.Text != string.Empty)
                {
                    CmdLls.Parameters.AddWithValue("@smsterLls", this.TbSemLls.Text);
                }
                
                if (this.DLProdi3.SelectedValue != "All")
                {
                    SqlParameter ProdiParam = new SqlParameter("@idprodi", this.DLProdi3.SelectedValue);
                    CmdLls.Parameters.Add(ProdiParam);
                } 

                if (this.TbThnAngkatan.Text != string.Empty)
                {
                    SqlParameter ThnParam = new SqlParameter("@angkatan", this.TbThnAngkatan.Text);
                    CmdLls.Parameters.Add(ThnParam);
                }

                if (this.DLSex3.SelectedValue != "-1")
                {
                    SqlParameter GenderParam = new SqlParameter("@gender", this.DLSex3.SelectedItem.Text);
                    CmdLls.Parameters.Add(GenderParam);
                }

                //if (this.DLReligi.SelectedValue != "-1")
                //{
                //    SqlParameter AgamaParam = new SqlParameter("@agama", this.DLReligi.SelectedItem.Text);
                //    CmdLls.Parameters.Add(AgamaParam);
                //}

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
            //}
            //else
            //{
            //    // ------------ Identitas Data Mahasiswa LULUS ------------
            //    // --- table bak_lulus ---

            //    if (this.DLSex.SelectedValue != "-1")
            //    {
            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Gender Tidak Digunakan');", true);
            //        return;
            //    }

            //    if (this.DLReligi.SelectedValue != "-1")
            //    {
            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Agama Tidak Digunakan');", true);
            //        return;
            //    }


            //    int TotalRows = 0;
            //    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(CS))
            //    {
            //        con.Open();
            //        // --------------------- Fill Gridview  ------------------------
            //        SqlCommand CmdLls = new SqlCommand("SpSrcMhsLulus", con);
            //        CmdLls.CommandType = System.Data.CommandType.StoredProcedure;

            //        CmdLls.Parameters.AddWithValue("@index", PageIndex);
            //        CmdLls.Parameters.AddWithValue("@size", this.GVMhs.PageSize);
            //        CmdLls.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);

            //        if (this.TbAngkatan.Text != string.Empty)
            //        {
            //            SqlParameter AngkatanParam = new SqlParameter("@angkatan", this.TbAngkatan.Text);
            //            CmdLls.Parameters.Add(AngkatanParam);
            //        }

            //        if (this.DLStatus.SelectedValue != "-1")
            //        {
            //            SqlParameter StatusParam = new SqlParameter("@status", this.DLStatus.SelectedValue);
            //            CmdLls.Parameters.Add(StatusParam);
            //        }

            //        if (this.TbSrcNama.Text != string.Empty)
            //        {
            //            SqlParameter NamaParam = new SqlParameter("@nama", this.TbSrcNama.Text);
            //            CmdLls.Parameters.Add(NamaParam);
            //        }

            //        SqlParameter TotalRow = new SqlParameter();
            //        TotalRow.ParameterName = "@totalRow";
            //        TotalRow.SqlDbType = System.Data.SqlDbType.Int;
            //        TotalRow.Size = 20;
            //        TotalRow.Direction = System.Data.ParameterDirection.Output;
            //        CmdLls.Parameters.Add(TotalRow);

            //        DataTable TableKRS = new DataTable();
            //        TableKRS.Columns.Add("No");
            //        TableKRS.Columns.Add("NPM");
            //        TableKRS.Columns.Add("Nama");
            //        TableKRS.Columns.Add("Program Studi");
            //        TableKRS.Columns.Add("Tahun Angkatan");
            //        TableKRS.Columns.Add("Jenis Kelas");
            //        TableKRS.Columns.Add("Status");

            //        using (SqlDataReader rdr = CmdLls.ExecuteReader())
            //        {
            //            if (rdr.HasRows)
            //            {
            //                while (rdr.Read())
            //                {
            //                    DataRow datarow = TableKRS.NewRow();
            //                    datarow["No"] = rdr["urutan"];
            //                    datarow["NPM"] = rdr["npm"];
            //                    datarow["Nama"] = rdr["nama"];
            //                    datarow["Program Studi"] = rdr["id_prog_study"];
            //                    datarow["Tahun Angkatan"] = rdr["thn_angkatan"];
            //                    datarow["Jenis Kelas"] = rdr["jenis_kelas"];

            //                    //----- status ------
            //                    string st = rdr["status"].ToString();
            //                    if (rdr["status"].ToString() == "A")
            //                    {
            //                        datarow["STATUS"] = "AKTIF";
            //                    }
            //                    else if (rdr["status"].ToString() == "C")
            //                    {
            //                        datarow["STATUS"] = "CUTI";
            //                    }
            //                    else if (rdr["status"].ToString() == "D")
            //                    {
            //                        datarow["STATUS"] = "DROP-OUT";
            //                    }
            //                    else if (rdr["status"].ToString() == "G")
            //                    {
            //                        datarow["STATUS"] = "DOUBLE DEGREE";
            //                    }
            //                    else if (rdr["status"].ToString() == "K")
            //                    {
            //                        datarow["STATUS"] = "KELUAR";
            //                    }
            //                    else if (rdr["status"].ToString() == "N")
            //                    {
            //                        datarow["STATUS"] = "NON-AKTIF";
            //                    }
            //                    else if (rdr["status"].ToString() == "L")
            //                    {
            //                        datarow["STATUS"] = "LULUS";
            //                    }

            //                    TableKRS.Rows.Add(datarow);
            //                }

            //                rdr.Close();
            //                TotalRows = (int)CmdLls.Parameters["@totalRow"].Value;

            //                //this.DLKelas.SelectedIndex = 0;

            //                //Fill Gridview
            //                this.GVMhs.DataSource = TableKRS;
            //                this.GVMhs.DataBind();
            //            }
            //            else
            //            {
            //                //clear Gridview
            //                TableKRS.Rows.Clear();
            //                TableKRS.Clear();
            //                GVMhs.DataSource = TableKRS;
            //                GVMhs.DataBind();

            //                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
            //            }
            //        }
            //    }

            //    //paging
            //    //DataBindRepeater(PageIndex, this.GVMhs.PageSize, TotalRows);
            //}

        }

        private void LoadGridViewNama(int PageIndex, int PageSize)
        {
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

        protected void PageButton_Click(object sender, EventArgs e)
        {
            //int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //PageIndex -= 1;
            //GVMhs.PageIndex = PageIndex;

            //LoadGVStatus(PageIndex, this.GVMhs.PageSize);

            if (this.LbKetSelect.Text == "advance")
            {
                int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                PageIndex -= 1;
                GVMhs.PageIndex = PageIndex;

                LoadGVStatus(PageIndex, this.GVMhs.PageSize);
            }
            else if (this.LbKetSelect.Text =="search")
            {
                int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                PageIndex -= 1;
                GVMhs.PageIndex = PageIndex;

                LoadGridViewNama(PageIndex, this.GVMhs.PageSize);
            } 
            else if (this.LbKetSelect.Text =="Lls/Cuti")
            {
                int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
                PageIndex -= 1;
                GVMhs.PageIndex = PageIndex;

                LoadGVStatusLls(PageIndex, this.GVMhs.PageSize);
            }

            //if (this.LbKet.Text == "Prodi")
            //{
            //    int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //    PageIndex -= 1;
            //    GVMhs.PageIndex = PageIndex;

            //    LoadGridViewProdi(PageIndex, this.GVMhs.PageSize);
            //}
            //else if (this.LbKet.Text == "Gender")
            //{
            //    int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //    PageIndex -= 1;
            //    GVMhs.PageIndex = PageIndex;

            //    LoadGridViewGender(PageIndex, this.GVMhs.PageSize);
            //}
            //else if (this.LbKet.Text == "Agama")
            //{
            //    int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //    PageIndex -= 1;
            //    GVMhs.PageIndex = PageIndex;

            //    LoadGridViewAgama(PageIndex, this.GVMhs.PageSize);
            //}
            //else if (this.LbKet.Text == "Angkatan")
            //{
            //    int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //    PageIndex -= 1;
            //    GVMhs.PageIndex = PageIndex;

            //    LoadGridViewAngkatan(PageIndex, this.GVMhs.PageSize);
            //}
            //else if (this.LbKet.Text == "Nama")
            //{
            //    int PageIndex = int.Parse((sender as LinkButton).CommandArgument);
            //    PageIndex -= 1;
            //    GVMhs.PageIndex = PageIndex;

            //    LoadGridViewNama(PageIndex, this.GVMhs.PageSize);
            //}
        }

        protected void BtnSrcLLs_Click(object sender, EventArgs e)
        {
            // PRODI Wajib
            if (this.DLProdi3.SelectedValue == "-1")
            {
                string message = "alert('Pilih Program Studi')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            
            // Status Wajib
            if (this.DLStatus3.SelectedValue != "C" && this.DLStatus3.SelectedValue != "L" && this.DLStatus3.SelectedValue != "D" && this.DLStatus3.SelectedValue != "G" && this.DLStatus3.SelectedValue != "K" && this.DLStatus3.SelectedValue != "N")
            {
                string message = "alert('Status Mahasiswa Tidak Benar')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            //LULUS wajib Isi SEMESTER
            if (this.DLStatus3.SelectedValue == "L")
            {
                if (this.TbSemLls.Text == string.Empty)
                {
                    string message = "alert('Semester Wajib Diisi')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else
                {
                    if (this.TbSemLls.Text.Length != 5)
                    {
                        string message = "alert('ISI SEMESTER 5 KARAKTER')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }
                }
            }

            ////SELAIN LULUS TIDAK USAH ISI SEMESTER
            //if (this.DLStatus3.SelectedValue != "C" && this.DLStatus3.SelectedValue != "L")
            //{
            //    if (this.TbSemLls.Text != string.Empty)
            //    {
            //        string message = "alert('SEMESTER TIDAK PERLU DIISI')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //}

            //Tahun Angkatan
            if (this.TbThnAngkatan.Text != string.Empty)
            {
                if (this.TbThnAngkatan.Text.Length != 9)
                {
                    string message = "alert('ISI TAHUN MASUK 9 KARAKTER')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }

            this.LbKetSelect.Text = "Lls/Cuti";
            LoadGVStatusLls(0, this.GVMhs.PageSize);
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (this.DLStatus2.SelectedValue == "-1")
            {
                string message = "alert('PILIH STATUS MAHASISWA')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ModalPopupExtender1.Show();
                return;
            }

            // ---- ==============  Mahasiswa Lulus ============ ------
            if (this.DLStatus2.SelectedValue == "L" || this.DLStatus2.SelectedItem.Text == "LULUS")
            {
                if (this.LbNPM.Text == "" || this.LbNPM.Text == null)
                {
                    string message = "alert('GET NPM TIDAK BENAR')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }

                //form validation
                if (this.TbTglLulus.Text == string.Empty)
                {
                    string message = "alert('ISI TANGGAL LULUS')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbSKS.Text == string.Empty)
                {
                    string message = "alert('ISI JUMLAH SKS DITEMPUH')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbIPK.Text == string.Empty || this.TbIPK.Text.Length != 4)
                {
                    string message = "alert('ISI IPK')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TBNoSK.Text == string.Empty)
                {
                    string message = "alert('ISI NOMOR SK')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbTglSK.Text == string.Empty)
                {
                    string message = "alert('ISI TANGGAL SK')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbNoIjazah.Text == string.Empty)
                {
                    string message = "alert('ISI NOMOR IJAZAH')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.DLJalur.SelectedValue == "-1")
                {
                    string message = "alert('PILIH JALUR LULUS')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbThnLls.Text == string.Empty || this.TbThnLls.Text.Length != 9)
                {
                    string message = "alert('ISI TAHUN LULUS')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbSmstrLls.Text == string.Empty || this.TbSmstrLls.Text.Length != 5)
                {
                    string message = "alert('ISI ISI SEMESTER LULUS')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.DLJalur.SelectedValue == "-1")
                {
                    string message = "alert('PILIH JALUR PELAKSANAAN')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }
                if (this.TbJudul.Text == string.Empty)
                {
                    string message = "alert('ISI JUDUL')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ModalPopupExtender1.Show();
                    return;
                }

                try
                {
                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        SqlCommand CmdUpStatus = new SqlCommand("SpUpdateMhsLls", con);
                        CmdUpStatus.CommandType = System.Data.CommandType.StoredProcedure;
                        CmdUpStatus.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        if (this.DLStatus2.SelectedValue == "L")
                        {
                            CmdUpStatus.Parameters.AddWithValue("@status", "L4ls");
                        }
                        CmdUpStatus.Parameters.AddWithValue("@tgl_lls", this.TbTglLulus.Text);
                        CmdUpStatus.Parameters.AddWithValue("@sks", this.TbSKS.Text);
                        CmdUpStatus.Parameters.AddWithValue("@ipk", Convert.ToDecimal(this.TbIPK.Text));
                        CmdUpStatus.Parameters.AddWithValue("@NoSK", this.TBNoSK.Text);
                        CmdUpStatus.Parameters.AddWithValue("@tglSK", this.TbTglSK.Text);
                        CmdUpStatus.Parameters.AddWithValue("@NoIjz", this.TbNoIjazah.Text);
                        CmdUpStatus.Parameters.AddWithValue("@jalur_lls", this.DLJalur.SelectedValue);
                        CmdUpStatus.Parameters.AddWithValue("@thn_lls", this.TbThnLls.Text);
                        CmdUpStatus.Parameters.AddWithValue("@semester", this.TbSmstrLls.Text);
                        CmdUpStatus.Parameters.AddWithValue("@plsk", this.DLPelaksanaan.SelectedValue);
                        CmdUpStatus.Parameters.AddWithValue("@judul", this.TbJudul.Text);

                        CmdUpStatus.ExecuteNonQuery();

                        this.LbNPM.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                    }


                    //clear Gridview
                    DataTable TableMhs = new DataTable();
                    TableMhs.Rows.Clear();
                    TableMhs.Clear();
                    GVMhs.DataSource = TableMhs;
                    GVMhs.DataBind();

                    // clear temp data
                    this.TbTglLulus.Text = string.Empty;
                    this.TbSKS.Text = string.Empty;
                    this.TbIPK.Text = string.Empty;
                    this.TBNoSK.Text = string.Empty;
                    this.TbTglSK.Text = string.Empty;
                    this.TbNoIjazah.Text = string.Empty;
                    this.DLJalur.SelectedValue = "-1";
                    this.TbThnLls.Text = string.Empty;
                    this.TbSmstrLls.Text = string.Empty;
                    this.DLJalur.SelectedValue = "-1";
                    this.TbJudul.Text = string.Empty;

                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
            }
            else
            {
                if (this.DLStatus2.SelectedValue == "L" || this.DLStatus2.SelectedItem.Text == "LULUS")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('INPUT STATUS LULUS ERROR');", true);
                    ModalPopupExtender1.Show();
                    return;
                }

                //---- ====== Mahasiswa CUTI/DO/Non-Aktif/Double Degree ======= ----
                try
                {
                    // ---- mahasiswa lulus tidak boleh diaktifkan kembali ----
                    if (this.LbStatus.Text == "LULUS")
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('MAHASISWA TERCATAT LULUS');", true);
                        ModalPopupExtender1.Show();
                        return;
                    }


                    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();

                        SqlCommand CmdUpStatus = new SqlCommand("SpUpdateStatusMhs", con);
                        CmdUpStatus.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUpStatus.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                        CmdUpStatus.Parameters.AddWithValue("@status", this.DLStatus2.SelectedValue);

                        CmdUpStatus.ExecuteNonQuery();

                        this.LbNPM.Text = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                    }

                    // clear temp data
                    this.TbTglLulus.Text = string.Empty;
                    this.TbSKS.Text = string.Empty;
                    this.TbIPK.Text = string.Empty;
                    this.TBNoSK.Text = string.Empty;
                    this.TbTglSK.Text = string.Empty;
                    this.TbNoIjazah.Text = string.Empty;
                    this.DLJalur.SelectedValue = "-1";
                    this.TbThnLls.Text = string.Empty;
                    this.TbSmstrLls.Text = string.Empty;
                    this.DLJalur.SelectedValue = "-1";
                    this.TbJudul.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    ModalPopupExtender1.Show();
                    return;
                }
            }
        }

        protected void BtnUpdateAktif_Click(object sender, EventArgs e)
        {
            //if (this.DLStatusAktif.SelectedValue == "-1" || this.DLStatusAktif.SelectedItem.Text == "Status")
            //{
            //    string message = "alert('PILIH STATUS MAHASISWA')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    ModalPopupExtender1.Show();
            //    return;
            //}

            //if (this.DLStatusAktif.SelectedValue != "A" && this.DLStatusAktif.SelectedValue != "D" && this.DLStatusAktif.SelectedValue != "K" && this.DLStatusAktif.SelectedValue != "N")
            //{
            //    string message = "alert('STATUS MAHASISWA TIDAK BENAR')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    ModalPopupExtender1.Show();
            //    return;
            //}
            
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdUpStatus = new SqlCommand("UPDATE dbo.bak_mahasiswa SET status=@status,smstr_cuti=NULL WHERE npm=@NPM", con);
                    CmdUpStatus.CommandType = System.Data.CommandType.Text;
                    CmdUpStatus.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    //CmdUpStatus.Parameters.AddWithValue("@status", this.DLStatusAktif.SelectedValue);

                    CmdUpStatus.ExecuteNonQuery();

                    this.LbNPM.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                }

                //clear Gridview
                DataTable TableMhs = new DataTable();
                TableMhs.Rows.Clear();
                TableMhs.Clear();
                GVMhs.DataSource = TableMhs;
                GVMhs.DataBind();

                // clear temp data
                this.TbTglLulus.Text = string.Empty;
                this.TbSKS.Text = string.Empty;
                this.TbIPK.Text = string.Empty;
                this.TBNoSK.Text = string.Empty;
                this.TbTglSK.Text = string.Empty;
                this.TbNoIjazah.Text = string.Empty;
                this.DLJalur.SelectedValue = "-1";
                this.TbThnLls.Text = string.Empty;
                this.TbSmstrLls.Text = string.Empty;
                this.DLJalur.SelectedValue = "-1";
                this.TbJudul.Text = string.Empty;

            }
            catch (Exception ex)
            {
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                string message = "alert('"+ex.Message+"')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ModalPopupExtender1.Show();
                return;
            }

        }

        protected void BtnUpdateCuti_Click(object sender, EventArgs e)
        {
            //if (this.DLStatusCuti.SelectedValue == "-1" || this.DLStatusCuti.SelectedItem.Text == "Status")
            //{
            //    string message = "alert('PILIH STATUS MAHASISWA')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    ModalPopupExtender1.Show();
            //    return;
            //}

            //if (this.DLStatusCuti.SelectedValue != "C")
            //{
            //    string message = "alert('STATUS MAHASISWA TIDAK BENAR')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    ModalPopupExtender1.Show();
            //    return;
            //}

            //if (this.TbSmstrCuti.Text.Length != 5)
            //{
            //    string message = "alert('INVALID SEMESTER CUTI')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    ModalPopupExtender1.Show();
            //    return;
            //}

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdUpStatus = new SqlCommand("UPDATE dbo.bak_mahasiswa SET status='C',smstr_cuti=@smster WHERE npm=@NPM", con);
                    CmdUpStatus.CommandType = System.Data.CommandType.Text;
                    CmdUpStatus.Parameters.AddWithValue("@npm", this.LbNPM.Text);
                    //CmdUpStatus.Parameters.AddWithValue("@smster", TbSmstrCuti.Text);

                    CmdUpStatus.ExecuteNonQuery();

                    this.LbNPM.Text = "";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                }

                //clear Gridview
                DataTable TableMhs = new DataTable();
                TableMhs.Rows.Clear();
                TableMhs.Clear();
                GVMhs.DataSource = TableMhs;
                GVMhs.DataBind();

                // clear temp data
                this.TbTglLulus.Text = string.Empty;
                this.TbSKS.Text = string.Empty;
                this.TbIPK.Text = string.Empty;
                this.TBNoSK.Text = string.Empty;
                this.TbTglSK.Text = string.Empty;
                this.TbNoIjazah.Text = string.Empty;
                this.DLJalur.SelectedValue = "-1";
                this.TbThnLls.Text = string.Empty;
                this.TbSmstrLls.Text = string.Empty;
                this.DLJalur.SelectedValue = "-1";
                this.TbJudul.Text = string.Empty;

            }
            catch (Exception ex)
            {
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                string message = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ModalPopupExtender1.Show();
                return;
            }
        }
    }
}