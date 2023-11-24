using Godot;
using System;

namespace Dashboard.Pinboard;

public partial class Pinboard : Button
{
    public static bool EditingPinboard = false;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        /*GetNode<SubViewport>("/root/Dashboard/1/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Dashboard/2/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Dashboard/3/Windows").GuiDisableInput = yes;
        GetNode<SubViewport>("/root/Dashboard/4/Windows").GuiDisableInput = yes;
        GetNode<SubViewportContainer>("/root/Dashboard/1").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Dashboard/2").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Dashboard/3").Visible = !yes;
        GetNode<SubViewportContainer>("/root/Dashboard/4").Visible = !yes;
        GetNode<Control>("/root/DashboardInterface/WindowSpace").Visible = !yes;
        EditingPinboard = yes;*/
    }
}
