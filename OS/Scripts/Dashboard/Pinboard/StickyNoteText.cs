using Godot;
using Kickstart.Records;
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
        var mj = RecordManager.Load<DashboardConfig>();
        mj.Pinboard[
            GetParent<StickyNote>().PinboardItem
        ].Text = Text;
        RecordManager.Save(mj);
    }
}
