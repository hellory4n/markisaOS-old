using Godot;
using System;

namespace Kickstart.Records;

/// <summary>
/// Stores user info.
/// </summary>
record MarkisaUser : Record
{
    public MarkisaUser()
    {
        Filename = "MarkisaUser";
        Type = RecordType.User;
    }

    /// <summary>
    /// The major version, incremented with really big updates that break a lot of stuff.
    /// </summary>
    public uint Major = 0;
    /// <summary>
    /// The minor version, incremented with smaller updates that add backwards-compatible features. Minor versions are supposed to work with the 3 minor/major versions before it. Resets when the major version is incremented.
    /// </summary>
    public uint Minor = 11;
    /// <summary>
    /// The patch version, incremented with very small updates only intended to fix bugs and stuff. Resets when the major or minor version is incremented.
    /// </summary>
    public uint Patch = 0;
    /// <summary>
    /// The build number, corresponds to the total amount of git commits as of the time of the update's release.
    /// </summary>
    public uint Build = 360;
    /// <summary>
    /// If true, this is a beta version.
    /// </summary>
    public bool Beta = true;

    /// <summary>
    /// The name displayed for the user, not the one used for saving the user's data.
    /// </summary>
    public string DisplayName = "John Doe";
    /// <summary>
    /// The name used when saving user data. Also used on the web, where it gets the @ symbol in front of it.
    /// </summary>
    public string Username = "john.doe";
    /// <summary>
    /// A path (not in cabinetfs) pointing to the photo the user uses.
    /// </summary>
    public string Photo;

    /// <summary>
    /// Converts the version the user is currently using into a string.
    /// </summary>
    /// <returns>A string with the format "vMAJOR.MINOR.PATCH.BUILD" + "(Beta)" at the end if it is one.</returns>
    public string VersionToString()
    {
        if (Beta)
            return $"v{Major}.{Minor}.{Patch}.{Build} (Beta)";
        else
            return $"v{Major}.{Minor}.{Patch}.{Build}";
    }
}
