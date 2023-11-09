using Godot;
using System;

public partial class StickyNoteText : TextEdit {
    Timer StupidTimer;

    public override void _Ready() {
        base._Ready();
        StupidTimer = GetNode<Timer>("Autosave");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        StupidTimer.Paused = GetFocusOwner() != this;
    }

    public void ActuallySaveAndStuff() {
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        pinboard.Items[
            GetParent<StickyNote>().PinboardItem
        ].Text = Text;
        SavingManager.Save(SavingManager.CurrentUser, pinboard);
    }
}
