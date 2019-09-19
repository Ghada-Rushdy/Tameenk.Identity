using System;
using System.Collections.Generic;
using System.Text;

namespace Tameenk.Identity.Log.DAL
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TKey id);
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(TKey id);
        void Save();
    }
}
