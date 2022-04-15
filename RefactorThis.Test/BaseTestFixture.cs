using NUnit.Framework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Product.Repository.Implementors;
using Domain.Product;

namespace RefactorThis.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    public abstract class BaseTestFixture<TContext> : BaseConfigurationTestFixture where TContext : DbContext
    {
        private SqliteConnection _sqliteConnection;
        private string ConnectionString = "DataSource=:memory:";

        protected ProductContext Context { get; set; }
        protected internal ProductRepository Repository => new ProductRepository(Context);

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            _sqliteConnection = new SqliteConnection(ConnectionString);
            SetupDbContext();

            base.OneTimeSetUpBase();
        }

        [SetUp]
        public void SetUpBase()
        {
            _sqliteConnection.Open();
            SetupDbContext();
            PopulateRepository();
            Context.SaveChanges();
        }

        [TearDown]
        public void TearDownBase()
        {
            _sqliteConnection.Close();
        }

        private void SetupDbContext()
        {
            _sqliteConnection.Open();
            var dbOptions = new DbContextOptionsBuilder<ProductContext>().UseSqlite(_sqliteConnection).Options;
            Context = SetupDbContext(dbOptions);
        }

        protected abstract void PopulateRepository();
        protected abstract ProductContext SetupDbContext(DbContextOptions<ProductContext> option);

    }
}
