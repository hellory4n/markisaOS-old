using Godot;
using System;

namespace Kickstart.Records;

/// <summary>
/// Base class for markisaOS data. Use with RecordManager.
/// </summary>
public partial interface IRecord
{
    /// <summary>
    /// Gets the type of the record.
    /// </summary>
    /// <returns>The type of the record.</returns>
    public static virtual RecordType GetType() { return default; }
    /// <summary>
    /// Gets the filename of the record. Automatically gets a folder path at the beginning.
    /// </summary>
    /// <returns>The filename of the record.</returns>
    public static virtual string GetFilename() { return default; }
    /// <summary>
    /// Get the app/website that uses this data. Only used if the type is RecordType.App or RecordType.Website. To make sure this is unique, we recommend putting the author before the name, e.g. "Passionfruit/Files" or "JohnDoe/example.com"
    /// </summary>
    /// <returns>The app/website that uses this data.</returns>
    public static virtual string GetAppName() { return default; }
}

/// <summary>
/// Determines how records are saved.
/// </summary>
public enum RecordType
{
    /// <summary>
    /// Global records are data that are the same for every user.
    /// </summary>
    Global,
    /// <summary>
    /// User records are data that are local to each user.
    /// </summary>
    User,
    /// <summary>
    /// App records are data that are local to the current user and an app.
    /// </summary>
    App,
    /// <summary>
    /// Website records are data that are local to the current user and an app.
    /// </summary>
    Website
}