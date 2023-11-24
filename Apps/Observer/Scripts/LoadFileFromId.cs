using Godot;
using System;

public partial class LoadFileFromId : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string coolId = GetNode<LineEdit>("../LineEdit").Text;
        if (!CabinetfsManager.IdExists(coolId)) {
            GetNode<Label>("../Label").Text = "File doesn't exist!";
            return;
        }

        CabinetfsFile m = CabinetfsManager.LoadById<CabinetfsFile>(coolId);
        if (m.Type != "Picture" && m.Type != "Audio" && m.Type != "Video") {
            GetNode<Label>("../Label").Text = "Invalid file!";
            return;
        }

        // fun
        if (m.Type == "Picture")
            GetParent().GetParent().GetParent<Observer>().ObserverMode = Observer.OpenMode.Image;
        else if (m.Type == "Audio") {
            GetParent().GetParent().GetParent<Observer>().ObserverMode = Observer.OpenMode.Audio;
        } else if (m.Type == "Video") {
            GetParent().GetParent().GetParent<Observer>().ObserverMode = Observer.OpenMode.Video;
        }
        GetParent().GetParent().GetParent<Observer>().MediaId = coolId;
        GetParent().GetParent().GetParent<Observer>().Load();
        GetParent().GetParent().QueueFree();
    }
}
