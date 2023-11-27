using Godot;
using System;
using Kickstart.Records;

namespace Settings;

public partial class SystemInfo : Label {
    public override void _Ready() {
        base._Ready();
        MarkisaUser user = RecordManager.Load<MarkisaUser>();
        string model;
        if (OS.GetModelName() == "GenericDevice")
            model = "Placeholder Device™";
        else
            model = OS.GetModelName();

        string cpu;
        if (OS.GetProcessorName() == "")
            cpu = "Placeholder Processor™";
        else
            cpu = OS.GetProcessorName();

        Text = $"markisaOS {user.VersionToString()}\n© Passionfruit 2069\n{model}\n{cpu}";
    }
}
