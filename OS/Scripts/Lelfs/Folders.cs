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
        gaming.Id = "";

        // make a new id :)
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

        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            gaming.Path = $"{m.Path}/{name}";
        } else {
            gaming.Path = $"/{name}";
        }

        gaming.Items.Clear();
        foreach (string item in Items) {
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
