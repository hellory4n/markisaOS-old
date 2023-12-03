using Godot;
using System;

namespace Kickstart.Records;

/// <summary>
/// A package describes information about a markisaOS app.
/// </summary>
public partial struct Package
{
    /// <summary>
    /// The name that will be displayed to the user.
    /// </summary>
    public string DisplayName;
    /// <summary>
    /// The author of this app.
    /// </summary>
    public string Author;
    /// <summary>
    /// A path to the icon used by the app, with a 28x28 size.
    /// </summary>
    public string Icon;
    /// <summary>
    /// A path to the scene with the app, with a MksWindow as its root.
    /// </summary>
    public string Executable;
    /// <summary>
    /// A path to the scene with the uninstaller for this app. If null, this app cannot be uninstalled.
    /// </summary>
    public string Uninstaller;
    /// <summary>
    /// The categories of this app.
    /// </summary>
    public Categories[] Categories;
    /// <summary>
    /// The list of languages this language supports, with the format here: https://docs.godotengine.org/en/stable/tutorials/i18n/locales.html, ignored if the author is "Passionfruit".
    /// </summary>
    public string[] Languages;
}

public enum Categories
{
    /// <summary>
    /// Basically categorizing apps as "Other"
    /// </summary>
    Accessories,
    /// <summary>
    /// Apps used for modding and stuff.
    /// </summary>
    Development,
    /// <summary>
    /// Videogames.
    /// </summary>
    Games,
    /// <summary>
    /// Apps used for doing stuff with graphics.
    /// </summary>
    Graphics,
    /// <summary>
    /// Apps related to the world wide web.
    /// </summary>
    Internet,
    /// <summary>
    /// Apps that do stuff with media.
    /// </summary>
    Multimedia,
    /// <summary>
    /// Apps used in offices and stuff.
    /// </summary>
    Office,
    /// <summary>
    /// Apps that come with the system or something.
    /// </summary>
    System,
    /// <summary>
    /// Very small apps like a calculator and a photo viewer.
    /// </summary>
    Utilities
}