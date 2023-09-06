using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();

        // testing the filesystem :)
        /*Folder coolFolder = new Folder("Malware");
        coolFolder.Save();
        BaseLelfs suffering = new BaseLelfs("Stupidity", coolFolder.Path);
        suffering.Save();
        coolFolder.Items.Add(suffering.Path);
        coolFolder.Save();*/
        Folder coolFolder = LelfsManager.Load<Folder>("/Malware");
        BaseLelfs stupidity = coolFolder.LoadLocal<BaseLelfs>("Stupidity");
        GD.Print(stupidity.Id);

        Vector2 bruh = ResolutionManager.Resolution;

        Viewport pain = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        pain.Size = bruh;

        UserLelsktop suffer = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);

        // load the wallpaper
        string wallpaperPath = suffer.Wallpaper;
        Texture wallpaper = ResourceLoader.Load<Texture>(wallpaperPath);
        GetNode<Sprite>("Wallpaper").Texture = wallpaper;

        // startup sound :)
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Startup);

        // cool dock :)
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/LelsktopInterface.tscn");
        CanvasLayer lelsktopInterface = (CanvasLayer)m.Instance();
        GetTree().Root.CallDeferred("add_child", lelsktopInterface);
        lelsktopInterface.GetNode<Panel>("Dock").RectSize = new Vector2(bruh.x, 80);

        // play the animation for the dock and make sure the position on the animation is correct :)
        Animation animationomg = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Startup");
        // this didn't work
        // int track = animationomg.FindTrack("Painful/Panel"); 
        int keyStart = animationomg.TrackFindKey(0, 0);
        int keyEnd = animationomg.TrackFindKey(0, 0.5f);
        animationomg.TrackSetKeyValue(0, keyStart, new Vector2(0, bruh.y));
        animationomg.TrackSetKeyValue(0, keyEnd, new Vector2(0, bruh.y-75));

        // and also fix the animation for the quick settings
        Animation animationOrSomething = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
            GetAnimation("OpenQuickSettings");
        int keyStartOrSomething = animationOrSomething.TrackFindKey(0, 0);
        int keyEndOrSomething = animationOrSomething.TrackFindKey(0, 0.5f);
        animationOrSomething.TrackSetKeyValue(0, keyStartOrSomething, new Vector2(bruh.x-400, -200));
        animationOrSomething.TrackSetKeyValue(0, keyEndOrSomething, new Vector2(bruh.x-400, 40));

        Animation animationButDifferent = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").
            GetAnimation("CloseQuickSettings");
        int keyStartButDifferent = animationButDifferent.TrackFindKey(0, 0);
        int keyEndButDifferent = animationButDifferent.TrackFindKey(0, 0.5f);
        animationButDifferent.TrackSetKeyValue(0, keyStartButDifferent, new Vector2(bruh.x-400, 40));
        animationButDifferent.TrackSetKeyValue(0, keyEndButDifferent, new Vector2(bruh.x-400, -200));
        
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
            lelsktopInterface.GetNode<HBoxContainer>("Dock/DockStuff/QuickLaunch").AddChild(yes);
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Vector2 pain = ResolutionManager.Resolution;
        Viewport bruh = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        Panel appMenu = GetNode<Panel>("/root/LelsktopInterface/AppMenu");
        Panel quickSettings = GetNode<Panel>("/root/LelsktopInterface/QuickSettings");
        Color invisible = new Color(1, 1, 1, 0);

        if (GetGlobalMousePosition().y < 40 || GetGlobalMousePosition().y > pain.y-75 ||
        appMenu.Modulate != invisible || quickSettings.Modulate != invisible) {
            bruh.GuiDisableInput = true;
        } else {
            bruh.GuiDisableInput = false;
        }
    }
}
