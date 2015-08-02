using ServiceStack;

namespace MailApi.ServiceModel
{
    [Route("/domains", "GET")]
    [Route("/domains/{domainName}", "GET")]
    public class Domains : IReturn<object>
    {
        public string domainName { get; set; }
    }

    [Route("/domains/{domainName}/accounts", "GET")]
    [Route("/domains/{domainName}/accounts/{accountName}", "GET")]
    public class Accounts : IReturn<object>
    {
        public string domainName { get; set; }
        public string accountName { get; set; }
    }

    [Route("/domains/{domainName}/aliases", "GET")]
    [Route("/domains/{domainName}/aliases/{alaisName}", "GET")]
    public class Aliases : IReturn<object>
    {
        public string domainName { get; set; }
        public string aliasName { get; set; }
    }
}