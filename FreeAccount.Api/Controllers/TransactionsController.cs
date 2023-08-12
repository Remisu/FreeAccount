using FreeAccount.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreeAccount.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly string _folderPath = @"C:\FreeAccount\Accounts";

        [HttpPost("transfer")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult TransferFunds(TransferRequest transferRequest)
        {
            if (!ValidateNif(transferRequest.FromNif, out IActionResult badNifResult1) ||
                !ValidateNif(transferRequest.ToNif, out IActionResult badNifResult2))
            {
                return BadRequest("NIF inválido.");
            }

            if (transferRequest.FromNif == transferRequest.ToNif)
            {
                return BadRequest("Não é permitida transferência para o mesmo NIF.");
            }

            string fromFilePath = Path.Combine(_folderPath, $"{transferRequest.FromNif}.txt");
            string toFilePath = Path.Combine(_folderPath, $"{transferRequest.ToNif}.txt");

            if (!System.IO.File.Exists(fromFilePath))
            {
                return NotFound("Conta de origem não existe.");
            }

            if (!System.IO.File.Exists(toFilePath))
            {
                return NotFound("Conta de destino não existe.");
            }

            string[] fromLines = System.IO.File.ReadAllLines(fromFilePath);
            if (fromLines.Length < 3)
            {
                return BadRequest("Formato inválido do arquivo de origem.");
            }

            string[] toLines = System.IO.File.ReadAllLines(toFilePath);
            if (toLines.Length < 3)
            {
                return BadRequest("Formato inválido do arquivo de destino.");
            }

            if (!decimal.TryParse(fromLines[2].Replace("Saldo: ", ""), out decimal fromSaldo))
            {
                return BadRequest("Formato inválido do saldo de origem.");
            }

            if (!decimal.TryParse(toLines[2].Replace("Saldo: ", ""), out decimal toSaldo))
            {
                return BadRequest("Formato inválido do saldo de destino.");
            }

            decimal amount = transferRequest.Amount;
            if (amount <= 0 || amount > fromSaldo)
            {
                return BadRequest("Valor inválido para transferência.");
            }

            fromSaldo -= amount;
            toSaldo += amount;

            using (StreamWriter writer = new StreamWriter(fromFilePath))
            {
                writer.WriteLine($"Nome: {fromLines[0].Replace("Nome: ", "")}");
                writer.WriteLine($"Email: {fromLines[1].Replace("Email: ", "")}");
                writer.WriteLine($"Saldo: {fromSaldo}");
            }

            using (StreamWriter writer = new StreamWriter(toFilePath))
            {
                writer.WriteLine($"Nome: {toLines[0].Replace("Nome: ", "")}");
                writer.WriteLine($"Email: {toLines[1].Replace("Email: ", "")}");
                writer.WriteLine($"Saldo: {toSaldo}");
            }

            return Ok("Transferência realizada com sucesso.");
        }


        [HttpPost("add-balance")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult AddBalance(AddBalanceRequest addBalanceRequest)
        {
            if (!ValidateNif(addBalanceRequest.Nif, out IActionResult badNifResult))
            {
                return BadRequest("NIF inválido.");
            }

            string filePath = Path.Combine(_folderPath, $"{addBalanceRequest.Nif}.txt");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("A conta não existe.");
            }

            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length < 3)
            {
                return BadRequest("Formato inválido do arquivo.");
            }

            if (!decimal.TryParse(lines[2].Replace("Saldo: ", ""), out decimal currentSaldo))
            {
                return BadRequest("Formato inválido do saldo.");
            }

            currentSaldo += addBalanceRequest.Amount;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Nome: {lines[0].Replace("Nome: ", "")}");
                writer.WriteLine($"Email: {lines[1].Replace("Email: ", "")}");
                writer.WriteLine($"Saldo: {currentSaldo}");
            }

            return Ok("Saldo adicionado com sucesso.");
        }

        private bool ValidateNif(string nif, out IActionResult result)
        {
            if (!nif.All(char.IsDigit) || nif.Length != 9)
            {
                result = BadRequest("NIF inválido. Favor verificar se o NIF possui apenas 9 dígitos numéricos.");
                return false;
            }

            result = null;
            return true;
        }
    }
}
