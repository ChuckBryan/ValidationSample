using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace ValidationSample.Validation
{
    public class RequiredIfClientSideFluentPropertyValidator : FluentValidationPropertyValidator
    {
        private RequiredIfClientSideValidator RequiredIfClientSideValidator
        {
            get { return (RequiredIfClientSideValidator)Validator; }
        }

        public RequiredIfClientSideFluentPropertyValidator(ModelMetadata metadata,
                                                       ControllerContext controllerContext,
                                                       PropertyRule propertyDescription,
                                                       IPropertyValidator validator)
            : base(metadata, controllerContext, propertyDescription, validator)
        {
            ShouldValidate = false;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!ShouldGenerateClientSideRules()) yield break;

            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            string message = formatter.BuildMessage(RequiredIfClientSideValidator.ErrorMessageSource.GetString());

            var rule = new ModelClientValidationRule()
            {
                ValidationType = "problemtyperequiredif",
                ErrorMessage = message
            };

            string depProp = BuildDependentPropertyId(Metadata, ControllerContext as ViewContext);
            // find the value on the control we depend on;
            // if it's a bool, format it javascript style 
            // (the default is True or False!)
/*
            string targetValue = (RequiredIfClientSideValidator.TargetValue ?? "").ToString();
            if (RequiredIfClientSideValidator.TargetValue.GetType() == typeof(bool))
                targetValue = targetValue.ToLower();
*/

            rule.ValidationParameters.Add("dependentproperty", depProp);
            //rule.ValidationParameters.Add("targetvalue", targetValue);

            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            // build the ID of the property
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(RequiredIfClientSideValidator.DependentProperty);
            // unfortunately this will have the name of the current field appended to the beginning,
            // because the TemplateInfo's context has had this fieldname appended to it. Instead, we
            // want to get the context as though it was one level higher (i.e. outside the current property,
            // which is the containing object (our Person), and hence the same level as the dependent property.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
                // strip it off again
                depProp = depProp.Substring(thisField.Length);
            return depProp;
        }
    }
}