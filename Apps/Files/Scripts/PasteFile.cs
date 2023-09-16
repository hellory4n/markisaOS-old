using Godot;
using System;

public class PasteFile : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string OldFile;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string gkfngof;
        string suffering;
        if (Parent != "/") {
            Folder parent = LelfsManager.LoadById<Folder>(Parent);
            gkfngof = parent.Path;
            suffering = $"{parent.Path}/{filename}";
        } else {
            gkfngof = "/";
            suffering = $"/{filename}";
        }

        // making a file that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "File already exists!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually copy the file :)
        BaseLelfs oldFile = LelfsManager.LoadById<BaseLelfs>(OldFile);
        if (oldFile.Type != "Folder") {
            if (Parent != "/")
                oldFile.Copy<BaseLelfs>(filename, Parent);
            else
                oldFile.Copy<BaseLelfs>(filename);
        } else {
            Folder oldFileButItsAFolder = LelfsManager.LoadById<Folder>(OldFile);
            if (Parent != "/")
                oldFileButItsAFolder.CopyFolder(filename, Parent);
            else
                oldFileButItsAFolder.CopyFolder(filename);
        }

        Close();
        ThingThatINeedToRefresh.Refresh(gkfngof);
    }
}
