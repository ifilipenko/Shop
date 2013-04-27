using System.Web.Mvc;
using Shop.EntityFramework;
using Shop.Filters;
using StructureMap;

namespace Shop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UnitOfWorkScopeFilter(ObjectFactory.GetInstance<IUnitOfWorkScope>));
            filters.Add(new HandleErrorAttribute());
        }
    }
}