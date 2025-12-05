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

List<IMC> IMCs = new List<IMC>
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
        ctx.IMCs.FirstOrDefault(x => x.id == imc.id);
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
    ctx.IMCs.Add(imc);
    ctx.SaveChanges();
    return Results.Created("", imc);
});

//Listar imcs
app.MapGet("/api/imc/listar", ([FromServices] AppDataContext ctx) =>
{
    //Validar se existe alguma coisa dentro da lista    
    if (ctx.IMCs.Any())
    {
        return Results.Ok(ctx.IMCs.ToList());
    }
    return Results.BadRequest("Lista vazia");
});

//Listar imcs por classificação
app.MapGet("/api/imc/listarporstatus/{classificacao}", ([FromServices] AppDataContext ctx, string classificacao) =>
{
 
    IMC? resultado = ctx.IMCs.Find(classificacao);
    if (resultado is null)
    {
        return Results.NotFound("Nada na classificação");
    }
    return Results.Ok(resultado);
});

app.UseCors("Acesso Total");

app.Run();
