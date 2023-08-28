using Godot;
using System;
using Newtonsoft.Json;

public class SavingManager : Node {
    public static string CurrentUser = "";
    public enum Info {
        BasicInfo,
        UserInfo
    }

    public static void NewUser(string user, UserInfo info) {
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
        pain.StoreString(
            JsonConvert.SerializeObject(info)
        );
        pain.Close();
    }    

    public static T Load<T>(string user, Info info) {
        string filename = "";
        switch (info) {
            case Info.BasicInfo:
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case Info.UserInfo:
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Read) == Error.Ok) {
            T m = JsonConvert.DeserializeObject<T>(file.GetAsText());
            file.Close();
            return m;
        } else {
            GD.PushError($"Failed to load data from user \"{user}\", are you sure it exists?");
            return default;
        }
    }

    public static void Save<T>(string user, Info info, T data) {
        string filename = "";
        switch (info) {
            case Info.BasicInfo:
                filename = $"user://Users/{user}/BasicInfo.json";
                break;
            case Info.UserInfo:
                filename = $"user://Users/{user}/UserInfo.json";
                break;
            default:
                GD.PushError("Invalid user info type!");
                break;
        }

        File file = new File();
        if (file.Open(filename, File.ModeFlags.Write) == Error.Ok) {
            file.StoreString(JsonConvert.SerializeObject(data));
            file.Close();
        } else {
            GD.PushError($"Failed to save data from user \"{user}\", are you sure it exists?");
        }
    }
}
