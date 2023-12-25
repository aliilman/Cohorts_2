using Cohorts_1.FakeDependencyInjectionServises;
using Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFakeDbService,FakeDbService>();
builder.Services.AddScoped<FakeDependencyInjection,FakeDependencyInjection>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Projede swagger implementasyonu gerçekleştirilmelidir.
    app.UseSwagger();//swager implemantasyonu
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();
