using Godot;
using System;

public class InstallelAccentColor : OptionButton {
    public override void _Ready() {
        base._Ready();
        Connect("item_selected", this, nameof(Click));
    }

    public void Click(int index) {
        // this is fine.
        switch (index) {
            case 0:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Black/Theme.tres");
                break;
            case 1:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres");
                break;
            case 2:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Green/Theme.tres");
                break;
            case 3:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Orange/Theme.tres");
                break;
            case 4:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Pink/Theme.tres");
                break;
            case 5:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Purple/Theme.tres");
                break;
            case 6:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Red/Theme.tres");
                break;
            case 7:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-White/Theme.tres");
                break;
            case 8:
                GetNode<BaseWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Yellow/Theme.tres");
                break;
        }
    }
}
