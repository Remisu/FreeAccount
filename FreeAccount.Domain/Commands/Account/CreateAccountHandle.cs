using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FreeAccount.Domain.Abstractions.Message;
using FreeAccount.Domain.Contract.Account;
using MediatR;

namespace FreeAccount.Domain.Commands.Account
{
    public class CreateAccountHandle : ICommandHandler<CreateAccountCommand, CreateAccountResponse>, IDisposable
    {
        private readonly string _folderPath;

        public CreateAccountHandle(ISender sender)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _folderPath = Path.Combine(documentsPath, "FreeAccount", "Accounts");

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            string folderPath = _folderPath;
            string filePath = Path.Combine(folderPath, $"{request.Nif}.txt");

            if (File.Exists(filePath))
                return new CreateAccountResponse { Message = "A conta já existe." };

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Nome: {request.Name}");
                writer.WriteLine($"Email: {request.Email}");
                writer.WriteLine($"Saldo: {request.Amount}");
            }

            var response = new CreateAccountResponse
            {
                Message = $"Conta criada com sucesso para {request.Name} - Email : {request.Email}"
            };

            return response;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
