using Godot;
using System;

public class Observer : BaseWindow {
    public enum Mode {
        Image,
        Audio,
        Video,
        Nothing
    }

    public Mode ObserverMode = Mode.Nothing;
    public string MediaId;

    public override void _Ready() {
        base._Ready();
        Load();
    }

    public void Load() {
        switch (ObserverMode) {
            case Mode.Image:
                PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Observer/ImageView.tscn");
                Control coolImageThing = m.Instance<Control>();

                LelfsFile coolFile = LelfsManager.LoadById<LelfsFile>(MediaId);
                if (coolFile.Data.ContainsKey("Resource")) {
                    // one of the codes of all time
                    coolImageThing.GetNode<TextureRect>("Image").Texture = 
                        ResourceLoader.Load<Texture>(coolFile.Data["Resource"].ToString());
                }
                AddChild(coolImageThing);
                break;
            case Mode.Audio:
                PackedScene m2 = ResourceLoader.Load<PackedScene>("res://Apps/Observer/MusicPlayer.tscn");
                Control coolMusicThing = m2.Instance<Control>();

                LelfsFile epicFile = LelfsManager.LoadById<LelfsFile>(MediaId);
                if (epicFile.Data.ContainsKey("Resource")) {
                    // one of the codes of all time
                    coolMusicThing.GetNode<AudioStreamPlayer>("M/N/O/Audio").Stream = 
                        ResourceLoader.Load<AudioStream>(epicFile.Data["Resource"].ToString());
                }

                // figure out the epic cool name :)
                string coolName = "";
                if (epicFile.Metadata.ContainsKey("Artist")) 
                    coolName += $"{epicFile.Metadata["Artist"]} - ";
                if (epicFile.Metadata.ContainsKey("Album"))
                    coolName += $"{epicFile.Metadata["Album"]} - ";
                if (epicFile.Metadata.ContainsKey("TrackNumber"))
                    coolName += $"{epicFile.Metadata["TrackNumber"]} ";
                if (epicFile.Metadata.ContainsKey("Title"))
                    coolName += $"{epicFile.Metadata["Title"]}";
                if (!epicFile.Metadata.ContainsKey("Title"))
                    coolName = epicFile.Name;

                coolMusicThing.GetNode<Label>("M/N/Label").Text = coolName;

                AddChild(coolMusicThing);
                break;
            case Mode.Nothing:
                PackedScene m1 = ResourceLoader.Load<PackedScene>("res://Apps/Observer/NothingLoaded.tscn");
                CenterContainer coolNothingThing = m1.Instance<CenterContainer>();
                AddChild(coolNothingThing);
                break;
        }
    }
}