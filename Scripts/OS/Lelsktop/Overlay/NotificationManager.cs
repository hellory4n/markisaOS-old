using Godot;
using System;

public class NotificationManager : Node {
    public void ShowNotification(string text) {
        PackedScene ye = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Overlay/Notification.tscn");
        Panel notificationThingy = ye.Instance<Panel>();
        notificationThingy.GetNode<Label>("Text").Text = text;
        AddChild(notificationThingy);

        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Notification);
    }
}
