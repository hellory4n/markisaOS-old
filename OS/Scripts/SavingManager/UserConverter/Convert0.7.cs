using Godot;
using System;
using Newtonsoft.Json;
using System.Linq;

public partial class UserConverter : Node
{
    /// <summary>
    /// v0.8 added multimedia stuff and very cool utilities
    /// </summary>
    /// <param name="user">The user to convert</param>
    public static void Convert0_7(string user)
    {
        var coolApps = SavingManager.Load<InstalledApps>(user);
        // fun
        var fuckAll = coolApps.All.ToList();
        fuckAll.Add(new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"));
        fuckAll.Add(new Lelapp("Notebook", "res://Apps/Notebook/Assets/IconSmall.png", "res://Apps/Notebook/Notebook.tscn"));
        fuckAll.Add(new Lelapp("Calculator", "res://Apps/Calculator/Assets/IconSmall.png", "res://Apps/Calculator/Calculator.tscn"));
        var fuckUtilities = coolApps.Utilities.ToList();
        fuckUtilities.Add(new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"));
        fuckUtilities.Add(new Lelapp("Notebook", "res://Apps/Notebook/Assets/IconSmall.png", "res://Apps/Notebook/Notebook.tscn"));
        fuckUtilities.Add(new Lelapp("Calculator", "res://Apps/Calculator/Assets/IconSmall.png", "res://Apps/Calculator/Calculator.tscn"));
        coolApps.All = fuckAll.ToArray();
        coolApps.Utilities = fuckUtilities.ToArray();
        var fuckGraphics = coolApps.Graphics.ToList();
        fuckGraphics.Add(new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"));
        coolApps.Graphics = fuckGraphics.ToArray();
        var fuckMultimedia = coolApps.Multimedia.ToList();
        fuckMultimedia.Add(new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"));
        coolApps.Multimedia = fuckMultimedia.ToArray();
        SavingManager.Save(user, coolApps);

        SavingManager.Save(user, new BasicUser
        {
            MajorVersion = 0,
            MinorVersion = 8,
        });
    }
}
