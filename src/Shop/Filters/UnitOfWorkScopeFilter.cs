using System;
using System.Web.Mvc;
using Shop.EntityFramework;

namespace Shop.Filters
{
    public class UnitOfWorkScopeFilter : IActionFilter
    {
        private readonly Func<IUnitOfWorkScope> _scopeGetter;

        public UnitOfWorkScopeFilter(Func<IUnitOfWorkScope> scopeGetter)
        {
            _scopeGetter = scopeGetter;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _scopeGetter().OpenUnitOfWork();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null && _scopeGetter().Get().NeedSubmit)
            {
                _scopeGetter().Get().SubmitChanges();
            }
            _scopeGetter().CloseUnitOfWork();
        }
    }
}