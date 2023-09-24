using Godot;
using System;

public class LoadFileFromId : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        string coolId = GetNode<LineEdit>("../LineEdit").Text;
        if (!LelfsManager.IdExists(coolId)) {
            GetNode<Label>("../Label").Text = "File doesn't exist!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        LelfsFile m = LelfsManager.LoadById<LelfsFile>(coolId);
        if (m.Type != "Picture" && m.Type != "Audio") {
            GetNode<Label>("../Label").Text = "Invalid file!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // fun
        if (m.Type == "Picture")
            GetParent().GetParent().GetParent<Observer>().ObserverMode = Observer.Mode.Image;
        else {
            GetParent().GetParent().GetParent<Observer>().ObserverMode = Observer.Mode.Audio;
        }
        GetParent().GetParent().GetParent<Observer>().MediaId = coolId;
        GetParent().GetParent().GetParent<Observer>().Load();
        GetParent().GetParent().QueueFree();
    }
}
