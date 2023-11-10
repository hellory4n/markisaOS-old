using Godot;
using System;

namespace Lelsktop.Interface;

public partial class OpenQuickSettings : Button
{
    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (toggledOn)
            animation.Play("CloseQuickSettings");
        else
            animation.Play("OpenQuickSettings");
    }
}
