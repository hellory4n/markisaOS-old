using Godot;
using System;

public partial class InstallelWallpaper : OptionButton {
    public override void _Ready() {
        base._Ready();
        Connect("item_selected", new Callable(this, nameof(Click)));
    }

    public void Click(int index) {
        // this is fine.
        switch (index) {
            case 0:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/HighPeaks.jpg");
                break;
            case 1:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/Flowers.png");
                break;
            case 2:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/Beaches.png");
                break;
            case 3:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/Space.png");
                break;
            case 4:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/Mountains.png");
                break;
            case 5:
                GetNode<Sprite2D>("/root/InstallelOobe/Wallpaper").Texture2D = ResourceLoader.Load<Texture2D>("res://Assets/Wallpapers/Aurora.png");
                break;
        }
    }
}
