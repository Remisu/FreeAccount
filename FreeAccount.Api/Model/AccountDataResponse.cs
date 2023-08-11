namespace FreeAccount.Api.Model
{
    internal class AccountDataResponse
    {
        public string Nif { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
}