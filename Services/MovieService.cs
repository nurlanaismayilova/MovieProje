using MySql.Data.MySqlClient;
using MovieProject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace MovieProject.Services
{
    public class MovieService
    {
        private readonly string _connectionString;

        public MovieService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Wishlist> GetAllWishlist()
        {
            var wishlist = new List<Wishlist>();
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = "SELECT wishlist_id, user_name, movie_id FROM wishlist";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    wishlist.Add(new Wishlist
                    {
                        WishlistId = reader.GetInt32("wishlist_id"),
                        UserName = reader.GetString("user_name"),
                        MovieId = reader.GetInt32("movie_id")
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching wishlist: " + ex.Message);
            }

            return wishlist;
        }
        public void AddWishlist(Wishlist wishlist)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string query = "INSERT INTO wishlist (user_name, movie_id) VALUES (@userName, @movieId)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@userName", wishlist.UserName);
            cmd.Parameters.AddWithValue("@movieId", wishlist.MovieId);

            cmd.ExecuteNonQuery();
        }


        public Wishlist GetWishlistById(int id)
        {
            Wishlist wishlist = null;
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = "SELECT wishlist_id, user_name, movie_id FROM wishlist WHERE wishlist_id = @id";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    wishlist = new Wishlist
                    {
                        WishlistId = reader.GetInt32("wishlist_id"),
                        UserName = reader.GetString("user_name"),
                        MovieId = reader.GetInt32("movie_id")
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching wishlist by ID: " + ex.Message);
            }

            return wishlist;
        }

        public void UpdateWishlist(Wishlist wishlist)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = "UPDATE wishlist SET user_name = @userName, movie_id = @movieId WHERE wishlist_id = @id";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", wishlist.UserName);
                cmd.Parameters.AddWithValue("@movieId", wishlist.MovieId);
                cmd.Parameters.AddWithValue("@id", wishlist.WishlistId);

                Console.WriteLine($"Executing query: {query}");
                Console.WriteLine($"@userName = {wishlist.UserName}, @movieId = {wishlist.MovieId}, @id = {wishlist.WishlistId}");

                int affectedRows = cmd.ExecuteNonQuery();

                Console.WriteLine($"Rows affected: {affectedRows}");

                if (affectedRows > 0)
                {
                    Console.WriteLine("Wishlist updated successfully!");
                }
                else
                {
                    Console.WriteLine("No rows affected. Possible issue with the query or data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating wishlist: " + ex.Message);
            }
        }


        public void DeleteWishlist(int id)
        {
            try
            {
                using var conn = new MySqlConnection(_connectionString);
                conn.Open();

                string query = "DELETE FROM wishlist WHERE wishlist_id = @id";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting wishlist: " + ex.Message);
            }
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
            var actors = new List<Actor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT actor_id, name FROM actors", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                actors.Add(new Actor
                {
                    ActorId = reader.GetInt32("actor_id"),
                    Name = reader.GetString("name")
                });
            }

            return actors;
        }

        public List<Genre> GetAllGenres()
        {
            var genres = new List<Genre>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT genre_id, name FROM genres", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                genres.Add(new Genre
                {
                    GenreId = reader.GetInt32("genre_id"),
                    Name = reader.GetString("name")
                });
            }

            return genres;
        }

        public List<Rating> GetAllRatings()
        {
            var ratings = new List<Rating>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT rating_id, movie_id, score, review FROM ratings", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ratings.Add(new Rating
                {
                    RatingId = reader.GetInt32("rating_id"),
                    MovieId = reader.GetInt32("movie_id"),
                    Score = reader.GetDecimal("score"),
                    Review = reader.GetString("review")
                });
            }

            return ratings;
        }

        public List<MovieActor> GetAllMovieActors()
        {
            var movieActors = new List<MovieActor>();

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string query = "SELECT movie_id, actor_id FROM movie_actors";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                movieActors.Add(new MovieActor
                {
                    MovieId = reader.GetInt32("movie_id"),
                    ActorId = reader.GetInt32("actor_id")
                });
            }

            return movieActors;
        }

        public List<Movie> SearchMovies(string query)
        {
            var movies = new List<Movie>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT movie_id, title, release_year, genre_id FROM movies 
                   WHERE movie_id LIKE @q 
                      OR title LIKE @q 
                      OR release_year LIKE @q 
                      OR genre_id LIKE @q";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", $"%{query}%");

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

        public List<Actor> SearchActors(string query)
        {
            var actors = new List<Actor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT actor_id, name FROM actors 
                   WHERE actor_id LIKE @q 
                      OR name LIKE @q";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", $"%{query}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                actors.Add(new Actor
                {
                    ActorId = reader.GetInt32("actor_id"),
                    Name = reader.GetString("name")
                });
            }
            return actors;
        }

        public List<Genre> SearchGenres(string query)
        {
            var genres = new List<Genre>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT genre_id, name FROM genres 
                   WHERE genre_id LIKE @q 
                      OR name LIKE @q";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", $"%{query}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                genres.Add(new Genre
                {
                    GenreId = reader.GetInt32("genre_id"),
                    Name = reader.GetString("name")
                });
            }
            return genres;
        }

        public List<Rating> SearchRatings(string query)
        {
            var ratings = new List<Rating>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT rating_id, movie_id, score, review FROM ratings 
                   WHERE rating_id LIKE @q 
                      OR movie_id LIKE @q 
                      OR score LIKE @q 
                      OR review LIKE @q";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", $"%{query}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ratings.Add(new Rating
                {
                    RatingId = reader.GetInt32("rating_id"),
                    MovieId = reader.GetInt32("movie_id"),
                    Score = reader.GetDecimal("score"),
                    Review = reader.GetString("review")
                });
            }
            return ratings;
        }

        public List<MovieActor> SearchMovieActors(string query)
        {
            var movieActors = new List<MovieActor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT movie_id, actor_id FROM movie_actors 
                   WHERE movie_id LIKE @q 
                      OR actor_id LIKE @q";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", $"%{query}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movieActors.Add(new MovieActor
                {
                    MovieId = reader.GetInt32("movie_id"),
                    ActorId = reader.GetInt32("actor_id")
                });
            }
            return movieActors;
        }
    }
}
