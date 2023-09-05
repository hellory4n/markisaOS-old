using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CreateUser : Button {
    LineEdit name;
    LineEdit username;
    ItemList icons;
    Label errorThingy;
    

    public override void _Ready() {
        base._Ready();
        name = GetNode<LineEdit>("../../Name");
        username = GetNode<LineEdit>("../../Username");
        icons = GetNode<ItemList>("../../Icons");
        errorThingy = GetNode<Label>("/root/NewUser/Control/ErrorThingy");
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");

        // make the icon be a string and not a number
        string icon = "";
        if (icons.GetSelectedItems().Length > 0) {
            switch (icons.GetSelectedItems()[0]) {
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
        if (name.Text == "") {
            errorThingy.Text = "Invalid name!";
            sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        if (username.Text == "") {
            errorThingy.Text = "Invalid username!";
            sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // these characters are not allowed in windows, which isn't good since each user is a folder and stuff
        Regex what = new Regex("[\"/<>:\\|?*]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (what.Matches(name.Text).Count > 0) {
            errorThingy.Text = "Names can't include the characters \\/<>:|?*";
            sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        Regex idkman = new Regex("[^[a-z0-9._]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        if (idkman.Matches(username.Text).Count > 0) {
            errorThingy.Text = "Lelnet usernames only allow lowercase characters, numbers, underscores (_) and periods (.)";
            sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // make sure the user doesn't already exist :)
        List<string> users = new List<string>();
        Directory dir = new Directory();
        dir.Open("user://Users/");
        dir.ListDirBegin(true);
        string filename = dir.GetNext();
        while (filename != "") {
            users.Add(filename);
            filename = dir.GetNext();
        }
        dir.ListDirEnd();

        if (users.Contains(name.Text)) {
            errorThingy.Text = $"User \"{name.Text}\" already exists!";
            sounds.PlaySoundEffect(SoundManager.SoundEffects.Error);
            return;
        }

        // now we actually make the user and login
        UserInfo info = new UserInfo {
            Photo = icon,
            LelnetUsername = username.Text
        };
        SavingManager.NewUser(name.Text, info);

        SavingManager.CurrentUser = name.Text;
        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Lelsktop.tscn");
        Node lelsktop = packedScene.Instance();
        GetTree().Root.AddChild(lelsktop);
        GetNode<Node2D>("/root/Onboarding").QueueFree();
        GetNode<Node2D>("/root/NewUser").QueueFree();
    }
}
