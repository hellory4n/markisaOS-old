using Godot;
using System;
using System.Collections.Generic;

public class FileTabs : HBoxContainer {
    public List<TabThing> TabButtons = new List<TabThing>();
    public List<Control> TabContent = new List<Control>();
    PackedScene TabThing = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
    PackedScene TabContentThing = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabContent.tscn");

    public override void _Ready() {
        base._Ready();
        HSplitContainer coolTab = TabContentThing.Instance<HSplitContainer>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        TabThing fart = TabThing.Instance<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = "Home";
        coolTab.GetNode<FileView>("Content/ContentThing/ItemList").TabThing = fart;

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
    }

    public void UpdateStuff(Control activeContent, TabThing activeButton) {
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
