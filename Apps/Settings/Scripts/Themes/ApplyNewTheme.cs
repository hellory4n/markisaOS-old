using Godot;
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
            "Black" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Black/Theme.tres"),
            "Blue" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres"),
            "Green" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Green/Theme.tres"),
            "Orange" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Orange/Theme.tres"),
            "Pink" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Pink/Theme.tres"),
            "Purple" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Purple/Theme.tres"),
            "Red" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Red/Theme.tres"),
            "White" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-White/Theme.tres"),
            "Yellow" => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Yellow/Theme.tres"),
            _ => GD.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres"),
        };

        // apply the theme :)))))))))))))))))))))))))))))
        GetNode<Control>("/root/Lelsktop/1/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Lelsktop/2/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Lelsktop/3/Windows/ThemeThing").Theme = theme;
        GetNode<Control>("/root/Lelsktop/4/Windows/ThemeThing").Theme = theme;
        CanvasLayer lelsktopInterface = GetNode<CanvasLayer>("/root/LelsktopInterface");
        lelsktopInterface.GetNode<Panel>("Dock").Theme = theme;
        lelsktopInterface.GetNode<Panel>("QuickSettings").Theme = theme;
        lelsktopInterface.GetNode<Panel>("AppMenu").Theme = theme;
        lelsktopInterface.GetNode<Panel>("Panel").Theme = theme;

        // save the settings :)))))))))))))))))))))))))))))))))))))))))))))))))
        UserLelsktop asdadjffjsfjaf = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);
        asdadjffjsfjaf.Theme = $"Leltheme-Dark-{option.Text}";
        SavingManager.Save(SavingManager.CurrentUser, asdadjffjsfjaf);
    }
}
