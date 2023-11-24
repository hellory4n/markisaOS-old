using Godot;
using System;
using Newtonsoft.Json;

public partial class UserConverter : Node
{
    /// <summary>
    /// Versions before the user conversion code thingy existed
    /// </summary>
    /// <param name="user">The user to convert</param>
    public static void ReallyOldVersions(string user)
    {
        // create the installed apps and quick settings thing
        FileAccess j = FileAccess.Open($"user://Users/{user}/InstalledApps.json", FileAccess.ModeFlags.Write);
        j.StoreString(
            JsonConvert.SerializeObject(new InstalledApps(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
        }));
        j.Close();

        FileAccess suffer = FileAccess.Open($"user://Users/{user}/QuickLaunch.json", FileAccess.ModeFlags.Write);
        suffer.StoreString(
            JsonConvert.SerializeObject(new QuickLaunch(), new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
        }));
        suffer.Close();

        // setup the filesystem
        DirAccess.MakeDirRecursiveAbsolute($"user://Users/{user}/Files/");

        FileAccess haha = FileAccess.Open($"user://Users/{user}/Files/root.json", FileAccess.ModeFlags.Write);
        haha.StoreString("{\"$type\":\"CabinetfsRoot, lelcubeOS\",\"Id\":\"root\",\"Parent\":null,\"Name\":\"\",\"Metadata\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib\"},\"Path\":\"/\",\"Type\":\"Root\",\"Data\":{\"$type\":\"System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib\"}}");
        haha.Close();

        CabinetfsManager.UpdatePaths();
        CabinetfsRoot.CreateRoot();
        CabinetfsManager.NewFileStructure();

        SavingManager.Save(user, new BasicUser
        {
            MajorVersion = 0,
            MinorVersion = 7,
        });
    }
}
