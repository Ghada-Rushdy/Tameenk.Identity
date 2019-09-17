using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Tameenk.Identity.DAL
{
    public class BaseDataAccess<TEntity,TKey> where TEntity:class
    {
        protected readonly DbSet<TEntity> entity;
        protected readonly TameenkIdentityDbContext context;

        BaseDataAccess()
        {
            this.context = new TameenkIdentityDbContext();
            entity = context.Set<TEntity>();
        }

        public int Count()
        {
            return entity.Count();
        }

        public List<TEntity> GetAll()
        {
            return entity.ToList();                
        }

        public TEntity GetByToken(string token)
        {
            return entity.Find(token);
        }

        public int Add(TEntity entity)
        {
            context.Add(entity);
            return context.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges();
        }
        public int Remove(TEntity entity)
        {
            this.entity.Remove(entity);
            return context.SaveChanges();
        }
    }
}
