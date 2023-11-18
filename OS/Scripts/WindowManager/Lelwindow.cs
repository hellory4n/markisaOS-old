using Godot;
using System;
using System.Collections.Generic;
using Lelcore.Drivers;
using Lelsktop.Interface;

namespace Lelsktop.Wm;

/// <summary>
/// A basic window. Adds window decorations, manages opening, closing, and minimizing animations, and also manages window snapping and making windows active.
/// </summary>
[GlobalClass]
public partial class Lelwindow : Window
{
	Vector2I ScreenSize;
	Vector2I PreviousPosition = new(0, 0);
	/// <summary>
	/// The icon used for the button on the dock. Recommended size is 40x40.
	/// </summary>
	[Export]
	public Texture2D Icon;
	bool CanSnap = false;
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
	/// This is used by the dock, don't touch it.
	/// </summary>
	public Vector2I PositionBeforeMinimizing;
	readonly Vector2I MinimizedPlace = new(69420, 69420);

	public override void _Ready()
	{
		base._Ready();
		ScreenSize = ResolutionManager.Resolution;

		// make it inherit the theme from the viewport
		Theme = null;

		// a window snapping just because your mouse was on the dock is quite inconvenient
		Timer jgjk = new()
        {
			Name = "jrgjdkggooghmgdgddgsaa39933",
			WaitTime = 0.5,
			Autostart = true,
			OneShot = true
		};
		jgjk.Timeout += () =>
		{
			CanSnap = true;
		};
		AddChild(jgjk);

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

		// :)
		GuiDisableInput = !HasFocus();

		// window snapping :)
		// is the window moving?
		if (PreviousPosition == Position || !CanSnap || Unresizable)
			return;
		
		MoveToForeground();
		
		// restore :)
		if (Size.Y == ScreenSize.Y-85)
		{
  	 	 	Size = PreviousSize;
			// so it doesn't immediately go back to its original state again lol
			return;
		}
		
		if (!Input.IsActionJustReleased("click"))
			return;

		if (GetTree().Root.GetMousePosition().Y < 80)
			Maximize();

		if (GetTree().Root.GetMousePosition().X < 40)
			SnapToLeft();

		if (GetTree().Root.GetMousePosition().X > ScreenSize.X-115)
			SnapToRight();

		PreviousPosition = Position;
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

	/// <summary>
	/// If true, the window will get minimized. If false, it will get restored.
	/// </summary>
	/// <param name="value">Value.</param>
	public void Minimize(bool value)
	{
		if (value)
		{
			PositionBeforeMinimizing = Position;
			Position = MinimizedPlace;
		}
		else
			Position = PositionBeforeMinimizing;
	}

	/// <summary>
	/// If true, the window is currently minimized.
	/// </summary>
	/// <returns>Whether or not the window is currently minimized.</returns>
	public bool IsMinimized()
	{
		return Position == MinimizedPlace;
	}
}
