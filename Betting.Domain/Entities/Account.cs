namespace Betting.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;

        public IList<CasinoWager> CasinoWagers { get; set; } = [];
    }
}
