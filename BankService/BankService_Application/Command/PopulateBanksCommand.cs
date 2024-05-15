namespace BankService_Application.Command
{
    using MediatR;

    public class PopulateBanksCommand : IRequest<bool>
    {
        public string BanksApiSource { get; }

        public string EndpointPath { get; }

        public PopulateBanksCommand(string apiSource, string path)
        { 
            BanksApiSource = apiSource;
            EndpointPath = path;
        }
}
}
