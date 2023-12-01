using Godot;
using Newtonsoft.Json;
using System;

namespace Kickstart.Records;

/// <summary>
/// Manages records.
/// </summary>
public partial class Record<T> where T : struct, IRecordData
{
    /// <summary>
    /// The data of this record.
    /// </summary>
    public T Data;

    /// <summary>
    /// Saves the record.
    /// </summary>
    public void Save()
    {
        string path = ProcessFilename();
        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(
            JsonConvert.SerializeObject(Data, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            })
        );
    }

    /// <summary>
    /// Loads a record.
    /// </summary>
    public Record()
    {
        // temporary object so we can generate a path and load the data frfrfr
        T Data = new();
        string path = ProcessFilename();

        if (FileAccess.FileExists(path))
        {
            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            Data = JsonConvert.DeserializeObject<T>(
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
                JsonConvert.SerializeObject(Data, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                })
            );
        }
    }
    
    string ProcessFilename()
    {
        string bruh = $"user://{Data.GetFilename()}";
        bruh = bruh.Replace("%user", $"Users/{RecordManager.CurrentUser}");
        DirAccess.MakeDirRecursiveAbsolute(bruh.GetBaseDir());
        return bruh;
    }
}

public partial class RecordManager
{
    public static string CurrentUser;
    public static string CurrentUserDisplayName;
}