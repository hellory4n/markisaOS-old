using Godot;
using System;

public partial class TextPeek : LineEdit {
    Panel TextPeekThingy;

    public override void _Ready() {
        base._Ready();
        if (OS.GetName() == "Android") {
            CanvasLayer m = new CanvasLayer {
                Layer = 128
            };
            PackedScene oneOfThePackedScenes = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Overlay/TextPeek.tscn");
            TextPeekThingy = (Panel)oneOfThePackedScenes.Instance();
            GetTree().Root.AddChild(m);
            m.AddChild(TextPeekThingy);
            TextPeekThingy.GlobalPosition = new Vector2(0, 0);
            TextPeekThingy.Visible = false;
            Connect("text_changed", new Callable(this, nameof(TextPeekEdit)));
            Connect("text_entered", new Callable(this, nameof(TextPeekDelete)));
        }
    }

    public void TextPeekEdit(string text) {
        TextPeekThingy.Visible = true;
        TextPeekThingy.GetNode<LineEdit>("Text").Text = text;
    }

    public void TextPeekDelete(string text) {
        TextPeekThingy.Visible = false;
    }
}
