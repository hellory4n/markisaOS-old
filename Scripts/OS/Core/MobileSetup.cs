using Godot;
using System;

public class MobileSetup : Control {
    public int FailedAttempts = 0;

    public override void _Ready() {
        base._Ready();

        // show the mobile setup thing :)
        // when the saving manager creates the settings files it already checks if the device if a computer,
        // and automatically skips the mobile setup thing if so
        DisplaySettings m = SavingManager.LoadSettings<DisplaySettings>(SavingManager.Settings.DisplaySettings);
        if (m.AlreadySetup) {
            PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Bootscreen.tscn");
            Node aNode = aPackedScene.Instance();
            GetTree().Root.CallDeferred("add_child", aNode);
            GetParent().QueueFree();
        } else {
            RectSize = m.Resolution/m.ScalingFactor;
            GetNode<Button>("Button").Connect("pressed", this, nameof(Thing));
        }
    }

    // idk man
    public override void _GuiInput(InputEvent @event) {
        if (Input.IsActionJustReleased("click_thingy")) {
            FailedAttempts++;
        }
        base._GuiInput(@event);
    }

    public void Thing() {
        if (FailedAttempts > 0) {
            // first update the resolution
            DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>(
                SavingManager.Settings.DisplaySettings
            );
            display.ScalingFactor += 0.2f;
            SavingManager.SaveSettings(SavingManager.Settings.DisplaySettings, display);
            ResolutionManager m = GetNode<ResolutionManager>("/root/ResolutionManager");
            m.Update();

            // then update the ui
            RectSize = display.Resolution / display.ScalingFactor;
            GetNode<Label>("Title").Text = "Let's try again";
            GetNode<Label>("Text").Text = "We have updated the UI, try again to see if that works for you";
            
            // recalculate the scale for the background
            float scale;
            if (display.Resolution/display.ScalingFactor > new Vector2(1280, 720)) {
                scale = (Mathf.Max((display.Resolution/display.ScalingFactor).x,
                    (display.Resolution/display.ScalingFactor).y) - 1280) / 1280;
                scale += 1;
            } else {
                scale = Mathf.Max((display.Resolution/display.ScalingFactor).x,
                    (display.Resolution/display.ScalingFactor).y) / 1280;
            }
            GetParent().GetNode<Sprite>("Background").Scale = new Vector2(scale, scale);
            // idk why just diving the resolution by 2 doesn't work
            GetParent().GetNode<Sprite>("Background").Position = new Vector2(
                display.Resolution.x/display.ScalingFactor, display.Resolution.y/display.ScalingFactor
            ) / 2;

            FailedAttempts = 0;
        } else {
            // this seems right, show the bootscreen
            DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>(
                SavingManager.Settings.DisplaySettings
            );
            display.AlreadySetup = true;
            SavingManager.SaveSettings(SavingManager.Settings.DisplaySettings, display);

            PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/Bootscreen.tscn");
            Node jjkn = m.Instance();
            GetTree().Root.AddChild(jjkn);
            GetParent().QueueFree();
        }
    }
}
