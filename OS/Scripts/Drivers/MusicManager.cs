using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages music. NOTE: You should avoid using this class directly and instead use the MusicPlayer node.
/// </summary>
public partial class MusicManager : Node {
    List<AudioStreamPlayer> Players = new List<AudioStreamPlayer>();
    public List<bool> PausedPlayers = new List<bool>();
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
        PausedPlayers.Add(false);
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
            if (!PausedPlayers[i])
                yes.StreamPaused = !(i == ActiveMusic);
            else
                yes.StreamPaused = true;
        }

        int m = AudioServer.GetBusIndex("Music");
        AudioServer.SetBusVolumeDb(m, MusicVolume);
    }

    public void ExplodeEverything() {
        Players.Clear();
        PausedPlayers.Clear();
        ActiveMusic = 0;

        foreach (Node item in GetChildren()) {
            item.QueueFree();
        }
    }
}
