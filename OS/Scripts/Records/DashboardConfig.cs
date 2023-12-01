using Godot;
using System;
using System.Collections.Generic;

namespace Kickstart.Records;

/// <summary>
/// Stores information about the user's dashboard settings.
/// </summary>
public partial record struct DashboardConfig : IRecord
{
    public override string GetFilename()
    {
        
    }

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

    public DashboardConfig() {}
}

public enum DashboardMode
{
    Desktop,
    Mobile,
}

public partial record PinboardItem
{
    public bool IsStickyNote = false;
    public Vector2 Position = new(0, 0);
    public double Scale = 1;
    public string Text = "";
    public string TexturePath = "";
}