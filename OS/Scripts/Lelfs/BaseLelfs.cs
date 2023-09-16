using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Base for all Lelfs file formats.
/// </summary>
public class BaseLelfs {
    /// <summary>
    /// The unique ID for the file: A random number, encoded in base64, with 20 characters.
    /// </summary>
    public string Id;
    /// <summary>
    /// The unique ID for the parent of this file.
    /// </summary>
    public string Parent;
    /// <summary>
    /// The name of this file.
    /// </summary>
    public string Name;
    /// <summary>
    /// The metadata for this file.
    /// </summary>
    public Dictionary<string, object> Metadata = new Dictionary<string, object>();
    /// <summary>
    /// The path where this file can be accessed.
    /// </summary>
    public string Path;
    /// <summary>
    /// The type of this file.
    /// </summary>
    public string Type = "BaseLelfs";

    /// <summary>
    /// Initializes a BaseLelfs file. NOTE: Use <c>NewId</c> after this if you're creating new files.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="parent">The ID of the parent of the file.</param>
    public BaseLelfs(string name, string parent = null) {
        // very illegal names
        if (name.Contains("/"))
            GD.PushError("Filenames can't include forward slashes (/)");

        // setup stuff :)
        Parent = parent;
        Name = name;

        // yes :)
        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            Path = $"{m.Path}/{name}";
        } else {
            Path = $"/{name}";
        }

        // generates cool id
        if (!LelfsManager.FileExists(Path)) {
            Id = "";
            string[] possibleCharacters = {
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
                "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
                "y", "z", "-", "_"
            };
            Random random = new Random();
            for (int i = 0; i < 20; i++) {
                Id += possibleCharacters[random.Next(0, 63)];
            }
        }
    }

    /// <summary>
    /// Saves this file, or creates a new one if it hasn't been saved yet.
    /// </summary>
    public virtual void Save() {
        Directory directory = new Directory();
        File file = new File();
        directory.MakeDirRecursive($"user://Users/{SavingManager.CurrentUser}/Files/");

        if (!file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json") &&
        !LelfsManager.Paths.ContainsKey(Path)) {
            LelfsManager.Paths.Add(Path, Id);
            LelfsManager.SavePaths();

            if (Parent != null) {
                Folder pain = LelfsManager.LoadById<Folder>(Parent);
                pain.Items.Add(Id);
                pain.Save();
            }
        }

        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(this, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
            })
        );
        file.Close();
    }

    /// <summary>
    /// Loads a file inside this file.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="path">The path of the file, example: <c>FolderInsideThis/File</c></param>
    /// <returns>The file loaded.</returns>
    public T LoadLocal<T>(string path) where T : BaseLelfs {
        string actualPath = $"{Path}/{path}";
        if (LelfsManager.Paths.ContainsKey(actualPath)) {
            File file = new File();
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{LelfsManager.Paths[actualPath]}.json",
                File.ModeFlags.Read);
            T pain = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            pain.Id = LelfsManager.Paths[actualPath];
            file.Close();
            return pain;
        } else {
            GD.PushError($"No file was found at path \"{actualPath}\"!");
            return default;
        }
    }

    /// <summary>
    /// Renames this file.
    /// </summary>
    /// <param name="name">The new name of the file.</param>
    public virtual void Rename(string name) {
        if (Name == name)
            return;

        Name = name;

        LelfsManager.Paths.Remove(Path);

        if (Parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(Parent);
            Path = $"{m.Path}/{name}";
        } else {
            Path = $"/{name}";
        }
        
        Save();

        LelfsManager.Paths.Add(Path, Id);
        LelfsManager.SavePaths();
    }

    /// <summary>
    /// Deletes this file permanently.
    /// </summary>
    public virtual void Delete() {
        Directory directory = new Directory();
        if (directory.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json")) {
            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path);
            LelfsManager.SavePaths();
        } else {
            GD.PushError("File not saved yet!");
        }
    }
}
