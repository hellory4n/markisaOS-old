using Godot;
using System;

public partial class MusicPlayer : Node {
    [Export]
    public AudioStream Music;
    [Export]
    public float PitchScale = 1;
    [Export]
    public NodePath WindowPath = "";
    [Export]
    public bool UseCustomCheck = false;
    public bool CanPlay = true;
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
        if (GetNodeOrNull(WindowPath) != null) {
            if (GetNode<BaseWindow>(WindowPath).IsClosing) {
                musicManager.DeletePlayer(PlayerIndex);
                return;
            }
        }

        if (!UseCustomCheck) {
            CanPlay = GetNode<BaseWindow>(WindowPath).IsActive();
        }

        if (CanPlay) {
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
        musicManager.PausedPlayers[PlayerIndex] = pause;
    }

    public bool GetPaused() {
        return musicManager.PausedPlayers[PlayerIndex];
    }
}
