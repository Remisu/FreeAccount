using System.ComponentModel;

namespace FreeAccount.Api.Model
{

        public class UpdateAccountRequest
        {
            [Description("Novo Nome")]
            public string Name { get; set; }

            [Description("novo@example.com")]
            public string Email { get; set; }
        }
}
