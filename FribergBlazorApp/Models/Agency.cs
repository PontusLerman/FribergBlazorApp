﻿namespace FribergBlazorApp.Models
{
    public class Agency
    {
        public int agencyId { get; set; }
        public string agencyName { get; set; }
        public string agencyDescription { get; set; }
        public string agencyLogoURL { get; set; }
        public string[] employees { get; set; }
    }

}