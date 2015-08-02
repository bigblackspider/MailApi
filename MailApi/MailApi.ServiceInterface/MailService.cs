using System;
using System.Linq;
using MailApi.MailServer;
using MailApi.ServiceModel;
using ServiceStack;

namespace MailApi.ServiceInterface
{
    public class MailService : Service
    {
        public object Get(domains request)
        {
            if (string.IsNullOrEmpty(request.domainName))
                return Engine.GetDomains();
            return Engine.GetDomains().FirstOrDefault(
                d => string.Equals(d.DomainName.Trim(), request.domainName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase));
        }

        public object Get(Accounts request)
        {
            if (string.IsNullOrEmpty(request.domainName))
                throw new Exception("Domain name missing.");
            return string.IsNullOrEmpty(request.accountName)
                ? Engine.GetAccounts(request.domainName)
                : Engine.GetAccounts(request.domainName).Where(o => o.AccountName == request.accountName).ToList();
        }
    }
}