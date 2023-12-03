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
    public string Theme = "res://Assets/Themes/HighPeaks-Blue/Theme.tres";
    public List<Package> AllApps = DefaultAppGenerator5000.Generate();
    public List<Package> QuickLaunch = new();
    public List<Package> Startup = new();
    public Dictionary<string, PinboardItem> Pinboard = new();
    public DashboardMode Mode = DashboardMode.Desktop;
    /// <summary>
    /// A list of paths to the wallpapers the user has.
    /// </summary>
    public List<string> Wallpapers = new(new string[]{
        ""
    });

    public DashboardConfig() 
    {
        if (OS.GetName() == "Android")
            Mode = DashboardMode.Mobile;
        else
            Mode = DashboardMode.Desktop;
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