public class BasicUser {
    public int MajorVersion = 0;
    public int MinorVersion = 7;
    public int PatchVersion = 0;
}

public class UserInfo {
    public string Photo = "Lelcube";
    public string LelnetUsername = "john.doe";
}

public class UserLelsktop {
    public string Wallpaper = "res://Assets/Wallpapers/HighPeaks.jpg";
    public string Theme = "Leltheme-Dark-Blue";
}

public class Lelapp {
    public string Name = "App";
    public string Icon = "res://Assets/Themes/Leltheme-Dark-Blue/Icons/App.png";
    public string Scene = "res://Apps/WindowManagerTest/WindowManagerTest.tscn";

    public Lelapp(string name, string icon, string scene) {
        Name = name;
        Icon = icon;
        Scene = scene;
    }
}

public class InstalledApps {
    public Lelapp[] All = new Lelapp[]{};
    public Lelapp[] Accessories = new Lelapp[]{};
    public Lelapp[] Development = new Lelapp[]{};
    public Lelapp[] Games = new Lelapp[]{};
    public Lelapp[] Graphics = new Lelapp[]{};
    public Lelapp[] Internet = new Lelapp[]{};
    public Lelapp[] Multimedia = new Lelapp[]{};
    public Lelapp[] Office = new Lelapp[]{};
    public Lelapp[] System = new Lelapp[]{};
    public Lelapp[] Utilities = new Lelapp[]{};
}