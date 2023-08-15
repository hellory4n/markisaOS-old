using Godot;
using System;

public class Maximize : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        KinematicBody2D window = (KinematicBody2D)GetParent();
        Vector2 maximizedSize = ResolutionManager.GetScreenSize();
        Control content = GetNode<Control>("../Content");

        // check if the window is maximized
        if (window.Position != new Vector2(0, 0) && content.RectSize - new Vector2(0, 45) != maximizedSize) {
            window.Position = new Vector2(0, 0);
            content.RectSize = maximizedSize - new Vector2(0, 45);
            CollisionShape2D m = GetNode<CollisionShape2D>($"../EpicCollision");
            m.Shape = new RectangleShape2D {
                Extents = new Vector2(maximizedSize.x, 45)
            };
            m.Position = new Vector2(maximizedSize.x, 45);
            GetNode<Label>("../Name").RectSize = new Vector2(maximizedSize.x, 45);
            GetNode<TextureButton>("../Close").RectPosition = new Vector2(maximizedSize.x-36, 6);
            GetNode<Sprite>("../Pain").Scale = new Vector2(0.008f, 0.008f) * new Vector2(maximizedSize.x-18, 45);
            GetNode<KinematicBody2D>("../Resize").Position = new Vector2(maximizedSize.x-36, maximizedSize.y-36);
            RectPosition = new Vector2(maximizedSize.x-82, 6);
        } else {
            content.RectSize = content.RectMinSize;
            window.Position = new Vector2(150, 150);
            CollisionShape2D m = GetNode<CollisionShape2D>($"../EpicCollision");
            m.Shape = new RectangleShape2D {
                Extents = new Vector2(content.RectMinSize.x, 45)
            };
            m.Position = new Vector2(content.RectMinSize.x, 45);
            GetNode<Label>("../Name").RectSize = new Vector2(content.RectMinSize.x, 45);
            GetNode<TextureButton>("../Close").RectPosition = new Vector2(content.RectMinSize.x-36, 6);
            GetNode<Sprite>("../Pain").Scale = new Vector2(0.008f, 0.008f) * new Vector2(content.RectMinSize.x-18, 45);
            GetNode<KinematicBody2D>("../Resize").Position = new Vector2(content.RectMinSize.x+36, content.RectMinSize.y+36);
            RectPosition = new Vector2(content.RectMinSize.x-82, 6);
        }
    }
}
