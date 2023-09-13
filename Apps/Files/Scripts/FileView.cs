using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FileView : ItemList {
    Texture FolderIcon = ResourceLoader.Load<Texture>("res://Apps/Files/Assets/IconDock.png");
    List<string> CoolFiles = new List<string>();

    public override void _Ready() {
        base._Ready();
        Refresh("/");
        Connect("item_selected", this, nameof(ItemSelected));
        Connect("item_activated", this, nameof(Open));
    }

    void Refresh(string pathThingSomething) {
        // clear previous list :)))))
        for (int i = 0; i < Items.Count; i++) {
            RemoveItem(i);
            i--;
        }
        CoolFiles.Clear();

        if (pathThingSomething == "/") {
            // this does something
            var fart = LelfsManager.Paths.Where(kv => kv.Key.Count(c => c == '/') == 1)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var item in fart) {
                BaseLelfs pain = LelfsManager.Load<BaseLelfs>(item.Key);
                AddItem(pain.Name, FolderIcon);
                CoolFiles.Add(pain.Id);
            }
        } else {
            Folder nkbn = LelfsManager.Load<Folder>(pathThingSomething);
            foreach (var item in nkbn.Items) {
                BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(item);
                AddItem(pain.Name);
                CoolFiles.Add(pain.Id);
            }
        }
    }

    void ItemSelected(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);
        GD.Print($"Item \"{pain.Path}\" selected");
    }

    void Open(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);
        if (pain.Type == "Folder") {
            Refresh(pain.Path);
        }
        // TODO: add a thing that opens files :)
    }
}
