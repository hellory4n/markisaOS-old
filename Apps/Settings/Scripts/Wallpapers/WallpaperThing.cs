using Godot;
using System;

public partial class WallpaperThing : OptionButton {
    // public so when it's applied we don't have to load the wallpaper twice
    public Texture2D Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/HighPeaks.jpg");
    public string WallpaperPath = "res://Assets/Wallpapers/HighPeaks.jpg";

    public override void _Ready() {
        base._Ready();
        AddItem("High Peaks");
        AddItem("Flowers");
        AddItem("Beaches");
        AddItem("Space");
        AddItem("Mountains");
        AddItem("Aurora");
        Connect("item_selected", new Callable(this, nameof(PreviewThing)));
    }

    public void PreviewThing(int index) {
        switch (index) {
            case 0:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/HighPeaks.jpg");
                WallpaperPath = "res://Assets/Wallpapers/HighPeaks.jpg";
                break;
            case 1:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Flowers.png");
                WallpaperPath = "res://Assets/Wallpapers/Flowers.png";
                break;
            case 2:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Beaches.png");
                WallpaperPath = "res://Assets/Wallpapers/Beaches.png";
                break;
            case 3:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Space.png");
                WallpaperPath = "res://Assets/Wallpapers/Space.png";
                break;
            case 4:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Mountains.png");
                WallpaperPath = "res://Assets/Wallpapers/Mountains.png";
                break;
            case 5:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Aurora.png");
                WallpaperPath = "res://Assets/Wallpapers/Aurora.png";
                break;
        }
    }
}
