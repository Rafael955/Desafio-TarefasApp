using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;

namespace TarefasApp.Domain.Services
{
    public class UsuariosDomainService(IUsuariosRepository usuariosRepository) : IUsuariosDomainService
    {
        public UsuarioResponseDto? ObterUsuarioPorId(Guid? idUsuario)
        {
            var usuario = usuariosRepository.GetById(idUsuario);

            if (usuario == null)
                throw new ApplicationException("Usuario não foi encontrado.");

            return ToResponse(usuario);
        }

        public List<UsuarioResponseDto>? ListarUsuarios()
        {
            var usuarios = usuariosRepository.GetAll();

            if (usuarios == null)
                throw new ApplicationException("Nenhum usuario foi encontrado.");

            List<UsuarioResponseDto>? _usuarios = new List<UsuarioResponseDto>();

            foreach (var usuario in usuarios)
            {
                _usuarios.Add(ToResponse(usuario));
            }

            return _usuarios;
        }

        private UsuarioResponseDto ToResponse(Usuario usuario)
        {
            var result = new UsuarioResponseDto
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Senha = usuario.Senha,
                NivelAcesso = new
                {
                    Id = (int)usuario.NivelAcesso,
                    Descricao = usuario.NivelAcesso.ToString()
                },
                Projetos = new List<ProjetoResponseDto>(),
                Tarefas = new List<TarefaResponseDto>(),
                Comentarios = new List<ComentarioTarefaResponseDto>()
            };


            if (usuario.UsuarioProjetos != null)
            {
                foreach (var usuarioProjeto in usuario.UsuarioProjetos)
                {
                    if (usuarioProjeto.Projeto != null)
                    {
                        result.Projetos.Add(new ProjetoResponseDto
                        {
                            Id = usuarioProjeto.Projeto.Id,
                            Nome = usuarioProjeto.Projeto.Nome,
                            Descricao = usuarioProjeto.Projeto.Descricao
                        });
                    }
                }
            }

            if (usuario.Tarefas != null)
            {
                foreach (var tarefa in usuario.Tarefas)
                {
                    result.Tarefas.Add(new TarefaResponseDto
                    {
                        Id = tarefa.Id,
                        Titulo = tarefa.Titulo,
                        Descricao = tarefa.Descricao,
                        Prioridade = new
                        {
                            Id = (int)tarefa.Prioridade,
                            Descricao = tarefa.Prioridade.ToString()
                        },
                        Status = new
                        {
                            Id = (int)tarefa.Status,
                            Descricao = tarefa.Status.ToString()
                        },
                        DataVencimento = tarefa.DataVencimento,
                        IdProjeto = tarefa.IdProjeto
                    });
                }
            }

            if (usuario.Comentarios != null)
            {
                foreach (var comentario in usuario.Comentarios)
                {
                    result.Comentarios.Add(new ComentarioTarefaResponseDto
                    {
                        Id = comentario.Id,
                        Texto = comentario.Texto,
                        IdTarefa = comentario.IdTarefa,
                        IdUsuario = comentario.IdUsuario
                    });
                }
            }

            return result;
        }
    }
}
