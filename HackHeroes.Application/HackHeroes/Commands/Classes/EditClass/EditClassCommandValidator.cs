using FluentValidation;
using HackHeroes.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Classes.EditClass
{
    public class EditClassCommandValidator : AbstractValidator<EditClassCommand>
    {
        public EditClassCommandValidator(IHackHeroesRepository repository)
        {
            RuleFor(c => c.Name)
               .NotEmpty()
               .MaximumLength(4).WithMessage("The \"Name\" field must be 4 characters or fewer!")
               .MinimumLength(2).WithMessage("The length of \"Name\" must be at least 2 characters!")
                .Custom((value, context) =>
                {
                    if (value == null)
                    {
                        return;
                    }
                    var existingclass = repository.GetClassByName(value).Result;
                    if (existingclass != null)
                    {
                        context.AddFailure($"{value} is not unique name for class");
                    }
                });
        }
    }
}
