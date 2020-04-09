using System;
using System.Threading.Tasks;

namespace ProAgil.Business.Interfaces
{
    public interface IProAgilRepository<TEntity> : IDisposable where TEntity : class
    {
        //GERAL
         void Add(TEntity entity);
         void Update(TEntity entity);
         void Delete(TEntity entity);        
         Task<bool> SaveChangesAsync(); 
    }
}