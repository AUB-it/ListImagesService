using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var imagePath = builder.Configuration["ImagePath"];
var fileProvider = new PhysicalFileProvider(Path.GetFullPath(imagePath));
var requestPath = new PathString("/images");


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
