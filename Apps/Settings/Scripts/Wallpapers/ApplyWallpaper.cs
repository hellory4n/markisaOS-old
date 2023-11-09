using Godot;
using System;

public partial class ApplyWallpaper : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        Texture2D wallpaper;
        string wallpaperSaveThing;
        // is there a custom image the user chose?
        string coolWallpaperId = GetNode<LineEdit>("../LineEdit").Text;
        // hell
        if (LelfsManager.IdExists(coolWallpaperId)) {
            var coolImage = LelfsManager.LoadById<LelfsFile>(coolWallpaperId);
            if (coolImage.Type == "Picture") {
                if (coolImage.Data.ContainsKey("Resource")) {
                    wallpaper = ResourceManager.LoadImage(coolImage.Data["Resource"].ToString());
                    wallpaperSaveThing = coolWallpaperId;
                } else {
                    wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
                    wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
                }
            } else {
                wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
                wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
            }
        } else {
            // ok there's no wallpaper, use one of the default options
            wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
            wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
        }

        // actually apply the wallpaper
        GetNode<Sprite2D>("/root/Lelsktop/Wallpaper").Texture2D = wallpaper;

        // scale wallpaper :))))))))
        GetNode<ImageBackground>("/root/Lelsktop/Wallpaper").OriginalSize = wallpaper.GetSize();
        Vector2 bruh = ResolutionManager.Resolution;
        float scale;
        if (bruh > wallpaper.GetSize()) {
            scale = (Mathf.Max(bruh.x, bruh.y) - Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y)) /
                Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y);
            scale += 1;
        } else {
            scale = Mathf.Max(bruh.x, bruh.y) / Mathf.Max(wallpaper.GetSize().x, wallpaper.GetSize().y);
        }
        GetNode<Sprite2D>("/root/Lelsktop/Wallpaper").Scale = new Vector2(scale, scale);
        GetNode<Sprite2D>("/root/Lelsktop/Wallpaper").Position = bruh/2;

        // then save the new settings
        UserLelsktop m = SavingManager.Load<UserLelsktop>(SavingManager.CurrentUser);
        m.Wallpaper = wallpaperSaveThing;
        SavingManager.Save(SavingManager.CurrentUser, m);
    }
}
