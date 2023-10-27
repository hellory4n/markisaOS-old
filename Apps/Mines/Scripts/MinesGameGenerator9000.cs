using Godot;
using System;

public class MinesGameGenerator9000 : Node {
    readonly Texture Mine = ResourceLoader.Load<Texture>("res://Apps/Mines/Assets/OhNoes.png");
    Random random = new Random();

    public override void _Ready() {
        base._Ready();
        // first put the mines in places
        Vector2[] mines = new Vector2[10];

        for (int i = 0; i < mines.Length; i++) {
            Vector2 fun = new Vector2(random.Next(1, 10), random.Next(1, 10));
            mines[i] = fun;
            GetNode<TextureRect>($"../Stuff/{fun.x}x{fun.y}").Texture = Mine;
        }
    }
}
