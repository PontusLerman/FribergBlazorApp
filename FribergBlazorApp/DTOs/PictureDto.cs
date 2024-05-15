using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FribergWebAPI.DTOs;

namespace FribergBlazorApp.DTOs
{
    public class PictureDto
    {
		public int Id { get; set; }
		public string Picture { get; set; }
		public int ResidenceId { get; set; }
    }
}