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
                .WithMessage("O NIF não pode estar vazio.")
                .Length(9)
                .WithMessage("O NIF deve conter exatamente 9 caracteres.")
                .Matches("^[0-9]*$")
                .WithMessage("O NIF deve conter apenas números.");


            RuleFor(a => a.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Saldo inválido. O saldo deve ser maior ou igual a 0.");

            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("O NIF não pode estar vazio.")
                .EmailAddress()
                .WithMessage("Email inválido.");

        }
    }
}

