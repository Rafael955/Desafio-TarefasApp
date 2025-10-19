using Microsoft.EntityFrameworkCore;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;
using TarefasApp.Infra.Data.Contexts;
using TarefasApp.Infra.Data.Repositories;
using TarefasApp.Infra.Logging.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Configuração para que os endpoints da API fiquem em letras minúsculas
builder.Services.AddRouting(map => map.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrando as dependências com o tempo de vida desejado
builder.Services.AddScoped<ITarefasDomainService, TarefasDomainService>();
builder.Services.AddScoped<IProjetosDomainService, ProjetosDomainService>();
builder.Services.AddScoped<IUsuariosDomainService, UsuariosDomainService>();
builder.Services.AddScoped<IComentariosDomainService, ComentariosDomainService>();

builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
builder.Services.AddScoped<IProjetosRepository, ProjetosRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<IComentariosRepository, ComentariosRepository>();
builder.Services.AddScoped<IUsuarioProjetosRepository, UsuarioProjetosRepository>();

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseInMemoryDatabase("InMemoryDbForTesting"));
}
else
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDockerLocalhost")));

    //trocar para quando for usar o Dockerfile para containerizar a API
    //builder.Services.AddDbContext<DataContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDockerSqlServer"))); 
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program() { }
