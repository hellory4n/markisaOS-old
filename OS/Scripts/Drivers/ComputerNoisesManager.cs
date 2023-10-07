using Godot;
using System;

public class ComputerNoisesManager : Node {
    readonly AudioStream FanSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/194890__saphe__computer-fan.ogg");
    readonly AudioStream GpuFanSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/463482__soundsofscienceupf__gpu-fan.wav");
    readonly AudioStream HddSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/500168__sad3d__pc-hard-drive-noises.wav");
    AudioStreamPlayer Fan;
    AudioStreamPlayer GpuFan;
    AudioStreamPlayer Hdd;

    public override void _Ready() {
        base._Ready();
        Fan = new AudioStreamPlayer {
            Stream = FanSound,
            Autoplay = true,
            VolumeDb = GD.Linear2Db(0.5f)
        };
        GpuFan = new AudioStreamPlayer {
            Stream = GpuFanSound,
            Autoplay = true,
            VolumeDb = GD.Linear2Db(0.0333333333333f)
        };
        Hdd = new AudioStreamPlayer {
            Stream = HddSound,
            Autoplay = true,
            VolumeDb = GD.Linear2Db(0.0666666666667f)
        };
        AddChild(Fan);
        AddChild(GpuFan);
        AddChild(Hdd);
    }

    public override void _Process(float delta) {
        base._Process(delta);

        if (GetNodeOrNull("/root/Lelsktop") == null)
            return;

        // first we need to get how much the device is suffering
        // we need to check every single workspace :))))))))
        float seepeeyou = 10;
        float geepeeyou = 5;
        float memory = 10;
        float storage = 10;

        foreach (BaseWindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()) {
            seepeeyou += window.CpuUse;
            geepeeyou += window.GpuUse;
            memory += window.MemoryUse;
            storage += window.StorageUse;
        }

        foreach (BaseWindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()) {
            seepeeyou += window.CpuUse;
            geepeeyou += window.GpuUse;
            memory += window.MemoryUse;
            storage += window.StorageUse;
        }

        foreach (BaseWindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()) {
            seepeeyou += window.CpuUse;
            geepeeyou += window.GpuUse;
            memory += window.MemoryUse;
            storage += window.StorageUse;
        }

        foreach (BaseWindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()) {
            seepeeyou += window.CpuUse;
            geepeeyou += window.GpuUse;
            memory += window.MemoryUse;
            storage += window.StorageUse;
        }

        // if we use 200% of the cpu the computer is gonna explode
        seepeeyou = Math.Min(seepeeyou, 100)/100;
        geepeeyou = Math.Min(geepeeyou, 100)/100;
        memory = Math.Min(memory, 100)/100;
        storage = Math.Min(storage, 100)/100;

        Fan.VolumeDb = GD.Linear2Db(seepeeyou/2);
        GpuFan.VolumeDb = GD.Linear2Db(geepeeyou/1.5f);
        Hdd.VolumeDb = GD.Linear2Db(storage/1.5f);
    }
}
