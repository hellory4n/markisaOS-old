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
            RectPosition = new Vector2(78923478, 83948458);
            Click();
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

    public void Copy() {
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
                LelfsManager.Copy(oldFile.Id, filename, Parent);
            else
                LelfsManager.Copy(oldFile.Id, filename);
        } else {
            Folder oldFileButItsAFolder = LelfsManager.LoadById<Folder>(OldFile);
            if (Parent != "/")
                oldFileButItsAFolder.Copy(filename, Parent);
            else
                oldFileButItsAFolder.Copy(filename);
        }

        Close();
        ThingThatINeedToRefresh.ToCopy = null;
        ThingThatINeedToRefresh.Selected = null;
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }

    public void Cut() {
        BaseLelfs oldFile = LelfsManager.LoadById<BaseLelfs>(OldFile);
        string gkfngof;
        string suffering;
        if (Parent != "/") {
            Folder parent = LelfsManager.LoadById<Folder>(Parent);
            gkfngof = parent.Path;
            suffering = $"{parent.Path}/{oldFile.Name}";
        } else {
            gkfngof = "/";
            suffering = $"/{oldFile.Name}";
        }

        // making a file that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually move the file :)
        if (Parent != "/") {
            if (oldFile.Parent != null) {
                Folder pain = LelfsManager.LoadById<Folder>(oldFile.Parent);
                pain.Items.Remove(oldFile.Id);
                pain.Save();
            }

            Folder dddd = LelfsManager.LoadById<Folder>(Parent);
            dddd.Items.Add(oldFile.Id);
            dddd.Save();

            oldFile.Parent = Parent;
            oldFile.Path = suffering;
            oldFile.Save();
        } else {
            if (oldFile.Parent != null) {
                Folder pain = LelfsManager.LoadById<Folder>(oldFile.Parent);
                pain.Items.Remove(oldFile.Id);
                pain.Save();
            }

            oldFile.Parent = null;
            oldFile.Path = suffering;
            oldFile.Save();
        }

        Close();
        ThingThatINeedToRefresh.ToMove = null;
        ThingThatINeedToRefresh.Selected = null;
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
