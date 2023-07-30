using FreeAccount.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FreeAccount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountRequest request,CancellationToken cancellationToken)
        {
            if (System.IO.File.Exists($"C:\\FreeAccount\\Accounts\\{request.Nif}.txt"))
                return BadRequest();

            //System.IO.StreamWriter streamWriter PESQUISAR COMO ESCREVER NO FICHEIRO
            //VALIDAR SALDO

            var createAccountResponse = new CreateAccountResponse
            {
                Message = $"O nome é {request.Name} e o email  {request.Email}"
            };

            return Ok(createAccountResponse);
        }
    }
}
