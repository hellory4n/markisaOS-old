using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// A lelfs folder.
/// </summary>
public class Folder : BaseLelfs {
    /// <summary>
    /// The IDs of the items in this folder.
    /// </summary>
    public List<string> Items = new List<string>();

    /// <summary>
    /// Initializes a Lelfs folder. NOTE: Use <c>NewId</c> after this if you're creating new files.
    /// </summary>
    /// <param name="name">The name of the folder.</param>
    /// <param name="parent">The ID of the parent of the folder.</param>
    public Folder(string name, string parent = null) : base(name, parent) {
        Type = "Folder";
    }

    /// <summary>
    /// Copies this folder and all of its items.
    /// </summary>
    /// <param name="name">The name of the new folder.</param>
    /// <param name="parent">The ID of the parent of the new folder.</param>
    /// <returns>The copied folder.</returns>
    public Folder CopyFolder(string name, string parent = null) {
        Folder gaming = (Folder)MemberwiseClone();
        gaming.Name = name;
        gaming.Parent = parent;

        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            gaming.Path = $"{m.Path}/{name}";
        } else {
            gaming.Path = $"/{name}";
        }

        string[] funni = gaming.Items.ToArray();
        gaming.Items.Clear();

        // new id for the thing :)
        gaming.Id = "";
        string[] possibleCharacters = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "-", "_"
        };
        Random random = new Random();
        for (int i = 0; i < 20; i++) {
            gaming.Id += possibleCharacters[random.Next(0, 63)];
        }

        // we have to save it twice cuz yes please help me
        gaming.Save();

        foreach (string item in funni) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(item);
            // TODO: update this when making new types
            switch (m.Type) {
                case "Picture":
                    Picture pain1 = m.Copy<Picture>(m.Name, gaming.Id);
                    gaming.Items.Add(pain1.Id);
                    break;
                case "AnimatedPicture":
                    AnimatedPicture pain2 = m.Copy<AnimatedPicture>(m.Name, gaming.Id);
                    gaming.Items.Add(pain2.Id);
                    break;
                case "Audio":
                    Audio pain3 = m.Copy<Audio>(m.Name, gaming.Id);
                    gaming.Items.Add(pain3.Id);
                    break;
                case "Video":
                    Video pain4 = m.Copy<Video>(m.Name, gaming.Id);
                    gaming.Items.Add(pain4.Id);
                    break;
                default:
                    BaseLelfs pain = m.Copy<BaseLelfs>(m.Name, gaming.Id);
                    gaming.Items.Add(pain.Id);
                    break;
            }
            
        }

        gaming.Save();
        return gaming;
    }

    /// <summary>
    /// Renames this folder and updates the paths of its items.
    /// </summary>
    /// <param name="name">The new name of the folder.</param>
    public new void Rename(string name) {
        if (Name == name)
            return;

        Name = name;

        LelfsManager.Paths.Remove(Path);

        if (Parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(Parent);
            Path = $"{m.Path}/{Name}";
        } else {
            Path = $"/{Name}";
        }

        Save();

        LelfsManager.Paths.Add(Path, Id);

        foreach (string item in Items) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(item);
            m.Parent = Path;
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
    public new void Delete() {
        Directory directory = new Directory();
        if (directory.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json")) {
            foreach (var item in Items) {
                BaseLelfs bruh = LelfsManager.LoadById<BaseLelfs>(item);
                bruh.Delete();
            }

            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path);
            LelfsManager.SavePaths();            
        } else {
            GD.PushError("File not saved yet!");
        }
    }
}
