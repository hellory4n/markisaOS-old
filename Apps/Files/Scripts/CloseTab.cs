using Godot;
using Lelsktop.Wm;
using System;

public partial class CloseTab : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // comically large if statement
        if (Input.IsActionJustReleased("close_tab") && GetParent<Button>().ThemeTypeVariation == "ActiveTab") {
            Click();
        }
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
