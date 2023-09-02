using Godot;
using System;

public class DisplaySettingsAutoRevert : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Yes));
    }

    public void Yes() {
        ResolutionManager resolutionManager = GetNode<ResolutionManager>("/root/ResolutionManager");

        // since the settings aren't actually saved we can just do this
        resolutionManager.Update();

        // we need to undo the changes to the lelsktop
        Vector2 stupidity = ResolutionManager.GetScreenSize();
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

        GetParent<BaseWindow>().Visible = false;
    }
}
