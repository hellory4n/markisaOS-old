using Godot;
using System;
using Lelsktop.WindowManager;

public partial class Rename : Lelwindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string CoolFile;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Rename").Connect("pressed", new Callable(this, nameof(Click)));
        LelfsFile bruh = LelfsManager.LoadById<LelfsFile>(CoolFile);
        GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text = bruh.Name;
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        LelfsFile bruh = LelfsManager.LoadById<LelfsFile>(CoolFile);

        string newPath;
        if (Parent == "root") {
            LelfsFile yeah = LelfsManager.LoadById<LelfsFile>(Parent);
            newPath = $"{yeah.Path}/{bruh.Name}";
        } else {
            newPath = $"/{bruh.Path}";
        }

        // try to move it
        if (LelfsManager.FileExists(newPath)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "File with that name already exists!";
            return;
        }

        if (bruh.Type == "Folder") {
            Folder m = LelfsManager.LoadById<Folder>(CoolFile);
            // make sure we can put bajillions of files in the trash
            m.Rename(filename);
        } else {
            // make sure we can put bajillions of files in the trash
            bruh.Rename(filename);
        }

        Close();
        ThingThatINeedToRefresh.Refresh(ThingThatINeedToRefresh.Path, false);
    }
}
