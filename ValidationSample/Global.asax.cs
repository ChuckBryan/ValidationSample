using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using ValidationSample.Validation;

namespace ValidationSample
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            FluentValidationModelValidationFactory requiredIfClientSideValidationFactory =
                (metadata, context, rule, validator) =>
                    new RequiredIfClientSideFluentPropertyValidator(metadata, context, rule, validator);


            FluentValidationModelValidatorProvider.Configure(
                provider =>
                {
                    provider.Add(typeof(RequiredIfClientSideValidator), requiredIfClientSideValidationFactory);
                });
        }
    }
}