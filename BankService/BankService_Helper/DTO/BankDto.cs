namespace BankService_Helper.DTO
{
    /// <summary>
    /// DTO class for bank entity
    /// </summary>
    public class BankDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Bic { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
