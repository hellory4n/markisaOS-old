using Godot;
using System;
using Lelsktop.WindowManager;

public partial class NotebookOpen : Lelwindow {
    public TextEditThing Tfhsjkgjrrh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("M/H/Open").Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        string id = GetNode<LineEdit>("M/H/EyeDee").Text;
        NotificationManager notificationManager = GetNode<NotificationManager>("/root/NotificationManager");

        // error handling haha
        if (!LelfsManager.IdExists(id)) {
            notificationManager.ShowErrorNotification("File not found!");
            return;
        }

        // actually open the file
        var epicFile = LelfsManager.LoadById<LelfsFile>(id);
        if (epicFile.Data.ContainsKey("Text")) {
            Tfhsjkgjrrh.Text = epicFile.Data["Text"].ToString();
        } else {
            Tfhsjkgjrrh.Text = "";
        }
        Tfhsjkgjrrh.EpicFilename = epicFile.Name;
        Tfhsjkgjrrh.SavedText = Tfhsjkgjrrh.Text;
        Tfhsjkgjrrh.CoolId = epicFile.Id;
        Close();
    }
}
