using System.Data.Entity;
using System.Web.Mvc;
using Shop.Domain;

namespace Shop.Controllers
{
    public abstract class EntityFrameworkController<TDataContext> : Controller
        where TDataContext : DbContext, new()
    {
        private readonly UnitOfWorkScope<TDataContext> _scope;

        protected EntityFrameworkController(IUnitOfWorkScope<TDataContext> scope)
        {
            _scope = (UnitOfWorkScope<TDataContext>) scope;
        }

        public IUnitOfWorkScope<TDataContext> Scope
        {
            get { return _scope; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _scope.OpenUnitOfWork();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.Exception == null)
            {
                _scope.Get().SubmitChanges();
            }
            _scope.CloseUnitOfWork();
        }
    }
}