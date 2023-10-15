using Godot;
using System;

public class InstallelSteps : Label {
    public override void _Process(float delta) {
        base._Process(delta);
        var yes = GetNode<ProgressBar>("../ProgressBar");

        // yandere simulator
        double max = yes.MaxValue;
        if (yes.Value/max > 0.999) {
            PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelFinish.tscn");
            BaseWindow jjkn = (BaseWindow)m.Instance();    
            GetNode<Control>("/root/Installel/1/Windows/ThemeThing").AddChild(jjkn);
            jjkn.Visible = true;
            GetParent().GetParent<BaseWindow>().Close();
            QueueFree();
        } else if (yes.Value/max > 0.91) {
            Text = "Finishing installation...";
        } else if (yes.Value/max > 0.81) {
            Text = "Installing additional updates...";
        } else if (yes.Value/max > 0.75) {
            Text = "Installing additional software...";
        } else if (yes.Value/max > 0.69) {
            Text = "Installing drivers...";
        } else if (yes.Value/max > 0.53) {
            Text = "Installing system apps...";
        } else if (yes.Value/max > 0.31) {
            Text = "Installing lelsktop...";
        } else if (yes.Value/max > 0.23) {
            Text = "Installing lelcore...";
        } else if (yes.Value/max > 0.20) {
            Text = "Installing bootloader...";
        } else if (yes.Value/max > 0.02) {
            Text = "Partitioning storage device...";
        } else {
            Text = "Starting installation...";
        }
    }
}
