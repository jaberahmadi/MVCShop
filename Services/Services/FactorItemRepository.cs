using DataLayer;
using DomainClass;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WiewModels
{
    public class FactorItemRepository : IDisposable, IFactorItemRepository
    {
        private MvcShopDbEntitie db = null;

        public FactorItemRepository()
        {
            db = new MvcShopDbEntitie();
        }

        public bool Add(FactorItem entity, bool autoSave = true)
        {
            try
            {
                db.FactorItems.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(FactorItem entity, bool autoSave = true)
        {
            try
            {
                db.FactorItems.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(FactorItem entity, bool autoSave = true)
        {
            try
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                var entity = db.FactorItems.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public FactorItem Find(int id)
        {
            try
            {
                return db.FactorItems.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<FactorItem> Where(Expression<Func<FactorItem, bool>> predicate)
        {
            try
            {
                return db.FactorItems.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<FactorItem> Select()
        {
            try
            {
                return db.FactorItems.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<FactorItem, TResult>> selector)
        {
            try
            {
                return db.FactorItems.Select(selector);
            }
            catch
            {
                return null;
            }
        }

        public int GetLastIdentity()
        {
            try
            {
                if (db.FactorItems.Any())
                    return db.FactorItems.OrderByDescending(p => p.FactorItemId).First().FactorItemId;
                else
                    return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int Save()
        {
            try
            {
                return db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }

        ~FactorItemRepository()
        {
            Dispose(false);
        }
    }
}