using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class AddToQuickLaunch : Button {
    public Lelapp App;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/QuickLaunchButton.tscn");
        DefaultOpenWindowButton yes = packedScene.Instance<DefaultOpenWindowButton>();
        yes.Icon = ResourceLoader.Load<Texture>(App.Icon);
        yes.WindowScene = App.Scene;
        GetNode<VBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/QuickLaunch").AddChild(yes);

        QuickLaunch quickLaunch = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser);
        List<Lelapp> pain = quickLaunch.Apps.ToList();
        pain.Add(App);
        SavingManager.Save(SavingManager.CurrentUser, new QuickLaunch{
            Apps = pain.ToArray()
        });

        GetParent().GetParent<ListAppsButQuickLaunch>().UpdateItems();
    }
}
