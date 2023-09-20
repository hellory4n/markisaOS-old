using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages sound effects and music.
/// </summary>
public class SoundManager : Node {
    /// <summary>
    /// System sound effects.
    /// </summary>
    public enum SoundEffects {
        Startup,
        Shutdown,
        Error,
        Notification,
        Warning
    }

    // it's on a list so the number of the enum is also the index in this list or something, and also would
    // allow people to change them
    public List<AudioStreamMP3> SoundFiles = new List<AudioStreamMP3>();
    public Node sounds = new Node {
        Name = "Sounds"
    };
    public Node music = new Node {
        Name = "Music"
    };
    /// <summary>
    /// The volume of sound effects in decibels, I think.
    /// </summary>
    public float SoundVolume = 1;
    /// <summary>
    /// The volume of music in decibels, I think.
    /// </summary>
    public float MusicVolume = 1;

    public override void _Ready() {
        base._Ready();
        SoundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Startup.mp3"));
        SoundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Shutdown.mp3"));
        SoundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Error.mp3"));
        SoundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Notification.mp3"));
        SoundFiles.Add(ResourceLoader.Load<AudioStreamMP3>("res://Audio/Sounds/Warning.mp3"));

        AddChild(sounds);
        AddChild(music);
    }

    /// <summary>
    /// Plays a system sound effect.
    /// </summary>
    /// <param name="sound">The sound effect to play.</param>
    public void PlaySoundEffect(SoundEffects sound) {
        int enumButNumber = (int)sound;
        AudioStreamPlayer m = new AudioStreamPlayer {
            Stream = SoundFiles[enumButNumber],
            Bus = "Sounds"
        };
        sounds.AddChild(m);
        m.Playing = true;
    }

    /// <summary>
    /// Play any sound file.
    /// </summary>
    /// <param name="sound">The sound file to play.</param>
    public void PlaySound(AudioStream sound) {
        AudioStreamPlayer m = new AudioStreamPlayer {
            Stream = sound
        };
        sounds.AddChild(m);
        m.Playing = true;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // there's too many sound effects now, delete some of them
        if (sounds.GetChildCount() > 10) {
            sounds.GetChild(0).QueueFree();
        }

        int m = AudioServer.GetBusIndex("Sounds");
        AudioServer.SetBusVolumeDb(m, SoundVolume);
    }
}
