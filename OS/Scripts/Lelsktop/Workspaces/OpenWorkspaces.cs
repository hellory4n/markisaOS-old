using Godot;
using System;

public partial class OpenWorkspaces : Button
{
    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (toggledOn)
            animation.Play("CloseWorkspaces");
        else
            animation.Play("OpenWorkspaces");
    }
}
