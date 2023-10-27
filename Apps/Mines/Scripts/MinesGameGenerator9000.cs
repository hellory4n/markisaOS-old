using Godot;
using System;
using System.Linq;

public class MinesGameGenerator9000 : Node {
    readonly Texture Mine = ResourceLoader.Load<Texture>("res://Apps/Mines/Assets/OhNoes.png");
    readonly Texture[] NumberStuff = new Texture[]{
        ResourceLoader.Load<Texture>("res://Apps/Mines/Assets/One.png"),
        ResourceLoader.Load<Texture>("res://Apps/Mines/Assets/Two.png"),
        ResourceLoader.Load<Texture>("res://Apps/Mines/Assets/Three.png"),
    };
    Random random = new Random();
    [Export]
    Vector2 Grid = new Vector2(9, 9);
    [Export]
    int Mines = 10;

    public override void _Ready() {
        base._Ready();
        // first put the mines in places
        Vector2[] mines = new Vector2[Mines];
        for (int i = 0; i < Mines; i++) {
            Vector2 fun = new Vector2(random.Next(1, (int)Grid.x), random.Next(1, (int)Grid.x));
            mines[i] = fun;
            GetNode<TextureRect>($"../Stuff/{fun.x}x{fun.y}").Texture = Mine;
        }

        // then we put the number stuff :)
        foreach (var mine in mines) {
            // we do the loop backwards cuz closer mines get priority or something
            for (int i = 3; i >= 1 ; i--) {
                // we need to check the thing in all 8 sides of the mine lol
                Vector2[] epicPlaces = new Vector2[] {
                    new Vector2(mine.x+i, mine.y),
                    new Vector2(mine.x+i, mine.y+i),
                    new Vector2(mine.x, mine.y+i),
                    new Vector2(mine.x-i, mine.y+i),
                    new Vector2(mine.x-i, mine.y),
                    new Vector2(mine.x-i, mine.y-i),
                };

                foreach (var place in epicPlaces) {
                    TextureRect thingy1 = GetNodeOrNull<TextureRect>($"../Stuff/{place.x}x{place.y}");
                    if (thingy1 != null && !mines.Contains(place))
                        thingy1.Texture = NumberStuff[i-1];
                }
            }
        }
    }
}
