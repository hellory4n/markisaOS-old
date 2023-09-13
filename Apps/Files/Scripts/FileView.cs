using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FileView : ItemList {
    readonly Texture FolderIcon = ResourceLoader.Load<Texture>("res://Apps/Files/Assets/IconDock.png");
    readonly Texture FileIcon = ResourceLoader.Load<Texture>("res://Apps/Files/Assets/File.png");
    List<string> CoolFiles = new List<string>();
    string Path = "/";

    public override void _Ready() {
        base._Ready();
        Refresh("/");
        Connect("item_selected", this, nameof(ItemSelected));
        Connect("item_activated", this, nameof(Open));
        Connect("nothing_selected", this, nameof(NothingSelected));
        GetNode<LineEdit>("../Toolbar/Path").Connect("text_entered", this, nameof(PathEdit));
    }

    void Refresh(string pathThingSomething) {
        Path = pathThingSomething;

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
                switch (pain.Type) {
                    case "Folder":
                        AddItem(pain.Name, FolderIcon);
                        break;
                    case "Picture":
                        Picture m = (Picture)pain;
                        AddItem(pain.Name, m.GetResource());
                        break;
                    default:
                        AddItem(pain.Name, FileIcon);
                        break;
                }
                
                CoolFiles.Add(pain.Id);
            }

            // update the inspector
            GetNode<Label>("../../Inspector/Label").Text = "My Computer";
        } else {
            Folder nkbn = LelfsManager.Load<Folder>(pathThingSomething);
            foreach (var item in nkbn.Items) {
                BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(item);
                switch (pain.Type) {
                    case "Folder":
                        AddItem(pain.Name, FolderIcon);
                        break;
                    case "Picture":
                        Picture m = LelfsManager.LoadById<Picture>(item);
                        AddItem(pain.Name, m.GetResource());
                        break;
                    default:
                        AddItem(pain.Name, FileIcon);
                        break;
                }

                CoolFiles.Add(pain.Id);
            }

            // update the inspector
            GetNode<Label>("../../Inspector/Label").Text = $"{nkbn.Name}\nPath: {nkbn.Path}\nID: {nkbn.Id}\nParent ID: {nkbn.Parent}\nType: {nkbn.Type}\n\nMetadata:\n";
            foreach (var metadata in nkbn.Metadata) {
                GetNode<Label>("../../Inspector/Label").Text += $"{metadata.Key}: {metadata.Value}\n";
            }
        }
    }

    void ItemSelected(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);

        // update the inspector
        GetNode<Label>("../../Inspector/Label").Text = $"{pain.Name}\nPath: {pain.Path}\nID: {pain.Id}\nParent ID: {pain.Parent}\nType: {pain.Type}\n\nMetadata:\n";
        foreach (var metadata in pain.Metadata) {
            GetNode<Label>("../../Inspector/Label").Text += $"{metadata.Key}: {metadata.Value}\n";
        }
    }

    void Open(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);
        GetNode<LineEdit>("../Toolbar/Path").Text = pain.Path;
        if (pain.Type == "Folder") {
            Refresh(pain.Path);
        }
        // TODO: add a thing that opens files :)
    }

    void NothingSelected() {
        // updates the inspector to have information of the current folder
        if (Path == "/") {
            GetNode<Label>("../../Inspector/Label").Text = "My Computer";
        } else {
            Folder nkbn = LelfsManager.Load<Folder>(Path);
            GetNode<Label>("../../Inspector/Label").Text = $"{nkbn.Name}\nPath: {nkbn.Path}\nID: {nkbn.Id}\nParent ID: {nkbn.Parent}\nType: {nkbn.Type}\n\nMetadata:\n";
            foreach (var metadata in nkbn.Metadata) {
                GetNode<Label>("../../Inspector/Label").Text += $"{metadata.Key}: {metadata.Value}\n";
            }
        }
    }

    void PathEdit(string path) {
        if (path == "/") {
            Refresh(path);
            return;
        }

        if (LelfsManager.FileExists(path)) {
            BaseLelfs m = LelfsManager.Load<BaseLelfs>(path);
            if (m.Type == "Folder") {
                Refresh(Path);
            }
        }
    }
}
