using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// A file in Lelfs.
/// </summary>
public partial class LelfsFile {
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
    public string Path3D;
    /// <summary>
    /// The type of this file.
    /// </summary>
    public string Type = "LelfsFile";
    /// <summary>
    /// This is where apps write their data to.
    /// </summary>
    public Dictionary<string, object> Data = new Dictionary<string, object>();

    /// <summary>
    /// Saves this file, or creates a new one if it hasn't been saved yet.
    /// </summary>
    public void Save() {
        DirAccess directory = new DirAccess();
        File file = new File();
        directory.MakeDirRecursive($"user://Users/{SavingManager.CurrentUser}/Files/");

        if (!LelfsManager.FileExists(Path3D)) {
            LelfsManager.Paths.Add(Path3D, Id);
            LelfsManager.SavePaths();
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
    /// Copies the current file. NOTE: Use <c>CopyFolder</c> if this is a folder.
    /// </summary>
    /// <typeparam name="T">The type of the new file.</typeparam>
    /// <param name="name">The name of the new file.</param>
    /// <param name="parent">The parent of the new file.</param>
    /// <returns>The ID of the copied file.</returns>
    public virtual string Copy(string name, string parent = null) {
        var gaming = JsonConvert.DeserializeObject<LelfsFile>(
            JsonConvert.SerializeObject(this)
        );
        gaming.Name = name;
        gaming.Parent = parent;
        gaming.Id = LelfsManager.GenerateID();

        if (parent != null) {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(parent);
            gaming.Path3D = $"{m.Path3D}/{gaming.Name}";
        } else {
            gaming.Path3D = $"/{gaming.Name}";
        }

        if (!LelfsManager.FileExists(gaming.Path3D)) {
            LelfsManager.Paths.Add(gaming.Path3D, gaming.Id);
            LelfsManager.SavePaths();
        }

        gaming.Save();
        return gaming.Id;
    }

    /// <summary>
    /// Renames this file.
    /// </summary>
    /// <param name="name">The new name of the file.</param>
    public virtual void Rename(string name) {
        Name = name;

        LelfsManager.Paths.Remove(Path3D);

        if (Parent != "root") {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(Parent);
            Path3D = $"{m.Path3D}/{name}";
        } else {
            Path3D = $"/{name}";
        }
        
        Save();
    }

    /// <summary>
    /// Deletes this file permanently.
    /// </summary>
    public virtual void Delete() {
        DirAccess directory = new DirAccess();
        if (LelfsManager.FileExists(Path3D)) {
            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path3D);
            LelfsManager.SavePaths();
        } else {
            GD.PushError("File not saved yet!");
        }
    }

    /// <summary>
    /// Changes the parent of a file.
    /// </summary>
    /// <param name="parent">The ID of the new parent.</param>
    public virtual void Move(string parent) {
        if (Parent == parent)
            return;

        Parent = parent;

        LelfsManager.Paths.Remove(Path3D);

        if (parent != "root") {
            LelfsFile m = LelfsManager.LoadById<LelfsFile>(parent);
            Path3D = $"{m.Path3D}/{Name}";
        } else {
            Path3D = $"/{Name}";
        }

        Save();

        LelfsManager.Paths.Add(Path3D, Id);
        LelfsManager.SavePaths();
    }
}
