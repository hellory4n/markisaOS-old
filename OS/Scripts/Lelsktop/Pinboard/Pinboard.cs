using Godot;
using System;

namespace Lelsktop.Pinboard;

public partial class Pinboard : Button
{
    public static bool EditingPinboard = false;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        /*GetNode<SubViewport>("/root/Lelsktop/1/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Lelsktop/2/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Lelsktop/3/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Lelsktop/4/Windows").GuiDisableInput = yes;
        GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = !yes;
        GetNode<Control>("/root/LelsktopInterface/WindowSpace").Visible = !yes;
        EditingPinboard = yes;*/
    }
}
