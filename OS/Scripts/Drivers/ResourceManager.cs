using Godot;
using System;

public partial class ResourceManager : Node {
    /// <summary>
    /// Loads an image from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the image (not in lelfs).</param>
    /// <returns>The image loaded.</returns>
    public static Texture2D LoadImage(string path) {
        if (path.StartsWith("res://")) {
            return ResourceLoader.Load<CompressedTexture2D>(path);
        } else {
            Image image = new Image();
            image.Load(path);
            ImageTexture texture = new ImageTexture();
            texture.CreateFromImage(image);
            return texture;
        }
    }

    /// <summary>
    /// Loads audio from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the audio (not in lelfs).</param>
    /// <returns>The audio loaded.</returns>
    public static AudioStream LoadAudio(string path) {
        if (path.StartsWith("res://")) {
            return ResourceLoader.Load<AudioStream>(path);
        } else {
            if (path.EndsWith(".ogg")) {
                var ohGeeGee = new AudioStreamOggVorbis();
                File file2 = new File();
                file2.Open(path, File.ModeFlags.Read);
                ohGeeGee.Data = file2.GetBuffer((long)file2.GetLength());
                file2.Close();
                return ohGeeGee;
            } else {
                GD.PushError("Invalid file!");
                return default;
            }
        }
    }

    /// <summary>
    /// Loads a video from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the video (not in lelfs).</param>
    /// <returns>The video loaded.</returns>
    public static VideoStream LoadVideo(string path) {
        return ResourceLoader.Load<VideoStream>(path);
    }
}
