using Hoangldp.Web.Engine;
using Hoangldp.Web.Finder;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Hoangldp.Web.Data
{
    public abstract class DatabaseContext : DbContext, IDataContext
    {
        public DatabaseContext() : base() {}

        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString) {}

        public DatabaseContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) {}

        public DatabaseContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) {}

        public DatabaseContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext) {}

        public DatabaseContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) {}

        public DatabaseContext(DbCompiledModel model) : base(model) {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var listTypeBuilder = EngineContext.Current.Resolve<ITypeFinder>().FindClassesOfType<IModelBuilder>();
            foreach (Type typeBuilder in listTypeBuilder)
            {
                IModelBuilder builder = (IModelBuilder)Activator.CreateInstance(typeBuilder, modelBuilder);
                builder.Build();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
