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
    //public partial class WebForm18 : System.Web.UI.Page
    public partial class WebForm18 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmdvw = new SqlCommand("SELECT fak_kode, fak_name FROM bak_fakultas", con);
                    cmdvw.CommandType = System.Data.CommandType.Text;

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Kode Fakultas");
                    Table.Columns.Add("Nama Fakultas");

                    con.Open();
                    using (SqlDataReader rdr = cmdvw.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();
                                datarow["Kode Fakultas"] = rdr["fak_kode"];
                                datarow["Nama Fakultas"] = rdr["fak_name"];
                                //DateTime DateAwal = Convert.ToDateTime(rdr["open_date"]);
                                //DateTime DateAkhir = Convert.ToDateTime(rdr["close_date"]);
                                //datarow["Tanggal Buka"] = DateAwal.ToString("dd MMMM, yyyy");
                                //datarow["Tanggal Tutup"] = DateAkhir.ToString("dd MMMM, yyyy");
                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVFakultas.DataSource = Table;
                            this.GVFakultas.DataBind();

                            Table.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVFakultas.DataSource = Table;
                            GVFakultas.DataBind();
                        }
                    }
                }
            }
        }

        protected void BtnDetail_Click(object sender, EventArgs e)
        {

        }
    }
}