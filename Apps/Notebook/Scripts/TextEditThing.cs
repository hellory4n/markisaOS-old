using Godot;
using System;
using Kickstart.Cabinetfs;

public partial class TextEditThing : TextEdit {
    public string EpicFilename;
    public string SavedText;
    public string CoolId;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("../Toolbar/Save").Connect("pressed", new Callable(this, nameof(Save)));
        GetNode<Button>("../Toolbar/SaveAs").Connect("pressed", new Callable(this, nameof(SaveAs)));
        GetNode<Button>("../Toolbar/Open").Connect("pressed", new Callable(this, nameof(Open)));
    }

    public override void _Process(double delta) {
        base._Process(delta);
        if (CabinetfsManager.IdExists(CoolId)) {
            GetNode<Label>("../TabTitle").Text = EpicFilename;
        } else {
            GetNode<Label>("../TabTitle").Text = "Untitled";
        }

        if (Text != SavedText) {
            GetNode<Label>("../TabTitle").Text += "*";
        }

        // help
        if (Input.IsActionJustReleased("save") && HasFocus()) {
            Save();
        }
        if (Input.IsActionJustReleased("save_as") && HasFocus()) {
            SaveAs();
        }
        if (Input.IsActionJustReleased("open") && HasFocus()) {
            Open();
        }
    }

    public void Save() {
        if (!CabinetfsManager.IdExists(CoolId)) {
            SaveAs();
            return;
        }

        var m = CabinetfsManager.LoadFile(CoolId);
        m.Data["Text"] = Text;
        m.Save();
        SavedText = Text;
    }

    public void SaveAs() {
        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Notebook/SaveAs.tscn");
        NotebookSaveAs jjkn = m.Instantiate<NotebookSaveAs>();
        jjkn.Tfhsjkgjrrh = this;
        wm.AddWindow(jjkn);*/
    }

    public void Open() {
        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Notebook/Open.tscn");
        var jjkn = m.Instantiate<NotebookOpen>();
        jjkn.Tfhsjkgjrrh = this;
        wm.AddWindow(jjkn);*/
    }
}
