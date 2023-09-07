using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FileView : VBoxContainer {
    public PackedScene Folder = ResourceLoader.Load<PackedScene>("res://Apps/Files/Folder.tscn");
    public PackedScene File = ResourceLoader.Load<PackedScene>("res://Apps/Files/File.tscn");

    public override void _Ready() {
        base._Ready();
        Refresh("/");
        GetNode<LineEdit>("PathThing").Connect("text_entered", this, nameof(Refresh));
    }

    public void Refresh(string pathThingSomething) {
        // clear previous list
        foreach (Node boom in GetChildren()) {
            if (boom.Name != "PathThing")
                boom.QueueFree();
        }

        GetNode<LineEdit>("PathThing").Text = pathThingSomething;

        if (pathThingSomething == "/") {
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
        } else {
            Folder nkbn = LelfsManager.Load<Folder>(pathThingSomething);
            foreach (var item in nkbn.Items) {
                BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(item);
                if (pain.Type == "Folder") {
                    Button jgkjfg = Folder.Instance<Button>();
                    jgkjfg.Text = pain.Name;
                    jgkjfg.HintTooltip = item;
                    AddChild(jgkjfg);
                } else {
                    Button bkfnjng = File.Instance<Button>();
                    bkfnjng.Text = pain.Name;
                    bkfnjng.HintTooltip = item;
                    AddChild(bkfnjng);
                }
            }
        }
    }
}
