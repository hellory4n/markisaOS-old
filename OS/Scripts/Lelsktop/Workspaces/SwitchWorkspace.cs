using Godot;
using System;

public class SwitchWorkspace : Button {
    [Export(PropertyHint.Range, "1,4")]
    int Workspace = 1;
    
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // couldn't be bothered to do this properly
        switch (Workspace) {
            case 1:
                GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = true;
                GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/1/Windows");
                break;
            case 2:
                GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = true;
                GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/2/Windows");
                break;
            case 3:
                GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = true;
                GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/3/Windows");
                break;
            case 4:
                GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = true;
                WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/4/Windows");
                break;
        }
    }
}
