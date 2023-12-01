using Godot;
using Kickstart.Records;
using System;

namespace Dashboard.Pinboard;

public partial class StickyNoteText : TextEdit
{
    Timer StupidTimer;
    string Hi;

    public override void _Ready()
    {
        base._Ready();
        StupidTimer = GetNode<Timer>("Autosave");
        Hi = GetParent<StickyNote>().PinboardItem;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        StupidTimer.Paused = !HasFocus();
    }

    public void ActuallySaveAndStuff()
    {
        var mj = new Record<DashboardConfig>();
        var fuck = mj.Data.Pinboard[Hi];
        fuck.Text = Text;
        mj.Data.Pinboard[Hi] = fuck;
        mj.Save();
    }
}
