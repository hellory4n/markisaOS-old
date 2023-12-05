using Godot;
using Kickstart.Drivers;
using System;
using System.Collections.Generic;
using Dashboard.Wm;
using Dashboard.Toolkit;
using Dashboard.Pinboard;
using Kickstart.Records;
using Kickstart.Cabinetfs;

namespace Dashboard;

public partial class Dashboard : Control
{
	/// <summary>
	/// If true, the user is currently using either the dock, panel, app menu, quick settings, or the workspace switcher.
	/// </summary>
	public static bool InteractingWithDashboardInterface = false;
	public static bool InteractingWithPanel = false;
	public static bool InteractingWithDock = false;
	[Export]
	public SubViewport Windows;
	[Export]
	public SubViewport Interface;
	[Export]
	Panel Dock;
	[Export]
	Panel Panel;
	[Export]
	Panel QuickSettings;
	[Export]
	Panel AppMenu;
	[Export]
	AnimationPlayer Animator;
	[Export]
	SubViewport Pinboard;
	[Export]
	PackedScene Sticker;
	[Export]
	PackedScene StickyNote;
	Color Invisible = new(1, 1, 1, 0);

	public override void _Ready()
	{
		base._Ready();

		Vector2I bruh = ResolutionManager.Resolution;

		Record<DashboardConfig> suffer = new();

		// load the wallpaper :)))))))))
		TextureRect wallOfPaper = GetNode<TextureRect>("Wallpaper");
		switch (suffer.Data.WallpaperMode)
		{
			case DashboardConfig.WallpaperModeEnum.Center:
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.KeepSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepCentered;
				break;
			case DashboardConfig.WallpaperModeEnum.Cover:
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepAspectCovered;
				break;
			case DashboardConfig.WallpaperModeEnum.Stretch:
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.Scale;
				break;
			case DashboardConfig.WallpaperModeEnum.KeepAspect:
				wallOfPaper.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
				wallOfPaper.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
				break;
		}

		// is it a default wallpaper?
		if (ResourceLoader.Exists(suffer.Data.Wallpaper))
		{
			string wallpaperPath = suffer.Data.Wallpaper;
			Texture2D wallpaper = GD.Load<Texture2D>(wallpaperPath);
			wallOfPaper.Texture = wallpaper;
			
		// is it a cabinetfs file?
		}
		else if (CabinetfsManager.IdExists(suffer.Data.Wallpaper))
		{
			var epicFile = CabinetfsManager.LoadFile(suffer.Data.Wallpaper);
			Texture2D wallpaper = ResourceManager.LoadImage(epicFile.Data["Resource"].ToString());
			wallOfPaper.Texture = wallpaper;
		}
		// ok it's broken, just load the default wallpaper
		else
		{
			var wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/HighPeaks.jpg");
			wallOfPaper.Texture = wallpaper;
		}

		Dock.Size = new Vector2(75, bruh.Y);

		// play the animation for the dock and make sure the position on the animation is correct :)
		// i could use a tween but last time i tried tweens it looked like shit
		Animation animationomg = Animator.GetAnimation("Startup");
		int keyStart = animationomg.TrackFindKey(0, 0);
		int keyEnd = animationomg.TrackFindKey(0, 0.5f);
		animationomg.TrackSetKeyValue(0, keyStart, new Vector2(bruh.X, 0));
		animationomg.TrackSetKeyValue(0, keyEnd, new Vector2(bruh.X-75, 0));

		// and also fix the animation for the quick settings
		Animation animationOrSomething = Animator.GetAnimation("OpenQuickSettings");
		int keyStartOrSomething = animationOrSomething.TrackFindKey(0, 0);
		int keyEndOrSomething = animationOrSomething.TrackFindKey(0, 0.5f);
		animationOrSomething.TrackSetKeyValue(0, keyStartOrSomething, new Vector2(bruh.X-375, -475));
		animationOrSomething.TrackSetKeyValue(0, keyEndOrSomething, new Vector2(bruh.X-375, 40));

		Animation animationButDifferent = Animator.GetAnimation("CloseQuickSettings");
		int keyStartButDifferent = animationButDifferent.TrackFindKey(0, 0);
		int keyEndButDifferent = animationButDifferent.TrackFindKey(0, 0.5f);
		animationButDifferent.TrackSetKeyValue(0, keyStartButDifferent, new Vector2(bruh.X-375, 40));
		animationButDifferent.TrackSetKeyValue(0, keyEndButDifferent, new Vector2(bruh.X-375, -475));

		// load theme
		Theme theme = GD.Load<Theme>(suffer.Data.Theme);
		Windows.GetNode<Control>("ThemeThing").Theme = theme;
		Dock.Theme = theme;
		QuickSettings.Theme = theme;
		AppMenu.Theme = theme;
		Panel.Theme = theme;

		// quick launch stuff
		foreach (var app in suffer.Data.QuickLaunch)
		{
			PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/QuickLaunchButton.tscn");
			OpenWindow yes = packedScene.Instantiate<OpenWindow>();
			yes.Icon = GD.Load<Texture2D>(app.Icon);
			yes.WindowScene = app.Executable;
			yes.TooltipText = Tr(app.DisplayName);
			Dock.GetNode("DockStuff/QuickLaunch").AddChild(yes);
		}

		// load the pinboard stuff :)))
		foreach (var item in suffer.Data.Pinboard)
		{
			if (item.Value.IsStickyNote)
			{
				var bullshit = StickyNote.Instantiate<StickyNote>();
				bullshit.Position = item.Value.Position;
				bullshit.PinboardItem = item.Key;
				Pinboard.AddChild(bullshit);
				bullshit.GetNode<TextEdit>("Text").Text = item.Value.Text;
			}
			else
			{
				var sticker = Sticker.Instantiate<Sticker>();
				sticker.Position = item.Value.Position;
				sticker.Scale = new Vector2((float)item.Value.Scale, (float)item.Value.Scale);
				sticker.Texture = ResourceManager.LoadImage(item.Value.TexturePath);
				sticker.PinboardItem = item.Key;
				Pinboard.AddChild(sticker);
			}
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		// if the user is editing the pinboard stuff we don't need to process this shit anyway
		/*if (Pinboard.Pinboard.EditingPinboard)
		{
			InteractingWithDashboardInterface = false;
			return;
		}*/

		Vector2 pain = ResolutionManager.Resolution;

		// quite the mouthful innit
		InteractingWithDashboardInterface = GetGlobalMousePosition().Y < 40 ||
		GetGlobalMousePosition().X > pain.X-75 || AppMenu.Modulate != Invisible ||
		QuickSettings.Modulate != Invisible;
	}
}
