using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace FribergWebAPI.Models
{
    //Pontus
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
