using Godot;
using System;

public class WindowSpace : Control {
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            if (bruh.Pressed) {
                Panel appMenu = GetParent().GetNode<Panel>("AppMenu");
                Panel quickSettings = GetParent().GetNode<Panel>("QuickSettings");
                AnimationPlayer animation = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");
                bool doTheThingToAllWindows = false;

                if (appMenu.RectPosition.y > 0) {
                    animation.Play("CloseAppMenu");
                    // the button is on toggle mode so this makes it less weird 
                    GetNode<Button>("/root/LelsktopInterface/Panel/Apps").Pressed = false;
                    doTheThingToAllWindows = true;
                }
                
                if (quickSettings.RectPosition.y > 0) {
                    animation.Play("CloseQuickSettings");
                    // the button is on toggle mode so this makes it less weird 
                    GetNode<Button>("/root/LelsktopInterface/Panel/Settings").Pressed = false;
                    doTheThingToAllWindows = true;
                }

                if (doTheThingToAllWindows)
                    WindowManager.EnableWindowInput();
            }
        }
        base._GuiInput(@event);
    }
}
