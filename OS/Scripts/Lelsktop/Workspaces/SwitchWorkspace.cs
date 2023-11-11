using Godot;
using System;

public partial class SwitchWorkspace : Button {
    [Export(PropertyHint.Range, "1,4")]
    int Workspace = 1;
    
    /*public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // couldn't be bothered to do this properly
        switch (Workspace) {
            case 1:
                GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = true;
                GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/1/Windows");
                break;
            case 2:
                GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = true;
                GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/2/Windows");
                break;
            case 3:
                GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = true;
                GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/3/Windows");
                break;
            case 4:
                GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = true;
                WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/4/Windows");
                break;
        }
    }*/
}
