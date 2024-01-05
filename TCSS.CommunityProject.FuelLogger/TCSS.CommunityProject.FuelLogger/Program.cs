using TCSS.CommunityProject.FuelLogger;

var fuelDataAccess = new DataAccess();
fuelDataAccess.CreateDatabase();
SeedData.SeedRecords(10);
Console.ReadLine();