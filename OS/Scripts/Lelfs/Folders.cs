using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// A lelfs folder.
/// </summary>
public class Folder : LelfsFile {
    /// <summary>
    /// Copies this folder and all of its items.
    /// </summary>
    /// <param name="name">The name of the new folder.</param>
    /// <param name="parent">The ID of the parent of the new folder.</param>
    /// <returns>The ID of the copied folder.</returns>
    public override string Copy(string name, string parent = null) {
        // MemberwiseClone() is no worky xd
        string fghjrnewhjoerthlk = JsonConvert.SerializeObject(this, new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All,
        });
        var gaming = JsonConvert.DeserializeObject<Folder>(fghjrnewhjoerthlk, new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All,
        });

        gaming.Name = name;
        gaming.Parent = parent;

        if (parent != "root") {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(parent);
            gaming.Path = $"{m.Path}/{name}";
        } else {
            gaming.Path = $"/{name}";
        }

        gaming.Id = LelfsManager.GenerateID();

        foreach (string item in LelfsManager.GetFolderItems(Path)) {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(item);
            m.Copy(m.Name, gaming.Id);
        }

        gaming.Save();
        return gaming.Id;
    }

    /// <summary>
    /// Renames this folder and updates the paths of its items.
    /// </summary>
    /// <param name="name">The new name of the folder.</param>
    public override void Rename(string name) {
        if (Name == name)
            return;

        Name = name;

        LelfsManager.Paths.Remove(Path);

        if (Parent != "root") {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(Parent);
            Path = $"{m.Path}/{Name}";
        } else {
            Path = $"/{Name}";
        }

        Save();

        foreach (string item in LelfsManager.GetFolderItems(Path)) {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(item);
            m.Parent = Id;
            LelfsManager.Paths.Remove(m.Path);
            m.Path = $"{Path}/{m.Name}";
            LelfsManager.Paths.Add(m.Path, m.Id);
            m.Save();
        }

        LelfsManager.SavePaths();
    }

    /// <summary>
    /// Deletes this folder and all of its items.
    /// </summary>
    public override void Delete() {
        Directory directory = new Directory();
        if (directory.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json")) {
            foreach (var item in LelfsManager.GetFolderItems(Path)) {
                LelfsFile bruh = LelfsManager.LoadById<LelfsFile>(item);
                bruh.Delete();
            }

            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path);
            LelfsManager.SavePaths();            
        } else {
            GD.PushError("File not saved yet!");
        }
    }

    /// <summary>
    /// Loads a file inside this folder.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="path">The path of the file, example: <c>FolderInsideThis/File</c></param>
    /// <returns>The file loaded.</returns>
    public T LoadLocal<T>(string path) where T : LelfsFile {
        string actualPath = $"{Path}/{path}";
        return LelfsManager.Load<T>(actualPath);
    }
}
