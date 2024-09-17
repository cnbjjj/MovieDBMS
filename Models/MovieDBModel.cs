namespace MovieDBMS.Models
{
    public abstract class MovieDBModel: IMovieDBModel
    {
        public static IMovieDBModel Parse(string formatedString, string seperator = @"\|") => throw new NotImplementedException();
    }
}
