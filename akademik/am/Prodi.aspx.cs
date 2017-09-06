using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace akademik.am
{
    //public partial class WebForm17 : System.Web.UI.Page
    public partial class WebForm17 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmdvw = new SqlCommand("SELECT bak_prog_study.id_prog_study, bak_prog_study.prog_study,bak_prog_study.jenjang FROM bak_prog_study", con);
                    cmdvw.CommandType = System.Data.CommandType.Text;

                    DataTable Table = new DataTable();
                    Table.Columns.Add("Kode");
                    Table.Columns.Add("Program Studi");
                    Table.Columns.Add("Jenjang");

                    con.Open();
                    using (SqlDataReader rdr = cmdvw.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = Table.NewRow();
                                datarow["Kode"] = rdr["id_prog_study"];
                                datarow["Program Studi"] = rdr["prog_study"];
                                datarow["Jenjang"] = rdr["jenjang"];
                                //DateTime DateAwal = Convert.ToDateTime(rdr["open_date"]);
                                //DateTime DateAkhir = Convert.ToDateTime(rdr["close_date"]);
                                //datarow["Tanggal Buka"] = DateAwal.ToString("dd MMMM, yyyy");
                                //datarow["Tanggal Tutup"] = DateAkhir.ToString("dd MMMM, yyyy");
                                Table.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVProdi.DataSource = Table;
                            this.GVProdi.DataBind();

                            Table.Dispose();
                        }
                        else
                        {
                            //clear Gridview
                            Table.Rows.Clear();
                            Table.Clear();
                            GVProdi.DataSource = Table;
                            GVProdi.DataBind();
                        }
                    }
                }
            }
        }
    }
}