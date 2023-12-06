using Dashboard.Wm;
using Godot;
using Kickstart.Cabinetfs;
using System;
using System.Collections.Generic;

namespace Dashboard.Toolkit;

[GlobalClass]
public partial class MksTabs : HSplitContainer
{
	[Export(PropertyHint.File, "*.tscn")]
	public string TabContent = "";
	[Export]
	MksWindow Window;
	Dictionary<string, Control> Tabs = new();
	VBoxContainer Sidebar;
	Control TabRoot;
	Button AddTabButton;
	CloseWindow Exit;
	readonly PackedScene SidebarScene = GD.Load<PackedScene>("res://OS/Dashboard/TabSidebar.tscn");

    public override void _Ready()
    {
        base._Ready();
		SetAnchorsPreset(LayoutPreset.FullRect);

		Control FUCK = SidebarScene.Instantiate<Control>();
		AddChild(FUCK);
		FUCK.Size = new Vector2(200, FUCK.Size.Y);

		Sidebar = FUCK.GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");
		AddTabButton = FUCK.GetNode<Button>("ScrollContainer/VBoxContainer/AddTab");
		AddTabButton.Pressed += AddTab;
		Exit = FUCK.GetNode<CloseWindow>("ScrollContainer/VBoxContainer/Exit");
		Exit.Window = Window;

		TabRoot = new Control
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			SizeFlagsVertical = SizeFlags.ExpandFill
		};
		AddChild(TabRoot);

		AddTab();
    }

    public void AddTab()
	{
		string thisIsTheKey = CabinetfsManager.GenerateId();

		// add the content
		PackedScene content = GD.Load<PackedScene>(TabContent);
		Control newTab = content.Instantiate<Control>();
		TabRoot.AddChild(newTab);
		Tabs.Add(thisIsTheKey, newTab);

		// add the tab switcher thing
		Button tabSwitcherThingy = new()
		{
			Text = "Tab",
			ThemeTypeVariation = "SidebarButton",
			TextOverrunBehavior = TextServer.OverrunBehavior.TrimEllipsis,
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			CustomMinimumSize = new Vector2(200, 40)
		};
		Sidebar.AddChild(tabSwitcherThingy);
		tabSwitcherThingy.Pressed += () => SwitchTab(thisIsTheKey);
	}

	public void SwitchTab(string key)
	{
		foreach (var tab in Tabs)
			tab.Value.Visible = key == tab.Key;
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		// so they're always at the end of the list
		AddTabButton.MoveToFront();
		Exit.MoveToFront();
    }
}
