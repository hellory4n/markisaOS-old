using Godot;
using System;

public partial class LeltabsClose : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // comically large if statement
        if (Input.IsActionJustReleased("close_tab") && GetParent<Button>().ThemeTypeVariation == "ActiveTab"
        && GetParent().GetParent().GetParent().GetParent<Lelwindow>().IsActive()) {
            Click();
        }
    }

    public void Click() {
        LeltabsTab m = GetParent<LeltabsTab>();

        var help = m.GetParent<Leltabs>();
        help.TabButtons.Remove(m);
        help.TabContent.Remove(m.TabContent);

        m.TabContent.QueueFree();
        m.QueueFree();
    }
}
