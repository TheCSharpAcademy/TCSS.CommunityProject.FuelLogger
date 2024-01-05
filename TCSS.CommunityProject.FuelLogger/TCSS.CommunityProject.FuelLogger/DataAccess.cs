﻿using Dapper;
using Microsoft.Data.Sqlite;
using TCSS.CommunityProject.FuelLogger.Models;


namespace TCSS.CommunityProject.FuelLogger;
internal class DataAccess
{
    string connectionString = @"Data Source=fuelLogger.db";
    public void CreateDatabase()
    {
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

    internal void BulkInsertRecords(List<FuelRecord> records)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string insertQuery = @"
        INSERT INTO FuelRecord (DateCreated, Make,Model,FuelType,Year)
        VALUES (@DateCreated, @Make,@Model,@FuelType,@Year)";

            connection.Execute(insertQuery, records.Select(record => new
            {
                record.DateCreated,
                record.Make,
                record.Model,
                record.FuelType,
                record.Year
            }));
        }
    }
}

