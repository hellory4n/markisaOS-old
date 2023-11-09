using Godot;
using System;
using System.Collections.Generic;

public partial class Leltabs : HBoxContainer {
    public List<LeltabsTab> TabButtons = new List<LeltabsTab>();
    public List<Control> TabContent = new List<Control>();
    [Export(PropertyHint.File, "*.tscn")]
    public string TabThing = "res://OS/Lelsktop/TabThing.tscn";
    [Export(PropertyHint.File, "*.tscn")]
    public string TabContentThing = "";
    public Control ActiveTab;

    public override void _Ready() {
        base._Ready();
        var m = ResourceLoader.Load<PackedScene>(TabContentThing);
        var coolTab = m.Instance<Control>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        var h = ResourceLoader.Load<PackedScene>(TabThing);
        var fart = h.Instance<LeltabsTab>();
        fart.TabContent = coolTab;
        fart.Text = "New Tab";

        CallDeferred("add_child", fart);
        GetParent().GetParent().CallDeferred("add_child", coolTab);

        TabContent.Add(coolTab);
        TabButtons.Add(fart);
    }

    // close the window if all tabs are closed
    public override void _Process(float delta) {
        base._Process(delta);
        BaseWindow pain = GetParent().GetParent<BaseWindow>();
        if (TabButtons.Count == 0 && !pain.IsClosing) {
            pain.Visible = false;
        }

        // epic tab titles :))))
        for (int i = 0; i < TabContent.Count; i++) {
            Control dhjhdjhjjghfj = TabContent[i];
            TabButtons[i].Text = dhjhdjhjjghfj.GetNode<Label>("TabTitle").Text;
        }
    }

    public void UpdateStuff(Control activeContent, LeltabsTab activeButton) {
        foreach (var funni in TabButtons) {
            if (funni == activeButton) {
                funni.ThemeTypeVariation = "ActiveTab";
            } else {
                funni.ThemeTypeVariation = "InactiveTab";
            }
        }

        foreach (var jcnkv in TabContent) {
            if (jcnkv == activeContent) {
                jcnkv.Visible = true;
            } else {
                jcnkv.Visible = false;
            }
        }
    }
}
