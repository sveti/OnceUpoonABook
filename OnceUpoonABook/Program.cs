using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using System.Globalization;

//fix of double issues
System.Globalization.CultureInfo customCulture = new CultureInfo("en-US");
customCulture.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = customCulture;
CultureInfo.DefaultThreadCurrentUICulture = customCulture;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false)
       .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

//DB Context Configuration
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<IPublisherService,PublisherService>();
builder.Services.AddScoped<IBookService,BookService>();

///stop the null screaming
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

//Seed the db
DBSeed.Seed(app);

app.Run();
