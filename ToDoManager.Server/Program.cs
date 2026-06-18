using Microsoft.EntityFrameworkCore;
using ToDoManager.Persistence;
using ToDoManager.Persistence.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// controllers
builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// interceptor
builder.Services.AddScoped<EntityInterceptor>();

// database
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

    options.AddInterceptors(sp.GetRequiredService<EntityInterceptor>());

    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

// services TODO

var app = builder.Build();

// swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// db migration and seeding by startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();

    var seeder = new Seeder(dbContext);
    seeder.Seed();
}

app.Run();

// integration test
public partial class Program
{

}