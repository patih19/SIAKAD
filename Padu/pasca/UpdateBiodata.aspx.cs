using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Padu.pasca
{
    public partial class UpdateBiodata : MhsPasca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl control = (HtmlGenericControl)base.Master.FindControl("NavBiodata");
                control.Attributes.Add("class", "dropdown active opened");
                //HtmlGenericControl control2 = (HtmlGenericControl)base.Master.FindControl("SubNavKHS");
                //control2.Attributes.Add("style", "display: block;");
            }
        }

        protected void BtnSvBiodata_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TbNIK.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI Nomor Induk Kependudukan');", true);
                return;
            }
            if (this.TbNIK.Text.Length > 16)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('NIK lebih dari 16 digit !');", true);
                return;
            }
            if (this.TbNIK.Text.Length < 13)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('NIK kurang dari 13 digit !');", true);
                return;
            }
            if (this.TbTmpLahir.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI KOTA/KAB. TEMPAT LAHIR');", true);
                return;
            }
            if (this.TBTtl.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TANGGAL LAHIR');", true);
                return;
            }
            if (this.DLGender.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JENIS KELAMIN');", true);
                return;
            }
            if (this.DropDownListProv.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PROVINSI');", true);
                return;
            }
            if (this.DropDownListKab.SelectedValue == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KOTA/KABUPATEN');", true);
                return;
            }
            if (this.DropDownListKec.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KECAMATAN');", true);
                return;
            }
            if (this.DropDownListDesa.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH DESA/KELURAHAN');", true);
                return;
            }

            if (this.TbAlamat.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI ALAMAT RUMAH');", true);
                return;
            }
            if (this.TbKdPOS.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI KODE POS ALAMAT RUMAH');", true);
                return;
            }
            if (this.TBHp.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NOMOR HP');", true);
                return;
            }
            if (this.TbEmail.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI ALAMAT EMAIL');", true);
                return;
            }

            // ---------------- INSERT DATA -------------------------
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //SqlTransaction trans = con.BeginTransaction();

                SqlCommand CmdBiodata = new SqlCommand(" UPDATE dbo.bak_mahasiswa SET nik=@nik,tmp_lahir=@tmp_lahir,ttl=@ttl,gender=@gender,agama=@agama,prov=@prov,kota=@kota,kec=@kec,desa=@desa,alamat=@alamat,kd_pos=@kd_pos,hp=@hp,email=@email WHERE npm=@npm", con);
                CmdBiodata.CommandType = System.Data.CommandType.Text;

                CmdBiodata.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                CmdBiodata.Parameters.AddWithValue("@nik", TbNIK.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@tmp_lahir",this.TbTmpLahir.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@ttl", Convert.ToDateTime(TBTtl.Text.Trim()));
                CmdBiodata.Parameters.AddWithValue("@gender", DLGender.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@agama", DLAgama.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@prov", DropDownListProv.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kota", DropDownListKab.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kec", DropDownListKec.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@desa", DropDownListDesa.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@alamat", TbAlamat.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kd_pos", TbKdPOS.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@hp", TBHp.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@email", TbEmail.Text.Trim());

                CmdBiodata.ExecuteNonQuery();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Berhasil Diupdate');", true);

                // Response.Redirect("~/account/pasca/UpdateBiodata.aspx");
            }

        }
    }
}