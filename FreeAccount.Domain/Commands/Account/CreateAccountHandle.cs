using System;
using FreeAccount.Domain.Abstractions.Message;
using FreeAccount.Domain.Contract.Account;
using MediatR;

namespace FreeAccount.Domain.Commands.Account
{
    public class CreateAccountHandle : ICommandHandler<CreateAccountCommand, CreateAccountResponse>, IDisposable
    {
        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            /*


            if (!IsValidEmail(request.Email))
                return BadRequest("Email inválido.");

            string folderPath = _folderPath;
            string filePath = Path.Combine(folderPath, $"{request.Nif}.txt");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (System.IO.File.Exists(filePath))
                return BadRequest("A conta já existe.");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Nome: {request.Name}");
                writer.WriteLine($"Email: {request.Email}");
                writer.WriteLine($"Saldo: {request.Amount}");
            }

            var createAccountResponse = new CreateAccountResponse
            {
                Message = $"Conta criada com sucesso para {request.Name} - Email : {request.Email}"
            };*/
            var response = new CreateAccountResponse();
            return response;
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

