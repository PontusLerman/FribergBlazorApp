using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Data;
using System.Text.Json.Serialization;
using FribergWebAPI.Data.Repositories;
using FribergWebAPI.Data.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FribergWebAPI.IdentityData;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using FribergWebAPI.Models;

var MyAllowSpecificOrigins = "myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					policy =>
					{
						policy.WithOrigins("https://localhost:7082")
											.AllowAnyHeader()
											.AllowAnyMethod(); 
					});
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

builder.Services.AddIdentity<Realtor, IdentityRole>(options => 
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
	options.ClientId = "686855374153-fhvpksja8ll0a1v74iofld75fviv63f6.apps.googleusercontent.com";
	options.ClientSecret = "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";
});


/* var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(cfg =>
{
	cfg.RequireHttpsMetadata = false;
	cfg.SaveToken = true;
	cfg.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});  */

// Add services to the container.

builder.Services.AddControllers()
				.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(typeof(Program));

//author: Christian
builder.Services.AddScoped<IResidence, ResidenceRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();

//author: Johan
builder.Services.AddScoped<IAgency, AgencyRepository>();
builder.Services.AddScoped<IMunicipality, MunicipalityRepository>();
builder.Services.AddScoped<IResidencePicture, ResidencePictureRepository>();

//author: Pontus
builder.Services.AddScoped<IRealtor, RealtorRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
