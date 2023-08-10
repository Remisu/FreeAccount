namespace FreeAccount.Api.Model
{
    public class TransferRequest
    {
        public string FromNif { get; set; }
        public string ToNif { get; set; }
        public decimal Amount { get; set; }
    }
}


