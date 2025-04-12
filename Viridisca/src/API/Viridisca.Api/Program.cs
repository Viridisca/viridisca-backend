WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // app.ApplyMigrations();
}

// app.UseExceptionHandler();

// app.UseAuthentication();
// app.UseAuthorization();

app.Run();
