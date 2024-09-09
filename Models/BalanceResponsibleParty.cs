namespace PTANetBackMVC.Models
{
    public class BalanceResponsibleParty
    {
        public int Id { get; set; }  // Clave primaria generada automáticamente por la base de datos
        public string BrpCode { get; set; }  // Código del BRP, obligatorio
        public string BrpName { get; set; }  // Nombre del BRP, obligatorio
        public string? BusinessId { get; set; }  // ID de negocio, puede ser nulo
        public string? CodingScheme { get; set; }  // Esquema de codificación, puede ser nulo
        public string? Country { get; set; }  // País, puede ser nulo
        public string? ValidityEnd { get; set; }  // Fecha de fin de validez, puede ser nulo
        public string? ValidityStart { get; set; }  // Fecha de inicio de validez, puede ser nulo
    }
}
