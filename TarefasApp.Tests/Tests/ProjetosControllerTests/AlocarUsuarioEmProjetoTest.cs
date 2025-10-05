using Azure;
using Azure.Core;
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
    public class AlocarUsuarioEmProjetoTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public AlocarUsuarioEmProjetoTest()
        {
            // criando uma variável para instanciar e executar a API
            //_client = new CustomWebApplicationFactory<Program>().CreateClient();
            _client = new CustomWebApplicationFactory().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve alocar um usuário em um projeto com sucesso")]
        public void Deve_AlocarUmUsuarioEmUmProjetoComSucesso()
        {
            #region Criando novo projeto

            var requestProjeto = new ProjetoRequestDto
            {
                Nome = _faker.Name.JobTitle(),
                Descricao = _faker.Lorem.Paragraph().ClampLength(10, 255)
            };

            var responseProjeto = _client.PostAsJsonAsync("/api/projetos/criar-projeto", requestProjeto)?.Result;

            var contentProjeto = responseProjeto?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projeto = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(contentProjeto);

            #endregion

            #region Pegando Usuário Padrão Admin

            var responseUsuarios = _client.GetAsync($"/api/usuarios/listar-usuarios")?.Result;

            var contentUsuarios = responseUsuarios?.Content.ReadAsStringAsync()?.Result;

            UsuarioResponseDto usuarioAdmin = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UsuarioResponseDto>>(contentUsuarios).FirstOrDefault(u => u.NivelAcesso.Descricao == "GERENTE");

            #endregion

            #region Alocando usuário no projeto criado

            var request = new AlocarUsuarioEmProjetoRequestDto
            {
                IdUsuario = usuarioAdmin.Id
            };

            var responseAlocar = _client.PostAsJsonAsync($"/api/projetos/{projeto.Id}/alocar-usuario", request)?.Result;

            responseAlocar?.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentProjetoComUsuarioAlocado = responseAlocar?.Content.ReadAsStringAsync()?.Result;

            ProjetoResponseDto projetoComUsuarioAlocado = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjetoResponseDto>(contentProjetoComUsuarioAlocado);

            projetoComUsuarioAlocado?.Id.Should().Be(projeto.Id);
            projetoComUsuarioAlocado?.Nome.Should().Be(projeto.Nome);
            projetoComUsuarioAlocado?.Descricao.Should().Be(projeto.Descricao);

            projetoComUsuarioAlocado?.Usuarios.Should().NotBeNullOrEmpty();
            projetoComUsuarioAlocado?.Usuarios.Count.Should().BeGreaterThanOrEqualTo(1);
            projetoComUsuarioAlocado?.Usuarios.Any(u => u.Id == usuarioAdmin.Id).Should().BeTrue();

            projetoComUsuarioAlocado?.Tarefas.Should().BeNullOrEmpty();

            #endregion
        }
    }
}
