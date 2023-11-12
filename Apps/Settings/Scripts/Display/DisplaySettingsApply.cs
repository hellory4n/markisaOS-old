using Godot;
using System;
using Lelsktop.WindowManager;

public partial class DisplaySettingsApply : Button {
    SpinBox resolutionWidth;
    SpinBox resolutionHeight;
    SpinBox scalingFactor;

    public override void _Ready() {
        base._Ready();

        resolutionWidth = GetNode<SpinBox>("../ResolutionThing/Width");
        resolutionHeight = GetNode<SpinBox>("../ResolutionThing/Height");
        scalingFactor = GetNode<SpinBox>("../ScalingFactor");

        // set the value of the spin boxes to the current settings
        DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>();
        resolutionWidth.Value = display.Resolution.X;
        resolutionHeight.Value = display.Resolution.Y;
        scalingFactor.Value = display.ScalingFactor*100;

        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        Vector2 stupidity = new((float)resolutionWidth.Value, (float)resolutionHeight.Value);
        stupidity /= (float)(scalingFactor.Value / 100);

        // this doesn't actually save the settings
        //GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, stupidity);

        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Settings/DisplayConfirm.tscn");
        Lelwindow jjkn = (Lelwindow)m.Instantiate();    
        wm.AddWindow(jjkn);*/

        // yes
        /*jjkn.GetNode<DisplaySettingsActuallyApply>("Confirm").
            Resolution = new Vector2((float)resolutionWidth.Value, (float)resolutionHeight.Value);
        jjkn.GetNode<DisplaySettingsActuallyApply>("Confirm").ScalingFactor = (float)(scalingFactor.Value/100);

        // the window manager is gonna put it on the center but using the old settings
        jjkn.Position = new Vector2I(10, 90);*/
    }
}
