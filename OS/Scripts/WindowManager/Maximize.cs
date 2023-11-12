using Godot;
using System;
using Lelcore.Drivers;

namespace Lelsktop.Wm;

public partial class Maximize : Button
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        /*Window window = (Window)GetParent();
        if (!window.Resizable) {
            GetParent().GetNode<Button>("Minimize").Position = Position;
            QueueFree();
        }*/
    }

    public override void _Pressed()
    {
        base._Pressed();
        Window window = (Window)GetParent();
        Vector2I maximizedSize = (Vector2I)ResolutionManager.Resolution;
        maximizedSize = new Vector2I(maximizedSize.X-75, maximizedSize.Y-85);

        // check if the window is maximized
        if (window.Position != new Vector2(0, 85) && window.Size != maximizedSize)
        {
            window.Position = new Vector2I(0, 85);
            window.Size = maximizedSize;
        }
        else {
            window.Size = window.MinSize;
            window.Position = new Vector2I(150, 150);
        }
    }
}
