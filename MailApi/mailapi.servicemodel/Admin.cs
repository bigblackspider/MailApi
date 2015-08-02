using System.Collections.Generic;
using System.Xml.Serialization;
using ServiceStack;

namespace MailApi.ServiceModel
{
    [Route("/domains", "GET")]
    [Route("/domains/{domainName}", "GET")]
    public class domains : IReturn<DomainsResponse>
    {
        public string domainName { get; set; }
    }

    public class DomainsResponse
    {
        public List<domains> Domains { get; set; }
    }

    [Route("/domains/{domainName}/accounts", "GET")]
    [Route("/domains/{domainName}/accounts/{accountName}", "GET")]
    public class Accounts : IReturn<AccountsResponse>
    {
        public string domainName { get; set; }
        public string accountName { get; set; }
    }

    public class AccountsResponse
    {
        [return: XmlRoot(ElementName = "Accounts")]
        public List<Accounts> Accounts { get; set; }
    }
}