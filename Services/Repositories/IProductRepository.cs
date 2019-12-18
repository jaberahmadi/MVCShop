using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IProductRepository
    {
        bool Add(Product entity, bool autoSave = true);
        bool Update(Product entity, string imagePath, bool autoSave = true);
        bool Delete(Product entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        Product Find(int id);
        IQueryable<Product> Where(Expression<Func<Product, bool>> predicate);
        IQueryable<Product> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Product, TResult>> selector);
        int GetLastIdentity();
        int Save();


    }
}
