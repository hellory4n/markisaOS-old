using Godot;
using System;
using System.Collections.Generic;

public partial class Download : Button {
    [Export]
    string NewFilename = "";
    [Export(PropertyHint.Enum, "Text")]
    string Type = "Text";
    [Export]
    int ProgressBarMaxValue = 1;
    [Export]
    string DataKey = "";
    [Export(PropertyHint.MultilineText)]
    string DataValue = "";

    /*public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Websites/Downloader.tscn");
        WebsitesAppDownloaderWindow jjkn = m.Instantiate<WebsitesAppDownloaderWindow>();
        jjkn.NewFilename = NewFilename;
        jjkn.Type = Type;
        jjkn.ProgressBarMaxValue = ProgressBarMaxValue;
        // exporting a dictionary doesn't work
        jjkn.Data.Add(DataKey, DataValue);
        wm.AddWindow(jjkn);
    }*/
}
