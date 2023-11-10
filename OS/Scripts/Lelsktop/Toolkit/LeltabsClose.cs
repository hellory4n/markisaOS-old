using Godot;
using System;

namespace Lelsktop.Toolkit;

public partial class LeltabsClose : Button
{
    public override void _Process(double delta) {
        base._Process(delta);
        // comically large if statement
        /*if (Input.IsActionJustReleased("close_tab") && GetParent<Button>().ThemeTypeVariation == "ActiveTab"
        && GetParent().GetParent().GetParent().GetParent<Lelwindow>().IsActive()) {
            Click();
        }*/
    }

    public override void _Pressed()
    {
        base._Pressed();
        LeltabsTab m = GetParent<LeltabsTab>();

        var help = m.GetParent<Leltabs>();
        help.TabButtons.Remove(m);
        help.TabContent.Remove(m.TabContent);

        m.TabContent.QueueFree();
        m.QueueFree();
    }
}
