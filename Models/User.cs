using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieDBMS.Models
{
    public class User : MovieDBModel
    {
        [Key]
        public int UserID { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string ZipCode { get; set; }
        public override string ToString()
        {
            return $"User ID: {UserID}, Age: {Age}, Gender: {Gender}, Occupation: {Occupation}, Zip Code: {ZipCode}";
        }
        public static new IMovieDBModel Parse(string formatedString, string seperator = @"\|")
        {
            User user = new User();
            string[] data = Regex.Split(formatedString, seperator);
            user.UserID = int.Parse(data[0]);
            user.Age = int.Parse(data[1]);
            user.Gender = data[2];
            user.Occupation = data[3];
            user.ZipCode = data[4];
            return user;
        }
    }
}
