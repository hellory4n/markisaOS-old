using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Base type for all data in Lelfs.
/// </summary>
public class BaseLelfs {
    /// <summary>
    /// The unique ID used by the lelfs object, a number encoded in base 64 with 20 characters.
    /// </summary>
    public readonly string Id;
    /// <summary>
    /// The parent of the lelfs object.
    /// </summary>
    public BaseLelfs Parent;
    /// <summary>
    /// The name of the lelfs object. Slashes (/ and \) are not allowed.
    /// </summary>
    public string Name;
    /// <summary>
    /// The metadata for the lelfs object.
    /// </summary>
    public Dictionary<string, object> Metadata = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a lelfs object.
    /// </summary>
    /// <param name="parent">The parent of the lelfs object. If no argument is provided, it will be in the root of the filesystem.</param>
    /// <param name="name">The name of the lelfs object.</param>
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
    }

    /// <summary>
    /// Saves the file into the disk, or creates a new file if it doesn't exist yet.
    /// </summary>
    public void Save() {
        Directory directory = new Directory();
        File file = new File();
        directory.MakeDirRecursive($"user://Users/{SavingManager.CurrentUser}/Files/");
        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Id}.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(this)
        );
        file.Close();
    }

    /// <summary>
    /// Load a file with its ID.
    /// </summary>
    /// <typeparam name="T">The type of the file, must inherit from BaseLelfs.</typeparam>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The loaded file.</returns>
    public static T Load<T>(string id) where T : BaseLelfs {
        File file = new File();
        if (file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/{id}.json")) {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{id}.json", File.ModeFlags.Read);
            T yes = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            return yes;
        } else {
            GD.PushError($"File with ID \"{id}\" doesn't exist!");
            return default;
        }
    }
}
