using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class ProjetosDomainService (IProjetosRepository projetosRepository): IProjetosDomainService
    {
        public ProjetoResponseDto CriarProjeto(ProjetoRequestDto request)
        {
            #region Regra de Negócio: Projetos não podem ter nomes idênticos

            if (projetosRepository.GetByName(request.Nome) != null)
                throw new ApplicationException("Erro: Já existe um projeto com este nome!");

            #endregion

            var projeto = new Projeto
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Descricao = request.Descricao
            };

            projetosRepository.Add(projeto);

            var projetoCriado = projetosRepository.GetById(projeto.Id);

            return ToResponse(projetoCriado);
        }

        public ProjetoResponseDto AlterarProjeto(Guid? idProjeto, ProjetoRequestDto request)
        {
            var projetoPesquisado = projetosRepository.GetByName(request.Nome);

            if(projetoPesquisado == null)
                throw new ApplicationException("Projeto não encontrado!");

            #region Regra de Negócio: Projetos não podem ter nomes idênticos

            if (projetoPesquisado != null && projetoPesquisado.Id != idProjeto && projetoPesquisado.Nome.Equals(request.Nome))
                throw new ApplicationException("Já existe um projeto com este nome!");

            #endregion

            projetoPesquisado.Nome = request.Nome;
            projetoPesquisado.Descricao = request.Descricao;

            projetosRepository.Update(projetoPesquisado);

            var projetoAlterado = projetosRepository.GetById(projetoPesquisado.Id);

            return ToResponse(projetoAlterado);
        }

        public ProjetoResponseDto RemoverProjeto(Guid? idProjeto)
        {
            var projetoPesquisado = projetosRepository.GetById(idProjeto);

            if (projetoPesquisado == null)
                throw new ApplicationException("Projeto não encontrado!");

            #region Regra de Negócio: Não é permitido excluir projetos com Tarefas ainda em aberto

            if (projetoPesquisado != null && projetoPesquisado.Tarefas != null && projetoPesquisado.Tarefas.Any(x => x.Status != Status.CONCLUIDA))
                throw new ApplicationException("Não será possível excluir o Projeto pois o mesmo ainda possui Tarefas em aberto! Conclua ou remova estas Tarefas primeiro antes de tentar excluir!");

            #endregion

            projetosRepository.Delete(projetoPesquisado);

            return ToResponse(projetoPesquisado);
        }

        public ProjetoResponseDto? ObterProjetoPorId(Guid? idProjeto)
        {
            var projetoSelecionado = projetosRepository.GetById(idProjeto);

            if (projetoSelecionado == null)
                throw new ApplicationException("Projeto não encontrado!");

            return ToResponse(projetoSelecionado);
        }

        public List<ProjetoResponseDto>? ListarProjetos()
        {
            var projetosCadastrados = projetosRepository.GetAll();

            List<ProjetoResponseDto> projetosResponse = new List<ProjetoResponseDto>();

            foreach (var projetoCadastrado in projetosCadastrados)
            {
                projetosResponse.Add(ToResponse(projetoCadastrado));
            }

            return projetosResponse;
        }

        private ProjetoResponseDto ToResponse(Projeto projeto)
        {
            var projetoResponse = new ProjetoResponseDto
            {
                Id = projeto.Id,
                Nome = projeto.Nome,
                Descricao = projeto.Descricao
            };

            foreach (var tarefa in projeto.Tarefas)
            {
                projetoResponse.Tarefas?.Add(new TarefaResponseDto
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Prioridade = (int)tarefa.Prioridade, 
                    Status = (int)tarefa.Status,
                    DataVencimento = tarefa.DataVencimento,
                    Projeto = tarefa.Projeto?.Nome ?? "Não Informado",
                    Usuario = tarefa.Usuario?.NomeUsuario ?? "Não Informado"
                });
            }

            return projetoResponse;
        }

    }
}
