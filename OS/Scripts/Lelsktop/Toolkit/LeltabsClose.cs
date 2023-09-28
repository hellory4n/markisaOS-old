using Godot;
using System;

public class LeltabsClose : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // comically large if statement
        if (Input.IsActionJustReleased("close_tab") && GetParent<Button>().ThemeTypeVariation == "ActiveTab"
        && GetParent().GetParent().GetParent().GetParent<BaseWindow>().IsActive()) {
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
