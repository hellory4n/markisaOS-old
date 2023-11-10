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
    public static void Convert0_8(string user)
    {
        var coolApps = SavingManager.Load<InstalledApps>(user);
        // fun
        var fuckAll = coolApps.All.ToList();
        fuckAll.Add(new Lelapp("Websites", "res://Apps/Websites/Assets/IconSmall.png", "res://Apps/Websites/Websites.tscn"));
        fuckAll.Add(new Lelapp("Messages", "res://Apps/Messages/Assets/IconSmall.png", "res://Apps/Messages/Messages.tscn"));
        coolApps.All = fuckAll.ToArray();
        var fuckInternet = coolApps.Internet.ToList();
        fuckInternet.Add(new Lelapp("Websites", "res://Apps/Websites/Assets/IconSmall.png", "res://Apps/Websites/Websites.tscn"));
        fuckInternet.Add(new Lelapp("Messages", "res://Apps/Messages/Assets/IconSmall.png", "res://Apps/Messages/Messages.tscn"));
        coolApps.Internet = fuckInternet.ToArray();
        SavingManager.Save(user, coolApps);

        FileAccess suffer = FileAccess.Open($"user://Users/{user}/SocialStuff.json", FileAccess.ModeFlags.Write);
        suffer.StoreString(
            JsonConvert.SerializeObject(new SocialStuff(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
        }));
        suffer.Close();

        SavingManager.Save(user, new BasicUser
        {
            MajorVersion = 0,
            MinorVersion = 9,
        });
    }
}
