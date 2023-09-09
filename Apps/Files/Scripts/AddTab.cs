using Godot;
using System;

public class AddTab : Button {
    int Tabs = 1;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // always be in the end of the thing, i think
        Raise();
    }

    public void Click() {
        Tabs++;
        Control coolTab = new Control {
            AnchorRight = 1,
            AnchorBottom = 1,
            MarginTop = 45
        };
        PackedScene pain = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
        TabThing fart = pain.Instance<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = $"Tab {Tabs}";
        
        GetParent().AddChild(fart);
        GetParent().GetParent().GetParent().AddChild(coolTab);
        GetParent<FileTabs>().UpdateStuff(coolTab, fart);
        coolTab.AddChild(new Label {
            Text = $"tab {Tabs} :)",
            AnchorRight = 1,
            AnchorBottom = 1,
            Align = Label.AlignEnum.Center,
            Valign = Label.VAlign.Center,
            ThemeTypeVariation = "Header"
        });

        FileTabs bruh = GetParent<FileTabs>();
        bruh.TabContent.Add(coolTab);
        bruh.TabButtons.Add(fart);
    }
}
