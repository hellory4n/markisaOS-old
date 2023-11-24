using Godot;
using NathanHoad;
using System;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class SoundPlayer : Node
{
	[Export]
	public AudioStream Stream;
	
	[Export(PropertyHint.Range, "-80,24")]
	public double VolumeDb
	{
		get { return Player.VolumeDb; }
		set { Player.VolumeDb = (float)value; }
	}
	[Export(PropertyHint.Range, "0.01,4")]
	public double Pitch
	{
		get { return Player.PitchScale; }
		set { Player.PitchScale = (float)value; }
	}
	[Export]
	public bool Autoplay = false;
	[Export]
	public bool Paused
	{
		get { return Player.StreamPaused; }
		set { Player.StreamPaused = value; }
	}
	[Export]
	public bool UiSound = true;
	[Export]
	public string OverrideBus = "";
	
	[ExportSubgroup("System Sounds", "")]
	[Export]
	public bool PlaySystemSound = false;
	[Export(PropertyHint.Enum, "Startup:0,Shutdown:1,Logout:2,Warning:3,Error:4,Notification:5,CriticalError:6,Question:7,Success:8")]
	public int SystemSound = 0;
	
	AudioStreamPlayer Player = new();


    public override void _Ready()
    {
        base._Ready();
		// indeed
		if (PlaySystemSound)
		{
			Stream = SoundManager.SystemSoundFiles[SystemSound];
			Pitch = 1;
			VolumeDb = 0;
			UiSound = true;
		}
		
		if (Autoplay)
			Play();
    }

	public void Play()
	{
		if (UiSound)
			Player = SoundManager.PlayUISound(Stream, OverrideBus);
		else
			Player = SoundManager.PlaySound(Stream, OverrideBus);
	}

	public void Stop()
	{
		Player.Stop();
	}
}
