using Godot;
using Lelsktop.Wm;
using System;
using System.Collections.Generic;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class Leltabs : HBoxContainer
{
    public List<LeltabsTab> TabButtons = new();
    public List<Control> TabContent = new();
    [Export(PropertyHint.File, "*.tscn")]
    public string TabThing = "res://OS/Lelsktop/TabThing.tscn";
    [Export(PropertyHint.File, "*.tscn")]
    public string TabContentThing = "";
    public Control ActiveTab;

    public override void _Ready()
    {
        base._Ready();
        var m = GD.Load<PackedScene>(TabContentThing);
        var coolTab = m.Instantiate<Control>();
        // i have to set a theme at that scene so godot lets me put the correct sizes and stuff
        coolTab.Theme = null;

        var h = GD.Load<PackedScene>(TabThing);
        var fart = h.Instantiate<LeltabsTab>();
        fart.TabContent = coolTab;
        fart.Text = "New Tab";

        CallDeferred("add_child", fart);
        GetParent().GetParent().CallDeferred("add_child", coolTab);

        TabContent.Add(coolTab);
        TabButtons.Add(fart);
    }

    public override void _Process(double delta)
    {
        // epic tab titles :))))
        for (int i = 0; i < TabContent.Count; i++)
        {
            Control dhjhdjhjjghfj = TabContent[i];
            TabButtons[i].Text = dhjhdjhjjghfj.GetNode<Label>("TabTitle").Text;
        }
    }

    public void UpdateStuff(Control activeContent, LeltabsTab activeButton)
    {
        foreach (var funni in TabButtons)
        {
            if (funni == activeButton)
                funni.ThemeTypeVariation = "ActiveTab";
            else
                funni.ThemeTypeVariation = "InactiveTab";
        }

        foreach (var jcnkv in TabContent)
        {
            if (jcnkv == activeContent)
                jcnkv.Visible = true;
            else
                jcnkv.Visible = false;
        }
    }
}
