using Godot;
using System;

public class Sticker : Sprite {
    // based on https://gist.github.com/angstyloop/08200c6d816347c82ea1aed56c219f17
    enum StatusThingy {
        None,
        Clicked,
        Dragging
    }

    StatusThingy Status = StatusThingy.None;
    Vector2 MousePosition;
    public string PinboardItem;
    Vector2 EpicOffset;
    public static Sticker SelectedSticker;
    bool MousePressed = false;

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
                if (yes.Pressed) {
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
                } else if (Status == StatusThingy.Clicked && SelectedSticker == this) {
                    MousePressed = true;
                    Status = StatusThingy.Dragging;

                    
                }
            }
        }

        if (Status == StatusThingy.Dragging && MousePressed && @event is InputEventMouseMotion 
        && SelectedSticker == this) {
                Position = MousePosition + EpicOffset;
        }


        if (@event is InputEventMouseButton j) {
            if (j.ButtonIndex == (int)ButtonList.Left && !j.Pressed) {
                MousePressed = false;
                SelectedSticker = null;
                Status = StatusThingy.None;
                
                // we need to save the position :)))
                var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
                pinboard.Items[PinboardItem].Position = Position;
                SavingManager.Save(SavingManager.CurrentUser, pinboard);
            }
        }
    }
}
