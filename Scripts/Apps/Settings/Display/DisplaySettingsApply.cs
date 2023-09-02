using Godot;
using System;

public class DisplaySettingsApply : Button {
    SpinBox resolutionWidth;
    SpinBox resolutionHeight;
    SpinBox scalingFactor;

    public override void _Ready() {
        base._Ready();

        resolutionWidth = GetNode<SpinBox>("../ResolutionThing/Width");
        resolutionHeight = GetNode<SpinBox>("../ResolutionThing/Height");
        scalingFactor = GetNode<SpinBox>("../ScalingFactor");

        // set the value of the spin boxes to the current settings
        DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>();
        resolutionWidth.Value = display.Resolution.x;
        resolutionHeight.Value = display.Resolution.y;
        scalingFactor.Value = display.ScalingFactor*100;

        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        Vector2 stupidity = new Vector2((float)resolutionWidth.Value, (float)resolutionHeight.Value);
        stupidity /= (float)(scalingFactor.Value / 100);

        // this doesn't actually save the settings
        GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, stupidity);

        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Settings/DisplayConfirm.tscn");
        BaseWindow jjkn = (BaseWindow)m.Instance();    
        wm.AddWindow(jjkn);

        // yes
        jjkn.GetNode<DisplaySettingsActuallyApply>("Confirm").
            Resolution = new Vector2((float)resolutionWidth.Value, (float)resolutionHeight.Value);
        jjkn.GetNode<DisplaySettingsActuallyApply>("Confirm").ScalingFactor = (float)(scalingFactor.Value/100);

        // the window manager is gonna put it on the center but using the old settings
        jjkn.RectPosition = stupidity/2-(jjkn.RectSize/2);

        // imagine if only part of your windows appear, quite inconvenient
        GetNode<Viewport>("/root/Lelsktop/Thing/Windows").Size = stupidity;

        // resize the dock and panel
        GetNode<Panel>("/root/LelsktopInterface/Dock").RectPosition = new Vector2(0, stupidity.y-75);
        GetNode<Panel>("/root/LelsktopInterface/Dock").RectSize = new Vector2(stupidity.x, 75);
        GetNode<Panel>("/root/LelsktopInterface/Panel").RectSize = new Vector2(stupidity.x, 40);

        // change the animation for the quick settings
        Animation animationOrSomething = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer").
            GetAnimation("OpenQuickSettings");
        int keyStartOrSomething = animationOrSomething.TrackFindKey(0, 0);
        int keyEndOrSomething = animationOrSomething.TrackFindKey(0, 0.5f);
        animationOrSomething.TrackSetKeyValue(0, keyStartOrSomething, new Vector2(stupidity.x-400, -200));
        animationOrSomething.TrackSetKeyValue(0, keyEndOrSomething, new Vector2(stupidity.x-400, 40));

        Animation animationButDifferent = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer").
            GetAnimation("CloseQuickSettings");
        int keyStartButDifferent = animationButDifferent.TrackFindKey(0, 0);
        int keyEndButDifferent = animationButDifferent.TrackFindKey(0, 0.5f);
        animationButDifferent.TrackSetKeyValue(0, keyStartButDifferent, new Vector2(stupidity.x-400, 40));
        animationButDifferent.TrackSetKeyValue(0, keyEndButDifferent, new Vector2(stupidity.x-400, -200));

        // change wallpaper scale
        float scale;
        ImageBackground wallpaper = GetNode<ImageBackground>("/root/Lelsktop/Wallpaper");
        if (stupidity > wallpaper.OriginalSize) {
            scale = (Mathf.Max(stupidity.x, stupidity.y) - Mathf.Max(wallpaper.OriginalSize.x, 
                wallpaper.OriginalSize.y)) / Mathf.Max(wallpaper.OriginalSize.x, wallpaper.OriginalSize.y);
            scale += 1;
        } else {
            scale = Mathf.Max(stupidity.x, stupidity.y) /
                Mathf.Max(wallpaper.OriginalSize.x, wallpaper.OriginalSize.y);
        }
        wallpaper.Scale = new Vector2(scale, scale);
        wallpaper.Position = stupidity/2;
    }
}
