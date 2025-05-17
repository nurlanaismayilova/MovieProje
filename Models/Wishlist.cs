namespace MovieProject.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }
        public decimal? Rating;
    }
}
