using Godot;
using System;

public class Sticker : Sprite {
    // this is stolen from https://gist.github.com/angstyloop/08200c6d816347c82ea1aed56c219f17
    enum StatusThingy {
        None,
        Clicked,
        Released,
        Dragging
    }

    StatusThingy Status = StatusThingy.None;
    Vector2 MousePosition;
    public string PinboardItem;
    Vector2 EpicOffset;
    public static Sticker SelectedSticker;

    public override void _Process(float delta) {
        base._Process(delta);
        if (Status == StatusThingy.Dragging && !Lelsktop.InteractingWithLelsktopInterface) {
            Position = MousePosition + EpicOffset;
        }
    }

    public override void _Input(InputEvent @event) {
        base._Input(@event);

        if (@event is InputEventMouse m) {
            MousePosition = m.Position;
        }

        if (@event is InputEventMouseButton yes) {
            if (yes.ButtonIndex == (int)ButtonList.Left) {
                if (Status != StatusThingy.Dragging && yes.Pressed) {
                    Rect2 aRect = new Rect2(
                        Position.x - Texture.GetSize().x * Scale.x / 2, Position.y - Texture.GetSize().y * Scale.y / 2,
                        Texture.GetSize().x * Scale.x, Texture.GetSize().y * Scale.y
                    );

                    if (aRect.HasPoint(yes.Position)) {
                        Raise();
                        Status = StatusThingy.Clicked;
                        EpicOffset = Position - yes.Position;
                        SelectedSticker = this;
                    }
                } else if (Status == StatusThingy.Dragging && !yes.Pressed) {
                    Status = StatusThingy.Released;

                    // we need to save the position :)))
                    var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
                    pinboard.Items[PinboardItem].Position = Position;
                    SavingManager.Save(SavingManager.CurrentUser, pinboard);

                    SelectedSticker = null;
                }
            }
        }

        if (Status == StatusThingy.Clicked && SelectedSticker == this && @event is InputEventMouseMotion) {
            Status = StatusThingy.Dragging;
        }
    }
}
