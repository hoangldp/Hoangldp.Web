using Hoangldp.Web.Engine;
using Hoangldp.Web.Finder;
using System;
using System.Data.Entity;

namespace Hoangldp.Web.Data
{
    public abstract class DatabaseContext : DbContext, IDataContext
    {
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
