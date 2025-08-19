using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Domain.Interfaces.Repositories
{
    public interface IComentariosRepository
    {
        void Add(Comentario comentario);

        void Update(Comentario comentario);

        void Delete(Guid id);

        List<Comentario>? GetAll();

        Comentario? GetById(Guid? Id);
    }
}
