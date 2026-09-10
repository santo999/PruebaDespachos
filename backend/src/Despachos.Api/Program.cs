using Despachos.Application;
using Despachos.Infrastructure;
using Despachos.Api.Middleware;
using Despachos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


const string PoliticaCorsFrontend = "AllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(PoliticaCorsFrontend, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();


app.UseCors(PoliticaCorsFrontend);

app.UseHttpsRedirection();



// app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{

    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

}

app.Run();
