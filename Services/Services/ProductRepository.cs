using DataLayer;
using DomainClass;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace WiewModels
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MvcShopDbEntitie db = null;

        public ProductRepository()
        {
            db = new MvcShopDbEntitie();
        }

        public bool Add(Product entity, bool autoSave = true)
        {
            try
            {
                db.Products.Add(entity);
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

        public bool Update(Product entity, string imagePath, bool autoSave = true)
        {
            try
            {
                if (imagePath != null)
                {
                    entity.Image = imagePath;
                }
                db.Products.Attach(entity);
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

        public bool Delete(Product entity, bool autoSave = true)
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
                var entity = db.Products.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    bool result = Convert.ToBoolean(db.SaveChanges());
                    if (result)
                    {
                        try
                        {
                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Files\\UploadImages\\" + entity.Image) == true)
                            {
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Files\\UploadImages\\" + entity.Image);
                            }
                        }
                        catch { }
                    }
                    return result;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Product Find(int id)
        {
            try
            {
                return db.Products.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                return db.Products.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> Select()
        {
            try
            {
                return db.Products.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<Product, TResult>> selector)
        {
            try
            {
                return db.Products.Select(selector);
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
                if (db.Products.Any())
                    return db.Products.OrderByDescending(p => p.ProductId).First().ProductId;
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

        ~ProductRepository()
        {
            Dispose(false);
        }
    }
}