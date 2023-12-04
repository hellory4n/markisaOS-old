using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class WallpaperThing : ItemList
{
    Record<DashboardConfig> Record = new();

    public override void _Ready()
    {
        base._Ready();
        foreach (var wallpaerper in Record.Data.Wallpapers)
        {
            Texture2D hi = GD.Load<Texture2D>(wallpaerper);
            AddItem("", hi);
        }
    }

    public void ApplyThemeAndShit(int index)
    {
        Texture2D newWallpaperOmgmomogmogmo = GetItemIcon(index);
        TextureRect wallOfPaper = GetNode<TextureRect>("/root/Dashboard/Wallpaper");
        wallOfPaper.Texture = newWallpaperOmgmomogmogmo;

        Record.Data.Wallpaper = newWallpaperOmgmomogmogmo.ResourcePath;
        Record.Save();
    }
}
