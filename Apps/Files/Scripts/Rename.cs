using Godot;
using System;
using Dashboard.Wm;
using Kickstart.Cabinetfs;

public partial class Rename : DashboardWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;
    public string CoolFile;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Rename").Connect("pressed", new Callable(this, nameof(Click)));
        File bruh = CabinetfsManager.LoadFile(CoolFile);
        GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text = bruh.Name;
    }

    public void Click() {
        string filename = GetNode<LineEdit>("CenterContainer/VBoxContainer/Name").Text;
        File bruh = CabinetfsManager.LoadFile(CoolFile);

        string newPath;
        if (Parent == "root") {
            File yeah = CabinetfsManager.LoadFile(Parent);
            newPath = $"{yeah.Path}/{bruh.Name}";
        } else {
            newPath = $"/{bruh.Path}";
        }

        // try to move it
        if (CabinetfsManager.PathExists(newPath)) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label").Text = "File with that name already exists!";
            return;
        }

        if (bruh.Type == "Folder") {
            Folder m = CabinetfsManager.LoadFolder(CoolFile);
            // make sure we can put bajillions of files in the trash
            m.Rename(filename);
        } else {
            // make sure we can put bajillions of files in the trash
            bruh.Rename(filename);
        }

        EmitSignal(SignalName.CloseRequested);
        //ThingThatINeedToRefresh.Refresh(ThingThatINeedToRefresh.Path, false);
    }
}
