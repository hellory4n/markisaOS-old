using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Toolkit;
using Kickstart.Records;

namespace QuickLaunchSetup;

public partial class AddToQuickLaunch : Button
{
    public Package App;

    public override void _Pressed()
    {
        base._Pressed();
        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/QuickLaunchButton.tscn");
        OpenWindow yes = packedScene.Instantiate<OpenWindow>();
        yes.Icon = GD.Load<Texture2D>(App.Icon);
        yes.WindowScene = App.Executable;
        GetNode<VBoxContainer>("/root/DashboardInterface/Dock/DockStuff/QuickLaunch").AddChild(yes);

        var m = RecordManager.Load<DashboardConfig>();
        m.QuickLaunch.Add(App);
        RecordManager.Save(m);

        GetParent().GetParent<ListAppsButQuickLaunch>().UpdateItems();
    }
}
