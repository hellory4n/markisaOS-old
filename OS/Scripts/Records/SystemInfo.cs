using Godot;
using System;
using System.Collections.Generic;

namespace Kickstart.Records;

public partial record SystemInfo : Record
{
    public SystemInfo()
    {
        Filename = "SystemInfo";
        Type = RecordType.Global;
    }

    /// <summary>
    /// If true, the player has already finished the installation process.
    /// </summary>
    public bool Installed = false;
    /// <summary>
    /// A path to what scene should the game load after markisaOS finishes booting.
    /// </summary>
    public string Onboarding = "res://OS/Kickstart/Onboarding.tscn";
    public List<string> Achievements = new();
}