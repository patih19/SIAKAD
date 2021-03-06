﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Keuangan
{
    public class mahasiswa
    {
        public string npm { get; set; }
        public string nama { get; set; }
        public string thn_angkatan { get; set; }
        public string kelas { get; set; }
        public string Prodi { get; set; }
        public string id_prodi { get; set; }

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
                            this.npm = npm;
                            nama = rdr["nama"].ToString();
                            kelas = rdr["kelas"].ToString();
                            thn_angkatan = rdr["thn_angkatan"].ToString();
                            Prodi = rdr["prog_study"].ToString();
                            id_prodi = rdr["id_prog_study"].ToString();
                        }
                    }
                }
            }
        }
    }

    public class Mhs
    {
    }
}