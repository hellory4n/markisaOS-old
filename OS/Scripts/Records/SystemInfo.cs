using Godot;
using System;
using System.Collections.Generic;

namespace Kickstart.Records;

public partial struct SystemInfo : IRecordData
{
    public readonly string GetFilename() { return "Settings/SystemInfo.json"; }
    /// <summary>
    /// If true, the player has already finished the installation process.
    /// </summary>
    public bool Installed = false;
    /// <summary>
    /// A path to what scene should the game load after markisaOS finishes booting.
    /// </summary>
    public string Onboarding = "res://OS/Kickstart/Onboarding.tscn";
    public List<string> Achievements = new();
    public string Language = "en";

    public SystemInfo() {}
}