using Godot;
using System;
using Dashboard.Toolkit;
using Kickstart.Records;
using Dashboard.Wm;
using System.Linq;

namespace QuickLaunchSetup;

public partial class RemoveApp : Button
{
    public Package App;
    [Export(PropertyHint.File, "*.tscn")]
    string QuickLaunchSetup = "";

    public override void _Pressed()
    {
        base._Pressed();
        VBoxContainer yes = GetNode<VBoxContainer>("/root/Dashboard/Inter/Face/Dock/DockStuff/QuickLaunch");
        foreach (Node quickSettingsThing in yes.GetChildren())
        {
            if (quickSettingsThing is OpenWindow h)
            {
                if (h.WindowScene == App.Executable)
                    h.QueueFree();
            }
        }

        var m = new Record<DashboardConfig>();
        m.Data.QuickLaunch = m.Data.QuickLaunch.SkipWhile(x => x.Executable == App.Executable).ToList();
        m.Save();

        // couldn't make it delete all of the children properly for some reason so it just relaunches the app lol
        GetParent().GetParent().GetParent().GetParent<MksWindow>().EmitSignal(MksWindow.SignalName.CloseRequested);
        MksWindow fucker = GD.Load<PackedScene>(QuickLaunchSetup).Instantiate<MksWindow>();
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        wm.AddWindow(fucker);
    }
}
