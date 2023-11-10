using Godot;
using System;

namespace Lelsktop.Interface;

public partial class QuickSettingsUserIcon : Sprite2D
{
    public override void _Ready()
    {
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
        switch (photo)
        {
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
