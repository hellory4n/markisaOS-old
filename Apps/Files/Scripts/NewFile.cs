using Godot;
using System;

public class NewFile : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;

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

        // actually make the file :)
        BaseLelfs newFile;
        if (Parent != "/")
            newFile = new BaseLelfs(filename, Parent);
        else
            newFile = new BaseLelfs(filename);

        // TODO: make an actual time system thing
        newFile.Metadata.Add("CreationDate", DateTime.Now);
        newFile.Metadata.Add("Author", SavingManager.CurrentUser);
        newFile.Save();

        Close();
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
