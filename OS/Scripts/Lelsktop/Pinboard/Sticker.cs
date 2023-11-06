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
    Timer Smaller;
    Timer Bigger;

    public override void _Ready() {
        base._Ready();
        Smaller = GetNode<Timer>("Smaller");
        Bigger = GetNode<Timer>("Bigger");
        Smaller.Paused = true;
        Bigger.Paused = true;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (Status == StatusThingy.Dragging && !Lelsktop.InteractingWithLelsktopInterface) {
            Position = MousePosition + EpicOffset;
        }
        
        Rect2 aRect = new Rect2(
            Position.x - Texture.GetSize().x * Scale.x / 2, Position.y - Texture.GetSize().y * Scale.y / 2,
            Texture.GetSize().x * Scale.x, Texture.GetSize().y * Scale.y
        );

        // change size :)))))
        Smaller.Paused = !PinboardSelectThingy.DecreaseSize.Intersects(aRect);
        Bigger.Paused = !PinboardSelectThingy.IncreaseSize.Intersects(aRect);

        // delete sticker :)))))))))))))))
        if (PinboardSelectThingy.RemoveSticker.Intersects(aRect) && SelectedSticker == this) {
            var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
            pinboard.Items.Remove(PinboardItem);
            SavingManager.Save(SavingManager.CurrentUser, pinboard);

            SelectedSticker = null;
            QueueFree();
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

    public void GetSmallerOmgomgomg() {
        if (SelectedSticker != this) {
            Smaller.Paused = true;
            return;
        }

        float nfjggjfg = (float)Math.Log(Scale.x + 0.1, 10);
        float help = (float)Math.Pow(10, nfjggjfg - 0.1);

        Scale = new Vector2(help, help);

        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        pinboard.Items[PinboardItem].Scale = help;
        SavingManager.Save(SavingManager.CurrentUser, pinboard);
    }

    public void GetBiggerOmgomgomg() {
        if (SelectedSticker != this) {
            Bigger.Paused = true;
            return;
        }

        float nfjggjfg = (float)Math.Log(Scale.x + 0.1, 10);
        float help = (float)Math.Pow(10, nfjggjfg + 0.1);

        Scale = new Vector2(help, help);

        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        pinboard.Items[PinboardItem].Scale = help;
        SavingManager.Save(SavingManager.CurrentUser, pinboard);
    }
}
