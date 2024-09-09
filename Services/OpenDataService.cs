using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PTANetBackMVC.DTOs;
using PTANetBackMVC.Models;
using System.Collections.Generic;

namespace PTANetBackMVC.Services
{
    public class OpenDataService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public OpenDataService(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // Obtener datos de la API
        public async Task<List<BalanceResponsiblePartyDto>> GetBalanceResponsiblePartiesAsync()
        {
            string url = "https://api.opendata.esett.com/EXP01/BalanceResponsibleParties";

            // Realiza la solicitud GET a la API
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Deserializar el contenido de la respuesta en una lista de DTOs
            var content = await response.Content.ReadAsStringAsync();
            var parties = JsonSerializer.Deserialize<List<BalanceResponsiblePartyDto>>(content);

            return parties;
        }

        // Almacenar datos en la base de datos
        public async Task StoreBalanceResponsiblePartiesAsync(List<BalanceResponsiblePartyDto> partyDtos)
        {
            foreach (var partyDto in partyDtos)
            {
                // Mapear el DTO a la entidad y agregar al contexto
                var partyEntity = new BalanceResponsibleParty
                {
                    BrpCode = partyDto.brpCode,
                    BrpName = partyDto.brpName,
                    BusinessId = partyDto.businessId,
                    CodingScheme = partyDto.codingScheme,
                    Country = partyDto.country,
                    ValidityEnd = partyDto.validityEnd,
                    ValidityStart = partyDto.validityStart
                };

                _context.BalanceResponsibleParties.Add(partyEntity);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}
