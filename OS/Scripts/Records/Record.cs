using Godot;
using System;

namespace Kickstart.Records;

/// <summary>
/// Base class for markisaOS data. Use with RecordManager.
/// </summary>
public partial record Record
{
    /// <summary>
    /// The filename of the record. Automatically gets a folder path at the beginning.
    /// </summary>
	public string Filename;
    /// <summary>
    /// The type of the record.
    /// </summary>
    public RecordType Type;
    /// <summary>
    /// The app/website that uses this data. Only used if the type is RecordType.App or RecordType.Website. To make sure this is unique, we recommend putting the author before the name, e.g. "Passionfruit/Files" or "JohnDoe/example.com"
    /// </summary>
    public string AppName;
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