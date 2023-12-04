using Godot;
using System;
using System.Collections.Generic;
using Kickstart.Drivers;
using Dashboard.Interface;

namespace Dashboard.Wm;

/// <summary>
/// A basic window. Adds window decorations, manages opening, closing, and minimizing animations, and also manages window snapping and making windows active.
/// </summary>
[GlobalClass]
public partial class MksWindow : Window
{
	Vector2I ScreenSize;
	Vector2I PreviousPosition = new(0, 0);
	/// <summary>
	/// The icon used for the button on the dock. Recommended size is 40x40.
	/// </summary>
	[Export]
	public Texture2D Icon;
	[Export]
	public int CpuUse = 1;
	[Export]
	public int GpuUse = 1;
	[Export]
	public int MemoryUse = 1;
	[Export]
	public int StorageUse = 1;
	/// <summary>
	/// If false, close requests will automatically be handled by the window.
	/// </summary>
	[Export]
	public bool CustomCloseRequest = false;
	Vector2I PreviousSize;
	/// <summary>
	/// The button in dock corresponding to this window.
	/// </summary>
	public OpenWindowButton DockButton;

	public override void _Ready()
	{
		base._Ready();
		ScreenSize = ResolutionManager.Resolution;

		// make it inherit the theme from the viewport
		Theme = null;

		// so true
		if (!CustomCloseRequest)
		{
			CloseRequested += () =>
			{
				QueueFree();
				DockButton.QueueFree();
			};
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		/*PreviousPosition = Position;
		
		// is the window moving?
		if (PreviousPosition != Position || Unresizable)
			return;

		if (Input.IsActionJustReleased("click"))
		{
			if (GetTree().Root.GetMousePosition().X < 40)
				SnapToLeft();

			if (GetTree().Root.GetMousePosition().X > ScreenSize.X-115)
				SnapToRight();
				
			if (GetTree().Root.GetMousePosition().Y < 80)
				Maximize();
		}
		// restore :))
		else if (Input.IsActionJustPressed("click") && Size.Y == ScreenSize.Y-85)
			Size = PreviousSize;*/
	}

	/// <summary>
	/// Maximizes the window.
	/// </summary>
	public void Maximize()
	{
		PreviousSize = Size;
		Vector2I newSize = new(ScreenSize.X-75, ScreenSize.Y-85);
		Position = new Vector2I(0, 85);
		Size = newSize;
	}

	/// <summary>
	/// Snaps the window to the left side of the screen.
	/// </summary>
	public void SnapToLeft()
	{
		PreviousSize = Size;
		Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
		Position = new Vector2I(0, 85);
		Size = newSize;
	}

	/// <summary>
	/// Snaps the window to the right side of the screen.
	/// </summary>
	public void SnapToRight()
	{
		PreviousSize = Size;
		Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
		Position = new Vector2I((ScreenSize.X-75)/2, 85);
		Size = newSize;
	}
}
