namespace Betting.Domain
{
    public class CasinoWager
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid TransactionId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public Guid BrandId { get; set; }
        public Guid ExternalReferenceId { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; } = null!;
        public string SessionData { get; set; } = null!;
        public long Duration { get; set; }

        public virtual Game Game { get; set; }
        public virtual Account Account { get; set; }
    }
}
