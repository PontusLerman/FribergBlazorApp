
//author: Pontus Lerman
using Microsoft.AspNetCore.Identity;

namespace FribergWebAPI.Models
{
	public class Realtor : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public Agency Agency { get; set; }
		public string? Picture { get; set; }
		public List<string> Roles { get; set; }
		public virtual List<Residence> ResidenceList { get; set; }
	}
}
