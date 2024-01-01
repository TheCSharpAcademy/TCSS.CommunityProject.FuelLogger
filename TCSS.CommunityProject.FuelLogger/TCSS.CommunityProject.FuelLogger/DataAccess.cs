using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

namespace TCSS.CommunityProject.FuelLogger
{
    internal class DataAccess
    {

        public void CreateDatabase()
        {
            string connectionString = @"Data Source=fuelLogger.db";

            SqliteConnection connection = new(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @$"CREATE TABLE IF NOT EXISTS 'vehicle' (
                                VehicleId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                DateCreated DATE NOT NULL,
                                Make TEXT NOT NULL,
                                Model TEXT NOT NULL,
                                FuelType INT NOT NULL
                    )";
            tableCmd.ExecuteNonQuery();
            connection.Close();


        }
    }
}
