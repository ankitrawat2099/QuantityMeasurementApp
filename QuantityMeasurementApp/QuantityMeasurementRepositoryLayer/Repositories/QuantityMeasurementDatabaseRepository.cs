using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string connectionString;

        public QuantityMeasurementDatabaseRepository(IConfiguration configuration)
        {
            // Reads the connection string from appsettings.json
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection() => new SqlConnection(connectionString);

        public void Save(QuantityMeasurementEntity entity)
        {
            using var connection = GetConnection();
            connection.Open();

            string query =
                @"INSERT INTO QuantityMeasurements
                (FirstValue, FirstUnit, SecondValue, SecondUnit, Operation, Result, MeasurementType)
                VALUES
                (@FirstValue, @FirstUnit, @SecondValue, @SecondUnit, @Operation, @Result, @MeasurementType)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstValue", entity.FirstValue);
            command.Parameters.AddWithValue("@FirstUnit", entity.FirstUnit);
            command.Parameters.AddWithValue("@SecondValue", entity.SecondValue);
            command.Parameters.AddWithValue("@SecondUnit", entity.SecondUnit);
            command.Parameters.AddWithValue("@Operation", entity.Operation);
            command.Parameters.AddWithValue("@Result", entity.Result);
            command.Parameters.AddWithValue("@MeasurementType", entity.MeasurementType);

            command.ExecuteNonQuery();
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            var list = new List<QuantityMeasurementEntity>();

            using var connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM QuantityMeasurements";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(ReadEntity(reader));
            }

            return list;
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            var list = new List<QuantityMeasurementEntity>();

            using var connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM QuantityMeasurements WHERE Operation=@Operation";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Operation", operation);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadEntity(reader));
            }

            return list;
        }

        public List<QuantityMeasurementEntity> GetByMeasurementType(string type)
        {
            var list = new List<QuantityMeasurementEntity>();

            using var connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM QuantityMeasurements WHERE MeasurementType=@Type";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Type", type);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(ReadEntity(reader));
            }

            return list;
        }

        public int GetTotalCount()
        {
            using var connection = GetConnection();
            connection.Open();

            string query = "SELECT COUNT(*) FROM QuantityMeasurements";
            using var command = new SqlCommand(query, connection);

            return (int)command.ExecuteScalar();
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();
            connection.Open();

            string query = "DELETE FROM QuantityMeasurements";
            using var command = new SqlCommand(query, connection);

            command.ExecuteNonQuery();
        }

        // Helper method to map SqlDataReader to entity
        private QuantityMeasurementEntity ReadEntity(SqlDataReader reader)
        {
            return new QuantityMeasurementEntity
            {
                Id = (int)reader["Id"],
                FirstValue = (double)reader["FirstValue"],
                FirstUnit = reader["FirstUnit"].ToString()!,
                SecondValue = (double)reader["SecondValue"],
                SecondUnit = reader["SecondUnit"].ToString()!,
                Operation = reader["Operation"].ToString()!,
                Result = (double)reader["Result"],
                MeasurementType = reader["MeasurementType"].ToString()!
            };
        }
    }
}