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

                string query = "SELECT title, release_year FROM movies";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(new Movie
                    {
                        Title = reader.GetString("title"),
                        ReleaseYear = reader.GetInt32("release_year")
                    });
                }
            }
            catch (Exception ex)
            {
                // Optional: log the error, show alert, etc.
                Console.WriteLine("Error fetching movies: " + ex.Message);
            }

            return movies;
        }



        public List<Actor> GetAllActors()
        {
            var list = new List<Actor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT name FROM actors", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Actor { Name = reader.GetString("name") });
            }

            return list;
        }

        public List<Rating> GetAllRatings()
        {
            var list = new List<Rating>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT score, review FROM ratings", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Rating
                {
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

            var cmd = new MySqlCommand("SELECT name FROM genres", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Genre { Name = reader.GetString("name") });
            }

            return list;
        }
    }
}
