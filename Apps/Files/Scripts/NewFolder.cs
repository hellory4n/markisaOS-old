using Godot;
using System;
using Dashboard.Wm;
using Kickstart.Cabinetfs;
using Kickstart.Records;

public partial class NewFolder : MksWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Create").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        string gkfngof = CabinetfsManager.LoadFile(Parent).Path;
        string suffering;
        if (gkfngof == "/")
            suffering = $"/{filename}";
        else
            suffering = $"{gkfngof}/{filename}";

        // making a folder that already exists would be pretty uncool
        if (CabinetfsManager.PathExists(suffering)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "Folder already exists!";
            return;
        }

        // actually make the folder :)
        Folder newFolder = CabinetfsManager.NewFolder(filename, Parent);

        // TODO: make an actual time system thing
        newFolder.Metadata.Add("CreationDate", DateTime.Now);
        newFolder.Metadata.Add("Author", RecordManager.CurrentUser);
        newFolder.Save();

        EmitSignal(SignalName.CloseRequested);
        //ThingThatINeedToRefresh.Refresh(gkfngof, false);
    }
}
