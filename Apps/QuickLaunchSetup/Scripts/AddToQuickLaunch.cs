using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Toolkit;

public partial class AddToQuickLaunch : Button {
    public Lelapp App;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/QuickLaunchButton.tscn");
        OpenWindow yes = packedScene.Instantiate<OpenWindow>();
        yes.Icon = GD.Load<Texture2D>(App.Icon);
        yes.WindowScene = App.Scene;
        GetNode<VBoxContainer>("/root/DashboardInterface/Dock/DockStuff/QuickLaunch").AddChild(yes);

        QuickLaunch quickLaunch = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser);
        List<Lelapp> pain = quickLaunch.Apps.ToList();
        pain.Add(App);
        SavingManager.Save(SavingManager.CurrentUser, new QuickLaunch{
            Apps = pain.ToArray()
        });

        GetParent().GetParent<ListAppsButQuickLaunch>().UpdateItems();
    }
}
