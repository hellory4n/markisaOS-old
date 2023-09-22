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
            case Mode.Nothing:
                PackedScene m1 = ResourceLoader.Load<PackedScene>("res://Apps/Observer/NothingLoaded.tscn");
                CenterContainer coolNothingThing = m1.Instance<CenterContainer>();
                AddChild(coolNothingThing);
                break;
        }
    }
}