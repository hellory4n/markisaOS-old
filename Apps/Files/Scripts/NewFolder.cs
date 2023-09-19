using Godot;
using System;

public class NewFolder : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string gkfngof = LelfsManager.LoadById<LelfsFile>(Parent).Path;
        string suffering;
        if (gkfngof == "/")
            suffering = $"/{filename}";
        else
            suffering = $"{gkfngof}/{filename}";

        // making a folder that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "Folder already exists!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually make the folder :)
        Folder newFolder = LelfsManager.NewFolder(filename, Parent);

        // TODO: make an actual time system thing
        newFolder.Metadata.Add("CreationDate", DateTime.Now);
        newFolder.Metadata.Add("Author", SavingManager.CurrentUser);
        newFolder.Save();

        Close();
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
