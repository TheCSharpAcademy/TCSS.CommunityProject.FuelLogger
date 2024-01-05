using Newtonsoft.Json;
using System.Text.Json.Nodes;
using TCSS.CommunityProject.FuelLogger.Models;

namespace TCSS.CommunityProject.FuelLogger;

internal static class SeedData
{
    internal static async void SeedRecords(int count)
    {
        Random random = new();
        List<FuelRecord> records = new();
        string[] carMakers = new[] { "toyota", "mazda", "ford", "mercedes", "audi", "nissan", "hyundai", "Renault" };
        for (int i = 0; i < count; i++)
        {
            string carMake = carMakers[random.Next(0, carMakers.Length)];
            using (HttpClient httpClient = new HttpClient())
            {
                Console.WriteLine("new");
                string url = $"https://api.api-ninjas.com/v1/cars?limit=1&make={carMake}";

                // Create a new HttpRequestMessage
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                // Set a custom header
                request.Headers.Add("X-Api-Key", "WAVkDKfIiGKTjo7fikhTdQ==PJoxRy4t2tj6lySz");

                // Send the request and get the response
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var jsonObject = JsonNode.Parse(content);
                    Console.WriteLine(jsonObject.ToString());
                    Console.WriteLine(jsonObject[0]);
                    FuelRecord model = JsonConvert.DeserializeObject<FuelRecord>(jsonObject[random.Next(0, 1)].ToString());
                    model.Year = random.Next(1970, 2023);
                    model.FuelType = random.Next(1, 3);
                    model.DateCreated = DateTime.Now.ToString();
                    records.Add(model);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
        DataAccess dataAccess = new();
        dataAccess.BulkInsertRecords(records);
    }
}
