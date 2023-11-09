using Godot;
using System;

public partial class SystemMonitorThingy : ProgressBar {
    [Export(PropertyHint.Enum, "CPU,GPU,Memory,Storage")]
    string Thingy;

    ComputerNoisesManager computerNoises;

    public override void _Ready() {
        base._Ready();
        computerNoises = GetNode<ComputerNoisesManager>("/root/ComputerNoisesManager");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        switch (Thingy) {
            case "CPU":
                Value = computerNoises.CpuUsage*100;
                break;
            case "GPU":
                Value = computerNoises.GpuUsage*100;
                break;
            case "Memory":
                Value = computerNoises.MemoryUsage*100;
                break;
            case "Storage":
                Value = computerNoises.StorageUsage*100;
                break;
        }
    }
}
