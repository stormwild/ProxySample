using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDomain
{
    public class Repository<T> : IRepository<T>
    {
        private readonly IList<T> _data;

        public Repository()
        {
            _data = new List<T>();
        }

        public void Add(T entity)
        {
            _data.Add(entity);
            Console.WriteLine($"Adding {entity}");
        }

        public void Delete(T entity)
        {
            _data.Remove(entity);
            Console.WriteLine($"Delete {entity}");
        }

        public void Update(T entity)
        {
            foreach (var value in _data)
            {
                Type valueType = value.GetType();
                var props = valueType.GetProperties();

                Type entityType = entity.GetType();

                foreach (var prop in props)
                {
                    var entityProp = entityType.GetProperty(prop.Name);

                    if (entityProp == null) continue;
                    
                    var entityValue = entityProp.GetValue(entity);
                    prop.SetValue(value, entityValue);
                }
            }

            Console.WriteLine($"Update {entity}");
        }

        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("GetAll entities");
            return _data;
        }

        public T GetById(int id)
        {
            Console.WriteLine($"GetById entity {id}");
            return _data.FirstOrDefault(e =>
            {
                Type entityType = e.GetType();
                var prop = entityType.GetProperty("Id");

                if (prop == null) return false;

                var entityValue = prop.GetValue(e);
                return entityValue != null && entityValue.Equals(id);

            });
        }
    }
}
