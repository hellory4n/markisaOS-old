using Godot;
using System;
using System.Collections.Generic;
using Dashboard.Wm;
using Dashboard.Overlay;
using Kickstart.Cabinetfs;

public partial class WebsitesAppDownloaderWindow : MksWindow {
    public string NewFilename = "";
    public string Type = "Text";
    public int ProgressBarMaxValue = 1;
    public Dictionary<string, object> Data = new();
    Random random = new();
    bool m = false;

    public override void _Ready() {
        base._Ready();
        GetNode<ProgressBar>("M/N/ProgressBar").MaxValue = ProgressBarMaxValue;
    }

    public override void _Process(double delta) {
        base._Process(delta);
        var j = GetNode<ProgressBar>("M/N/ProgressBar");
        // makes the progress bar look janky
        if (random.Next(0, 3) == 1)
            j.Value += random.Next(1, 5);
        if (j.Value >= j.MaxValue && !m) {
            File h = CabinetfsManager.NewFile(
                NewFilename, CabinetfsManager.GetId("/Home/Downloads")
            );
            h.Type = Type;
            h.Data = Data;
            h.Save();

            var notificationManager = GetNode<NotificationManager>("/root/NotificationManager");
            notificationManager.ShowNotification($"File {NewFilename} has been downloaded.", "Websites");

            m = true;
            EmitSignal(SignalName.CloseRequested);
        }
    }
}
