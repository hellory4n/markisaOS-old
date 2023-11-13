using Godot;
using System;
using System.Collections.Generic;
using Lelcore.Drivers;

namespace Lelsktop.Wm;

/// <summary>
/// A basic window. Adds window decorations, manages opening, closing, and minimizing animations, and also manages window snapping and making windows active.
/// </summary>
[GlobalClass]
public partial class Lelwindow : Window
{
	Vector2I ScreenSize;
	Vector2I PreviousPosition = new(0, 0);
	AnimationPlayer Animation;
	/// <summary>
	/// The icon used for the button on the dock. Recommended size is 40x40.
	/// </summary>
	[Export]
	public Texture2D Icon;
	/// <summary>
	/// Used by the button on the dock to check if it should delete itself, as if it checked if the window was queued for deletion, there would be a little delay before it actually deleted the button.
	/// </summary>
	public bool IsClosing = false;
	/// <summary>
	/// Prevents the lelsktop from changing the theme of the window based on the settings the user chose.
	/// </summary>
	[Export]
	public bool CustomTheme = false;
	/// <summary>
	/// The maximize button of the window.
	/// </summary>
	public Button Maximize;
	/// <summary>
	/// The minimize button of the window.
	/// </summary>
	public Button Minimize;
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

	public override void _Ready()
	{
		base._Ready();
		ScreenSize = ResolutionManager.Resolution;

		// makes it use the theme from the viewport where all of the windows are located
		if (!CustomTheme)
			Theme = null;

		PackedScene maximize = GD.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
		Maximize = (Button)maximize.Instantiate();
		AddChild(Maximize);

		PackedScene minimize = GD.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
		Minimize = (Button)minimize.Instantiate();
		AddChild(Minimize);

		// epic animation for opening the window, very important indeed
		Animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Animation.Play("Open");

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
				// animation.Play("Close");
				QueueFree();
				IsClosing = true;
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

		// maximize
		if (GetTree().Root.GetMousePosition().Y < 80)
		{
			Vector2I newSize = new(ScreenSize.X-75, ScreenSize.Y-85);
			Position = new Vector2I(0, 85);
			PreviousSize = Size;
			Size = newSize;
		}

		// snap to left side
		if (GetTree().Root.GetMousePosition().X < 40)
		{
			Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
			Position = new Vector2I(0, 85);
			PreviousSize = Size;
			Size = newSize;
		}

		// snap to right side
		if (GetTree().Root.GetMousePosition().X > ScreenSize.X-115)
		{
			Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
			Position = new Vector2I((ScreenSize.Y-75)/2, 85);
			PreviousSize = Size;
			Size = newSize;
		}

		PreviousPosition = Position;
	}
}
