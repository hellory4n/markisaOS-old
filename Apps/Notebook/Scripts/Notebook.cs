using Godot;
using System;

public partial class Notebook : Lelwindow {
    public bool Suffer = false;
    public bool J = true;
    public string Fhgkfdlkjgjkhgjf = "";

    // there was a race condition thing, it was trying to change the tab before the tab is open
    public override void _Process(double delta) {
        base._Process(delta);
        if (Suffer && J) {
            var pain = LelfsManager.LoadById<LelfsFile>(Fhgkfdlkjgjkhgjf);
            GetNode<Label>("./TabContent/TabTitle").Text = pain.Name;
            var g = GetNode<TextEditThing>("TabContent/TextEdit");
            if (pain.Data.ContainsKey("Text")) {
                g.Text = pain.Data["Text"].ToString();
                g.SavedText = pain.Data["Text"].ToString();
            } else {
                g.Text = "";
                g.SavedText = "";
            }
            g.EpicFilename = pain.Name;
            g.CoolId = pain.Id;
            J = false;
        }
    }
}
