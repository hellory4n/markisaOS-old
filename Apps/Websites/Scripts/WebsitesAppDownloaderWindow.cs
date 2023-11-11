using Godot;
using System;
using System.Collections.Generic;
using Lelsktop.WindowManager;
using Lelsktop.Overlay;

public partial class WebsitesAppDownloaderWindow : Lelwindow {
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
            LelfsFile h = LelfsManager.NewFile(
                NewFilename, LelfsManager.PermanentPath("/Home/Downloads")
            );
            h.Type = Type;
            h.Data = Data;
            h.Save();

            var notificationManager = GetNode<NotificationManager>("/root/NotificationManager");
            notificationManager.ShowNotification($"File {NewFilename} has been downloaded.");

            m = true;
            Close();
        }
    }
}
