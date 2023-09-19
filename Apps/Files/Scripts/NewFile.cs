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
        string gkfngof = LelfsManager.LoadById<LelfsFile>(Parent).Path;
        string suffering;
        if (Parent == "root")
            suffering = $"/{filename}";
        else
            suffering = $"{gkfngof}/{filename}";

        // making a file that already exists would be pretty uncool
        if (LelfsManager.FileExists(suffering)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "File already exists!";
            SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
            soundManager.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // actually make the file :)
        LelfsFile newFile;
        newFile = new LelfsFile(filename, Parent);

        // TODO: make an actual time system thing
        newFile.Metadata.Add("CreationDate", DateTime.Now);
        newFile.Metadata.Add("Author", SavingManager.CurrentUser);
        newFile.Save();

        Close();
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
