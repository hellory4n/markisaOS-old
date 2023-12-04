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

        /*DashboardConfig.WallpaperModeEnum help;
        switch (WallpaperMode.Selected)
		{
			case 0: // center
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.KeepSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepCentered;
                help = DashboardConfig.WallpaperModeEnum.Center;
				break;
			case 1: // cover
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepAspectCovered;
                help = DashboardConfig.WallpaperModeEnum.Cover;
				break;
			case 2: // stretch
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.Scale;
                help = DashboardConfig.WallpaperModeEnum.Stretch;
				break;
			case 3: // keep aspect
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
                help = DashboardConfig.WallpaperModeEnum.KeepAspect;
				break;
            default: // so the compiler doesn't complain
                help = DashboardConfig.WallpaperModeEnum.Cover;
                break;
		}*/

        Record.Data.Wallpaper = newWallpaperOmgmomogmogmo.ResourcePath;
        Record.Save();
    }
}
