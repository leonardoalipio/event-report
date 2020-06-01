using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Business.Interfaces;
using ProAgil.Data.Context;

namespace ProAgil.Data.Repositories {
    public class ProAgilRepository<TEntity> : IProAgilRepository<TEntity> where TEntity : class 
    {
        protected readonly ProAgilContext _context;

        public ProAgilRepository (ProAgilContext context) {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add (TEntity entity) {
            _context.Add(entity);
        }

        public void Update (TEntity entity) {
            _context.Update(entity);
        }

        public void Delete (TEntity entity) {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync () {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async void Dispose () {
            await _context.DisposeAsync();
        }
    }
}