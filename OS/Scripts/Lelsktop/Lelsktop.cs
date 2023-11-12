using Godot;
using System;
using System.Collections.Generic;

namespace Lelsktop;

public partial class Lelsktop : Node2D
{
	/// <summary>
	/// If true, the user is currently using either the dock, panel, app menu, quick settings, or the workspace switcher.
	/// </summary>
	public static bool InteractingWithLelsktopInterface = false;

	/*public override void _Ready() {
		base._Ready();

		Vector2 bruh = ResolutionManager.Resolution;

		GetNode<SubViewport>("1/Windows").Size = bruh;
		GetNode<SubViewport>("2/Windows").Size = bruh;
		GetNode<SubViewport>("3/Windows").Size = bruh;
		GetNode<SubViewport>("4/Windows").Size = bruh;

		WindowManager.CurrentWorkspace = GetNode<SubViewport>("1/Windows");

		SavingManager.ConvertOldUser(SavingManager.CurrentUser);
		UserLelsktop suffer = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);

		// load the wallpaper
		// is it a default wallpaper?
		if (ResourceLoader.Exists(suffer.Wallpaper)) {
			string wallpaperPath = suffer.Wallpaper;
			Texture2D wallpaper = GD.Load<Texture2D>(wallpaperPath);
			GetNode<Sprite2D>("Wallpaper").Texture2D = wallpaper;
		// is it a lelfs file?
		} else if (LelfsManager.IdExists(suffer.Wallpaper)) {
			var epicFile = LelfsManager.LoadById<LelfsFile>(suffer.Wallpaper);
			Texture2D wallpaper = ResourceManager.LoadImage(epicFile.Data["Resource"].ToString());
			GetNode<Sprite2D>("Wallpaper").Texture2D = wallpaper;

			// scale wallpaper thing :))))
			GetNode<ImageBackground>("Wallpaper").OriginalSize = wallpaper.GetSize();
			float scale;
			if (bruh > wallpaper.GetSize()) {
				scale = (Mathf.Max(bruh.x, bruh.y) - Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y)) /
					Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y);
				scale += 1;
			} else {
				scale = Mathf.Max(bruh.x, bruh.y) / Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y);
			}
			GetNode<Sprite2D>("Wallpaper").Scale = new Vector2(scale, scale);
			GetNode<Sprite2D>("Wallpaper").Position = bruh/2;

		// ok it's broken, just load the default wallpaper
		} else {
			var wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/HighPeaks.jpg");
			GetNode<Sprite2D>("Wallpaper").Texture2D = wallpaper;
		}

		// startup sound :)
		SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
		sounds.PlaySoundEffect(SoundManager.SoundEffects.Startup);

		// cool dock :)
		PackedScene m = GD.Load<PackedScene>("res://OS/Lelsktop/LelsktopInterface.tscn");
		CanvasLayer lelsktopInterface = (CanvasLayer)m.Instantiate();
		GetTree().Root.CallDeferred("add_child", lelsktopInterface);
		lelsktopInterface.GetNode<Panel>("Dock").Size = new Vector2(75, bruh.y);

		// play the animation for the dock and make sure the position on the animation is correct :)
		Animation animationomg = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Startup");
		// this didn't work
		// int track = animationomg.FindTrack("Painful/Panel"); 
		int keyStart = animationomg.TrackFindKey(0, 0);
		int keyEnd = animationomg.TrackFindKey(0, 0.5f);
		animationomg.TrackSetKeyValue(0, keyStart, new Vector2(bruh.x, 0));
		animationomg.TrackSetKeyValue(0, keyEnd, new Vector2(bruh.x-75, 0));

		// and also fix the animation for the quick settings
		Animation animationOrSomething = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
			GetAnimation("OpenQuickSettings");
		int keyStartOrSomething = animationOrSomething.TrackFindKey(0, 0);
		int keyEndOrSomething = animationOrSomething.TrackFindKey(0, 0.5f);
		animationOrSomething.TrackSetKeyValue(0, keyStartOrSomething, new Vector2(bruh.x-375, -475));
		animationOrSomething.TrackSetKeyValue(0, keyEndOrSomething, new Vector2(bruh.x-375, 40));

		Animation animationButDifferent = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
			GetAnimation("CloseQuickSettings");
		int keyStartButDifferent = animationButDifferent.TrackFindKey(0, 0);
		int keyEndButDifferent = animationButDifferent.TrackFindKey(0, 0.5f);
		animationButDifferent.TrackSetKeyValue(0, keyStartButDifferent, new Vector2(bruh.x-375, 40));
		animationButDifferent.TrackSetKeyValue(0, keyEndButDifferent, new Vector2(bruh.x-375, -475));
		
		lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").Play("Startup");

		// and the workspace menu
		Animation an = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
			GetAnimation("OpenWorkspaces");
		int keySta = an.TrackFindKey(0, 0);
		int keyEn = an.TrackFindKey(0, 0.5f);
		an.TrackSetKeyValue(0, keySta, new Vector2(bruh.x, 64));
		an.TrackSetKeyValue(0, keyEn, new Vector2(bruh.x-225, 64));

		Animation ani = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
			GetAnimation("CloseWorkspaces");
		int keyStar = ani.TrackFindKey(0, 0);
		int keyEndnbnbgf = ani.TrackFindKey(0, 0.5f);
		ani.TrackSetKeyValue(0, keyStar, new Vector2(bruh.x-225, 64));
		ani.TrackSetKeyValue(0, keyEndnbnbgf, new Vector2(bruh.x, 64));
		
		lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").Play("Startup");

		// load theme
		Theme theme = GD.Load<Theme>($"res://Assets/Themes/{suffer.Theme}/Theme.tres");
		GetNode<Control>("1/Windows/ThemeThing").Theme = theme;
		GetNode<Control>("2/Windows/ThemeThing").Theme = theme;
		GetNode<Control>("3/Windows/ThemeThing").Theme = theme;
		GetNode<Control>("4/Windows/ThemeThing").Theme = theme;
		lelsktopInterface.GetNode<Panel>("Dock").Theme = theme;
		lelsktopInterface.GetNode<Panel>("QuickSettings").Theme = theme;
		lelsktopInterface.GetNode<Panel>("AppMenu").Theme = theme;
		lelsktopInterface.GetNode<Panel>("Panel").Theme = theme;

		// quick launch stuff
		Lelapp[] apps = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser).Apps;
		foreach (var app in apps) {
			PackedScene packedScene = GD.Load<PackedScene>("res://OS/Lelsktop/QuickLaunchButton.tscn");
			DefaultOpenWindowButton yes = packedScene.Instantiate<DefaultOpenWindowButton>();
			yes.Icon = GD.Load<Texture2D>(app.Icon);
			yes.WindowScene = app.Scene;
			yes.TooltipText = app.Name;
			lelsktopInterface.GetNode<VBoxContainer>("Dock/DockStuff/QuickLaunch").AddChild(yes);
		}

		// load the pinboard stuff :)))
		Dictionary<string, PinboardItem> items = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser).Items;
		var pinboard = GetNode<Node2D>("Pinboard");
		var ftgkvtfyu = GD.Load<PackedScene>("res://OS/Lelsktop/Sticker.tscn");
		var bhsdffgyu = GD.Load<PackedScene>("res://OS/Lelsktop/StickyNote.tscn");

		foreach (var item in items) {
			if (item.Value.IsStickyNote) {
				var bullshit = bhsdffgyu.Instantiate<StickyNote>();
				bullshit.Position = item.Value.Position;
				bullshit.PinboardItem = item.Key;
				pinboard.AddChild(bullshit);
				bullshit.GetNode<TextEdit>("Text").Text = item.Value.Text;
			} else {
				var sticker = ftgkvtfyu.Instantiate<Sticker>();
				sticker.Position = item.Value.Position;
				sticker.Rotation = item.Value.Rotation;
				sticker.Scale = new Vector2(item.Value.Scale, item.Value.Scale);
				sticker.Texture2D = ResourceManager.LoadImage(item.Value.TexturePath);
				sticker.PinboardItem = item.Key;
				pinboard.AddChild(sticker);
			}
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		// if the user is editing the pinboard stuff we don't need to process this shit anyway
		if (Pinboard.EditingPinboard) {
			InteractingWithLelsktopInterface = false;
			return;
		}

		Vector2 pain = ResolutionManager.Resolution;
		SubViewport bruh1 = GetNode<SubViewport>("/root/Lelsktop/1/Windows");
		SubViewport bruh2 = GetNode<SubViewport>("/root/Lelsktop/2/Windows");
		SubViewport bruh3 = GetNode<SubViewport>("/root/Lelsktop/3/Windows");
		SubViewport bruh4 = GetNode<SubViewport>("/root/Lelsktop/4/Windows");
		Panel appMenu = GetNode<Panel>("/root/LelsktopInterface/AppMenu");
		Panel quickSettings = GetNode<Panel>("/root/LelsktopInterface/QuickSettings");
		Panel workspaces = GetNode<Panel>("/root/LelsktopInterface/Workspaces");
		Color invisible = new(1, 1, 1, 0);

		if (GetGlobalMousePosition().y < 40 || GetGlobalMousePosition().x > pain.x-75 ||
		appMenu.Modulate != invisible || quickSettings.Modulate != invisible
		|| workspaces.Modulate != invisible) {
			bruh1.GuiDisableInput = true;
			bruh2.GuiDisableInput = true;
			bruh3.GuiDisableInput = true;
			bruh4.GuiDisableInput = true;
			
			InteractingWithLelsktopInterface = true;
		} else {
			// suffering
			if (WindowManager.CurrentWorkspace == bruh1)
				bruh1.GuiDisableInput = false;
			if (WindowManager.CurrentWorkspace == bruh2)
				bruh2.GuiDisableInput = false;
			if (WindowManager.CurrentWorkspace == bruh3)
				bruh3.GuiDisableInput = false;
			if (WindowManager.CurrentWorkspace == bruh4)
				bruh4.GuiDisableInput = false;

			InteractingWithLelsktopInterface = false;
		}
	}*/
}
