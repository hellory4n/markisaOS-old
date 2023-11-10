using Godot;
using System;
using Newtonsoft.Json;
using System.Linq;

public partial class UserConverter : Node
{
    /// <summary>
    /// v0.9 added the web browser, messaging app, and computer noises
    /// </summary>
    /// <param name="user">The user to convert</param>
    public static void Convert0_9(string user)
    {
        var coolApps = SavingManager.Load<InstalledApps>(user);
        // fun
        coolApps.All = coolApps.All.Append(new Lelapp("Mines", "res://Apps/Mines/Assets/IconSmall.png", "res://Apps/Mines/Mines.tscn")).ToArray();
        coolApps.All = coolApps.All.Append(new Lelapp("Sausage Clicker", "res://Apps/SausageClicker/Assets/IconSmall.png", "res://Apps/SausageClicker/SausageClicker.tscn")).ToArray();
        coolApps.All = coolApps.All.Append(new Lelapp("Tour", "res://Apps/Tour/Assets/IconSmall.png", "res://Apps/Tour/Tour.tscn")).ToArray();
        coolApps.Games = coolApps.Games.Append(new Lelapp("Mines", "res://Apps/Mines/Assets/IconSmall.png", "res://Apps/Mines/Mines.tscn")).ToArray();
        coolApps.Games = coolApps.Games.Append(new Lelapp("Sausage Clicker", "res://Apps/SausageClicker/Assets/IconSmall.png", "res://Apps/SausageClicker/SausageClicker.tscn")).ToArray();
        coolApps.Utilities = coolApps.Utilities.Append(new Lelapp("Tour", "res://Apps/Tour/Assets/IconSmall.png", "res://Apps/Tour/Tour.tscn")).ToArray();
        SavingManager.Save(user, coolApps);

        FileAccess fgbfg = FileAccess.Open($"user://Users/{user}/LelsktopPinboard.json", FileAccess.ModeFlags.Write);
        fgbfg.StoreString(
            JsonConvert.SerializeObject(new LelsktopPinboard())
        );
        fgbfg.Close();

        // new files and stuff :)
        Folder samplePictures = LelfsManager.NewFolder("Sample Pictures", LelfsManager.PermanentPath("/Home/Pictures"));
        samplePictures.Metadata.Add("CreationDate", DateTime.Now);
        samplePictures.Save();

        LelfsFile highPeaks = LelfsManager.NewFile("High Peaks", samplePictures.Id);
        highPeaks.Type = "Picture";
        highPeaks.Data.Add("Resource", "res://Assets/Wallpapers/HighPeaks.jpg");
        highPeaks.Metadata.Add("CreationDate", DateTime.Now);
        highPeaks.Save();

        LelfsFile flowers = LelfsManager.NewFile("Flowers", samplePictures.Id);
        flowers.Type = "Picture";
        flowers.Data.Add("Resource", "res://Assets/Wallpapers/Flowers.png");
        flowers.Metadata.Add("CreationDate", DateTime.Now);
        flowers.Save();

        LelfsFile beaches = LelfsManager.NewFile("Beaches", samplePictures.Id);
        beaches.Type = "Picture";
        beaches.Data.Add("Resource", "res://Assets/Wallpapers/Beaches.png");
        beaches.Metadata.Add("CreationDate", DateTime.Now);
        beaches.Save();

        LelfsFile aurora = LelfsManager.NewFile("Aurora", samplePictures.Id);
        aurora.Type = "Picture";
        aurora.Data.Add("Resource", "res://Assets/Wallpapers/Aurora.png");
        aurora.Metadata.Add("CreationDate", DateTime.Now);
        aurora.Save();

        LelfsFile mountains = LelfsManager.NewFile("Mountains", samplePictures.Id);
        mountains.Type = "Picture";
        mountains.Data.Add("Resource", "res://Assets/Wallpapers/Mountains.png");
        mountains.Metadata.Add("CreationDate", DateTime.Now);
        mountains.Save();

        LelfsFile space = LelfsManager.NewFile("Space", samplePictures.Id);
        space.Type = "Picture";
        space.Data.Add("Resource", "res://Assets/Wallpapers/Space.png");
        space.Metadata.Add("CreationDate", DateTime.Now);
        space.Save();

        LelfsFile logo = LelfsManager.NewFile("lelcubeOS", LelfsManager.PermanentPath("/Home/Pictures"));
        logo.Type = "Picture";
        logo.Data.Add("Resource", "res://Assets/Boot/Logo2.png");
        logo.Metadata.Add("CreationDate", DateTime.Now);
        logo.Save();

        SavingManager.Save(user, new BasicUser
        {
            MajorVersion = 0,
            MinorVersion = 10,
        });
    }
}
