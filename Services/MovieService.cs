using MovieProject.Models;
using MySql.Data.MySqlClient;

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

            return movies;
        }
    }
}
