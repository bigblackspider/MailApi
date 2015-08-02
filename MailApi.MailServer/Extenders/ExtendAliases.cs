using System.Collections.Generic;
using System.Linq;
using hMailServer;

namespace MailApi.MailServer.Extenders
{
    public static class ExtendAliases
    {
        public delegate bool Filter(Alias domain);

        public static IEnumerable<Alias> List(this Aliases domains)
        {
            var index = 0;
            while (index < domains.Count)
            {
                yield return domains[index++];
            }
        }

        public static IEnumerable<Alias> List(this Aliases domains, Filter filter)
        {
            return domains.List().Where(domain => filter(domain));
        }

        public static bool Exists(this Aliases domains, string name)
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