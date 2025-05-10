namespace MovieProject.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;

        public int ActorId { get; set; }
        public string ActorName { get; set; } = string.Empty;
    }
}
