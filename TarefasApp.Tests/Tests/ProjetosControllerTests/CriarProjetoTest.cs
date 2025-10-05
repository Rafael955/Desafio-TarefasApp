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
    public class CriarProjetoTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public CriarProjetoTest()
        {
            //criando uma variável para instanciar e executar a API
            //_client = new WebApplicationFactory<Program>().CreateClient();
            _client = new CustomWebApplicationFactory().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar um projeto com sucesso")]
        public void Deve_CriarUmProjetoComSucesso()
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

            projeto?.Id.Should().NotBeEmpty();
            projeto?.Nome.Should().Be(request.Nome);
            projeto?.Descricao.Should().Be(request.Descricao);
        }

        //Projetos não podem ter nomes idênticos

        [Fact(DisplayName = "Deve retornar um erro quando um projeto de mesmo nome já estiver cadastrado")]
        public void Deve_RetornarErro_Quando_ProjetoComMesmoNomeJaCadastrado()
        {
            var request = new ProjetoRequestDto
            {
                Nome = _faker.Company.CompanyName(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.Created);

            response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = response?.Content.ReadAsStringAsync()?.Result;

            result.Should().Contain("Erro: Já existe um projeto com este nome!");
        }
    }
}
