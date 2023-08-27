using Godot;
using System;

public class ImageBackground : Sprite {
    [Export]
    Vector2 OriginalSize = new Vector2(0, 0);

    public override void _Ready() {
        base._Ready();
        Vector2 screenSize = ResolutionManager.GetScreenSize();

        // first scale the image
        float scale;
        if (screenSize > OriginalSize) {
            scale = (Mathf.Max(screenSize.x, screenSize.y) - Mathf.Max(OriginalSize.x, OriginalSize.y)) /
                Mathf.Max(OriginalSize.x, OriginalSize.y);
            scale += 1;
        } else {
            scale = Mathf.Max(screenSize.x, screenSize.y) / Mathf.Max(OriginalSize.x, OriginalSize.y);
        }
        Scale = new Vector2(scale, scale);

        // then put it in the center of the screen
        Position = screenSize/2;
    }
}
