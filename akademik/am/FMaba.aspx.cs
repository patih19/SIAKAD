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
    public partial class WebForm34 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PanelMaba.Enabled = false;
                this.PanelMaba.Visible = false;
            }
        }

        protected void BtnJadwal_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["FEEDER"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //----------------------------------------- -------------------------------------------
                con.Open();
                SqlCommand CmdJadwal = new SqlCommand("SpFeederMaba", con);
                CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                CmdJadwal.Parameters.AddWithValue("@thn_angkatan", this.DLTahun.SelectedValue.ToString());

                DataTable TableJadwal = new DataTable();
                TableJadwal.Columns.Add("Nama");
                TableJadwal.Columns.Add("Tempat");
                TableJadwal.Columns.Add("Gender");
                TableJadwal.Columns.Add("TTL");
                TableJadwal.Columns.Add("Agama");
                TableJadwal.Columns.Add("Ibu");
                TableJadwal.Columns.Add("NIK");
                TableJadwal.Columns.Add("Kwn");
                TableJadwal.Columns.Add("Desa");
                TableJadwal.Columns.Add("Id Wilayah");
                //TableJadwal.Columns.Add("Jenis Kelas");
                TableJadwal.Columns.Add("Keb. Ayah");
                TableJadwal.Columns.Add("Keb. Ibu");
                TableJadwal.Columns.Add("KK");
                TableJadwal.Columns.Add("KPS");
                TableJadwal.Columns.Add("Id Prodi");
                TableJadwal.Columns.Add("NPM");
                TableJadwal.Columns.Add("Jenis Pendaftaran");
                TableJadwal.Columns.Add("Tgl. Masuk");
                TableJadwal.Columns.Add("Semester Mulai");

                using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        this.LbJadwalResult.Text = "";

                        //this.PanelJadwal.Enabled = true;
                        //this.PanelJadwal.Visible = true;

                        while (rdr.Read())
                        {
                            DataRow datarow = TableJadwal.NewRow();
                            datarow["Nama"] = rdr["nama"].ToString().ToUpper().Trim();
                            datarow["Tempat"] = rdr["tmp_lahir"].ToString().ToUpper().Trim();

                            string gender = rdr["gender"].ToString().Trim();
                            if (gender == "Laki-laki")
                            {
                                datarow["Gender"] = "L";
                            }
                            else if (gender == "Perempuan")
                            {
                                datarow["Gender"] = "P";
                            }

                            DateTime TglLahir = Convert.ToDateTime(rdr["ttl"]);
                            datarow["TTL"] = TglLahir.ToString("yyyy-MM-dd");

                            string agama = rdr["agama"].ToString().Trim();
                            if (agama == "Islam")
                            {
                                datarow["Agama"] = "1";
                            }
                            else if (agama == "Protestan")
                            {
                                datarow["Agama"] = "2";
                            }
                            else if (agama == "Katholik")
                            {
                                datarow["Agama"] = "3";
                            }
                            else if (agama == "Hindu")
                            {
                                datarow["Agama"] = "4";
                            }
                            else if (agama == "Budha")
                            {
                                datarow["Agama"] = "5";
                            }
                            else if (agama == "Konghucu")
                            {
                                datarow["Agama"] = "6";
                            }

                            datarow["Ibu"] = rdr["ibu"].ToString().ToUpper().Trim();
                            datarow["NIK"] = rdr["nik"].ToString().ToUpper().Trim();
                            datarow["Kwn"] = "ID";
                            datarow["Desa"] = rdr["desa"].ToString().ToUpper().Trim();
                            datarow["Id Wilayah"] = "000000";
                            datarow["Keb. Ayah"] = "0";
                            datarow["Keb. Ibu"] = "0";
                            datarow["KK"] = "0";
                            datarow["KPS"] = "0";

                            string IdProdi = rdr["id_prog_study"].ToString().Trim();
                            datarow["Id Prodi"] = IdProdi.Replace("-", "");

                            datarow["NPM"] = rdr["npm"];
                            datarow["Jenis Pendaftaran"] = "1";

                            if (rdr["tgl_masuk"] == DBNull.Value)
                            {
                                datarow["Tgl. Masuk"] = "";
                            }
                            else
                            {
                                DateTime TglMasuk = Convert.ToDateTime(rdr["tgl_masuk"]);
                                datarow["Tgl. Masuk"] = TglMasuk.ToString("yyyy-MM-dd");
                            }

                            datarow["Semester Mulai"] = rdr["smster_mulai"];
                            TableJadwal.Rows.Add(datarow);
                        }

                        //Fill Gridview
                        this.GVMaba.DataSource = TableJadwal;
                        this.GVMaba.DataBind();

                        // Show panel Maba
                        this.PanelMaba.Enabled = true;
                        this.PanelMaba.Visible = true;
                    }
                    else
                    {
                        this.LbJadwalResult.Text = "Data Tidak Ditemukan";
                        this.LbJadwalResult.ForeColor = System.Drawing.Color.Blue;

                        // hide panel Maba
                        this.PanelMaba.Enabled = false;
                        this.PanelMaba.Visible = false;

                        //clear Gridview
                        TableJadwal.Rows.Clear();
                        TableJadwal.Clear();
                        GVMaba.DataSource = TableJadwal;
                        GVMaba.DataBind();
                    }
                }
            }
        }

        protected void GVMaba_PreRender(object sender, EventArgs e)
        {
            if (GVMaba.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVMaba.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVMaba.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
}