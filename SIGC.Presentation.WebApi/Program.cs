
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SIGC.ApplicationService;
using SIGC.DomainModel.Dtos;
using SIGC.Infrastructure.ADONET.SQLSERVER;
using SIGC.Infrastructure.GeneralService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sg =>
{
    sg.EnableAnnotations();
});

var JWTConfigurationSection = builder.Configuration.GetSection("JWTToken");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    JWTConfigurationSection.Bind(
                        options.TokenValidationParameters);

                    options.TokenValidationParameters
                           .IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(
                    JWTConfigurationSection["SecurityKey"]!));
                });

var storeOptions = builder.Configuration.GetSection("Storage").Get<StorageOptions>();
builder.Services.Configure<LocalOptions>(builder.Configuration.GetSection("Storage:Local"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddSIGCCoreApplicationService();
builder.Services.AddSIGCInfrastructureGeneralService();
builder.Services.AddSIGCInfrastructureADONETSQLSERVER(builder.Configuration,"ConnectionStrings");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
if (storeOptions.UsedLocal())
{ 
    var internalFolder = Path.Combine(app.Environment.ContentRootPath, storeOptions.Local.PhysicalPathBase); 
    Directory.CreateDirectory(internalFolder);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(internalFolder),
        RequestPath = storeOptions.Local.VirtualPathBase
    });
}
 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
