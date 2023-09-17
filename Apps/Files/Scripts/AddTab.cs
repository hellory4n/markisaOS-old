using Godot;
using System;

public class AddTab : Button {
    readonly PackedScene TabThing = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
    readonly PackedScene TabContent = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabContent.tscn");

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
        HSplitContainer coolTab = TabContent.Instance<HSplitContainer>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        TabThing fart = TabThing.Instance<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = $"/";
        coolTab.GetNode<FileView>("Content/ContentThing/ItemList").TabThing = fart;
        
        GetParent().AddChild(fart);
        GetParent().GetParent().GetParent().AddChild(coolTab);
        GetParent<FileTabs>().UpdateStuff(coolTab, fart);

        FileTabs bruh = GetParent<FileTabs>();
        bruh.TabContent.Add(coolTab);
        bruh.TabButtons.Add(fart);
    }
}
