using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class RemoveFromQuickLaunch : Button {
    public Lelapp App;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        HBoxContainer yes = GetNode<HBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/QuickLaunch");
        foreach (DefaultOpenWindowButton quickSettingsThing in yes.GetChildren()) {
            if (quickSettingsThing.WindowScene == App.Scene) {
                quickSettingsThing.QueueFree();
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
