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
	/// <summary>
	/// 0 = not snapped, 1 = maximized, 2 = snap left, 3 = snap right
	/// </summary>
	int SnapStateThingy = 0;

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

		// epic window snapping bonanza
		if (Unresizable || !HasFocus())
			return;
		
		int newSnapStateThingy = -1;
		if (Input.IsActionJustReleased("Maximize")) newSnapStateThingy = 1;
		if (Input.IsActionJustReleased("SnapLeft")) newSnapStateThingy = 2;
		if (Input.IsActionJustReleased("SnapRight")) newSnapStateThingy = 3;

		if (newSnapStateThingy == -1)
			return;

		if (newSnapStateThingy == SnapStateThingy)
		{
			Size = PreviousSize;
			Position = PreviousPosition;
			SnapStateThingy = 0;
		}
		else
		{
			if (newSnapStateThingy == 1) Maximize();
			if (newSnapStateThingy == 2) SnapToLeft();
			if (newSnapStateThingy == 3) SnapToRight();
		}
	}

	/// <summary>
	/// Maximizes the window.
	/// </summary>
	public void Maximize()
	{
		PreviousSize = Size;
		PreviousPosition = Position;
		Vector2I newSize = new(ScreenSize.X-75, ScreenSize.Y-85);
		Position = new Vector2I(0, 85);
		Size = newSize;
		SnapStateThingy = 1;
	}

	/// <summary>
	/// Snaps the window to the left side of the screen.
	/// </summary>
	public void SnapToLeft()
	{
		PreviousSize = Size;
		PreviousPosition = Position;
		Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
		Position = new Vector2I(0, 85);
		Size = newSize;
		SnapStateThingy = 2;
	}

	/// <summary>
	/// Snaps the window to the right side of the screen.
	/// </summary>
	public void SnapToRight()
	{
		PreviousSize = Size;
		PreviousPosition = Position;
		Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
		Position = new Vector2I((ScreenSize.X-75)/2, 85);
		Size = newSize;
		SnapStateThingy = 3;
	}
}
