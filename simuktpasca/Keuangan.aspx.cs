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

namespace simuktpasca
{
    public partial class Keuangan : User
    {
        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }

        public string _ThnAngkatan
        {
            get { return this.ViewState["ThnAngkatan"].ToString(); }
            set { this.ViewState["ThnAngkatan"] = (object)value; }
        }

        public decimal _TotalBayar
        {
            get { return Convert.ToDecimal(this.ViewState["TotalBebanAwal"].ToString()); }
            set { this.ViewState["TotalBebanAwal"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavPembayaran");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavPembayaran");
                control2.Attributes.Add("style", "display: block;");

                this.PanelLihatTagihan.Enabled = false;
                this.PanelLihatTagihan.Visible = false;
            }
        }

        protected bool PopulateBiodata( string npm)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        "SELECT        bak_mahasiswa.npm, bak_mahasiswa.nama, bak_prog_study.prog_study, bak_mahasiswa.thn_angkatan " +
                        "FROM            bak_mahasiswa INNER JOIN " +
                                                 "bak_prog_study ON bak_mahasiswa.id_prog_study = bak_prog_study.id_prog_study " +
                        "WHERE npm = @npm AND bak_mahasiswa.status NOT IN ('K', 'D', 'L') " +
                        "", con);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@npm", npm);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this.LbNPM.Text = rdr["npm"].ToString();
                                this.LbNama.Text = rdr["nama"].ToString();
                                this.LbProdi.Text = rdr["prog_study"].ToString();
                                this.LbAngkatan.Text = rdr["thn_angkatan"].ToString();
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        protected void PopulateTagihan(string npm)
        {
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        "SELECT semester, biaya, jenis_tagihan FROM dbo.keu_tagihan WHERE npm =@npm" +
                        "", con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@npm", npm);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Biaya");
                    Table.Columns.Add("Jenis Biaya");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();
                                datarow["Semester"] = rdr["semester"];
                                string Biaya = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", Convert.ToInt32(rdr["biaya"].ToString()));
                                datarow["Biaya"] = Biaya;
                                datarow["Jenis Biaya"] = rdr["jenis_tagihan"];

                                Table.Rows.Add(datarow);
                            }

                            this.PanelLihatTagihan.Enabled = true;
                            this.PanelLihatTagihan.Visible = true;

                            //Fill Gridview
                            this.GvTagihan.DataSource = Table;
                            this.GvTagihan.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        protected void PopulateHistoryBayar( string npm)
        {
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        "SELECT        nomor, billingNo, payeeId, billRef4, amount_total, cicilan, post_date, pay_date, status, keterangan "+
                        "FROM            keu_posting_bank "+
                        "WHERE payeeId = @npm "+
                        "ORDER BY billRef4 DESC "+
                        "", con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@npm", npm);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("No Billing");
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Biaya");
                    Table.Columns.Add("Cicilan");
                    Table.Columns.Add("Tanggal Aktif");
                    Table.Columns.Add("Tanggal Bayar");
                    Table.Columns.Add("Status");
                    Table.Columns.Add("Keterangan");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["No Billing"] = rdr["billingNo"];
                                datarow["Semester"] = rdr["billRef4"];
                                string Biaya = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", Convert.ToInt32(rdr["amount_total"].ToString()));
                                datarow["Biaya"] = Biaya;
                                datarow["Cicilan"] = rdr["cicilan"];
                                datarow["Tanggal Aktif"] = rdr["post_date"];
                                if (rdr["pay_date"] != DBNull.Value)
                                {
                                    datarow["Tanggal Bayar"] = rdr["pay_date"];
                                }                               
                                datarow["Status"] = rdr["status"];
                                datarow["Keterangan"] = rdr["keterangan"];

                                Table.Rows.Add(datarow);
                            }

                            this.PanelLihatTagihan.Enabled = true;
                            this.PanelLihatTagihan.Visible = true;

                            //Fill Gridview
                            this.GvHistoryBayar.DataSource = Table;
                            this.GvHistoryBayar.DataBind();
                        } else
                        {
                            this.PanelLihatTagihan.Enabled = false;
                            this.PanelLihatTagihan.Visible = false;

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('DATA PEMBAYARAN TIDAK DITEMUKAN...');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                }
            }
        }

        int TotalTerbayar = 0;
        public void HitungKekurangan(GridView gridview)
        {
            for (int currentRow = 0; currentRow < gridview.Rows.Count; currentRow++)
            {
                bool hutang = true;

                //Lunas
                if (gridview.Rows[currentRow].Cells[6].Text.Trim() == "paid" || gridview.Rows[currentRow].Cells[6].Text.Trim() == "offline")
                {
                    hutang = false;

                    string StrBayar = gridview.Rows[currentRow].Cells[2].Text.Trim();
                    StrBayar = StrBayar.Replace("Rp", "").Trim();
                    StrBayar = StrBayar.Replace(".", "").Trim();
                    int Bayar = Convert.ToInt32(StrBayar);

                    TotalTerbayar += Bayar;

                    string FormattedLunas = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", TotalTerbayar);
                    this.LbTerbayar.Text = FormattedLunas;
                }
                // Hutang
                else if (hutang)
                {
                    string StrHutang = gridview.Rows[currentRow].Cells[2].Text.Trim();
                    StrHutang = StrHutang.Replace("Rp", "").Trim();
                    StrHutang = StrHutang.Replace(".", "").Trim();
                    int Hutang = Convert.ToInt32(StrHutang);

                    gridview.Rows[currentRow].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb59f");
                }
            }
        }

        protected void BtnLihatTagihan_Click(object sender, EventArgs e)
        {
            this.TbNPM.Text.Trim();

            if (PopulateBiodata(this.TbNPM.Text.Trim()))
            {
                PopulateHistoryBayar(this.TbNPM.Text.Trim());
                PopulateTagihan(this.TbNPM.Text.Trim());
                HitungKekurangan(this.GvHistoryBayar);
                HitungTotalTagihan(this.GvTagihan);

                string tagihan = this.LbTagihan.Text.Replace("Rp", "");
                tagihan = tagihan.Replace(".", "");
                string bayar = this.LbTerbayar.Text.Replace("Rp", "");
                bayar = bayar.Replace(".", "");

                int kekurangan = Convert.ToInt32(tagihan) - Convert.ToInt32(bayar);
                string FormatedKekurangan = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", kekurangan);

                this.LbKekurangan.Text = Convert.ToString(FormatedKekurangan);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mahasiswa Tidak Ditemukan Atau Satus Mahasiswa Tidak Aktif/Cuti');", true);
                return;
            }
        }

        int TotTagihan = 0;
        public void HitungTotalTagihan(GridView gridview)
        {
            for (int currentRow = 0; currentRow < gridview.Rows.Count; currentRow++)
            {
                string StrTagihan = gridview.Rows[currentRow].Cells[1].Text.Trim();
                StrTagihan = StrTagihan.Replace("Rp", "").Trim();
                StrTagihan = StrTagihan.Replace(".", "").Trim();
                int Tagihan = Convert.ToInt32(StrTagihan);

                TotTagihan += Tagihan;

                string FormatedTagihan = string.Format(new System.Globalization.CultureInfo("id"), "{0:c}", TotTagihan);
                this.LbTagihan.Text = FormatedTagihan;
            }
        }
    }

}