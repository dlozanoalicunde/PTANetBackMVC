namespace PTANetBackMVC.DTOs
{
    public class BalanceResponsiblePartyDto
    {
        public string brpCode { get; set; }  // Código del BRP (Balance Responsible Party)
        public string brpName { get; set; }  // Nombre del BRP
        public string? businessId { get; set; }  // ID de negocio, puede ser nulo
        public string? codingScheme { get; set; }  // Esquema de codificación, puede ser nulo
        public string? country { get; set; }  // País, puede ser nulo
        public string? validityEnd { get; set; }  // Fecha de fin de validez
        public string? validityStart { get; set; }  // Fecha de inicio de validez
    }
}
