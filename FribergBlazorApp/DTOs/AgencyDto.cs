﻿//author: Christian Alp, Johan Krångh, Pontus Lerman
namespace FribergBlazorApp.DTOs
{
	public class AgencyDto
	{
		public int AgencyId { get; set; }
		public string AgencyName { get; set; }
		public string AgencyDescription { get; set; }
		public string? AgencyLogoURL { get; set; } = "";
	}
}
