using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
 opt.UseInMemoryDatabase("DbMyApi"));

var app = builder.Build();
//metodos principais : get , post , put , delete ,get por id

//retorna todos os produtos do banco 
app.MapGet ("/produto", (AppDbContext db) => {

var todosProdutos = db.Produtos.ToList();

    return Results.Ok(todosProdutos);
});

//insere produto

app.MapPost("/produto", (Produto umProduto, AppDbContext db) => {
   db.Produtos.Add(umProduto);
   db.SaveChanges(); 
    return Results.Created($"/produto/{umProduto.Id}", umProduto);
});

//atualiza produto

app.MapPut("/produto/{id}", (int id, Produto umProduto, AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if (produtoExistente is null)
    {
        return Results.NotFound();
    }

    produtoExistente.Nome = umProduto.Nome;
    produtoExistente.Quantidade = umProduto.Quantidade;
    produtoExistente.Comprado = umProduto.Comprado;
    
    db.SaveChanges();
    return Results.NoContent();
});

//remove um produto

app.MapDelete("/produto/{id}",(int id, 
 AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if (produtoExistente == null)
    {
        return Results.NotFound();
    }  
    db.Produtos.Remove(produtoExistente);
    db.SaveChanges();

    return Results.NoContent();
});

//Obtem por id

app.MapGet("/produto/{id}",(int id, 
 AppDbContext db) => {
    var produtoExistente = db.Produtos.Find(id);

    if (produtoExistente == null)
    {
        return Results.NotFound();
    }  
    return Results.Ok(produtoExistente);
});

app.Run();




/*List<Candidato> candidatos = new List<Candidato>();

 app.MapPost("/Candidato", (Candidato umCandidato) => {
    Candidato candidato = new Candidato();
    candidato.Id = umCandidato.Id;
    candidato.Nome = umCandidato.Nome;
    candidato.Telefone = umCandidato.Telefone;
    candidato.dataDeNascimento = umCandidato.dataDeNascimento;
    candidatos.Add(candidato);
    return Results.Ok(candidato);    
 });

app.MapGet("/Candidato", () => {
    foreach (Candidato candidato in candidatos){
        return Results.Ok (candidatos);
    }
    return Results.NotFound("não possuem candidatos registrados");
});

app.MapGet("/Candidato/{id}",(int id) => {
    foreach (Candidato candidato in candidatos){
        if (id == candidato.Id){
            return Results.Ok(candidato);
        }
    }
    return Results.NotFound("Candidato Inexistente");
});

 app.MapPut("/Candidato/{id}", (int Id, Candidato candidato) => {
    
   var candidatoDB = new Candidato();
     candidatoDB.Id = Id;

    candidatoDB.Nome = candidato.Nome;
    candidatoDB.Telefone = candidato.Telefone;
     candidatoDB.dataDeNascimento = candidato.dataDeNascimento;

     return Results.Ok ("Candidato atualizado!");
 });

 app.MapDelete("/Candidato/{id}", (int Id) => {
    foreach (Candidato candidato in candidatos){
        if (Id == candidato.Id){
            candidatos.Remove(candidato);
            return Results.Ok("Candidato excluído");
        }
    }
    return Results.BadRequest("usuário não encontrado");
 });

app.Run();


public class Candidato 
{
    public int Id {get; set;}
    public string Nome {get; set;}
    public string Telefone {get; set;}
    public string dataDeNascimento {get; set;}
} */

