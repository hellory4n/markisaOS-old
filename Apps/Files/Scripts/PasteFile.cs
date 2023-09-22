using Godot;
using System;

public class PasteFile : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string OldFile;
    public bool Move;

    public override void _Ready() {
        base._Ready();
        // we don't need to ask for a new name to move files :)
        if (Move) {
            RectMinSize = new Vector2(0, 0);
            RectSize = new Vector2(0, 0);
            RectClipContent = true;
            Click();
            Close();
        } else {
            GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", this, nameof(Click));
        }
    }

    public void Click() {
        if (!Move) {
            Copy();
        } else {
            Cut();
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (Move) {
            Close();
        }
    }

    public void Copy() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string suffering;

        Folder parent = LelfsManager.LoadById<Folder>(Parent);
        string gkfngof = parent.Path;
        if (Parent == "/")
            suffering = $"/{filename}";
        else
            suffering = $"{parent.Path}/{filename}";

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
        string gkfngof = parent.Path;
        string suffering;
        if (Parent == "root")
            suffering = $"/{oldFile.Name}";
        else
            suffering = $"{parent.Path}/{oldFile.Name}";

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
