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

    public static void Save<T>(T data) where T : Record
    {
        string path = data.Type switch
        {
            RecordType.User => $"user://Users/{CurrentUser}/{data.Filename}",
            RecordType.App => $"user://User/{CurrentUser}/Apps/{data.AppName}/{data.Filename}",
            RecordType.Website => $"user://Users/{CurrentUser}/Web/{data.AppName}/{data.Filename}",
            _ => $"user://Settings/{data.Filename}",
        };
        DirAccess.MakeDirRecursiveAbsolute(path.Replace(data.Filename, ""));

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            })
        );
    }

    public static T Load<T>() where T : Record, new()
    {
        // temporary object so we can generate a path and load the data frfrfr
        T data = new();
        string path = data.Type switch
        {
            RecordType.User => $"user://Users/{CurrentUser}/{data.Filename}",
            RecordType.App => $"user://Users/{CurrentUser}/Apps/{data.AppName}/{data.Filename}",
            RecordType.Website => $"user://Users/{CurrentUser}/Web/{data.AppName}/{data.Filename}",
            _ => $"user://Settings/{data.Filename}",
        };
        DirAccess.MakeDirRecursiveAbsolute(path.Replace(data.Filename, ""));

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