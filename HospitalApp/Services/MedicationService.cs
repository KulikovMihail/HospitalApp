using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Services
{
    public static class MedicationService
    {
        public static List<Medication> GetAllMedications()
        {
            var list = new List<Medication>();
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT MedicationID, Name, Description, Quantity FROM Medications", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var m = new Medication
                        {
                            MedicationID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Quantity = reader.GetInt32(3)
                        };
                        list.Add(m);
                    }
                }
            }
            return list;
        }

        public static void AddMedication(Medication m)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Medications (Name, Description, Quantity) VALUES (@name, @desc, @qty)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", m.Name);
                cmd.Parameters.AddWithValue("@desc", string.IsNullOrEmpty(m.Description) ? (object)DBNull.Value : m.Description);
                cmd.Parameters.AddWithValue("@qty", m.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateMedication(Medication m)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "UPDATE Medications SET Name=@name, Description=@desc, Quantity=@qty WHERE MedicationID=@id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", m.Name);
                cmd.Parameters.AddWithValue("@desc", string.IsNullOrEmpty(m.Description) ? (object)DBNull.Value : m.Description);
                cmd.Parameters.AddWithValue("@qty", m.Quantity);
                cmd.Parameters.AddWithValue("@id", m.MedicationID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteMedication(int medicationId)
        {
            using (var conn = DBHelper.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Medications WHERE MedicationID=@id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", medicationId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
