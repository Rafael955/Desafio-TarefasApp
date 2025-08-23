using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Tests.ProjetosControllerTests
{
    public class AlterarProjetoTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public AlterarProjetoTest()
        {
            //criando uma variável para instanciar e executar a API
            _client = new WebApplicationFactory<Program>().CreateClient();

            //instanciando a biblioteca do Bogus
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve alterar um projeto com sucesso")]
        public void Deve_AlterarUmProjetoComSucesso()
        {

        }
    }
}
