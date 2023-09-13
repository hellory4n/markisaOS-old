using Godot;

public class Picture : BaseLelfs {
    public string PhotoPath;

    public Picture(
        string name,
        string photo,
        string parent = null,
        string author = "Unknown",
        string location = "Unknown",
        string description = "Photo",
        string camera = "Unknown",
        string claimRights = "Unknown"
    ) : base(name, parent) {
        PhotoPath = photo;
        Metadata.Add("Author", author);
        Metadata.Add("Location", location);
        Metadata.Add("Description", description);
        Metadata.Add("Camera", camera);
        Metadata.Add("ClaimRights", claimRights);
        Type = "Picture";
    }

    public Texture GetResource() {
        return ResourceLoader.Load<Texture>(PhotoPath);
    }
}

public class AnimatedPicture : BaseLelfs {
    public SpriteFrames Animation;

    public AnimatedPicture(
        string name,
        SpriteFrames animation,
        string parent = null,
        string author = "Unknown",
        string location = "Unknown",
        string description = "Photo",
        string camera = "Unknown",
        string claimRights = "Unknown"
    ) : base(name, parent) {
        Animation = animation;
        Metadata.Add("Author", author);
        Metadata.Add("Location", location);
        Metadata.Add("Description", description);
        Metadata.Add("Camera", camera);
        Metadata.Add("ClaimRights", claimRights);
        Type = "AnimatedPicture";
    }
}

public class Audio : BaseLelfs {
    public string AudioPath;
    public Picture CoverArt;

    public Audio(string name,
        string photo,
        string parent = null,
        string artist = "Unknown",
        string album = "None",
        string trackTitle = "Audio",
        string genre = "Audio",
        int trackNumber = 0,
        Picture coverArt = null
    ) : base(name, parent) {
        AudioPath = photo;
        Metadata.Add("Artist", artist);
        Metadata.Add("Album", album);
        Metadata.Add("TrackTitle", trackTitle);
        Metadata.Add("Genre", genre);
        Metadata.Add("TrackNumber", trackNumber);
        CoverArt = coverArt;
        Type = "Audio";
    }

    public AudioStream GetResource() {
        return ResourceLoader.Load<AudioStream>(AudioPath);
    }
}

public class Video : BaseLelfs {
    public string VideoPath;
    public Picture Thumbnail;

    public Video(
        string name,
        string parent = null,
        string author = "Unknown",
        string description = "Video",
        string location = "Unknown",
        string language = null,
        Picture thumbnail = null
    ) : base(name, parent) {
        Metadata.Add("Author", author);
        Metadata.Add("Description", description);
        Metadata.Add("Location", location);
        Metadata.Add("Language", language);
        Thumbnail = thumbnail;
        Type = "Video";
    }

    public VideoStream GetResource() {
        return ResourceLoader.Load<VideoStream>(VideoPath);
    }
}