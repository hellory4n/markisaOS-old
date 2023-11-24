using Godot;
using System;

namespace Dashboard.Toolkit;

public partial class LeltabsTab : Button
{
    public Control TabContent;

    public override void _Pressed() {
        base._Pressed();
        GetParent<Leltabs>().UpdateStuff(TabContent, this);
    }
}
