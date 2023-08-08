﻿using FreeAccount.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FreeAccount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Cria uma nova conta com os dados fornecidos.
        /// </summary>
        /// <param name="request">Os dados para a criação da conta.</param>
        /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
        /// <returns>Um objeto contendo a resposta da criação da conta.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateAccountResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Create(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (!ValidateNif(request.Nif, out IActionResult badNifResult))
                return badNifResult;

            if (request.Saldo < 0)
                return BadRequest("Saldo inválido. O saldo deve ser maior ou igual a 0.");

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
                Message = $"Conta criada com sucesso para {request.Name} - Email : {request.Email}"
            };

            return Ok(createAccountResponse);
        }

        /// <summary>
        /// Obtém os dados de uma conta com base no NIF fornecido.
        /// </summary>
        /// <param name="nif">O NIF da conta a ser recuperada.</param>
        /// <returns>Um objeto contendo os dados da conta.</returns>
        [HttpGet("{nif}")]
        [ProducesResponseType(typeof(AccountDataResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetAccountData(string nif)
        {
            if (!ValidateNif(nif, out IActionResult badNifResult))
                return badNifResult;

            string folderPath = @"C:\FreeAccount\Accounts";
            string filePath = Path.Combine(folderPath, $"{nif}.txt");

            if (!System.IO.File.Exists(filePath))
                return NotFound("A conta não existe.");

            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length < 3)
                return BadRequest("Formato inválido do arquivo.");

            string name = lines[0].Replace("Nome: ", "");
            string email = lines[1].Replace("Email: ", "");
            decimal saldo;
            if (!decimal.TryParse(lines[2].Replace("Saldo: ", ""), out saldo))
                return BadRequest("Formato inválido do saldo.");

            var accountData = new AccountDataResponse
            {
                Nif = nif,
                Name = name,
                Email = email,
                Saldo = saldo
            };

            return Ok(accountData);
        }

        /// <summary>
        /// Atualiza os dados de uma conta com base no NIF fornecido.
        /// </summary>
        /// <param name="nif">O NIF da conta a ser atualizada.</param>
        /// <param name="updateRequest">Os dados atualizados da conta.</param>
        /// <returns>Uma mensagem de sucesso indicando a atualização da conta.</returns>
        [HttpPut("{nif}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult UpdateAccountData(string nif, [FromBody] UpdateAccountRequest updateRequest)
        {
            if (!ValidateNif(nif, out IActionResult badNifResult))
                return badNifResult;

            string folderPath = @"C:\FreeAccount\Accounts";
            string filePath = Path.Combine(folderPath, $"{nif}.txt");

            if (!System.IO.File.Exists(filePath))
                return NotFound("A conta não existe.");

            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length < 3)
                return BadRequest("Formato inválido do arquivo.");

            string name = lines[0].Replace("Nome: ", "");
            string email = lines[1].Replace("Email: ", "");

            if (!string.IsNullOrEmpty(updateRequest.Name))
                name = updateRequest.Name;

            if (!string.IsNullOrEmpty(updateRequest.Email))
                email = updateRequest.Email;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Nome: {name}");
                writer.WriteLine($"Email: {email}");
            }

            return Ok("Dados da conta atualizados com sucesso.");
        }

        private bool ValidateNif(string nif, out IActionResult result)
        {
            if (!nif.All(char.IsDigit) || nif.Length != 9)
            {
                result = BadRequest("Nif inválido. Favor verificar se o Nif possui apenas 9 dígitos numéricos.");
                return false;
            }

            result = null;
            return true;
        }
    }
}
