using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Kickstart.Records;

namespace Kickstart.Cabinetfs;

/// <summary>
/// A file in Cabinetfs.
/// </summary>
public partial class File
{
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
    public Dictionary<string, object> Metadata = new();
    /// <summary>
    /// The path where this file can be accessed.
    /// </summary>
    public string Path;
    /// <summary>
    /// The type of this file.
    /// </summary>
    public string Type = "CabinetfsFile";
    /// <summary>
    /// This is where apps write their data to.
    /// </summary>
    public Dictionary<string, object> Data = new();

    /// <summary>
    /// Saves this file, or creates a new one if it hasn't been saved yet.
    /// </summary>
    public void Save()
    {
        DirAccess.MakeDirRecursiveAbsolute($"user://Users/{RecordManager.CurrentUser}/Files/");

        if (!CabinetfsManager.PathExists(Path))
        {
            CabinetfsManager.Paths.Add(Path, Id);
            CabinetfsManager.SavePaths();
        }

        using var file = FileAccess.Open($"user://Users/{RecordManager.CurrentUser}/Files/{Id}.json", FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(this, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
            })
        );
    }

    /// <summary>
    /// Copies the current file. Use Folder.Copy() if you also want to copy the folder's children.
    /// </summary>
    /// <param name="name">The name of the new file.</param>
    /// <param name="parent">The parent of the new file.</param>
    /// <returns>The ID of the copied file.</returns>
    public virtual File Copy(string name, string parent = null)
    {
        var gaming = JsonConvert.DeserializeObject<File>(
            JsonConvert.SerializeObject(this)
        );
        gaming.Name = name;
        gaming.Parent = parent;
        gaming.Id = CabinetfsManager.GenerateId();

        if (parent != null)
        {
            File m = CabinetfsManager.LoadFile(parent);
            gaming.Path = $"{m.Path}/{gaming.Name}";
        }
        else
            gaming.Path = $"/{gaming.Name}";

        if (!CabinetfsManager.PathExists(gaming.Path))
        {
            CabinetfsManager.Paths.Add(gaming.Path, gaming.Id);
            CabinetfsManager.SavePaths();
        }

        gaming.Save();
        return gaming;
    }

    /// <summary>
    /// Renames this file.
    /// </summary>
    /// <param name="name">The new name of the file.</param>
    public virtual void Rename(string name)
    {
        Name = name;

        CabinetfsManager.Paths.Remove(Path);

        if (Parent != "root")
        {
            File m = CabinetfsManager.LoadFile(Parent);
            Path = $"{m.Path}/{name}";
        }
        else
            Path = $"/{name}";
        
        Save();
    }

    /// <summary>
    /// Deletes this file permanently.
    /// </summary>
    public virtual void Delete()
    {
        if (CabinetfsManager.PathExists(Path))
        {
            DirAccess.RemoveAbsolute($"user://Users/{RecordManager.CurrentUser}/Files/{Id}.json");
            CabinetfsManager.Paths.Remove(Path);
            CabinetfsManager.SavePaths();
        }
        else
            GD.PushError("File not saved yet!");
    }

    /// <summary>
    /// Changes the parent of a file.
    /// </summary>
    /// <param name="parent">The ID of the new parent.</param>
    public virtual void Move(string parent)
    {
        if (Parent == parent)
            return;

        Parent = parent;

        CabinetfsManager.Paths.Remove(Path);

        if (parent != "root")
        {
            File m = CabinetfsManager.LoadFile(parent);
            Path = $"{m.Path}/{Name}";
        }
        else
            Path = $"/{Name}";

        Save();

        CabinetfsManager.Paths.Add(Path, Id);
        CabinetfsManager.SavePaths();
    }
}
