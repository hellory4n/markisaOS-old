using Godot;
using System;
using System.Reflection;

public partial class SystemInfo : Label {
    public override void _Ready() {
        base._Ready();
        BasicUser version = SavingManager.Load<BasicUser>(SavingManager.CurrentUser);
        string model;
        if (OS.GetModelName() == "GenericDevice")
            model = "Unable to get model name™";
        else
            model = OS.GetModelName();

        string cpu;
        if (OS.GetProcessorName() == "")
            cpu = "Unable to get CPU name™";
        else
            cpu = OS.GetProcessorName();

        Text = $"lelcubeOS v{version.MajorVersion}.{version.MinorVersion}.{version.PatchVersion}\n© Passionfruit 2069\nModel {model}\n{cpu}";
    }
}
