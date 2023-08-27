using Godot;
using System;

public class EpicCoolAmazingBeautifulGorgeousFantasticMajesticButton : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        NotificationManager notifications = GetNode<NotificationManager>("/root/NotificationManager");
        notifications.ShowNotification("your amazon delivery has arrived");
    }
}
