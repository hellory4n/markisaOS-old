using Godot;
using System;

public partial class WebMusicPlayer : Node {
    [Export]
    public AudioStream Music;
    [Export]
    public float PitchScale = 1;
    [Export]
    public NodePath WebsiteRoot = "";
    MusicManager musicManager;
    public int PlayerIndex;
    Leltabs Leltabs;

    public override void _Ready() {
        base._Ready();
        musicManager = GetNode<MusicManager>("/root/MusicManager");
        PlayerIndex = musicManager.AddMusic(Music);
        Leltabs = GetNode(WebsiteRoot).GetParent().GetParent().GetNode<Leltabs>("Leltabs/TabBar");
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // yes.
        if (GetNode(WebsiteRoot).GetParent().GetParent<Lelwindow>().IsClosing) {
            musicManager.DeletePlayer(PlayerIndex);
            return;
        }

        if (Leltabs.ActiveTab == GetNode(WebsiteRoot)) {
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
