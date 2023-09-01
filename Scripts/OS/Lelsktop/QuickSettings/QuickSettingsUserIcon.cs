using Godot;
using System;

public class QuickSettingsUserIcon : Sprite {
    public override void _Ready() {
        base._Ready();
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
                
        // cool user photo
        string photo = SavingManager.Load<UserInfo>(SavingManager.CurrentUser).Photo;

        switch (photo) {
            case "Cat":
                Texture = cat;
                break;
            case "Flower":
                Texture = flower;
                break;
            case "Balloon":
                Texture = balloons;
                break;
            case "Car":
                Texture = car;
                break;
            case "Dog":
                Texture = dog;
                break;
            case "Duck":
                Texture = duck;
                break;
            case "Pancakes":
                Texture = pancakes;
                break;
            case "Brushes":
                Texture = brushes;
                break;
            case "Shuttle":
                Texture = shuttle;
                break;
            case "Football":
                Texture = football;
                break;
        }
    }
}
