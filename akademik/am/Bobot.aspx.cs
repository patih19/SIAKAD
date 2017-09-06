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
    //public partial class WebForm13 : System.Web.UI.Page
    public partial class WebForm13 : Bak_staff
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // ---------- Gridview SKS ------------------
                string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    // --------------------- Fill Gridview UAS Makul ------------------------
                    SqlCommand CmdBobot = new SqlCommand("select * from bak_bobot", con);
                    CmdBobot.CommandType = System.Data.CommandType.Text;

                    DataTable TableBobot = new DataTable();
                    TableBobot.Columns.Add("Nilai");
                    TableBobot.Columns.Add("Point");

                    using (SqlDataReader rdr = CmdBobot.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                DataRow datarow = TableBobot.NewRow();
                                datarow["Nilai"] = rdr["nilai"];
                                datarow["Point"] = rdr["point"];

                                TableBobot.Rows.Add(datarow);
                            }

                            //Fill Gridview
                            this.GVBobot.DataSource = TableBobot;
                            this.GVBobot.DataBind();
                        }
                        else
                        {
                            //clear Gridview
                            TableBobot.Rows.Clear();
                            TableBobot.Clear();
                            GVBobot.DataSource = TableBobot;
                            GVBobot.DataBind();
                        }
                    }
                }
            }
        }

        protected void GVBobot_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            List<TableCell> columns = new List<TableCell>();

            foreach (DataControlField column in GVBobot.Columns)
            {
                TableCell cell = row.Cells[0];
                row.Cells.Remove(cell);
                columns.Add(cell);
            }

            row.Cells.AddRange(columns.ToArray());
        }
    }
}