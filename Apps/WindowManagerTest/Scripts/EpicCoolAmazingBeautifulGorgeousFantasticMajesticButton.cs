using Godot;
using System;
using Lelsktop.Overlay;

public partial class EpicCoolAmazingBeautifulGorgeousFantasticMajesticButton : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        NotificationManager notifications = GetNode<NotificationManager>("/root/NotificationManager");
        notifications.ShowNotification("your amazon delivery has arrived");
    }
}
