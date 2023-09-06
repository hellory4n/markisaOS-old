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
        Folder gaming = this;
        gaming.Name = name;
        gaming.Parent = parent;
        gaming.Save();

        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            Path = $"{m.Path}/{name}";
        } else {
            Path = $"/{name}";
        }

        foreach (string item in gaming.Items) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(item);
            m.Parent = gaming.Id;
            m.Path = $"{gaming.Path}/{m.Name}";
            m.Save();
        }

        return gaming;
    }

    public new void Rename(string name) {
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
            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path);
            LelfsManager.SavePaths();

            foreach (var item in Items) {
                BaseLelfs bruh = LelfsManager.LoadById<BaseLelfs>(item);
                bruh.Delete();
            }
        } else {
            GD.PushError("File not saved yet!");
        }
    }
}
