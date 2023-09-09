using Godot;
using System;
using System.Collections.Generic;

public class FileTabs : HBoxContainer {
    public List<TabThing> TabButtons = new List<TabThing>();
    public List<Control> TabContent = new List<Control>();

    public override void _Ready() {
        base._Ready();
        Control coolTab = new Control {
            AnchorRight = 1,
            AnchorBottom = 1,
            MarginTop = 45
        };
        PackedScene pain = ResourceLoader.Load<PackedScene>("res://Apps/Files/TabThing.tscn");
        TabThing fart = pain.Instance<TabThing>();
        fart.TabContent = coolTab;
        fart.Text = $"Tab 1";

        CallDeferred("add_child", fart);
        GetParent().GetParent().CallDeferred("add_child", coolTab);
        coolTab.AddChild(new Label {
            Text = "tab 1 :)",
            AnchorRight = 1,
            AnchorBottom = 1,
            Align = Label.AlignEnum.Center,
            Valign = Label.VAlign.Center,
            ThemeTypeVariation = "Header"
        });

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
