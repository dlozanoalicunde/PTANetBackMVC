namespace PTA.Models
{
    public class DistributionSystemOperator : Audit
    {
        public int Id { get; set; }
        public string CodingScheme { get; set; }
        public string Country { get; set; }
        public string DsoCode { get; set; }
        public string DsoName { get; set; }
    }
}