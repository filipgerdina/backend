using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3001") // Add your frontend's origin here
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // This is important for SignalR

        policy.WithOrigins("http://localhost:3002") // Add your frontend's origin here
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // This is important for SignalR
        policy.WithOrigins("http://localhost:3003") // Add your frontend's origin here
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials(); // This is important for SignalR
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigins");

// Map the SignalR Hub endpoint
app.MapHub<MyHub>("/myHub");  // Define your SignalR Hub here

app.Run();

public class MyHub : Hub
{
    public async Task ClearCache(String? dataSource)
    {
        if (dataSource != null)
        {
            await Clients.All.SendAsync("ReceiveClearCacheSignal", dataSource);
        }
        else {
            await Clients.All.SendAsync("ReceiveClearCacheSignal");
        }
    }
}