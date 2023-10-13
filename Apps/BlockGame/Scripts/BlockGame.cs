using Godot;
using System;
using System.Collections.Generic;

public class BlockGame : Control {
    readonly Texture TPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/TPiece.png");
    readonly Texture LPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/LPiece.png");
    readonly Texture SPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/SPiece.png");
    readonly Texture SquarePiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/SquarePiece.png");
    readonly Texture StraightPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/StraightPiece.png");
    enum Pieces {
        T,
        L,
        S,
        Square,
        Straight
    }

    public override void _Ready() {
        base._Ready();
        foreach (var h in MakePiece(Pieces.T, new Vector2(60, 0))) {
            AddChild(h);
        }
    }

    List<TextureRect> MakePiece(Pieces piece, Vector2 position) {
        List<TextureRect> m = new List<TextureRect>();
        // this is definitely one way of making pieces
        switch (piece) {
            case Pieces.T:
                var t1 = new TextureRect {
                    RectPosition = position,
                    Texture = TPiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var t2 = new TextureRect {
                    RectPosition = position + new Vector2(20, 0),
                    Texture = TPiece,
                    Name = $"{(position.x/20)+1},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var t3 = new TextureRect {
                    RectPosition = position + new Vector2(40, 0),
                    Texture = TPiece,
                    Name = $"{(position.x/20)+2},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var t4 = new TextureRect {
                    RectPosition = position + new Vector2(20, 20),
                    Texture = TPiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    t1,
                    t2,
                    t3,
                    t4
                });
                break;
            case Pieces.L:
                var l1 = new TextureRect {
                    RectPosition = position,
                    Texture = LPiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var l2 = new TextureRect {
                    RectPosition = position + new Vector2(0, 20),
                    Texture = LPiece,
                    Name = $"{position.x/20},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                var l3 = new TextureRect {
                    RectPosition = position + new Vector2(0, 40),
                    Texture = LPiece,
                    Name = $"{position.x/20},{(position.y/20)+2}",
                    RectSize = new Vector2(20, 20)
                };
                var l4 = new TextureRect {
                    RectPosition = position + new Vector2(20, 40),
                    Texture = LPiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    l1,
                    l2,
                    l3,
                    l4
                });
                break;
            case Pieces.S:
                var s1 = new TextureRect {
                    RectPosition = position,
                    Texture = SPiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var s2 = new TextureRect {
                    RectPosition = position + new Vector2(0, 20),
                    Texture = SPiece,
                    Name = $"{position.x/20},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                var s3 = new TextureRect {
                    RectPosition = position + new Vector2(20, 20),
                    Texture = SPiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                var s4 = new TextureRect {
                    RectPosition = position + new Vector2(20, 40),
                    Texture = SPiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    s1,
                    s2,
                    s3,
                    s4
                });
                break;
            case Pieces.Straight:
                var straight1 = new TextureRect {
                    RectPosition = position,
                    Texture = StraightPiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var straight2 = new TextureRect {
                    RectPosition = position + new Vector2(20, 0),
                    Texture = StraightPiece,
                    Name = $"{(position.x/20)+1},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var straight3 = new TextureRect {
                    RectPosition = position + new Vector2(40, 0),
                    Texture = StraightPiece,
                    Name = $"{(position.x/20)+2},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var straight4 = new TextureRect {
                    RectPosition = position + new Vector2(60, 0),
                    Texture = StraightPiece,
                    Name = $"{(position.x/20)+3},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    straight1,
                    straight2,
                    straight3,
                    straight4
                });
                break;
            case Pieces.Square:
                var square1 = new TextureRect {
                    RectPosition = position,
                    Texture = SquarePiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var square2 = new TextureRect {
                    RectPosition = position + new Vector2(20, 0),
                    Texture = SquarePiece,
                    Name = $"{(position.x/20)+1},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var square3 = new TextureRect {
                    RectPosition = position + new Vector2(0, 20),
                    Texture = SquarePiece,
                    Name = $"{position.x/20},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                var square4 = new TextureRect {
                    RectPosition = position + new Vector2(20, 20),
                    Texture = SquarePiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    square1,
                    square2,
                    square3,
                    square4
                });
                break;
        }
        return m;
    }
}
