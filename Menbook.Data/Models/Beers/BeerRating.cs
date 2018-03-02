namespace Menbook.Data.Models.Beers
{
    public class BeerRating
    {
        public int BeerId { get; set; }

        public Beer Beer { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int Rate { get; set; }
    }
}
