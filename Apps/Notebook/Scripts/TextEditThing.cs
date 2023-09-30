using Godot;
using System;

public class TextEditThing : TextEdit {
    public string EpicFilename;
    public string SavedText;
    public string CoolId;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("../Toolbar/Save").Connect("pressed", this, nameof(Save));
        GetNode<Button>("../Toolbar/SaveAs").Connect("pressed", this, nameof(SaveAs));
        GetNode<Button>("../Toolbar/Open").Connect("pressed", this, nameof(Open));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (LelfsManager.IdExists(CoolId)) {
            GetNode<Label>("../TabTitle").Text = EpicFilename;
        } else {
            GetNode<Label>("../TabTitle").Text = "Untitled";
        }

        if (Text != SavedText) {
            GetNode<Label>("../TabTitle").Text += "*";
        }

        // help
        if (Input.IsActionJustReleased("save") && GetFocusOwner() == this) {
            Save();
        }
        if (Input.IsActionJustReleased("save_as") && GetFocusOwner() == this) {
            SaveAs();
        }
        if (Input.IsActionJustReleased("open") && GetFocusOwner() == this) {
            Open();
        }
    }

    public void Save() {
        if (!LelfsManager.IdExists(CoolId)) {
            SaveAs();
            return;
        }

        var m = LelfsManager.LoadById<LelfsFile>(CoolId);
        m.Data["Text"] = Text;
        m.Save();
        SavedText = Text;
    }

    public void SaveAs() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Notebook/SaveAs.tscn");
        NotebookSaveAs jjkn = m.Instance<NotebookSaveAs>();
        jjkn.Tfhsjkgjrrh = this;
        wm.AddWindow(jjkn);
    }

    public void Open() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Notebook/Open.tscn");
        var jjkn = m.Instance<NotebookOpen>();
        jjkn.Tfhsjkgjrrh = this;
        wm.AddWindow(jjkn);
    }
}