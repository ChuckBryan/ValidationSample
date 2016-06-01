using FluentValidation.Validators;

namespace ValidationSample.Validation
{
    public class ProblemTypeRequiredIfClientSideValidator : PropertyValidator
    {
        public string DependentProperty { get; set; }

        public ProblemTypeRequiredIfClientSideValidator(string errorMessage, string dependentProperty)
            : base(errorMessage)
        {
            this.DependentProperty = dependentProperty;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //This is not a server side validation rule. So, should not effect at the server side.  
            return true;
        }
    }
}