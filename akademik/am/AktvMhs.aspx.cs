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
    //public partial class WebForm29 : System.Web.UI.Page
    public partial class WebForm29 : Bak_staff
    {
        public string _No
        {
            get { return this.ViewState["No"].ToString(); }
            set { this.ViewState["No"] = (object)value; }
        }

        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }

        public string _Nama
        {
            get { return this.ViewState["Nama"].ToString(); }
            set { this.ViewState["Nama"] = (object)value; }
        }

        public string _Semester
        {
            get { return this.ViewState["Semester"].ToString(); }
            set { this.ViewState["Semester"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PupulateProdi();
            }
        }

        private void PupulateProdi()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SELECT id_prog_study,prog_study FROM dbo.bak_prog_study", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    this.DLProdi.DataSource = CmdJadwal.ExecuteReader();
                    this.DLProdi.DataTextField = "prog_study";
                    this.DLProdi.DataValueField = "id_prog_study";
                    this.DLProdi.DataBind();

                    con.Close();
                    con.Dispose();
                }

                this.DLProdi.Items.Insert(0, new ListItem("-- Program Studi --","-1"));
                
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void BtnAktvMhs_Click(object sender, EventArgs e)
        {
            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }

            if (this.DLProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.DlSemester.SelectedValue == "semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            //try
            //{
            //    string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(CS))
            //    {
            //        //------------------------------------------------------------------------------------
            //        con.Open();

            //        SqlCommand CmdGenKHS = new SqlCommand("SpGenerateTRAKM", con);
            //        CmdGenKHS.CommandType = System.Data.CommandType.StoredProcedure;

            //        CmdGenKHS.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text);
            //        CmdGenKHS.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);

            //        CmdGenKHS.ExecuteNonQuery();

            //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Generate KHS Berhasil ...');", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
            //    return;
            //}
        }

        protected void DLProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAktvMhs();

        }

        protected void LoadAktvMhs()
        {

            if (this.DLTahun.SelectedItem.Text == "Tahun")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Tahun');", true);
                return;
            }

            if (this.DLProdi.SelectedValue == "-1")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Program Studi');", true);
                return;
            }
            if (this.DlSemester.SelectedValue == "semester")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Pilih Semester');", true);
                return;
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("SpGetAktvMhs", con);
                    CmdJadwal.CommandType = System.Data.CommandType.StoredProcedure;

                    CmdJadwal.Parameters.AddWithValue("@semester", this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text.Trim());
                    CmdJadwal.Parameters.AddWithValue("@idprodi", this.DLProdi.SelectedValue);

                    DataTable TableJadwal = new DataTable();
                    TableJadwal.Columns.Add("Nomor");
                    TableJadwal.Columns.Add("No");
                    TableJadwal.Columns.Add("NPM");
                    TableJadwal.Columns.Add("Nama");
                    TableJadwal.Columns.Add("IPS");
                    TableJadwal.Columns.Add("SKS-Sem");
                    TableJadwal.Columns.Add("IPK");
                    TableJadwal.Columns.Add("SKS-Total");
                    TableJadwal.Columns.Add("ThnAngkatan");

                    using (SqlDataReader rdr = CmdJadwal.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableJadwal.NewRow();
                                datarow["Nomor"] = rdr["nomor"];
                                datarow["No"] = rdr["no"];
                                datarow["NPM"] = rdr["npm"];
                                datarow["Nama"] = rdr["nama"];
                                datarow["IPS"] = rdr["ips"];
                                datarow["SKS-Sem"] = rdr["sks_sem"];
                                datarow["IPK"] = rdr["ipk"];
                                datarow["SKS-Total"] = rdr["sks_total"];
                                datarow["ThnAngkatan"] = rdr["thn_angkatan"].ToString().Trim().Substring(0, 4);

                                TableJadwal.Rows.Add(datarow);
                            }
                            // Semester
                            _Semester = this.DLTahun.SelectedValue.ToString() + this.DlSemester.SelectedItem.Text.Trim();

                            //Fill Gridview
                            this.GVAktvMhs.DataSource = TableJadwal;
                            this.GVAktvMhs.DataBind();

                            MarkEdit(this.GVAktvMhs);
                        }
                        else
                        {
                            //clear Gridview
                            TableJadwal.Rows.Clear();
                            TableJadwal.Clear();
                            GVAktvMhs.DataSource = TableJadwal;
                            GVAktvMhs.DataBind();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Data Tidak Ditemukan');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        public void MarkEdit(GridView gridview)
        {
            for (int i = 0; i < GVAktvMhs.Rows.Count; i++)
            {
                LinkButton CB = (LinkButton)GVAktvMhs.Rows[i].FindControl("LnkEdit");

                if (Convert.ToInt16(GVAktvMhs.Rows[i].Cells[8].Text.Trim()) + 1 <= 2015)
                {                    
                    CB.Visible = true;
                }
                else
                {
                    CB.Visible = false;
                }
            }
        }

        protected void GVAktvMhs_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in this.GVAktvMhs.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }
            row.Cells.AddRange(columns.ToArray());
        }

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();

            // Get Row Index
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index2 = gvRow.RowIndex;

            // Id Aktv Mhs, NPM & Nama
            _No = this.GVAktvMhs.Rows[index2].Cells[1].Text.Trim();
            _NPM = this.GVAktvMhs.Rows[index2].Cells[2].Text.Trim();
            _Nama = this.GVAktvMhs.Rows[index2].Cells[3].Text.Trim();

            this.LbNPM.Text = _NPM;
            this.LbNama.Text = _Nama;
            this.TbIPK.Text = this.GVAktvMhs.Rows[index2].Cells[6].Text.Trim();
            this.TbSKSTotal.Text = this.GVAktvMhs.Rows[index2].Cells[7].Text.Trim();
            this.LbSemester.Text = _Semester;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if ((this.TbIPK.Text.Trim().Length > 4) || (this.TbIPK.Text.Trim().Length == 0))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('Isi IPK');", true);
                ModalPopupExtender1.Show();
                return;
            }

            if ((this.TbSKSTotal.Text.Trim().Length > 3) || (this.TbSKSTotal.Text.Trim().Length == 0))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('ISI SKS Total');", true);
                ModalPopupExtender1.Show();
                return;
            }

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdJadwal = new SqlCommand("UPDATE dbo.bak_aktv_mhs SET ipk=@ipk, sks_total=@skstotal WHERE no=@no", con);
                    CmdJadwal.CommandType = System.Data.CommandType.Text;

                    CmdJadwal.Parameters.AddWithValue("@ipk", this.TbIPK.Text.Trim());
                    CmdJadwal.Parameters.AddWithValue("@skstotal", this.TbSKSTotal.Text.Trim());
                    CmdJadwal.Parameters.AddWithValue("@no",_No.Trim());
                    
                    CmdJadwal.ExecuteNonQuery();

                    LoadAktvMhs();

                    string msg = "alert('Update Berhasil...')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
                }
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void GVAktvMhs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // id aktv mhs
            e.Row.Cells[1].Visible = false;
            // thn angkatan
            e.Row.Cells[8].Visible = false;
        }


    }
}