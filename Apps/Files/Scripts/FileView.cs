using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class FileView : ItemList {
    readonly Texture FolderIcon = ResourceLoader.Load<Texture>("res://Apps/Files/Assets/IconDock.png");
    readonly Texture FileIcon = ResourceLoader.Load<Texture>("res://Apps/Files/Assets/File.png");
    List<string> CoolFiles = new List<string>();
    public string Path = "/";
    public Button TabThing;

    public override void _Ready() {
        base._Ready();
        Refresh("/");
        Connect("item_selected", this, nameof(ItemSelected));
        Connect("item_activated", this, nameof(Open));
        Connect("nothing_selected", this, nameof(NothingSelected));
        Connect("item_rmb_selected", this, nameof(ContextMenu));
        GetNode<LineEdit>("../Toolbar/Path").Connect("text_entered", this, nameof(PathEdit));
    }

    public void Refresh(string pathThingSomething) {
        Path = pathThingSomething;
        // couldn't be bothered to do this properly
        if (pathThingSomething != "/")
            TabThing.Text = pathThingSomething.Split("/").Last();
        else
            TabThing.Text = "/";

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

            UpdateInspector("/");
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

            UpdateInspector(pathThingSomething);
        }
    }

    void ItemSelected(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);

        // update the inspector
        UpdateInspector(pain.Path);
    }

    void Open(int index) {
        BaseLelfs pain = LelfsManager.LoadById<BaseLelfs>(CoolFiles[index]);
        if (pain.Type == "Folder") {
            GetNode<LineEdit>("../Toolbar/Path").Text = pain.Path;
            Refresh(pain.Path);
        }
        // TODO: add a thing that opens files :)
    }

    void NothingSelected() {
        // updates the inspector to have information of the current folder :)
        UpdateInspector(Path);
    }

    void PathEdit(string path) {
        if (path == "/") {
            Refresh(path);
            return;
        }

        if (LelfsManager.FileExists(path)) {
            BaseLelfs m = LelfsManager.Load<BaseLelfs>(path);
            if (m.Type == "Folder") {
                Refresh(path);
            }
        }
    }

    void ContextMenu(int index, Vector2 position) {
        PopupMenu yes = new PopupMenu {
            RectPosition = GetParent<Control>().RectGlobalPosition + position
        };
        yes.AddItem("Cool Item 1");
        yes.AddItem("Cool Item 2");
        yes.AddItem("Cool Item 3");
        yes.AddItem("Cool Item 4");
        GetParent().AddChild(yes);
        yes.Popup_();
    }

    void UpdateInspector(string path) {
        if (path == "/") {
            GetNode<Label>("../../Inspector/M/Label").Text = "My Computer";
            GetNode<Copy>("../../Inspector/M/CopyID").Visible = false;
            GetNode<Copy>("../../Inspector/M/CopyPath").Visible = false;
        } else {
            BaseLelfs nkbn = LelfsManager.Load<BaseLelfs>(path);
            GetNode<Label>("../../Inspector/M/Label").Text = $"{nkbn.Name}\nType: {nkbn.Type}\n\nMetadata:\n";
            foreach (var metadata in nkbn.Metadata) {
                GetNode<Label>("../../Inspector/M/Label").Text += $"{metadata.Key}: {metadata.Value}\n";
            }
            GetNode<Copy>("../../Inspector/M/CopyID").Visible = true;
            GetNode<Copy>("../../Inspector/M/CopyPath").Visible = true;
            GetNode<Copy>("../../Inspector/M/CopyID").TextToCopy = nkbn.Id;
            GetNode<Copy>("../../Inspector/M/CopyPath").TextToCopy = path;
        }
    }
}
