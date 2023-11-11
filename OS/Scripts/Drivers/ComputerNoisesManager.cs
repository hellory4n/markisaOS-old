using Godot;
using System;
using Lelsktop.WindowManager;

namespace Lelcore.Drivers;

public partial class ComputerNoisesManager : Node
{
    readonly AudioStream FanSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/194890__saphe__computer-fan.ogg");
    readonly AudioStream GpuFanSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/463482__soundsofscienceupf__gpu-fan.wav");
    readonly AudioStream HddSound = ResourceLoader.Load<AudioStream>("res://Audio/Sounds/ComputerNoises/500168__sad3d__pc-hard-drive-noises.wav");
    AudioStreamPlayer Fan;
    AudioStreamPlayer GpuFan;
    AudioStreamPlayer Hdd;
    public float CpuUsage = 10;
    public float GpuUsage = 5;
    public float StorageUsage = 10;
    public float MemoryUsage = 10;

    public override void _Ready()
    {
        base._Ready();
        // h
        /*CpuUsage = Math.Min(CpuUsage, 100)/100;
        GpuUsage = Math.Min(GpuUsage, 100)/100;
        StorageUsage = Math.Min(StorageUsage, 100)/100;

        Fan = new AudioStreamPlayer {
            Stream = FanSound,
            Autoplay = true,
            VolumeDb = GD.LinearToDb(CpuUsage/1.5f)
        };
        GpuFan = new AudioStreamPlayer {
            Stream = GpuFanSound,
            Autoplay = true,
            VolumeDb = GD.LinearToDb(GpuUsage/1.25f)
        };
        Hdd = new AudioStreamPlayer {
            Stream = HddSound,
            Autoplay = true,
            VolumeDb = GD.LinearToDb(StorageUsage*1.25f)
        };
        AddChild(Fan);
        AddChild(GpuFan);
        AddChild(Hdd);*/
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        /*if (GetNodeOrNull("/root/Lelsktop") == null)
            return;

        // first we need to get how much the device is suffering
        float cpuOmg = 10;
        float gpuOmg = 5;
        float memoryOmg = 10;
        float storageOmg = 10;
        // we need to check every single workspace :))))))))

        foreach (Lelwindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()) {
            cpuOmg += window.CpuUse;
            gpuOmg += window.GpuUse;
            memoryOmg += window.MemoryUse;
            storageOmg += window.StorageUse;
        }

        foreach (Lelwindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()) {
            cpuOmg += window.CpuUse;
            gpuOmg += window.GpuUse;
            memoryOmg += window.MemoryUse;
            storageOmg += window.StorageUse;
        }

        foreach (Lelwindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()) {
            cpuOmg += window.CpuUse;
            gpuOmg += window.GpuUse;
            memoryOmg += window.MemoryUse;
            storageOmg += window.StorageUse;
        }

        foreach (Lelwindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()) {
            cpuOmg += window.CpuUse;
            gpuOmg += window.GpuUse;
            memoryOmg += window.MemoryUse;
            storageOmg += window.StorageUse;
        }

        // if we use 200% of the cpu the computer is gonna explode
        CpuUsage = Math.Min(cpuOmg, 100)/100;
        GpuUsage = Math.Min(gpuOmg, 100)/100;
        MemoryUsage = Math.Min(memoryOmg, 100)/100;
        StorageUsage = Math.Min(storageOmg, 100)/100;

        Fan.VolumeDb = GD.LinearToDb(CpuUsage/2);
        GpuFan.VolumeDb = GD.LinearToDb(GpuUsage/1.5f);
        Hdd.VolumeDb = GD.LinearToDb(StorageUsage);*/
    }
}
