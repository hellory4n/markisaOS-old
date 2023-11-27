using Godot;
using System;
using Dashboard.Wm;

public partial class AddTab : Button {
    readonly PackedScene TabThing = GD.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
    readonly PackedScene TabContent = GD.Load<PackedScene>("res://Apps/Files/TabContent.tscn");

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // always be in the end of the thing, i think
        MoveToFront();

        // help
        if (Input.IsActionJustReleased("add_tab")) {
            Click();
        }
    }

    public void Click() {
        HSplitContainer coolTab = TabContent.Instantiate<HSplitContainer>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        TabThing fart = TabThing.Instantiate<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = $"/";
        //coolTab.GetNode<FileView>("Content/ContentThing/ItemList").TabThing = fart;
        
        GetParent().AddChild(fart);
        GetParent().GetParent().GetParent().AddChild(coolTab);
        GetParent<FileTabs>().UpdateStuff(coolTab, fart);

        FileTabs bruh = GetParent<FileTabs>();
        bruh.TabContent.Add(coolTab);
        bruh.TabButtons.Add(fart);
    }
}
