using AlicundeTest.Domain.Abstract;
using AlicundeTest.Domain.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AlicundeTest.Persistence.Seeds;

public class InitialSeed
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
    /// <returns></returns>
    public static async Task Seed(AlicundeTestDbContext context, IUnitOfWork unitOfWork)
    {
        var baks = await GetBanksAsync("https://api.opendata.esett.com/EXP06/Banks");

        if (baks != null)
        {
            await context.Banks.AddRangeAsync(baks);
            await unitOfWork.SaveChangesAsync();
        }
    }

    private static async Task<List<Bank>> GetBanksAsync(string url)
    {
        List<Bank> banks = new List<Bank>();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic jsonArray = JsonConvert.DeserializeObject(json);

                foreach (dynamic jsonObject in jsonArray)
                {
                    // Deserialize each JSON object into a Bank object using the chosen method
                    var bank = DeserializeBank(jsonObject.ToString()); // Pass JSON object as a string
                    banks.Add(bank);
                }
            }
        }

        return banks;
    }

    public static Bank DeserializeBank(string json)
    {
        dynamic jsonObject = JsonConvert.DeserializeObject(json); // Use Newtonsoft.Json for dynamic parsing

        var bank = new Bank();
        bank.Name = jsonObject.ContainsKey("name") ? jsonObject.name : null; // Handle missing properties
        bank.BIC = jsonObject.ContainsKey("bic") ? jsonObject.bic : null;
        bank.Country = jsonObject.ContainsKey("country") ? jsonObject.country : null;

        return bank;
    }
}

