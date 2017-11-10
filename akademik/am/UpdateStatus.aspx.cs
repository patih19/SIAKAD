using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace akademik.am
{
    public partial class UpdateStatus : Bak_staff
    {

        public string _SEMESTER
        {
            get { return this.ViewState["SEMESTER"].ToString(); }
            set { this.ViewState["SEMESTER"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GetStatusUpdate();
                ReadSemesterUpdate();
                GetLastSemester();
            }
        }

        protected void GetLastSemester()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdUpdateMhs = new SqlCommand("" +
                        "SELECT        TOP(1) bak_jadwal.semester " +
                        "FROM            bak_jadwal INNER JOIN " +
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN " +
                                                 "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study " +
                        "WHERE bak_prog_study.jenjang NOT IN('S2') " +
                        "GROUP BY bak_jadwal.semester " +
                        "ORDER BY bak_jadwal.semester DESC " +
                        "", con);

                    CmdUpdateMhs.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader rdr = CmdUpdateMhs.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while(rdr.Read())
                            {
                                _SEMESTER = rdr["semester"].ToString().Trim();
                            }
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

        protected void GetStatusUpdate()
        {                       
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdUpdateMhs = new SqlCommand(""+
                        "DECLARE @SemAktivUpdateMhs VARCHAR(5) = '' "+

                        "SELECT        TOP(1) @SemAktivUpdateMhs = bak_jadwal.semester "+
                        "FROM            bak_jadwal INNER JOIN "+
                                                 "bak_krs ON bak_jadwal.no_jadwal = bak_krs.no_jadwal INNER JOIN "+
                                                 "bak_prog_study ON bak_jadwal.id_prog_study = bak_prog_study.id_prog_study "+
                        "WHERE bak_prog_study.jenjang NOT IN('S2') "+
                        "GROUP BY bak_jadwal.semester "+
                        "ORDER BY bak_jadwal.semester DESC "+

                        "SELECT id_update FROM bak_update_status_mhs WHERE semester = @SemAktivUpdateMhs "+
                        "", con);

                    CmdUpdateMhs.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader rdr = CmdUpdateMhs.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            this.LbMsgUpdate.Text = "Data mahasiswa pada semester ini sudah diupdate";
                            this.LbMsgUpdate.ForeColor = System.Drawing.Color.Green;
                            this.BtnUpdateStatus.Enabled = false;
                            this.BtnUpdateStatus.Visible = false;
                        }
                        else
                        {
                            this.LbMsgUpdate.Text = "Data mahasiswa pada semester ini belum diupdate";
                            this.LbMsgUpdate.ForeColor = System.Drawing.Color.Red;
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

        protected void ReadSemesterUpdate()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    //------------------------------------------------------------------------------------
                    con.Open();

                    SqlCommand CmdUpdateMhs = new SqlCommand("SELECT TOP 10 * FROM dbo.bak_update_status_mhs ORDER BY semester DESC", con);

                    CmdUpdateMhs.CommandType = System.Data.CommandType.Text;

                    DataTable TablePengajuan = new DataTable();
                    TablePengajuan.Columns.Add("Semester");
                    TablePengajuan.Columns.Add("Tanggal Update");

                    using (SqlDataReader rdr = CmdUpdateMhs.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TablePengajuan.NewRow();
                                datarow["Semester"] = rdr["semester"];
                                datarow["Tanggal Update"] = rdr["tgl_update"];

                                TablePengajuan.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GvSemUpdate.DataSource = TablePengajuan;
                            this.GvSemUpdate.DataBind();
                        }
                        else
                        {
                            TablePengajuan.Rows.Clear();
                            TablePengajuan.Clear();

                            this.GvSemUpdate.DataSource = TablePengajuan;
                            this.GvSemUpdate.DataBind();
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

        protected void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlTransaction Tran = con.BeginTransaction();
                try
                {
                    //-- ================================= UPDATE STATUS MHS (Mhs UKT) ============================================= --
                    //--catatan : 
                    // --update status mhs dilakukan setelah proses KRS selesai dilaksanan pada semester berjalan(TIDAK BOLEH DITAWAR !!!)
                    //-- update status mhs dilakukan satu minggu setelah masa KRS berakhir
                    // --mhs yg tidak mengajukan cuti akan dicatat sebagai mhs mangkir(non aktif)
                    //-- mhs yg sudah sudah tercatat dua kali mangkir (non aktif), pd semster aktif yg sedang berjalan mhs tersebut akan dikeluarkan
                    // -- ============================================================================================================== --

                    SqlCommand CmdUpdateMhs = new SqlCommand("SpDoUpdateDataMhs", con, Tran);
                    CmdUpdateMhs.CommandType = System.Data.CommandType.StoredProcedure;
                    CmdUpdateMhs.Parameters.AddWithValue("@SemUpdate",_SEMESTER);
                    CmdUpdateMhs.ExecuteNonQuery();
                    Tran.Commit();
                }
                catch (Exception ex)
                {
                    Tran.Rollback();

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message.ToString() + "');", true);
                    return;
                }

            }

        }
    }
}