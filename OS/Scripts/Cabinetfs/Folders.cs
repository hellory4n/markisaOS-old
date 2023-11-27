using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Kickstart.Records;

namespace Kickstart.Cabinetfs;

/// <summary>
/// A cabinetfs folder.
/// </summary>
public partial class Folder : File
{
    /// <summary>
    /// Copies this folder and all of its items. Use File.Copy() if you don't want copy the items.
    /// </summary>
    /// <param name="name">The name of the new folder.</param>
    /// <param name="parent">The ID of the parent of the new folder.</param>
    /// <returns>The copied folder.</returns>
    public override Folder Copy(string name, string parent = null)
    {
        // MemberwiseClone() is no worky xd
        var gaming = JsonConvert.DeserializeObject<Folder>(
            JsonConvert.SerializeObject(this)
        );

        gaming.Name = name;
        gaming.Parent = parent;
        gaming.Id = CabinetfsManager.GenerateId();

        if (parent != "root")
        {
            File m = CabinetfsManager.LoadFile(parent);
            gaming.Path = $"{m.Path}/{name}";
        }
        else
            gaming.Path = $"/{name}";

        gaming.Save();

        foreach (File m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadFolder(m.Id);
                // haha recursion
                ha.Copy(ha.Name, gaming.Id);
            }
            else
                m.Copy(m.Name, gaming.Id);
        }

        return gaming;
    }

    /// <summary>
    /// Renames this folder and updates the paths of its items.
    /// </summary>
    /// <param name="name">The new name of the folder.</param>
    public override void Rename(string name)
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

        foreach (File m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadFolder(m.Id);
                // haha recursion
                ha.Rename(ha.Name);
            }
            else
            {
                m.Parent = Id;
                CabinetfsManager.Paths.Remove(m.Path);
                m.Path = $"{Path}/{m.Name}";
                CabinetfsManager.Paths.Add(m.Path, m.Id);
                m.Save();
            }
        }

        CabinetfsManager.SavePaths();
    }

    /// <summary>
    /// Deletes this folder and all of its items.
    /// </summary>
    public override void Delete()
    {
        if (FileAccess.FileExists($"user://Users/{RecordManager.CurrentUser}/Files/{Id}.json"))
        {
            foreach (File m in CabinetfsManager.GetFolderItems(Path))
            {
                if (m.Type == "Folder")
                {
                    Folder ha = CabinetfsManager.LoadFolder(m.Id);
                    // haha recursion
                    ha.Delete();
                } else
                    m.Delete();
            }

            DirAccess.RemoveAbsolute($"user://Users/{RecordManager.CurrentUser}/Files/{Id}.json");
            CabinetfsManager.Paths.Remove(Path);
            CabinetfsManager.SavePaths();            
        } else
            GD.PushError("File not saved yet!");
    }

    /// <summary>
    /// Loads a file inside this folder.
    /// </summary>
    /// <param name="path">The path of the file, example: <c>FolderInsideThis/File</c></param>
    /// <returns>The file loaded.</returns>
    public File LoadLocalFile(string path)
    {
        string actualPath = $"{Path}/{path}";
        return CabinetfsManager.LoadFile(CabinetfsManager.GetId(actualPath));
    }
    
    /// <summary>
    /// Loads a folder inside this folder.
    /// </summary>
    /// <param name="path">The path of the folder, example: <c>FolderInsideThis/OtherFolder</c></param>
    /// <returns>The folder loaded.</returns>
    public Folder LoadLocalFolder(string path)
    {
        string actualPath = $"{Path}/{path}";
        return CabinetfsManager.LoadFolder(CabinetfsManager.GetId(actualPath));
    }

    /// <summary>
    /// Changes the parent of this folder and all of its items.
    /// </summary>
    /// <param name="parent">The ID of the new parent.</param>
    public override void Move(string parent)
    {
        Parent = parent;

        CabinetfsManager.Paths.Remove(Path);

        if (Parent != "root")
        {
            File m = CabinetfsManager.LoadFile(Parent);
            Path = $"{m.Path}/{Name}";
        } else
            Path = $"/{Name}";

        Save();

        foreach (File m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadFolder(m.Id);
                // haha recursion
                ha.Move(Id);
            }
            else
            {
                m.Parent = Id;
                CabinetfsManager.Paths.Remove(m.Path);
                m.Path = $"{Path}/{m.Name}";
                CabinetfsManager.Paths.Add(m.Path, m.Id);
                m.Save();
            }
        }

        CabinetfsManager.SavePaths();
    }
}
