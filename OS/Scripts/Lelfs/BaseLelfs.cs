using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BaseLelfs {
    public string Id;
    public string Parent;
    public string Name;
    public Dictionary<string, object> Metadata = new Dictionary<string, object>();
    public string Path;
    public readonly string Type = "BaseLelfs";

    public BaseLelfs(string name, string parent = null) {
        // very illegal names
        if (name.Contains("/"))
            GD.PushError("Filenames can't include forward slashes (/)");

        // setup stuff :)
        Parent = parent;
        Name = name;

        // yes :)
        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            Path = $"{m.Path}/{name}";
        } else {
            Path = $"/{name}";
        }
    }

    public void NewId() {
        Id = "";
        string[] possibleCharacters = {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "-", "_"
        };
        Random random = new Random();
        for (int i = 0; i < 20; i++) {
            Id += possibleCharacters[random.Next(0, 63)];
        }
    }

    public void Save() {
        Directory directory = new Directory();
        File file = new File();
        directory.MakeDirRecursive($"user://Users/{SavingManager.CurrentUser}/Files/");

        if (!file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json") &&
        !LelfsManager.Paths.ContainsKey(Path)) {
            LelfsManager.Paths.Add(Path, Id);
            LelfsManager.SavePaths();
        }

        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(this)
        );
        file.Close();
    }

    public T LoadLocal<T>(string path) where T : BaseLelfs {
        string actualPath = $"{Path}/{path}";
        if (LelfsManager.Paths.ContainsKey(actualPath)) {
            File file = new File();
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{LelfsManager.Paths[actualPath]}.json",
                File.ModeFlags.Read);
            T pain = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            pain.Id = LelfsManager.Paths[actualPath];
            file.Close();
            return pain;
        } else {
            GD.PushError($"No file was found at path \"{actualPath}\"!");
            return default;
        }
    }

    public T Copy<T>(string name, string parent = null) where T : BaseLelfs {
        var gaming = (T)MemberwiseClone();
        gaming.Name = name;
        gaming.Parent = parent;
        gaming.NewId();

        if (parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(parent);
            gaming.Path = $"{m.Path}/{gaming.Name}";
        } else {
            gaming.Path = $"/{gaming.Name}";
        }

        if (!LelfsManager.Paths.ContainsKey(gaming.Path)) {
            LelfsManager.Paths.Add(gaming.Path, gaming.Id);
            LelfsManager.SavePaths();
        }

        gaming.Save();
        return gaming;
    }

    public void Rename(string name) {
        if (Name == name)
            return;

        Name = name;

        LelfsManager.Paths.Remove(Path);

        if (Parent != null) {
            BaseLelfs m = LelfsManager.LoadById<BaseLelfs>(Parent);
            Path = $"{m.Path}/{name}";
        } else {
            Path = $"/{name}";
        }
        
        Save();

        LelfsManager.Paths.Add(Path, Id);
        LelfsManager.SavePaths();
    }

    public void Delete() {
        Directory directory = new Directory();
        if (directory.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json")) {
            directory.Remove($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json");
            LelfsManager.Paths.Remove(Path);
            LelfsManager.SavePaths();
        } else {
            GD.PushError("File not saved yet!");
        }
    }
}
