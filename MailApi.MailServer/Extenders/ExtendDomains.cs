using System.Collections.Generic;
using System.Linq;
using hMailServer;

namespace MailApi.MailServer.Extenders
{
    public static class ExtendDomains
    {
        public delegate bool Filter(Domain domain);

        public static IEnumerable<Domain> List(this Domains domains)
        {
            var index = 0;
            while (index < domains.Count)
            {
                yield return domains[index++];
            }
        }

        public static IEnumerable<Domain> List(this Domains domains, Filter filter)
        {
            return domains.List().Where(domain => filter(domain));
        }

        public static bool Exists(this Domains domains, string name)
        {
            try
            {
                return (domains.ItemByName[name] != null);
            }
            catch
            {
                return false;
            }
        }
    }
}