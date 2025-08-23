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

namespace TarefasApp.Tests.ProjetosControllerTests
{
    public class ObterProjetoPorIdTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public ObterProjetoPorIdTest()
        {
            //criando uma variável para instanciar e executar a API
            _client = new WebApplicationFactory<Program>().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve obter um projeto pelo id informado com sucesso")]
        public void Deve_ObterUmProjetoPeloIdInformadoComSucesso()
        {
            var request = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var response = _client.PostAsJsonAsync("/api/projetos/criar-projeto", request)?.Result;

            var content = response?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            response = _client.GetAsync($"/api/projetos/obter-projeto/{projeto.Id}")?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.OK);

            content = response?.Content.ReadAsStringAsync()?.Result;

            projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(content);

            projeto?.Id.Should().NotBeEmpty();
            projeto?.Nome.Should().Be(request.Nome);
            projeto?.Descricao.Should().Be(request.Descricao);

            projeto?.Usuarios.Should().BeNullOrEmpty();
            projeto?.Tarefas.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "Deve retornar erro ao tentar obter um projeto que não existe")]
        public void Deve_RetornarErroProjetoNaoEncontrado()
        {
            var response = _client.GetAsync("/api/projetos/obter-projeto/00000000-0000-0000-0000-000000000000")?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = response?.Content.ReadAsStringAsync()?.Result;

            content.Should().Contain("Projeto não encontrado!");
        }
    }
}
