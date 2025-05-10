using MySql.Data.MySqlClient;
using MovieProject.Models;

namespace MovieProject.Services
{
    public class MovieService
    {
        private readonly string _connectionString;

        public MovieService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string query = "SELECT movie_id, title, release_year, genre_id FROM movies";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                movies.Add(new Movie
                {
                    MovieId = reader.GetInt32("movie_id"),
                    Title = reader.GetString("title"),
                    ReleaseYear = reader.GetInt32("release_year"),
                    GenreId = reader.GetInt32("genre_id")
                });
            }

            return movies;
        }

        public List<Actor> GetAllActors()
        {
            var list = new List<Actor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT actor_id, name FROM actors", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Actor
                {
                    ActorId = reader.GetInt32("actor_id"),
                    Name = reader.GetString("name")
                });
            }

            return list;
        }

        public List<Rating> GetAllRatings()
        {
            var list = new List<Rating>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT rating_id, movie_id, score, review FROM ratings", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Rating
                {
                    RatingId = reader.GetInt32("rating_id"),
                    MovieId = reader.GetInt32("movie_id"),
                    Score = reader.GetDecimal("score"),
                    Review = reader.GetString("review")
                });
            }

            return list;
        }

        public List<Genre> GetAllGenres()
        {
            var list = new List<Genre>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT genre_id, name FROM genres", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Genre
                {
                    GenreId = reader.GetInt32("genre_id"),
                    Name = reader.GetString("name")
                });
            }

            return list;
        }

        public List<MovieActor> GetAllMovieActors()
        {
            var list = new List<MovieActor>();

            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = @"
                    SELECT 
                        ma.movie_id, m.title AS movie_title,
                        ma.actor_id, a.name AS actor_name
                    FROM movie_actors ma
                    JOIN movies m ON ma.movie_id = m.movie_id
                    JOIN actors a ON ma.actor_id = a.actor_id";

                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new MovieActor
                    {
                        MovieId = reader.GetInt32("movie_id"),
                        MovieTitle = reader.GetString("movie_title"),
                        ActorId = reader.GetInt32("actor_id"),
                        ActorName = reader.GetString("actor_name")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching movie-actors: " + ex.Message);
            }

            return list;
        }
    }
}
