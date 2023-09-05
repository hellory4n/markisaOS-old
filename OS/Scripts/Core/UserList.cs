using Godot;
using System;

public class UserList : VBoxContainer {
    public override void _Ready() {
        base._Ready();

        PackedScene stupidity = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/UserButton.tscn");
        Texture cat = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Cat.png");
        Texture flower = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Flower.png");
        Texture balloons = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Balloons.png");
        Texture car = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Car.png");
        Texture dog = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Dog.png");
        Texture duck = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Duck.png");
        Texture pancakes = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Pancakes.png");
        Texture brushes = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Brushes.png");
        Texture shuttle = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Shuttle.png");
        Texture football = ResourceLoader.Load<Texture>("res://Assets/UserIcons/Football.png");

        Directory dir = new Directory();
        if (dir.DirExists("user://Users/")) {
            dir.Open("user://Users/");
            dir.ListDirBegin(true);
            string filename = dir.GetNext();
            while (filename != "") {
                Button useromgomgomg = (Button)stupidity.Instance();
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
            dir.ListDirEnd();
        }
    }
}
