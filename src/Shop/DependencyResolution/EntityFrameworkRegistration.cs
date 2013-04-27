using System;
using Shop.Domain.Model;
using Shop.EntityFramework;
using StructureMap.Configuration.DSL;

namespace Shop.DependencyResolution
{
    public class EntityFrameworkRegistration : Registry
    {
        public EntityFrameworkRegistration()
        {
            For<IUnitOfWorkScope>()
                .Use(context => context.GetInstance<UnitOfWorkScope<ProductModelContext>>());

            For<IDbContextScope>()
                .Use(context => context.GetInstance<UnitOfWorkScope<ProductModelContext>>());

            For<UnitOfWorkScope<ProductModelContext>>()
                .HttpContextScoped()
                .Use<UnitOfWorkScope<ProductModelContext>>();
            
            For<IProductModelContext>()
                .Use(context =>
                    {
                        Func<IProductModelContext> dbProductFactory =
                            () => context.GetInstance<UnitOfWorkScope<ProductModelContext>>().DbContext;
                        return LazyInitalizationProxy<IProductModelContext>.Create(dbProductFactory);
                    });
        }
    }
}