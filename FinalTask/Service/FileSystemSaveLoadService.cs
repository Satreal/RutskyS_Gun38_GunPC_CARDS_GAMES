using FinalTask.Profile;


namespace FinalTask.Service
{
    public class FileSystemSaveLoadService : ISaveLoadService<ProfilePlayer>
    {
        public string PathPC { get; }
        public FileSystemSaveLoadService(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            PathPC = path;
        }
        public ProfilePlayer LoadData(string identificator)
        {
            string fullPath = Path.Combine(PathPC, identificator + ".txt");
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File with name {identificator} not found");
            }
            var data = File.ReadAllText(fullPath);
            var parts = data.Split('|');
            return new ProfilePlayer(parts[0]) { Bank = int.Parse(parts[1]) };
        }
        public void SaveData(ProfilePlayer save, string identificator)
        {
            string fullPath = Path.Combine(PathPC, identificator + ".txt");
            File.WriteAllText(fullPath, $"{save.Name}|{save.Bank}");
        }

    }
}
