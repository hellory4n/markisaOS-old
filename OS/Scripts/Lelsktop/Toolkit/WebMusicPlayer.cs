using Godot;
using System;

public class WebMusicPlayer : Node {
    [Export]
    public AudioStream Music;
    [Export]
    public float PitchScale = 1;
    [Export]
    public NodePath TabContentRoot = "";
    MusicManager musicManager;
    public int PlayerIndex;

    public override void _Ready() {
        base._Ready();
        musicManager = GetNode<MusicManager>("/root/MusicManager");
        PlayerIndex = musicManager.AddMusic(Music);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // yes.
        if (GetNode(TabContentRoot).GetParent<BaseWindow>().IsClosing) {
            musicManager.DeletePlayer(PlayerIndex);
            return;
        }

        if (GetNode(TabContentRoot).GetNodeOrNull("IsActiveTab") != null) {
            musicManager.SetActiveMusic(PlayerIndex);
        }

        musicManager.SetPitchScale(PlayerIndex, PitchScale);
    }

    /// <summary>
    /// Returns the position in the AudioStream in seconds.
    /// </summary>
    /// <returns>The position in the AudioStream in seconds.</returns>
    public float GetPlaybackPosition() {
        return musicManager.GetChild<AudioStreamPlayer>(PlayerIndex).GetPlaybackPosition();
    }

    public void SetPaused(bool pause) {
        musicManager.GetChild<AudioStreamPlayer>(PlayerIndex).StreamPaused = pause;
    }

    public bool GetPaused() {
        return musicManager.GetChild<AudioStreamPlayer>(PlayerIndex).StreamPaused; 
    }
}
