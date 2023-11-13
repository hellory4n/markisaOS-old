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
	Vector2 screenSize;
	Vector2 previousPosition = new(0, 0);
	AnimationPlayer animation;
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

	public override void _Ready()
	{
		base._Ready();
		screenSize = ResolutionManager.Resolution;

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
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		animation.Play("Open");

		Timer jgjk = new()
        {
			Name = "jrgjdkggooghmgdgddgsaa39933",
			WaitTime = 0.5f,
			Autostart = true,
			OneShot = true
		};
		jgjk.Connect("timeout", new Callable(this, nameof(SnapThing)));
		AddChild(jgjk);

		// so true
		if (!CustomCloseRequest) {
			CloseRequested += () => {
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
		// first check if the window is moving
		/*if (previousPosition != Position && Resizable) {
			Raise();
			if (GetGlobalMousePosition().y < 60 && CanSnap) {
				Vector2 maximizedSize = new(screenSize.x-75, screenSize.y-85);
				Position = new Vector2(0, 85);
				Size = maximizedSize;
			}

			if (GetGlobalMousePosition().x < 40 && CanSnap) {
				Vector2 newSize = new((screenSize.x-75)/2, screenSize.y-85);
				Position = new Vector2(0, 85);
				Size = newSize;
			}

			if (GetGlobalMousePosition().x > screenSize.x-115 && CanSnap) {
				Vector2 newSize = new((screenSize.x-75)/2, screenSize.y-85);
				Position = new Vector2((screenSize.x-75)/2, 85);
				Size = newSize;
			}
		}

		previousPosition = Position;*/
	}

	void Close()
	{
		animation.Play("Close");
		IsClosing = true;
	}

	// so the window doesn't get snapped just because your mouse was in the dock when you opened it
	void SnapThing()
	{
		CanSnap = true;
	}
}
