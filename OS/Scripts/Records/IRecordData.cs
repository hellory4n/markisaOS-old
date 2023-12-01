using Godot;
using System;

namespace Kickstart.Records;

/// <summary>
/// Base class for markisaOS data. Use with Record. Implement this interface with a struct.
/// </summary>
public partial interface IRecordData
{
    /// <summary>
    /// Gets the filename of the record. If you're making your own apps then it's a good idea to put the author before the filename, so instead of <c>Data.json</c>, you would make <c>Author/Data.json</c> Examples:
    /// <list type="bullet">
    /// <item>Global data: <c>Settings/Data.json</c></item>
    /// <item>User data: <c>%user/Data.json</c></item>
    /// <item>App data: <c>%user/Apps/Data.json</c></item>
    /// <item>Website data: <c>%user/Web/example.com/Data.json</c></item>
    /// </list>
    /// </summary>
    /// <returns>The filename of the record.</returns>
    public abstract string GetFilename();
}
