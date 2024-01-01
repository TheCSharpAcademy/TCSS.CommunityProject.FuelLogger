using Microsoft.Data.Sqlite;
using Dapper;

namespace TCSS.CommunityProject.FuelLogger;
    internal class DataAccess
    {

        public void CreateDatabase()
        {
            string connectionString = @"Data Source=fuelLogger.db";

            SqliteConnection connection = new(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @$"CREATE TABLE IF NOT EXISTS 'FuelRecord' (
                                VehicleId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                DateCreated DATE NOT NULL,
                                Make TEXT NOT NULL,
                                Model TEXT NOT NULL,
                                FuelType INT NOT NULL,
                                Year INT NOT NULL
                    )";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }

