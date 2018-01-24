using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Padu.account
{
    public class mhs
    {
        public string npm { get; set; }
        public string nama { get; set; }
        public string thn_angkatan { get; set; }
        public string gelombang { get; set; }
        public string kelas { get; set; }
        public string semester { get; set; }
        public string Prodi { get; set; }
        public string DosenPA { get; set; }

        public void ReadMahasiswa(string npm)
        {
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@npm", npm);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            nama = rdr["nama"].ToString();
                            kelas = rdr["kelas"].ToString();
                            gelombang = rdr["gelombang"].ToString();
                            thn_angkatan = rdr["thn_angkatan"].ToString();
                            Prodi = rdr["prog_study"].ToString();
                            if (rdr["dosen"] != DBNull.Value)
                            {
                                DosenPA = rdr["dosen"].ToString().Trim();
                            }
                            else
                            {
                                DosenPA = "Belum memiliki Pembimbing Akademik";
                            }
                        }
                    }
                }
            }
        }
    }

    public class MhsDAL
    {
        public static List<mhs> GetMhsByNPM(string npm)
        {
            List<mhs> listMhs = new List<mhs>();
            string CS = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpGetMhsByNPM", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@npm", npm);
               
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            mhs mahasiswa = new mhs();
                            //contoh convert jika dibutuhkan:
                            //mahasiswa.gelombang = Convert.ToInt32(rdr["gelombang"]);

                            mahasiswa.nama = rdr["nama"].ToString();
                            mahasiswa.kelas = rdr["kelas"].ToString();
                            mahasiswa.gelombang = rdr["gelombang"].ToString();
                            mahasiswa.thn_angkatan = rdr["thn_angkatan"].ToString();

                            listMhs.Add(mahasiswa);
                        }
                    }
                }
                return listMhs;
            }
        }
    }
}
