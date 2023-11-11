using Godot;
using System;
using System.Text.RegularExpressions;
using Lelsktop.Overlay;

namespace Lelcore.Installel;

public partial class InstallelFinish : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        // first we make this idiot proof
        // this code is mostly stolen from res://OS/Scripts/Core/CreateUser.cs
        string name = GetNode<LineEdit>("../../../Step2/M/Name").Text;
        string lelnetUsername = GetNode<LineEdit>("../../../Step2/M/Username").Text;

        if (name == "")
        {
            Shit("Invalid name!", true);
            return;
        }

        if (lelnetUsername == "")
        {
            Shit("Invalid username!", true);
            return;
        }

        Regex what = new("[\"/<>:\\|?*]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (what.Matches(name).Count > 0)
        {
            Shit("Names can't include the characters \\/<>:|?*", true);
            return;
        }

        Regex idkman = new("[^[a-z0-9._]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (idkman.Matches(lelnetUsername).Count > 0)
        {
            Shit("Lelnet usernames only allow lowercase characters, numbers, underscores (_) and periods (.)", true);
            return;
        }

        // too lazy to make icons be an enum lol
        string icon = "";
        switch (GetNode<OptionButton>("../../../Step2/M/Photo").Selected)
        {
            case 0: icon = "Cat"; break;
            case 1: icon = "Flower"; break;
            case 2: icon = "Balloon"; break;
            case 3: icon = "Car"; break;
            case 4: icon = "Dog"; break;
            case 5: icon = "Duck"; break;
            case 6: icon = "Pancakes"; break;
            case 7: icon = "Brushes"; break;
            case 8: icon = "Shuttle"; break;
            case 9: icon = "Football"; break;
        }

        // actually make the user :)
        UserInfo info = new()
        {
            Photo = icon,
            LelnetUsername = lelnetUsername
        };
        SavingManager.NewUser(name, info);

        // set the accent color lol
        string theme = "Leltheme-Dark-";
        switch (GetNode<OptionButton>("../../../Step3/M/AccentColor").Selected)
        {
            case 0: theme += "Black"; break;
            case 1: theme += "Blue"; break;
            case 2: theme += "Green"; break;
            case 3: theme += "Orange"; break;
            case 4: theme += "Pink"; break;
            case 5: theme += "Purple"; break;
            case 6: theme += "Red"; break;
            case 7: theme += "White"; break;
            case 8: theme += "Yellow"; break;
        }
        var shitfuckery = SavingManager.Load<UserLelsktop>(name);
        shitfuckery.Theme = theme;

        // set the wallpaper haha yes
        string wallpaper = "";
        switch (GetNode<OptionButton>("../../../Step3/M/Wallpaper").Selected)
        {
            case 0: wallpaper = "res://Assets/Wallpapers/HighPeaks.jpg"; break;
            case 1: wallpaper = "res://Assets/Wallpapers/Flowers.png"; break;
            case 2: wallpaper = "res://Assets/Wallpapers/Beaches.png"; break;
            case 3: wallpaper = "res://Assets/Wallpapers/Space.png"; break;
            case 4: wallpaper = "res://Assets/Wallpapers/Mountains.png"; break;
            case 5: wallpaper = "res://Assets/Wallpapers/Aurora.png"; break;
        }
        shitfuckery.Wallpaper = wallpaper;
        SavingManager.Save(name, shitfuckery);

        // yes
        SavingManager.SaveSettings(new InstallerInfo {
            IsInstalled = true
        });

        // finally login :)))))))))))))))))))))))))))))))))
        SavingManager.CurrentUser = name;
        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Lelsktop.tscn");
        Node lelsktop = packedScene.Instantiate();
        GetTree().Root.AddChild(lelsktop);
        GetNode<Node2D>("/root/InstallelOobe").QueueFree();
    }

    public void Shit(string error, bool goToStep2)
    {
        var notificationManager = GetNode<NotificationManager>("/root/NotificationManager");
        notificationManager.ShowErrorNotification(error);

        if (goToStep2)
        {
            GetNode<Control>("../../../Step2").Visible = true;
            GetNode<Control>("../../../Step3").Visible = false;
        }
    }
}
