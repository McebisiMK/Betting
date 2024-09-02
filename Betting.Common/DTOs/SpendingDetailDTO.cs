namespace Betting.Common.DTOs
{
    public class SpendingDetailDTO
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public decimal TotlaAmountSpend { get; set; }
    }
}
