using Microsoft.EntityFrameworkCore;
using speezs.API.Configurations;
using speezs.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

BuilderConfiguration.Configure(builder);

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.


//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseSwagger(c =>
//{
//	c.RouteTemplate = "professorniyaniya/swagger/{documentname}/swagger.json";
//});

//app.UseSwaggerUI(c =>
//{
//	c.SwaggerEndpoint("/professorniyaniya/swagger/v1/swagger.json", "My Cool API V1");
//	c.RoutePrefix = "professorniyaniya/swagger";
//});

app.UseCors(builder =>
{
	builder.AllowAnyOrigin()
		   .AllowAnyMethod()
		   .AllowAnyHeader();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
