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
    //public partial class WebForm4 : System.Web.UI.Page
    public partial class WebForm4 : Bak_Admin
    {
        protected void Re_Load()
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            PnlNewKalender.Enabled = false;
            PnlNewKalender.Visible = false;

            PnlNewGasal.Enabled = false;
            PnlNewGasal.Visible = false;

            PnlNewGenap.Enabled = false;
            PnlNewGenap.Visible = false;

            PnlEditKegiatan.Enabled = false;
            PnlEditKegiatan.Visible = false;

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // ---------- Gridview Kalender Akademik Semester I ------------------
                SqlCommand CmdKal = new SqlCommand("SpListAkademik1", con);
                CmdKal.CommandType = System.Data.CommandType.StoredProcedure;


                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("No");
                TableJadwal.Columns.Add("Jenis");
                TableJadwal.Columns.Add("Kegiatan");
                TableJadwal.Columns.Add("Mulai");
                TableJadwal.Columns.Add("Selesai");
                TableJadwal.Columns.Add("Jenjang");

                using (SqlDataReader rdr = CmdKal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["No"] = rdr["no"];
                            datarow["Jenis"] = rdr["jenis"];
                            datarow["Kegiatan"] = rdr["keg"];
                            // tgl mulai
                            if (rdr["tgl_mulai"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr["tgl_mulai"]);
                                datarow["Mulai"] = TglUjian.ToString("dd-MM-yyyy");
                            }
                            // tgl selesai
                            if (rdr["tgl_sls"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr["tgl_sls"]);
                                datarow["Selesai"] = TglUjian.ToString("dd-MM-yyyy");
                            }
                            datarow["jenjang"] = rdr["tgl_mulai"].ToString().Trim();

                            TableJadwal.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVGasal.DataSource = TableJadwal;
                        this.GVGasal.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        this.GVGasal.DataSource = TableJadwal;
                        this.GVGasal.DataBind();

                        this.PnlNewGasal.Enabled = true;
                        this.PnlNewGasal.Visible = true;
                    }
                }

                // ---------- Gridview Kalender Akademik Semester II ------------------
                SqlCommand CmdKal2 = new SqlCommand("SpListAkademik2", con);
                CmdKal2.CommandType = System.Data.CommandType.StoredProcedure;


                DataTable TableJadwal2 = new DataTable();
                TableJadwal2.Columns.Add("No");
                TableJadwal2.Columns.Add("Jenis");
                TableJadwal2.Columns.Add("Kegiatan");
                TableJadwal2.Columns.Add("Mulai");
                TableJadwal2.Columns.Add("Selesai");
                TableJadwal2.Columns.Add("Jenjang");

                using (SqlDataReader rdr2 = CmdKal2.ExecuteReader())
                {
                    if (rdr2.HasRows)
                    {
                        while (rdr2.Read())
                        {
                            DataRow datarow = TableJadwal2.NewRow();
                            datarow["No"] = rdr2["no"];
                            datarow["Jenis"] = rdr2["jenis"];
                            datarow["Kegiatan"] = rdr2["keg"];
                            // tgl mulai
                            if (rdr2["tgl_mulai"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr2["tgl_mulai"]);
                                datarow["Mulai"] = TglUjian.ToString("dd-MM-yyyy");
                            }
                            // tgl selesai
                            if (rdr2["tgl_sls"] == DBNull.Value)
                            {
                            }
                            else
                            {
                                DateTime TglUjian = Convert.ToDateTime(rdr2["tgl_sls"]);
                                datarow["Selesai"] = TglUjian.ToString("dd-MM-yyyy");
                            }

                            datarow["jenjang"] = rdr2["tgl_mulai"].ToString().Trim();

                            TableJadwal2.Rows.Add(datarow);
                        }
                        //Fill Gridview
                        this.GVGenap.DataSource = TableJadwal2;
                        this.GVGenap.DataBind();
                    }
                    else
                    {
                        //clear Gridview
                        TableJadwal2.Rows.Clear();
                        TableJadwal2.Clear();
                        this.GVGenap.DataSource = TableJadwal2;
                        this.GVGenap.DataBind();

                        this.PnlNewGenap.Enabled = true;
                        this.PnlNewGenap.Visible = true;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PnlAddKeg1.Enabled = false;
                PnlAddKeg1.Visible = false;

                PnlAddKeg2.Enabled = false;
                PnlAddKeg2.Visible = false;

                PnlNewKalender.Enabled = false;
                PnlNewKalender.Visible = false;

                PnlEditKegiatan.Enabled = false;
                PnlEditKegiatan.Visible = false;


                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    // ---------- Gridview Kalender Akademik Semester I ------------------
                    SqlCommand CmdKal = new SqlCommand("SpListAkademik1", con);
                    CmdKal.CommandType = System.Data.CommandType.StoredProcedure;


                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("No");
                    TableJadwal.Columns.Add("Jenis");
                    TableJadwal.Columns.Add("Kegiatan");
                    TableJadwal.Columns.Add("Mulai");
                    TableJadwal.Columns.Add("Selesai");
                    TableJadwal.Columns.Add("Jenjang");

                    using (SqlDataReader rdr = CmdKal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["No"] = rdr["no"];
                                datarow["Jenis"] = rdr["jenis"];
                                datarow["Kegiatan"] = rdr["keg"];
                                // tgl mulai
                                if (rdr["tgl_mulai"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_mulai"]);
                                    datarow["Mulai"] = TglUjian.ToString("dd-MM-yyyy");
                                }
                                // tgl selesai
                                if (rdr["tgl_sls"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["tgl_sls"]);
                                    datarow["Selesai"] = TglUjian.ToString("dd-MM-yyyy");
                                }

                                datarow["Jenjang"] = rdr["jenjang"].ToString().Trim();

                                TableJadwal.Rows.Add(datarow);
                            }
                            //Fill Gridview
                            this.GVGasal.DataSource = TableJadwal;
                            this.GVGasal.DataBind();

                            PnlNewGasal.Enabled = false;
                            PnlNewGasal.Visible = false;
                        }
                        else
                        {
                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            this.GVGasal.DataSource = TableJadwal;
                            this.GVGasal.DataBind();

                            this.PnlNewGasal.Enabled = true;
                            this.PnlNewGasal.Visible = true;
                        }
                    }

                    // ---------- Gridview Kalender Akademik Semester II ------------------
                    SqlCommand CmdKal2 = new SqlCommand("SpListAkademik2", con);
                    CmdKal2.CommandType = System.Data.CommandType.StoredProcedure;


                    DataTable TableJadwal2 = new DataTable();
                    TableJadwal2.Columns.Add("No");
                    TableJadwal2.Columns.Add("Jenis");
                    TableJadwal2.Columns.Add("Kegiatan");
                    TableJadwal2.Columns.Add("Mulai");
                    TableJadwal2.Columns.Add("Selesai");
                    TableJadwal2.Columns.Add("Jenjang");

                    using (SqlDataReader rdr2 = CmdKal2.ExecuteReader())
                    {
                        if (rdr2.HasRows)
                        {
                            while (rdr2.Read())
                            {
                                DataRow datarow = TableJadwal2.NewRow();
                                datarow["No"] = rdr2["no"];
                                datarow["Jenis"] = rdr2["jenis"];
                                datarow["Kegiatan"] = rdr2["keg"];
                                // tgl mulai
                                if (rdr2["tgl_mulai"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglMulai = Convert.ToDateTime(rdr2["tgl_mulai"]);
                                    datarow["Mulai"] = TglMulai.ToString("dd-MM-yyyy");
                                }
                                // tgl selesai
                                if (rdr2["tgl_sls"] == DBNull.Value)
                                {
                                }
                                else
                                {
                                    DateTime TglSls = Convert.ToDateTime(rdr2["tgl_sls"]);
                                    datarow["Selesai"] = TglSls.ToString("dd-MM-yyyy");
                                }
                                datarow["Jenjang"] = rdr2["jenjang"].ToString().Trim();

                                TableJadwal2.Rows.Add(datarow);
                            }
                            //Fill Gridview
                            this.GVGenap.DataSource = TableJadwal2;
                            this.GVGenap.DataBind();

                            this.PnlNewGenap.Enabled = false;
                            this.PnlNewGenap.Visible = false;
                        }
                        else
                        {
                            //clear Gridview
                            TableJadwal2.Rows.Clear();
                            TableJadwal2.Clear();
                            this.GVGenap.DataSource = TableJadwal2;
                            this.GVGenap.DataBind();

                            this.PnlNewGenap.Enabled = true;
                            this.PnlNewGenap.Visible = true;
                        }
                    }
                }
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            // from validation
            if (this.DLJenisKeg.SelectedValue == "Jenis Kegiatan")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kegiatan');", true);
                return;
            }
            if (this.TbKeg.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Uraian Kegiatan');", true);
                return;
            }
            if (this.TbMulai.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            if (this.TbSelesai.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            if (this.DLJenjang.SelectedValue.Trim() == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jenjang');", true);
                return;
            }


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdKeg1 = new SqlCommand("SpInKeg1", con);
                CmdKeg1.CommandType = System.Data.CommandType.StoredProcedure;

                CmdKeg1.Parameters.AddWithValue("@jenis_keg", this.DLJenisKeg.SelectedValue);
                CmdKeg1.Parameters.AddWithValue("@keg", this.TbKeg.Text);
                CmdKeg1.Parameters.AddWithValue("@tgl_mulai", Request.Form[this.TbMulai.UniqueID]);
                CmdKeg1.Parameters.AddWithValue("@tgl_selesai", Request.Form[this.TbSelesai.UniqueID]);
                CmdKeg1.Parameters.AddWithValue("@jenjang", this.DLJenjang.SelectedValue.Trim());

                CmdKeg1.ExecuteNonQuery();
            }

            //reload page
            Re_Load();
        }

        protected void BtnOKGenap_Click(object sender, EventArgs e)
        {
            // from validation
            if (this.DLJenisKeg2.SelectedValue == "Jenis Kegiatan")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kegiatan');", true);
                return;
            }
            if (this.TbKeg2.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Uraian Kegiatan');", true);
                return;
            }
            if (this.TbMulai2.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            if (this.TbSelesai2.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            if (this.DLJenjang2.SelectedValue.Trim() == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Jenjang');", true);
                return;
            }


            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdKeg1 = new SqlCommand("SpInKeg2", con);
                CmdKeg1.CommandType = System.Data.CommandType.StoredProcedure;

                CmdKeg1.Parameters.AddWithValue("@jenis_keg", this.DLJenisKeg2.SelectedValue);
                CmdKeg1.Parameters.AddWithValue("@keg", this.TbKeg2.Text);
                CmdKeg1.Parameters.AddWithValue("@tgl_mulai", Request.Form[this.TbMulai2.UniqueID]);
                CmdKeg1.Parameters.AddWithValue("@tgl_selesai", Request.Form[this.TbSelesai2.UniqueID]);
                CmdKeg1.Parameters.AddWithValue("@jenjang", this.DLJenjang2.SelectedValue.Trim());

                CmdKeg1.ExecuteNonQuery();
            }

            //reload page
            Re_Load();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // from validation
            if (this.TbNewTahun.Text == "" || this.TbNewTahun.Text == "NULL")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tahun Akademik Baru');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdNewYear = new SqlCommand("INSERT INTO dbo.bak_kal " +
                "(jenis ,keg ,tgl_mulai ,tgl_sls ,semester,thn) " +
                "VALUES  ( @jenis_keg ,@keg, @tgl_mulai , @tgl_selesai ,@semester, @maxthn)", con);

                CmdNewYear.CommandType = System.Data.CommandType.Text;

                CmdNewYear.Parameters.AddWithValue("@jenis_keg", "New");
                CmdNewYear.Parameters.AddWithValue("@keg", "New");
                CmdNewYear.Parameters.AddWithValue("@tgl_mulai", "2000-01-01");
                CmdNewYear.Parameters.AddWithValue("@tgl_selesai", "2000-01-01");
                CmdNewYear.Parameters.AddWithValue("@semester", "new");
                CmdNewYear.Parameters.AddWithValue("@maxthn", this.TbNewTahun.Text);

                CmdNewYear.ExecuteNonQuery();

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // from validation
            if (this.DLJenisKeg3.SelectedValue == "Jenis Kegiatan")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kegiatan');", true);
                return;
            }
            if (this.TbKeg3.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Uraian Kegiatan');", true);
                return;
            }
            if (this.TbMulai3.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            if (this.TbSelesai3.UniqueID.ToString() == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tanggal Mulai Kegiatan');", true);
                return;
            }

            string TbMulai3 = Request.Form[this.TbMulai3.UniqueID];
            string thn1 = TbMulai3.Substring(0, 4);
            string bln1 = TbMulai3.Substring(5, 2);
            string tgl1 = TbMulai3.Substring(8, 2);

            string TbSelesai3 = Request.Form[this.TbSelesai3.UniqueID];
            string thn2 = TbSelesai3.Substring(0, 4);
            string bln2 = TbSelesai3.Substring(5, 2);
            string tgl2 = TbSelesai3.Substring(8, 2);

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdNewYear = new SqlCommand("SpUpKegAkedemik", con);
                CmdNewYear.CommandType = System.Data.CommandType.StoredProcedure;

                CmdNewYear.Parameters.AddWithValue("@no", this.LbNoKeg.Text);
                CmdNewYear.Parameters.AddWithValue("@jenis_keg", this.DLJenisKeg3.SelectedItem.Text);
                CmdNewYear.Parameters.AddWithValue("@keg", this.TbKeg3.Text);
                CmdNewYear.Parameters.AddWithValue("@tgl_mulai", Convert.ToDateTime(thn1 + "-" + bln1 + "-" + tgl1));
                CmdNewYear.Parameters.AddWithValue("@tgl_sls", Convert.ToDateTime(thn2 + "-" + bln2 + "-" + tgl2));

                CmdNewYear.ExecuteNonQuery();
            }

            //reload page
            Re_Load();
        }

        protected void BtnNewKal_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            PnlNewKalender.Enabled = true;
            PnlNewKalender.Visible = true;
        }

        protected void BtNewGasal_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = true;
            PnlAddKeg1.Visible = true;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            // get tahun terakhir
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // ---------- Gridview Kalender Akademik Semester I ------------------
                SqlCommand CmdKal = new SqlCommand("select TOP 1 bak_kal.thn from bak_kal ORDER BY thn DESC", con);
                CmdKal.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdKal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbTahun.Text = rdr["thn"].ToString();
                        }
                    }
                    else
                    {
                        this.TbTahun.Text = "NULL";
                    }
                }
            }
        }

        protected void BtNewGenap_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = true;
            PnlAddKeg2.Visible = true;

            // get tahun terakhir
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // ---------- Gridview Kalender Akademik Semester I ------------------
                SqlCommand CmdKal = new SqlCommand("select TOP 1 bak_kal.thn from bak_kal ORDER BY thn DESC", con);
                CmdKal.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdKal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbTahun2.Text = rdr["thn"].ToString();
                        }
                    }
                    else
                    {
                        this.TbTahun2.Text = "NULL";
                    }
                }
            }
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = true;
            PnlAddKeg1.Visible = true;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            PnlEditKegiatan.Enabled = false;
            PnlEditKegiatan.Visible = false;

            // get tahun terakhir
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // ---------- Gridview Kalender Akademik Semester I ------------------
                SqlCommand CmdKal = new SqlCommand("select TOP 1 bak_kal.thn from bak_kal ORDER BY thn DESC", con);
                CmdKal.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdKal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbTahun.Text = rdr["thn"].ToString();
                        }
                    }
                    else
                    {
                        this.TbTahun.Text = "NULL";
                    }
                }
            }
        }

        protected void Btn_del_Click(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            int no = Convert.ToInt16(this.GVGasal.Rows[index].Cells[3].Text);

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdDel = new SqlCommand("SpDelKegAkademik", con);
                CmdDel.CommandType = System.Data.CommandType.StoredProcedure;

                CmdDel.Parameters.AddWithValue("@nomor", no);

                CmdDel.ExecuteNonQuery();

            }

            //reload page
            Re_Load();
        }

        protected void BtnEditKeg_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            PnlEditKegiatan.Enabled = true;
            PnlEditKegiatan.Visible = true;

            PnlNewKalender.Enabled = false;
            PnlNewKalender.Visible = false;

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            this.LbNoKeg.Text = this.GVGasal.Rows[index].Cells[3].Text;
            this.DLJenisKeg3.SelectedItem.Text = this.GVGasal.Rows[index].Cells[4].Text;
            this.TbKeg3.Text = this.GVGasal.Rows[index].Cells[5].Text;
            // ----tanggal mulai
            string TbMulai3 = this.GVGasal.Rows[index].Cells[6].Text;
            string thn1 = TbMulai3.Substring(6, 4);
            string bln1 = TbMulai3.Substring(3, 2);
            string tgl1 = TbMulai3.Substring(0, 2);
            // ----tanggal selesai
            string TbSelesai3 = this.GVGasal.Rows[index].Cells[7].Text;
            string thn2 = TbSelesai3.Substring(6, 4);
            string bln2 = TbSelesai3.Substring(3, 2);
            string tgl2 = TbSelesai3.Substring(0, 2);
            // ---- fotmat tanggal mulai & selesai
            this.TbMulai3.Text = thn1 + "-" + bln1 + "-" + tgl1;
            this.TbSelesai3.Text = thn2 + "-" + bln2 + "-" + tgl2;
        }

        protected void Btn_Add_Click1(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = true;
            PnlAddKeg2.Visible = true;

            PnlEditKegiatan.Enabled = false;
            PnlEditKegiatan.Visible = false;

            // get tahun terakhir
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                // ---------- Gridview Kalender Akademik Semester I ------------------
                SqlCommand CmdKal = new SqlCommand("select TOP 1 bak_kal.thn from bak_kal ORDER BY thn DESC", con);
                CmdKal.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader rdr = CmdKal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            this.TbTahun2.Text = rdr["thn"].ToString();
                        }
                    }
                    else
                    {
                        this.TbTahun2.Text = "NULL";
                    }
                }
            }
        }

        protected void Btn_del_Click1(object sender, EventArgs e)
        {
            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            int no = Convert.ToInt16(this.GVGenap.Rows[index].Cells[3].Text);

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand CmdDel = new SqlCommand("SpDelKegAkademik", con);
                CmdDel.CommandType = System.Data.CommandType.StoredProcedure;

                CmdDel.Parameters.AddWithValue("@nomor", no);

                CmdDel.ExecuteNonQuery();

            }

            //reload page
            Re_Load();
        }

        protected void BtnEditKeg2_Click(object sender, EventArgs e)
        {
            PnlAddKeg1.Enabled = false;
            PnlAddKeg1.Visible = false;

            PnlAddKeg2.Enabled = false;
            PnlAddKeg2.Visible = false;

            PnlEditKegiatan.Enabled = true;
            PnlEditKegiatan.Visible = true;

            PnlNewKalender.Enabled = false;
            PnlNewKalender.Visible = false;

            // get row index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            this.LbNoKeg.Text = this.GVGenap.Rows[index].Cells[3].Text;
            this.DLJenisKeg3.SelectedItem.Text = this.GVGenap.Rows[index].Cells[4].Text;
            this.TbKeg3.Text = this.GVGenap.Rows[index].Cells[5].Text;
            // ----tanggal mulai
            string TbMulai3 = this.GVGenap.Rows[index].Cells[6].Text;
            string thn1 = TbMulai3.Substring(6, 4);
            string bln1 = TbMulai3.Substring(3, 2);
            string tgl1 = TbMulai3.Substring(0, 2);
            // ----tanggal selesai
            string TbSelesai3 = this.GVGenap.Rows[index].Cells[7].Text;
            string thn2 = TbSelesai3.Substring(6, 4);
            string bln2 = TbSelesai3.Substring(3, 2);
            string tgl2 = TbSelesai3.Substring(0, 2);
            // ---- fotmat tanggal mulai & selesai
            this.TbMulai3.Text = thn1 + "-" + bln1 + "-" + tgl1;
            this.TbSelesai3.Text = thn2 + "-" + bln2 + "-" + tgl2;
        }

        protected void GVGasal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }

        protected void GVGenap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
}