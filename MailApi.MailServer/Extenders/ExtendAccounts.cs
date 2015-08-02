using System.Collections.Generic;
using System.Linq;
using hMailServer;

namespace MailApi.MailServer.Extenders
{
    public static class ExtendAccounts
    {
        public delegate bool Filter(Account account);

        public static IEnumerable<Account> List(this Accounts accounts)
        {
            var index = 0;
            while (index < accounts.Count)
            {
                yield return accounts[index++];
            }
        }

        public static IEnumerable<Account> List(this Accounts accounts, Filter filter)
        {
            return accounts.List().Where(domain => filter(domain));
        }

        public static bool Exists(this Accounts accounts, string name)
        {
            try
            {
                return (accounts.ItemByAddress[name] != null);
            }
            catch
            {
                return false;
            }
        }
    }
}