namespace Betting.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public string Name { get; set; } = null!;
        public string Theme { get; set; } = null!;

        public Provider? Provider { get; set; }
        public IList<CasinoWager> CasinoWagers { get; set; } = [];
    }
}
