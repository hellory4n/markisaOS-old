using Dashboard.Overlay;
using Godot;
using Kickstart.Records;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kickstart.Onboarding;

public partial class CreateUser : Button
{
    [Export]
    public LineEdit DisplayName;
    [Export]
    public LineEdit Username;
    [Export]
    public OptionButton Icosbnhjrsnjgt;
    [Export]
    public Window GetParentDotGetParentDotGetParentDotGetParentDotGetParent;


    public override void _Pressed() {
        base._Pressed();
        // make the icon be a string and not a number
        string icon = Icosbnhjrsnjgt.GetSelectedId() switch
        {
            0 => "res://Assets/UserIcons/Cat.png",
            1 => "res://Assets/UserIcons/Flower.png",
            2 => "res://Assets/UserIcons/Balloons.png",
            3 => "res://Assets/UserIcons/Car.png",
            4 => "res://Assets/UserIcons/Dog.png",
            5 => "res://Assets/UserIcons/Duck.png",
            6 => "res://Assets/UserIcons/Pancakes.png",
            7 => "res://Assets/UserIcons/Brushes.png",
            8 => "res://Assets/UserIcons/Shuttle.png",
            9 => "res://Assets/UserIcons/Football.png",
            _ => ""
        };

        NotificationManager ohFuckOff = GetNode<NotificationManager>("/root/NotificationManager");

        // make sure the name and username things actually have something
        if (DisplayName.Text == "")
        {
            ohFuckOff.ShowErrorNotification("Invalid name!", "markisaOS");
            return;
        }

        if (Username.Text == "")
        {
            ohFuckOff.ShowErrorNotification("Invalid username!", "markisaOS");
            return;
        }

        Regex idkman = new("[^[a-z0-9._]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (idkman.Matches(Username.Text).Count > 0)
        {
            ohFuckOff.ShowErrorNotification("Lelnet usernames only allow lowercase characters, numbers, underscores (_) and periods (.)", "markisaOS");
            return;
        }

        // make sure the user doesn't already exist :)
        if (DirAccess.DirExistsAbsolute("user://Users"))
        {
            List<string> users = new();
            DirAccess dir = DirAccess.Open("user://Users/");
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "")
            {
                users.Add(filename);
                filename = dir.GetNext();
            }

            if (users.Contains(Username.Text))
            {
                ohFuckOff.ShowErrorNotification("Username already claimed!", "markisaOS");
                return;
            }
        }

        // now we actually make the user and login
        MarkisaUser user = new() {
            DisplayName = DisplayName.Text,
            Username = Username.Text,
            Photo = icon
        };
        RecordManager.CurrentUser = Username.Text;
        RecordManager.CurrentUserDisplayName = DisplayName.Text;
        RecordManager.Save(user);

        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/Dashboard.tscn");
        Node dashboard = packedScene.Instantiate();
        GetTree().Root.AddChild(dashboard);
        GetNode<Node2D>("/root/Onboarding").QueueFree();
        GetNode<Node2D>("/root/NewUser").QueueFree();
        GetParentDotGetParentDotGetParentDotGetParentDotGetParent.QueueFree();
    }
}
