using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using System.Data.SqlClient;
using System.Configuration;

namespace SIM.WEB_SERVICE
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetName(string knownCategoryValues)
        {
            return this.GetData("select userid,username,password from [user];").ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] semester(string knownCategoryValues, string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            // select TOP 1 semester from keu_biaya ORDER BY semester DESC
            SqlCommand sqlCommand = new SqlCommand("select semester from keu_biaya group by semester");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["semester"].ToString(), sqlDataReader["semester"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] tahun(string knownCategoryValues, string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("select tahun from keu_biaya_akhir group by tahun");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["tahun"].ToString(), sqlDataReader["tahun"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        private List<CascadingDropDownNameValue> GetData(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand(query);
            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue()
                        {
                            name = sqlDataReader[1].ToString() + " | " + sqlDataReader[1].ToString(),
                            value = sqlDataReader[0].ToString()
                        });
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list;
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetData2(string knownCategoryValues, string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("select userid,username,password from [user];");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["username"].ToString(), sqlDataReader["username"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetData3(string knownCategoryValues, string category)
        {
            string str = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["UserName"];
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("SELECT [user].userid, [user].username, kota.kota FROM kota INNER JOIN [user] ON kota.id_kota = [user].id_kota where [USER].username=@0");
            sqlCommand.Parameters.AddWithValue("@0", (object)str);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["kota"].ToString(), sqlDataReader["kota"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] ProgStudy(string knownCategoryValues, string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlCommand sqlCommand = new SqlCommand("select bak_prog_study.id_prog_study,bak_prog_study.prog_study from bak_prog_study"))
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    sqlCommand.Connection = sqlConnection;
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                        while (sqlDataReader.Read())
                            list.Add(new CascadingDropDownNameValue(sqlDataReader["prog_study"].ToString(), sqlDataReader["id_prog_study"].ToString()));
                        sqlDataReader.Close();
                        sqlDataReader.Dispose();
                        sqlConnection.Close();
                        return list.ToArray();
                    }
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] Prov(string knownCategoryValues, string category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("select daerah_provinsi.id_prov,daerah_provinsi.nama from daerah_provinsi");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["nama"].ToString(), sqlDataReader["id_prov"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] Kotakab(string knownCategoryValues, string category)
        {
            string str = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["id_provinsi"];
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("SELECT   daerah_provinsi.id_prov, daerah_kotakab.id_kotakab, daerah_kotakab.nama FROM     daerah_kotakab INNER JOIN daerah_provinsi ON daerah_kotakab.id_prov = daerah_provinsi.id_prov where daerah_provinsi.id_prov=@id_prov");
            sqlCommand.Parameters.AddWithValue("@id_prov", (object)str);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["nama"].ToString(), sqlDataReader["id_kotakab"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] Kecamatan(string knownCategoryValues, string category)
        {
            string str = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["id_kotakab"];
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("SELECT   daerah_kotakab.id_kotakab, daerah_kecamatan.nama AS kecamatan, daerah_kecamatan.id_kec FROM     daerah_kecamatan INNER JOIN daerah_kotakab ON daerah_kecamatan.id_kotakab = daerah_kotakab.id_kotakab where daerah_kotakab.id_kotakab=@id_kotakab");
            sqlCommand.Parameters.AddWithValue("@id_kotakab", (object)str);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["kecamatan"].ToString(), sqlDataReader["id_kec"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }

        [WebMethod]
        public CascadingDropDownNameValue[] Desa(string knownCategoryValues, string category)
        {
            string str = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["id_kec"];
            string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            SqlCommand sqlCommand = new SqlCommand("SELECT     daerah_kecamatan.id_kec, daerah_desa.id_desa, daerah_desa.nama FROM       daerah_desa INNER JOIN daerah_kecamatan ON daerah_desa.id_kec = daerah_kecamatan.id_kec where daerah_kecamatan.id_kec=@id_kec");
            sqlCommand.Parameters.AddWithValue("@id_kec", (object)str);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
                    while (sqlDataReader.Read())
                        list.Add(new CascadingDropDownNameValue(sqlDataReader["nama"].ToString(), sqlDataReader["nama"].ToString()));
                    sqlDataReader.Close();
                    sqlDataReader.Dispose();
                    sqlConnection.Close();
                    return list.ToArray();
                }
            }
        }
    }
}
