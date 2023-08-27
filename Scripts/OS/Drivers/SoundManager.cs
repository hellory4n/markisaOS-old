using Godot;
using System;
using System.Collections.Generic;

public class SoundManager : AudioStreamPlayer {
    public enum SoundEffects {
        Startup,
        Shutdown,
        Error,
        Notification
    }

    // it's on a list so the number of the enum is also the index in this list or something
    public List<AudioStreamMP3> soundFiles = new List<AudioStreamMP3>();

    public override void _Ready() {
        base._Ready();
        soundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Startup.mp3"));
        soundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Shutdown.mp3"));
        soundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Error.mp3"));
        soundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Notification.mp3"));
    }

    public void PlaySoundEffect(SoundEffects sound) {
        int enumButNumber = (int)sound;
        Stream = soundFiles[enumButNumber];
        Playing = true;
    }

    public void PlaySound(AudioStream sound) {
        Stream = sound;
        Playing = true;
    }
}
