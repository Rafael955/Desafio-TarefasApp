using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class ComentariosDomainService(IComentariosRepository comentariosRepository, IUsuariosRepository usuariosRepository, ITarefasRepository tarefasRepository, IHistoricoRepository historicoRepository) : IComentariosDomainService
    {
        public ComentarioTarefaResponseDto AdicionarComentario(ComentarioTarefaRequestDto request)
        {
            var usuarioPesquisado = usuariosRepository.GetById(request.IdUsuario);

            if (usuarioPesquisado == null)
                throw new ApplicationException("O usuário informado não foi encontrado.");

            var tarefaPesquisada = tarefasRepository.GetById(request.IdTarefa);

            if (tarefaPesquisada == null)
                throw new ApplicationException("A tarefa informada não foi encontrada.");

            var comentario = new Comentario
            {
                Id = Guid.NewGuid(),
                Texto = request.Texto,
                IdUsuario = request.IdUsuario,
                IdTarefa = request.IdTarefa,
            };

            comentariosRepository.Add(comentario);

            comentario = comentariosRepository.GetById(comentario.Id);

            if(comentario == null)
                throw new ApplicationException("O comentário não foi encontrado após a inserção.");

            #region Regra de Negócio: Os comentários devem ser registrados no histórico de alterações da tarefa.

            string descricaoHistoricoComentario;

            DefinirHistoricoComentariosTarefa(comentario, out descricaoHistoricoComentario);

            if (!string.IsNullOrEmpty(descricaoHistoricoComentario))
            {
                var historicoAlteracao = new Historico
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = tarefaPesquisada.IdUsuario,
                    IdTarefa = tarefaPesquisada.Id,
                    DataHoraModificacao = DateTime.Now,
                    Descricao = descricaoHistoricoComentario
                };

                historicoRepository.Add(historicoAlteracao);
            }

            #endregion

            return ToResponse(comentario);
        }

        public ComentarioTarefaResponseDto AlterarComentario(Guid? idComentario, ComentarioTarefaRequestDto request)
        {
            var usuarioPesquisado = usuariosRepository.GetById(request.IdUsuario);

            if (usuarioPesquisado == null)
                throw new ApplicationException("O usuário informado não foi encontrado.");

            var tarefaPesquisada = tarefasRepository.GetById(request.IdTarefa);

            if (tarefaPesquisada == null)
                throw new ApplicationException("A tarefa informada não foi encontrada.");

            var comentarioPesquisado = comentariosRepository.GetById(idComentario);

            if (comentarioPesquisado == null)
                throw new ApplicationException("O comentário informado não foi encontrado.");

            comentarioPesquisado.Texto = request.Texto;

            comentariosRepository.Update(comentarioPesquisado);

            var comentarioAlterado = comentariosRepository.GetById(idComentario);

            if (comentarioAlterado == null)
                throw new ApplicationException("O comentário não foi encontrado após a alteração.");

            #region Regra de Negócio: Os comentários devem ser registrados no histórico de alterações da tarefa.

            string descricaoHistoricoComentario;

            DefinirHistoricoComentariosTarefa(comentarioAlterado, out descricaoHistoricoComentario);

            if (!string.IsNullOrEmpty(descricaoHistoricoComentario))
            {
                var historicoAlteracao = new Historico
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = tarefaPesquisada.IdUsuario,
                    IdTarefa = tarefaPesquisada.Id,
                    DataHoraModificacao = DateTime.Now,
                    Descricao = descricaoHistoricoComentario
                };

                historicoRepository.Add(historicoAlteracao);
            }

            #endregion

            return ToResponse(comentarioAlterado);
        }

        public ComentarioTarefaResponseDto RemoverComentario(Guid? idComentario)
        {
            var comentarioPesquisado = comentariosRepository.GetById(idComentario);

            if (comentarioPesquisado == null)
                throw new ApplicationException("O comentário informado não foi encontrado.");

            comentariosRepository.Delete(idComentario.Value);

            #region Regra de Negócio: Os comentários devem ser registrados no histórico de alterações da tarefa.

            string descricaoHistoricoComentario;

            DefinirHistoricoComentariosTarefa(comentarioPesquisado, out descricaoHistoricoComentario);

            if (!string.IsNullOrEmpty(descricaoHistoricoComentario))
            {
                var historicoAlteracao = new Historico
                {
                    Id = Guid.NewGuid(),
                    IdUsuario = comentarioPesquisado.IdUsuario,
                    IdTarefa = comentarioPesquisado.IdTarefa,
                    DataHoraModificacao = DateTime.Now,
                    Descricao = descricaoHistoricoComentario
                };

                historicoRepository.Add(historicoAlteracao);
            }

            #endregion

            return ToResponse(comentarioPesquisado);
        }

        private ComentarioTarefaResponseDto ToResponse(Comentario comentario)
        {
            return new ComentarioTarefaResponseDto
            {
                Id = comentario.Id,
                Texto = comentario.Texto,
                Tarefa = comentario.Tarefa == null ? null : new TarefaResponseDto
                {
                    Id = comentario.Tarefa.Id,
                    DataVencimento = comentario.Tarefa.DataVencimento,
                    Descricao = comentario.Tarefa.Descricao,
                    Prioridade = new
                    {
                        Id = (int)comentario.Tarefa.Prioridade,
                        Descricao = comentario.Tarefa.Prioridade.ToString()
                    },
                    Status = new
                    {
                        Id = (int)comentario.Tarefa.Status,
                        Descricao = comentario.Tarefa.Status.ToString()
                    },
                    Titulo = comentario.Tarefa.Titulo
                },
                Usuario = comentario.Usuario == null ? null : new UsuarioResponseDto
                {
                    Id = comentario.Usuario.Id,
                    NomeUsuario = comentario.Usuario.NomeUsuario,
                    Email = comentario.Usuario.Email,
                    NivelAcesso = new
                    {
                        Id = (int)comentario.Usuario.NivelAcesso,
                        Descricao = comentario.Usuario.NivelAcesso.ToString()
                    },
                }
            };
        }

        private void DefinirHistoricoComentariosTarefa(Comentario comentario, out string descricaoModificacoesTarefa)
        {
            descricaoModificacoesTarefa = string.Empty;
            
            descricaoModificacoesTarefa += $"Tarefa Id: {comentario.Tarefa.Id} |";

            descricaoModificacoesTarefa += $"Nome da Tarefa: {comentario.Tarefa.Titulo} |";
            
            descricaoModificacoesTarefa += $"Usuario Id: {comentario.Usuario.Id} |";

            descricaoModificacoesTarefa += $"Nome da Usuário: {comentario.Usuario.NomeUsuario} |";

            descricaoModificacoesTarefa += $"Comentário: {comentario.Texto} |";
        }
    }
}
