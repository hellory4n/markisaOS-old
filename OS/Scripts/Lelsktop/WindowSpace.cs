using Godot;
using System;

namespace Lelsktop.Interface;

public partial class WindowSpace : Control
{
    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton)
        {
            Panel appMenu = GetParent().GetNode<Panel>("AppMenu");
            Panel quickSettings = GetParent().GetNode<Panel>("QuickSettings");
            Panel workspaces = GetParent().GetNode<Panel>("Workspaces");

            if (appMenu.Position.Y > 0)
                GetNode<Button>("/root/LelsktopInterface/Panel/Apps")._Toggled(false);
            
            if (quickSettings.Position.Y > 0)
                GetNode<Button>("/root/LelsktopInterface/Panel/Settings")._Toggled(false);

            if (workspaces.Modulate != new Color(1, 1, 1, 0))
                GetNode<Button>("/root/LelsktopInterface/Dock/DockStuff/QuickLaunch/Workspaces")._Toggled(false);
        }
        base._GuiInput(@event);
    }
}
