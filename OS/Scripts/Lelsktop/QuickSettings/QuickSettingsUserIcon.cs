using Godot;
using System;

public partial class QuickSettingsUserIcon : Sprite2D {
    public override void _Ready() {
        base._Ready();
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
                
        // cool user photo
        string photo = SavingManager.Load<UserInfo>(SavingManager.CurrentUser).Photo;

        switch (photo) {
            case "Cat":
                Texture2D = cat;
                break;
            case "Flower":
                Texture2D = flower;
                break;
            case "Balloon":
                Texture2D = balloons;
                break;
            case "Car":
                Texture2D = car;
                break;
            case "Dog":
                Texture2D = dog;
                break;
            case "Duck":
                Texture2D = duck;
                break;
            case "Pancakes":
                Texture2D = pancakes;
                break;
            case "Brushes":
                Texture2D = brushes;
                break;
            case "Shuttle":
                Texture2D = shuttle;
                break;
            case "Football":
                Texture2D = football;
                break;
        }
    }
}
