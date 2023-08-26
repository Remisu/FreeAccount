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
                .Length(9)
                .WithMessage("O NIF deve conter exatamente 9 caracteres.");

            RuleFor(a => a.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Saldo inválido. O saldo deve ser maior ou igual a 0.");

            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email inválido.");

        }
    }
}

