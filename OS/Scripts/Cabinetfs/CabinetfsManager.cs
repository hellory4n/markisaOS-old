using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Kickstart.Records;

namespace Kickstart.Cabinetfs;

/// <summary>
/// Manages Cabinetfs. NOTE: Filesystems are local to each user.
/// </summary>
public partial class CabinetfsManager : Node
{
    /// <summary>
    /// The list of paths available in the filesystem. The key is the path, and the value is the ID.
    /// </summary>
    public static Dictionary<string, string> Paths = new();

    /// <summary>
    /// Reloads the paths available in the filesystem, or creates a new one if the user doesn't have the files for the filesystem yet.
    /// </summary>
    public static void UpdatePaths()
    {
        if (FileAccess.FileExists($"user://Users/{RecordManager.CurrentUser}/Files/db.json"))
        {
            using var file = FileAccess.Open($"user://Users/{RecordManager.CurrentUser}/Files/db.json", FileAccess.ModeFlags.Read);
            Paths = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                file.GetAsText()
            );
        }
        else
        {
            using var file = FileAccess.Open($"user://Users/{RecordManager.CurrentUser}/Files/db.json", FileAccess.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(Paths)
            );
        }
    }

    /// <summary>
    /// Saves the paths available in the filesystem.
    /// </summary>
    public static void SavePaths()
    {
        using var file = FileAccess.Open($"user://Users/{RecordManager.CurrentUser}/Files/db.json", FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Paths)
        );
    }

    /// <summary>
    /// Loads a file from its ID.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The loaded file.</returns>
    static T InternalLoad<T>(string id) where T : File
    {
        if (FileAccess.FileExists($"user://Users/{RecordManager.CurrentUser}/Files/{id}.json")) {
            using var file = FileAccess.Open($"user://Users/{RecordManager.CurrentUser}/Files/{id}.json", FileAccess.ModeFlags.Read);
            T yes = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            return yes;
        }
        else
        {
            GD.PushError($"File with ID \"{id}\" doesn't exist!");
            return default;
        }
    }

    /// <summary>
    /// Loads a file from its ID.
    /// </summary>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The loaded file.</returns>
    public static File LoadFile(string id)
    {
        return InternalLoad<File>(id);
    }

    /// <summary>
    /// Loads a folder from its ID.
    /// </summary>
    /// <param name="id">The ID of the folder.</param>
    /// <returns>The loaded folder.</returns>
    public static Folder LoadFolder(string id)
    {
        return InternalLoad<Folder>(id);
    }

    /// <summary>
    /// Checks if a file exists from its path.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>Whether or not the file exists.</returns>
    public static bool PathExists(string path)
    {
        return Paths.ContainsKey(path);
    }

    /// <summary>
    /// Checks if there is a file with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the file.</param>
    /// <returns>Whether or not a file with the specified ID exists.</returns>
    public static bool IdExists(string id)
    {
        return Paths.ContainsValue(id);
    }

    /// <summary>
    /// Returns the ID of a file from its path.
    /// </summary>
    /// <param name="path">The path of a file.</param>
    /// <returns>The ID of the file.</returns>
    public static string GetId(string path)
    {
        File jbkfjbg = LoadFile(path);
        return jbkfjbg.Id;
    }

    /// <summary>
    /// Finds all of the files inside a certain folder.
    /// </summary>
    /// <param name="path">The path of the folder.</param>
    /// <returns>An array of files in the folder. May also include folders, so remember to check that</returns>
    public static File[] GetFolderItems(string path)
    {
        // FIXME: loading every single in the filesystem just to check the parent is not very efficient,
        // who could have guessed?
        string parentId = LoadFolder(path).Id;
        List<File> pain = new();
        foreach (var item in Paths)
        {
            File bruh = LoadFile(item.Value);
            if (bruh.Parent == parentId)
                pain.Add(bruh);
        }
        return pain.ToArray();
    }

    /// <summary>
    /// Generates an ID for files.
    /// </summary>
    /// <returns></returns>
    public static string GenerateId()
    {
        string id = "";
        string[] possibleCharacters =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "-", "_"
        };
        Random random = new();
        for (int i = 0; i < 20; i++)
        {
            id += possibleCharacters[random.Next(0, 63)];
        }
        return id;
    }

    /// <summary>
    /// Initializes a Cabinetfs file, but doesn't save it.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="parent">The ID of the parent of the file.</param>
    public static File NewFile(string name, string parent)
    {
        // very illegal names
        if (name.Contains('/'))
        {
            GD.PushError("Filenames can't include forward slashes (/)");
            return default;
        }

        // setup stuff :)
        File yeah = new()
        {
            Parent = parent,
            Name = name,
            Id = GenerateId()
        };

        // yes :)
        if (parent != "root" || parent == null)
        {
            File m = LoadFile(parent);
            yeah.Path = $"{m.Path}/{name}";
        }
        else
            yeah.Path = $"/{name}";

        return yeah;
    }

    /// <summary>
    /// Initializes a Cabinetfs folder, but doesn't save it.
    /// </summary>
    /// <param name="name">The name of the folder.</param>
    /// <param name="parent">The ID of the parent of the folder.</param>
    public static Folder NewFolder(string name, string parent)
    {
        // very illegal names
        if (name.Contains('/'))
        {
            GD.PushError("Filenames can't include forward slashes (/)");
            return default;
        }

        // setup stuff :)
        Folder yeah = new()
        {
            Parent = parent,
            Name = name,
            Id = GenerateId(),
            Type = "Folder"
        };

        // yes :)
        if (parent != "root" || parent == null)
        {
            File m = LoadFile(parent);
            yeah.Path = $"{m.Path}/{name}";
        }
        else
            yeah.Path = $"/{name}";

        return yeah;
    }

    /// <summary>
    /// Setups the file structure new users always get.
    /// </summary>
    public static void NewFileStructure()
    {
        if (PathExists("/System") && PathExists("/Home"))
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

        File highPeaks = NewFile("High Peaks", samplePictures.Id);
        highPeaks.Type = "Picture";
        highPeaks.Data.Add("Resource", "res://Assets/Wallpapers/HighPeaks.jpg");
        highPeaks.Metadata.Add("CreationDate", DateTime.Now);
        highPeaks.Save();

        File flowers = NewFile("Flowers", samplePictures.Id);
        flowers.Type = "Picture";
        flowers.Data.Add("Resource", "res://Assets/Wallpapers/Flowers.jpg");
        flowers.Metadata.Add("CreationDate", DateTime.Now);
        flowers.Save();

        File beaches = NewFile("Beaches", samplePictures.Id);
        beaches.Type = "Picture";
        beaches.Data.Add("Resource", "res://Assets/Wallpapers/Beaches.jpg");
        beaches.Metadata.Add("CreationDate", DateTime.Now);
        beaches.Save();

        File aurora = NewFile("Aurora", samplePictures.Id);
        aurora.Type = "Picture";
        aurora.Data.Add("Resource", "res://Assets/Wallpapers/Aurora.jpg");
        aurora.Metadata.Add("CreationDate", DateTime.Now);
        aurora.Save();

        File mountains = NewFile("Mountains", samplePictures.Id);
        mountains.Type = "Picture";
        mountains.Data.Add("Resource", "res://Assets/Wallpapers/Mountains.jpg");
        mountains.Metadata.Add("CreationDate", DateTime.Now);
        mountains.Save();

        File space = NewFile("Space", samplePictures.Id);
        space.Type = "Picture";
        space.Data.Add("Resource", "res://Assets/Wallpapers/Space.jpg");
        space.Metadata.Add("CreationDate", DateTime.Now);
        space.Save();

        File logo = NewFile("lelcubeOS", pictures.Id);
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
    public static string NewPath(string parentPath, string name)
    {
        if (parentPath != "/")
            return $"/{name}";
        else
            return $"{parentPath}/{name}";
    }

    /// <summary>
    /// Gets the type of a file from its ID.
    /// </summary>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The type of the file.</returns>
    public static string GetFileType(string id)
    {
        return LoadFile(id).Type;
    }
}