using Godot;
using System;
using Dashboard.Wm;
using Dashboard.Overlay;

public partial class NotebookSaveAs : DashboardWindow {
    public TextEditThing Tfhsjkgjrrh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("M/H/Save").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string path = GetNode<LineEdit>("M/H/Path").Text;
        string name = GetNode<LineEdit>("M/H/Name").Text;
        NotificationManager notificationManager = GetNode<NotificationManager>("/root/NotificationManager");

        // error handling haha
        if (!CabinetfsManager.FileExists(path)) {
            notificationManager.ShowErrorNotification($"Folder {path} not found!", "Notebook");
            return;
        }

        CabinetfsFile h = CabinetfsManager.Load<CabinetfsFile>(path);
        if (h.Type != "Folder") {
            notificationManager.ShowErrorNotification($"{path} is not a folder!", "Notebook");
            return;
        }

        if (CabinetfsManager.FileExists(CabinetfsManager.NewPath(path, name))) {
            notificationManager.ShowErrorNotification($"File in {CabinetfsManager.NewPath(path, name)} already exists!", "Notebook");
            return;
        }

        // actually make the file
        CabinetfsFile epicFile = CabinetfsManager.NewFile(name, h.Id);
        epicFile.Type = "Text";
        epicFile.Data.Add("Text", Tfhsjkgjrrh.Text);
        epicFile.Metadata.Add("Author", SavingManager.CurrentUser);
        epicFile.Metadata.Add("CreationDate", DateTime.Now);
        epicFile.Save();

        Tfhsjkgjrrh.EpicFilename = epicFile.Name;
        Tfhsjkgjrrh.SavedText = Tfhsjkgjrrh.Text;
        Tfhsjkgjrrh.CoolId = epicFile.Id;
        EmitSignal(SignalName.CloseRequested);
    }
}
