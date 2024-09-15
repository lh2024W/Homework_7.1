namespace Homework_7._1
{
    public class Program
    {
        private static DatabaseService databaseService;
        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();

            databaseService.GetClientWithInfo();
        }
    }
}
