using iSpan_final_service.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Cors開放
var MyAllowSpecificOrigins = "AllowAny";
builder.Services.AddCors(options => {
    options.AddPolicy(
    name: MyAllowSpecificOrigins,
    policy => policy.WithOrigins("*").WithHeaders("*").WithMethods("*")
        );
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var WOBConnectingString = builder.Configuration.GetConnectionString("WOB");
builder.Services.AddDbContext<WOBContext>(options =>
    options.UseSqlServer(WOBConnectingString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//cors管線
app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
