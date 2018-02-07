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
    //public partial class WebForm3 : System.Web.UI.Page
    public partial class WebForm3 : Mhs_account
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            //--------------- Filter Ukuran Foto --------------------
            if (!FileUploadFoto.HasFile)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Foto Sebelum Upload');", true);
                return;
            }

            if (FileUploadFoto.PostedFile.ContentLength >= 102400)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "File Melebihi 100 KB";

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Ukuran File Melebihi Batas Ketentuan');", true);
                return;
            }

            // Read the file and convert it to Byte Array
            string filePath = FileUploadFoto.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".jpeg":
                    contenttype = "image/jpeg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
            }

            if (contenttype != String.Empty)
            {
                Stream fs = FileUploadFoto.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                int length = bytes.Length;

                //insert the file into database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("SpInsertFoto", connection);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        cmd.Parameters.AddWithValue("@foto", bytes);
                        cmd.ExecuteNonQuery();

                        //------------- InsertUpdateData(cmd) -----------;
                        cmd.Dispose();
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "File Uploaded Successfully";

                        ////========================== READER IMAGE FROM DB =========================
                        //SqlCommand CmdDisplay = new SqlCommand("select image from tes_upload where id=7;", connection);
                        //SqlDataReader reader = CmdDisplay.ExecuteReader();
                        //if (reader.HasRows)
                        //{
                        //    while (reader.Read())
                        //    {
                        //        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        //        Image1.ImageUrl = "data:image/png;base64," + base64String;
                        //    }
                        //}
                        //else
                        //{
                        //    Console.WriteLine("No Image found.");
                        //}
                        //reader.Close();
                        ////======================== END READER =========================

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ImageFoto.ImageUrl = "data:image/png;base64," + base64String;
                        BtnUpload.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                        return;
                    }
                }
            }
            else // type file tidak diperbolehkan
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('File Tidak Diperbolehkan');", true);
                return;
            }
        }
    }
}