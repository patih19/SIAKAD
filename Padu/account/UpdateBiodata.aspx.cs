using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Padu.account
{
    //public partial class WebForm7 : System.Web.UI.Page
    public partial class WebForm7 : Mhs_account
    {
        //------------- LogOut ------------------------------//
        protected override void OnInit(EventArgs e)
        {
            // Your code
            base.OnInit(e);
            keluar.ServerClick += new EventHandler(logout_ServerClick);
        }

        protected void logout_ServerClick(object sender, EventArgs e)
        {
            //Your Code here....
            this.Session["Name"] = (object)null;
            this.Session["Passwd"] = (object)null;
            this.Session["jenjang"] = (object)null;
            this.Session["prodi"] = (object)null;
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.Remove("jenjang");
            this.Session.Remove("prodi");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------

        private string StringProv;
        private string StringKota;
        private string StringKab;
        private string StringDesa;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // -----------------------------------Keterangan Biodata -----------------------------------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //SqlTransaction trans = con.BeginTransaction();

                    SqlCommand CmdBiodata = new SqlCommand("SpBiodataMhs", con);
                    //CmdPeriodik.Transaction = trans;
                    CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdBiodata.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                    // CmdBiodata.Parameters.AddWithValue("@no_tagihan", this.GVPendaftar.SelectedRow.Cells[1].Text);

                    using (SqlDataReader rdr = CmdBiodata.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                this.TbNama.Text = rdr["nama"].ToString().Trim();
                                this.TbNIK.Text = rdr["nik"].ToString().Trim();
                                this.TbTmpLhir.Text = rdr["tmp_lahir"].ToString().Trim();
                                if (rdr["ttl"] == DBNull.Value)
                                {

                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["ttl"]);
                                    this.TBTtl.Text = TglUjian.ToString("yyyy-MM-dd");
                                }

                                this.DLGender.SelectedValue = rdr["gender"].ToString().Trim();
                                this.DLAgama.SelectedValue = rdr["agama"].ToString().Trim();
                                this.DLDarah.SelectedValue = rdr["darah"].ToString().Trim();
                                this.TbAlamat.Text = rdr["alamat"].ToString().Trim();

                                StringProv = rdr["prov"].ToString().Trim();
                                StringKota = rdr["kota"].ToString().Trim();
                                StringKab = rdr["kec"].ToString().Trim();
                                StringDesa = rdr["desa"].ToString().Trim();

                                this.TbKdPOS.Text = rdr["kd_pos"].ToString().Trim();
                                this.TBHp.Text = rdr["hp"].ToString().Trim();
                                this.TbEmail.Text = rdr["email"].ToString().Trim();

                                this.TbSekolah.Text = rdr["sekolah"].ToString().Trim();
                                this.DLJurusan.SelectedValue = rdr["jurusan"].ToString().Trim();
                                this.DLStatusSekolah.SelectedValue = rdr["status_sekolah"].ToString().Trim();
                                this.TBThnLls.Text = rdr["thn_lls"].ToString().Trim();

                                this.TBAdik.Text = rdr["adik"].ToString().Trim();
                                this.TbKakak.Text = rdr["kakak"].ToString().Trim();
                                this.TbIbu.Text = rdr["ibu"].ToString().Trim();
                                this.TbAyah.Text = rdr["ayah"].ToString().Trim();
                                this.DLPendidikanAyah.SelectedValue = rdr["pendidikan_ayah"].ToString().Trim();
                                this.DLPendidikanIbu.SelectedValue = rdr["pendidikan_ibu"].ToString().Trim();
                                this.DLPekerjaanAyah.SelectedValue = rdr["pekerjaan_ayah"].ToString().Trim();
                                this.DLPekerjaanIbu.SelectedValue = rdr["pekerjaan_ibu"].ToString().Trim();
                                this.DLPenghasilan.SelectedValue = rdr["penghasilan_ortu"].ToString().Trim();
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

                    // ------- ID Desa Information  -------
                    SqlCommand CmdDesa = new SqlCommand("select id_desa from daerah_desa where id_kec=@IdKecamatan AND nama=@desa ", con);
                    CmdDesa.CommandType = System.Data.CommandType.Text;
                    CmdDesa.Parameters.AddWithValue("@IdKecamatan", this.CascadingDropDownKec.SelectedValue.ToString());
                    CmdDesa.Parameters.AddWithValue("@desa", StringDesa);

                    using (SqlDataReader rdrDesa = CmdDesa.ExecuteReader())
                    {
                        if (rdrDesa.HasRows)
                        {
                            while (rdrDesa.Read())
                            {
                                this.CascadingDropDownDesa.SelectedValue = rdrDesa["id_desa"].ToString();
                            }
                        }
                    }
                    // ---------------------------------

                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TbNama.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA');", true);
                return;
            }
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
            if (this.TbTmpLhir.Text == string.Empty)
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
            //if (this.DLWarga.SelectedValue == "-1")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH STATUS WARGA NEGARA');", true);
            //    return;
            //}
            if (this.DLDarah.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH GOLONGAN DARAH');", true);
                return;
            }
            if (this.DropDownListProv.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PROVINSI');", true);
                return;
            }
            if (this.DropDownListKab.SelectedValue== "")
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
            if (this.TbSekolah.Text == string.Empty || (this.TbSekolah.Text.Length < 6))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI SEKOLAH ASAL SESUAI PETUNJUK');", true);
                return;
            }
            if (this.DLJurusan.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH JURUSAN');", true);
                return;
            }
            if (this.DLStatusSekolah.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH STATUS SEKOLAH');", true);
                return;
            }

            if (this.TBThnLls.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TAHUN LULUS');", true);
                return;
            }
            if (this.TBAdik.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI JUMLAH ADIK');", true);
                return;
            }
            if (this.TbKakak.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI JUMLAH KAKAK');", true);
                return;
            }
            if (this.TbAyah.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA AYAH');", true);
                return;
            }
            if (this.TbIbu.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI NAMA IBU');", true);
                return;
            }
            if (this.DLPendidikanAyah.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PENDIDIKAN AYAH');", true);
                return;
            }
            if (this.DLPendidikanIbu.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PENDIDIKAN IBU');", true);
                return;
            }
            if (this.DLPekerjaanIbu.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PEKERJAAN IBU');", true);
                return;
            }
            if (this.DLPekerjaanAyah.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH PEKERJAAN AYAH');", true);
                return;
            }
            if (this.DLPenghasilan.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KATEGORI PENGHASILAN');", true);
                return;
            }

            Response.Write(this.CascadingDropDownKotakab.SelectedValue);

            // ---------------- INSERT DATA -------------------------
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //SqlTransaction trans = con.BeginTransaction();

                SqlCommand CmdBiodata = new SqlCommand("SpUpdateMhs", con);
                //CmdPeriodik.Transaction = trans;
                CmdBiodata.CommandType = System.Data.CommandType.StoredProcedure;

                CmdBiodata.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                CmdBiodata.Parameters.AddWithValue("@nik", TbNIK.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@gdr",DLGender.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@agm", DLAgama.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@drh", DLDarah.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@prov", DropDownListProv.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kota", DropDownListKab.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kec", DropDownListKec.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@desa", DropDownListDesa.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@alamat", TbAlamat.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@tmplahir", TbTmpLhir.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@ttl", Convert.ToDateTime(TBTtl.Text.Trim()));
                CmdBiodata.Parameters.AddWithValue("@pos", TbKdPOS.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@hp", TBHp.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@email", TbEmail.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@sch", TbSekolah.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@jrs", DLJurusan.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@stsch", DLStatusSekolah.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@thn_lls", TBThnLls.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@kakak", Convert.ToInt32(TbKakak.Text.Trim()));
                CmdBiodata.Parameters.AddWithValue("@adk", Convert.ToInt32(TBAdik.Text.Trim()));
                CmdBiodata.Parameters.AddWithValue("@ibu", TbIbu.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@ayah", TbAyah.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@pnd_ayah", DLPendidikanAyah.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@pnd_ibu", DLPendidikanIbu.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@pkj_ayah", DLPekerjaanAyah.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@pkj_ibu", DLPekerjaanIbu.SelectedItem.Text.Trim());
                CmdBiodata.Parameters.AddWithValue("@pghasil", DLPenghasilan.SelectedItem.Text.Trim());

                CmdBiodata.ExecuteNonQuery();

                Response.Redirect("~/account/Biodata.aspx");
            }
        }
    }
}