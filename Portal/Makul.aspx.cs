using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Portal
{
    public partial class WebForm16 : Tu
    {
        public string _KodeMakul
        {
            get {return this.ViewState["Kodemakul"].ToString();}
            set {this.ViewState["Kodemakul"] = (object)value; }
        }

        public string _KodeMakul2
        {
            get {return this.ViewState["Kodemakul"].ToString();}
            set {this.ViewState["Kodemakul"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load_Makul();
            }
        }

        protected void Load_Makul()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand CmdMakul = new SqlCommand("SpGetMakulByProdi", con);
                    CmdMakul.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdMakul.Parameters.AddWithValue("@idprodi", this.Session["level"].ToString());

                    DataTable TableMakul = new DataTable();
                    //TableMakul.Columns.Add("No");
                    TableMakul.Columns.Add("Kode");
                    TableMakul.Columns.Add("Mata Kuliah");
                    //TableMakul.Columns.Add("SKS Tatap Muka");
                    //TableMakul.Columns.Add("SKS Praktikum");
                    //TableMakul.Columns.Add("SKS Praktikum Lap");
                    TableMakul.Columns.Add("Total SKS");
                    TableMakul.Columns.Add("Semester");
                    TableMakul.Columns.Add("KURIKULUM");
                    //TableMakul.Columns.Add("Jenis");

                    using (SqlDataReader rdr = CmdMakul.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableMakul.NewRow();
                               // datarow["No"] = rdr["nomor"].ToString();
                                datarow["Kode"] = rdr["kode_makul"].ToString();
                                datarow["Mata Kuliah"] = rdr["makul"].ToString();

                                //if (rdr["tmuka"] != DBNull.Value)
                                //{
                                //    datarow["SKS Tatap Muka"] = rdr["tmuka"].ToString();
                                //}

                                //if (rdr["prak"] != DBNull.Value)
                                //{
                                //    datarow["SKS Praktikum"] = rdr["prak"].ToString();
                                //}

                                //if (rdr["lap"] != DBNull.Value)
                                //{
                                //    datarow["SKS Praktikum Lap"] = rdr["lap"].ToString();
                                //}

                                datarow["Total SKS"] = rdr["sks"].ToString();

                                //if (rdr["jenis"] != DBNull.Value)
                                //{
                                //    //datarow["Jenis"] = rdr["jenis"].ToString();
                                //    if (rdr["jenis"].ToString() == "A")
                                //    {
                                //        datarow["Jenis"] = "Wajib";
                                //    }
                                //    else if (rdr["jenis"].ToString() == "B")
                                //    {
                                //        datarow["Jenis"] = "Pilihan";
                                //    }
                                //    else if (rdr["jenis"].ToString() == "S")
                                //    {
                                //        datarow["Jenis"] = "TA/Skripsi/Tesis/Disertasi";
                                //    }
                                //}

                                datarow["Semester"] = rdr["semester"].ToString();
                                datarow["KURIKULUM"] = rdr["kurikulum"].ToString();

                                TableMakul.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVMakul.DataSource = TableMakul;
                            this.GVMakul.DataBind();

                        }
                        else
                        {
                            //clear Gridview
                            TableMakul.Rows.Clear();
                            TableMakul.Clear();
                            GVMakul.DataSource = TableMakul;
                            GVMakul.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Mata Kuliah Tidak Ditemukan');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void GVMakul_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVMakul.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        //int TotalSKS = 0;
        protected void GVMakul_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        // ---------- Hitung SKS dan Peserta -----------------
            //        int SKS = Convert.ToInt32(e.Row.Cells[6].Text);
            //        TotalSKS += SKS;
            //    }
            //    else if (e.Row.RowType == DataControlRowType.Footer)
            //    {
            //        e.Row.Cells[2].Text = "Jumlah SKS";
            //        e.Row.Cells[6].Text = TotalSKS.ToString();
            //    }
            //} catch
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Error in GVRowDataBound');", true);
            //}
        }

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            ////============= Edit SKS Mata Kuliah ====================== //
            //ModalPopupExtender1.Show();

            //// get row index
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;

            //this.LbIdMakul.Text = this.GVMakul.Rows[index].Cells[1].Text;
            //_KodeMakul = this.GVMakul.Rows[index].Cells[1].Text;
            //this.TbMakul.Text = this.GVMakul.Rows[index].Cells[2].Text;

            //if (this.GVMakul.Rows[index].Cells[3].Text != "&nbsp;")
            //{
            //    this.TbTtpMuka.Text = this.GVMakul.Rows[index].Cells[3].Text;
            //}
            //else
            //{
            //    this.TbTtpMuka.Text = "";
            //}

            //if (this.GVMakul.Rows[index].Cells[4].Text != "&nbsp;")
            //{
            //    this.TbPrak.Text = this.GVMakul.Rows[index].Cells[4].Text;
            //}
            //else
            //{
            //    this.TbPrak.Text = "";
            //}
            //if (this.GVMakul.Rows[index].Cells[5].Text != "&nbsp;")
            //{
            //    this.TbLaporan.Text = this.GVMakul.Rows[index].Cells[5].Text;
            //}
            //else
            //{
            //    this.TbLaporan.Text = "";
            //}

            //this.TbSKS.Text = this.GVMakul.Rows[index].Cells[6].Text;

            //if (this.GVMakul.Rows[index].Cells[7].Text != "&nbsp;")
            //{
            //    if (this.GVMakul.Rows[index].Cells[7].Text == "Wajib")
            //    {
            //        this.DlStatusMK.SelectedValue = "A";
            //    }
            //    else if (this.GVMakul.Rows[index].Cells[7].Text == "Pilihan")
            //    {
            //        this.DlStatusMK.SelectedValue = "B";
            //    }
            //    else if (this.GVMakul.Rows[index].Cells[7].Text == "TA/Skripsi/Tesis/Disertasi")
            //    {
            //        this.DlStatusMK.SelectedValue = "S";
            //    }
            //}
            //else
            //{
            //    this.DlStatusMK.SelectedValue = "-1";
            //}
            ////============= End Edit SKS Mata Kuliah ====================== //


            //// ================ SCRIPT VALID =====================/////
            ////============== Edit SKS Mata Kuliah ====================== //
            ////============== SCRIPT INSIDENTAL =========================//

            //ModalPopupExtender2.Show();

            //// get row index
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index2 = gvRow.RowIndex;

            //this.LbIdMakul2.Text = this.GVMakul.Rows[index2].Cells[0].Text.Trim();
            //_KodeMakul2 = this.GVMakul.Rows[index2].Cells[0].Text.Trim();
            //this.TbKdMakul2.Text = _KodeMakul;
            //this.TbMakul2.Text = this.GVMakul.Rows[index2].Cells[1].Text.Trim();
            //this.TbSKS2.Text = this.GVMakul.Rows[index2].Cells[2].Text.Trim();
            //this.TbSemester2.Text = this.GVMakul.Rows[index2].Cells[3].Text.Trim();

            ////============= End Edit SKS Mata Kuliah ====================== //


        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (this.TbMakul.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI MATA KULIAH');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbTtpMuka.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI TATAP MUKA');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbPrak.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI PRAKTIKUM');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (this.TbLaporan.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI LAPORAN');", true);
                ModalPopupExtender1.Show();
                return;
            }

            if (this.TbSKS.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI JUMLAH SKS');", true);
                ModalPopupExtender1.Show();
                return;
            }
            if (_KodeMakul != this.LbIdMakul.Text)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('MAKUL DIUBAH');", true);
                ModalPopupExtender1.Show();
                return;
            }

            if (this.DlStatusMK.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('PILIH KETERANGAN MATA KULIAH');", true);
                ModalPopupExtender1.Show();
                return;
            }


            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdUpStatus = new SqlCommand("SpUpMakul", con);
                    CmdUpStatus.CommandType = System.Data.CommandType.StoredProcedure;
                    CmdUpStatus.Parameters.AddWithValue("@kode", this.LbIdMakul.Text);
                    CmdUpStatus.Parameters.AddWithValue("@makul", this.TbMakul.Text);
                    CmdUpStatus.Parameters.AddWithValue("@ttpmuka", this.TbTtpMuka.Text);
                    CmdUpStatus.Parameters.AddWithValue("@praktik", this.TbPrak.Text);
                    CmdUpStatus.Parameters.AddWithValue("@laporan", this.TbLaporan.Text);
                    CmdUpStatus.Parameters.AddWithValue("@sks", this.TbSKS.Text);
                    CmdUpStatus.Parameters.AddWithValue("@jenis_makul", this.DlStatusMK.SelectedValue);
                    CmdUpStatus.ExecuteNonQuery();

                    Response.Redirect(Request.Url.AbsoluteUri);

                    this.LbIdMakul.Text= "";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Update Berhasil');", true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnUpdate2_Click(object sender, EventArgs e)
        {
            if (this.TbMakul2.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI MATA KULIAH');", true);
                ModalPopupExtender2.Show();
                return;
            }

            if (this.TbKdMakul2.Text == string.Empty)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI KODE MATA KULIAH');", true);
                ModalPopupExtender2.Show();
                return;
            }
            
            if (_KodeMakul2 != this.LbIdMakul2.Text)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('KODE MAKUL DIUBAH !!!');", true);
                ModalPopupExtender2.Show();
                return;
            }

            //if (this.TbSemester2.Text == string.Empty)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI SEMESTER PELAKSANAAN');", true);
            //    ModalPopupExtender2.Show();
            //    return;
            //}

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand CmdUpStatus = new SqlCommand("SpUbahMakul", con);
                    CmdUpStatus.CommandType = System.Data.CommandType.StoredProcedure;
                    CmdUpStatus.Parameters.AddWithValue("@OldKdMakul", this.LbIdMakul2.Text.Trim());
                    CmdUpStatus.Parameters.AddWithValue("@NewKdMakul", this.TbKdMakul2.Text.Trim());
                    CmdUpStatus.Parameters.AddWithValue("@SksMakul", this.TbSKS2.Text.Trim() );
                    CmdUpStatus.Parameters.AddWithValue("@SemMakul", this.TbSemester2.Text.Trim() );
                    CmdUpStatus.Parameters.AddWithValue("@Makul", this.TbMakul2.Text);

                    CmdUpStatus.ExecuteNonQuery();

                    this.LbIdMakul2.Text= "";
                    string msg = "alert('Update Berhasil ...')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

                    Load_Makul();
                }
            }
            catch (Exception ex)
            {
                this.LbIdMakul2.Text = "";
                string msg = "alert('" + ex.Message + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                return;
            }
        }

        protected void GVMakul_PreRender(object sender, EventArgs e)
        {
            if (this.GVMakul.Rows.Count > 0)
            {
                // this replace <td> with <th> and add the scope attribute
                GVMakul.UseAccessibleHeader = true;

                //this will add the <thead> and <tbody> elements
                GVMakul.HeaderRow.TableSection = TableRowSection.TableHeader;

                //this adds the <tfoot> element
                //remove if you don't have a footer row
                //GVJadwal.FooterRow.TableSection = TableRowSection.TableFooter;

            }
        }
    }
}