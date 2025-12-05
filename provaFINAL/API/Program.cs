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

List<IMC> Imcs = new List<IMC>
{
    new IMC { nome = "João Carlos Faria", altura = 1.75, peso = 70.0 },
    new IMC { nome = "Ana Maria da Souza", altura = 1.60, peso = 60.0 }
};

app.MapGet("/", () => "IMC");

//Cadastrar imc
app.MapPost("/api/imc/cadastrar", ([FromBody] IMC imc,
    [FromServices] AppDataContext ctx) =>
{
    IMC? resultado =
        ctx.Imcs.FirstOrDefault(x => x.id == imc.id);
    if (resultado is not null)
    {
        return Results.Conflict("IMC já existente!");
    }
    imc.imc = imc.peso / (imc.altura * imc.altura);
    if (imc.imc < 18.5)
    {
        imc.classificacao = "Magreza";
    }
    else if (imc.imc >= 18.5 && imc.imc < 24.9)
    {
        imc.classificacao = "Normal";
    }
    else if (imc.imc >= 25 && imc.imc < 29.9)
    {
        imc.classificacao = "Sobrepeso";
    }
    else if (imc.imc >= 30 && imc.imc < 34.9)
    {
        imc.classificacao = "Obesidade Grau I";
    }
    else if (imc.imc >= 35 && imc.imc < 39.9)
    {
        imc.classificacao = "Obesidade Grau II";
    }
    else
    {
        imc.classificacao = "Obesidade Grau III";
    }
    ctx.Imcs.Add(imc);
    ctx.SaveChanges();
    return Results.Created("", imc);
});



app.Run();
