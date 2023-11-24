using Godot;
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

        // now we actually make the user and login
        UserInfo info = new()
        {
            Photo = icon,
            LelnetUsername = username.Text
        };
        SavingManager.NewUser(name.Text, info);

        SavingManager.CurrentUser = name.Text;
        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/Dashboard.tscn");
        Node dashboard = packedScene.Instantiate();
        GetTree().Root.AddChild(dashboard);
        GetNode<Node2D>("/root/Onboarding").QueueFree();
        GetNode<Node2D>("/root/NewUser").QueueFree();
    }
}
