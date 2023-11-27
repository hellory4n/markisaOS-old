using System;
using Godot;

namespace Kickstart.Cabinetfs;

/// <summary>
/// CabinetfsRoot is the root of Cabinetfs. The parent of all files. Its name is "" and its ID is "root".
/// </summary>
public partial class CabinetfsRoot : Folder
{
    CabinetfsRoot()
    {
        Name = "";
        Id = "root";
        Path = "/";
        Parent = null;
        Type = "Root";
        CabinetfsManager.Paths.Clear();
        CabinetfsManager.Paths.Add("/", "root");
        CabinetfsManager.SavePaths();
    }

    /// <summary>
    /// I don't think you should copy root.
    /// </summary>
    /// <param name="name">What?</param>
    /// <param name="parent">This is literally the root of the filesystem.</param>
    /// <returns>An error.</returns>
    public override Folder Copy(string name, string parent = null)
    {
        GD.PushError("Can't copy root!");
        return default;
    }

    /// <summary>
    /// You can't rename root.
    /// </summary>
    /// <param name="name">Don't.</param>
    public override void Rename(string name)
    {
        GD.PushError("Can't rename root!");
    }

    /// <summary>
    /// You can't delete root, uninstall the game instead.
    /// </summary>
    public override void Delete()
    {
        GD.PushError("Can't delete root!");
    }

    /// <summary>
    /// You can't move root.
    /// </summary>
    /// <param name="parent">No.</param>
    public override void Move(string parent)
    {
        GD.PushError("Can't move root!");
    }

    /// <summary>
    /// Creates the root of the filesystem. Only use when making new users.
    /// </summary>
    public static void CreateRoot()
    {
        if (CabinetfsManager.PathExists("/"))
        {
            GD.PushError("Root already exists!");
            return;
        }

        CabinetfsRoot m = new();
    }
}