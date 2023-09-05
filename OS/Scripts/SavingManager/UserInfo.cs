/// <summary>
/// Stores the version of the game used by a user. Useful when converting saves between versions.
/// </summary>
public class BasicUser {
    /// <summary>
    /// Increased with major new versions, with a lot of new stuff, and that probably breaks old stuff.
    /// </summary>
    public int MajorVersion = 0;
    /// <summary>
    /// Increased with smaller new versions, usually some new features and improvements.
    /// </summary>
    public int MinorVersion = 7;
    /// <summary>
    /// Increased with very small new versions, such as bug fixes.
    /// </summary>
    public int PatchVersion = 0;
}

/// <summary>
/// Stores the photo and lelnet username of a user.
/// </summary>
public class UserInfo {
    /// <summary>
    /// The photo used by the user. I should probably use an enum or a file path.
    /// </summary>
    public string Photo = "Lelcube";
    /// <summary>
    /// The lelnet username used by the user. Only accepts lowercase characters, numbers, underscore (_), and periods (.)
    /// </summary>
    public string LelnetUsername = "john.doe";
}

/// <summary>
/// Stores the appearance settings of a user.
/// </summary>
public class UserLelsktop {
    /// <summary>
    /// A file path to the wallpaper used by the user.
    /// </summary>
    public string Wallpaper = "res://Assets/Wallpapers/HighPeaks.jpg";
    /// <summary>
    /// The theme used by the user. It's actually just a part of the file path to the actual theme, e.g. res://Assets/Themes/insert value here/Theme.tres
    /// </summary>
    public string Theme = "Leltheme-Dark-Blue";
}

/// <summary>
/// Type commonly used for listing apps.
/// </summary>

public class Lelapp {
    /// <summary>
    /// The name of the app.
    /// </summary>
    public string Name = "App";
    /// <summary>
    /// A file path to the icon of the app.
    /// </summary>
    public string Icon = "res://Assets/Themes/Leltheme-Dark-Blue/Icons/App.png";
    /// <summary>
    /// A file path to the scene of the app's window.
    /// </summary>
    public string Scene = "res://Apps/WindowManagerTest/WindowManagerTest.tscn";

    public Lelapp(string name, string icon, string scene) {
        Name = name;
        Icon = icon;
        Scene = scene;
    }
}

/// <summary>
/// Used by the app menu to list installed apps.
/// </summary>
public class InstalledApps {
    /// <summary>
    /// Every single app installed in the device.
    /// </summary>
    public Lelapp[] All = new Lelapp[]{};
    /// <summary>
    /// Basically categorizing apps as "Other"
    /// </summary>
    public Lelapp[] Accessories = new Lelapp[]{};
    /// <summary>
    /// Apps used for modding and stuff.
    /// </summary>
    public Lelapp[] Development = new Lelapp[]{};
    /// <summary>
    /// Videogames.
    /// </summary>
    public Lelapp[] Games = new Lelapp[]{};
    /// <summary>
    /// Apps used for doing stuff with graphics.
    /// </summary>
    public Lelapp[] Graphics = new Lelapp[]{};
    /// <summary>
    /// Apps related to the world wide web.
    /// </summary>
    public Lelapp[] Internet = new Lelapp[]{};
    /// <summary>
    /// Apps that do stuff with media.
    /// </summary>
    public Lelapp[] Multimedia = new Lelapp[]{};
    /// <summary>
    /// Apps used in offices and stuff.
    /// </summary>
    public Lelapp[] Office = new Lelapp[]{};
    /// <summary>
    /// Apps that come with the system or something.
    /// </summary>
    public Lelapp[] System = new Lelapp[]{};
    /// <summary>
    /// Very small apps like a calculator and a photo viewer.
    /// </summary>
    public Lelapp[] Utilities = new Lelapp[]{};
}

/// <summary>
/// Used for storing the apps pinned in the dock.
/// </summary>
public class QuickLaunch {
    /// <summary>
    /// The apps in the quick launcher.
    /// </summary>
    public Lelapp[] Apps = new Lelapp[]{};
}