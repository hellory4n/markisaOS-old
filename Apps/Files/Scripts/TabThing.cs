using Godot;
using System;

public partial class TabThing : Button {
    public Control TabContent;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        GetParent<FileTabs>().UpdateStuff(TabContent, this);
    }
}
