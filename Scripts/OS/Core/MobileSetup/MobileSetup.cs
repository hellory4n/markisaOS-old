using Godot;
using System;

public class MobileSetup : Node2D {
    public override void _Ready() {
        base._Ready();
        // checks if this is the first try
        // the mobile setup button changes the name of the new version of this node, so if the original name
        // is still being used then it's the first try
        if (Name != "MobileSetup") {
            GetNode<Label>("Control/Title").Text = "Let's try again";
            GetNode<Label>("Control/Text").Text = "We have increased the size of buttons, try tapping on the button again.";
        }
    }
}
