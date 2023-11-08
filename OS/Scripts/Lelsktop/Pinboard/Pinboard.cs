using Godot;
using System;

public class Pinboard : Button {
    public static bool EditingPinboard = false;

    public void Toggled(bool yes) {
        GetNode<Viewport>("/root/Lelsktop/1/Windows").GuiDisableInput = yes;
        GetNode<Viewport>("/root/Lelsktop/2/Windows").GuiDisableInput = yes;
        GetNode<Viewport>("/root/Lelsktop/3/Windows").GuiDisableInput = yes;
        GetNode<Viewport>("/root/Lelsktop/4/Windows").GuiDisableInput = yes;
        GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = !yes;
        GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = !yes;
        GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = !yes;
        GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = !yes;
        EditingPinboard = yes;
    }
}
