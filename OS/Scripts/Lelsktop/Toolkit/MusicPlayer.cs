using Godot;
using System;

public class MusicPlayer : Node {
    [Export]
    public AudioStream Music;
    [Export]
    public float PitchScale = 1;
    [Export]
    public bool Paused = false;
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

        if (CanPlay && !Paused) {
            musicManager.SetActiveMusic(PlayerIndex);
        }

        musicManager.SetPitchScale(PlayerIndex, PitchScale);

        // test :)))))
        if (Input.IsActionJustReleased("skip_boot")) {
            Paused = !Paused;
        }
    }

    /// <summary>
    /// Returns the position in the AudioStream in seconds.
    /// </summary>
    /// <returns>The position in the AudioStream in seconds.</returns>
    public float GetPlaybackPosition() {
        return musicManager.GetChild<AudioStreamPlayer>(PlayerIndex).GetPlaybackPosition();
    }
}
