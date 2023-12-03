using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class ApplyNewTheme : Button 
{
    [Export]
    ListThemes StupidThingy;

    public override void _Pressed()
    {
        base._Pressed();

        // get the button group :)
        ButtonGroup lol = StupidThingy.GetChild<Button>(0).ButtonGroup;

        // get the theme :)))))))))))
        Button ohFuckOff = (Button)lol.GetPressedButton();
        Record<DashboardConfig> record = new();
        Theme theme = GD.Load<Theme>(record.Data.Themes[ohFuckOff.Text]);

        // apply the theme :)))))))))))))))))))))))))))))
        GetNode<Control>("/root/Dashboard/M/Windows/ThemeThing").Theme = theme;
        Node dashboardInterface = GetNode("/root/Dashboard/Inter/Face");
        dashboardInterface.GetNode<Panel>("Dock").Theme = theme;
        dashboardInterface.GetNode<Panel>("QuickSettings").Theme = theme;
        dashboardInterface.GetNode<Panel>("AppMenu").Theme = theme;
        dashboardInterface.GetNode<Panel>("Panel").Theme = theme;

        // save the settings :)))))))))))))))))))))))))))))))))))))))))))))))))
        var asdadjffjsfjaf = new Record<DashboardConfig>();
        asdadjffjsfjaf.Data.Theme = record.Data.Themes[ohFuckOff.Text];
        asdadjffjsfjaf.Save();
    }
}
