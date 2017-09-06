using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Hosting;
using System.Diagnostics;
using System.IO;

namespace akademik.am
{
    //public partial class WebForm11 : System.Web.UI.Page
    public partial class WebForm11 : Bak_staff
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
            if (this.TBNpm.Text == string.Empty)
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
                Server.Transfer("~/am/PrintKRS.aspx",true);   
                //DoDownload("KRS-" + TBNpm.Text + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);
            }
            else if (this.RbKHS.Checked)
            {
                Server.Transfer("~/am/KHSCard.aspx");
            }

        }

        private void DoDownload(string FileName)
        {
            //var url = "http://localhost:2281/am/PrintKRS3.aspx?nim="+LbNPM.Text+"&semester="+Tahun+Semester+"";
            var url = Request.Url.AbsoluteUri;

            int IndMiring = url.LastIndexOf('/');
            var NewUrl = url.Substring(0,IndMiring+1)+"PrintKRS3.aspx?nim="+LbNPM.Text+"&semester="+Tahun+Semester+"";

            //return;
            var file = WKHtmlToPdf(NewUrl);
            if (file != null)
            {
                Response.ContentType = "Application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename="+FileName+".pdf");
                Response.BinaryWrite(file);
                Response.End();
            }
        }

        public byte[] WKHtmlToPdf(string url)
        {
            var fileName = " - ";
            var wkhtmlDir = "C:\\Program Files\\wkhtmltopdf\\";
            var wkhtml = "C:\\Program Files\\wkhtmltopdf\\bin\\wkhtmltopdf.exe";
            var p = new Process();

            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = wkhtml;
            p.StartInfo.WorkingDirectory = wkhtmlDir;

            string switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
            switches += "--page-size A4 ";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();


            //read output
            byte[] buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }

            // wait or exit
            p.WaitForExit(60000);

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();

            return returnCode == 0 ? file : null;
        }
    }
}