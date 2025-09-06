using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;
using SIGC.Presentation.AspNetCoreMVC.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
 
builder.Services.AddControllersWithViews(option =>
{
    option.Filters.Add<AuthorizationController>();
});
builder.Services.AddResponseCaching();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.Configure<ApiEndpoints>(builder.Configuration.GetSection("ApiEndpoints"));
var endpoints = builder.Configuration.GetSection("ApiEndpoints").Get<ApiEndpoints>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient(ConstantsHelper.HttpClientNames.ApiCommerce360, client => client.BaseAddress = new Uri(endpoints!.Commerce360))
                .AddHttpMessageHandler<AccessTokenHandler>();

builder.Services.AddHttpClient(ConstantsHelper.HttpClientNames.ApiAuth360, client => client.BaseAddress = new Uri(endpoints!.Commerce360)); 

builder.Services.AddScoped<IApiService,ApiService>();
builder.Services.AddScoped<IApiServiceFactory,ApiServiceFactory>();
builder.Services.AddScoped<IAuthService,AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
/*
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (SessionExpiredException)
    {
        context.Response.Redirect("/Login");
    }
});
*/
app.MapControllerRoute(
    name: "areas",
   // pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    pattern: "{area:exists}/{controller}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); 
 
app.Run();
