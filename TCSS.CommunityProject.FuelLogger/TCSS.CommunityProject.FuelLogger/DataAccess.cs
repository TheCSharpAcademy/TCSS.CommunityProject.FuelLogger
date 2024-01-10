using Dapper;
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
        tableCmd.CommandText = @$"CREATE TABLE IF NOT EXISTS Vehicles (
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

    internal void BulkInsertRecords(List<Vehicle> records)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string insertQuery = @"
        INSERT INTO Vehicles (DateCreated, Make,Model,FuelType,Year)
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

    internal void AddVehicle(Vehicle vehicle)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string addVehicleQuery = @" INSERT INTO Vehicles (DateCreated, Make,Model,FuelType,Year)
        VALUES (@DateCreated, @Make,@Model,@FuelType,@Year)";
        try
        {
            connection.Execute(addVehicleQuery, new { vehicle.DateCreated, vehicle.Make, vehicle.Model, vehicle.FuelType, vehicle.Year });
        }
        catch (Exception ex) { 
            Console.WriteLine($"There was a problem adding vehicle: {ex.Message}");
        }
    }
}

