using FeedbackService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCors", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});


builder.Services.AddDbContext<FeedbackDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHealthChecks();


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Feedback API v1");
    c.RoutePrefix = "swagger"; 
});

app.UseHttpsRedirection();


app.UseCors("OpenCors");

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

