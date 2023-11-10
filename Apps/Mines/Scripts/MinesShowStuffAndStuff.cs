using Godot;
using System;
using System.Linq;

namespace Mines;

public partial class MinesShowStuffAndStuff : TextureButton {
    Vector2 MinePosition;
    Texture2D OhNoes;
    Texture2D[] NumberStuff;
    Texture2D Nothingness;
    Texture2D EmptySquare;

    public override void _Ready() {
        base._Ready();
        // figure out the position based on the name
        // i could just make it show the variable on the inspector but manually changing it for 81 items 
        // is not ideal
        string[] yes = Name.ToString().Split("X");
        Position = new Vector2(int.Parse(yes[0]), int.Parse(yes[1]));

        // loading the same 5 textures 82 times also isn't ideal
        var bruh = GetNode<MinesGameGenerator9000>("../../../../GameGenerator9000");
        OhNoes = bruh.Mine;
        NumberStuff = bruh.NumberStuff;
        Nothingness = bruh.Nothingness;
        EmptySquare = bruh.EmptySquare;

        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // so true
        var square = GetNode<TextureRect>($"../../Stuff/{Position.X}X{Position.Y}");
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
        var square = GetNode<TextureRect>($"../../Stuff/{epicPosition.X}X{epicPosition.Y}");
        var squareButButton = GetNode<TextureButton>($"../{epicPosition.X}X{epicPosition.Y}");
        
        if (square.Texture != EmptySquare || squareButButton.TextureNormal == Nothingness) {
            return;
        }

        squareButButton.TextureNormal = Nothingness;
        bruh.ShownStuff++;
        
        // then try checking all of the corners stuff
        Vector2[] epicPlaces = new Vector2[] {
            new(epicPosition.X+1, epicPosition.Y),
            new(epicPosition.X, epicPosition.Y+1),
            new(epicPosition.X-1, epicPosition.Y),
            new(epicPosition.X, epicPosition.Y-1),
        };

        foreach (var place in epicPlaces) {
            // would be inconvenient if the square didn't exist
            if (GetNodeOrNull($"../{place.X}X{place.Y}") == null)
                continue;
            
            ShowEmptySquare(bruh, place);
        }
    }
}
