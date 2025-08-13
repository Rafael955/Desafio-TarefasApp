using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    public interface IHistoricoRepository
    {
        void Add(Historico historico);

        List<Historico>? GetAll();
    }
}
