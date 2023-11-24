using Godot;
using System;
using Dashboard.Wm;
using Dashboard.Overlay;

public partial class NotebookOpen : DashboardWindow {
    public TextEditThing Tfhsjkgjrrh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("M/H/Open").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string id = GetNode<LineEdit>("M/H/EyeDee").Text;
        NotificationManager notificationManager = GetNode<NotificationManager>("/root/NotificationManager");

        // error handling haha
        if (!CabinetfsManager.IdExists(id)) {
            notificationManager.ShowErrorNotification("File not found!", "Notebook");
            return;
        }

        // actually open the file
        var epicFile = CabinetfsManager.LoadById<CabinetfsFile>(id);
        if (epicFile.Data.ContainsKey("Text")) {
            Tfhsjkgjrrh.Text = epicFile.Data["Text"].ToString();
        } else {
            Tfhsjkgjrrh.Text = "";
        }
        Tfhsjkgjrrh.EpicFilename = epicFile.Name;
        Tfhsjkgjrrh.SavedText = Tfhsjkgjrrh.Text;
        Tfhsjkgjrrh.CoolId = epicFile.Id;
        EmitSignal(SignalName.CloseRequested);
    }
}
