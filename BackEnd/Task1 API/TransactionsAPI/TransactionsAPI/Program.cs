using Microsoft.OpenApi.Models;
using TransactionsAPI.Data;
using TransactionsAPI.Repositories;

var connectionString = "Data Source=transactions.db;";
TransactionRepository repository = new(connectionString);

// Criar o banco e a tabela, se ainda não existirem
DatabaseInitializer.InitializeDatabase(connectionString);

// var csvFilePath = "Sales.txt"; // Caminho do arquivo
// await CsvImporter.ImportTransactionsFromCsv(csvFilePath, connectionString);

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Transactions API", 
        Version = "v1",
        Description = "API para gerenciar transações financeiras"
    });
});

var app = builder.Build();

// Habilitar o Swagger na aplicação
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transactions API v1");
        c.RoutePrefix = string.Empty; // Deixa o Swagger acessível na raiz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
