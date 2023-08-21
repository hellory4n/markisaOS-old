using Godot;
using System;
using Newtonsoft.Json;

public class SavingManager : Node {
    public static string CurrentUser = "";

    public static void NewUser(string user) {
        File file = new File();
        Directory dir = new Directory();
        dir.MakeDirRecursive($"user://Users/{user}/");
        file.Open($"user://Users/{user}/BasicInfo.json", File.ModeFlags.Write);
        BasicUser m = new BasicUser();
        file.StoreString(
            JsonConvert.SerializeObject(m)
        );
        file.Close();

        File pain = new File();
        pain.Open($"user://Users/{user}/UserInfo.json", File.ModeFlags.Write);
        UserInfo a = new UserInfo();
        file.StoreString(
            JsonConvert.SerializeObject(a)
        );
    }

    public static BasicUser LoadBasicUser(string user) {
        File file = new File();
        
        if (file.Open($"user://Users/{user}/BasicInfo.json", File.ModeFlags.Read) == Error.Ok) {
            BasicUser m = JsonConvert.DeserializeObject<BasicUser>(file.GetAsText());
            file.Close();
            return m;
        } else {
            GD.PushError($"Failed to load OS version from user \"{user}\", are you sure it exists?");
            return null;
        }
    }

    public static void SaveBasicUser(string user, BasicUser data) {
        File file = new File();
        
        if (file.Open($"user://Users/{user}/BasicInfo.json", File.ModeFlags.Read) == Error.Ok) {
            file.StoreString(JsonConvert.SerializeObject(data));
            file.Close();
        } else {
            GD.PushError($"Failed to save OS version from user \"{user}\", are you sure it exists?");
        }
    }

    public static string GetUserPhoto(string user) {
        File file = new File();
        
        if (file.Open($"user://Users/{user}/UserInfo.json", File.ModeFlags.Read) == Error.Ok) {
            UserInfo m = JsonConvert.DeserializeObject<UserInfo>(file.GetAsText());
            file.Close();
            return m.Photo;
        } else {
            GD.PushError($"Failed to load photo from user \"{user}\", are you sure it exists?");
            return null;
        }
    }

    public static void SetUserPhoto(string user, string photo) {
        File file = new File();
        
        if (file.Open($"user://Users/{user}/UserInfo.json", File.ModeFlags.Read) == Error.Ok) {
            UserInfo pain = new UserInfo();
            pain.Photo = photo;
            file.StoreString(JsonConvert.SerializeObject(pain));
            file.Close();
        } else {
            GD.PushError($"Failed to save photo from user \"{user}\", are you sure it exists?");
        }
    }
}
