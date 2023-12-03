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
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Flowers.jpg");
                WallpaperPath = "res://Assets/Wallpapers/Flowers.jpg";
                break;
            case 2:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Beaches.jpg");
                WallpaperPath = "res://Assets/Wallpapers/Beaches.jpg";
                break;
            case 3:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Space.jpg");
                WallpaperPath = "res://Assets/Wallpapers/Space.jpg";
                break;
            case 4:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Mountains.jpg");
                WallpaperPath = "res://Assets/Wallpapers/Mountains.jpg";
                break;
            case 5:
                Wallpaper = GD.Load<Texture2D>("res://Assets/Wallpapers/Aurora.jpg");
                WallpaperPath = "res://Assets/Wallpapers/Aurora.jpg";
                break;
        }
    }
}
