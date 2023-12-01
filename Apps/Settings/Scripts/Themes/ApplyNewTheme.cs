using Godot;
using Kickstart.Records;
using System;

public partial class ApplyNewTheme : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // a questionable way of getting the buttongroup :)))))))
        ButtonGroup pain = GetNode<CheckBox>("../Grid/CheckBox").ButtonGroup;

        // load the theme :))))))
        CheckBox option = (CheckBox)pain.GetPressedButton();
        Theme theme = option.Text switch
        {
            "Black" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Black/Theme.tres"),
            "Blue" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Blue/Theme.tres"),
            "Green" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Green/Theme.tres"),
            "Orange" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Orange/Theme.tres"),
            "Pink" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Pink/Theme.tres"),
            "Purple" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Purple/Theme.tres"),
            "Red" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Red/Theme.tres"),
            "White" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-White/Theme.tres"),
            "Yellow" => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Yellow/Theme.tres"),
            _ => GD.Load<Theme>("res://Assets/Themes/HighPeaks-Blue/Theme.tres"),
        };

        // apply the theme :)))))))))))))))))))))))))))))
        GetNode<Control>("/root/Dashboard/1/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Dashboard/2/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Dashboard/3/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Dashboard/4/Windows/ThemeThing").Theme = theme;
        CanvasLayer dashboardInterface = GetNode<CanvasLayer>("/root/DashboardInterface");
        dashboardInterface.GetNode<Panel>("Dock").Theme = theme;
        dashboardInterface.GetNode<Panel>("QuickSettings").Theme = theme;
        dashboardInterface.GetNode<Panel>("AppMenu").Theme = theme;
        dashboardInterface.GetNode<Panel>("Panel").Theme = theme;

        // save the settings :)))))))))))))))))))))))))))))))))))))))))))))))))
        var asdadjffjsfjaf = new Record<DashboardConfig>();
        asdadjffjsfjaf.Data.Theme = theme.ResourcePath;
        asdadjffjsfjaf.Save();
    }
}
