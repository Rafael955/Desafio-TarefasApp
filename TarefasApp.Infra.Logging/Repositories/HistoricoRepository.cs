using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Infra.Logging.Contexts;

namespace TarefasApp.Infra.Logging.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly DataContext _context = new DataContext();

        public void Add(Historico historico)
        {
            _context.HistoricoTarefas?.InsertOne(historico);
        }

        public List<Historico>? GetAll()
        {
            var filter = Builders<Historico>.Filter.Empty;
            return _context.HistoricoTarefas.Find(filter).ToList();
        }
    }
}
