using Godot;
using Newtonsoft.Json;
using System;

namespace Kickstart.Records;

/// <summary>
/// Manages records.
/// </summary>
public partial class RecordManager : Node
{
    public static string CurrentUser;
    public static string CurrentUserDisplayName;

    public static void Save<T>(T data) where T : IRecord
    {
        string path = data.GetType() switch
        {
            RecordType.User => $"user://Users/{CurrentUser}/{data.GetFilename()}",
            RecordType.App => $"user://User/{CurrentUser}/Apps/{data.GetAppName()}/{data.GetFilename()}",
            RecordType.Website => $"user://Users/{CurrentUser}/Web/{data.GetAppName()}/{data.GetFilename()}",
            _ => $"user://Settings/{data.GetFilename()}",
        };
        DirAccess.MakeDirRecursiveAbsolute(path.Replace(data.GetFilename(), ""));

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            })
        );
    }

    public static T Load<T>() where T : IRecord, new()
    {
        // temporary object so we can generate a path and load the data frfrfr
        T data = new();
        string path = data.GetType() switch
        {
            RecordType.User => $"user://Users/{CurrentUser}/{data.GetFilename()}",
            RecordType.App => $"user://User/{CurrentUser}/Apps/{data.GetAppName()}/{data.GetFilename()}",
            RecordType.Website => $"user://Users/{CurrentUser}/Web/{data.GetAppName()}/{data.GetFilename()}",
            _ => $"user://Settings/{data.GetFilename()}",
        };
        DirAccess.MakeDirRecursiveAbsolute(path.Replace(data.GetFilename(), ""));

        if (FileAccess.FileExists(path))
        {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            return JsonConvert.DeserializeObject<T>(
                file.GetAsText(), new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                }
            );
        }
        else
        {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
            file.StoreString(
                JsonConvert.SerializeObject(data, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                })
            );
            return data;
        }
    }
}