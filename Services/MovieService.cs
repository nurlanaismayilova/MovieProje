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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching movies: " + ex.Message);
            }

            return movies;
        }

        public List<Actor> GetAllActors()
        {
            var list = new List<Actor>();

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching actors: " + ex.Message);
            }

            return list;
        }

        public List<Rating> GetAllRatings()
        {
            var list = new List<Rating>();

            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = "SELECT rating_id, score, review, movie_id FROM ratings";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Rating
                    {
                        RatingId = reader.GetInt32("rating_id"),
                        Score = reader.GetDecimal("score"),
                        Review = reader.GetString("review"),
                        MovieId = reader.GetInt32("movie_id")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ratings: " + ex.Message);
            }

            return list;
        }

        public List<Genre> GetAllGenres()
        {
            var list = new List<Genre>();

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching genres: " + ex.Message);
            }

            return list;
        }
    }
}
