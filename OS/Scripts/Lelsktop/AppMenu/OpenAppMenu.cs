using Godot;
using System;

namespace Lelsktop.Interface;

public partial class OpenAppMenu : Button
{
    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (!toggledOn)
            animation.Play("CloseAppMenu");
        else
            animation.Play("OpenAppMenu");
    }
}
