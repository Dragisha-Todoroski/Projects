using VideoGameLibraryApp.Helpers.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

DependencyInjectionHelper.InjectDbContext(builder.Services, builder.Configuration);
DependencyInjectionHelper.InjectServices(builder.Services);
DependencyInjectionHelper.InjectRepositories(builder.Services);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
