using Godot;
using System;
using Kickstart.Records;

namespace Dashboard.Pinboard;

// this is based on https://gist.github.com/angstyloop/08200c6d816347c82ea1aed56c219f17
public partial class Sticker : Sprite2D
{
    
    enum StatusThingy
    {
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
    public bool DoTheStickerThingy = true;

    public override void _Ready()
    {
        base._Ready();
        if (!DoTheStickerThingy)
            return;

        Smaller = GetNode<Timer>("Smaller");
        Bigger = GetNode<Timer>("Bigger");
        Smaller.Paused = true;
        Bigger.Paused = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (!Pinboard.EditingPinboard)
            return;

        if (Status == StatusThingy.Dragging && !Dashboard.InteractingWithDashboardInterface)
            Position = MousePosition + EpicOffset;
        
        Rect2 aRect = new(
            Position.X - Texture.GetSize().X * Scale.X / 2, Position.Y - Texture.GetSize().Y * Scale.Y / 2,
            Texture.GetSize().X * Scale.X, Texture.GetSize().Y * Scale.Y
        );

        // change size :)))))
        if (DoTheStickerThingy)
        {
            Smaller.Paused = !PinboardSelectThingy.DecreaseSize.Intersects(aRect);
            Bigger.Paused = !PinboardSelectThingy.IncreaseSize.Intersects(aRect);
        }

        // delete sticker :)))))))))))))))
        if (PinboardSelectThingy.RemoveSticker.Intersects(aRect) && SelectedSticker == this)
        {
            var dashboard = new Record<DashboardConfig>();
            dashboard.Data.Pinboard.Remove(PinboardItem);
            dashboard.Save();

            SelectedSticker = null;
            QueueFree();
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (!Pinboard.EditingPinboard)
            return;

        if (@event is InputEventMouse m)
            MousePosition = m.Position;

        if (@event is InputEventMouseButton yes)
        {
            if (yes.ButtonIndex == MouseButton.Left)
            {
                if (Status != StatusThingy.Dragging && yes.Pressed)
                {
                    Rect2 aRect = new(
                        Position.X - Texture.GetSize().X * Scale.X / 2, Position.Y - Texture.GetSize().Y * Scale.Y / 2,
                        Texture.GetSize().X * Scale.X, Texture.GetSize().Y * Scale.Y
                    );

                    if (aRect.HasPoint(yes.Position))
                    {
                        MoveToFront();
                        Status = StatusThingy.Clicked;
                        EpicOffset = Position - yes.Position;
                        SelectedSticker = this;
                    }
                }
                else if (Status == StatusThingy.Dragging && !yes.Pressed)
                {
                    Status = StatusThingy.Released;

                    // we need to save the position :)))
                    var dashboard = new Record<DashboardConfig>();
                    var sdgsdkgm = dashboard.Data.Pinboard[PinboardItem];
                    sdgsdkgm.Position = Position;
                    dashboard.Data.Pinboard[PinboardItem] = sdgsdkgm;
                    dashboard.Save();

                    SelectedSticker = null;
                }
            }
        }

        if (Status == StatusThingy.Clicked && SelectedSticker == this && @event is InputEventMouseMotion)
            Status = StatusThingy.Dragging;
    }

    public void GetSmallerOmgomgomg()
    {
        if (SelectedSticker != this)
        {
            Smaller.Paused = true;
            return;
        }

        float nfjggjfg = (float)Math.Log(Scale.X + 0.1, 10);
        float help = (float)Math.Pow(10, nfjggjfg - 0.1);

        Scale = new Vector2(help, help);

        var dashboard = new Record<DashboardConfig>();
        var sdgsdkgm = dashboard.Data.Pinboard[PinboardItem];
        sdgsdkgm.Position = Scale;
        dashboard.Data.Pinboard[PinboardItem] = sdgsdkgm;
        dashboard.Save();
    }

    public void GetBiggerOmgomgomg()
    {
        if (SelectedSticker != this)
        {
            Bigger.Paused = true;
            return;
        }

        float nfjggjfg = (float)Math.Log(Scale.X + 0.1, 10);
        float help = (float)Math.Pow(10, nfjggjfg + 0.1);

        Scale = new Vector2(help, help);

        var dashboard = new Record<DashboardConfig>();
        var sdgsdkgm = dashboard.Data.Pinboard[PinboardItem];
        sdgsdkgm.Position = Scale;
        dashboard.Data.Pinboard[PinboardItem] = sdgsdkgm;
        dashboard.Save();
    }
}
