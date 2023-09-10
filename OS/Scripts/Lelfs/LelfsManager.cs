using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages Lelfs. NOTE: Filesystems are local to each user.
/// </summary>
public class LelfsManager : Node {
    /// <summary>
    /// The list of paths available in the filesystem. The key is the path, and the value is the ID.
    /// </summary>
    public static Dictionary<string, string> Paths = new Dictionary<string, string>();

    /// <summary>
    /// Reloads the paths available in the filesystem, or creates a new one if the user doesn't have the files for the filesystem yet.
    /// </summary>
    public static void UpdatePaths() {
        File file = new File();
        if (file.FileExists($"user://Users/{SavingManager.CurrentUser}/Files/db.json")) {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Read);
            Paths = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                file.GetAsText()
            );
            file.Close();
        } else {
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(Paths)
            );
            file.Close();
        }
    }

    /// <summary>
    /// Saves the paths available in the filesystem.
    /// </summary>
    public static void SavePaths() {
        File file = new File();
        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Paths)
        );
        file.Close();
    }

    /// <summary>
    /// Loads a file from its path.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="path">The path of the file.</param>
    /// <returns>The loaded file.</returns>
    public static T Load<T>(string path) where T : BaseLelfs {
        if (Paths.ContainsKey(path)) {
            File file = new File();
            file.Open($"user://Users/{SavingManager.CurrentUser}/Files/{Paths[path]}.json", File.ModeFlags.Read);
            T pain = JsonConvert.DeserializeObject<T>(
                file.GetAsText()
            );
            pain.Id = Paths[path];
            file.Close();
            return pain;
        } else {
            GD.PushError($"No file was found at path \"{path}\"!");
            return default;
        }
    }

    /// <summary>
    /// Loads a file from its ID instead of its path.
    /// </summary>
    /// <typeparam name="T">The type of the file.</typeparam>
    /// <param name="id">The ID of the file.</param>
    /// <returns>The loaded file.</returns>
    public static T LoadById<T>(string id) where T : BaseLelfs {
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

    /// <summary>
    /// Checks if a file exists from its path.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>Whether or not the file exists.</returns>
    public static bool FileExists(string path) {
        if (Paths.ContainsKey(path)) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Returns the ID of a file from its path.
    /// </summary>
    /// <param name="path">The path of a file.</param>
    /// <returns>The ID of the file.</returns>
    public static string PermanentPath(string path) {
        BaseLelfs jbkfjbg = Load<BaseLelfs>(path);
        return jbkfjbg.Id;
    }
}