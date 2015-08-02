using System;
using System.Collections.Generic;
using System.Linq;
using hMailServer;
using MailApi.Lib.Dto;
using MailApi.MailServer.Extenders;
using Account = MailApi.Lib.Dto.Account;
using Alias = MailApi.Lib.Dto.Alias;
using MailDomain = hMailServer.Domain;
using Domain = MailApi.Lib.Dto.Domain;



using Settings = MailApi.MailServer.Properties.Settings;

namespace MailApi.MailServer
{
    public class Engine
    {
        private const string ERR_DOMAIN_MISSING = "Domain '{0}' does not exist.";
        private const string ERR_DOMAIN_EXISTS = "Domain '{0}' already exists.";
        private const string ERR_ACCOUNT_MISSING = "Account '{0}' does not exist in domain '{1}'.";
        private const string ERR_ACCOUNT_EXISTS = "Account '{0}' already exists in domain '{1}'.";
        private const string ERR_ALIAS_MISSING = "Alias '{0}' does not exist in domain '{1}'.";
        private const string ERR_ALIAS_EXISTS = "Alias '{0}' already exists in domain '{1}'.";
        
        private static readonly MailDomains mailDomains = new MailDomains();
        private static readonly IInterfaceApplication app = new Application();

       
        static Engine()
        {
            app.Authenticate(Settings.Default.MailAdminUser, Settings.Default.MailAdminPassword);
        }

        public static IEnumerable<Domain> GetDomains()
        {
            return app.Domains.List().Select(mailDomain => new Domain {DomainName = mailDomain.Name,CatchAllAccountName = mailDomain.Postmaster, Enabled = mailDomain.Active});
        }

        public static IEnumerable<Account> GetAccounts(string domainName)
        {
            var mailDomain = app.Domains.List().FirstOrDefault(o => o.Name == domainName);
            if (mailDomain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            return mailDomain.Accounts.List().Select(o => new Account {AccountName = o.Address});
        }

        public static IEnumerable<Alias> GetAliases(string domainName)
        {
            var mailDomain = app.Domains.List().FirstOrDefault(o => o.Name == domainName);
            if (mailDomain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            return mailDomain.Aliases.List().Select(o => new Alias { AliasName = o.Name });
        }

        public static void CreateDomain(string domainName)
        {
            mailDomains.Domains.Add(new Domain {DomainName = domainName, CatchAllAccountName = "admin@" + domainName});
            CreateAccount(domainName, "admin@" + domainName);
            CreateAlias(domainName, "postmaster@" + domainName, "admin@" + domainName);
            CreateAlias(domainName, "sales@" + domainName, "admin@" + domainName);
            CreateAlias(domainName, "info@" + domainName, "admin@" + domainName);
            CreateAlias(domainName, "abuse@" + domainName, "admin@" + domainName);
            CreateAccount(domainName, "noreply@" + domainName, true);
        }

        public static void DeleteDomain(string domainName)
        {
            var domain = mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            mailDomains.Domains.Remove(domain);
        }

        public static void CreateAccount(string domainName, string accountName, bool ignoreMail = false)
        {
           /* var domain = mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
            var account = domain.Accounts.FirstOrDefault(o => o.AccountName == accountName);
            if (account != null)
                throw new Exception(string.Format(ERR_ACCOUNT_EXISTS, accountName, domainName));
            domain.Accounts.Add(new Account {AccountName = accountName, IgnoreMail = ignoreMail});*/
        }

        public static void DeleteAccount(string domainName, string accountName)
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
            var domain = mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void ChangePassword(string domainName, string accountName, string oldPassword, string newPassword)
        {
            var domain = mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void ForgotPassword(string domainName, string accountName, string oldPassword, string newPassword)
        {
            var domain = mailDomains.Domains.FirstOrDefault(o => o.DomainName == domainName);
            if (domain == null)
                throw new Exception(string.Format(ERR_DOMAIN_MISSING, domainName));
        }

        public static void Refresh()
        {
            /*//********** Update Domains
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
                mailDomains.Domains.Add(domain);
            }*/
        }

        public static void Commit()
        {
        } 
    }
}