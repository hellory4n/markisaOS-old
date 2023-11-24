using Godot;
using System;
using Kickstart.Drivers;
using Dashboard.Toolkit;

public partial class ApplyWallpaper : Button {
    public override void _Pressed()
    {
        base._Pressed();
        Texture2D wallpaper;
        string wallpaperSaveThing;
        // is there a custom image the user chose?
        string coolWallpaperId = GetNode<LineEdit>("../LineEdit").Text;
        // hell
        if (CabinetfsManager.IdExists(coolWallpaperId))
        {
            var coolImage = CabinetfsManager.LoadById<CabinetfsFile>(coolWallpaperId);
            if (coolImage.Type == "Picture")
            {
                if (coolImage.Data.ContainsKey("Resource"))
                {
                    wallpaper = ResourceManager.LoadImage(coolImage.Data["Resource"].ToString());
                    wallpaperSaveThing = coolWallpaperId;
                }
                else
                {
                    wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
                    wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
                }
            }
            else
            {
                wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
                wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
            }
        }
        else
        {
            // ok there's no wallpaper, use one of the default options
            wallpaper = GetNode<WallpaperThing>("../ChooseThingy").Wallpaper;
            wallpaperSaveThing = GetNode<WallpaperThing>("../ChooseThingy").WallpaperPath;
        }

        // actually apply the wallpaper
        GetNode<Sprite2D>("/root/Dashboard/Wallpaper").Texture = wallpaper;

        // scale wallpaper :))))))))
        GetNode<ImageBackground>("/root/Dashboard/Wallpaper").OriginalSize = wallpaper.GetSize();
        Vector2 bruh = ResolutionManager.Resolution;
        float scale;
        if (bruh > wallpaper.GetSize()) {
            scale = (Mathf.Max(bruh.X, bruh.Y) - Mathf.Max(wallpaper.GetSize().X, wallpaper.GetSize().Y)) /
                Mathf.Max(wallpaper.GetSize().X, wallpaper.GetSize().Y);
            scale += 1;
        } else {
            scale = Mathf.Max(bruh.X, bruh.Y) / Mathf.Max(wallpaper.GetSize().X, wallpaper.GetSize().Y);
        }
        GetNode<Sprite2D>("/root/Dashboard/Wallpaper").Scale = new Vector2(scale, scale);
        GetNode<Sprite2D>("/root/Dashboard/Wallpaper").Position = bruh/2;

        // then save the new settings
        UserDashboard m = SavingManager.Load<UserDashboard>(SavingManager.CurrentUser);
        m.Wallpaper = wallpaperSaveThing;
        SavingManager.Save(SavingManager.CurrentUser, m);
    }
}
