using Godot;
using System;
using Newtonsoft.Json;
using System.Linq;

/// <summary>
/// Manages settings and progress both for users, and settings that apply for all users.
/// </summary>
public partial class SavingManager : Node
{
    public static string CurrentUser = "";

    // setups settings files, in _EnterTree() since it needs to be ready before the things that use the settings
    public override void _EnterTree()
    {
        base._EnterTree();
        DirAccess.MakeDirRecursiveAbsolute("user://Settings/");

        if (!FileAccess.FileExists("user://Settings/DisplaySettings.json"))
        {
            using var displaySettings = FileAccess.Open("user://Settings/DisplaySettings.json", FileAccess.ModeFlags.Write);
            DisplaySettings thing = new()
            {
                Resolution = DisplayServer.ScreenGetSize()
            };

            // you won't do the the mobile setup thing on a pc
            if (OS.GetName() != "Android")
                thing.AlreadySetup = true;

            displaySettings.StoreString(
                JsonConvert.SerializeObject(thing)
            );
        }

        // yes
        if (!FileAccess.FileExists("user://Settings/InstallerInfo.json"))
        {
            using var installerInfo = FileAccess.Open("user://Settings/InstallerInfo.json", FileAccess.ModeFlags.Write);
            installerInfo.StoreString(
                JsonConvert.SerializeObject(new InstallerInfo())
            );
            installerInfo.Close();
        }
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The name of the new user.</param>
    /// <param name="info">The photo and picture of the new user.</param>
    public static void NewUser(string user, UserInfo info)
    {
        CurrentUser = user;
        DirAccess.MakeDirRecursiveAbsolute($"user://Users/{user}");
        using var file = FileAccess.Open($"user://Users/{user}/BasicInfo.json", FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(new BasicUser())
        );

        using var pain = FileAccess.Open($"user://Users/{user}/UserInfo.json", FileAccess.ModeFlags.Write);
        pain.StoreString(
            JsonConvert.SerializeObject(info)
        );

        using var bruh = FileAccess.Open($"user://Users/{user}/UserLelsktop.json", FileAccess.ModeFlags.Write);
        bruh.StoreString(
            JsonConvert.SerializeObject(new UserLelsktop())
        );

        using var j = FileAccess.Open($"user://Users/{user}/InstalledApps.json", FileAccess.ModeFlags.Write);
        j.StoreString(
            JsonConvert.SerializeObject(new InstalledApps())
        );

        using var suffer = FileAccess.Open($"user://Users/{user}/QuickLaunch.json", FileAccess.ModeFlags.Write);
        suffer.StoreString(
            JsonConvert.SerializeObject(new QuickLaunch())
        );

        using var sdfhjjhgf = FileAccess.Open($"user://Users/{user}/SocialStuff.json", FileAccess.ModeFlags.Write);
        sdfhjjhgf.StoreString(
            JsonConvert.SerializeObject(new SocialStuff())
        );

        using var fgbfg = FileAccess.Open($"user://Users/{user}/LelsktopPinboard.json", FileAccess.ModeFlags.Write);
        fgbfg.StoreString(
            JsonConvert.SerializeObject(new LelsktopPinboard())
        );

        // setup the filesystem
        DirAccess.MakeDirRecursiveAbsolute($"user://Users/{user}/Files/");
        using var haha = FileAccess.Open($"user://Users/{user}/Files/root.json", FileAccess.ModeFlags.Write);
        haha.StoreString("{\"$type\":\"LelfsRoot, lelcubeOS\",\"Id\":\"root\",\"Parent\":null,\"Name\":\"\",\"Metadata\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib\"},\"Path\":\"/\",\"Type\":\"Root\",\"Data\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib\"}}");

        LelfsManager.UpdatePaths();
        LelfsRoot.CreateRoot();
        LelfsManager.NewFileStructure();
    }

    /// <summary>
    /// Loads data from a user.
    /// </summary>
    /// <typeparam name="T">The type of data to load.</typeparam>
    /// <param name="user">The user to load data from.</param>
    /// <returns>The data loaded.</returns>
    public static T Load<T>(string user)
    {
        string filename = "";
        switch (typeof(T).Name)
        {
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
            case nameof(SocialStuff):
                filename = $"user://Users/{user}/SocialStuff.json";
                break;
            case nameof(LelsktopPinboard):
                filename = $"user://Users/{user}/LelsktopPinboard.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                return default;
        }

        if (FileAccess.FileExists(filename))
        {
            using var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            });
            return m;
        } else
        {
            using var file = FileAccess.Open(filename, FileAccess.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(Activator.CreateInstance<T>(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            }));
            return Activator.CreateInstance<T>();
        }
    }

    /// <summary>
    /// Saves new data from a user.
    /// </summary>
    /// <typeparam name="T">The type of data to save.</typeparam>
    /// <param name="user">The user to save data from.</param>
    /// <param name="data">The new data to save.</param>
    public static void Save<T>(string user, T data)
    {
        string filename = "";
        switch (typeof(T).Name)
        {
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
            case nameof(SocialStuff):
                filename = $"user://Users/{user}/SocialStuff.json";
                break;
            case nameof(LelsktopPinboard):
                filename = $"user://Users/{user}/LelsktopPinboard.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                return;
        }

        if (FileAccess.FileExists(filename))
        {
            using var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
            file.StoreString(JsonConvert.SerializeObject(data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            }));
        }
        else
            GD.PushError($"Failed to save data from user \"{user}\", are you sure it exists?");
    }

    /// <summary>
    /// Loads settings that apply across all users.
    /// </summary>
    /// <typeparam name="T">The type of settings data to load.</typeparam>
    /// <returns>The loaded settings data.</returns>
    public static T LoadSettings<T>()
    {
        string filename = "";
        switch (typeof(T).Name)
        {
            case nameof(DisplaySettings):
                filename = $"user://Settings/DisplaySettings.json";
                break;
            case nameof(InstallerInfo):
                filename = $"user://Settings/InstallerInfo.json";
                break;
            default:
                GD.PushError("Invalid settings type!");
                return default;
        }

        if (FileAccess.FileExists(filename))
        {
            using var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            });
            return m;
        }
        else
        {
            GD.PushError($"Failed to load settings.");
            return default;
        }
    }

    /// <summary>
    /// Saves new settings that apply across all users.
    /// </summary>
    /// <typeparam name="T">The type of settings data to save.</typeparam>
    /// <param name="data">The new settings data to save.</param>
    public static void SaveSettings<T>(T data)
    {
        string filename = "";
        switch (typeof(T).Name)
        {
            case nameof(DisplaySettings):
                filename = $"user://Settings/DisplaySettings.json";
                break;
            case nameof(InstallerInfo):
                filename = $"user://Settings/InstallerInfo.json";
                break;
            default:
                GD.PushError("Invalid settings type!");
                return;
        }

        if (FileAccess.FileExists(filename))
        {
            using var file = FileAccess.Open(filename, FileAccess.ModeFlags.Read);
            file.StoreString(JsonConvert.SerializeObject(data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            }));
        } else
            GD.PushError($"Failed to save settings.");
    }

    /// <summary>
    /// Converts a save to the latest version.
    /// </summary>
    /// <param name="user">The user to convert.</param>
    public static void ConvertOldUser(string user)
    {
        var version = Load<BasicUser>(user);

        if (version.MajorVersion == 0 && version.MinorVersion < 7)
            UserConverter.ReallyOldVersions(user);
        else if (version.MajorVersion == 0 && version.MinorVersion == 7)
            UserConverter.Convert0_7(user);
        else if (version.MajorVersion == 0 && version.MinorVersion == 8)
            UserConverter.Convert0_8(user);
        else if (version.MajorVersion == 0 && version.MinorVersion == 9)
            UserConverter.Convert0_9(user);
    }
}