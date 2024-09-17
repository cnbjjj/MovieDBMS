using Utilities;

namespace MovieDBMS
{
    public delegate Task MenuOperation();
    public class MenuItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public MenuOperation Operation { get; set; } = () => Task.CompletedTask;
    }
    public class Program
    {
        static async Task Main(string[] args)
        {
            MovieDBMS movieDBMS = new MovieDBMS()
            {
                // File paths
                MovieFile = @"../../../Files/Movies.txt",
                UserFile = @"../../../Files/Users.txt",
                RatingFile = @"../../../Files/Ratings.txt",
                // Show [ShowingLimt] records at a time and start from [ShowingOffset] record
                ShowingLimit = 10,
                ShowingOffset = 10
            };
            // Menu items
            List<MenuItem> Menu = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Text = "Load Users", Operation = movieDBMS.LoadUsers },
                new MenuItem() { Id = 2, Text = "Load Movies", Operation = movieDBMS.LoadMovies },
                new MenuItem() { Id = 3, Text = "Load Ratings", Operation = movieDBMS.LoadRatings },
                new MenuItem() { Id = 4, Text = "Show Users", Operation = movieDBMS.ShowUsers },
                new MenuItem() { Id = 5, Text = "Show Movies", Operation = movieDBMS.ShowMovies },
                new MenuItem() { Id = 6, Text = "Show Ratings", Operation = movieDBMS.ShowRatings },
                new MenuItem() { Id = 7, Text = "Clear Data", Operation = movieDBMS.ClearData },
                new MenuItem() { Id = 8, Text = "Exit"}
            };

            string menuString = string.Empty;
            foreach (MenuItem item in Menu)
                menuString += $" {item.Id}. {item.Text}\n";
            int exit = Menu.Last().Id;
            int selected = 0;
            while (selected != exit)
            {
                Console.Clear();
                Console.WriteLine(" *******************************************");
                Console.WriteLine(" * Welcome to Movie Data Management System *");
                Console.WriteLine(" *******************************************");
                selected = Utility.ReadInputAsInt($"\n Please choose an action from below:\n{menuString} Please enter your choice: ");
                MenuItem? item = Menu.FirstOrDefault(m => m.Id == selected);
                if (item == null)
                    Console.WriteLine("\n - Unknown operation.");
                else
                    await item.Operation.Invoke();
                if(selected != exit)
                {
                    Console.Write("\n Press any key to go back to the [MENU]...");
                    Console.ReadKey();
                }
            }
        }
    }
}
