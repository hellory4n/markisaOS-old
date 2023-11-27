using Godot;
using Kickstart.Records;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kickstart.Onboarding;

public partial class CreateUser : Button
{
    LineEdit name;
    LineEdit username;
    ItemList icons;
    Label errorThingy;
    

    public override void _Ready()
    {
        base._Ready();
        name = GetNode<LineEdit>("../../Name");
        username = GetNode<LineEdit>("../../Username");
        icons = GetNode<ItemList>("../../Icons");
        errorThingy = GetNode<Label>("/root/NewUser/Control/ErrorThingy");
    }

    public override void _Pressed() {
        base._Pressed();
        // make the icon be a string and not a number
        string icon = "";
        if (icons.GetSelectedItems().Length > 0)
        {
            switch (icons.GetSelectedItems()[0])
            {
                case 0: icon = "res://Assets/UserIcons/Cat.png"; break;
                case 1: icon = "res://Assets/UserIcons/Flower.png"; break;
                case 2: icon = "res://Assets/UserIcons/Balloons.png"; break;
                case 3: icon = "res://Assets/UserIcons/Car.png"; break;
                case 4: icon = "res://Assets/UserIcons/Dog.png"; break;
                case 5: icon = "res://Assets/UserIcons/Duck.png"; break;
                case 6: icon = "res://Assets/UserIcons/Pancakes.png"; break;
                case 7: icon = "res://Assets/UserIcons/Brushes.png"; break;
                case 8: icon = "res://Assets/UserIcons/Shuttle.png"; break;
                case 9: icon = "res://Assets/UserIcons/Football.png"; break;
            }
        }

        // make sure the name and username things actually have something
        if (name.Text == "")
        {
            errorThingy.Text = "Invalid name!";
            return;
        }

        if (username.Text == "")
        {
            errorThingy.Text = "Invalid username!";
            return;
        }

        // these characters are not allowed in windows, which isn't good since each user is a folder and stuff
        Regex what = new("[\"/<>:\\|?*]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (what.Matches(name.Text).Count > 0)
        {
            errorThingy.Text = "Names can't include the characters \\/<>:|?*";
            return;
        }

        Regex idkman = new("[^[a-z0-9._]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (idkman.Matches(username.Text).Count > 0)
        {
            errorThingy.Text = "Lelnet usernames only allow lowercase characters, numbers, underscores (_) and periods (.)";
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

            if (users.Contains(name.Text))
            {
                errorThingy.Text = $"User \"{name.Text}\" already exists!";
                return;
            }
        }

        // now we actually make the user and login
        MarkisaUser user = new() {
            DisplayName = name.Text,
            Username = username.Text,
            Photo = icon
        };
        RecordManager.CurrentUser = username.Text;
        RecordManager.Save(user);

        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/Dashboard.tscn");
        Node dashboard = packedScene.Instantiate();
        GetTree().Root.AddChild(dashboard);
        GetNode<Node2D>("/root/Onboarding").QueueFree();
        GetNode<Node2D>("/root/NewUser").QueueFree();
    }
}
