using Godot;
using System;

public partial class ApplyNewTheme : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // a questionable way of getting the buttongroup :)))))))
        ButtonGroup pain = GetNode<CheckBox>("../Grid/CheckBox").Group;

        // load the theme :))))))
        CheckBox option = (CheckBox)pain.GetPressedButton();
        Theme theme;
        switch (option.Text) {
            case "Black":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Black/Theme.tres");
                break;
            case "Blue":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres");
                break;
            case "Green":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Green/Theme.tres");
                break;
            case "Orange":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Orange/Theme.tres");
                break;
            case "Pink":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Pink/Theme.tres");
                break;
            case "Purple":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Purple/Theme.tres");
                break;
            case "Red":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Red/Theme.tres");
                break;
            case "White":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-White/Theme.tres");
                break;
            case "Yellow":
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Yellow/Theme.tres");
                break;
            default:
                theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres");
                break;
        }

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
