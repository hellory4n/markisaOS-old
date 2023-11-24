using Godot;
using System;

namespace Kickstart.Drivers;

public partial class ResourceManager : Node
{
    /// <summary>
    /// Loads an image from an specified path (not in cabinetfs).
    /// </summary>
    /// <param name="path">The path of the image (not in cabinetfs).</param>
    /// <returns>The image loaded.</returns>
    public static Texture2D LoadImage(string path)
    {
        if (path.StartsWith("res://"))
            return GD.Load<CompressedTexture2D>(path);
        else
        {
            Image image = new();
            image.Load(path);
            ImageTexture texture = ImageTexture.CreateFromImage(image);
            return texture;
        }
    }

    /// <summary>
    /// Loads audio from an specified path (not in cabinetfs).
    /// </summary>
    /// <param name="path">The path of the audio (not in cabinetfs).</param>
    /// <returns>The audio loaded.</returns>
    public static AudioStream LoadAudio(string path)
    {
        if (path.StartsWith("res://"))
            return GD.Load<AudioStream>(path);
        else
        {
            if (path.EndsWith(".ogg"))
            {
                using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
                var ohGeeGee = AudioStreamOggVorbis.LoadFromBuffer(file.GetBuffer((long)file.GetLength()));
                return ohGeeGee;
            }
            else
            {
                GD.PushError("Invalid file!");
                return default;
            }
        }
    }

    /// <summary>
    /// Loads a video from an specified path (not in cabinetfs).
    /// </summary>
    /// <param name="path">The path of the video (not in cabinetfs).</param>
    /// <returns>The video loaded.</returns>
    public static VideoStream LoadVideo(string path)
    {
        return GD.Load<VideoStream>(path);
    }
}
