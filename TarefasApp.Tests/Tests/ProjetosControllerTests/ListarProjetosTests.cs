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
    public class ListarProjetosTests
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public ListarProjetosTests()
        {
            //criando uma variável para instanciar e executar a API
            //_client = new WebApplicationFactory<Program>().CreateClient();
            _client = new CustomWebApplicationFactory().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve listar todos os projetos com sucesso")]
        public void Deve_ListarTodosOsProjetosComSucesso()
        {
            for (int i = 0; i < 3; i++)
            {
                var request = new ProjetoRequestDto
                {
                    Nome = _faker.Name.JobTitle(),
                    Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
                };

                _client.PostAsJsonAsync("/api/projetos/criar-projeto", request);
            }

            var response = _client.GetAsync($"/api/projetos/listar-projetos")?.Result;

            response?.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = response?.Content.ReadAsStringAsync()?.Result;

            List<ProjetoResponseDto> projetos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProjetoResponseDto>>(content);

            projetos?.Count.Should().BeGreaterThanOrEqualTo(3);

            foreach (var projeto in projetos)
            {
                projeto?.Id.Should().NotBeEmpty();
                projeto?.Nome.Should().NotBeNullOrEmpty();
                projeto?.Descricao.Should().NotBeNullOrEmpty();

                projeto?.Usuarios.Should().BeNullOrEmpty();
                projeto?.Tarefas.Should().BeNullOrEmpty();
            }
        }
    }
}
