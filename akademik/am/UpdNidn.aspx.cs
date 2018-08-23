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
    public partial class WebForm32 : Bak_staff
    {
        public string _NIDN
        {
            get { return (this.ViewState["nidn"].ToString()); }
            set { this.ViewState["nidn"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelUbahNIDN.Enabled = false;
                this.PanelUbahNIDN.Visible = false;
                this.TbOldNIDN.Text = "";
            }
        }

        protected void BtnCari_Click(object sender, EventArgs e)
        {
            //clear label result
            this.LbResult.Text = "";

            if (this.TbOldNIDN.Text == "")
            {
                this.PanelUbahNIDN.Enabled = false;
                this.PanelUbahNIDN.Visible = false;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NIDN');", true);
                return;
            }

            //read Dosen
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand CmdPasswd = new SqlCommand("SELECT bak_dosen.no, bak_dosen.nidn, bak_dosen.nama, bak_prog_study.prog_study, bak_dosen.aktif"+
                                                      " FROM bak_dosen INNER JOIN bak_prog_study ON bak_dosen.prodi = bak_prog_study.id_prog_study WHERE bak_dosen.nidn = @nidn", con);
                CmdPasswd.CommandType = System.Data.CommandType.Text;

                CmdPasswd.Parameters.AddWithValue("@nidn", this.TbOldNIDN.Text);


                using (SqlDataReader rdr = CmdPasswd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {                     
                        this.PanelUbahNIDN.Enabled = true;
                        this.PanelUbahNIDN.Visible = true;

                        this.LbResult.Text = "Data Ditemukan";
                        this.LbResult.ForeColor = System.Drawing.Color.Green;

                        while (rdr.Read())
                        {
                            this.LbNama.Text = rdr["nama"].ToString();
                            this.LbOldNidn.Text = rdr["nidn"].ToString();
                            this.LbProdi.Text = rdr["prog_study"].ToString();
                            this.LbStatus.Text = rdr["aktif"].ToString();
                            this._NIDN = rdr["nidn"].ToString();
                        }
                    }
                    else
                    {
                        this.LbResult.Text = "NIDN Tidak Ditemukan";
                        this.LbResult.ForeColor = System.Drawing.Color.Red;

                        this.PanelUbahNIDN.Enabled = false;
                        this.PanelUbahNIDN.Visible = false;

                        this._NIDN = "";

                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan, PERIKSA NPM');", true);
                        return;
                    }
                }
            }
        }

        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (this.TbNewNIDN.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NIDN BARU');", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction Tran = con.BeginTransaction();

                try
                {
                    SqlCommand CmdUpdateNidn = new SqlCommand("SpUpdateNidn", con, Tran);
                    CmdUpdateNidn.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdUpdateNidn.Parameters.AddWithValue("@oldnidn", this._NIDN);
                    CmdUpdateNidn.Parameters.AddWithValue("@newnidn", this.TbNewNIDN.Text);

                    CmdUpdateNidn.ExecuteNonQuery();

                    this.LbResult.Text = System.String.Empty;
                    this.TbOldNIDN.Text = System.String.Empty;
                    this.TbNewNIDN.Text = System.String.Empty;

                    this.PanelUbahNIDN.Enabled = false;
                    this.PanelUbahNIDN.Visible = false;

                    this._NIDN = "";

                    Tran.Commit();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('UPDATE NIDN BERHASIL');", true);

                } catch(Exception ex)
                {
                    Response.Write(ex.Message);

                    Tran.Rollback();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }


            }

        }
    }
}