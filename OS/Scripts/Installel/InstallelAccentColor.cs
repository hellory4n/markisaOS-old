using Godot;
using System;
using Lelsktop.WindowManager;

namespace Lelcore.Installel;

public partial class InstallelAccentColor : OptionButton
{
    public override void _Ready()
    {
        base._Ready();
        Connect("item_selected", new Callable(this, nameof(Click)));
    }

    public void Click(int index)
    {
        // this is fine.
        switch (index)
        {
            case 0:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Black/Theme.tres");
                break;
            case 1:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Blue/Theme.tres");
                break;
            case 2:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Green/Theme.tres");
                break;
            case 3:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Orange/Theme.tres");
                break;
            case 4:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Pink/Theme.tres");
                break;
            case 5:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Purple/Theme.tres");
                break;
            case 6:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Red/Theme.tres");
                break;
            case 7:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-White/Theme.tres");
                break;
            case 8:
                GetNode<Lelwindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = ResourceLoader.Load<Theme>("res://Assets/Themes/Leltheme-Dark-Yellow/Theme.tres");
                break;
        }
    }
}
