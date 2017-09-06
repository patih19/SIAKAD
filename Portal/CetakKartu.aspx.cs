using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    //public partial class WebForm3 : System.Web.UI.Page
    public partial class WebForm3 : Tu
    {
        // Create Mahasiswa Object
        Mhs mhs = new Mhs();

        //------------------- Identifikasi Mahasiswa ------------------ //
        public string NPM
        {
            get { return LbNPM.Text; }
        }
        public string JenisKelas
        {
            get { return this.LbClass.Text; }
        }
        public string Tahun
        {
            get { return this.DLTahun.SelectedItem.Text; }
        }
        public string Semester
        {
            get { return this.DLSemester.SelectedItem.Text; }
        }
        public string Name
        {
            get { return this.LbNama.Text; }
        }
        public string Id_Prodi
        {
            get { return this.LbIdProdi.Text; }
        }
        public string Prodi
        {
            get { return this.LbProdi.Text; }
        }
        public string Angkatan
        {
            get { return this.LbThnAngkatan.Text; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TBNpm_TextChanged(object sender, EventArgs e)
        {
            // ------- Read Mahasiswa ----------
            try
            {
                mhs.ReadMahasiswa(this.TBNpm.Text);

                LbNama.Text = mhs.nama.ToString();
                LbClass.Text = mhs.kelas.ToString();
                LbProdi.Text = mhs.Prodi.ToString();
                LbThnAngkatan.Text = mhs.thn_angkatan.ToString();
                LbNPM.Text = mhs.npm.ToString();
                LbIdProdi.Text = mhs.id_prodi.ToString();
            }
            catch (Exception)
            {
                LbNama.Text = "Nama";
                LbClass.Text = "Jenis Kelas";
                LbProdi.Text = "Program Studi";
                LbThnAngkatan.Text = "Tahun Angkatan";
                LbNPM.Text = "NPM";

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Mahasiswa tidak ditemukan');", true);
                return;
            }
        }

        protected void BtnFilterMhs_Click(object sender, EventArgs e)
        {
            //form validation
            if (this.TBNpm.Text == "")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi NPM');", true);
                return;
            }
            if (this.RbKHS.Checked == false && this.RbKRS.Checked == false)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Jenis Kartu');", true);
                return;
            }
            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }
            if (this.DLSemester.SelectedItem.Text == "Semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            // Kartu Ujian Tengah Semester
            if (this.RbKRS.Checked)
            {
                Server.Transfer("~/am/PrintKRS.aspx");
            }
            else if (this.RbKHS.Checked)
            {
                Server.Transfer("~/am/KHSCard.aspx");
            }

        }
    }
}