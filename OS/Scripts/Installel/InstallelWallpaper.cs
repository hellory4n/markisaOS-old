using Godot;
using System;

public class InstallelWallpaper : OptionButton {
    public override void _Ready() {
        base._Ready();
        Connect("item_selected", this, nameof(Click));
    }

    public void Click(int index) {
        // this is fine.
        switch (index) {
            case 0:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/HighPeaks.jpg");
                break;
            case 1:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Flowers.png");
                break;
            case 2:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Beaches.png");
                break;
            case 3:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Space.png");
                break;
            case 4:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Mountains.png");
                break;
            case 5:
                GetNode<Sprite>("/root/InstallelOobe/Wallpaper").Texture = ResourceLoader.Load<Texture>("res://Assets/Wallpapers/Aurora.png");
                break;
        }
    }
}
