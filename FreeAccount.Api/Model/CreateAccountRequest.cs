namespace FreeAccount.Api.Model
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nif { get; set; }
        public decimal Amount { get; set; }

    }
}
