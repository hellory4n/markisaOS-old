using Godot;
using System;

namespace Lelsktop.Interface;

public partial class OpenQuickSettings : Button
{
    [Export]
    AnimationPlayer Animation;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (!toggledOn)
            Animation.Play("CloseQuickSettings");
        else
            Animation.Play("OpenQuickSettings");
    }
}
