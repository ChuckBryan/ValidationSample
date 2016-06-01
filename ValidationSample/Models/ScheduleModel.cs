using System;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;
using ValidationSample.Validation;

namespace ValidationSample.Models
{
    [Validator(typeof(ScheduleModelValidator))]
    public class ScheduleModel
    {

        public string ScheduleEnd { get; set; }

        public int ProblemTypeId { get; set; }

/*        public bool PropertyB { get; set; }

        public string SomeOtherProperty { get; set; }*/
    }

    public class ScheduleModelValidator : AbstractValidator<ScheduleModel>
    {
        public ScheduleModelValidator()
        {
            //RuleFor(m => m.ScheduleEnd).NotNull().WithMessage("FV - Add Date");

            RuleFor(m => m.ProblemTypeId).GreaterThan(0)
                .WithMessage("FV - Add Problem Type SS")
                .When(model =>
                {
                    DateTime scheduleEndDate;

                    if (DateTime.TryParse(model.ScheduleEnd, out scheduleEndDate))
                    {
                        return scheduleEndDate < DateTime.UtcNow;
                    }

                    return false;
                })
                .SetValidator(new ProblemTypeRequiredIfClientSideValidator("FV - Add Problem Type CS", "ScheduleEnd"));

        }

    }

    
}