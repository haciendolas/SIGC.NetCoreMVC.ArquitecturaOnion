using Microsoft.Extensions.FileProviders;
using SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Services.TaxService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.EstablishmentService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.WarehouseService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ActiveIngredientService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.AttributeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.BrandService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPresentationService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ManufacturerService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PharmaceuticalFormService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PrescriptionTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PresentationService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PriceTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.TherapeuticActionService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.UnitMeasureService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.ConstantService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UbigeoService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserCompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserService;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;
using SIGC.Presentation.AspNetCoreMVC.Services.AuthService; 
using SIGC.Presentation.AspNetCoreMVC.Services.RolePermissionService;
 

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
builder.Services.AddScoped<AccessTokenHandler>();
builder.Services.AddScoped<IApiService,ApiService>();
builder.Services.AddScoped<IApiServiceFactory,ApiServiceFactory>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IPageCompanyService, PageCompanyService>();
builder.Services.AddScoped<ICompanyService,CompanyService>();
builder.Services.AddScoped<IUbigeoService,UbigeoService>();
builder.Services.AddScoped<IConstantService,ConstantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCompanyService, UserCompanyService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<SIGC.Presentation.AspNetCoreMVC.Services.EstablishmentService.IEstablishmentService, SIGC.Presentation.AspNetCoreMVC.Services.EstablishmentService.EstablishmentService>();
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IPharmaceuticalFormService, PharmaceuticalFormService>();
builder.Services.AddScoped<IActiveIngredientService, ActiveIngredientService>();
builder.Services.AddScoped<IPrescriptionTypeService, PrescriptionTypeService>();
builder.Services.AddScoped<ITherapeuticActionService, TherapeuticActionService>();
builder.Services.AddScoped<IUnitMeasureService, UnitMeasureService>();
builder.Services.AddScoped<IPriceTypeService, PriceTypeService>();
builder.Services.AddScoped<IPresentationService, PresentationService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddScoped<ICatalogPresentationService, CatalogPresentationService>();
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

// Sirve todos los Scripts dentro de todas las Areas
var areasPath = Path.Combine(Directory.GetCurrentDirectory(), "Areas");
if (Directory.Exists(areasPath))
{
    foreach (var areaDir in Directory.GetDirectories(areasPath))
    {
        var areaName = Path.GetFileName(areaDir);
        var scriptsDir = Path.Combine(areaDir, "Scripts");

        if (Directory.Exists(scriptsDir))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(scriptsDir),
                //RequestPath = $"/Areas/{areaName}/Scripts"   //En la vista debe agregarse asi <script type="text/javascript" src="~/Areas/Product/Scripts/ProductIndex.js?v=1.0"></script>
                RequestPath = "/" + areaName + "/Scripts"  //En la vista debe agregarse asi <script type="text/javascript" src="~/Product/Scripts/ProductIndex.js?v=1.0"></script>
            });
        }
    }
}

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
