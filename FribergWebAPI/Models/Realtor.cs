using System.Reflection.Metadata.Ecma335;

namespace FribergWebAPI.Models
{
    //Pontus
    public class Realtor 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Picture { get; set; }
        public Agency Agency { get; set; }
        public virtual List<Residence> ResidenceList { get; set; }
    }
}
