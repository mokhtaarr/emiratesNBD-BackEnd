using DAL.Context;
using DAL.Interfaces;
using DAL.IRepository;
using DAL.User;
using Infrastrucure.Repositories;
using Infrastrucure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// sql Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmiratesContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<EmiratesUser, IdentityRole>().AddEntityFrameworkStores<EmiratesContext>();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.User.AllowedUserNameCharacters = "Ð Ö Õ Ë Þ Ý Û Ú å Î Í Ì Ï Ô Ó í È á Ç Ê ä ã ß Ø Æ Á Ä Ñ áÇ ì É æ Ò Ù  abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
});

var jwt = builder.Configuration.GetSection("Token");



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"])),
            ValidIssuer = jwt["Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    }); 



builder.Services.AddAuthorization();


builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<ITokenService,TokenService>();


builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
