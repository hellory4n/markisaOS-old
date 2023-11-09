using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RemoveFromQuickLaunch : Button {
    public Lelapp App;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        VBoxContainer yes = GetNode<VBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/QuickLaunch");
        foreach (Node quickSettingsThing in yes.GetChildren()) {
            if (quickSettingsThing is DefaultOpenWindowButton) {
                DefaultOpenWindowButton h = GetNode<DefaultOpenWindowButton>(quickSettingsThing.GetPath());
                if (h.WindowScene == App.Scene) {
                    h.QueueFree();
                }
            }
        }

        QuickLaunch quickLaunch = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser);
        List<Lelapp> pain = quickLaunch.Apps.ToList();
        // .Remove() is no worky :(
        for (int i = 0; i < pain.Count; i++) {
            if (pain[i].Name == App.Name) {
                pain.RemoveAt(i);
                i--;
            }
        }
        SavingManager.Save(SavingManager.CurrentUser, new QuickLaunch {
            Apps = pain.ToArray()
        });

        GetParent().GetParent<ListAppsButQuickLaunch>().UpdateItems();
    }
}
