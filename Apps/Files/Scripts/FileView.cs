using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Wm;
using Kickstart.Drivers;
using Dashboard.Toolkit;
using Kickstart.Cabinetfs;

namespace Files;

public partial class FileView : ItemList
{
    [Export]
    public Texture2D FolderIcon;
    [Export]
    public Texture2D FileIcon;
    [Export]
    public Texture2D MusicIcon;
    [Export]
    public Texture2D VideoIcon;
    [Export]
    public Texture2D TextIcon;
    List<string> CoolFiles = new();
    public string Path = "/";
    [Export]
    public MksTabRoot TabThing;
    public string Selected;
    public string ToCopy;
    [Export]
    public PopupMenu ContextMenuThing;
    List<string> Paths = new();
    int PathIndex = -1;
    public string ToCut;
    [Export]
    public Button CopyButton;
    [Export]
    public Button CutButton;
    [Export]
    public Button PasteButton;
    [Export]
    public Button DeleteButton;
    [Export]
    public Button RenameButton;
    [Export]
    public Button BackButton;
    [Export]
    public Button ForwardButton;
    [Export]
    public Button UpButton;
    [Export]
    public LineEdit PathEdit;

    public override void _Ready()
    {
        base._Ready();
        Refresh("/Home");
        /*Connect("item_selected", new Callable(this, nameof(FileSelected)));
        Connect("nothing_selected", new Callable(this, nameof(NothingSelected)));
        Connect("item_rmb_selected", new Callable(this, nameof(ContextMenu)));
        GetNode<LineEdit>("../Toolbar/Path").Connect("text_entered", new Callable(this, nameof(PathEdit)));
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
        GetNode<Button>("../FileOperations/Rename").Connect("pressed", new Callable(this, nameof(RenameFile)));*/
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        CopyButton.Disabled = Selected == null;
        CutButton.Disabled = Selected == null;
        PasteButton.Disabled = ToCopy == null && ToCut == null;
        DeleteButton.Disabled = Selected == null;
        RenameButton.Disabled = Selected == null;
        ContextMenuThing.SetItemDisabled(4, ToCopy == null && ToCut == null);
        BackButton.Disabled = PathIndex == 0;
        ForwardButton.Disabled = PathIndex == Paths.Count-1;
        UpButton.Disabled = Path == "/";

        // shortcuts :)
        /*if (Input.IsActionJustReleased("back") && TabThing.ThemeTypeVariation == "ActiveTab" && !GetNode<Button>("../Toolbar/Back").Disabled) {
            Back();
        }

        if (Input.IsActionJustReleased("forward") && TabThing.ThemeTypeVariation == "ActiveTab" && !GetNode<Button>("../Toolbar/Forward").Disabled) {
            Forward();
        }

        if (Input.IsActionJustReleased("up") && TabThing.ThemeTypeVariation == "ActiveTab" && Path != "/") {
            Up();
        }

        if (Input.IsActionJustReleased("refresh") && TabThing.ThemeTypeVariation == "ActiveTab") {
            Refresh(Path, false);
        }

        if (Input.IsActionJustReleased("copy") && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            CopyFile();
        }

        if (Input.IsActionJustReleased("cut") && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            MoveFile();
        }

        if (Input.IsActionJustReleased("paste") && TabThing.ThemeTypeVariation == "ActiveTab" && ToCopy != null && ToCut != null) {
            PasteFile();
        }

        if (Input.IsActionJustReleased("delete") && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            DeleteFile();
        }

        if (Input.IsActionJustReleased("rename")  && TabThing.ThemeTypeVariation == "ActiveTab" && Selected != null) {
            RenameFile();
        }

        if (Input.IsActionJustReleased("new_but_different") && GetParent().GetParent().GetParent().GetParent<MksWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab") {
            WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
            PackedScene m = GD.Load<PackedScene>("res://Apps/Files/NewFolder.tscn");
            NewFolder jjkn = m.Instantiate<NewFolder>();

            File dfggfdf = CabinetfsManager.LoadFile(Path);
            jjkn.Parent = dfggfdf.Id;

            jjkn.ThingThatINeedToRefresh = this;

            wm.AddWindow(jjkn);
        }

        if (Input.IsActionJustReleased("new") && GetParent().GetParent().GetParent().GetParent<MksWindow>()
        .IsActive() && TabThing.ThemeTypeVariation == "ActiveTab") {
            WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
            PackedScene m = GD.Load<PackedScene>("res://Apps/Files/NewFile.tscn");
            NewFile jjkn = m.Instantiate<NewFile>();

            // pain
            File dfggfdf = CabinetfsManager.LoadFile(Path);
            jjkn.Parent = dfggfdf.Id;

            jjkn.ThingThatINeedToRefresh = this;

            wm.AddWindow(jjkn);
        }*/
    }    

    public void Refresh(string pathThingSomething, bool addToHistory = true)
    {
        // forgor to check this lol
        if (!CabinetfsManager.PathExists(pathThingSomething))
            return;

        Path = pathThingSomething;
        PathEdit.Text = pathThingSomething;
        if (addToHistory)
        {
            Paths.Add(pathThingSomething);
            PathIndex = Paths.Count-1;
        }

        // couldn't be bothered to do this properly
        if (pathThingSomething == "/System/Trash")
        {
            TabThing.TabTitle = "Trash";
            PathEdit.Text = "Trash";
        }
        else if (pathThingSomething == "/")
            TabThing.TabTitle = "root";
        else
            TabThing.TabTitle = pathThingSomething.Split("/").Last();

        Clear();
        CoolFiles.Clear();

        // questionable way of sorting stuff :)
        File[] m = CabinetfsManager.GetFolderItems(Path);
        var sortedStuff = m
            .OrderByDescending(obj => obj.Type == "Folder")
            .ThenBy(obj => obj.Name)
            .ToArray();

        foreach (var pain in sortedStuff)
        {
            switch (pain.Type)
            {
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
                    if (pain.Data.ContainsKey("CoverArt"))
                    {
                        // yeah
                        AddItem(pain.Name, ResourceManager.LoadImage(
                            CabinetfsManager.LoadFile(pain.Data["CoverArt"].ToString())
                                .Data["Resource"].ToString()
                            )
                        );
                    } else
                        AddItem(pain.Name, MusicIcon);
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

    void FileSelected(int index)
    {
        File pain = CabinetfsManager.LoadFile(CoolFiles[index]);
        UpdateInspector(pain.Path);
        if (pain.Id == Selected)
            Open(index);
        
        Selected = pain.Id;
    }

    void Open(int index)
    {
        File pain = CabinetfsManager.LoadFile(CoolFiles[index]);
        switch (pain.Type)
        {
            case "Folder":
                Refresh(pain.Path);
                break;
            
            /*case "Picture":
                WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m = GD.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn = m.Instantiate<Observer>();
                jjkn.ObserverMode = Observer.Mode.Image;
                jjkn.MediaId = pain.Id;
                wm.AddWindow(jjkn);
                break;
            
            case "Audio":
                WindowManager wm1 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m1 = GD.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn1 = m1.Instantiate<Observer>();
                jjkn1.ObserverMode = Observer.Mode.Audio;
                jjkn1.MediaId = pain.Id;
                wm1.AddWindow(jjkn1);
                break;
            
            case "Video":
                WindowManager wm2 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m2 = GD.Load<PackedScene>("res://Apps/Observer/Observer.tscn");
                Observer jjkn2 = m2.Instantiate<Observer>();
                jjkn2.ObserverMode = Observer.Mode.Video;
                jjkn2.MediaId = pain.Id;
                wm2.AddWindow(jjkn2);
                break;
            
            case "Text":
                WindowManager wm3 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m3 = GD.Load<PackedScene>("res://Apps/Notebook/Notebook.tscn");
                var jjkn3 = m3.Instantiate<Notebook>();
                wm3.AddWindow(jjkn3);
                jjkn3.Suffer = true;
                jjkn3.Fhgkfdlkjgjkhgjf = pain.Id;
                break;*/
        }
    }

    void NothingSelected()
    {
        // updates the inspector to have information of the current folder :)
        UpdateInspector(Path);
        Selected = null;
    }

    void ContextMenu(int index, Vector2I position)
    {
        File pain = CabinetfsManager.LoadFile(CoolFiles[index]);
        UpdateInspector(pain.Path);
        Selected = pain.Id;
        ContextMenuThing.SetItemDisabled(3, false);
        ContextMenuThing.SetItemDisabled(5, false);
        ContextMenuThing.SetItemDisabled(6, false);
        ContextMenuThing.SetItemDisabled(7, false);
        ContextMenuThing.Position = (Vector2I)(GlobalPosition + position);
        ContextMenuThing.Popup();
    }

    void UpdateInspector(string path)
    {
        /*if (path == "/") {
            GetNode<Label>("../../Inspector/M/Label").Text = "My Computer";
            GetNode<Copy>("../../Inspector/M/CopyID").Visible = false;
            GetNode<Copy>("../../Inspector/M/CopyPath").Visible = false;
        } else {
            File nkbn = CabinetfsManager.LoadFile(path);
            GetNode<Label>("../../Inspector/M/Label").Text = $"{nkbn.Name}\nType: {nkbn.Type}\n\nMetadata:\n";
            foreach (var metadata in nkbn.Metadata) {
                GetNode<Label>("../../Inspector/M/Label").Text += $"{metadata.Key}: {metadata.Value}\n";
            }
            GetNode<Copy>("../../Inspector/M/CopyID").Visible = true;
            GetNode<Copy>("../../Inspector/M/CopyPath").Visible = true;
            GetNode<Copy>("../../Inspector/M/CopyID").TextToCopy = nkbn.Id;
            GetNode<Copy>("../../Inspector/M/CopyPath").TextToCopy = path;
        }*/
    }

    void CopyFile()
    {
        ToCopy = Selected;
    }

    void CutFile()
    {
        ToCut = Selected;
    }

    void PasteFile()
    {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Files/Paste.tscn");
        PasteFile jjkn = m.Instantiate<PasteFile>();

        // pain
        /*File dfggfdf = CabinetfsManager.LoadFile(Path);
        jjkn.Parent = dfggfdf.Id;

        jjkn.ThingThatINeedToRefresh = this;
        if (ToCut == null) {
            jjkn.OldFile = ToCopy;
            jjkn.Move = false;
        } else {
            jjkn.OldFile = ToCut;
            jjkn.Move = true;
        }*/

        wm.AddWindow(jjkn);
    }

    void ContextMenuSelected(int index)
    {
        switch (index)
        {
            case 0:
                WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m = GD.Load<PackedScene>("res://Apps/Files/NewFile.tscn");
                NewFile jjkn = m.Instantiate<NewFile>();

                // pain
                File dfggfdf = CabinetfsManager.LoadFile(Path);
                jjkn.Parent = dfggfdf.Id;
                jjkn.ThingThatINeedToRefresh = this;

                wm.AddWindow(jjkn);
                break;
            
            case 1:
                WindowManager wm1 = GetNode<WindowManager>("/root/WindowManager");
                PackedScene m1 = GD.Load<PackedScene>("res://Apps/Files/NewFolder.tscn");
                NewFolder jjkn1 = m1.Instantiate<NewFolder>();

                // pain
                File dfggfdf1 = CabinetfsManager.LoadFile(Path);
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
                CutFile();
                break;
            
            case 6:
                DeleteFile();
                break;
            
            case 7:
                RenameFile();
                break;
        }
    }

    void ContextMenuButDifferent(Vector2 position)
    {
        ContextMenuThing.SetItemDisabled(3, true);
        ContextMenuThing.SetItemDisabled(5, true);
        ContextMenuThing.SetItemDisabled(6, true);
        ContextMenuThing.SetItemDisabled(7, true);
        ContextMenuThing.Position = (Vector2I)(GlobalPosition + position);
        ContextMenuThing.Popup();
    }

    void Back()
    {
        PathIndex--;
        Refresh(Paths[PathIndex], false);
    }

    void Forward()
    {
        PathIndex++;
        Refresh(Paths[PathIndex], false);
    }

    void Up()
    {
        File currentThing = CabinetfsManager.LoadFile(Path);
        File g = CabinetfsManager.LoadFile(currentThing.Parent);
        Refresh(g.Path);
    }

    void RefreshButton()
    {
        Refresh(Path, false);
    }

    void DeleteFile()
    {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Files/Delete.tscn");
        Delete jjkn = m.Instantiate<Delete>();

        // pain
        File dfggfdf = CabinetfsManager.LoadFile(Path);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = this;
        jjkn.CoolFile = Selected;

        wm.AddWindow(jjkn);
    }

    void RenameFile()
    {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Files/Rename.tscn");
        Rename jjkn = m.Instantiate<Rename>();

        // pain
        File dfggfdf = CabinetfsManager.LoadFile(Path);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = this;
        jjkn.CoolFile = Selected;

        wm.AddWindow(jjkn);
    }
}
