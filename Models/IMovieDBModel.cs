namespace MovieDBMS.Models
{
    // Or the delegate can be replaced with Func<string, string, IMovieDBModel>
    public delegate IMovieDBModel Parser(string line, string delimiter);
    public interface IMovieDBModel
    {
        public static IMovieDBModel Parse(string line, string delimiter) => throw new NotImplementedException();
    }
}
