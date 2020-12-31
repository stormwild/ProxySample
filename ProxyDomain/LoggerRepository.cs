using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ProxyDomain
{
    public class LoggerRepository<T> : IRepository<T>
    {
        private readonly IRepository<T> _decorated;
        private readonly ILog _log;

        public LoggerRepository(IRepository<T> decorated, ILog log)
        {
            _log = log;
            _decorated = decorated;
        }

        public void Add(T entity)
        {
            _log.Info($"Adding {entity}");
            _decorated.Add(entity);
            _log.Info($"Added {entity}");
        }

        public void Delete(T entity)
        {
            _log.Info($"Adding {entity}");
            _decorated.Delete(entity);
            _log.Info($"Added {entity}");
        }

        public void Update(T entity)
        {
            _log.Info($"Adding {entity}");
            _decorated.Update(entity);
            _log.Info($"Added {entity}");
        }

        public IEnumerable<T> GetAll()
        {
            _log.Info($"Getting all entities");
            var result = _decorated.GetAll();
            _log.Info($"Returning {result.Count()} {result.GetType().Name}");
            return result;
        }

        public T GetById(int id)
        {
            _log.Info($"GetById {id}");
            var result = _decorated.GetById(id);
            _log.Info($"Returning {result?.GetType().Name}");
            return result;
        }
    }
}
