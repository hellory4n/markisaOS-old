using Godot;
using System;

public partial class Delete : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string CoolFile;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/M/Delete").Connect("pressed", new Callable(this, nameof(Click)));
        GetNode<Button>("CenterContainer/VBoxContainer/M/Nvm").Connect("pressed", new Callable(this, nameof(NvmLol)));
        var soundManager = GetNode<SoundManager>("/root/SoundManager");
        soundManager.PlaySoundEffect(SoundManager.SoundEffects.Warning);
    }

    public void Click() {
        LelfsFile bruh = LelfsManager.LoadById<LelfsFile>(CoolFile);
        if (GetNode<CheckBox>("CenterContainer/VBoxContainer/PermanentlyDelete").Pressed) {
            // permanently delete
            if (bruh.Type == "Folder") {
                Folder m = LelfsManager.LoadById<Folder>(CoolFile);
                m.Delete();
            } else {
                bruh.Delete();
            }
        } else {
            // try to move it
            if (!LelfsManager.FileExists("/System/Trash")) {
                var soundManager = GetNode<SoundManager>("/root/SoundManager");
                soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
                GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "Couldn't find the trash folder! Your system could be corrupted.";
                return;
            }

            if (bruh.Type == "Folder") {
                Folder m = LelfsManager.LoadById<Folder>(CoolFile);
                // make sure we can put bajillions of files in the trash
                m.Rename($"{m.Name} - {m.Id}");
                m.Move(
                    LelfsManager.PermanentPath("/System/Trash")
                );
            } else {
                // make sure we can put bajillions of files in the trash
                bruh.Rename($"{bruh.Name} - {bruh.Id}");
                bruh.Move(
                    LelfsManager.PermanentPath("/System/Trash")
                );
            }
        }

        Close();
        ThingThatINeedToRefresh.Refresh(ThingThatINeedToRefresh.Path3D, false);
    }

    public void NvmLol() {
        Close();
    }
}
