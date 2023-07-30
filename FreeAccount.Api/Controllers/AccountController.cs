using FreeAccount.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace FreeAccount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (!IsValidNif(request.Nif))
                return BadRequest("Nif inválido. Favor verificar se o Nif possui apenas 9 digitos numéricos.");

            string folderPath = @"C:\FreeAccount\Accounts";
            string filePath = Path.Combine(folderPath, $"{request.Nif}.txt");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (System.IO.File.Exists(filePath))
                return BadRequest("A conta já existe.");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Nome: {request.Name}");
                writer.WriteLine($"Email: {request.Email}");
                writer.WriteLine($"Saldo: {request.Saldo}");
            }

            var createAccountResponse = new CreateAccountResponse
            {
                Message = $"Conta criada com sucesso para {request.Name}."
            };

            return Ok(createAccountResponse);
        }

        private bool IsValidNif(string nif)
        {
            if (!nif.All(char.IsDigit))
                return false;

            if (nif.Length != 9)
                return false;

            return true;
        }
    }
}