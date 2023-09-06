using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class LelfsManager : Node {
    public static Dictionary<string, string> Paths = new Dictionary<string, string>();

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

    public static void SavePaths() {
        File file = new File();
        file.Open($"user://Users/{SavingManager.CurrentUser}/Files/db.json", File.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Paths)
        );
        file.Close();
    }

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

    public static bool FileExists(string path) {
        if (Paths.ContainsKey(path))
            return true;
        else
            return false;
    }

    public static string PermanentPath(string path) {
        BaseLelfs jbkfjbg = Load<BaseLelfs>(path);
        return jbkfjbg.Id;
    }
}