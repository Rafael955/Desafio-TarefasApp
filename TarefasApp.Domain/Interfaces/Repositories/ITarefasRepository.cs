using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Dtos.Requests;
using TarefasApp.Domain.Dtos.Responses;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    public interface ITarefasRepository
    {
        void Add(Tarefa tarefa);

        void Update(Tarefa tarefa);

        void Delete(Tarefa tarefa);

        List<Tarefa>? GetAll();

        Tarefa? GetById(Guid? Id);
    }
}
