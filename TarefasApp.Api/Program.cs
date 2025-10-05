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

builder.Services.AddTransient<ITarefasDomainService, TarefasDomainService>();
builder.Services.AddTransient<IProjetosDomainService, ProjetosDomainService>();
builder.Services.AddTransient<IUsuariosDomainService, UsuariosDomainService>();
builder.Services.AddTransient<IComentariosDomainService, ComentariosDomainService>();

builder.Services.AddTransient<ITarefasRepository, TarefasRepository>();
builder.Services.AddTransient<IProjetosRepository, ProjetosRepository>();
builder.Services.AddTransient<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddTransient<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddTransient<IComentariosRepository, ComentariosRepository>();
builder.Services.AddTransient<IUsuarioProjetosRepository, UsuarioProjetosRepository>();

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseInMemoryDatabase("InMemoryDbForTesting"));
}
else
{
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDocker")));
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
