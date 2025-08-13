using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    public interface IProjetosRepository
    {
        void Add(Projeto projeto);

        void Update(Projeto projeto);

        void Delete(Projeto projeto);

        List<Projeto>? GetAll();

        Projeto? GetById(Guid? Id);

        Projeto? GetByName(string nome);

        bool VerifyLimitOfTasks(Guid? Id);
    }
}
