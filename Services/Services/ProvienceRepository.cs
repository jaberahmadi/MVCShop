using DataLayer;
using DomainClass;
using System;
using System.Linq;

namespace WiewModels
{
    public class ProvienceRepository : IDisposable,IOstanRepository
    {
        private MvcShopDbEntitie db = null;

        public ProvienceRepository()
        {
            db = new MvcShopDbEntitie();
        }

        public bool Add(Ostan entity, bool autoSave = true)
        {
            try
            {
                db.Province.Add(entity);
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

        public bool Update(Ostan entity, bool autoSave = true)
        {
            try
            {
                db.Province.Attach(entity);
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

        public bool Delete(Ostan entity, bool autoSave = true)
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
                var entity = db.Province.Find(id);
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

        public Ostan Find(int id)
        {
            try
            {
                return db.Province.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Ostan> Where(System.Linq.Expressions.Expression<Func<Ostan, bool>> predicate)
        {
            try
            {
                return db.Province.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Ostan> Select()
        {
            try
            {
                return db.Province.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Ostan, TResult>> selector)
        {
            try
            {
                return db.Province.Select(selector);
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
                if (db.Province.Any())
                    return db.Province.OrderByDescending(p => p.OstanId).First().OstanId;
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

        ~ProvienceRepository()
        {
            Dispose(false);
        }
    }
}