using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShowDesktop : Button {
    List<AnimationPlayer> WindowAnimators = new List<AnimationPlayer>();

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        if (Pressed) {
            WindowAnimators = new List<AnimationPlayer>();

            // find every window ever
            foreach (Node window in WindowManager.CurrentWorkspace.GetNode("ThemeThing").GetChildren()) {
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

    public override void _Process(float delta) {
        base._Process(delta);
        // yes.
        if (GetFocusOwner() != this) {
            Pressed = false;
        }
    }
}
