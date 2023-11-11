using Godot;
using System;

namespace Lelcore.Drivers;

public partial class ResourceManager : Node
{
    /// <summary>
    /// Loads an image from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the image (not in lelfs).</param>
    /// <returns>The image loaded.</returns>
    public static Texture2D LoadImage(string path)
    {
        if (path.StartsWith("res://"))
        {
            return ResourceLoader.Load<CompressedTexture2D>(path);
        }
        else
        {
            Image image = new();
            image.Load(path);
            ImageTexture texture = ImageTexture.CreateFromImage(image);
            return texture;
        }
    }

    /// <summary>
    /// Loads audio from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the audio (not in lelfs).</param>
    /// <returns>The audio loaded.</returns>
    public static AudioStream LoadAudio(string path)
    {
        /*if (path.StartsWith("res://"))
        {
            return ResourceLoader.Load<AudioStream>(path);
        }
        else
        {
            if (path.EndsWith(".ogg"))
            {
                var ohGeeGee = new AudioStreamOggVorbis();
                FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
                ohGeeGee. = file.GetBuffer((long)file.GetLength());
                file2.Close();
                return ohGeeGee;
            } else {
                GD.PushError("Invalid file!");
                return default;
            }
        }*/
        return ResourceLoader.Load<AudioStream>(path);
    }

    /// <summary>
    /// Loads a video from an specified path (not in lelfs).
    /// </summary>
    /// <param name="path">The path of the video (not in lelfs).</param>
    /// <returns>The video loaded.</returns>
    public static VideoStream LoadVideo(string path)
    {
        return ResourceLoader.Load<VideoStream>(path);
    }
}
