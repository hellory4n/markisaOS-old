using Godot;
using System;

public partial class PasteFile : Lelwindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string OldFile;
    public bool Move;

    public override void _Ready() {
        base._Ready();
        // we don't need to ask for a new name to move files :)
        if (Move) {
            CustomMinimumSize = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            ClipContents = true;
            Click();
            Close();
        } else {
            GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", new Callable(this, nameof(Click)));
        }
    }

    public void Click() {
        if (!Move) {
            Copy();
        } else {
            Cut();
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
        if (Move) {
            Close();
        }
    }

    public void Copy() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string suffering;

        Folder parent = LelfsManager.LoadById<Folder>(Parent);
        string gkfngof = parent.Path3D;
        if (Parent == "/")
            suffering = $"/{filename}";
        else
            suffering = $"{parent.Path3D}/{filename}";

        // making a file that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "File already exists!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually copy the file :)
        LelfsFile oldFile = LelfsManager.LoadById<LelfsFile>(OldFile);
        if (oldFile.Type != "Folder") {
            oldFile.Copy(filename, Parent);
        } else {
            Folder oldFileButItsAFolder = LelfsManager.LoadById<Folder>(OldFile);
            oldFileButItsAFolder.Copy(filename, Parent);
        }

        Close();
        ThingThatINeedToRefresh.ToCopy = null;
        ThingThatINeedToRefresh.Selected = null;
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }

    public void Cut() {
        LelfsFile oldFile = LelfsManager.LoadById<LelfsFile>(OldFile);
        Folder parent = LelfsManager.LoadById<Folder>(Parent);
        string gkfngof = parent.Path3D;
        string suffering;
        if (Parent == "root")
            suffering = $"/{oldFile.Name}";
        else
            suffering = $"{parent.Path3D}/{oldFile.Name}";

        // making a file that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually move the file :)
        if (oldFile.Type == "Folder") {
            Folder h = LelfsManager.LoadById<Folder>(OldFile);
            h.Move(Parent);
        } else {
            oldFile.Move(Parent);
        }

        ThingThatINeedToRefresh.ToMove = null;
        ThingThatINeedToRefresh.Selected = null;
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
