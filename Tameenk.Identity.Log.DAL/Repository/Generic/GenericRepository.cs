using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Log.DAL
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private LogContext  _context = null;
        private DbSet<TEntity> entity = null;

        public GenericRepository(LogContext _logContext)
        {
            this._context = _logContext;
            entity = _context.Set<TEntity>();
        }

        public virtual void Delete(TKey id)
        {
            TEntity existing = entity.Find(id);
            entity.Remove(existing);
            Save();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return entity.ToList();
        }

        public virtual TEntity GetById(TKey id)
        {
            return entity.Find(id);
        }

        public virtual void Insert(TEntity obj)
        {
            entity.Add(obj);
            Save();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            entity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        
    }
}
