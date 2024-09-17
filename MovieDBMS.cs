using Utilities;
using Microsoft.EntityFrameworkCore;
using MovieDBMS.Models;

namespace MovieDBMS
{
    public class MovieDBMS
    {
        public string MovieFile { get; set; } = @"./Movies.txt";
        public string UserFile { get; set; } = @"./Users.txt";
        public string RatingFile { get; set; } = @"./Ratings.txt";
        public int ShowingLimit { get; set; } = 10;
        public int ShowingOffset { get; set; } = 0;

        public async Task LoadData()
        {
            await LoadUsers();
            await LoadMovies();
            await LoadRatings();
        }

        public async Task ClearData()
        {
            Console.WriteLine("");
            await ClearData<Rating>();
            await ClearData<Movie>();
            await ClearData<User>();
        }

        public async Task LoadMovies()
        {
            Console.WriteLine("");
            await ClearData<Movie>();
            Console.WriteLine(" - Loading Movies ");
            await ParseData<Movie>(MovieFile, @"\|", Movie.Parse);
            Console.WriteLine(" - Movies Loaded");
        }
        public async Task ShowMovies()
        {
            Utility.PrintTitle("Showing Movies");
            await Show<Movie>(ShowingOffset,ShowingLimit);
            Utility.PrintEnd();
        }

        public async Task LoadUsers()
        {
            Console.WriteLine("");
            await ClearData<User>();
            Console.WriteLine(" - Loading Users");
            await ParseData<User>(UserFile, @"\|", User.Parse);
            Console.WriteLine(" - Users Loaded");
        }
        public async Task ShowUsers()
        {
            Utility.PrintTitle("Showing Users");
            await Show<User>(ShowingOffset, ShowingLimit);
            Utility.PrintEnd();
        }
        public async Task LoadRatings()
        {
            Console.WriteLine("");
            await ClearData<Rating>();
            Console.WriteLine(" - Loading Ratings");
            await ParseData<Rating>(RatingFile, @"\s+", Rating.Parse, false);
            Console.WriteLine(" - Ratings Loaded");
        }
        public async Task ShowRatings()
        {
            Utility.PrintTitle("Showing Ratings");
            await Show<Rating>(ShowingOffset, ShowingLimit);
            Utility.PrintEnd();
        }

        // Parse data from the file
        private static async Task ParseData<T>(string file, string delimiter, Parser Parse, bool hasKey = true) where T : class, IMovieDBModel
        {
            char[] spinner = { '|', '/', '-', '\\' };
            var lines = await File.ReadAllLinesAsync(file);
            int totalLine = lines.Length;
            int currentLine = 0;
            string sql = string.Empty;
            foreach (var line in lines)
            {
                IMovieDBModel? data = null;
                try
                {
                    currentLine++;
                    data = Parse(line, delimiter);
                    if (data is Movie movie)
                        await RestoreData(movie);
                    if (data is User user)
                        await RestoreData(user);
                    if (data is Rating rating)
                        await RestoreData(rating, false);
                    Console.Write($"\r {spinner[currentLine % spinner.Length]} Processing {currentLine} of {totalLine}");
                }
                catch (Exception e)
                {
                    Utility.PrintMessageBlock("ERROR", $"An error occured while parsing data: \n Line: {currentLine} \n Text: {line} \n Error Message: {e.Message}");
                }
            }
            Console.Write($"\r {spinner[2]} Processing {currentLine} of {totalLine}");
            Console.WriteLine();
        }

        // Restore data to the exsisting database
        private static async Task RestoreData<T>(T data, bool hasKey = true) where T : class
        {
            string name = typeof(T).Name;
            string tableName = GetTableName<T>();
            try
            {
                using MovieDBContext db = new();
                // By default EF generates new transcation for each command
                // So for inserting primary key values, we need to use a single transaction to enable and disable IDENTITY_INSERT
                using var transaction = await db.Database.BeginTransactionAsync();
                // Enable IDENTITY_INSERT for inserting primary key values
                if (hasKey)
                    await db.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} ON");
                await db.Set<T>().AddAsync(data);
                await db.SaveChangesAsync();
                // Disable IDENTITY_INSERT after the data restore
                if (hasKey)
                    await db.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} OFF");
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Exception? inner = e.InnerException;
                string msg = $"An error occured while saving data: \n Type: {name}, \n Data: {data}";
                if (inner != null)
                    msg += $"\n Inner Exception: {inner.Message}";
                Utility.PrintMessageBlock("ERROR", $"{msg}");
            }
        }

        // Clear exsisting data from the database
        private static async Task ClearData<T>() where T : class
        {
            string name = typeof(T).Name;
            try
            {
                Console.WriteLine($" - Clearing {name}");
                using MovieDBContext db = new();
                await db.Database.CanConnectAsync();
                db.Set<T>().RemoveRange(db.Set<T>());
                await db.SaveChangesAsync();
                Console.WriteLine($" - {name} Cleared");
            }
            catch (Exception e)
            {
                Console.WriteLine($" An error occured while clearing data: {name}, {e.Message}");
            }
        }

        // Show data from the database
        private static async Task Show<T>(int from = 0, int limit = 10) where T : class
        {
            using MovieDBContext db = new();
            await db.Database.CanConnectAsync();
            var data = db.Set<T>();
            Console.WriteLine($" Skipped:{from}, showing {limit} out of {await data.CountAsync()}\n");
            var list = data.Skip(from).Take(limit).ToList();
            foreach (var item in list)
            {
                Console.WriteLine($" {item}");
            }
        }

        private static string GetTableName<T>() where T : class
        {
            // TODO: Implement this method, return the correct table name for the given type
            return typeof(T).Name + "s";
        }

    }
}