using System.Collections.Generic;
namespace MailApi.Lib.Dto
{
    public class Domain
    {
        public string DomainName { get; set; }
		public bool Enabled { get; set; }
        public string CatchAllAccountName { get; set; }

       public Domain()
        {
            Enabled = true;
        }
    }
}