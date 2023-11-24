using Godot;
using System;

namespace Dashboard.Interface;

public partial class OpenAppMenu : Button
{
    [Export]
    AnimationPlayer Animation;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (!toggledOn)
            Animation.Play("CloseAppMenu");
        else
            Animation.Play("OpenAppMenu");
    }
}
