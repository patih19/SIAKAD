using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Padu.account
{
    public partial class Start : Mhs_account
    {
        public string _NPM
        {
            get { return this.ViewState["NPM"].ToString(); }
            set { this.ViewState["NPM"] = (object)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _NPM = this.Session["Name"].ToString().Trim();

                //------------- Get Tahun Angkatan Mahasiswa --------------------- //
                int AngkatanMhs = 0;

                string CSMaba = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CSMaba))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@npm", _NPM);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                AngkatanMhs = Convert.ToInt16(rdr["thn_angkatan"].ToString().Trim().Substring(0,4));
                            }
                        }
                        rdr.Close();
                        rdr.Dispose();
                    }
                }


                if (AngkatanMhs >= 2015)
                {
                    // ------- UKT (2015 dan setelahnya)------
                    Response.Redirect("~/account/ukt.aspx");
                }
                else
                {
                    //-------- Mahasiswa Lama (2014 dan sebelumnya) --------
                    Response.Redirect("~/account/keu.aspx");

                }

            }
        }
    }
}