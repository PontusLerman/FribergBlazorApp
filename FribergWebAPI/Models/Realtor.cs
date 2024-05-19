
//author: Pontus Lerman, Christian Alp
using Microsoft.AspNetCore.Identity;

namespace FribergWebAPI.Models
{
	public class Realtor : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public Agency Agency { get; set; }
		public string? Picture { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
		public virtual List<Residence> ResidenceList { get; set; }
        public bool Approved { get; set; }
    }
}
