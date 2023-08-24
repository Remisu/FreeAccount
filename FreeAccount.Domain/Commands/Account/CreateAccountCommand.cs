using System;
using FreeAccount.Domain.Abstractions.Message;
using FreeAccount.Domain.Contract.Account;

namespace FreeAccount.Domain.Commands.Account
{
	public class CreateAccountCommand : ICommand<CreateAccountResponse>
	{
        public CreateAccountCommand(string name, string email, string nif, decimal amount)
        {
            Name = name;
            Email = email;
            Nif = nif;
            Amount = amount;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Nif { get; set; }
        public decimal Amount { get; set; }
    }
}

