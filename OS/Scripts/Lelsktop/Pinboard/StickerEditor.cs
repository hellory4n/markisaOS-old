using Godot;
using System;

public class StickerEditor : Panel {
    public override void _Process(float delta) {
        base._Process(delta);
        // yes.
        Visible = Sticker.SelectedSticker != null;

        // so true
        Theme = GetNode<Control>("../1/Windows/ThemeThing").Theme;
    }

    public void EditScale(float value) {
        Sticker.SelectedSticker.Scale = new Vector2(value, value);
    }

    public void SaveScale(bool valueChanged) {
        if (!valueChanged)
            return;
        
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        // help
        pinboard.Items[Sticker.SelectedSticker.PinboardItem].Scale = Sticker.SelectedSticker.Scale.x;
        SavingManager.Save(SavingManager.CurrentUser, pinboard);
    }

    public void DeleteSticker() {
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        pinboard.Items.Remove(Sticker.SelectedSticker.PinboardItem);
        SavingManager.Save(SavingManager.CurrentUser, pinboard);

        Sticker.SelectedSticker.QueueFree();
        Sticker.SelectedSticker = null;
    }
}
