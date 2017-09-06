using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Padu.account
{
    public partial class WebForm9 : Mhs_account
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
            this.Session.Remove("Name");
            this.Session.Remove("Passwd");
            this.Session.RemoveAll();
            this.Response.Redirect("~/Padu_login.aspx");
        }
        // -------------- End Logout ----------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelJadwal.Enabled = false;
                this.PanelJadwal.Visible = false;
            }
        }

        protected void BtnDownoad_Click(object sender, EventArgs e)
        {
            if (this.RbUTS.Checked)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    try
                    {
                        con.Open();
                        // -------------------- Cek Update Biodata ---------------------
                        SqlCommand CmdCekBiodata = new SqlCommand("select npm,perbarui from bak_mahasiswa where npm=@npm", con);
                        //CmdCekBiodata.Transaction = trans;
                        CmdCekBiodata.CommandType = System.Data.CommandType.Text;
                        CmdCekBiodata.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        using (SqlDataReader RdrBiodata = CmdCekBiodata.ExecuteReader())
                        {
                            if (RdrBiodata.HasRows)
                            {
                                while (RdrBiodata.Read())
                                {
                                    if (RdrBiodata["perbarui"].ToString() != "Y" )
                                    {
                                        Response.Redirect("~/account/UpdateBiodata.aspx");   
                                    }                      
                                }

                            }
                        }                        

                        //---------------------- Validasi Download ---------------------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUTS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdCekMasa.ExecuteNonQuery();

                        // --------------------- Enable Panel ------------------------
                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        // --------------------- Fill Gridview UTS Makul ------------------------
                        SqlCommand CmdListUTS = new SqlCommand("SpListUTS", con);
                        CmdListUTS.CommandType = System.Data.CommandType.StoredProcedure;


                        CmdListUTS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdListUTS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue + this.DLSemester.SelectedValue);

                        DataTable TableUTSMakul = new DataTable();
                        TableUTSMakul.Columns.Add("Kode");
                        TableUTSMakul.Columns.Add("Mata kuliah");
                        TableUTSMakul.Columns.Add("SKS");
                        TableUTSMakul.Columns.Add("Tanggal Ujian");
                        //TableUTSMakul.Columns.Add("Tanda Tangan");

                        using (SqlDataReader rdr = CmdListUTS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow = TableUTSMakul.NewRow();
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    //--- format tanggal ---
                                    if (rdr["tgl_uts"] == DBNull.Value)
                                    {
                                    }
                                    else
                                    {
                                        DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uts"]);
                                        datarow["Tanggal Ujian"] = TglUjian.ToString("dd-MMMM-yyyy");
                                    }
                                    TableUTSMakul.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVJadwal.DataSource = TableUTSMakul;
                                this.GVJadwal.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUTSMakul.Rows.Clear();
                                TableUTSMakul.Clear();
                                GVJadwal.DataSource = TableUTSMakul;
                                GVJadwal.DataBind();
                            }
                        }

                        //------------------------- GV UTS JADWAL ---------------------
                        SqlCommand CmdUTSJadwal = new SqlCommand("SpListUTS", con);
                        CmdUTSJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUTSJadwal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdUTSJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue + this.DLSemester.SelectedValue);

                        DataTable TableUTSJadwal = new DataTable();
                        TableUTSJadwal.Columns.Add("Hari");
                        TableUTSJadwal.Columns.Add("Mata Kuliah");
                        TableUTSJadwal.Columns.Add("Dosen");
                        TableUTSJadwal.Columns.Add("Mulai");
                        TableUTSJadwal.Columns.Add("Selesai");
                        TableUTSJadwal.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdUTSJadwal.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow2 = TableUTSJadwal.NewRow();
                                    datarow2["Hari"] = rdr["hari_uts"];
                                    datarow2["Mata Kuliah"] = rdr["makul"];
                                    datarow2["Mulai"] = rdr["jam_mulai_uts"];
                                    datarow2["Selesai"] = rdr["jam_sls_uts"];
                                    datarow2["Ruang"] = rdr["ruang_uts"];
                                    datarow2["Dosen"] = rdr["nama"];

                                    TableUTSJadwal.Rows.Add(datarow2);
                                }

                                //Fill Gridview
                                this.GVJadwalDetail.DataSource = TableUTSJadwal;
                                this.GVJadwalDetail.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUTSJadwal.Rows.Clear();
                                TableUTSJadwal.Clear();
                                GVJadwalDetail.DataSource = TableUTSJadwal;
                                GVJadwalDetail.DataBind();
                            }
                        }




                        //SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUTS", con);
                        ////CmdCekMasa.Transaction = trans;
                        //CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        //CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        //CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        //CmdCekMasa.ExecuteNonQuery();

                        ////Server.Transfer("~/am/KartuUTS.aspx", true);   
                        //DoDownloadKartuUTS("Kartu_UTS-" + this.Session["Name"].ToString() + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);
                    }
                    catch (Exception ex)
                    {

                        this.PanelJadwal.Enabled = false;
                        this.PanelJadwal.Visible = false;

                        this.DLSemester.SelectedIndex = 0;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                        //string message = "alert('" + ex.Message.ToString() + "')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }
            }
            else if (this.RbUAS.Checked)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    try
                    {
                        con.Open();

                        // -------------------- Cek Update Biodata ---------------------
                        SqlCommand CmdCekBiodata = new SqlCommand("select npm,perbarui from bak_mahasiswa where npm=@npm", con);
                        //CmdCekBiodata.Transaction = trans;
                        CmdCekBiodata.CommandType = System.Data.CommandType.Text;
                        CmdCekBiodata.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        using (SqlDataReader RdrBiodata = CmdCekBiodata.ExecuteReader())
                        {
                            if (RdrBiodata.HasRows)
                            {
                                while (RdrBiodata.Read())
                                {
                                    if (RdrBiodata["perbarui"].ToString() != "Y")
                                    {
                                        Response.Redirect("~/account/UpdateBiodata.aspx");
                                    }
                                }
                            }
                        }

                        // ------------------------ Validasi Download -------------------------
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUAS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdCekMasa.ExecuteNonQuery();

                        // --------------------- Enable Panel ------------------------
                        this.PanelJadwal.Enabled = true;
                        this.PanelJadwal.Visible = true;

                        // --------------------- Fill Gridview UAS Makul ------------------------
                        SqlCommand CmdListUAS = new SqlCommand("SpListUAS", con);
                        CmdListUAS.CommandType = System.Data.CommandType.StoredProcedure;


                        CmdListUAS.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdListUAS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue + this.DLSemester.SelectedValue);

                        DataTable TableUASMakul = new DataTable();
                        TableUASMakul.Columns.Add("Kode");
                        TableUASMakul.Columns.Add("Mata kuliah");
                        TableUASMakul.Columns.Add("SKS");
                        TableUASMakul.Columns.Add("Tanggal Ujian");
                        //TableUASMakul.Columns.Add("Tanda Tangan");

                        using (SqlDataReader rdr = CmdListUAS.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow = TableUASMakul.NewRow();
                                    datarow["Kode"] = rdr["kode_makul"];
                                    datarow["Mata Kuliah"] = rdr["makul"];
                                    datarow["SKS"] = rdr["sks"];
                                    //--- format tanggal ---
                                    if (rdr["tgl_uas"] == DBNull.Value)
                                    {
                                    }
                                    else
                                    {
                                        DateTime TglUjian = Convert.ToDateTime(rdr["tgl_uas"]);
                                        datarow["Tanggal Ujian"] = TglUjian.ToString("dd-MMMM-yyyy");
                                    }

                                    TableUASMakul.Rows.Add(datarow);
                                }

                                //Fill Gridview
                                this.GVJadwal.DataSource = TableUASMakul;
                                this.GVJadwal.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUASMakul.Rows.Clear();
                                TableUASMakul.Clear();
                                GVJadwal.DataSource = TableUASMakul;
                                GVJadwal.DataBind();
                            }
                        }

                        //------------------------- GV UAS JADWAL ---------------------
                        SqlCommand CmdUASJadwal = new SqlCommand("SpListUAS", con);
                        CmdUASJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdUASJadwal.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());
                        CmdUASJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue + this.DLSemester.SelectedValue);

                        DataTable TableUASJadwal = new DataTable();
                        TableUASJadwal.Columns.Add("Hari");
                        TableUASJadwal.Columns.Add("Mata Kuliah");
                        TableUASJadwal.Columns.Add("Dosen");
                        TableUASJadwal.Columns.Add("Mulai");
                        TableUASJadwal.Columns.Add("Selesai");
                        TableUASJadwal.Columns.Add("Ruang");

                        using (SqlDataReader rdr = CmdUASJadwal.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    DataRow datarow2 = TableUASJadwal.NewRow();
                                    datarow2["Hari"] = rdr["hari_uas"];
                                    datarow2["Mata Kuliah"] = rdr["makul"];
                                    datarow2["Mulai"] = rdr["jam_mulai_uas"];
                                    datarow2["Selesai"] = rdr["jam_sls_uas"];
                                    datarow2["Ruang"] = rdr["ruang_uas"];
                                    datarow2["Dosen"] = rdr["nama"];

                                    TableUASJadwal.Rows.Add(datarow2);
                                }

                                //Fill Gridview
                                this.GVJadwalDetail.DataSource = TableUASJadwal;
                                this.GVJadwalDetail.DataBind();
                            }
                            else
                            {
                                //clear Gridview
                                TableUASJadwal.Rows.Clear();
                                TableUASJadwal.Clear();
                                GVJadwalDetail.DataSource = TableUASJadwal;
                                GVJadwalDetail.DataBind();
                            }
                        }


                        // SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUAS", con);
                        // //CmdCekMasa.Transaction = trans;
                        // CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        // CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        // CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        // CmdCekMasa.ExecuteNonQuery();

                        //// Server.Transfer("~/am/KartuUAS.aspx", true);  
                        // DoDownloadKartuUAS("Kartu_UAS-" + this.Session["Name"].ToString() + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);
                    }
                    catch (Exception ex)
                    {
                        this.PanelJadwal.Enabled = false;
                        this.PanelJadwal.Visible = false;

                        this.DLSemester.SelectedIndex = 0;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);

                        //string message = "alert('" + ex.Message.ToString() + "')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }
            }
        }

        private void DoDownloadKartuUTS(string FileName)
        {
            //var url = "http://localhost:2281/am/PrintKRS3.aspx?nim="+LbNPM.Text+"&semester="+Tahun+Semester+"";
            var url = Request.Url.AbsoluteUri;

            int IndMiring = url.LastIndexOf('/');
            var NewUrl = url.Substring(0, IndMiring + 1) + "KartuUTS.aspx?nim=" + this.Session["Name"].ToString() + "&semester=" + this.DLTahun.SelectedItem.Text + this.DLSemester.Text + "";

            //return;
            var file = WKHtmlToPdf(NewUrl);
            if (file != null)
            {
                Response.ContentType = "Application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");
                Response.BinaryWrite(file);
                Response.End();
            }
        }

        private void DoDownloadKartuUAS(string FileName)
        {
            //var url = "http://localhost:2281/am/PrintKRS3.aspx?nim="+LbNPM.Text+"&semester="+Tahun+Semester+"";
            var url = Request.Url.AbsoluteUri;

            int IndMiring = url.LastIndexOf('/');
            var NewUrl = url.Substring(0, IndMiring + 1) + "KartuUAS.aspx?nim=" + this.Session["Name"].ToString() + "&semester=" + this.DLTahun.SelectedItem.Text + this.DLSemester.Text + "";

            //return;
            var file = WKHtmlToPdf(NewUrl);
            if (file != null)
            {
                Response.ContentType = "Application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");
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
            switches += "--margin-top 10mm --margin-bottom 0mm --margin-right 15mm --margin-left 15mm ";
            switches += "--page-size A4 ";
            switches += "--disable-smart-shrinking";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.RbUTS.Checked)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    try
                    {
                        con.Open();
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUTS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdCekMasa.ExecuteNonQuery();

                        //Server.Transfer("~/am/KartuUTS.aspx", true);   
                        DoDownloadKartuUTS("Kartu_UTS-" + this.Session["Name"].ToString() + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);
                    }
                    catch (Exception ex)
                    {
                        this.DLSemester.SelectedIndex = 0;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
            else if (this.RbUAS.Checked)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    try
                    {
                        con.Open();
                        SqlCommand CmdCekMasa = new SqlCommand("SpCekDownloadUAS", con);
                        //CmdCekMasa.Transaction = trans;
                        CmdCekMasa.CommandType = System.Data.CommandType.StoredProcedure;

                        CmdCekMasa.Parameters.AddWithValue("@semester", this.DLTahun.SelectedItem.Text + this.DLSemester.Text);
                        CmdCekMasa.Parameters.AddWithValue("@npm", this.Session["Name"].ToString());

                        CmdCekMasa.ExecuteNonQuery();

                        // Server.Transfer("~/am/KartuUAS.aspx", true);  
                        DoDownloadKartuUAS("Kartu_UAS-" + this.Session["Name"].ToString() + "-" + DLTahun.SelectedItem.Text + DLSemester.SelectedItem.Text);

                    }
                    catch (Exception ex)
                    {
                        this.DLSemester.SelectedIndex = 0;
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    }
                }
            }
        }
    }
}