using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Stores the version of the game used by a user. Useful when converting saves between versions.
/// </summary>
public class BasicUser {
    /// <summary>
    /// Increased with major new versions, with a lot of new stuff, and that probably breaks old stuff.
    /// </summary>
    public int MajorVersion = 0;
    /// <summary>
    /// Increased with smaller new versions, usually some new features and improvements.
    /// </summary>
    public int MinorVersion = 10;
    /// <summary>
    /// Increased with very small new versions, such as bug fixes.
    /// </summary>
    public int PatchVersion = 0;
}

/// <summary>
/// Stores the photo and lelnet username of a user.
/// </summary>
public class UserInfo {
    /// <summary>
    /// The photo used by the user. I should probably use an enum or a file path.
    /// </summary>
    public string Photo = "Lelcube";
    /// <summary>
    /// The lelnet username used by the user. Only accepts lowercase characters, numbers, underscore (_), and periods (.)
    /// </summary>
    public string LelnetUsername = "john.doe";
}

/// <summary>
/// Stores the appearance settings of a user.
/// </summary>
public class UserLelsktop {
    /// <summary>
    /// A file path to the wallpaper used by the user.
    /// </summary>
    public string Wallpaper = "res://Assets/Wallpapers/HighPeaks.jpg";
    /// <summary>
    /// The theme used by the user. It's actually just a part of the file path to the actual theme, e.g. res://Assets/Themes/insert value here/Theme.tres
    /// </summary>
    public string Theme = "Leltheme-Dark-Blue";
}

/// <summary>
/// Type commonly used for listing apps.
/// </summary>

public class Lelapp {
    /// <summary>
    /// The name of the app.
    /// </summary>
    public string Name = "App";
    /// <summary>
    /// A file path to the icon of the app.
    /// </summary>
    public string Icon = "res://Assets/Themes/Leltheme-Dark-Blue/Icons/App.png";
    /// <summary>
    /// A file path to the scene of the app's window.
    /// </summary>
    public string Scene = "res://Apps/WindowManagerTest/WindowManagerTest.tscn";

    public Lelapp(string name, string icon, string scene) {
        Name = name;
        Icon = icon;
        Scene = scene;
    }
}

/// <summary>
/// Used by the app menu to list installed apps.
/// </summary>
public class InstalledApps {
    /// <summary>
    /// Every single app installed in the device.
    /// </summary>
    public Lelapp[] All = new Lelapp[]{};
    /// <summary>
    /// Basically categorizing apps as "Other"
    /// </summary>
    public Lelapp[] Accessories = new Lelapp[]{};
    /// <summary>
    /// Apps used for modding and stuff.
    /// </summary>
    public Lelapp[] Development = new Lelapp[]{};
    /// <summary>
    /// Videogames.
    /// </summary>
    public Lelapp[] Games = new Lelapp[]{};
    /// <summary>
    /// Apps used for doing stuff with graphics.
    /// </summary>
    public Lelapp[] Graphics = new Lelapp[]{};
    /// <summary>
    /// Apps related to the world wide web.
    /// </summary>
    public Lelapp[] Internet = new Lelapp[]{};
    /// <summary>
    /// Apps that do stuff with media.
    /// </summary>
    public Lelapp[] Multimedia = new Lelapp[]{};
    /// <summary>
    /// Apps used in offices and stuff.
    /// </summary>
    public Lelapp[] Office = new Lelapp[]{};
    /// <summary>
    /// Apps that come with the system or something.
    /// </summary>
    public Lelapp[] System = new Lelapp[]{};
    /// <summary>
    /// Very small apps like a calculator and a photo viewer.
    /// </summary>
    public Lelapp[] Utilities = new Lelapp[]{};

    public InstalledApps() {
        All = new Lelapp[]{
            new Lelapp("Test App", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/App.png", "res://Apps/WindowManagerTest/WindowManagerTest.tscn"),
            new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn"),
            new Lelapp("Files", "res://Apps/Files/Assets/IconSmall.png", "res://Apps/Files/Files.tscn"),
            new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"),
            new Lelapp("Notebook", "res://Apps/Notebook/Assets/IconSmall.png", "res://Apps/Notebook/Notebook.tscn"),
            new Lelapp("Calculator", "res://Apps/Calculator/Assets/IconSmall.png", "res://Apps/Calculator/Calculator.tscn"),
            new Lelapp("Websites", "res://Apps/Websites/Assets/IconSmall.png", "res://Apps/Websites/Websites.tscn"),
            new Lelapp("Messages", "res://Apps/Messages/Assets/IconSmall.png", "res://Apps/Messages/Messages.tscn"),
            new Lelapp("Mines", "res://Apps/Mines/Assets/IconSmall.png", "res://Apps/Mines/Mines.tscn"),
            new Lelapp("Sausage Clicker", "res://Apps/SausageClicker/Assets/IconSmall.png", "res://Apps/SausageClicker/SausageClicker.tscn")
        };
        Accessories = new Lelapp[]{
            new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn"),
            new Lelapp("Files", "res://Apps/Files/Assets/IconSmall.png", "res://Apps/Files/Files.tscn")
        };
        System = new Lelapp[]{
            new Lelapp("Settings", "res://Assets/Themes/Leltheme-Dark-Blue/Icons/Accessories.png", "res://Apps/Settings/Settings.tscn")
        };
        Graphics = new Lelapp[]{
            new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn")
        };
        Internet = new Lelapp[]{
            new Lelapp("Websites", "res://Apps/Websites/Assets/IconSmall.png", "res://Apps/Websites/Websites.tscn"),
            new Lelapp("Messages", "res://Apps/Messages/Assets/IconSmall.png", "res://Apps/Messages/Messages.tscn")
        };
        Multimedia = new Lelapp[]{
            new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn")
        };
        Utilities = new Lelapp[]{
            new Lelapp("Observer", "res://Apps/Observer/Assets/IconSmall.png", "res://Apps/Observer/Observer.tscn"),
            new Lelapp("Notebook", "res://Apps/Notebook/Assets/IconSmall.png", "res://Apps/Notebook/Notebook.tscn"),
            new Lelapp("Calculator", "res://Apps/Calculator/Assets/IconSmall.png", "res://Apps/Calculator/Calculator.tscn")
        };
        Games = new Lelapp[]{
            new Lelapp("Mines", "res://Apps/Mines/Assets/IconSmall.png", "res://Apps/Mines/Mines.tscn"),
            new Lelapp("Sausage Clicker", "res://Apps/SausageClicker/Assets/IconSmall.png", "res://Apps/SausageClicker/SausageClicker.tscn")
        };
    }
}

/// <summary>
/// Used for storing the apps pinned in the dock.
/// </summary>
public class QuickLaunch {
    /// <summary>
    /// The apps in the quick launcher.
    /// </summary>
    public Lelapp[] Apps = new Lelapp[]{};
}

/// <summary>
/// Used for storing the user's conversations.
/// </summary>
public class SocialStuff {
    /// <summary>
    /// The user's conversations.
    /// </summary>
    public Conversation[] Conversations = new Conversation[]{};

    public SocialStuff() {
        Conversations = Conversations.Append(new Conversation {
            Name = "Passionfruit Support",
            Icon = "res://Apps/Messages/Assets/Support.png",
            Messages = new Message[]{
                new Message {
                    Author = "Passionfruit Support",
                    Text = $"Hello, {SavingManager.CurrentUser}! How could i help you?"
                }
            },
            Choices = new Dictionary<string, MessagingManager.Messages>(){
                {"I need help with lelcubeOS Me apps", MessagingManager.Messages.PassionfruitSupportApps},
                {"I need help with my device", MessagingManager.Messages.PassionfruitSupportDevice},
                {"I want a refund for my device", MessagingManager.Messages.PassionfruitSupportRefund},
                {"I am going to sue you", MessagingManager.Messages.PassionfruitSupportLawsuit}
            }
        }).ToArray();
    }
}

/// <summary>
/// A conversation.
/// </summary>
public class Conversation {
    /// <summary>
    /// The name of the conversation.
    /// </summary>
    public string Name = "";
    /// <summary>
    /// The path of the icon for the conversation. (28x28)
    /// </summary>
    public string Icon = "res://Apps/Messages/Assets/IconSmall.png";
    /// <summary>
    /// The choices the user can use to continue the conversation.
    /// </summary>
    public Dictionary<string, MessagingManager.Messages> Choices = new Dictionary<string, MessagingManager.Messages>();
    /// <summary>
    /// The messages of the conversation.
    /// </summary>
    public Message[] Messages = new Message[]{};
}

/// <summary>
/// A message.
/// </summary>
public class Message {
    /// <summary>
    /// The author of the message. ("You" for the user)
    /// </summary>
    public string Author = "";
    /// <summary>
    /// The text of the message.
    /// </summary>
    public string Text = "";
}