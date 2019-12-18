using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WiewModels
{
    public interface IFactorItemRepository
    {
        bool Add(FactorItem entity, bool autoSave = true);
        bool Update(FactorItem entity, bool autoSave = true);
        bool Delete(FactorItem entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        FactorItem Find(int id);
        IQueryable<FactorItem> Where(Expression<Func<FactorItem, bool>> predicate);
        IQueryable<FactorItem> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<FactorItem, TResult>> selector);
        int GetLastIdentity();
        int Save();
    }
}
