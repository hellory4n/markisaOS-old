using Godot;
using System;
using Kickstart.Drivers;
using Kickstart.Records;
using Kickstart.Cabinetfs;

namespace Dashboard.Pinboard;

public partial class AddStickyNote : TextureButton
{
    [Export]
    PackedScene StickyNote;
    [Export]
    SubViewport Pinboard;

    public override void _Ready()
    {
        base._Ready();
        // the 69 is not a joke i swear
        Position = new Vector2(ResolutionManager.Resolution.X/2 - 69, 65);
    }

    public override void _Pressed()
    {
        base._Pressed();
        // save the sticker
        var notThePinboard = RecordManager.Load<DashboardConfig>();
        var stickerdbgfdf = new PinboardItem
        {
            Position = new Vector2(250, 160),
            IsStickyNote = true,
            Text = "Write text here..."
        };

        string bullshit = CabinetfsManager.GenerateId();
        notThePinboard.Pinboard.Add(bullshit, stickerdbgfdf);
        RecordManager.Save(notThePinboard);

        // make it show up and stuff :)
        var sticker = StickyNote.Instantiate<StickyNote>();
        sticker.Position = stickerdbgfdf.Position;
        sticker.GetNode<TextEdit>("Text").Text = stickerdbgfdf.Text;
        sticker.PinboardItem = bullshit;
        Pinboard.AddChild(sticker);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        // so true
        //Visible = Pinboard.EditingPinboard;
    }
}
