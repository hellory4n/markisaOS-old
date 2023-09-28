using Godot;
using System;
using System.ComponentModel;

public class LeltabsAdd : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // always be in the end of the thing, i think
        Raise();

        // help
        if (Input.IsActionJustReleased("add_tab") && GetParent().GetParent().GetParent<BaseWindow>().IsActive()) {
            Click();
        }
    }

    public void Click() {
        var m = ResourceLoader.Load<PackedScene>(GetParent<Leltabs>().TabContentThing);
        var coolTab = m.Instance<Control>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        var h = ResourceLoader.Load<PackedScene>(GetParent<Leltabs>().TabThing);
        var fart = h.Instance<LeltabsTab>();
        fart.TabContent = coolTab;
        fart.Text = "New Tab";
        
        GetParent().AddChild(fart);
        GetParent().GetParent().GetParent().AddChild(coolTab);
        GetParent<Leltabs>().UpdateStuff(coolTab, fart);

        var bruh = GetParent<Leltabs>();
        bruh.TabContent.Add(coolTab);
        bruh.TabButtons.Add(fart);
    }
}
