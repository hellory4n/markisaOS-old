using Godot;
using System;

namespace Lelcore.Onboarding;

public partial class UserList : VBoxContainer
{
    public override void _Ready()
    {
        base._Ready();

        PackedScene stupidity = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/UserButton.tscn");
        Texture2D cat = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Cat.png");
        Texture2D flower = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Flower.png");
        Texture2D balloons = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Balloons.png");
        Texture2D car = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Car.png");
        Texture2D dog = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Dog.png");
        Texture2D duck = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Duck.png");
        Texture2D pancakes = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Pancakes.png");
        Texture2D brushes = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Brushes.png");
        Texture2D shuttle = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Shuttle.png");
        Texture2D football = ResourceLoader.Load<Texture2D>("res://Assets/UserIcons/Football.png");

        if (DirAccess.DirExistsAbsolute("user://Users/")) {
            DirAccess dir = DirAccess.Open("user://Users/");
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "") {
                Button useromgomgomg = (Button)stupidity.Instantiate();
                useromgomgomg.Text = filename;
                
                // cool user photo
                string photo = SavingManager.Load<UserInfo>(filename).Photo;
                switch (photo) {
                    case "Cat":
                        useromgomgomg.Icon = cat;
                        break;
                    case "Flower":
                        useromgomgomg.Icon = flower;
                        break;
                    case "Balloon":
                        useromgomgomg.Icon = balloons;
                        break;
                    case "Car":
                        useromgomgomg.Icon = car;
                        break;
                    case "Dog":
                        useromgomgomg.Icon = dog;
                        break;
                    case "Duck":
                        useromgomgomg.Icon = duck;
                        break;
                    case "Pancakes":
                        useromgomgomg.Icon = pancakes;
                        break;
                    case "Brushes":
                        useromgomgomg.Icon = brushes;
                        break;
                    case "Shuttle":
                        useromgomgomg.Icon = shuttle;
                        break;
                    case "Football":
                        useromgomgomg.Icon = football;
                        break;
                }

                AddChild(useromgomgomg);
                filename = dir.GetNext();
            }
        }
    }
}
