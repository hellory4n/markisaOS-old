using Godot;
using System;
using System.Collections.Generic;

namespace Kickstart.Records;

/// <summary>
/// Stores information about the user's dashboard settings.
/// </summary>
public partial struct DashboardConfig : IRecordData
{
    public readonly string GetFilename() { return "%user/DashboardConfig.json"; }

	/// <summary>
    /// A path to the wallpaper used by the user.
    /// </summary>
    public string Wallpaper = "res://Assets/Wallpapers/HighPeaks.jpg";
    /// <summary>
    /// A path to the theme used by the user
    /// </summary>
    public string Theme = "res://Assets/Themes/HighPeaks-Dark-Blue/Theme.tres";
    public List<Package> AllApps = DefaultAppGenerator5000.Generate();
    public List<Package> QuickLaunch = new();
    public List<Package> Startup = new();
    public Dictionary<string, PinboardItem> Pinboard = new();
    public DashboardMode Mode = DashboardMode.Desktop;
    /// <summary>
    /// A list of paths to the wallpapers the user has.
    /// </summary>
    public List<string> Wallpapers = new() {
        "res://Assets/Wallpapers/HighPeaks.jpg",
        "res://Assets/Wallpapers/Flowers.jpg",
        "res://Assets/Wallpapers/Beaches.jpg",
        "res://Assets/Wallpapers/Space.jpg",
        "res://Assets/Wallpapers/Mountains.jpg",
        "res://Assets/Wallpapers/Aurora.jpg",
        "res://Assets/Wallpapers/Forest.jpg",
        "res://Assets/Wallpapers/Lake.jpg",
        "res://Assets/Wallpapers/Coffee.jpg",
        "res://Assets/Wallpapers/GreenPeaks.jpg"
    };
    public WallpaperModeEnum WallpaperMode = WallpaperModeEnum.Cover;
    /// <summary>
    /// The themes the user has installed, with the key being the name, and the value being the path.
    /// </summary>
    public Dictionary<string, string> Themes = new() {
        {"markisaOS Dark Black", "res://Assets/Themes/HighPeaks-Dark-Black/Theme.tres"},
        {"markisaOS Dark Blue", "res://Assets/Themes/HighPeaks-Dark-Blue/Theme.tres"},
        {"markisaOS Dark Green", "res://Assets/Themes/HighPeaks-Dark-Green/Theme.tres"},
        {"markisaOS Dark Orange", "res://Assets/Themes/HighPeaks-Dark-Orange/Theme.tres"},
        {"markisaOS Dark Pink", "res://Assets/Themes/HighPeaks-Dark-Pink/Theme.tres"},
        {"markisaOS Dark Purple", "res://Assets/Themes/HighPeaks-Dark-Purple/Theme.tres"},
        {"markisaOS Dark Red", "res://Assets/Themes/HighPeaks-Dark-Red/Theme.tres"},
        {"markisaOS Dark Yellow", "res://Assets/Themes/HighPeaks-Dark-Yellow/Theme.tres"},
        {"markisaOS High Contrast Dark", "res://Assets/Themes/HighPeaks-HighContrast-Dark/Theme.tres"},
    };

    public DashboardConfig() 
    {
        if (OS.GetName() == "Android")
            Mode = DashboardMode.Mobile;
        else
            Mode = DashboardMode.Desktop;
    }

    public enum WallpaperModeEnum
    {
        Center,
        Cover,
        Stretch,
        KeepAspect
    }
}

public enum DashboardMode
{
    Desktop,
    Mobile,
}

public partial struct PinboardItem
{
    public bool IsStickyNote = false;
    public Vector2 Position = new(0, 0);
    public double Scale = 1;
    public string Text = "";
    public string TexturePath = "";

    public PinboardItem() {}
}