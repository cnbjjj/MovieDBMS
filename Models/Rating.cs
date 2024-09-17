
using MovieDBMS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace MovieDBMS
{
    public class Rating: MovieDBModel
    {
        [Key]
        public int RatingID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public int RatingValue { get; set; }
        public int Timestamp { get; set; }
        public User? User { get; set; }
        public Movie? Movie { get; set; }
        public override string ToString()
        {
            return $"User ID: {UserID}, Movie ID: {MovieID}, Rating Value: {RatingValue}, Timestamp: {Timestamp}";
        }
        public static new IMovieDBModel Parse(string formatedString, string seperator = @"\|")
        {
            Rating rating = new Rating();
            string[] data = Regex.Split(formatedString, seperator);
            //rating.RatingID = int.Parse(data[0]);
            rating.UserID = int.Parse(data[0]);
            rating.MovieID = int.Parse(data[1]);
            rating.RatingValue = int.Parse(data[2]);
            rating.Timestamp = int.Parse(data[3]);
            return rating;
        }
    }
}
