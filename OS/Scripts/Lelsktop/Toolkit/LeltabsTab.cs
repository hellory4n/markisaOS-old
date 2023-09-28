using Godot;
using System;

public class LeltabsTab : Button {
    public Control TabContent;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetParent<Leltabs>().UpdateStuff(TabContent, this);
    }
}
