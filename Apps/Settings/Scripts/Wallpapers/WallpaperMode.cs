using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class WallpaperMode : OptionButton
{
	Record<DashboardConfig> Record = new();

	public void OnItemSelected(int index)
	{
		TextureRect wallOfPaper = GetNode<TextureRect>("/root/Dashboard/Wallpaper");
		DashboardConfig.WallpaperModeEnum help;
        switch (index)
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
		}

		Record.Data.WallpaperMode = help;
		Record.Save();
	}
}
