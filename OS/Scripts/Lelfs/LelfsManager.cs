using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages Lelfs. NOTE: Filesystems are local to each user.
/// </summary>
public class LelfsManager : Node {
    /// <summary>
    /// The list of paths available in the filesystem. The key is the path, and the value is the ID.
    /// </summary>
    public static Dictionary<string, string> Paths = new Dictionary<string, string>();

    /// <summary>
    /// Reloads the paths available in the filesystem, or creates a new one if the user doesn't have the files for the filesystem yet.
    /// </summary>
    public static void UpdatePaths() {
        File file = new File();
        if (file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/db.json")) {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Read);
            Paths = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                file.GetAsText()
            );
            file.Close();
        } else {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(Paths)
            );
            file.Close();
        }
    }

    /// <summary>
    /// Saves the paths available in the filesystem.
    /// </summary>
    public static void SavePaths() {
        File file = new File();
        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Paths)
        );
        file.Close();
    }

    /// <summary>
    /// Loads a file from its path.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="path">The path of the file.</param>
    /// <returns>The loaded file.</returns>
    public static T Load<T>(string path) where T : LelfsFile {
        if (Paths.ContainsKey(path)) {
            File file = new File();
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Paths[path]}.json", File.ModeFlags.Read);
            T pain = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            pain.Id = Paths[path];
            file.Close();
            return pain;
        } else {
            GD.PushError($"No file was found at path \"{path}\"!");
            return default;
        }
    }

    /// <summary>
    /// Loads a file from its ID instead of its path.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The loaded file.</returns>
    public static T LoadById<T>(string id) where T : LelfsFile {
        File file = new File();
        if (file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{id}.json")) {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{id}.json", File.ModeFlags.Read);
            T yes = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            return yes;
        } else {
            GD.PushError($"File with ID \"{id}\" doesn't exist!");
            return default;
        }
    }

    /// <summary>
    /// Checks if a file exists from its path.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>Whether or not the file exists.</returns>
    public static bool FileExists(string path) {
        if (Paths.ContainsKey(path)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Returns the ID of a file from its path.
    /// </summary>
    /// <param name="path">The path of a file.</param>
    /// <returns>The ID of the file.</returns>
    public static string PermanentPath(string path) {
        LelfsFile jbkfjbg = Load<LelfsFile>(path);
        return jbkfjbg.Id;
    }

    /// <summary>
    /// Finds all of the files inside a certain folder.
    /// </summary>
    /// <param name="path">The path of the folder.</param>
    /// <returns>An array of IDs of each file in the folder</returns>
    public static string[] GetFolderItems(string path) {
        // FIXME: this is very much not efficient and would get slower with more files, 
        // please fix this at some point for fuck's sake
        string parentId = Load<Folder>(path).Id;
        List<string> pain = new List<string>();
        foreach (var item in Paths) {
            LelfsFile bruh = LoadById<LelfsFile>(item.Value);
            if (bruh.Parent == parentId)
                pain.Add(bruh.Id);
        }
        return pain.ToArray();
    }

    /// <summary>
    /// Generates an ID for files.
    /// </summary>
    /// <returns></returns>
    public static string GenerateID() {
        string id = "";
        string[] possibleCharacters = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "-", "_"
        };
        Random random = new Random();
        for (int i = 0; i < 20; i++) {
            id += possibleCharacters[random.Next(0, 63)];
        }
        return id;
    }

    /// <summary>
    /// Initializes a Lelfs file, but doesn't save it.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="parent">The ID of the parent of the file.</param>
    public static LelfsFile NewFile(string name, string parent) {
        // very illegal names
        if (name.Contains("/")) {
            GD.PushError("Filenames can't include forward slashes (/)");
            return default;
        }

        // setup stuff :)
        LelfsFile yeah = new LelfsFile {
            Parent = parent,
            Name = name,
            Id = GenerateID()
        };

        // yes :)
        if (parent != "root" || parent == null) {
            LelfsFile m = LoadById<LelfsFile>(parent);
            yeah.Path = $"{m.Path}/{name}";
        } else {
            yeah.Path = $"/{name}";
        }

        return yeah;
    }

    /// <summary>
    /// Initializes a Lelfs folder, but doesn't save it.
    /// </summary>
    /// <param name="name">The name of the folder.</param>
    /// <param name="parent">The ID of the parent of the folder.</param>
    public static Folder NewFolder(string name, string parent) {
        // very illegal names
        if (name.Contains("/")) {
            GD.PushError("Filenames can't include forward slashes (/)");
            return default;
        }

        // setup stuff :)
        Folder yeah = new Folder {
            Parent = parent,
            Name = name,
            Id = GenerateID(),
            Type = "Folder"
        };

        // yes :)
        if (parent != "root" || parent == null) {
            LelfsFile m = LoadById<LelfsFile>(parent);
            yeah.Path = $"{m.Path}/{name}";
        } else {
            yeah.Path = $"/{name}";
        }

        return yeah;
    }
}