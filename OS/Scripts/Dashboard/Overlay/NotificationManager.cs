using Godot;
using System;

namespace Dashboard.Overlay;

/// <summary>
/// Manages notifications.
/// </summary>
public partial class NotificationManager : Node
{
    readonly PackedScene ye = GD.Load<PackedScene>("res://OS/Dashboard/Overlay/Notification.tscn");

    /// <summary>
    /// Shows a notification.
    /// </summary>
    /// <param name="text">The text to show in the notification.</param>
    /// <param name="app">The app that sent this notification.</param>
    public void ShowNotification(string text, string app)
    {
        Panel notificationThingy = ye.Instantiate<Panel>();
        notificationThingy.GetNode<Label>("Text").Text = text;
        notificationThingy.GetNode<Label>("App").Text = app;
        AddChild(notificationThingy);
    }

    /// <summary>
    /// Shows a notification with an error sound.
    /// </summary>
    /// <param name="text">The text to show in the notification.</param>
    /// /// <param name="app">The app that sent this notification.</param>
    public void ShowErrorNotification(string text, string app)
    {
        Panel notificationThingy = ye.Instantiate<Panel>();
        notificationThingy.GetNode<Label>("Text").Text = text;
        notificationThingy.GetNode<Label>("App").Text = app;
        AddChild(notificationThingy);
    }
}
