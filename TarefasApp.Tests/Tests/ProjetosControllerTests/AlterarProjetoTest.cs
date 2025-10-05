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
using TarefasApp.Tests.Factories;

namespace TarefasApp.Tests.ProjetosControllerTests
{
    public class AlterarProjetoTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public AlterarProjetoTest()
        {
            // criando uma variável para instanciar e executar a API
            //_client = new WebApplicationFactory<Program>().CreateClient();
            _client = new CustomWebApplicationFactory().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve alterar um projeto com sucesso")]
        public void Deve_AlterarUmProjetoComSucesso()
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

            //Atualizando projeto recem criado
            var requestUpdate = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var responseUpdate = _client.PutAsJsonAsync($"/api/projetos/alterar-projeto/{projeto.Id}", requestUpdate)?.Result;

            responseUpdate?.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentUpdated = responseUpdate?.Content.ReadAsStringAsync()?.Result;

            var projetoUpdated = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(contentUpdated);

            projetoUpdated?.Id.Should().NotBeEmpty();
            projetoUpdated?.Nome.Should().Be(requestUpdate.Nome);
            projetoUpdated?.Descricao.Should().Be(requestUpdate.Descricao);
        }

        [Fact(DisplayName = "Deve retornar um erro quando um projeto de mesmo nome já estiver cadastrado")]
        public void Deve_RetornarErro_Quando_ProjetoComMesmoNomeJaCadastrado()
        {
            var request = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            var secondRequest = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", secondRequest)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = response?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            //Tentando atualizar segundo projeto com o mesmo nome do primeiro
            var requestUpdate = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var responseUpdate = _client.PutAsJsonAsync($"/api/projetos/alterar-projeto/{projeto.Id}", requestUpdate)?.Result;

            responseUpdate?.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = responseUpdate?.Content.ReadAsStringAsync()?.Result;

            result.Should().Contain("Já existe um projeto com este nome!");
        }
    }
}
