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
    public partial class WebForm26 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelRekap.Enabled = false;
                this.PanelRekap.Visible = false;
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TbThnAngkatan.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi Tahun Angkatan');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun" || this.DLTahun.SelectedItem.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            this.PanelRekap.Enabled = true;
            this.PanelRekap.Visible = true;

            // ----------------- 
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                //1. --------------------- Ekonomi Reguler -----------------------
                SqlCommand CekEkoReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekEkoReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekEkoReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekEkoReg.Parameters.AddWithValue("@Prodi", "S1 EKONOMI PEMBANGUNAN");
                CekEkoReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekEkoReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter EkoRegTotal = new SqlParameter();
                EkoRegTotal.ParameterName = "@Total";
                EkoRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                EkoRegTotal.Size = 20;
                EkoRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekEkoReg.Parameters.Add(EkoRegTotal);

                CekEkoReg.ExecuteNonQuery();

                this.LbEkoReg.Text = EkoRegTotal.Value.ToString();


                // 2. --------------------- Ekonomi Non Reguler -----------------------
                SqlCommand CekEkoNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekEkoNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekEkoNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekEkoNonReg.Parameters.AddWithValue("@Prodi", "S1 EKONOMI PEMBANGUNAN");
                CekEkoNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekEkoNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);


                SqlParameter EkoNonRegTotal = new SqlParameter();
                EkoNonRegTotal.ParameterName = "@Total";
                EkoNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                EkoNonRegTotal.Size = 20;
                EkoNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekEkoNonReg.Parameters.Add(EkoNonRegTotal);

                CekEkoNonReg.ExecuteNonQuery();

                this.LbEkoNonRegTotal.Text = EkoNonRegTotal.Value.ToString();


                // 3. --------------------- Akuntansi Reguler -----------------------
                SqlCommand CekaAkunReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekaAkunReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekaAkunReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekaAkunReg.Parameters.AddWithValue("@Prodi", "D3 AKUNTANSI");
                CekaAkunReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekaAkunReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter AkunRegTotal = new SqlParameter();
                AkunRegTotal.ParameterName = "@Total";
                AkunRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                AkunRegTotal.Size = 20;
                AkunRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekaAkunReg.Parameters.Add(AkunRegTotal);

                CekaAkunReg.ExecuteNonQuery();

                this.LbAkunRegTotal.Text = AkunRegTotal.Value.ToString();

                //// 4. --------------------- Akuntansi Non Reguler -----------------------
                //SqlCommand CekAkunNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                //CekAkunNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                //CekAkunNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                //CekAkunNonReg.Parameters.AddWithValue("@Prodi", "D3 AKUNTANSI");
                //CekAkunNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                //CekAkunNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                //SqlParameter AkunNonRegTotal = new SqlParameter();
                //AkunNonRegTotal.ParameterName = "@Total";
                //AkunNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                //AkunNonRegTotal.Size = 20;
                //AkunNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                //CekAkunNonReg.Parameters.Add(AkunNonRegTotal);

                //CekAkunNonReg.ExecuteNonQuery();

                //this.LbAAkunNonRegTotal.Text = AkunNonRegTotal.Value.ToString();


                // 5. --------------------- SOSPOL Reguler -----------------------
                SqlCommand CekSospolReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekSospolReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekSospolReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekSospolReg.Parameters.AddWithValue("@Prodi", "S1 ILMU ADMINISTRASI NEGARA");
                CekSospolReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekSospolReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter SospolRegTotal = new SqlParameter();
                SospolRegTotal.ParameterName = "@Total";
                SospolRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                SospolRegTotal.Size = 20;
                SospolRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekSospolReg.Parameters.Add(SospolRegTotal);

                CekSospolReg.ExecuteNonQuery();

                this.LbSospolRegTotal.Text = SospolRegTotal.Value.ToString();

                // 6. --------------------- SOSPOL Non Reguler -----------------------
                SqlCommand CekSospolNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekSospolNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekSospolNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekSospolNonReg.Parameters.AddWithValue("@Prodi", "S1 ILMU ADMINISTRASI NEGARA");
                CekSospolNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekSospolNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter SospolNonRegTotal = new SqlParameter();
                SospolNonRegTotal.ParameterName = "@Total";
                SospolNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                SospolNonRegTotal.Size = 20;
                SospolNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekSospolNonReg.Parameters.Add(SospolNonRegTotal);

                CekSospolNonReg.ExecuteNonQuery();

                this.LbSospolNonRegTotal.Text = SospolNonRegTotal.Value.ToString();

                // 7. --------------------- INGGRIS Reguler -----------------------
                SqlCommand CekInggrisReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekInggrisReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekInggrisReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekInggrisReg.Parameters.AddWithValue("@Prodi", "S1 PENDIDIKAN BAHASA INGGRIS");
                CekInggrisReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekInggrisReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter InggrisRegTotal = new SqlParameter();
                InggrisRegTotal.ParameterName = "@Total";
                InggrisRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                InggrisRegTotal.Size = 20;
                InggrisRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekInggrisReg.Parameters.Add(InggrisRegTotal);

                CekInggrisReg.ExecuteNonQuery();

                this.LbInggrisRegTotal.Text = InggrisRegTotal.Value.ToString();

                // 8. --------------------- INGGRIS Non Reguler -----------------------
                SqlCommand CekInggrisNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekInggrisNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekInggrisNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekInggrisNonReg.Parameters.AddWithValue("@Prodi", "S1 PENDIDIKAN BAHASA INGGRIS");
                CekInggrisNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekInggrisNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter InggrisNonRegTotal = new SqlParameter();
                InggrisNonRegTotal.ParameterName = "@Total";
                InggrisNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                InggrisNonRegTotal.Size = 20;
                InggrisNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekInggrisNonReg.Parameters.Add(InggrisNonRegTotal);

                CekInggrisNonReg.ExecuteNonQuery();

                this.LbInggrisNonRegTotal.Text = InggrisNonRegTotal.Value.ToString();

                // 9. --------------------- INDONESIA Reguler -----------------------
                SqlCommand CekIndonesiaReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekIndonesiaReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekIndonesiaReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekIndonesiaReg.Parameters.AddWithValue("@Prodi", "S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA");
                CekIndonesiaReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekIndonesiaReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter IndonesiaRegTotal = new SqlParameter();
                IndonesiaRegTotal.ParameterName = "@Total";
                IndonesiaRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                IndonesiaRegTotal.Size = 20;
                IndonesiaRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekIndonesiaReg.Parameters.Add(IndonesiaRegTotal);

                CekIndonesiaReg.ExecuteNonQuery();

                this.LbIndonesiaRegTotal.Text = IndonesiaRegTotal.Value.ToString();

                // 10. --------------------- INDONESIA Non Reguler -----------------------
                SqlCommand CekIndonesiaNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekIndonesiaNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekIndonesiaNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekIndonesiaNonReg.Parameters.AddWithValue("@Prodi", "S1 PENDIDIKAN BAHASA DAN SASTRA INDONESIA");
                CekIndonesiaNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekIndonesiaNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter IndonesiaNonRegTotal = new SqlParameter();
                IndonesiaNonRegTotal.ParameterName = "@Total";
                IndonesiaNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                IndonesiaNonRegTotal.Size = 20;
                IndonesiaNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekIndonesiaNonReg.Parameters.Add(IndonesiaNonRegTotal);

                CekIndonesiaNonReg.ExecuteNonQuery();

                this.LbIndonesiaNonRegTotal.Text = IndonesiaNonRegTotal.Value.ToString();

                // 11. --------------------- AGRO Reguler -----------------------
                SqlCommand CekAgroReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekAgroReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekAgroReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekAgroReg.Parameters.AddWithValue("@Prodi", "S1 AGROTEKNOLOGI");
                CekAgroReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekAgroReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter AgroRegTotal = new SqlParameter();
                AgroRegTotal.ParameterName = "@Total";
                AgroRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                AgroRegTotal.Size = 20;
                AgroRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekAgroReg.Parameters.Add(AgroRegTotal);

                CekAgroReg.ExecuteNonQuery();

                this.LbAgroRegTotal.Text = AgroRegTotal.Value.ToString();

                // 12. --------------------- AGRO Non Reguler -----------------------
                SqlCommand CekAgroNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekAgroNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekAgroNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekAgroNonReg.Parameters.AddWithValue("@Prodi", "S1 AGROTEKNOLOGI");
                CekAgroNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekAgroNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter AgroNonRegTotal = new SqlParameter();
                AgroNonRegTotal.ParameterName = "@Total";
                AgroNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                AgroNonRegTotal.Size = 20;
                AgroNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekAgroNonReg.Parameters.Add(AgroNonRegTotal);

                CekAgroNonReg.ExecuteNonQuery();

                this.LbAgroNonRegTotal.Text = AgroNonRegTotal.Value.ToString();

                // 13. --------------------- ELEKTRO Reguler -----------------------
                SqlCommand CekElektroReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekElektroReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekElektroReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekElektroReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK ELEKTRO");
                CekElektroReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekElektroReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter ElektroRegTotal = new SqlParameter();
                ElektroRegTotal.ParameterName = "@Total";
                ElektroRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                ElektroRegTotal.Size = 20;
                ElektroRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekElektroReg.Parameters.Add(ElektroRegTotal);

                CekElektroReg.ExecuteNonQuery();

                this.LbElektroRegTotal.Text = ElektroRegTotal.Value.ToString();

                // 14. --------------------- ELEKTRO Non Reguler -----------------------
                SqlCommand CekElektroNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekElektroNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekElektroNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                CekElektroNonReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK ELEKTRO");
                CekElektroNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekElektroNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter ElektroNonRegTotal = new SqlParameter();
                ElektroNonRegTotal.ParameterName = "@Total";
                ElektroNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                ElektroNonRegTotal.Size = 20;
                ElektroNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekElektroNonReg.Parameters.Add(ElektroNonRegTotal);

                CekElektroNonReg.ExecuteNonQuery();

                this.LbElektroNonRegTotal.Text = ElektroNonRegTotal.Value.ToString();

                // 15. --------------------- MESIN Reguler -----------------------
                SqlCommand CekMesinReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekMesinReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekMesinReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekMesinReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK MESIN");
                CekMesinReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekMesinReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter MesinRegTotal = new SqlParameter();
                MesinRegTotal.ParameterName = "@Total";
                MesinRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                MesinRegTotal.Size = 20;
                MesinRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekMesinReg.Parameters.Add(MesinRegTotal);

                CekMesinReg.ExecuteNonQuery();

                this.LbMesinRegTotal.Text = MesinRegTotal.Value.ToString();

                // 16. --------------------- MESIN Non Reguler -----------------------
                SqlCommand MesinNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                MesinNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                MesinNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                MesinNonReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK MESIN");
                MesinNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                MesinNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter MesinNonRegTotal = new SqlParameter();
                MesinNonRegTotal.ParameterName = "@Total";
                MesinNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                MesinNonRegTotal.Size = 20;
                MesinNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                MesinNonReg.Parameters.Add(MesinNonRegTotal);

                MesinNonReg.ExecuteNonQuery();

                this.LbMesinNonRegTotal.Text = MesinNonRegTotal.Value.ToString();

                // 17. --------------------- SIPIL Reguler -----------------------
                SqlCommand CekSipilReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekSipilReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekSipilReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekSipilReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK SIPIL");
                CekSipilReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekSipilReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter SipilRegTotal = new SqlParameter();
                SipilRegTotal.ParameterName = "@Total";
                SipilRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                SipilRegTotal.Size = 20;
                SipilRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekSipilReg.Parameters.Add(SipilRegTotal);

                CekSipilReg.ExecuteNonQuery();

                this.LbSipilRegTotal.Text = SipilRegTotal.Value.ToString();

                // 18. --------------------- SIPIL Non Reguler -----------------------
                SqlCommand SipilNonReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                SipilNonReg.CommandType = System.Data.CommandType.StoredProcedure;

                SipilNonReg.Parameters.AddWithValue("@Kelas", "NON REGULER");
                SipilNonReg.Parameters.AddWithValue("@Prodi", "S1 TEKNIK SIPIL");
                SipilNonReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                SipilNonReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter SipilNonRegTotal = new SqlParameter();
                SipilNonRegTotal.ParameterName = "@Total";
                SipilNonRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                SipilNonRegTotal.Size = 20;
                SipilNonRegTotal.Direction = System.Data.ParameterDirection.Output;
                SipilNonReg.Parameters.Add(SipilNonRegTotal);

                SipilNonReg.ExecuteNonQuery();

                this.LbSipilNonRegTotal.Text = SipilNonRegTotal.Value.ToString();

                // 19. --------------------- OTOMOTIF Reguler -----------------------
                SqlCommand CekOtoReg = new SqlCommand("SpRekapMhsAktifSmstr", con);
                CekOtoReg.CommandType = System.Data.CommandType.StoredProcedure;

                CekOtoReg.Parameters.AddWithValue("@Kelas", "REGULER");
                CekOtoReg.Parameters.AddWithValue("@Prodi", "D3 TEKNIK MESIN");
                CekOtoReg.Parameters.AddWithValue("@ThnAngkatan", TbThnAngkatan.Text);
                CekOtoReg.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.SelectedItem.Text);

                SqlParameter OtoRegTotal = new SqlParameter();
                OtoRegTotal.ParameterName = "@Total";
                OtoRegTotal.SqlDbType = System.Data.SqlDbType.Int;
                OtoRegTotal.Size = 20;
                OtoRegTotal.Direction = System.Data.ParameterDirection.Output;
                CekOtoReg.Parameters.Add(OtoRegTotal);

                CekOtoReg.ExecuteNonQuery();

                this.LbOtoRegTotal.Text = OtoRegTotal.Value.ToString();
            }
        }
    }
}