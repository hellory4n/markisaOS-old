using Godot;
using System;

namespace Lelsktop.Toolkit;

public partial class LeltabsTab : Button
{
    public Control TabContent;

    public override void _Pressed() {
        base._Pressed();
        GetParent<Leltabs>().UpdateStuff(TabContent, this);
    }
}
