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

    public override void _Ready()
    {
        base._Ready();
		SetAnchorsPreset(LayoutPreset.FullRect);

        // setup the node structure and shit :)
        Control ngjdngjd = new()
        {
            CustomMinimumSize = new Vector2(200, 0)
        };
        AddChild(ngjdngjd);

		VSeparator gregregation = new();
		gregregation.SetAnchorsPreset(LayoutPreset.FullRect);
		AddChild(ngjdngjd);

		// sorry for that
		ScrollContainer thatThingWhenYouDragYourFingerFromTheBottomToTheTopOfTheScreen = new();
		thatThingWhenYouDragYourFingerFromTheBottomToTheTopOfTheScreen.SetAnchorsPreset(LayoutPreset.FullRect);
		ngjdngjd.AddChild(thatThingWhenYouDragYourFingerFromTheBottomToTheTopOfTheScreen);

		Sidebar = new VBoxContainer();
		thatThingWhenYouDragYourFingerFromTheBottomToTheTopOfTheScreen.AddChild(Sidebar);

		Button addTab = new()
		{
			Text = "Add Tab",
			ThemeTypeVariation = "SidebarButton",
			TextOverrunBehavior = TextServer.OverrunBehavior.TrimEllipsis,
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			CustomMinimumSize = new Vector2(200, 40)
		};
		addTab.Pressed += AddTab;
		Sidebar.AddChild(addTab);

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
			Text = "New Tab",
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
}
