using finefin.api.Data;
using finefin.api.Extensions;
using finefin.api.Http.Filters;
using finefin.api.Http.Middlewares;
using valet.lib.Config;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringHandler()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddData(builder.Configuration);
builder.Services.AddProviders(builder.Configuration);

builder.Services.AddValet<AppDbContext>(builder.Configuration, true)
    .UsePasswordHasher()
    .UseTokenJwt(builder.Configuration)
    .UseValetSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseMiddleware<CultureMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();

public partial class Program
{

}