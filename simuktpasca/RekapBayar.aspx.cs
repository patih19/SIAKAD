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
    public partial class RekapBayar : User
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavPembayaran");
                control.Attributes.Add("class", "dropdown active opened");
                HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavPembayaran");
                control2.Attributes.Add("style", "display: block;");

                this.PanelContentTagihan.Enabled = false;
                this.PanelContentTagihan.Visible = false;
            }
        }

        protected void BtnOpenTagihan_Click(object sender, EventArgs e)
        {
            PopulateHistoryBayar(this.TbThnAngkatan.Text.Trim());
        }

        protected void PopulateHistoryBayar(string TahunAngkatan)
        {
            string CS = ConfigurationManager.ConnectionStrings["PascaDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        " SELECT ROW_NUMBER() OVER(ORDER BY keu_posting_bank.post_date ASC) as nomor, billingNo, payeeId, name, bak_prog_study.prog_study, thn_angkatan, billRef4, amount_total, keu_posting_bank.post_date, keu_posting_bank.status, keu_posting_bank.keterangan "+
                        " FROM            keu_posting_bank INNER JOIN UntidarDb.dbo.bak_mahasiswa ON  UntidarDb.dbo.bak_mahasiswa.npm = dbo.keu_posting_bank.payeeId COLLATE Latin1_General_CI_AS "+
                                        " INNER JOIN UntidarDb.dbo.bak_prog_study ON UntidarDb.dbo.bak_prog_study.id_prog_study = UntidarDb.dbo.bak_mahasiswa.id_prog_study "+
                        " WHERE thn_angkatan=@ThnAngkatan" +
                        "", con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ThnAngkatan", TahunAngkatan);

                    DataTable Table = new DataTable();
                    Table.Columns.Add("No");
                    Table.Columns.Add("Bill");
                    Table.Columns.Add("NPM");
                    Table.Columns.Add("Nama");
                    Table.Columns.Add("Program Studi");
                    Table.Columns.Add("Tahun");
                    Table.Columns.Add("Semester");
                    Table.Columns.Add("Biaya");
                    Table.Columns.Add("Status");
                    Table.Columns.Add("Keterangan");

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.PanelContentTagihan.Enabled = true;
                            this.PanelContentTagihan.Visible = true;

                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();

                                datarow["No"] = rdr["nomor"];
                                datarow["Bill"] = rdr["billingNo"];
                                datarow["NPM"] = rdr["payeeId"];
                                datarow["Nama"] = rdr["name"];
                                datarow["Program Studi"] = rdr["prog_study"];
                                datarow["Tahun"] = rdr["thn_angkatan"];
                                datarow["Semester"] = rdr["billRef4"];
                                datarow["Biaya"] = rdr["amount_total"];
                                datarow["Status"] = rdr["status"];
                                datarow["Keterangan"] = rdr["keterangan"];

                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvRekapBayar.DataSource = Table;
                            this.GvRekapBayar.DataBind();
                        }
                        else
                        {
                            this.PanelContentTagihan.Enabled = false;
                            this.PanelContentTagihan.Visible = false;

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
    }
}