using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FileView : VBoxContainer {
    public string Path = "/";
    public PackedScene Folder = ResourceLoader.Load<PackedScene>("res://Apps/Files/Folder.tscn");
    public PackedScene File = ResourceLoader.Load<PackedScene>("res://Apps/Files/File.tscn");

    public override void _Ready() {
        base._Ready();
        Refresh();
    }

    public void Refresh() {
        GetNode<Label>("PathThing").Text = $"Path: {Path}";
        if (Path == "/") {
            // this does something
            var fart = LelfsManager.Paths.Where(kv => kv.Key.Count(c => c == '/') == 1)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var item in fart) {
                BaseLelfs pain = LelfsManager.Load<BaseLelfs>(item.Key);
                if (pain.Type == "Folder") {
                    Button jgkjfg = Folder.Instance<Button>();
                    jgkjfg.Text = pain.Name;
                    jgkjfg.HintTooltip = item.Key;
                    AddChild(jgkjfg);
                } else {
                    Button bkfnjng = File.Instance<Button>();
                    bkfnjng.Text = pain.Name;
                    bkfnjng.HintTooltip = item.Key;
                    AddChild(bkfnjng);
                }
            }
        }
    }
}
