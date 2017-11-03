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
    public partial class WebForm27 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelListDosen.Enabled = false;
                this.PanelListDosen.Visible = false;

                PopulateProdi();
                PopulateProdi2();
                PopulateProdi3();
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

                    DataTable TableProdi = new DataTable();
                    TableProdi.Columns.Add("id_prog_study");
                    TableProdi.Columns.Add("prog_study");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while(rdr.Read())
                            {
                                DataRow datarow = TableProdi.NewRow();
                                datarow["id_prog_study"] = rdr["id_prog_study"];
                                datarow["prog_study"] = rdr["prog_study"];

                                TableProdi.Rows.Add(datarow);
                            }
                        }
                    }


                    this.DLProdi.DataSource = TableProdi;
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    CmdJadwal.ExecuteReader().Close();
                    CmdJadwal.ExecuteReader().Dispose();

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

        private void PopulateProdi2()
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

                    DataTable TableProdi = new DataTable();
                    TableProdi.Columns.Add("id_prog_study");
                    TableProdi.Columns.Add("prog_study");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableProdi.NewRow();
                                datarow["id_prog_study"] = rdr["id_prog_study"];
                                datarow["prog_study"] = rdr["prog_study"];

                                TableProdi.Rows.Add(datarow);
                            }
                        }
                    }


                    this.DLProdiDosen.DataSource = TableProdi;
                    this.DLProdiDosen.DataTextField = "prog_study";
                    this.DLProdiDosen.DataValueField = "id_prog_study";
                    this.DLProdiDosen.DataBind();

                    CmdJadwal.ExecuteReader().Close();
                    CmdJadwal.ExecuteReader().Dispose();

                    con.Close();
                    con.Dispose();
                }
                this.DLProdiDosen.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void PopulateProdi3()
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

                    DataTable TableProdi = new DataTable();
                    TableProdi.Columns.Add("id_prog_study");
                    TableProdi.Columns.Add("prog_study");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableProdi.NewRow();
                                datarow["id_prog_study"] = rdr["id_prog_study"];
                                datarow["prog_study"] = rdr["prog_study"];

                                TableProdi.Rows.Add(datarow);
                            }
                        }
                    }


                    this.DLEditProdi.DataSource = TableProdi;
                    this.DLEditProdi.DataTextField = "prog_study";
                    this.DLEditProdi.DataValueField = "id_prog_study";
                    this.DLEditProdi.DataBind();

                    CmdJadwal.ExecuteReader().Close();
                    CmdJadwal.ExecuteReader().Dispose();

                    con.Close();
                    con.Dispose();
                }
                this.DLEditProdi.Items.Insert(0, new ListItem("-- Program Studi --", "-1"));
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void DLProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DLProdi.SelectedItem.Text == "Program Studi")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("SpGetDosenByProdi", con);
                    CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDosen.Parameters.AddWithValue("@prodi", this.DLProdi.SelectedValue);

                    DataTable TableDosen = new DataTable();
                    TableDosen.Columns.Add("NIDN");
                    TableDosen.Columns.Add("NAMA");
                    TableDosen.Columns.Add("HANDPHONE");
                    TableDosen.Columns.Add("ALAMAT");

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelListDosen.Enabled = true;
                            this.PanelListDosen.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableDosen.NewRow();
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["NAMA"] = rdr["nama"];
                                datarow["HANDPHONE"] = rdr["hp"];
                                datarow["ALAMAT"] = rdr["alamat"];

                                TableDosen.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVDosen.DataSource = TableDosen;
                            this.GVDosen.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableDosen.Rows.Clear();
                            TableDosen.Clear();
                            GVDosen.DataSource = TableDosen;
                            GVDosen.DataBind();

                            this.PanelListDosen.Enabled = false;
                            this.PanelListDosen.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                            return;
                        }
                    }

                    // loop to cek status dosen
                    for (int i = 0; i < this.GVDosen.Rows.Count; i++)
                    {
                        SqlCommand CmdAktif = new SqlCommand("SpGetDosenByProdi", con);
                        CmdAktif.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdAktif.Parameters.AddWithValue("@prodi", this.DLProdi.SelectedValue);

                        using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["aktif"].ToString() == "yes")
                                    {
                                        CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                                        ch.Checked = true;
                                    }
                                    else if (rdr["aktif"].ToString() != "yes")
                                    {
                                        CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                                        ch.Checked = false;
                                    }
                                }
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
        }

        protected void BtnTambahDosen_Click(object sender, EventArgs e)
        {
            if (this.TbNIDN.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NIDN ...');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.TbNama.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA ...!!');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.DLProdiDosen.SelectedItem.Text == "Program Studi" || this.DLProdiDosen.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.DLGender.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelamin');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.DLAgama.SelectedItem.Text == "Agama")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Agama');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.TBTglLahir.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TANGGAL LAHIR ...');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.TbAlamat.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI ALAMAT RUMAH...');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.TBHp.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NOMOR HANDPHONE...');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }
            if (this.TbEmail.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('E-MAIL WAJIB DIISI...');", true);
                this.ModalPopupExtenderAdd.Show();
                return;
            }

            // ------- Insert Dosen ------
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("SpInsertDosen", con);
                    CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDosen.Parameters.AddWithValue("@nama", this.TbNama.Text);
                    CmdDosen.Parameters.AddWithValue("@nidn", this.TbNIDN.Text);
                    CmdDosen.Parameters.AddWithValue("@prodi", this.DLProdiDosen.SelectedValue);
                    CmdDosen.Parameters.AddWithValue("@gender",this.DLGender.SelectedValue );
                    CmdDosen.Parameters.AddWithValue("@agama", this.DLAgama.SelectedItem.Text);
                    CmdDosen.Parameters.AddWithValue("@alamat", this.TbAlamat.Text);
                    CmdDosen.Parameters.AddWithValue("@hp", this.TBHp.Text);
                    CmdDosen.Parameters.AddWithValue("@email", this.TbEmail.Text);
                    CmdDosen.Parameters.AddWithValue("@tglahir", this.TBTglLahir.Text);

                    CmdDosen.ExecuteNonQuery();

                    this.DLProdiDosen.SelectedIndex = 0;
                    this.DLAgama.SelectedIndex = 0;
                    this.DLGender.SelectedIndex = 0;

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('INPUT BERHASIL...');", true);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnCari_Click(object sender, EventArgs e)
        {
            if (this.DLCari.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Pencarian');", true);
                return;
            }

            if (this.TbSearch.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Ketik Kata Kunci Pencarian');", true);
                return;
            }

            if (this.TbSearch.Text.Length < 4)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Kata Kunci Minimal 4 Huruf atau Angka');", true);
                return;
            }


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("SpCariDosen", con);
                    CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDosen.Parameters.AddWithValue("@input", this.TbSearch.Text);

                    DataTable TableDosen = new DataTable();
                    TableDosen.Columns.Add("NIDN");
                    TableDosen.Columns.Add("NAMA");
                    TableDosen.Columns.Add("HANDPHONE");
                    TableDosen.Columns.Add("ALAMAT");

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelListDosen.Enabled = true;
                            this.PanelListDosen.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = TableDosen.NewRow();
                                datarow["NIDN"] = rdr["nidn"];
                                datarow["NAMA"] = rdr["nama"];
                                datarow["HANDPHONE"] = rdr["hp"];
                                datarow["ALAMAT"] = rdr["alamat"];

                                TableDosen.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVDosen.DataSource = TableDosen;
                            this.GVDosen.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableDosen.Rows.Clear();
                            TableDosen.Clear();
                            GVDosen.DataSource = TableDosen;
                            GVDosen.DataBind();

                            this.PanelListDosen.Enabled = false;
                            this.PanelListDosen.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                            return;
                        }
                    }

                    // loop to cek status dosen
                    for (int i = 0; i < this.GVDosen.Rows.Count; i++)
                    {

                        SqlCommand CmdAktif = new SqlCommand("SpGetDosenByProdi", con);
                        CmdAktif.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdAktif.Parameters.AddWithValue("@prodi", this.DLProdi.SelectedValue);

                        using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["aktif"].ToString() == "yes")
                                    {
                                        CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                                        ch.Checked = true;
                                    }
                                    else
                                    {
                                        CheckBox ch = (CheckBox)this.GVDosen.Rows[i].FindControl("CBAktif");
                                        ch.Checked = false;
                                    }
                                }
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
        }

        protected void GVDosen_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVDosen.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void BtnTbhDosen_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderAdd.Show();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            ModalPopupExtenderEdit.Show();
            this.LbNd.Text = this.GVDosen.Rows[index].Cells[0].Text;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("SpCariDosen", con);
                    CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDosen.Parameters.AddWithValue("@input", this.GVDosen.Rows[index].Cells[0].Text);

                    using (SqlDataReader rdr = CmdDosen.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this.TbEditNama.Text = rdr["nama"].ToString();
                                this.DLEditProdi.SelectedValue = rdr["prodi"].ToString();
                                // ----- gender ----/
                                if (rdr["gender"] == DBNull.Value)
                                {
                                    this.DLEditGender.SelectedValue = "gender";
                                }
                                else if (rdr["gender"].ToString() == "M")
                                {
                                    this.DLEditGender.SelectedItem.Text = "Laki-laki";
                                }
                                else if (rdr["gender"].ToString() == "F")
                                {
                                    this.DLEditGender.SelectedItem.Text = "Perempuan";
                                }
                                // ----- agama ----- /
                                if (rdr["agama"] == DBNull.Value)
                                {
                                    this.DLEditAgama.SelectedValue = "-1";
                                }
                                else 
                                {
                                    this.DLEditAgama.SelectedItem.Text = rdr["agama"].ToString();
                                }

                                //----- tgl lahir ----/
                                if (rdr["tglahir"] == DBNull.Value)
                                {
                                    this.TBEditTgLahir.Text = "";
                                }
                                else
                                {
                                    DateTime TglLahir = Convert.ToDateTime(rdr["tglahir"]);
                                    this.TBEditTgLahir.Text = TglLahir.ToString("yyyy-MM-dd");
                                }

                                // ----alamat -----/
                                if (rdr["alamat"] == DBNull.Value)
                                {
                                    this.TbEditAlamat.Text = "";
                                }
                                else
                                {
                                    this.TbEditAlamat.Text = rdr["alamat"].ToString();
                                }
                                // ------ HP -----/
                                if (rdr["hp"] == DBNull.Value)
                                {
                                    this.TbEditHp.Text = "";
                                }
                                else
                                {
                                    this.TbEditHp.Text = rdr["hp"].ToString();
                                }
                                // ------ E-Mail -----/
                                if (rdr["email"] == DBNull.Value)
                                {
                                    this.TbEditEmail.Text = "";
                                }
                                else
                                {
                                    this.TbEditEmail.Text = rdr["email"].ToString();
                                }
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
        }

        protected void BtnSaveEdit_Click(object sender, EventArgs e)
        {
            //Response.Write(this.LbNd.Text);   ==> OK <== 

            if (this.TbEditNama.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Nama Harus Diisi');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.DLEditProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.DLEditGender.SelectedValue == "gender")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kelamin');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.DLEditAgama.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Agama');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.TBEditTgLahir.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Tanggal Lahir Harus Diisi');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.TbEditAlamat.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Alamat Harus Diisi');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }
            if (this.TbEditEmail.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Email Harus Diisi');", true);
                this.ModalPopupExtenderEdit.Show();
                return;
            }


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("UPDATE dbo.bak_dosen SET nama=@nama, prodi=@prodi,gender=@gender,agama=@agama,tglahir=@tglahir,alamat=@alamat,hp=@hp,email=@email where nidn=@nidn ", con);
                    CmdDosen.CommandType = System.Data.CommandType.Text;

                    CmdDosen.Parameters.AddWithValue("@nama",TbEditNama.Text);
                    CmdDosen.Parameters.AddWithValue("@prodi",DLEditProdi.SelectedValue);
                    CmdDosen.Parameters.AddWithValue("@gender",DLEditGender.SelectedItem.Text);
                    CmdDosen.Parameters.AddWithValue("@agama",DLEditAgama.SelectedItem.Text);
                    CmdDosen.Parameters.AddWithValue("@tglahir",TBEditTgLahir.Text);
                    CmdDosen.Parameters.AddWithValue("@alamat",TbEditAlamat.Text);
                    CmdDosen.Parameters.AddWithValue("@hp", TbEditHp.Text);
                    CmdDosen.Parameters.AddWithValue("@email",TbEditEmail.Text);
                    CmdDosen.Parameters.AddWithValue("@nidn", this.LbNd.Text);

                    CmdDosen.ExecuteNonQuery();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Status Berhasil');", true);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void CBAktif_CheckedChanged(object sender, EventArgs e)
        {
            // cek punya jadwal mengajar pd semester ini ? tp sistem belum dapat mendeteksi ....


            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand CmdDosen = new SqlCommand("SpStatusDosen", con);
                    CmdDosen.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdDosen.Parameters.AddWithValue("@nidn", this.GVDosen.Rows[index].Cells[0].Text);
                    CmdDosen.Parameters.AddWithValue("@nama",this.GVDosen.Rows[index].Cells[1].Text);

                    CmdDosen.ExecuteNonQuery();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Status Berhasil');", true);
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }
    }
}