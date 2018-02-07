using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Padu.account
{
    //public partial class WebForm6 : System.Web.UI.Page
    public partial class WebForm6 : Mhs_account
    {
        //instance object mahasiswa 
        Mhs mhs = new Mhs();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMhs.Visible = false;

                // 1. ------- Read Mahasiswa ----------
                try
                {
                    mhs.ReadMahasiswa(this.Session["Name"].ToString());

                    LbNama.Text = mhs.nama.ToString();
                    LbKelas.Text = mhs.kelas.ToString();
                    LbProdi.Text = mhs.Prodi.ToString();
                    LbNpm.Text = mhs.npm.ToString();
                }
                catch (Exception)
                {
                    LbNama.Text = "";
                    LbKelas.Text = "";
                    LbProdi.Text = "";
                    LbNpm.Text = "";

                    //clear Gridview
                    DataTable TableKP = new DataTable();
                    TableKP.Rows.Clear();
                    TableKP.Clear();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                    return;
                }


                // 2. -----------------------------------Keterangan Biodata -----------------------------------------------
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
                                this.LbName.Text = rdr["nama"].ToString();
                                this.LbNIK.Text = rdr["nik"].ToString();
                                this.LbTmpLahir.Text = rdr["tmp_lahir"].ToString();
                                if (rdr["ttl"] == DBNull.Value)
                                {

                                }
                                else
                                {
                                    DateTime TglUjian = Convert.ToDateTime(rdr["ttl"]);
                                    this.LbTglLhair.Text = TglUjian.ToString("dd-MM-yyyy");
                                }

                                this.LbGender.Text = rdr["gender"].ToString();
                                this.LbAgama.Text = rdr["agama"].ToString();
                                this.LbDarah.Text = rdr["darah"].ToString();
                                this.LbAlamat.Text = rdr["alamat"].ToString();
                                this.LbProv.Text = rdr["prov"].ToString();
                                this.LbKotaKab.Text = rdr["kota"].ToString();
                                this.LbKec.Text = rdr["kec"].ToString();
                                this.LbDesa.Text = rdr["desa"].ToString();
                                this.LbKdPOS.Text = rdr["kd_pos"].ToString();
                                this.LbHp.Text = rdr["hp"].ToString();
                                this.LbEmail.Text = rdr["email"].ToString();

                                this.LbSekolah.Text = rdr["sekolah"].ToString();
                                this.LbJurusan.Text = rdr["jurusan"].ToString();
                                this.LbStatus.Text = rdr["status_sekolah"].ToString();
                                this.LbThnLulus.Text = rdr["thn_lls"].ToString();

                                this.LbAdik.Text = rdr["adik"].ToString();
                                this.LbKakak.Text = rdr["kakak"].ToString();
                                this.LbNamaIbu.Text = rdr["ibu"].ToString();
                                this.LbNamaAyah.Text = rdr["ayah"].ToString();
                                this.LbPendidikanAyah.Text = rdr["pendidikan_ayah"].ToString();
                                this.LbPendidikanIbu.Text = rdr["pendidikan_ibu"].ToString();
                                this.LbPekerjaanAyah.Text = rdr["pekerjaan_ayah"].ToString();
                                this.LbPekerjaanIbu.Text = rdr["pekerjaan_ibu"].ToString();
                                this.LbPenghasilan.Text = rdr["penghasilan_ortu"].ToString();
                            }
                        }
                    }
                }

                LihatFoto(this.Session["Name"].ToString());
            }
        }

        protected void LihatFoto(string npm)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    //========================== READER IMAGE FROM DB =========================
                    SqlCommand CmdDisplay = new SqlCommand("SELECT npm, foto FROM bak_mahasiswa WHERE (npm=@npm)", connection);
                    CmdDisplay.Parameters.AddWithValue("@npm", npm); // this.Session["Name"].ToString());
                    SqlDataReader reader = CmdDisplay.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Byte[] bytes = (Byte[])reader["foto"];
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            Image1.ImageUrl = "data:image/png;base64," + base64String;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Image found.");
                    }
                    reader.Close();
                    //======================== END READER =========================
                }
                catch (Exception ex)
                {
                    Image1.ImageUrl = null;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/account/UpdateBiodata.aspx");
        }
    }
}