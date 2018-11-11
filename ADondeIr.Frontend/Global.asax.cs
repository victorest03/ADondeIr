

namespace ADondeIr.Frontend
{
    using FluentValidation.Mvc;
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder(typeof(DateTime)));
            FluentValidationModelValidatorProvider.Configure();
        }
    }

    public class DateTimeModelBinder : IModelBinder
    {
        private readonly Type _type;
        public DateTimeModelBinder(Type type)
        {
            _type = type;
        }
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var date = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (string.IsNullOrEmpty(date))
                return Activator.CreateInstance(_type);

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));
            var success = DateTime.TryParse(date, CultureInfo.GetCultureInfo("es-ES"), DateTimeStyles.None, out var dt);

            return success ? dt : Activator.CreateInstance(_type);
        }
    }
}
