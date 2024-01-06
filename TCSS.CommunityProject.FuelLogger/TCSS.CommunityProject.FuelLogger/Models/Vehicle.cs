namespace TCSS.CommunityProject.FuelLogger.Models;

public class Vehicle
{
    public long VehicleId { get; set; }
    public string DateCreated { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public long FuelType { get; set; }
    public long Year { get; set; }
}
