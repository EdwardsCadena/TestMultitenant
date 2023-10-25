using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestProject.Api.Middleware;
using TestProject.Core.Interfaces;
using TestProject.Infrastructure.Data;
using TestProject.Infrastructure.Interfaces;
using TestProject.Infrastructure.Repository;
using TestProject.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Conxicon a la base de datos
builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Test"));
});

builder.Services.Configure<TestProject.Infrastructure.Options.PasswordOptions>(builder.Configuration.GetSection("PasswordOptions"));

//Injeccion de dependendecias

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProductRepository, ProductsRepository>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
    };

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var datacontext = scope.ServiceProvider.GetRequiredService<TestContext>();
    datacontext.Database.Migrate();
}    



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CustomUnauthorizedMiddleware>();

app.MapControllers();

app.Run();
