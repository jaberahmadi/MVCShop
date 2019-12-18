using DomainClass;
using System.Data.Entity;


namespace DataLayer
{
    public class MvcShopDbEntitie : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<FactorItem> FactorItems { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Ostan> Province { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Reseller> Resellers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
