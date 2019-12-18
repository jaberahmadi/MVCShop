using DataLayer;
using DomainClass;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WiewModels
{
    public class CityRepository : IDisposable , ICityRepository
    {
        private MvcShopDbEntitie db = null;

        public CityRepository()
        {
            db = new MvcShopDbEntitie();
        }

        public bool Add(City entity, bool autoSave = true)
        {
            try
            {
                
                db.Cities.Add(entity);
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

        public bool Update(City entity, bool autoSave = true)
        {
            try
            {
                db.Cities.Attach(entity);
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

        public bool Delete(City entity, bool autoSave = true)
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
                var entity = db.Cities.Find(id);
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

        public City Find(int id)
        {
            try
            {

                return db.Cities.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<City> Where(Expression<Func<City, bool>> predicate)
        {
            try
            {
                return db.Cities.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<City> Select()
        {
            try
            {
                return db.Cities.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<City, TResult>> selector)
        {
            try
            {
                return db.Cities.Select(selector);
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
                if (db.Cities.Any())
                    return db.Cities.OrderByDescending(p => p.CityId).First().CityId;
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

        ~CityRepository()
        {
            Dispose(false);
        }
    }
}