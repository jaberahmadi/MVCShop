using DataLayer;
using DomainClass;
using System;
using System.Linq;

namespace WiewModels
{
    public class ResellerRepository : IDisposable, IResellerRepository
    {
        private MvcShopDbEntitie db = null;

        public ResellerRepository()
        {
            db = new MvcShopDbEntitie();
        }

        public bool Add(Reseller entity, bool autoSave = true)
        {
            try
            {
                db.Resellers.Add(entity);
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

        public bool Update(Reseller entity, bool autoSave = true)
        {
            try
            {
                db.Resellers.Attach(entity);
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

        public bool Delete(Reseller entity, bool autoSave = true)
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
                var entity = db.Resellers.Find(id);
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

        public Reseller Find(int id)
        {
            try
            {
                return db.Resellers.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Reseller> Where(System.Linq.Expressions.Expression<Func<Reseller, bool>> predicate)
        {
            try
            {
                return db.Resellers.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Reseller> Select()
        {
            try
            {
                return db.Resellers.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Reseller, TResult>> selector)
        {
            try
            {
                return db.Resellers.Select(selector);
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
                if (db.Resellers.Any())
                    return db.Resellers.OrderByDescending(p => p.ResellerId).First().ResellerId;
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

        ~ResellerRepository()
        {
            Dispose(false);
        }
    }
}