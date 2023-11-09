using System;
using Godot;

/// <summary>
/// LelfsRoot is the root of Lelfs. The parent of all files. Its name is "" and its ID is "root".
/// </summary>
public partial class LelfsRoot : Folder {
    LelfsRoot() {
        Name = "";
        Id = "root";
        Path3D = "/";
        Parent = null;
        Type = "Root";
        LelfsManager.Paths.Clear();
        LelfsManager.Paths.Add("/", "root");
        LelfsManager.SavePaths();
    }

    /// <summary>
    /// I don't think you should copy root.
    /// </summary>
    /// <param name="name">What?</param>
    /// <param name="parent">This is literally the root of the filesystem.</param>
    /// <returns>An error.</returns>
    public override string Copy(string name, string parent = null) {
        GD.PushError("Can't copy root!");
        return default;
    }

    /// <summary>
    /// You can't rename root.
    /// </summary>
    /// <param name="name">Don't.</param>
    public override void Rename(string name) {
        GD.PushError("Can't rename root!");
    }

    /// <summary>
    /// You can't delete root, uninstall the game instead.
    /// </summary>
    public override void Delete() {
        GD.PushError("Can't delete root!");
    }

    /// <summary>
    /// You can't move root.
    /// </summary>
    /// <param name="parent">No.</param>
    public override void Move(string parent) {
        GD.PushError("Can't move root!");
    }

    /// <summary>
    /// Creates the root of the filesystem. Only use when making new users.
    /// </summary>
    public static void CreateRoot() {
        if (LelfsManager.FileExists("/")) {
            GD.PushError("Root already exists!");
            return;
        }

        LelfsRoot m = new LelfsRoot();
    }
}