namespace WebApplication.Model
{
    public class ServiceProvider
    {
        public string ?BspCode { get; set; }
        public string ?BspName { get; set; }
        public string ?Country { get; set; }
        public string ?BusinessId { get; set; }
        public string ?CodingScheme { get; set; }

        public ServiceProvider() { }

        public ServiceProvider(string bspCode, string bspName, string country, string businessId, string codingScheme)
        {
            BspCode = bspCode;
            BspName = bspName;
            Country = country;
            BusinessId = businessId;
            CodingScheme = codingScheme;
        }

    }
}
