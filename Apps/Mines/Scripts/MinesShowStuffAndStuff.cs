using Godot;
using System;
using System.Linq;

public class MinesShowStuffAndStuff : TextureButton {
    Vector2 Position;
    Texture OhNoes;
    Texture[] NumberStuff;
    Texture Nothingness;

    public override void _Ready() {
        base._Ready();
        // figure out the position based on the name
        // i could just make it show the variable on the inspector but manually changing it for 81 items 
        // is not ideal
        string[] yes = Name.Split("x");
        Position = new Vector2(int.Parse(yes[0]), int.Parse(yes[1]));

        // loading the same 5 textures 82 times also isn't ideal
        var bruh = GetNode<MinesGameGenerator9000>("../../../../GameGenerator9000");
        OhNoes = bruh.Mine;
        NumberStuff = bruh.NumberStuff;
        Nothingness = bruh.Nothingness;

        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // so true
        var square = GetNode<TextureRect>($"../../Stuff/{Position.x}x{Position.y}");
        var bruh = GetNode<MinesGameGenerator9000>("../../../../GameGenerator9000");

        // if it's a number we just reveal this shit
        if (NumberStuff.Contains(square.Texture)) {
            TextureNormal = Nothingness;
            bruh.ShownStuff++;
        // oh noes
        } else if (square.Texture == OhNoes) {
            GetParent<GridContainer>().Visible = false;
        // this is an empty square, try showing as many empty squares as possible
        } else {
            ShowEmptySquare(bruh, Position);
        }
    }

    public void ShowEmptySquare(MinesGameGenerator9000 bruh, Vector2 epicPosition) {
        var square = GetNode<TextureRect>($"../../Stuff/{epicPosition.x}x{epicPosition.y}");
        
        if (NumberStuff.Contains(square.Texture)) {
            return;
        }

        GetNode<TextureButton>($"../{epicPosition.x}x{epicPosition.y}").TextureNormal = Nothingness;
        bruh.ShownStuff++;
        
        // then try checking all of the corners stuff
        Vector2[] epicPlaces = new Vector2[] {
            new Vector2(epicPosition.x+1, epicPosition.y),
            new Vector2(epicPosition.x+1, epicPosition.y+1),
            new Vector2(epicPosition.x, epicPosition.y+1),
            new Vector2(epicPosition.x-1, epicPosition.y+1),
            new Vector2(epicPosition.x-1, epicPosition.y),
            new Vector2(epicPosition.x-1, epicPosition.y-1),
            new Vector2(epicPosition.x, epicPosition.y-1),
            new Vector2(epicPosition.x+1, epicPosition.y-1)
        };

        foreach (var place in epicPlaces) {
            // would be inconvenient if the square didn't exist
            if (GetNodeOrNull($"../{place.x}x{place.y}") == null)
                continue;
            
            ShowEmptySquare(bruh, place);
        }
    }
}
