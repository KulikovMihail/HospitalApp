using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Services
{
    public static class PatientService
    {
        public static List<Patient> GetAllPatients()
        {
            var list = new List<Patient>();
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT PatientID, FirstName, LastName, Age, Diagnosis FROM Patients", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Patient
                        {
                            PatientID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Age = reader.GetInt32(3),
                            Diagnosis = reader.IsDBNull(4) ? "" : reader.GetString(4)
                        };
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        public static void AddPatient(Patient p)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Patients (FirstName, LastName, Age, Diagnosis) " +
                            "VALUES (@fn, @ln, @age, @diag)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fn", p.FirstName);
                cmd.Parameters.AddWithValue("@ln", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.Parameters.AddWithValue("@diag", string.IsNullOrEmpty(p.Diagnosis) ? (object)DBNull.Value : p.Diagnosis);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdatePatient(Patient p)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "UPDATE Patients SET FirstName=@fn, LastName=@ln, Age=@age, Diagnosis=@diag WHERE PatientID=@id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fn", p.FirstName);
                cmd.Parameters.AddWithValue("@ln", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.Parameters.AddWithValue("@diag", string.IsNullOrEmpty(p.Diagnosis) ? (object)DBNull.Value : p.Diagnosis);
                cmd.Parameters.AddWithValue("@id", p.PatientID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeletePatient(int patientId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Patients WHERE PatientID=@id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", patientId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
