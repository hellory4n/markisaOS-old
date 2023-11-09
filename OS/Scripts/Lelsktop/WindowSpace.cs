using Godot;
using System;

public partial class WindowSpace : Control {
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton) {
            Panel appMenu = GetParent().GetNode<Panel>("AppMenu");
            Panel quickSettings = GetParent().GetNode<Panel>("QuickSettings");
            Panel workspaces = GetParent().GetNode<Panel>("Workspaces");
            AnimationPlayer animation = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");

            if (appMenu.Position.y > 0) {
                animation.Play("CloseAppMenu");
                // the button is on toggle mode so this makes it less weird 
                GetNode<Button>("/root/LelsktopInterface/Panel/Apps").Pressed = false;
            }
            
            if (quickSettings.Position.y > 0) {
                animation.Play("CloseQuickSettings");
                // the button is on toggle mode so this makes it less weird 
                GetNode<Button>("/root/LelsktopInterface/Panel/Settings").Pressed = false;
            }

            if (workspaces.Modulate != new Color(1, 1, 1, 0)) {
                animation.Play("CloseWorkspaces");
                // the button is on toggle mode so this makes it less weird 
                GetNode<Button>("/root/LelsktopInterface/Dock/DockStuff/QuickLaunch/Workspaces").Pressed = false;
            }
        }
        base._GuiInput(@event);
    }
}
