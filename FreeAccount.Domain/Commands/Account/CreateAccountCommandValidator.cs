using System;
using FluentValidation;

namespace FreeAccount.Domain.Commands.Account
{
	public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
	{
		public CreateAccountCommandValidator()
		{
			RuleFor(a => a.Nif)
				.NotEmpty()
				.MaximumLength(9);

			//todo objeto do create vai ter que ser feito aqui
        }
    }
}

