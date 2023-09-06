using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BaseLelfs {
    public string Id;
    public BaseLelfs Parent;
    public string Name;
    public Dictionary<string, object> Metadata = new Dictionary<string, object>();
    public string Path;
    public readonly string Type = "BaseLelfs";

    public BaseLelfs(string name, BaseLelfs parent = null) {
        // generate the id
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

        // setup stuff :)
        Parent = parent;
        Name = name;

        // yes :)
        if (parent != null) {
            Path = $"{parent.Path}/{name}";
        } else {
            Path = $"/{name}";
        }
    }

    public BaseLelfs(string name, BaseLelfs parent = null, bool newFile = false) : this(name, parent)
    {
    }

    public void Save() {
        Directory directory = new Directory();
        File file = new File();
        directory.MakeDirRecursive($"user://Users/{SavingManager.CurrentUser}/Files/");

        if (!file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json"))
            LelfsManager.AddPath(Path, Id);

        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(this)
        );
        file.Close();
    }
}
