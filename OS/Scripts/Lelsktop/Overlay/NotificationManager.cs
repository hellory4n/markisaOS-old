using Godot;
using System;

/// <summary>
/// Manages notifications.
/// </summary>
public class NotificationManager : Node {
    /// <summary>
    /// Shows a notification.
    /// </summary>
    /// <param name="text">The text to show in the notification.</param>
    public void ShowNotification(string text) {
        PackedScene ye = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Overlay/Notification.tscn");
        Panel notificationThingy = ye.Instance<Panel>();
        notificationThingy.GetNode<Label>("Text").Text = text;
        AddChild(notificationThingy);

        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Notification);
    }

    /// <summary>
    /// Shows a notification with an error sound.
    /// </summary>
    /// <param name="text">The text to show in the notification.</param>
    public void ShowErrorNotification(string text) {
        PackedScene ye = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Overlay/Notification.tscn");
        Panel notificationThingy = ye.Instance<Panel>();
        notificationThingy.GetNode<Label>("Text").Text = text;
        AddChild(notificationThingy);

        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
    }
}
