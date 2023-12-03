using Godot;
using System;
using Dashboard.Wm;

namespace Kickstart.Installel;

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
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Black/Theme.tres");
                break;
            case 1:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Blue/Theme.tres");
                break;
            case 2:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Green/Theme.tres");
                break;
            case 3:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Orange/Theme.tres");
                break;
            case 4:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Pink/Theme.tres");
                break;
            case 5:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Purple/Theme.tres");
                break;
            case 6:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Red/Theme.tres");
                break;
            case 7:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-White/Theme.tres");
                break;
            case 8:
                GetNode<MksWindow>("/root/InstallelOobe/1/Windows/ThemeThing/Installel").Theme = GD.Load<Theme>("res://Assets/Themes/HighPeaks-Yellow/Theme.tres");
                break;
        }
    }
}
