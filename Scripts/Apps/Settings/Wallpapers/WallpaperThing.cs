using Godot;
using System;

public class WallpaperThing : OptionButton {
    // public so when it's applied we don't have to load the wallpaper twice
    public Texture Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/HighPeaks.jpg");
    public string WallpaperPath = "res://Assets/Wallpapers/HighPeaks.jpg";

    public override void _Ready() {
        base._Ready();
        AddItem("High Peaks");
        AddItem("Flowers");
        AddItem("Beaches");
        AddItem("Space");
        AddItem("Mountains");
        AddItem("Aurora");
        Connect("item_selected", this, nameof(PreviewThing));
    }

    public void PreviewThing(int index) {
        Sprite preview = GetNode<Sprite>("../Control/Preview");
        switch (index) {
            case 0:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/HighPeaks.jpg");
                WallpaperPath = "res://Assets/Wallpapers/HighPeaks.jpg";
                break;
            case 1:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Flowers.png");
                WallpaperPath = "res://Assets/Wallpapers/Flowers.png";
                break;
            case 2:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Beaches.png");
                WallpaperPath = "res://Assets/Wallpapers/Beaches.png";
                break;
            case 3:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Space.png");
                WallpaperPath = "res://Assets/Wallpapers/Space.png";
                break;
            case 4:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Mountains.png");
                WallpaperPath = "res://Assets/Wallpapers/Mountains.png";
                break;
            case 5:
                Wallpaper = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Aurora.png");
                WallpaperPath = "res://Assets/Wallpapers/Aurora.png";
                break;
        }
        preview.Texture = Wallpaper;
    }
}
