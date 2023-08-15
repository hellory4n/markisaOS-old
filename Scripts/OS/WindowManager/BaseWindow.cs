using Godot;
using System;

public class BaseWindow : WindowDialog {
    public override void _Ready() {
        base._Ready();
        PackedScene maximize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
        TextureButton yes = (TextureButton)maximize.Instance();
        AddChild(yes);

        PackedScene minimize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
        TextureButton perhaps = (TextureButton)minimize.Instance();
        AddChild(perhaps);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // windowdialog's close button just makes it invisible
        if (!Visible)
            QueueFree();
    }

    // make the window active :)
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            if (bruh.Pressed)
                Raise();
        }
        base._GuiInput(@event);
    }
}
