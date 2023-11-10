using Godot;
using System;

public partial class NotebookSaveAs : Lelwindow {
    public TextEditThing Tfhsjkgjrrh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("M/H/Save").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string path = GetNode<LineEdit>("M/H/Path3D").Text;
        string name = GetNode<LineEdit>("M/H/Name").Text;
        NotificationManager notificationManager = GetNode<NotificationManager>("/root/NotificationManager");

        // error handling haha
        if (!LelfsManager.FileExists(path)) {
            notificationManager.ShowErrorNotification($"Folder {path} not found!");
            return;
        }

        LelfsFile h = LelfsManager.Load<LelfsFile>(path);
        if (h.Type != "Folder") {
            notificationManager.ShowErrorNotification($"{path} is not a folder!");
            return;
        }

        if (LelfsManager.FileExists(LelfsManager.NewPath(path, name))) {
            notificationManager.ShowErrorNotification($"File in {LelfsManager.NewPath(path, name)} already exists!");
            return;
        }

        // actually make the file
        LelfsFile epicFile = LelfsManager.NewFile(name, h.Id);
        epicFile.Type = "Text";
        epicFile.Data.Add("Text", Tfhsjkgjrrh.Text);
        epicFile.Metadata.Add("Author", SavingManager.CurrentUser);
        epicFile.Metadata.Add("CreationDate", DateTime.Now);
        epicFile.Save();

        Tfhsjkgjrrh.EpicFilename = epicFile.Name;
        Tfhsjkgjrrh.SavedText = Tfhsjkgjrrh.Text;
        Tfhsjkgjrrh.CoolId = epicFile.Id;
        Close();
    }
}
