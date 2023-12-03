using Godot;
using System;
using System.Collections.Generic;

namespace Kickstart.Records;

public partial class DefaultAppGenerator5000
{
    public static List<Package> Generate()
    {
        return new List<Package> {
            new() {
                DisplayName = "Test App",
                Icon = "res://Assets/Themes/HighPeaks-Dark-Blue/Icons/App.png",
                Executable = "res://Apps/WindowManagerTest/WindowManagerTest.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Utilities }
            },
            new() {
                DisplayName = "Settings",
                Icon = "res://Assets/Themes/HighPeaks-Dark-Blue/Icons/Accessories.png",
                Executable = "res://Apps/Settings/Settings.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Accessories, Categories.System }
            },
            new() {
                DisplayName = "Files",
                Icon = "res://Apps/Files/Assets/IconSmall.png",
                Executable = "res://Apps/Files/Files.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Accessories }
            },
            new() {
                DisplayName = "Observer",
                Icon = "res://Apps/Observer/Assets/IconSmall.png",
                Executable = "res://Apps/Observer/Observer.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Utilities, Categories.Multimedia, Categories.Graphics }
            },
            new() {
                DisplayName = "Notebook",
                Icon = "res://Apps/Notebook/Assets/IconSmall.png",
                Executable = "res://Apps/Notebook/Notebook.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Utilities }
            },
            new() {
                DisplayName = "Calculator",
                Icon = "res://Apps/Calculator/Assets/IconSmall.png",
                Executable = "res://Apps/Calculator/Calculator.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Utilities }
            },
            new() {
                DisplayName = "Websites",
                Icon = "res://Apps/Websites/Assets/IconSmall.png",
                Executable = "res://Apps/Websites/Websites.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Internet }
            },
            new() {
                DisplayName = "Messages",
                Icon = "res://Apps/Messages/Assets/IconSmall.png",
                Executable = "res://Apps/Messages/Messages.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Internet }
            },
            new() {
                DisplayName = "Mines",
                Icon = "res://Apps/Mines/Assets/IconSmall.png",
                Executable = "res://Apps/Mines/Mines.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Games }
            },
            new() {
                DisplayName = "Sausage Clicker",
                Icon = "res://Apps/SausageClicker/Assets/IconSmall.png",
                Executable = "res://Apps/SausageClicker/SausageClicker.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Games }
            },
            new() {
                DisplayName = "Tour",
                Icon = "res://Apps/Tour/Assets/IconSmall.png",
                Executable = "res://Apps/Tour/Tour.tscn",
                Author = "Passionfruit Corporation",
                Categories = new Categories[] { Categories.Utilities }
            }
        };
    }
}