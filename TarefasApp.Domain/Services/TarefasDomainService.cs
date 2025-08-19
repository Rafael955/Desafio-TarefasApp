using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Enums;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class TarefasDomainService(ITarefasRepository tarefasRepository, IProjetosRepository projetosRepository, IHistoricoRepository historicoRepository) : ITarefasDomainService
    {
        public TarefaResponseDto CriarTarefa(TarefaRequestDto request)
        {
            if (projetosRepository.GetById(request.IdProjeto) == null)
                throw new ApplicationException("O projeto não foi encontrado!");

            #region Regra de Negócio: Não será permitido cadastrar mais de 20 Tarefas por Projeto

            if (projetosRepository.VerifyLimitOfTasks(request.IdProjeto))
                throw new ApplicationException("Não será possível cadastrar uma tarefa pois este projeto já alcançou o limite de 20 tarefas! ");

            #endregion

            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Titulo = request.Titulo,
                DataVencimento = request.DataVencimento,
                Descricao = request.Descricao,
                IdProjeto = request.IdProjeto,
                IdUsuario = request.IdUsuario,
                Prioridade = (Prioridade)request.Prioridade,
                Status = (Status)request.Status
            };

            tarefasRepository.Add(tarefa);

            var tarefaCriada = tarefasRepository.GetById(tarefa.Id);

            return ToResponse(tarefaCriada);
        }

        public TarefaResponseDto AlterarTarefa(Guid? IdTarefa, TarefaRequestDto request)
        {
            if (projetosRepository.GetById(request.IdProjeto) == null)
                throw new ApplicationException("O projeto não foi encontrado!");

            var tarefaPesquisada = tarefasRepository.GetById(IdTarefa);

            if (tarefaPesquisada == null)
                throw new ApplicationException("Tarefa não encontrada!");

            #region Regra de Negócio: Não será permitido alterar a Prioridade de uma tarefa depois que ela foi criada

            if ((Prioridade)request.Prioridade != tarefaPesquisada.Prioridade)
                throw new ApplicationException("Não é possível alterar o nível de Prioridade de uma Tarefa! ");

            #endregion

            string descricaoModificacoesTarefa;

            DefinirModificacoesTarefas(tarefaPesquisada, request, out descricaoModificacoesTarefa);

            tarefaPesquisada.Titulo = request.Titulo;
            tarefaPesquisada.DataVencimento = request.DataVencimento;
            tarefaPesquisada.Descricao = request.Descricao;
            tarefaPesquisada.IdProjeto = request.IdProjeto;
            tarefaPesquisada.IdUsuario = request.IdUsuario;
            tarefaPesquisada.Prioridade = (Prioridade)request.Prioridade;
            tarefaPesquisada.Status = (Status)request.Status;

            tarefasRepository.Update(tarefaPesquisada);

            var tarefaAlterada = tarefasRepository.GetById(tarefaPesquisada.Id);

            #region Regra de Negócio: Cada vez que uma tarefa for atualizada (status, detalhes, etc.), a API deve registrar um histórico de alterações para a tarefa.

            if (!string.IsNullOrEmpty(descricaoModificacoesTarefa))
            {
                var historicoAlteracao = new Historico
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = tarefaPesquisada.IdUsuario,
                    DataHoraModificacao = DateTime.Now,
                    Descricao = descricaoModificacoesTarefa
                };

                historicoRepository.Add(historicoAlteracao);
            }

            #endregion

            return ToResponse(tarefaAlterada);
        }


        public TarefaResponseDto ExcluirTarefa(Guid? IdTarefa)
        {
            var tarefaPesquisada = tarefasRepository.GetById(IdTarefa);

            if (tarefaPesquisada == null)
                throw new ApplicationException("Tarefa não encontrada!");

            tarefasRepository.Delete(tarefaPesquisada);

            return ToResponse(tarefaPesquisada);
        }

        public TarefaResponseDto ObterTarefaPorId(Guid? IdTarefa)
        {
            var tarefaSelecionada = tarefasRepository.GetById(IdTarefa);

            if (tarefaSelecionada == null)
                throw new ApplicationException("Tarefa não encontrada!");

            return ToResponse(tarefaSelecionada);
        }

        public List<TarefaResponseDto> ListarTarefas()
        {
            var tarefasCadastradas = tarefasRepository.GetAll();

            List<TarefaResponseDto> tarefasResponse = new List<TarefaResponseDto>();

            foreach (var tarefaCadastrada in tarefasCadastradas)
            {
                tarefasResponse.Add(ToResponse(tarefaCadastrada));
            }

            return tarefasResponse;
        }

        private TarefaResponseDto ToResponse(Tarefa tarefa)
        {
            var _tarefa =  new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Prioridade = (int)tarefa.Prioridade,
                Status = (int)tarefa.Status,
                DataVencimento = tarefa.DataVencimento,
                Projeto = new
                {
                    tarefa.Projeto?.Id,
                    tarefa.Projeto?.Nome
                },
                Usuario = new
                {
                    tarefa.Usuario?.Id,
                    tarefa.Usuario?.NomeUsuario
                },
                Comentarios = new List<ComentarioTarefaResponseDto>()
            };

            if(tarefa.Comentarios != null)
            {
                foreach (var comentario in tarefa.Comentarios)
                {
                    _tarefa.Comentarios?.Add(new ComentarioTarefaResponseDto
                    {
                        Id = comentario.Id.Value,
                        Texto = comentario.Texto
                    });
                }
            }

            return _tarefa;
        }

        private void DefinirModificacoesTarefas(Tarefa tarefaPesquisada, TarefaRequestDto request, out string descricaoModificacoesTarefa)
        {
            descricaoModificacoesTarefa = string.Empty;

            if (tarefaPesquisada.Titulo != request.Titulo)
                descricaoModificacoesTarefa += $"Titulo: De {tarefaPesquisada.Titulo} para {request.Titulo} |";

            if (tarefaPesquisada.DataVencimento != request.DataVencimento)
                descricaoModificacoesTarefa += $"DataVencimento: De {tarefaPesquisada.DataVencimento} para {request.DataVencimento} |";

            if (tarefaPesquisada.Descricao != request.Descricao)
                descricaoModificacoesTarefa += $"Descricao: De {tarefaPesquisada.Descricao} para {request.Descricao} |";

            if (tarefaPesquisada.IdProjeto != request.IdProjeto)
                descricaoModificacoesTarefa += $"IdProjeto: De {tarefaPesquisada.IdProjeto} para {request.IdProjeto} |";

            if (tarefaPesquisada.IdUsuario != request.IdUsuario)
                descricaoModificacoesTarefa += $"IdUsuario: De {tarefaPesquisada.IdUsuario} para {request.IdUsuario} |";

            if (tarefaPesquisada.Prioridade != (Prioridade)request.Prioridade)
                descricaoModificacoesTarefa += $"Prioridade: De {tarefaPesquisada.Prioridade.ToString()} para {(Prioridade)request.Prioridade} |";

            if (tarefaPesquisada.Status != (Status)request.Status)
                descricaoModificacoesTarefa += $"Status: De {tarefaPesquisada.Status} para {request.Status} |";
        }
    }
}
