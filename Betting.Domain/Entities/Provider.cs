namespace Betting.Domain
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public IList<Game> Games { get; set; } = [];
    }
}
