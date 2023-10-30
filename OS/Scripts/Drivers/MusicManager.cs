using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages music. NOTE: You should avoid using this class directly and instead use the MusicPlayer node.
/// </summary>
public class MusicManager : Node {
    List<AudioStreamPlayer> Players = new List<AudioStreamPlayer>();
    int ActiveMusic = 0;
    public float MusicVolume = 0;

    public int AddMusic(AudioStream audio) {
        var player = new AudioStreamPlayer {
            Stream = audio,
            Bus = "Music",
            Autoplay = true,
            StreamPaused = true
        };
        AddChild(player);
        Players.Add(player);
        return Players.Count-1;
    }

    public void SetActiveMusic(int index) {
        ActiveMusic = index;
    }

    public void SetPitchScale(int index, float pitch) {
        Players[index].PitchScale = pitch;
    }

    public void DeletePlayer(int index) {
        GetChild<AudioStreamPlayer>(index).Stop();
        GetChild<AudioStreamPlayer>(index).VolumeDb = int.MinValue;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        for (int i = 0; i < Players.Count; i++) {
            AudioStreamPlayer yes = Players[i];
            yes.StreamPaused = !(i == ActiveMusic);
        }

        int m = AudioServer.GetBusIndex("Music");
        AudioServer.SetBusVolumeDb(m, MusicVolume);
    }
}
