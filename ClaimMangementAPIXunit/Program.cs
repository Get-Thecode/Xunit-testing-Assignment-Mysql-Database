
using ClaimsManagementApi.Data;
using ClaimsManagementApi.Repositories;
using ClaimsManagementApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Retrieve AWS settings from appsettings.json
//var awsOptions = builder.Configuration.GetSection("AWS");

//// Use BasicAWSCredentials for AWS S3
//var credentials = new BasicAWSCredentials(
//    awsOptions["AccessKey"],
//    awsOptions["SecretKey"]
//);

//// Correctly retrieve the region; ensure it's not null or empty
//RegionEndpoint region = !string.IsNullOrEmpty(awsOptions["Region"])
//    ? RegionEndpoint.GetBySystemName(awsOptions["Region"])
//    : RegionEndpoint.EUNorth1; // Default region, adjust as necessary

//// Register AmazonS3 client with the specified credentials and region
//builder.Services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(credentials, region));

//// Register S3Service for dependency injection
//builder.Services.AddSingleton<S3Service>();

// Configure Entity Framework Core with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Register repositories and services
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();

// Configure Swagger/OpenAPI
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
