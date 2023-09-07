using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Folder : BaseLelfs {
    public new string Type = "Folder";
    public List<string> Items = new List<string>();

    // yes :)
    public Folder(string name, string parent = null) : base(name, parent) {}

    public Folder CopyFolder(string name, string parent = null) {
        Folder gaming = (Folder)MemberwiseClone();
        gaming.Name = name;
        gaming.Parent = parent;
        gaming.NewId();

        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            gaming.Path = $"{m.Path}/{name}";
        } else {
            gaming.Path = $"/{name}";
        }

        string[] funni = gaming.Items.ToArray();
        gaming.Items.Clear();

        // we have to save it twice cuz yes please help me
        gaming.Save();

        foreach (string item in funni) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(item);
            BaseLelfs pain = m.Copy<BaseLelfs>(m.Name, gaming.Id);
            gaming.Items.Add(pain.Id);
        }

        gaming.Save();
        return gaming;
    }

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
