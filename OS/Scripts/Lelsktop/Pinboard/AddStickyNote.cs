using Godot;
using System;

public class AddStickyNote : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));

        // the 69 is not a joke i swear
        RectPosition = new Vector2(ResolutionManager.Resolution.x/2 - 69, 65);
    }

    public void Click() {
        // save the sticker
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        var stickerdbgfdf = new PinboardItem {
            Position = new Vector2(250, 160),
            IsStickyNote = true,
            Text = "Write text here..."
        };

        string bullshit = LelfsManager.GenerateID();
        pinboard.Items.Add(bullshit, stickerdbgfdf);
        SavingManager.Save(SavingManager.CurrentUser, pinboard);

        // add it and stuff :)
        var ftgkvtfyu = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/StickyNote.tscn");
        var sticker = ftgkvtfyu.Instance<StickyNote>();
        sticker.Position = stickerdbgfdf.Position;
        sticker.GetNode<TextEdit>("Text").Text = stickerdbgfdf.Text;
        sticker.PinboardItem = bullshit;
        GetNode("/root/Lelsktop/Pinboard").AddChild(sticker);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // so true
        Visible = Pinboard.EditingPinboard;
    }
}
