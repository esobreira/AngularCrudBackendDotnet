using BEComentarios;
using BEComentarios.Domain;
using BEComentarios.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//     .AddJsonOptions(opts => new JsonSerializerOptions
//     {
//         PropertyNamingPolicy = new LowerCaseNamingPolicy(),
//         WriteIndented = true,
//         Converters =
//         {
//             new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
//         }
//     });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<ComentarioRepository>();

builder.Services.AddCors(cors =>
    cors.AddPolicy("CorsPolicy", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();