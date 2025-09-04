using FinalTask.Profile;


namespace FinalTask.Service
{
    public class FileSystemSaveLoadService : ISaveLoadService<ProfilePlayer>
    {
        private string _pathPC;
        public FileSystemSaveLoadService(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _pathPC = path;
        }
        public ProfilePlayer LoadData(string identificator)
        {
            string fullPath = Path.Combine(_pathPC, identificator + ".txt");
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
            string fullPath = Path.Combine(_pathPC, identificator + ".txt");
            File.WriteAllText(fullPath, $"{save.Name}|{save.Bank}");
        }

    }
}
