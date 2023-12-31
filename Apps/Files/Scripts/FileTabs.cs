using Godot;
using System;
using System.Collections.Generic;
using Dashboard.Wm;

public partial class FileTabs : HBoxContainer {
    public List<TabThing> TabButtons = new();
    public List<Control> TabContent = new();
    PackedScene TabThing = GD.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
    PackedScene TabContentThing = GD.Load<PackedScene>("res://Apps/Files/TabContent.tscn");

    public override void _Ready() {
        base._Ready();
        HSplitContainer coolTab = TabContentThing.Instantiate<HSplitContainer>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        TabThing fart = TabThing.Instantiate<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = "Home";
        //coolTab.GetNode<FileView>("Content/ContentThing/ItemList").TabThing = fart;

        CallDeferred("add_child", fart);
        GetParent().GetParent().CallDeferred("add_child", coolTab);

        TabContent.Add(coolTab);
        TabButtons.Add(fart);
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
