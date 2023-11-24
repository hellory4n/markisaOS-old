using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// A cabinetfs folder.
/// </summary>
public partial class Folder : CabinetfsFile
{
    /// <summary>
    /// Copies this folder and all of its items.
    /// </summary>
    /// <param name="name">The name of the new folder.</param>
    /// <param name="parent">The ID of the parent of the new folder.</param>
    /// <returns>The ID of the copied folder.</returns>
    public override string Copy(string name, string parent = null)
    {
        // MemberwiseClone() is no worky xd
        var gaming = JsonConvert.DeserializeObject<Folder>(
            JsonConvert.SerializeObject(this)
        );

        gaming.Name = name;
        gaming.Parent = parent;
        gaming.Id = CabinetfsManager.GenerateID();

        if (parent != "root")
        {
            CabinetfsFile m = CabinetfsManager.LoadById<CabinetfsFile>(parent);
            gaming.Path = $"{m.Path}/{name}";
        }
        else
            gaming.Path = $"/{name}";

        gaming.Save();

        foreach (CabinetfsFile m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadById<Folder>(m.Id);
                // haha recursion
                ha.Copy(ha.Name, gaming.Id);
            }
            else
                m.Copy(m.Name, gaming.Id);
        }

        return gaming.Id;
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
            CabinetfsFile m = CabinetfsManager.LoadById<CabinetfsFile>(Parent);
            Path = $"{m.Path}/{name}";
        }
        else
            Path = $"/{name}";

        Save();

        foreach (CabinetfsFile m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadById<Folder>(m.Id);
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
        if (FileAccess.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json"))
        {
            foreach (CabinetfsFile m in CabinetfsManager.GetFolderItems(Path))
            {
                if (m.Type == "Folder")
                {
                    Folder ha = CabinetfsManager.LoadById<Folder>(m.Id);
                    // haha recursion
                    ha.Delete();
                } else
                    m.Delete();
            }

            DirAccess.RemoveAbsolute($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            CabinetfsManager.Paths.Remove(Path);
            CabinetfsManager.SavePaths();            
        } else
            GD.PushError("File not saved yet!");
    }

    /// <summary>
    /// Loads a file inside this folder.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="path">The path of the file, example: <c>FolderInsideThis/File</c></param>
    /// <returns>The file loaded.</returns>
    public T LoadLocal<T>(string path) where T : CabinetfsFile
    {
        string actualPath = $"{Path}/{path}";
        return CabinetfsManager.Load<T>(actualPath);
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
            CabinetfsFile m = CabinetfsManager.LoadById<CabinetfsFile>(Parent);
            Path = $"{m.Path}/{Name}";
        } else
            Path = $"/{Name}";

        Save();

        foreach (CabinetfsFile m in CabinetfsManager.GetFolderItems(Path))
        {
            if (m.Type == "Folder")
            {
                Folder ha = CabinetfsManager.LoadById<Folder>(m.Id);
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
