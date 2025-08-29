using System;
using System.IO;


namespace FinalTask.Service
{
    internal class FileSystemSaveLoadService : ISaveLoadService<string>
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
        public string LoadData(string identificator)
        {
            string fullPath = Path.Combine(PathPC, identificator + ".txt");
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File with name {identificator} not found");
            }
            return File.ReadAllText(fullPath);
        }
        public void SaveData(string save, string identificator)
        {
            string fullPath = Path.Combine(PathPC, identificator + ".txt");
            File.WriteAllText(fullPath, save);
        }

    }
}
