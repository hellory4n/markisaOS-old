using Godot;
using System;

public class FileImporter : BaseWindow {
    public string Parent;
    public FileView ThingThatINeedToRefresh;

    public override void _Ready() {
        base._Ready();
        GetNode<Button>("CenterContainer/VBoxContainer/Next").Connect("pressed", this, nameof(Click));
        GetNode<OptionButton>("CenterContainer/VBoxContainer/Options").Connect("item_selected", this, nameof(Thing));
        Thing(0);
    }

    public void Click() {
        // get the file format filter things :)
        // it didn't work
        FileDialog pain = GetNode<FileDialog>("Pain");
        /*int index = GetNode<OptionButton>("CenterContainer/VBoxContainer/Options").Selected;
        switch (index) {
            case 0:
                pain.Filters = new string[]{".png", ".jpeg", ".jpg", ".webp", ".svg"};
                break;
            case 1:
                pain.Filters = new string[]{".mp3", ".wav", ".ogg"};
                break;
            case 2:
                pain.Filters = new string[]{".ogv"};
                break;
        }*/

        // use the correct home folder
        if (OS.GetName() == "Android")
            pain.CurrentDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        else
            pain.CurrentDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);

        pain.Popup_();
        pain.RectPosition = new Vector2(0, 40);
        pain.RectSize = ResolutionManager.Resolution - new Vector2(75, 40);
        pain.Connect("file_selected", this, nameof(ImportFile));
    }

    public void Thing(int index) {
        if (index == 0) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label2").Text = "Supported formats: .png (no HDR), .jpeg/.jpg, .webp, .svg (limited support)";
            GetNode<HBoxContainer>("CenterContainer/VBoxContainer/VideoThing").Visible = false;
        } else if (index == 1) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label2").Text = "Supported formats: .ogg only, you can use websites to convert your files";
            GetNode<HBoxContainer>("CenterContainer/VBoxContainer/VideoThing").Visible = false;
        } else if (index == 2) {
            GetNode<Label>("CenterContainer/VBoxContainer/Label2").Text = "Supported formats: .ogv only due to technical limitations, you can use websites to convert your files";
            GetNode<HBoxContainer>("CenterContainer/VBoxContainer/VideoThing").Visible = true;
        }
    }

    public void ImportFile(string path) {
        // import file
        Directory dir = new Directory();
        dir.MakeDirRecursive("user://ImportedFiles/");
        string newPath = $"user://ImportedFiles/{LelfsManager.GenerateID()}.{StringExtensions.Extension(path)}";
        dir.Copy(path, newPath);

        // make the file in lelfs
        Random random = new Random();
        if (StringExtensions.Extension(newPath) == "png" || StringExtensions.Extension(newPath) == "jpg" ||
        StringExtensions.Extension(newPath) == "jpeg" || StringExtensions.Extension(newPath) == "webp" ||
        StringExtensions.Extension(newPath) == "svg") {
            LelfsFile file = LelfsManager.NewFile($"Picture {random.Next(0, 999999)}", Parent);
            file.Type = "Picture";
            file.Data.Add("Resource", newPath);
            file.Metadata.Add("CreationDate", DateTime.Now);
            file.Save();
        } else if (StringExtensions.Extension(newPath) == "mp3" ||
        StringExtensions.Extension(newPath) == "ogg" || StringExtensions.Extension(newPath) == "wav") {
            LelfsFile file = LelfsManager.NewFile($"Audio {random.Next(0, 999999)}", Parent);
            file.Type = "Audio";
            file.Data.Add("Resource", newPath);
            file.Metadata.Add("CreationDate", DateTime.Now);
            file.Save();
        } else if (StringExtensions.Extension(newPath) == "ogv") {
            LelfsFile file = LelfsManager.NewFile($"Video {random.Next(0, 999999)}", Parent);
            file.Type = "Video";
            file.Data.Add("Resource", newPath);
            file.Metadata.Add("CreationDate", DateTime.Now);
            file.Save();
        }

        Close();
        ThingThatINeedToRefresh.Refresh(ThingThatINeedToRefresh.Path, false);
    }
}
