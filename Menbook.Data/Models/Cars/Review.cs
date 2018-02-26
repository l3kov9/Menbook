namespace Menbook.Data.Models.Cars
{
    public class Review
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public string Text { get; set; }
    }
}
