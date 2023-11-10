using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lelsktop.Interface;

public partial class ShowDesktop : Button
{
    /*List<AnimationPlayer> WindowAnimators = new();

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (toggledOn) {
            WindowAnimators = new List<AnimationPlayer>();

            // find every window ever
            foreach (Node window in WindowManager.WindowManager.CurrentWorkspace.GetNode("ThemeThing").GetChildren()) {
                WindowAnimators.Add(window.GetNode<AnimationPlayer>("AnimationPlayer"));
                WindowAnimators.Last().Play("Minimize");
            }
        } else {
            foreach (var animator in WindowAnimators) {
                if (IsInstanceValid(animator))
                    animator.Play("Restore");
            }
            WindowAnimators = new List<AnimationPlayer>();
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // yes.
        if (GetFocusOwner() != this) {
            Pressed = false;
        }
    }*/
}
