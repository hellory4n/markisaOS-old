using Godot;
using System;
using Dashboard.Toolkit;
using Kickstart.Records;
using Dashboard.Wm;

namespace QuickLaunchSetup;

public partial class AddApp : Button
{
    public Package App;
    [Export(PropertyHint.File, "*.tscn")]
    string QuickLaunchSetup = "";

    public override void _Pressed()
    {
        base._Pressed();
        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/QuickLaunchButton.tscn");
        OpenWindow yes = packedScene.Instantiate<OpenWindow>();
        yes.Icon = GD.Load<Texture2D>(App.Icon);
        yes.WindowScene = App.Executable;
        GetNode<VBoxContainer>("/root/Dashboard/Inter/Face/Dock/DockStuff/QuickLaunch").AddChild(yes);

        var m = new Record<DashboardConfig>();
        m.Data.QuickLaunch.Add(App);
        m.Save();

        // couldn't make it delete all of the children properly for some reason so it just relaunches the app lol
        GetParent().GetParent().GetParent().GetParent<MksWindow>().EmitSignal(MksWindow.SignalName.CloseRequested);
        MksWindow fucker = GD.Load<PackedScene>(QuickLaunchSetup).Instantiate<MksWindow>();
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        wm.AddWindow(fucker);
    }
}
