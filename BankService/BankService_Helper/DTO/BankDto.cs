namespace BankService_Helper.DTO
{
    public class BankDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Bic { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
