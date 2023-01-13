using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using OnceUpoonABook.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;

//fix of double issues
System.Globalization.CultureInfo customCulture = new CultureInfo("en-US");
customCulture.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = customCulture;
CultureInfo.DefaultThreadCurrentUICulture = customCulture;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("OnceUpoonABookContextConnection") ?? throw new InvalidOperationException("Connection string 'OnceUpoonABookContextConnection' not found.");

var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false)
       .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

//DB Context Configuration
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnectionString")));

//security
builder.Services.AddIdentity<OnceUpoonABookUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDBContext>().AddRoles<IdentityRole>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<IPublisherService,PublisherService>();
builder.Services.AddScoped<IBookService,BookService>();

builder.Services.AddRazorPages();

///stop the null screaming
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

///password modifier

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.ReturnUrlParameter = "/Books";
});

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

//Seed the db
await DBSeed.Seed(app,builder.Configuration);


app.Run();
