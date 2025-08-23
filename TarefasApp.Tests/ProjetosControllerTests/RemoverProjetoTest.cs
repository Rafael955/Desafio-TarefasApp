using Bogus;
using Bogus.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Enums;

namespace TarefasApp.Tests.ProjetosControllerTests
{
    public class RemoverProjetoTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public RemoverProjetoTest()
        {
            //criando uma variável para instanciar e executar a API
            _client = new WebApplicationFactory<Program>().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve remover um projeto do sistema com sucesso")]
        public void Deve_RemoverProjetoComSucesso()
        {
            var request = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = response?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            //Removendo o projeto criado
            response = _client.DeleteAsync($"/api/projetos/remover-projeto/{projeto.Id}")?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.OK);

            content = response?.Content.ReadAsStringAsync()?.Result;

            projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            projeto?.Id.Should().NotBeEmpty();
            projeto?.Nome.Should().Be(request.Nome);
            projeto?.Descricao.Should().Be(request.Descricao);
        }

        [Fact(DisplayName = "Deve retornar um erro quando projeto ainda tiver tarefas em aberto")]
        public void Deve_RetornarErro_Quando_ProjetoAindaTiverTarefasEmAberto()
        {
            var request = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = response?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            //Criando Tarefa para Projeto

            var request2 = new TarefaRequestDto
            {
                Titulo = _faker.Name.JobTitle(),
                DataVencimento = DateTime.Now.AddDays(1),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 200),
                IdProjeto = projeto.Id.Value,
                IdUsuario = null,
                Prioridade = (int)Prioridade.ALTA,
                Status = (int)Status.EM_ANDAMENTO
            };

            response = _client.PostAsJsonAsync("/api/tarefas/criar-tarefa", request2)?.Result;
            
            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            //Tentando agora remover o Projeto com a tarefa ainda em andamento

            response = _client.DeleteAsync($"/api/projetos/remover-projeto/{projeto.Id}")?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            content = response?.Content.ReadAsStringAsync()?.Result;

            content.Should().Contain("Não será possível excluir o Projeto pois o mesmo ainda possui Tarefas em aberto! Conclua ou remova estas Tarefas primeiro antes de tentar excluir!");
        }
    }
}
