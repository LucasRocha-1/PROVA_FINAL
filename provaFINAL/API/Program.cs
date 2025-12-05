using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

List<IMC> imcs = new List<IMC>
{
    
};

app.MapGet("/", () => "IMC");

//Cadastrar produto
app.MapPost("/api/imc/cadastrar", ([FromBody] IMC imc,
    [FromServices] AppDataContext ctx) =>
{
    IMC? resultado =
        ctx.imcs.FirstOrDefault(x => x.id == imc.id);
    if (resultado is not null)
    {
        return Results.Conflict("IMC já existente!");
    }
    imc.imc = imc.peso / (imc.altura * imc.altura);
    ctx.imcs.Add(imc);
    ctx.SaveChanges();
    return Results.Created("", imc);
});

app.Run();
