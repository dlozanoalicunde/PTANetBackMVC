using AlicundeTest.Domain.Abstract;
using AlicundeTest.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AlicundeTest.Persistence.Seeds;

public class InitialSeed
{
    /// <summary>
    /// Gets the data from endpoint and seeds the DB
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
    /// <returns></returns>
    public static async Task Seed(AlicundeTestDbContext context, IUnitOfWork unitOfWork, ILogger logger)
    {
        var baks = await GetBanksAsync("https://api.opendata.esett.com/EXP06/Banks", logger);

        if (baks != null)
        {
            await context.Banks.AddRangeAsync(baks);
            await unitOfWork.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Gets the data from the endpoint
    /// </summary>
    /// <param name="url">Endpoint</param>
    /// <param name="logger"></param>
    /// <returns>List<Bank></returns>
    private static async Task<List<Bank>> GetBanksAsync(string url, ILogger logger)
    {
        List<Bank> banks = new List<Bank>();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic jsonArray = JsonConvert.DeserializeObject(json);

                    if (jsonArray != null)
                    {
                        foreach (dynamic jsonObject in jsonArray)
                        {
                            // Deserialize each JSON object into a Bank object using the chosen method
                            var bank = DeserializeBank(jsonObject.ToString()); // Pass JSON object as a string
                            banks.Add(bank);
                        }
                    }
                }
                else
                {
                    string errorMessage = "Endpoint response not successful";
                    logger.LogError(errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "It has not been possible to carry out the initial seed: ");
                throw;
            }
            
        }

        return banks;
    }

    /// <summary>
    /// Method that deserializes the response of the endpoint to bank
    /// </summary>
    /// <param name="json">Json line</param>
    /// <returns>Bank</returns>
    public static Bank DeserializeBank(string json)
    {
        dynamic jsonObject = JsonConvert.DeserializeObject(json); // Use Newtonsoft.Json for dynamic parsing

        string Name = jsonObject.ContainsKey("name") ? jsonObject.name : null; // Handle missing properties
        string BIC = jsonObject.ContainsKey("bic") ? jsonObject.bic : null;
        string Country = jsonObject.ContainsKey("country") ? jsonObject.country : null;

        var bank = new Bank(Name, BIC, Country);

        return bank;
    }
}

