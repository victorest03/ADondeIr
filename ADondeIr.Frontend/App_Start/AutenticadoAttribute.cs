namespace ADondeIr.Frontend
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using BusinessLogic;
    using Common.Session;
    using Model;

    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public bool IsAdmin { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                if(!IsAdmin) return;

                var user = (Usuario)SessionHelper.GetUser();
                if(user.isAdmin) return;
            }

            if (SessionHelper.ExistCookieSession())
            {
                var user = new UsuarioBl().Get(int.Parse(SessionHelper.GetCookie()));
                if (IsAdmin)
                {
                    if (user.isAdmin)
                    {
                        SessionHelper.AddUserToSession(user);
                        return;
                    }

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "Index"
                    }));
                }
                else
                {
                    SessionHelper.AddUserToSession(user);
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }

    }
    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession()) return;

            if (!SessionHelper.ExistCookieSession()) return;

            var user = new UsuarioBl().Get(int.Parse(SessionHelper.GetCookie()));

            SessionHelper.AddUserToSession(user);
            
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Dashboard",
                action = "Index"
            }));
        }
    }
}