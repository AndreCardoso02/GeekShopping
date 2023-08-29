using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
        #region ctor
        public MySqlContext() {}
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {}
        #endregion

        #region DbSets
        public DbSet<Product> Products { get; set; }
        #endregion

    }
}
