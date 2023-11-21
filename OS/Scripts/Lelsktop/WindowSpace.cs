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

            if (appMenu.Position.Y > 0)
            {
                GetNode<Button>("/root/Lelsktop/Inter/Face/Panel/Apps").SetPressedNoSignal(false);
                GetNode<Button>("/root/Lelsktop/Inter/Face/Panel/Apps")._Toggled(false);
            }
            
            if (quickSettings.Position.Y > 0)
            {
                GetNode<Button>("/root/Lelsktop/Inter/Face/Panel/Settings").SetPressedNoSignal(false);
                GetNode<Button>("/root/Lelsktop/Inter/Face/Panel/Settings")._Toggled(false);
            }
        }
        base._GuiInput(@event);
    }
}
