using Godot;
using System;

public class BricksPaddle : TextureRect {

    public override void _Process(float delta) {
        base._Process(delta);

        RectPosition = new Vector2(
            Mathf.Max(0, GetGlobalMousePosition().x - RectGlobalPosition.x), 218
        );
    }
}
