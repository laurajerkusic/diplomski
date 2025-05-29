using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Phonebook.BLL.Services;
using Phonebook.DAL.Data;
using Phonebook.DAL.Repositories;
using Phonebook.API;
using Phonebook.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularApp", builder =>
	{
		builder.WithOrigins("http://localhost:4200")
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhonebookDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPhonebookRepository, PhonebookRepository>();
builder.Services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
builder.Services.AddScoped<IRepository<PhoneType>, Repository<PhoneType>>();

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IPhoneNumberService, PhoneNumberService>();
builder.Services.AddScoped<IPhoneTypeService, PhoneTypeService>();

builder.Services.AddAutoMapper(typeof(PhonebookProfile));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});
builder.Services.AddAuthorization();

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

app.Run();