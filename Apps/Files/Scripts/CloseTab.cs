using Godot;
using System;

public class CloseTab : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        TabThing m = GetParent<TabThing>();

        FileTabs help = m.GetParent<FileTabs>();
        help.TabButtons.Remove(m);
        help.TabContent.Remove(m.TabContent);

        m.TabContent.QueueFree();
        m.QueueFree();
    }
}
