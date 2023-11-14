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
	/// <summary>
	/// Used by the button on the dock to check if it should delete itself, as if it checked if the window was queued for deletion, there would be a little delay before it actually deleted the button.
	/// </summary>
	public bool IsClosing = false;
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
	public Vector2I Origin;

	public override void _Ready()
	{
		base._Ready();
		ScreenSize = ResolutionManager.Resolution;

		// awesome opening animation
		Random random = new();
		int side = random.Next(3);
		Origin = side switch
		{
			0 => new(0 - Size.X, random.Next(0, ScreenSize.Y)),
			1 => new(random.Next(0, ScreenSize.X), 0 - Size.Y),
			2 => new(random.Next(0, ScreenSize.X), ScreenSize.Y + Size.Y),
			_ => Vector2I.Zero
		};
		
		// put it on the center of the screen
        Vector2I finalPosition;
		// windows are maximized by default on mobile
		if (OS.GetName() == "Android" && !Unresizable)
		{
			finalPosition = new Vector2I(0, 85);
			Size = WindowManager.WindowSpace;
		}
		else
			finalPosition = WindowManager.WindowSpace/2 - (Size/2);

		Position = Origin;
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", finalPosition, 0.2).SetEase(Tween.EaseType.OutIn);

		// a window snapping just because your mouse was on the dock is quite inconvenient
		Timer jgjk = new()
        {
			Name = "jrgjdkggooghmgdgddgsaa39933",
			WaitTime = 0.2,
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
				Tween tween = CreateTween();
				tween.TweenProperty(this, "position", Origin, 0.2).SetEase(Tween.EaseType.OutIn);
				tween.Finished += () =>
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
		
		if (!Input.IsActionJustReleased("click"))
			return;

		// maximize
		if (GetTree().Root.GetMousePosition().Y < 80)
		{
			PreviousSize = Size;
			Vector2I newSize = new(ScreenSize.X-75, ScreenSize.Y-85);
			Position = new Vector2I(0, 85);
			Size = newSize;
		}

		// snap to left side
		if (GetTree().Root.GetMousePosition().X < 40)
		{
			PreviousSize = Size;
			Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
			Position = new Vector2I(0, 85);
			Size = newSize;
		}

		// snap to right side
		if (GetTree().Root.GetMousePosition().X > ScreenSize.X-115)
		{
			PreviousSize = Size;
			Vector2I newSize = new((ScreenSize.X-75)/2, ScreenSize.Y-85);
			Position = new Vector2I((ScreenSize.X-75)/2, 85);
			Size = newSize;
		}

		PreviousPosition = Position;
	}
}
