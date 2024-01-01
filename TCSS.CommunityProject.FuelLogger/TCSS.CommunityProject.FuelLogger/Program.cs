using Microsoft.Data.Sqlite;
using Dapper;
using TCSS.CommunityProject.FuelLogger;

var fuelDataAccess = new DataAccess();
fuelDataAccess.CreateDatabase();