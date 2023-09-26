using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();

        Vector2 bruh = ResolutionManager.Resolution;

        Viewport pain = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        pain.Size = bruh;

        // test image file omgogogogomg
        if (!LelfsManager.FileExists("/Home/Videos/pain")) {
            LelfsFile painPicture = LelfsManager.NewFile("pain", LelfsManager.PermanentPath("/Home/Videos"));
            painPicture.Type = "Video";
            painPicture.Data.Add("Resource", "/home/toddynho/Videos/how to not start a video/light-or-dark.ogv");
            painPicture.Save();
        }

        SavingManager.ConvertOldUser(SavingManager.CurrentUser);
        UserLelsktop suffer = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);

        // load the wallpaper
        // is it a default wallpaper?
        if (ResourceLoader.Exists(suffer.Wallpaper)) {
            string wallpaperPath = suffer.Wallpaper;
            Texture wallpaper = ResourceLoader.Load<Texture>(wallpaperPath);
            GetNode<Sprite>("Wallpaper").Texture = wallpaper;
        // is it a lelfs file?
        } else if (LelfsManager.IdExists(suffer.Wallpaper)) {
            var epicFile = LelfsManager.LoadById<LelfsFile>(suffer.Wallpaper);
            Texture wallpaper = ResourceManager.LoadImage(epicFile.Data["Resource"].ToString());
            GetNode<Sprite>("Wallpaper").Texture = wallpaper;

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
            GetNode<Sprite>("Wallpaper").Scale = new Vector2(scale, scale);
            GetNode<Sprite>("Wallpaper").Position = bruh/2;

        // ok it's broken, just load the default wallpaper
        } else {
            var wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/HighPeaks.jpg");
            GetNode<Sprite>("Wallpaper").Texture = wallpaper;
        }

        // startup sound :)
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Startup);

        // cool dock :)
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/LelsktopInterface.tscn");
        CanvasLayer lelsktopInterface = (CanvasLayer)m.Instance();
        GetTree().Root.CallDeferred("add_child", lelsktopInterface);
        lelsktopInterface.GetNode<Panel>("Dock").RectSize = new Vector2(75, bruh.y);

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
        animationOrSomething.TrackSetKeyValue(0, keyStartOrSomething, new Vector2(bruh.x-475, -241));
        animationOrSomething.TrackSetKeyValue(0, keyEndOrSomething, new Vector2(bruh.x-475, 40));

        Animation animationButDifferent = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
            GetAnimation("CloseQuickSettings");
        int keyStartButDifferent = animationButDifferent.TrackFindKey(0, 0);
        int keyEndButDifferent = animationButDifferent.TrackFindKey(0, 0.5f);
        animationButDifferent.TrackSetKeyValue(0, keyStartButDifferent, new Vector2(bruh.x-475, 40));
        animationButDifferent.TrackSetKeyValue(0, keyEndButDifferent, new Vector2(bruh.x-475, -241));
        
        lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").Play("Startup");

        // load theme
        Theme theme = ResourceLoader.Load<Theme>($"res://Assets/Themes/{suffer.Theme}/Theme.tres");
        pain.GetNode<Control>("ThemeThing").Theme = theme;
        lelsktopInterface.GetNode<Panel>("Dock").Theme = theme;
        lelsktopInterface.GetNode<Panel>("QuickSettings").Theme = theme;
        lelsktopInterface.GetNode<Panel>("AppMenu").Theme = theme;
        lelsktopInterface.GetNode<Panel>("Panel").Theme = theme;

        // quick launch stuff
        Lelapp[] apps = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser).Apps;
        foreach (var app in apps) {
            PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/QuickLaunchButton.tscn");
            DefaultOpenWindowButton yes = packedScene.Instance<DefaultOpenWindowButton>();
            yes.Icon = ResourceLoader.Load<Texture>(app.Icon);
            yes.WindowScene = app.Scene;
            yes.HintTooltip = app.Name;
            lelsktopInterface.GetNode<VBoxContainer>("Dock/DockStuff/QuickLaunch").AddChild(yes);
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Vector2 pain = ResolutionManager.Resolution;
        Viewport bruh = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        Panel appMenu = GetNode<Panel>("/root/LelsktopInterface/AppMenu");
        Panel quickSettings = GetNode<Panel>("/root/LelsktopInterface/QuickSettings");
        Color invisible = new Color(1, 1, 1, 0);

        if (GetGlobalMousePosition().y < 40 || GetGlobalMousePosition().x > pain.x-75 ||
        appMenu.Modulate != invisible || quickSettings.Modulate != invisible) {
            bruh.GuiDisableInput = true;
        } else {
            bruh.GuiDisableInput = false;
        }
    }
}
