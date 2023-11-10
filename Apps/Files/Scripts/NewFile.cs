using Godot;
using System;

public partial class NewFile : Lelwindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string gkfngof = LelfsManager.LoadById<LelfsFile>(Parent).Path3D;
        string suffering;
        if (gkfngof == "/")
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
        LelfsFile newFile = LelfsManager.NewFile(filename, Parent);

        // TODO: make an actual time system thing
        newFile.Metadata.Add("CreationDate", DateTime.Now);
        newFile.Metadata.Add("Author", SavingManager.CurrentUser);
        newFile.Save();

        Close();
        ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
