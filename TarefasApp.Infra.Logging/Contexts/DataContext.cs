using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Domain.Entities;

namespace TarefasApp.Infra.Logging.Contexts
{
    public class DataContext
    {
        private readonly string Url = "mongodb://admin:desafio2025@localhost:27018/";
        //private readonly string Url = "mongodb://admin:desafio2025@mongodb:27017";
        private readonly string Db = "Historico_Tarefas";

        private readonly IMongoDatabase _mongoDatabase;

        public DataContext()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(Url));
            var mongoClient = new MongoClient(settings);

            _mongoDatabase = mongoClient.GetDatabase(Db);
        }

        public IMongoCollection<Historico>? HistoricoTarefas
        {
            get
            {
                return _mongoDatabase?.GetCollection<Historico>("HistoricoTarefas");
            }
        }
    }
}
