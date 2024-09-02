using Microsoft.AspNetCore.Mvc;
using WebApplication.Repository;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly ILogger<ServiceProvidersController> _logger;
        private readonly IServiceProviderRepository _serviceProviderRepository;

        public ServiceProvidersController(ILogger<ServiceProvidersController> logger,
                                IServiceProviderRepository clientRepository)
        {
            _logger = logger;
            _serviceProviderRepository = clientRepository;
        }


        // GET: api/<ServiceProvidersController>
        [HttpGet]
        public async Task<List<ServiceProvider>> GetServiceProviders()
        {
            return await _serviceProviderRepository.GetServiceProviders();
        }

        // GET: api/<ServiceProvidersController>
        [HttpGet]
        public async Task<ServiceProvider> GetServiceProviderById(String businessId)
        {
            return await _serviceProviderRepository.GetServiceProviderById(businessId)
        }

    }
}
