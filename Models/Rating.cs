namespace MovieProject.Models
{
    public class Rating
    {
        public decimal Score { get; set; }
        public string Review { get; set; }
        public int RatingId { get; set; }
        public int MovieId { get; set; }
    }
}
