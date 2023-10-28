using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.EditStudent
{
    public class EditStudentCommandValidator : AbstractValidator<EditStudentCommand>
    {
        public EditStudentCommandValidator()
        {

            Regex regex = new Regex("^[a-zA-Z]+$");          
            RuleFor(s => s.FirstName)                       
                .NotEmpty()
                .MinimumLength(2)
                .Custom((value, context) =>
                {
                    if (!regex.IsMatch(value) || value.Contains(" ")) 
                    {
                        context.AddFailure("The firstname can't include numbers,white signs and special signs!");

                    }
                })
                .MaximumLength(20);

            RuleFor(s => s.LastName)
              .MinimumLength(2)
              .MaximumLength(20)
              .Custom((value, context) =>
              {
                  if (!regex.IsMatch(value) || value.Contains(" "))  
                  {
                      context.AddFailure("The firstname can't include numbers,white signs and special signs!");

                  }
              })
              .NotEmpty();
        }
    }
}
