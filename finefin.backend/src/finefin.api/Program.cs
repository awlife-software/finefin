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
builder.Services.AddSwaggerGen();

builder.Services.AddData(builder.Configuration);
builder.Services.AddProviders(builder.Configuration);

builder.Services.AddValet<AppDbContext>(builder.Configuration, true)
    .UsePasswordHasher()
    .UseTokenGenerator(builder.Configuration)
    .UseValetJwt(builder.Configuration)
    .UseValetSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

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

app.MapControllers();

app.Run();
