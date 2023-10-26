using Godot;
using System;

public class BricksPaddle : TextureRect {
    public override void _Process(float delta) {
        base._Process(delta);
        RectGlobalPosition = new Vector2(
            GetViewport().GetMousePosition().x - RectGlobalPosition.x,
            GetParent<Control>().RectGlobalPosition.y-218
        );
    }
}
