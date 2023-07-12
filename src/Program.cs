using BEComentarios;
using BEComentarios.Domain;
using BEComentarios.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
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

AddComentariosData(app);

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddComentariosData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    #region "Criar Tabela"

    // Cria a tabela para o modo in memory.
    const string sql = "Create table Comentarios (Id INTEGER primary key autoincrement, Titulo text, Criador text, Texto text, DataCriacao datetime null)";

    var conn = db.Database.GetDbConnection();
    conn.Open();

    var cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.CommandType = System.Data.CommandType.Text;
    cmd.ExecuteNonQuery();

    #endregion

    var comentario1 = new Comentario
    {
        Id = 1,
        Titulo = "Titulo 1",
        Texto = "Texto 1",
        Criador = "Criador 1",
        DataCriacao = DateTime.Now,
    };

    var comentario2 = new Comentario
    {
        Id = 2,
        Titulo = "Titulo 2",
        Texto = "Texto 2",
        Criador = "Criador 2",
        DataCriacao = DateTime.Now,
    };

    var comentario3 = new Comentario
    {
        Id = 3,
        Titulo = "Titulo 3",
        Texto = "Texto 3",
        Criador = "Criador 3",
        DataCriacao = DateTime.Now,
    };

    db.Comentarios.Add(comentario1);
    db.Comentarios.Add(comentario2);
    db.Comentarios.Add(comentario3);

    db.SaveChanges();
}