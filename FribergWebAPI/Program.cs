﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FribergWebAPI.Data;
using FribergWebAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using FribergWebAPI.Data.Repositories;
using FribergWebAPI.Data.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

// Add services to the container.

builder.Services.AddControllers()
				.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
