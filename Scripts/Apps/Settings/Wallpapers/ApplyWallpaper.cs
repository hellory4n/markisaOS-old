using Godot;
using System;

public class ApplyWallpaper : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // first apply the wallpaper
        Texture wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
        GetNode<Sprite>("/root/Lelsktop/Wallpaper").Texture = wallpaper;

        // then save the new settings
        UserLelsktop m = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);
        m.Wallpaper = wallpaper.ResourcePath;
        SavingManager.Save(SavingManager.CurrentUser, m);
    }
}
