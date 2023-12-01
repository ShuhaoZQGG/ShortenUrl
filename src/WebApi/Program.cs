using Microsoft.OpenApi.Models;
using WebApi.Configurations;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ShrinkUrlSettings>(builder.Configuration.GetSection("ShrinkUrlSettings"));
builder.Services.AddTransient<IGenerateUrlService, GenerateUrlService>();
builder.Services.AddControllers(opt => {
  opt.AllowEmptyInputInBodyModelBinding = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shorten Url API", Version = "v1" });
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

app.Run();
