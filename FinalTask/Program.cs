using FinalTask.Casino;
using FinalTask.Service;



namespace FinalTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            var saveLoadService = new FileSystemSaveLoadService("Profiles");
            IGame casino = new FinalTask.Casino.Casino(saveLoadService);
            casino.StartGame();
            
        }
    }
}
