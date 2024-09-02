
using log4net;
using System.Data.SqlClient;
using System.Reflection;
using ServiceProvider = WebApplication.Model.ServiceProvider;

namespace WebApplication.Repository
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {

        private readonly IConfiguration _configuration;
        public static ILog Logger { get; set; }
       
        public ServiceProviderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Logger = LogManager.GetLogger(Assembly.GetExecutingAssembly().GetTypes().First());
            log4net.Config.XmlConfigurator.Configure();
        }

        public async Task<ServiceProvider> GetServiceProviderById(String businessId)
        {
            Logger.Info("GetServiceProviderById");
            ServiceProvider provider = new ServiceProvider();
            try
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("AzureConnection")))              
                    {
                        await cn.OpenAsync();
                        using (SqlCommand cmd = new SqlCommand("Select * from ServiceProviders WHERE BussinesId = @BussinesId", cn))
                        {
                            cmd.Parameters.AddWithValue("@BusinessId", businessId);
                            SqlDataReader dr = await cmd.ExecuteReaderAsync();
                            while (await dr.ReadAsync())
                            {
                                provider.BspCode = dr.GetString(0);
                                provider.BspName = dr.GetString(1);
                                provider.Country = dr.GetString(2);
                                provider.BusinessId = dr.GetString(3);
                                provider.CodingScheme = dr.GetString(4);
                            }
                        }
                    }
                    Logger.Info("Result: \n" + "BspCode: " + provider.BspCode + "\nBspName: " + provider.BspName + "\nCountry: " + provider.Country + "\nBusinessId: " + provider.BusinessId + "\nCodingScheme: " + provider.CodingScheme)
                }
                catch (Exception ex)
                {
                    Logger.Error("Exception: " + ex.Message);
                    Logger.Error("Not found businessId: " + businessId);
                }
            return provider;
        }



        public async Task<List<ServiceProvider>> GetServiceProviders()
        {
            Logger.Info("GetServiceProviders");

            List<ServiceProvider> providers = new List<ServiceProvider>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("AzureConnection")))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Select * from ServiceProviders", cn))
                    {
                        SqlDataReader dr = await cmd.ExecuteReaderAsync();
                        while (await dr.ReadAsync())
                        {
                            ServiceProvider provider = new ServiceProvider();
                            provider.BspCode = dr.GetString(0);
                            provider.BspName = dr.GetString(1);
                            provider.Country = dr.GetString(2);
                            provider.BusinessId = dr.GetString(3);
                            provider.CodingScheme = dr.GetString(4);
                            providers.Add(provider);
                        }
                    }
                }
                Logger.Info("List of providers: " + providers.Count.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error("Exception: " + ex.Message);
            }
            return providers;
        }

    }

}
