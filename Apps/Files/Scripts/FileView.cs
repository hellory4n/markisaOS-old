using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class FileView : ItemList {
    readonly Texture2D FolderIcon = ResourceLoader.Load<Texture2D>("res://Apps/Files/Assets/IconDock.png");
    readonly Texture2D FileIcon = ResourceLoader.Load<Texture2D>("res://Apps/Files/Assets/File.png");
    readonly Texture2D MusicIcon = ResourceLoader.Load<Texture2D>("res://Apps/Files/Assets/Music.png");
    readonly Texture2D VideoIcon = ResourceLoader.Load<Texture2D>("res://Apps/Files/Assets/Video.png");
    readonly Texture2D TextIcon = ResourceLoader.Load<Texture2D>("res://Apps/Files/Assets/Text.png");
    List<string> CoolFiles = new List<string>();
    public string Path3D = "/";
    public Button TabThing;
    public string Selected;
    public string ToCopy;
    public PopupMenu ContextMenuThing;
    List<string> Paths = new List<string>();
    int PathIndex = -1;
    public string ToMove;

    public override void _Ready() {
        base._Ready();
        Refresh("/Home");
        Connect("item_selected", new Callable(this, nameof(ItemSelected)));
        Connect("nothing_selected", new Callable(this, nameof(NothingSelected)));
        Connect("item_rmb_selected", new Callable(this, nameof(ContextMenu)));
        GetNode<LineEdit>("../Toolbar/Path3D").Connect("text_entered", new Callable(this, nameof(PathEdit)));
        GetNode<Button>("../FileOperations/Copy").Connect("pressed", new Callable(this, nameof(CopyFile)));
        GetNode<Button>("../FileOperations/Paste").Connect("pressed", new Callable(this, nameof(PasteFile)));
        ContextMenuThing = GetNode<PopupMenu>("../ContextMenu");
        ContextMenuThing.Connect("index_pressed", new Callable(this, nameof(ContextMenuSelected)));
        Connect("rmb_clicked", new Callable(this, nameof(ContextMenuButDifferent)));
        GetNode<Button>("../Toolbar/Back").Connect("pressed", new Callable(this, nameof(Back)));
        GetNode<Button>("../Toolbar/Forward").Connect("pressed", new Callable(this, nameof(Forward)));
        GetNode<Button>("../Toolbar/Up").Connect("pressed", new Callable(this, nameof(Up)));
        GetNode<Button>("../Toolbar/Refresh").Connect("pressed", new Callable(this, nameof(RefreshButton)));
        GetNode<Button>("../FileOperations/Move").Connect("pressed", new Callable(this, nameof(MoveFile)));
        GetNode<Button>("../FileOperations/Delete").Connect("pressed", new Callable(this, nameof(DeleteFile)));
        GetNode<Button>("../FileOperations/Rename").Connect("pressed", new Callable(this, nameof(RenameFile)));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        GetNode<Button>("../FileOperations/Copy").Disabled = Selected == null;
        GetNode<Button>("../FileOperations/Move").Disabled = Selected == null;
        GetNode<Button>("../FileOperations/Paste").Disabled = ToCopy == null && ToMove == null;
        GetNode<Button>("../FileOperations/Delete").Disabled = Selected == null;
        GetNode<Button>("../FileOperations/Rename").Disabled = Selected == null;
        ContextMenuThing.SetItemDisabled(4, ToCopy == null && ToMove == null);
        GetNode<Button>("../Toolbar/Back").Disabled = PathIndex == 0;
        GetNode<Button>("../Toolbar/Forward").Disabled = PathIndex == Paths.Count-1;
        GetNode<Button>("../Toolbar/Up").Disabled = Path3D == "/";

        // shortcuts :)
        if (Input.IsActionJustReleased("back") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && !GetNode<Button>("../Toolbar/Back").Disabled) {
            Back();
        }

        if (Input.IsActionJustReleased("forward") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && !GetNode<Button>("../Toolbar/Forward").Disabled) {
            Forward();
        }

        if (Input.IsActionJustReleased("up") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && Path3D != "/") {
            Up();
        }

        if (Input.IsActionJustReleased("refresh") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab") {
            Refresh(Path3D, false);
        }

        if (Input.IsActionJustReleased("copy") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            CopyFile();
        }

        if (Input.IsActionJustReleased("cut") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            MoveFile();
        }

        if (Input.IsActionJustReleased("paste") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && ToCopy != null && ToMove != null) {
            PasteFile();
        }

        if (Input.IsActionJustReleased("delete") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            DeleteFile();
        }

        if (Input.IsActionJustReleased("rename") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            RenameFile();
        }

        if (Input.IsActionJustReleased("new_but_different") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab") {
            WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
            PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFolder.tscn");
            NewFolder jjkn = m.Instance<NewFolder>();

            LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
            jjkn.Parent = dfggfdf.Id;

            jjkn.ThingThatINeedToRefresh = this;

            wm.AddWindow(jjkn);
        }

        if (Input.IsActionJustReleased("new") && GetParent().GetParent().GetParent().GetParent<BaseWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab") {
            WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
            PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFile.tscn");
            NewFile jjkn = m.Instance<NewFile>();

            // pain
            LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
            jjkn.Parent = dfggfdf.Id;

            jjkn.ThingThatINeedToRefresh = this;

            wm.AddWindow(jjkn);
        }
    }    

    public void Refresh(string pathThingSomething, bool addToHistory = true) {
        // forgor to check this lol
        if (!LelfsManager.FileExists(pathThingSomething)) {
            return;
        }

        Path3D = pathThingSomething;
        GetNode<LineEdit>("../Toolbar/Path3D").Text = pathThingSomething;
        if (addToHistory) {
            Paths.Add(pathThingSomething);
            PathIndex = Paths.Count-1;
        }

        // couldn't be bothered to do this properly
        if (pathThingSomething == "/System/Trash") {
            TabThing.Text = "Trash";
            GetNode<LineEdit>("../Toolbar/Path3D").Text = "Trash";
        } else if (pathThingSomething == "/") {
            TabThing.Text = "root";
        } else {
            TabThing.Text = pathThingSomething.Split("/").Last();
        }

        // clear previous list :)))))
        for (int i = 0; i < Items.Count; i++) {
            RemoveItem(i);
            i--;
        }
        CoolFiles.Clear();

        // questionable way of sorting stuff :)
        LelfsFile[] m = LelfsManager.GetFolderItems(Path3D);
        var sortedStuff = m
            .OrderByDescending(obj => obj.Type == "Folder")
            .ThenBy(obj => obj.Name)
            .ToArray();

        foreach (var pain in sortedStuff) { 
            switch (pain.Type) {
                case "Folder":
                    AddItem(pain.Name, FolderIcon);
                    break;
                case "Picture":
                    if (pain.Data.ContainsKey("Resource"))
                        AddItem(pain.Name, ResourceManager.LoadImage(pain.Data["Resource"].ToString()));
                    else
                        AddItem(pain.Name, FileIcon);
                    break;
                case "Audio":
                    if (pain.Data.ContainsKey("CoverArt")) {
                        // yeah
                        AddItem(pain.Name, ResourceManager.LoadImage(
                            LelfsManager.LoadById<LelfsFile>(pain.Data["CoverArt"].ToString())
                                .Data["Resource"].ToString()
                            )
                        );
                    } else {
                        AddItem(pain.Name, MusicIcon);
                    }
                    break;
                case "Video":
                    AddItem(pain.Name, VideoIcon);
                    break;
                case "Text":
                    AddItem(pain.Name, TextIcon);
                    break;
                default:
                    AddItem(pain.Name, FileIcon);
                    break;
            }

            CoolFiles.Add(pain.Id);
        }

        UpdateInspector(pathThingSomething);
    }

    void ItemSelected(int index) {
        LelfsFile pain = LelfsManager.LoadById<LelfsFile>(CoolFiles[index]);
        UpdateInspector(pain.Path3D);
        if (pain.Id == Selected) {
            Open(index);
        }
        Selected = pain.Id;
    }

    void Open(int index) {
        LelfsFile pain = LelfsManager.LoadById<LelfsFile>(CoolFiles[index]);
        switch (pain.Type) {
            case "Folder":
                Refresh(pain.Path3D);
                break;
            case "Picture":
                WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn = m.Instance<Observer>();
                jjkn.ObserverMode = Observer.Mode.Image;
                jjkn.MediaId = pain.Id;
                wm.AddWindow(jjkn);
                break;
            case "Audio":
                WindowManager wm1 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m1 = ResourceLoader.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn1 = m1.Instance<Observer>();
                jjkn1.ObserverMode = Observer.Mode.Audio;
                jjkn1.MediaId = pain.Id;
                wm1.AddWindow(jjkn1);
                break;
            case "Video":
                WindowManager wm2 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m2 = ResourceLoader.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn2 = m2.Instance<Observer>();
                jjkn2.ObserverMode = Observer.Mode.Video;
                jjkn2.MediaId = pain.Id;
                wm2.AddWindow(jjkn2);
                break;
            case "Text":
                WindowManager wm3 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m3 = ResourceLoader.Load<PackedScene>("res://Apps/Notebook/Notebook.tscn");
                var jjkn3 = m3.Instance<Notebook>();
                wm3.AddWindow(jjkn3);
                jjkn3.Suffer = true;
                jjkn3.Fhgkfdlkjgjkhgjf = pain.Id;
                break;
        }
    }

    void NothingSelected() {
        // updates the inspector to have information of the current folder :)
        UpdateInspector(Path3D);
        Selected = null;
    }

    void PathEdit(string path) {
        if (LelfsManager.FileExists(path)) {
            LelfsFile m = LelfsManager.Load<LelfsFile>(path);
            if (m.Type == "Folder") {
                Refresh(path);
            }
        }
    }

    void ContextMenu(int index, Vector2 position) {
        LelfsFile pain = LelfsManager.LoadById<LelfsFile>(CoolFiles[index]);
        UpdateInspector(pain.Path3D);
        Selected = pain.Id;
        ContextMenuThing.SetItemDisabled(3, false);
        ContextMenuThing.SetItemDisabled(5, false);
        ContextMenuThing.SetItemDisabled(6, false);
        ContextMenuThing.SetItemDisabled(7, false);
        ContextMenuThing.Position = GlobalPosition + position;
        ContextMenuThing.Popup_();
    }

    void UpdateInspector(string path) {
        if (path == "/") {
            GetNode<Label>("../../Inspector/M/Label").Text = "My Computer";
            GetNode<Copy>("../../Inspector/M/CopyID").Visible = false;
            GetNode<Copy>("../../Inspector/M/CopyPath").Visible = false;
        } else {
            LelfsFile nkbn = LelfsManager.Load<LelfsFile>(path);
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

    void CopyFile() {
        ToCopy = Selected;
    }

    void MoveFile() {
        ToMove = Selected;
    }

    void PasteFile() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/Paste.tscn");
        PasteFile jjkn = m.Instance<PasteFile>();

        // pain
        LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
        jjkn.Parent = dfggfdf.Id;

        jjkn.ThingThatINeedToRefresh = this;
        if (ToMove == null) {
            jjkn.OldFile = ToCopy;
            jjkn.Move = false;
        } else {
            jjkn.OldFile = ToMove;
            jjkn.Move = true;
        }

        wm.AddWindow(jjkn);
    }

    void ContextMenuSelected(int index) {
        switch (index) {
            case 0:
                WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFile.tscn");
                NewFile jjkn = m.Instance<NewFile>();

                // pain
                LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
                jjkn.Parent = dfggfdf.Id;
                jjkn.ThingThatINeedToRefresh = this;

                wm.AddWindow(jjkn);
                break;
            case 1:
                WindowManager wm1 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m1 = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFolder.tscn");
                NewFolder jjkn1 = m1.Instance<NewFolder>();

                // pain
                LelfsFile dfggfdf1 = LelfsManager.Load<LelfsFile>(Path3D);
                jjkn1.Parent = dfggfdf1.Id;
                jjkn1.ThingThatINeedToRefresh = this;

                wm1.AddWindow(jjkn1);
                break;
            case 3:
                CopyFile();
                break;
            case 4:
                PasteFile();
                break;
            case 5:
                MoveFile();
                break;
            case 6:
                DeleteFile();
                break;
            case 7:
                RenameFile();
                break;
        }
    }

    void ContextMenuButDifferent(Vector2 position) {
        ContextMenuThing.SetItemDisabled(3, true);
        ContextMenuThing.SetItemDisabled(5, true);
        ContextMenuThing.SetItemDisabled(6, true);
        ContextMenuThing.SetItemDisabled(7, true);
        ContextMenuThing.Position = GlobalPosition + position;
        ContextMenuThing.Popup_();
    }

    void Back() {
        PathIndex--;
        Refresh(Paths[PathIndex], false);
    }

    void Forward() {
        PathIndex++;
        Refresh(Paths[PathIndex], false);
    }

    void Up() {
        LelfsFile currentThing = LelfsManager.Load<LelfsFile>(Path3D);
        LelfsFile g = LelfsManager.LoadById<LelfsFile>(currentThing.Parent);
        Refresh(g.Path3D);
    }

    void RefreshButton() {
        Refresh(Path3D, false);
    }

    void DeleteFile() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/Delete.tscn");
        Delete jjkn = m.Instance<Delete>();

        // pain
        LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = this;
        jjkn.CoolFile = Selected;

        wm.AddWindow(jjkn);
    }

    void RenameFile() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/Rename.tscn");
        Rename jjkn = m.Instance<Rename>();

        // pain
        LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(Path3D);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = this;
        jjkn.CoolFile = Selected;

        wm.AddWindow(jjkn);
    }
}
