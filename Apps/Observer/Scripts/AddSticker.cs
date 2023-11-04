using Godot;
using System;
using System.Linq;

public class AddSticker : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        Texture texture = GetNode<TextureRect>("../Image").Texture;

        // save the sticker
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        var sticker = new PinboardItem {
            TexturePath = texture.ResourcePath,
            Position = new Vector2(15, 55)
        };
        pinboard.Items = pinboard.Items.Append(sticker).ToArray();
        SavingManager.Save(SavingManager.CurrentUser, pinboard);

        // add it and stuff :)
        GetNode("/root/Lelsktop/Pinboard").AddChild(new TextureRect {
            Expand = true,
            Texture = texture,
            RectSize = texture.GetSize(),
            RectPivotOffset = texture.GetSize()/2,
            RectPosition = sticker.Position
        });

        var notifications = GetNode<NotificationManager>("/root/NotificationManager");
        notifications.ShowNotification("A sticker has been added to your pinboard.");
    }
}
