using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieDBMS.Models
{
    public class Movie : MovieDBModel
    {
        [Key]
        public int MovieID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string IMDbLink { get; set; }
        public bool Action { get; set; } = false;
        public bool Adventure { get; set; } = false;
        public bool Comedy { get; set; } = false;
        public bool Drama { get; set; } = false;
        public bool Romance { get; set; } = false;
        public bool Thriller { get; set; } = false;
        public bool ScienceFiction { get; set; } = false;
        public bool Animation { get; set; } = false;
        public bool Fantasy { get; set; } = false;
        public bool Horror { get; set; } = false;
        public bool Musical { get; set; } = false;
        public bool Mystery { get; set; } = false;
        public bool Documentary { get; set; } = false;
        public bool War { get; set; } = false;
        public bool Crime { get; set; } = false;
        public bool Western { get; set; } = false;
        public bool FilmNoir { get; set; } = false;
        public bool Childrens { get; set; } = false;
        public bool Other { get; set; } = false;
        public override string ToString()
        {
            return $"Movie ID: {MovieID}, Title: {Title}, Release Date: {ReleaseDate}, IMDb Link: {IMDbLink}";
        }
        public static new IMovieDBModel Parse(string formatedString, string seperator = @"\|")
        {
            bool bIs(string value) => value == "1";
            Movie movie = new Movie();
            string[] data = Regex.Split(formatedString, seperator);
            int genreStart = 5;
            movie.MovieID = int.Parse(data[0]);
            movie.Title = data[1];
            movie.ReleaseDate = DateTime.Parse(data[2]);
            // 3 is missing
            movie.IMDbLink = data[4];
            movie.Action = bIs(data[genreStart]);
            movie.Adventure = bIs(data[genreStart + 1]);
            movie.Comedy = bIs(data[genreStart + 2]);
            movie.Drama = bIs(data[genreStart + 3]);
            movie.Romance = bIs(data[genreStart + 4]);
            movie.Thriller = bIs(data[genreStart + 5]);
            movie.ScienceFiction = bIs(data[genreStart + 6]);
            movie.Animation = bIs(data[genreStart + 7]);
            movie.Fantasy = bIs(data[genreStart + 8]);
            movie.Horror = bIs(data[genreStart + 9]);
            movie.Musical = bIs(data[genreStart + 10]);
            movie.Mystery = bIs(data[genreStart + 11]);
            movie.Documentary = bIs(data[genreStart + 12]);
            movie.War = bIs(data[genreStart + 13]);
            movie.Crime = bIs(data[genreStart + 14]);
            movie.Western = bIs(data[genreStart + 15]);
            movie.FilmNoir = bIs(data[genreStart + 16]);
            movie.Childrens = bIs(data[genreStart + 17]);
            movie.Other = bIs(data[genreStart + 18]);
            return movie;
        }
    }
}
