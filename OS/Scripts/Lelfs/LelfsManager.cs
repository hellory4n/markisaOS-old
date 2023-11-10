using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages Lelfs. NOTE: Filesystems are local to each user.
/// </summary>
public partial class LelfsManager : Node {
    /// <summary>
    /// The list of paths available in the filesystem. The key is the path, and the value is the ID.
    /// </summary>
    public static Dictionary<string, string> Paths = new();

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
        return Paths.ContainsKey(path);
    }

    /// <summary>
    /// Checks if there is a file with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the file.</param>
    /// <returns>Whether or not a file with the specified ID exists.</returns>
    public static bool IdExists(string id) {
        return Paths.ContainsValue(id);
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
    public static LelfsFile[] GetFolderItems(string path) {
        // FIXME: this is very much not efficient and would get slower with more files, 
        // please fix this at some point for fuck's sake
        string parentId = Load<Folder>(path).Id;
        List<LelfsFile> pain = new();
        foreach (var item in Paths) {
            LelfsFile bruh = LoadById<LelfsFile>(item.Value);
            if (bruh.Parent == parentId)
                pain.Add(bruh);
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
        Random random = new();
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
        LelfsFile yeah = new()
        {
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
        Folder yeah = new()
        {
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

    /// <summary>
    /// Setups the file structure new users always get.
    /// </summary>
    public static void NewFileStructure() {
        if (FileExists("/System") && FileExists("/Home"))
            return;

        Folder system = NewFolder("System", "root");
        system.Metadata.Add("CreationDate", DateTime.Now);
        system.Save();

        Folder trash = NewFolder("Trash", system.Id);
        trash.Metadata.Add("CreationDate", DateTime.Now);
        trash.Save();

        Folder home = NewFolder("Home", "root");
        home.Metadata.Add("CreationDate", DateTime.Now);
        home.Save();

        Folder documents = NewFolder("Documents", home.Id);
        documents.Metadata.Add("CreationDate", DateTime.Now);
        documents.Save();

        Folder downloads = NewFolder("Downloads", home.Id);
        downloads.Metadata.Add("CreationDate", DateTime.Now);
        downloads.Save();

        Folder music = NewFolder("Music", home.Id);
        music.Metadata.Add("CreationDate", DateTime.Now);
        music.Save();

        Folder pictures = NewFolder("Pictures", home.Id);
        pictures.Metadata.Add("CreationDate", DateTime.Now);
        pictures.Save();

        Folder videos = NewFolder("Videos", home.Id);
        videos.Metadata.Add("CreationDate", DateTime.Now);
        videos.Save();

        Folder samplePictures = NewFolder("Sample Pictures", pictures.Id);
        samplePictures.Metadata.Add("CreationDate", DateTime.Now);
        samplePictures.Save();

        LelfsFile highPeaks = NewFile("High Peaks", samplePictures.Id);
        highPeaks.Type = "Picture";
        highPeaks.Data.Add("Resource", "res://Assets/Wallpapers/HighPeaks.jpg");
        highPeaks.Metadata.Add("CreationDate", DateTime.Now);
        highPeaks.Save();

        LelfsFile flowers = NewFile("Flowers", samplePictures.Id);
        flowers.Type = "Picture";
        flowers.Data.Add("Resource", "res://Assets/Wallpapers/Flowers.png");
        flowers.Metadata.Add("CreationDate", DateTime.Now);
        flowers.Save();

        LelfsFile beaches = NewFile("Beaches", samplePictures.Id);
        beaches.Type = "Picture";
        beaches.Data.Add("Resource", "res://Assets/Wallpapers/Beaches.png");
        beaches.Metadata.Add("CreationDate", DateTime.Now);
        beaches.Save();

        LelfsFile aurora = NewFile("Aurora", samplePictures.Id);
        aurora.Type = "Picture";
        aurora.Data.Add("Resource", "res://Assets/Wallpapers/Aurora.png");
        aurora.Metadata.Add("CreationDate", DateTime.Now);
        aurora.Save();

        LelfsFile mountains = NewFile("Mountains", samplePictures.Id);
        mountains.Type = "Picture";
        mountains.Data.Add("Resource", "res://Assets/Wallpapers/Mountains.png");
        mountains.Metadata.Add("CreationDate", DateTime.Now);
        mountains.Save();

        LelfsFile space = NewFile("Space", samplePictures.Id);
        space.Type = "Picture";
        space.Data.Add("Resource", "res://Assets/Wallpapers/Space.png");
        space.Metadata.Add("CreationDate", DateTime.Now);
        space.Save();

        LelfsFile logo = NewFile("lelcubeOS", pictures.Id);
        logo.Type = "Picture";
        logo.Data.Add("Resource", "res://Assets/Boot/Logo2.png");
        logo.Metadata.Add("CreationDate", DateTime.Now);
        logo.Save();
    }

    /// <summary>
    /// Generates a correct file path based on whether or not it's located in root.
    /// </summary>
    /// <param name="parentPath">The path of the parent.</param>
    /// <param name="name">The name of the new file</param>
    /// <returns>The path generated.</returns>
    public static string NewPath(string parentPath, string name) {
        if (parentPath != "/") {
            return $"/{name}";
        } else {
            return $"{parentPath}/{name}";
        }
    }
}