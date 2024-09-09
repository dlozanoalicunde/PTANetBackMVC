using Microsoft.AspNetCore.Mvc;
using PTANetBackMVC.Services;
using System.Threading.Tasks;

namespace PTANetBackMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenDataController : ControllerBase
    {
        private readonly OpenDataService _openDataService;

        public OpenDataController(OpenDataService openDataService)
        {
            _openDataService = openDataService;
        }

        [HttpGet("balance-responsible-parties")]
        public async Task<IActionResult> GetAndStoreBalanceResponsibleParties()
        {
            // Obtener los datos desde la API externa
            var partyDtos = await _openDataService.GetBalanceResponsiblePartiesAsync();

            // Almacenar los datos en la base de datos
            await _openDataService.StoreBalanceResponsiblePartiesAsync(partyDtos);

            // Retornar un mensaje de éxito
            return Ok("Datos almacenados correctamente");
        }
    }
}
