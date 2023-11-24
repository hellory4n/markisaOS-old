using Godot;
using System;

namespace Dashboard.Pinboard;

public partial class StickyNoteText : TextEdit
{
    Timer StupidTimer;

    public override void _Ready()
    {
        base._Ready();
        StupidTimer = GetNode<Timer>("Autosave");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        StupidTimer.Paused = !HasFocus();
    }

    public void ActuallySaveAndStuff()
    {
        var pinboard = SavingManager.Load<DashboardPinboard>(SavingManager.CurrentUser);
        pinboard.Items[
            GetParent<StickyNote>().PinboardItem
        ].Text = Text;
        SavingManager.Save(SavingManager.CurrentUser, pinboard);
    }
}
