using System;
using System.Collections.Generic;
using System.Linq;
using MailApi.Lib.Dto;

namespace MailApi.MailServer
{
    public class Engine
    {
        private const string ERR_DOMAIN_MISSING     = "Domain '{0}' does not exist.";
        private const string ERR_DOMAIN_EXISTS      = "Domain '{0}' already exists.";
        private const string ERR_ACCOUNT_MISSING    = "Account '{0}' does not exist in domain '{1}'.";
        private const string ERR_ACCOUNT_EXISTS     = "Account '{0}' already exists in domain '{1}'.";
        private const string ERR_ALIAS_MISSING      = "Alias '{0}' does not exist in domain '{1}'.";
        private const string ERR_ALIAS_EXISTS       = "Alias '{0}' already exists in domain '{1}'.";
        
        /*
        private readonly hMailServer.IInterfaceApplication _app = new  hMailServer.Application();

        public List<Domain> Domains
        {
            get
            {
                var lis = new List<Domain>();
                for (var i = 0; i < _app.Domains.Count; i++)
                    lis.Add(new Domain { DomainName = _app.Domains[i].Name });
                return lis; 
            }
        }
        
        public void Authenticate(string userName, string password)
        {
            _app.Authenticate(userName, password);
        }
        
        public List<Domain> ListDomains()
        {
            var app = new hMailServer.Application();

            app.Authenticate("Administrator", "Flipper0");
            var lis = new List<Domain>();
            for (var i = 0; i < app.Domains.Count; i++)
                lis.Add(new Domain { DomainName = app.Domains[i].Name });
            return lis;
        }
        */
        private static readonly MailDomains _mailDomains = new MailDomains();
       

        static Engine()
        {
            Refresh();
        }

        public static List<Domain> GetDomains()
        {
            return _mailDomains.Domains;
        }

        public static List<Account> GetAccounts(string domainName)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            return domain.Accounts;
        }

        public static List<Alias> GetAliases(string domainName)
        {
            return (from d in _mailDomains.Domains where d.DomainName == domainName select d.Aliases).FirstOrDefault();
        }


        public static void CreateDomain(string domainName)
        {
            _mailDomains.Domains.Add(new Domain {DomainName = domainName,CatchAllAccountName ="postmaster@" + domainName });
            CreateAccount(domainName,"postmaster@" + domainName);
            CreateAccount(domainName, "sales@" + domainName);
            CreateAccount(domainName, "info@" + domainName);
            CreateAccount(domainName, "noreply@" + domainName);
            CreateAlias(domainName, "abuse" + domainName, "postmaster@" + domainName);
        }

        public static void DeleteDomain(string domainName)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain==null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING,domainName));
            _mailDomains.Domains.Remove(domain);
        }

        public static void CreateAccount(string domainName, string accountName)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            var account = domain.Accounts.FirstOrDefault(o => o.AccountName == accountName);
            if (account != null)
                throw new Exception(string.Format(ERR_ACCOUNT_EXISTS, accountName,domainName));
            domain.Accounts.Add(new Account { AccountName = accountName });
        }

        public static void DeleteAccount(string domainName,string accountName)
        {
            //var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            //if (domain == null)
           //     throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
           // _accounts.Remove(_accounts.FirstOrDefault(o => o.AccountName == accountName));
        }

        public static void CreateAlias(string domainName, string aliasName, string accountName)
        {
          //  var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
          //  if (domain == null)
          //      throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
          //  _accounts.Add(new Account { AccountName = accountName });
        }

        public static void DeleteAlias(string domainName, string aliasName, string accountName)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void ChangePassword(string domainName, string accountName, string oldPassword, string newPassword)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void ForgotPassword(string domainName, string accountName, string oldPassword, string newPassword)
        {
            var domain = _mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void Refresh()
        {
            //********** Update Domains
            for (var i = 1; i < 10; i++)
            {
                var domain = new Domain
                {
                    DomainName = string.Format("testdomain{0:##0}.com", i),
                    Enabled = true
                };
                for (var j = 1; j < 10; j++)
                {
                    var account = new Account
                    {
                        AccountName = string.Format("testaccount{0:#0}@{1}", j, domain.DomainName)
                    };
                    domain.Accounts.Add(account);
                }
                for (var j = 1; j < 10; j++)
                {
                    var alias = new Alias
                    {
                        AliasName = string.Format("testalias{0:#0}@{1}", j, domain.DomainName),
                        AccountName = "postmaster@bigblackspider.com"
                    };
                    domain.Aliases.Add(alias);
                }
                _mailDomains.Domains.Add(domain);
            }
        }

        public static void Commit()
        {
            
        }
    }
}