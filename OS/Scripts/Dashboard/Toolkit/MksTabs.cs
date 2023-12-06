using Dashboard.Wm;
using Godot;
using Kickstart.Cabinetfs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Toolkit;

[GlobalClass]
public partial class MksTabs : HSplitContainer
{
	[Export(PropertyHint.File, "*.tscn")]
	public string TabContent = "";
	[Export]
	MksWindow Window;
	Dictionary<string, MksTabRoot> Tabs = new();
	Dictionary<string, Button> TabSwitchers = new();
	VBoxContainer Sidebar;
	Control TabRoot;
	Button AddTabButton;
	CloseWindow Exit;
	readonly PackedScene SidebarScene = GD.Load<PackedScene>("res://OS/Dashboard/TabSidebar.tscn");
	readonly Texture2D CloseIcon = GD.Load<Texture2D>("res://Assets/Themes/HighPeaks-Dark-Blue/Icons/Close.png");

    public override void _Ready()
    {
        base._Ready();
		SetAnchorsPreset(LayoutPreset.FullRect);
		Collapsed = true;
		DraggerVisibility = DraggerVisibilityEnum.HiddenCollapsed;

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
		MksTabRoot newTab = content.Instantiate<MksTabRoot>();
		TabRoot.AddChild(newTab);
		Tabs.Add(thisIsTheKey, newTab);

		// add theh tab switcher thing
		Button tabSwitcherThingy = new()
		{
			Text = "Tab",
			ThemeTypeVariation = "SidebarButton",
			TextOverrunBehavior = TextServer.OverrunBehavior.TrimEllipsis,
			SizeFlagsHorizontal = SizeFlags.ShrinkBegin,
			CustomMinimumSize = new Vector2(160, 40)
		};
		Sidebar.AddChild(tabSwitcherThingy);
		TabSwitchers.Add(thisIsTheKey,tabSwitcherThingy);
		tabSwitcherThingy.Pressed += () => SwitchTab(thisIsTheKey);

		// close tab button :)))))))
		Button closeThingy = new()
		{
			Icon = CloseIcon,
			ThemeTypeVariation = "SidebarButton",
			ExpandIcon = true,
			CustomMinimumSize = new Vector2(40, 40),
			Position = new Vector2(160, 0)
		};
		tabSwitcherThingy.AddChild(closeThingy);
		closeThingy.Pressed += () => CloseTab(thisIsTheKey);

		SwitchTab(thisIsTheKey);
	}

	public void SwitchTab(string key)
	{
		foreach (var tab in Tabs)
		{
			tab.Value.Visible = key == tab.Key;
			tab.Value.IsTabActive = key == tab.Key;
		}
	}

	public void CloseTab(string key)
	{
		Tabs[key].QueueFree();
		Tabs.Remove(key);
		TabSwitchers[key].QueueFree();
		SwitchTab(Tabs.First().Key);
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		// so they're always at the end of the list
		AddTabButton.MoveToFront();
		Exit.MoveToFront();

		// set tab titles
		foreach (var tab in Tabs)
			TabSwitchers[tab.Key].Text = tab.Value.TabTitle;
    }
}
