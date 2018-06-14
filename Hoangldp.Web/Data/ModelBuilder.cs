using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Hoangldp.Core.Web.Data
{
    public abstract class ModelBuilder<TEntity> : IModelBuilder where TEntity : class, IEntity, new()
    {
        private readonly DbModelBuilder _modelBuilder;

        public ModelBuilder(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Build()
        {
            OnModelCreating(_modelBuilder.Entity<TEntity>());
        }

        public abstract void OnModelCreating(EntityTypeConfiguration<TEntity> entityTypeBuilder);
    }
}
