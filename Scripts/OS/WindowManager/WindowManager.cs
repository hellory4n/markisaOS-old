using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node2D {
    PackedScene OpenWindow;
    Panel TextPeek;
    public static string TextPeekText = "";
    public static bool TextPeekVisible = false;

    public override void _Ready() {
        base._Ready();
        OpenWindow = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");

        // textpeek :omg:
        PackedScene pain = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Overlay/TextPeek.tscn");
        TextPeek = (Panel)pain.Instance();
        AddChild(TextPeek);
        TextPeek.RectMinSize = new Vector2(ResolutionManager.GetScreenSize().x, 40);
        TextPeek.RectPosition = new Vector2(69, 69);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (TextPeekVisible) {
            TextPeek.Visible = true;
            TextPeek.GetNode<LineEdit>("Text").Text = TextPeekText;
        } else {
            TextPeek.Visible = false;
        }
    }

    public void AddWindow(BaseWindow window) {
        Viewport lelsktop = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        lelsktop.AddChild(window);
        // using window.Popup_() makes it only work with 1 window, so this is a hack to bypass that
        window.Visible = true;

        // put it on the center of the screen
        Vector2 yes = ResolutionManager.GetScreenSize();
        window.RectPosition = yes/2 - (window.RectSize/2);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)OpenWindow.Instance();
        coolDockButton.Init(window);
        HBoxContainer dock = GetNode<HBoxContainer>("/root/LelsktopInterface/Dock/ScrollContainer/HBoxContainer");
        dock.AddChild(coolDockButton);

        // all windows are maximized by default on mobile
        if (OS.GetName() == "Android" && window.Resizable) {
            Vector2 maximizedSize = ResolutionManager.GetScreenSize();
            maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-160);
            window.RectPosition = new Vector2(0, 85);
            window.RectSize = maximizedSize;
        }
    }
}
