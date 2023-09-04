using Godot;
using System;
using Newtonsoft.Json;

public class SavingManager : Node {
    public static string CurrentUser = "";

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
            if (OS.GetName() != "Android")
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

        File bruh = new File();
        bruh.Open($"user://Users/{user}/UserLelsktop.json", File.ModeFlags.Write);
        bruh.StoreString(
            JsonConvert.SerializeObject(new UserLelsktop())
        );
        bruh.Close();

        File j = new File();
        j.Open($"user://Users/{user}/InstalledApps.json", File.ModeFlags.Write);
        j.StoreString(
            JsonConvert.SerializeObject(new InstalledApps {
                All = new Lelapp[]{
                    new Lelapp("Test App", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/App.png", "res://Apps/WindowManagerTest/WindowManagerTest.tscn"),
                    new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn")
                },
                Accessories = new Lelapp[]{
                    new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn")
                },
                System = new Lelapp[]{
                    new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn")
                }
            })
        );
        j.Close();

        File suffer = new File();
        suffer.Open($"user://Users/{user}/QuickLaunch.json", File.ModeFlags.Write);
        suffer.StoreString(
            JsonConvert.SerializeObject(new QuickLaunch())
        );
        suffer.Close();
    }

    public static T Load<T>(string user) {
        string filename = "";
        switch (typeof(T).Name) {
            case nameof(BasicUser):
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case nameof(UserInfo):
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            case nameof(UserLelsktop):
                filename = $"user://Users/{user}/UserLelsktop.json";
                break;
            case nameof(InstalledApps):
                filename = $"user://Users/{user}/InstalledApps.json";
                break;
            case nameof(QuickLaunch):
                filename = $"user://Users/{user}/QuickLaunch.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                return default;
        }

        File file = new File();
        if (file.FileExists(filename)) {
            file.Open(filename, File.ModeFlags.Read);
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText());
            file.Close();
            return m;
        } else {
            file.Open(filename, File.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(Activator.CreateInstance<T>())
            );
            return Activator.CreateInstance<T>();
        }
    }

    public static void Save<T>(string user, T data) {
        string filename = "";
        switch (typeof(T).Name) {
            case nameof(BasicUser):
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case nameof(UserInfo):
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            case nameof(UserLelsktop):
                filename = $"user://Users/{user}/UserLelsktop.json";
                break;
            case nameof(InstalledApps):
                filename = $"user://Users/{user}/UserLelsktop.json";
                break;
            case nameof(QuickLaunch):
                filename = $"user://Users/{user}/QuickLaunch.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                return;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Write) == Error.Ok) {
            file.StoreString(JsonConvert.SerializeObject(data));
            file.Close();
        } else {
            GD.PushError($"Failed to save data from user \"{user}\", are you sure it exists?");
        }
    }

    public static T LoadSettings<T>() {
        string filename = "";
        switch (typeof(T).Name) {
            case nameof(DisplaySettings):
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

    public static void SaveSettings<T>(T data) {
        string filename = "";
        switch (typeof(T).Name) {
            case nameof(DisplaySettings):
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