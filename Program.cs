using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using AspNetWebApiStarter.Models;
using AspNetWebApiStarter.Libs;
using System.Reflection;
using AspNetWebApiStarter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<DatabaseContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);

builder.Services.AddSingleton<Utils>();

var serviceTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(
      t => t.IsClass && !t.IsAbstract && typeof(IServiceMarker).IsAssignableFrom(t)
    );

foreach (var serviceType in serviceTypes)
{
  builder.Services.AddScoped(serviceType);
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      ValidateLifetime = true,
      ClockSkew = TimeSpan.Zero,
      IssuerSigningKey = signInKey
    };
  });

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", builder =>
  {
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
