using Godot;
using System;
using Lelcore.Drivers;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class ImageBackground : Sprite2D
{
	[Export]
	public Vector2 OriginalSize = new(0, 0);

	public override void _Ready()
	{
		base._Ready();
		Vector2 screenSize = ResolutionManager.Resolution;

		// first scale the image
		float scale;
		if (screenSize > OriginalSize)
		{
			scale = (Mathf.Max(screenSize.X, screenSize.Y) - Mathf.Max(OriginalSize.X, OriginalSize.Y)) /
				Mathf.Max(OriginalSize.X, OriginalSize.Y);
			scale += 1;
		}
		else
		{
			scale = Mathf.Max(screenSize.X, screenSize.Y) / Mathf.Max(OriginalSize.X, OriginalSize.Y);
		}

		Scale = new Vector2(scale, scale);

		// then put it in the center of the screen
		Position = screenSize/2;
	}
}
