using Godot;
using System;
using Newtonsoft.Json;

public class SavingManager : Node {
    public static string CurrentUser = "";
    public enum Info {
        BasicInfo,
        UserInfo
    }
    public enum Settings {
        DisplaySettings
    }

    // setups settings files, in _EnterTree() since it needs to be ready before the things that use the settings
    public override void _EnterTree() {
        base._EnterTree();
        Directory dir = new Directory();
        dir.MakeDirRecursive("user://Settings/");

        File displaySettings = new File();

        if (!displaySettings.FileExists("user://Settings/DisplaySettings.json")) {
            displaySettings.Open("user://Settings/DisplaySettings.json", File.ModeFlags.Write);
            DisplaySettings thing = new DisplaySettings {
                Resolution = OS.GetScreenSize()
            };

            // you won't do the the mobile setup thing on a pc
            if (OS.GetName() == "Android")
                thing.AlreadySetup = true;

            displaySettings.StoreString(
                JsonConvert.SerializeObject(thing)
            );
            displaySettings.Close();
        }
    }

    public static void NewUser(string user, UserInfo info) {
        File file = new File();
        Directory dir = new Directory();
        dir.MakeDirRecursive($"user://Users/{user}/");
        file.Open($"user://Users/{user}/BasicInfo.json", File.ModeFlags.Write);
        BasicUser m = new BasicUser();
        file.StoreString(
            JsonConvert.SerializeObject(m)
        );
        file.Close();

        File pain = new File();
        pain.Open($"user://Users/{user}/UserInfo.json", File.ModeFlags.Write);
        pain.StoreString(
            JsonConvert.SerializeObject(info)
        );
        pain.Close();
    }    

    public static T Load<T>(string user, Info info) {
        string filename = "";
        switch (info) {
            case Info.BasicInfo:
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case Info.UserInfo:
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Read) == Error.Ok) {
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText());
            file.Close();
            return m;
        } else {
            GD.PushError($"Failed to load data from user \"{user}\", are you sure it exists?");
            return default;
        }
    }

    public static void Save<T>(string user, Info info, T data) {
        string filename = "";
        switch (info) {
            case Info.BasicInfo:
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case Info.UserInfo:
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Write) == Error.Ok) {
            file.StoreString(JsonConvert.SerializeObject(data));
            file.Close();
        } else {
            GD.PushError($"Failed to save data from user \"{user}\", are you sure it exists?");
        }
    }

    public static T LoadSettings<T>(Settings settings) {
        string filename = "";
        switch (settings) {
            case Settings.DisplaySettings:
                filename = $"user://Settings/DisplaySettings.json";
                break;
            default:
                GD.PushError("Invalid settings type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Read) == Error.Ok) {
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText());
            file.Close();
            return m;
        } else {
            GD.PushError($"Failed to load settings.");
            return default;
        }
    }

    public static void SaveSettings<T>(Settings settings, T data) {
        string filename = "";
        switch (settings) {
            case Settings.DisplaySettings:
                filename = $"user://Settings/DisplaySettings.json";
                break;
            default:
                GD.PushError("Invalid settings type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Write) == Error.Ok) {
            file.StoreString(JsonConvert.SerializeObject(data));
            file.Close();
        } else {
            GD.PushError($"Failed to save settings.");
        }
    }
}
