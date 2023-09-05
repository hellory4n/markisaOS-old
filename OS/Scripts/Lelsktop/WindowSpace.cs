using Godot;
using System;

public class WindowSpace : Control {
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            Panel appMenu = GetParent().GetNode<Panel>("AppMenu");
            Panel quickSettings = GetParent().GetNode<Panel>("QuickSettings");
            AnimationPlayer animation = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");

            if (appMenu.RectPosition.y > 0) {
                animation.Play("CloseAppMenu");
                // the button is on toggle mode so this makes it less weird 
                GetNode<Button>("/root/LelsktopInterface/Panel/Apps").Pressed = false;
            }
            
            if (quickSettings.RectPosition.y > 0) {
                animation.Play("CloseQuickSettings");
                // the button is on toggle mode so this makes it less weird 
                GetNode<Button>("/root/LelsktopInterface/Panel/Settings").Pressed = false;
            }
        }
        base._GuiInput(@event);
    }
}
