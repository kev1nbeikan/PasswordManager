using Microsoft.EntityFrameworkCore;
using PasswordSaver.DataAccess;
using PasswordSaver.DataAccess.Repositories;
using PasswordSaver.Service;
using PasswordsSaver.Core.Abstractions;
using PasswordsSaver.Core.Abstractions.Infastructure;
using PasswordsSaver.Infastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PasswordsSaverDbContext>(options =>
    options.UseInMemoryDatabase("PasswordsSaver")
);


builder.Services.AddScoped<ISavedPasswordRepository, SavedPasswordRepository>();
builder.Services.AddScoped<IPasswordSaverService, PasswordSaverService>();
builder.Services.AddScoped<IPasswordHasher, MyPasswordHasher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();