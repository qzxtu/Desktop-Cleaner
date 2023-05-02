using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    // Define path for log file within application folder
    static string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;
    static string logFolderPath = Path.Combine(appFolderPath, "Logs");
    static string logFilePath = Path.Combine(logFolderPath, "log.txt");

    // Define paths for desktop and config files
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    static string configFilePath = Path.Combine(appFolderPath, "config.json");

    // Define folder names and list of sorting folders
    static string[] folderNames = { "Images", "Audios", "Videos", "CodeFiles", "ZipFiles", "TextFiles", "NewFolders", "Unknown", "Folders" };
    static List<string> sortingFolders = new List<string> { desktopPath };

    // Define config variable
    static Config config;

    static void Main(string[] args)
    {
        // Verify that the desktop path is not null or empty
        if (string.IsNullOrEmpty(desktopPath))
        {
            Console.WriteLine("Error: Desktop path is null or empty.");
            return;
        }

        // Load config and get files/folders from desktop
        LoadConfig();
        string[] files = Directory.GetFiles(desktopPath);
        string[] folders = Directory.GetDirectories(desktopPath);

        // Create sorting folders only if there are files with the corresponding extensions on the desktop
        foreach (string folderName in folderNames)
        {
            if (folderName == "Images" && !files.Any(file => config.ImageExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "Audios" && !files.Any(file => config.AudioExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "Videos" && !files.Any(file => config.VideoExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "CodeFiles" && !files.Any(file => config.CodeFileExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "ZipFiles" && !files.Any(file => config.ZipFileExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "TextFiles" && !files.Any(file => config.TextFileExtensions.Contains(Path.GetExtension(file).ToLower())))
                continue;
            if (folderName == "NewFolders" && !folders.Any(folder => IsNewFolder(folder)))
                continue;
            if (folderName == "Unknown" && !files.Any(file => GetDestinationFolder(Path.GetExtension(file).ToLower()) == "Unknown"))
                continue;

            string folderPath = Path.Combine(desktopPath, folderName);
            Directory.CreateDirectory(folderPath);
            sortingFolders.Add(folderPath);
        }

        // Move files to appropriate sorting folders
        foreach (string file in files)
        {
            if (IsShortcut(file) || IsRecycleBin(file))
            {
                continue;
            }

            string extension = Path.GetExtension(file).ToLower();
            string destinationFolder = Path.Combine(desktopPath, GetDestinationFolder(extension));
            MoveFile(file, destinationFolder);
        }

        // Move folders to appropriate sorting folders
        foreach (var folder in folders)
        {
            if (sortingFolders.Contains(folder)) continue;

            string destinationFolder = IsNewFolder(folder) ? "NewFolders" : "Folders";
            MoveFolder(folder, Path.Combine(desktopPath, destinationFolder));
        }
    }

    // Load config from file or create new config file if it doesn't exist
    static void LoadConfig()
    {
        if (!File.Exists(configFilePath))
        {
            config = new Config();
            string formattedJson = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configFilePath, formattedJson);
        }
        else
        {
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));
        }
    }

    // Check if a file is a shortcut or link
    static bool IsShortcut(string file)
    {
        bool goesToFile = (File.GetAttributes(file) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
        string ext = Path.GetExtension(file).ToLower();
        bool isLink = ext == ".url" || ext == ".lnk";
        return goesToFile || isLink;
    }

    // Check if a file is the Recycle Bin
    static bool IsRecycleBin(string file)
    {
        return Path.GetFileName(file) == "$RECYCLE.BIN";
    }

    // Get the appropriate destination folder for a file based on its extension
    static string GetDestinationFolder(string extension)
    {
        Dictionary<string[], string> extensionToFolderMap = new Dictionary<string[], string>
    {
        { config.ImageExtensions, "Images" },
        { config.AudioExtensions, "Audios" },
        { config.VideoExtensions, "Videos" },
        { config.CodeFileExtensions, "CodeFiles" },
        { config.TextFileExtensions, "TextFiles" },
        { config.ZipFileExtensions, "ZipFiles" }
    };

        return extensionToFolderMap.FirstOrDefault(x => x.Key.Contains(extension)).Value ?? "Unknown";
    }

    // Check if a folder is a new folder created by the user
    static bool IsNewFolder(string folder)
    {
        return Path.GetFileName(folder).StartsWith("New folder");
    }

    // Move a file to a destination folder and log the action
    static void MoveFile(string file, string destinationFolder)
    {
        try
        {
            File.Move(file, GenerateUniqueFileName(destinationFolder, Path.GetFileName(file)));
            Log($"Moved file {file} to {destinationFolder}");
        }
        catch (Exception ex)
        {
            Log($"Error moving file {file} to {destinationFolder}: {ex.Message}");
            return;
        }
    }

    // Move a folder to a destination folder and log the action
    static void MoveFolder(string folder, string destinationFolder)
    {
        try
        {
            Directory.Move(folder, GenerateUniqueFileName(destinationFolder, Path.GetFileName(folder)));
            Log($"Moved folder {folder} to {destinationFolder}");
        }
        catch (Exception ex)
        {
            Log($"Error moving folder {folder} to {destinationFolder}: {ex.Message}");
            return;
        }
    }

    // Generate a unique file name for a file or folder in a destination folder
    static string GenerateUniqueFileName(string targetFolder, string originalFileName)
    {
        string targetPath = Path.Combine(targetFolder, originalFileName);
        int fileCount = 1;

        while (File.Exists(targetPath) || Directory.Exists(targetPath))
        {
            string newFileName = $"{Path.GetFileNameWithoutExtension(originalFileName)} ({fileCount}){Path.GetExtension(originalFileName)}";
            targetPath = Path.Combine(targetFolder, newFileName);
            fileCount++;
        }

        return targetPath;
    }

    // Log a message to the log file
    static void Log(string message)
    {
        // Create Logs folder if it doesn't exist
        Directory.CreateDirectory(logFolderPath);

        // Verify that the log file path is not null or empty
        if (string.IsNullOrEmpty(logFilePath))
        {
            Console.WriteLine("Error: Log file path is null or empty.");
            return;
        }

        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }

    // Config class for storing extension mappings
    class Config
    {
        public string[] ImageExtensions { get; set; } = new[] { ".bmp", ".ico", ".tiff", ".jpeg", ".png", ".gif", ".jpg", ".webp" };
        public string[] AudioExtensions { get; set; } = new[] { ".m4a", ".aac", ".flac", ".ogg", ".wav", ".mp3", ".wma" };
        public string[] VideoExtensions { get; set; } = new[] { ".avi", ".mov", ".flv", ".wmv", ".mkv", ".mp4" };
        public string[] CodeFileExtensions { get; set; } = new[] { ".ini", ".hpp", ".h", ".js", ".html", ".css", ".cpp", ".cs", ".py" };
        public string[] TextFileExtensions { get; set; } = new[] { ".pdf", ".docx", ".txt" };
        public string[] ZipFileExtensions { get; set; } = new[] { ".gz", ".7z", ".zip", ".rar", ".tar" };
    }
}